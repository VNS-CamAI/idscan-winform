using PCSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!GetStatus())
            {
                DialogResult result = MessageBox.Show("Bạn phải kết nối với thiết bị trước khi khởi động chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Application.Run(new frLogin());
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private static bool GetStatus()
        {
            try
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("CCCD_Client");
                int a = 1;

                int nProcessID = System.Diagnostics.Process.GetCurrentProcess().Id;

                foreach (System.Diagnostics.Process process in processes)
                {
                    if (process.Id != nProcessID)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: Vui lòng kết nối lại thiết bị và khởi động lại phần mềm");
            }

            var context = new SCardContext();
            context.Establish(SCardScope.System);
            var readerNames = context.GetReaders();
            var readerName = readerNames.FirstOrDefault();

            if (readerName == null)
            {
                Console.WriteLine("No reader found.");
                return false;
            }
            return true;
        }
    }
}
