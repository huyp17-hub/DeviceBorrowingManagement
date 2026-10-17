/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class Microphone : TeachingDevice
    {
        public Microphone(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, int maxAllowedDays = 1, DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, maxAllowedDays, status) { }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            // Micro cần kiểm tra màng thu và tần số sóng: 12% giá trị
            return PurchasePrice * 0.12m;
        }
    }
}
