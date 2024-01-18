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
        public static string host;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string currentFolderPath = Directory.GetCurrentDirectory();
            string scriptPath = currentFolderPath + @"\host.config";
            Console.WriteLine(scriptPath);
            host = File.ReadAllText(scriptPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frLogin());
        }
    }
}
