/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceBorrowingManagement
{
    /// <summary>
    /// Lớp phòng học kết tập danh sách thiết bị
    /// </summary>
    public class ClassRoom
    {
        public string RoomId { get; }
        public string RoomName { get; }
        public int Capacity { get; }
        private readonly List<Device> _devices = new();

        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

        public ClassRoom(string roomId, string roomName, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomId)) throw new ArgumentException("Mã phòng không hợp lệ.", nameof(roomId));
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Không chấp nhận thiết bị null.");
            if (_devices.Any(d => d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Phòng đã có thiết bị mã [{device.DeviceId}].");

            _devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            var dev = FindDevice(deviceId);
            return dev != null && _devices.Remove(dev);
        }

        public Device FindDevice(string deviceId)
        {
            return _devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tính tổng chi phí bảo trì hằng năm bằng tính đa hình
        /// </summary>
        public decimal CalculateAnnualMaintenanceCost()
        {
            return _devices.Sum(d => d.CalculateAnnualMaintenanceCost());
        }

        /// <summary>
        /// Lọc thiết bị cần bảo trì: Đang UnderMaintenance hoặc đưa vào dùng trên 5 năm
        /// </summary>
        public List<Device> GetDevicesRequiringMaintenance()
        {
            int currentYear = DateTime.Now.Year;
            return _devices.Where(d => d.Status == DeviceStatus.UnderMaintenance || (currentYear - d.YearOfUsage) > 5).ToList();
        }
    }
}
