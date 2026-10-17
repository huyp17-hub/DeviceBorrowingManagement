/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class DeviceNotInSlipException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceNotInSlipException(string deviceId)
            : base($"Thiết bị [{deviceId}] không tồn tại trong phiếu mượn này.")
        {
            DeviceId = deviceId;
        }
    }
}
