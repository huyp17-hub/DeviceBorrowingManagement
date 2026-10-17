/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }

        public BorrowLimitExceededException(string borrowerId, int maxDevices)
            : base($"Người mượn [{borrowerId}] đã vượt quá số lượng thiết bị tối đa ({maxDevices}).")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maxDevices;
        }
    }
}
