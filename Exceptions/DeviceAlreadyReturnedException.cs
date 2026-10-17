/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class DeviceAlreadyReturnedException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceAlreadyReturnedException(string deviceId)
            : base($"Thiết bị [{deviceId}] đã được trả trước đó.")
        {
            DeviceId = deviceId;
        }
    }
}
