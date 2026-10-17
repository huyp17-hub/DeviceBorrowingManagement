/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;

namespace DeviceBorrowingManagement
{
    /// <summary>
    /// Thiết bị phục vụ giảng dạy mượn trả
    /// </summary>
    public abstract class TeachingDevice : Device
    {
        public int MaxAllowedDays { get; }

        protected TeachingDevice(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, int maxAllowedDays, DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, status)
        {
            if (maxAllowedDays <= 0)
                throw new ArgumentException("Số ngày mượn tối đa phải lớn hơn 0.", nameof(maxAllowedDays));
            MaxAllowedDays = maxAllowedDays;
        }

        public void MarkAsBorrowed()
        {
            Status = DeviceStatus.Borrowed;
        }

        public void MarkAsAvailable()
        {
            Status = DeviceStatus.Available;
        }
    }
}
