/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class DeviceUnavailableException : BorrowingException
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceUnavailableException(string deviceId, DeviceStatus status)
            : base($"Thiết bị [{deviceId}] hiện không sẵn sàng (Trạng thái: {status}).")
        {
            DeviceId = deviceId;
            CurrentStatus = status;
        }
    }
}
