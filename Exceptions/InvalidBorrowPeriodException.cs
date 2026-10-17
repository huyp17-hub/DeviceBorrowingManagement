/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class InvalidBorrowPeriodException : BorrowingException
    {
        public string DeviceId { get; }
        public int RequestedDays { get; }
        public int MaxAllowedDays { get; }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxDays, string message)
            : base(message)
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaxAllowedDays = maxDays;
        }
    }
}
