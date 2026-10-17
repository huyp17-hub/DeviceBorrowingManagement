/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class AirConditionerRemote : TeachingDevice
    {
        public AirConditionerRemote(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, int maxAllowedDays = 3, DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, maxAllowedDays, status) { }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            // Chi phí bảo trì 5% giá trị máy + chi phí thay pin định kỳ (50,000 VND)
            return (PurchasePrice * 0.05m) + 50000m;
        }
    }
}
