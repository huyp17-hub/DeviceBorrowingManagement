/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

namespace DeviceBorrowingManagement
{
    public class ProjectorRemote : TeachingDevice
    {
        public ProjectorRemote(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, int maxAllowedDays = 2, DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, maxAllowedDays, status) { }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            // Chi phí bảo trì 7% giá trị máy
            return PurchasePrice * 0.07m;
        }
    }
}
