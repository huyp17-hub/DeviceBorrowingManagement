/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;

namespace DeviceBorrowingManagement
{
    /// <summary>
    /// Ngoại lệ cơ sở cho toàn bộ nghiệp vụ mượn/trả thiết bị
    /// </summary>
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) : base(message) { }
        public BorrowingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
