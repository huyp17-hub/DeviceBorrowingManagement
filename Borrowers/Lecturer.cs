/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class Lecturer : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }

        public Lecturer(string id, string name)
        {
            BorrowerId = id;
            FullName = name;
        }

        public int GetMaximumDevices() => 3;
    }
}
