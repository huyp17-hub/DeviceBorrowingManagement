/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public interface IBorrower
    {
        string BorrowerId { get; }
        string FullName { get; }
        int GetMaximumDevices();
    }
}
