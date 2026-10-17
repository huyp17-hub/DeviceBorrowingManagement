/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;

namespace DeviceBorrowingManagement
{
    public class BorrowItem
    {
        public TeachingDevice Device { get; }
        public DateTime BorrowedDate { get; }
        public DateTime DueDate { get; }
        public int FromPeriod { get; set; }
        public int ToPeriod { get; set; }
        public DateTime? ReturnedDate { get; private set; }
        public bool IsReturned => ReturnedDate.HasValue;

        public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate, int fromPeriod = 1, int toPeriod = 3)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            if (dueDate < borrowedDate)
                throw new ArgumentException("Hạn trả (DueDate) phải sau hoặc bằng ngày mượn.");
            if (toPeriod < fromPeriod)
                throw new ArgumentException("Tiết kết thúc phải lớn hơn hoặc bằng tiết bắt đầu.");

            BorrowedDate = borrowedDate;
            DueDate = dueDate;
            FromPeriod = fromPeriod;
            ToPeriod = toPeriod;
            ReturnedDate = null;
        }

        public void Return(DateTime returnedDate)
        {
            if (IsReturned)
                throw new DeviceAlreadyReturnedException(Device.DeviceId);
            if (returnedDate < BorrowedDate)
                throw new BorrowingException($"Ngày trả ({returnedDate:d}) không được trước ngày mượn ({BorrowedDate:d}).");

            ReturnedDate = returnedDate;
            Device.MarkAsAvailable();
        }

        public bool IsLate()
        {
            if (ReturnedDate.HasValue)
                return ReturnedDate.Value.Date > DueDate.Date;
            return DateTime.Now.Date > DueDate.Date;
        }

        public int GetLateDays()
        {
            DateTime checkDate = ReturnedDate ?? DateTime.Now;
            if (checkDate.Date > DueDate.Date)
                return (checkDate.Date - DueDate.Date).Days;
            return 0;
        }
    }
}
