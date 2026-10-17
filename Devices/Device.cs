/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;

namespace DeviceBorrowingManagement
{
    /// <summary>
    /// Lớp trừu tượng định nghĩa thông tin thiết bị
    /// </summary>
    public abstract class Device
    {
        public string DeviceId { get; }
        public string DeviceName { get; set; }
        public int YearOfUsage { get; }
        public decimal PurchasePrice { get; }
        public DeviceStatus Status { get; protected set; }

        protected Device(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, DeviceStatus status = DeviceStatus.Available)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));
            if (purchasePrice <= 0)
                throw new ArgumentException("Giá mua phải lớn hơn 0.", nameof(purchasePrice));
            if (yearOfUsage > DateTime.Now.Year)
                throw new ArgumentException("Năm đưa vào sử dụng không được lớn hơn năm hiện tại.", nameof(yearOfUsage));

            DeviceId = deviceId;
            DeviceName = deviceName;
            YearOfUsage = yearOfUsage;
            PurchasePrice = purchasePrice;
            Status = status;
        }

        public void SetStatus(DeviceStatus newStatus)
        {
            Status = newStatus;
        }

        /// <summary>
        /// Tính chi phí bảo trì dự kiến hàng năm (Đa hình)
        /// </summary>
        public abstract decimal CalculateAnnualMaintenanceCost();

        public override string ToString()
        {
            return $"[{DeviceId}] {DeviceName} - Năm SD: {YearOfUsage} - Giá: {PurchasePrice:N0} VND - TT: {Status}";
        }
    }
}
