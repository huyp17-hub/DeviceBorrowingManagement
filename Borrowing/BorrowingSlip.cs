/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceBorrowingManagement
{
    public class BorrowingSlip
    {
        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }

        private readonly List<BorrowItem> _items = new();
        public IReadOnlyList<BorrowItem> Items => _items.AsReadOnly();

        public const decimal LateFeePerDay = 20000m; // Phí phạt trả trễ: 20k/ngày

        public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
        {
            if (string.IsNullOrWhiteSpace(slipId)) throw new ArgumentException("Mã phiếu không được trống.", nameof(slipId));
            if (string.IsNullOrWhiteSpace(teachingRoom)) throw new ArgumentException("Phòng dạy không được trống.", nameof(teachingRoom));
            Borrower = borrower ?? throw new ArgumentNullException(nameof(borrower));

            SlipId = slipId;
            TeachingRoom = teachingRoom;
            BorrowedDate = borrowedDate;
        }

        private void ValidateBorrowing(TeachingDevice device, int borrowDays)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiết bị không được null.");
            if (borrowDays <= 0)
                throw new InvalidBorrowPeriodException(device.DeviceId, borrowDays, device.MaxAllowedDays, "Số ngày mượn phải lớn hơn 0.");
            if (borrowDays > device.MaxAllowedDays)
                throw new InvalidBorrowPeriodException(device.DeviceId, borrowDays, device.MaxAllowedDays,
                    $"Thiết bị [{device.DeviceId}] chỉ cho phép mượn tối đa {device.MaxAllowedDays} ngày. Yêu cầu: {borrowDays} ngày.");
            if (_items.Any(i => i.Device.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateDeviceException(device.DeviceId);
            if (_items.Count >= Borrower.GetMaximumDevices())
                throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
            if (device.Status != DeviceStatus.Available)
                throw new DeviceUnavailableException(device.DeviceId, device.Status);
        }

        /// <summary>
        /// Thêm thiết bị vào phiếu đảm bảo tính Atomic (Nhất quán)
        /// </summary>
        public void AddDevice(TeachingDevice device, int borrowDays)
        {
            // 1. Kiểm tra toàn bộ điều kiện trước
            ValidateBorrowing(device, borrowDays);

            // 2. Tạo đối tượng mục mượn
            var item = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));

            // 3. Thay đổi trạng thái thiết bị và cập nhật danh sách
            device.MarkAsBorrowed();
            _items.Add(item);
        }

        /// <summary>
        /// Trả thiết bị và tính tiền phạt trễ hạn (nếu có)
        /// </summary>
        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không hợp lệ.", nameof(deviceId));

            var item = _items.FirstOrDefault(i => i.Device.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase));
            if (item == null)
                throw new DeviceNotInSlipException(deviceId);

            if (item.IsReturned)
                throw new DeviceAlreadyReturnedException(deviceId);

            item.Return(returnedDate);

            int lateDays = item.GetLateDays();
            return lateDays * LateFeePerDay;
        }

        public decimal CalculateTotalLateFee()
        {
            return _items.Sum(i => i.GetLateDays() * LateFeePerDay);
        }
    }
}
