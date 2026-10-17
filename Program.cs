/****************/
// Mã sinh viên: 202418915
// Họ tên:       Phạm Quốc Huy
/****************/

using System;

namespace DeviceBorrowingManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=================================================================");
            Console.WriteLine("    HỆ THỐNG QUẢN LÝ THIẾT BỊ VÀ PHIẾU MƯỢN TRẢ GIẢNG DẠY         ");
            Console.WriteLine("=================================================================\n");

            // --- 11.1. LUỒNG THÀNH CÔNG ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(">>> 11.1 KIỂM THỬ: LUỒNG THÀNH CÔNG <<<");
            Console.ResetColor();

            try
            {
                // 1. Tạo giảng viên
                var lecturer = new Lecturer("GV001", "Thầy Phạm Quốc Huy");
                Console.WriteLine($"1. Khởi tạo người mượn: {lecturer.FullName} (Hạn mức: {lecturer.GetMaximumDevices()} thiết bị)");

                // 2. Tạo thiết bị
                var remoteAC = new AirConditionerRemote("AC_01", "Điều khiển Daikin", 2024, 250000m, maxAllowedDays: 3);
                var mic = new Microphone("MIC_01", "Micro không dây Shure", 2025, 1200000m, maxAllowedDays: 2);
                var remoteProjector = new ProjectorRemote("PRJ_01", "Điều khiển máy chiếu Panasonic", 2023, 350000m, maxAllowedDays: 2);
                Console.WriteLine("2. Khởi tạo 3 thiết bị sẵn sàng.");

                // 3. Tạo phiếu mượn
                DateTime borrowDate = new DateTime(2026, 10, 10);
                var slip = new BorrowingSlip("PHIEU_01", lecturer, "Phòng 402 - Giảng đường A2", borrowDate);
                Console.WriteLine($"3. Tạo phiếu mượn: {slip.SlipId} tại {slip.TeachingRoom}");

                // 4. Thêm máy chiếu và micro vào phiếu
                slip.AddDevice(remoteProjector, 2); // Mượn 2 ngày
                slip.AddDevice(mic, 1);             // Mượn 1 ngày
                Console.WriteLine("4. Đã thêm [PRJ_01] và [MIC_01] vào phiếu thành công.");

                // 5. Trả máy chiếu đúng hạn (mượn ngày 10/10, hạn 12/10, trả 12/10)
                decimal feePrj = slip.ReturnDevice("PRJ_01", new DateTime(2026, 10, 12));
                Console.WriteLine($"5. Trả [PRJ_01] đúng hạn. Phí trễ: {feePrj:N0} VND.");

                // 6. Trả micro trễ 2 ngày (mượn 10/10, hạn 11/10, trả 13/10 => trễ 2 ngày)
                decimal feeMic = slip.ReturnDevice("MIC_01", new DateTime(2026, 10, 13));
                Console.WriteLine($"6. Trả [MIC_01] trễ hạn. Phí trễ: {feeMic:N0} VND.");

                // 7. Thống kê tình trạng
                Console.WriteLine("\n7. Thống kê danh sách thiết bị trong phiếu:");
                foreach (var item in slip.Items)
                {
                    string statusText = item.IsReturned
                        ? $"Đã trả ngày {item.ReturnedDate:dd/MM/yyyy} (Trễ {item.GetLateDays()} ngày)"
                        : "Chưa trả";
                    Console.WriteLine($"   - {item.Device.DeviceName} [{item.Device.DeviceId}] => {statusText}");
                }
                Console.WriteLine($"   => Tổng phí phạt của phiếu: {slip.CalculateTotalLateFee():N0} VND\n");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Lỗi không mong muốn]: {ex.Message}");
                Console.ResetColor();
            }

            // --- 11.2. LUỒNG PHÁT SINH NGOẠI LỆ ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(">>> 11.2 KIỂM THỬ: CÁC LUỒNG NGOẠI LỆ <<<");
            Console.ResetColor();

            // Tình huống 1: Mượn điều khiển đang UnderMaintenance
            RunTestCase("TH1: Mượn điều khiển điều hòa đang bảo trì (UnderMaintenance)", () =>
            {
                var ta = new TeachingAssistant("TA01", "Trợ giảng Lan");
                var acBroken = new AirConditionerRemote("AC_BROKEN", "Điều khiển hư", 2022, 200000m, 3, DeviceStatus.UnderMaintenance);
                var slip = new BorrowingSlip("P_ERR1", ta, "P.301", DateTime.Now);
                slip.AddDevice(acBroken, 1);
            });

            // Tình huống 2: Trợ giảng mượn thiết bị thứ 3 (Vượt hạn mức tối đa = 2)
            RunTestCase("TH2: Trợ giảng mượn vượt quá hạn mức (tối đa 2)", () =>
            {
                var ta = new TeachingAssistant("TA02", "Trợ giảng Nam");
                var d1 = new Microphone("M1", "Mic 1", 2025, 500000m, 2);
                var d2 = new Microphone("M2", "Mic 2", 2025, 500000m, 2);
                var d3 = new Microphone("M3", "Mic 3", 2025, 500000m, 2);
                var slip = new BorrowingSlip("P_ERR2", ta, "P.302", DateTime.Now);
                slip.AddDevice(d1, 1);
                slip.AddDevice(d2, 1);
                slip.AddDevice(d3, 1); // Quá hạn mức
            });

            // Tình huống 3: Thêm cùng một micro hai lần vào phiếu
            RunTestCase("TH3: Thêm trùng một thiết bị vào cùng phiếu mượn", () =>
            {
                var gv = new Lecturer("GV02", "Cô Mai");
                var mic = new Microphone("MIC_DUP", "Mic đơn", 2025, 600000m, 2);
                var slip = new BorrowingSlip("P_ERR3", gv, "P.201", DateTime.Now);
                slip.AddDevice(mic, 1);
                slip.AddDevice(mic, 1); // Trùng mã
            });

            // Tình huống 4: Mượn micro 3 ngày (trong khi giới hạn là 1 ngày)
            RunTestCase("TH4: Số ngày mượn lớn hơn giới hạn cho phép của thiết bị", () =>
            {
                var gv = new Lecturer("GV03", "Thầy Hoàng");
                var mic = new Microphone("MIC_SHORT", "Mic sự kiện", 2025, 800000m, maxAllowedDays: 1);
                var slip = new BorrowingSlip("P_ERR4", gv, "P.105", DateTime.Now);
                slip.AddDevice(mic, 3); // Giới hạn là 1 ngày
            });

            // Tình huống 5: Trả thiết bị không thuộc phiếu
            RunTestCase("TH5: Trả thiết bị không nằm trong phiếu mượn", () =>
            {
                var gv = new Lecturer("GV04", "Thầy Bình");
                var slip = new BorrowingSlip("P_ERR5", gv, "P.106", DateTime.Now);
                slip.ReturnDevice("DEVICE_KHONG_TON_TAI", DateTime.Now);
            });

            // Tình huống 6: Trả cùng một thiết bị hai lần
            RunTestCase("TH6: Trả cùng một thiết bị 2 lần", () =>
            {
                var gv = new Lecturer("GV05", "Cô Linh");
                var mic = new Microphone("MIC_DOUBLE", "Mic hội trường", 2025, 900000m, 2);
                var slip = new BorrowingSlip("P_ERR6", gv, "P.107", DateTime.Now);
                slip.AddDevice(mic, 1);
                slip.ReturnDevice("MIC_DOUBLE", DateTime.Now);
                slip.ReturnDevice("MIC_DOUBLE", DateTime.Now); // Trả lần 2
            });

            // Tình huống 7: Trả thiết bị với ngày trả trước ngày mượn
            RunTestCase("TH7: Trả thiết bị với ngày trả trước ngày mượn", () =>
            {
                var gv = new Lecturer("GV06", "Thầy Hùng");
                var mic = new Microphone("MIC_TIME", "Mic test", 2025, 900000m, 2);
                DateTime borrowDate = new DateTime(2026, 10, 15);
                var slip = new BorrowingSlip("P_ERR7", gv, "P.108", borrowDate);
                slip.AddDevice(mic, 1);
                slip.ReturnDevice("MIC_TIME", new DateTime(2026, 10, 10)); // Ngày trả trước ngày mượn
            });

            Console.WriteLine("\nHoàn tất kiểm thử tất cả kịch bản!");
        }

        private static void RunTestCase(string caseName, Action action)
        {
            Console.WriteLine($"\n[Kiểm tra] {caseName}");
            try
            {
                action();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  -> Không có ngoại lệ nào phát sinh.");
                Console.ResetColor();
            }
            catch (BorrowingException ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  -> Bắt được ngoại lệ nghiệp vụ dự kiến ({ex.GetType().Name}): {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  -> Bắt được ngoại lệ hệ thống khác ({ex.GetType().Name}): {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
