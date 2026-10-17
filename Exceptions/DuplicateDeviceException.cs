/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class DuplicateDeviceException : BorrowingException
    {
        public string DeviceId { get; }

        public DuplicateDeviceException(string deviceId)
            : base($"Thiết bị [{deviceId}] đã tồn tại trong danh sách phiếu mượn.")
        {
            DeviceId = deviceId;
        }
    }
}
