using CCCD_Client.Component;
using CCCD_Client.Model;
using DemoReader;
using MetroFramework.Forms;
using Newtonsoft.Json;
using PCSC;
using sharp.reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client
{
    public partial class frMain : MetroForm
    {

        private Identity data;
        private LoginModel model;
        public static SqlConnection connection { get; set; }
        public static List<ScanIdViewModel> listDataToSort { get; set; }

        public static string sortBy { get; set; }

        public static bool isAscending { get; set; }

        usrScan userScanComp;

        usrData userDataComp;
        public frMain(LoginModel _model)
        {

            try
            {
                //string connectionString = @"  ";
                //string host = @"http://117.4.247.68:18003/api/v1";
                //string test = encryptString(connectionString + "$!$" + host, "th1sk3yisv3ry5tr0n9@ndy0un3v3rkn0w1t!");
                //string test_ = decryptedString("+Mq/5bH4MHqWpI8an3uG9fvOouaz5Tp5naeeDY95juXp26XltuQzeJemmQiKeoX0/Mml4w==", "th1sk3yisv3ry5tr0n9@ndy0un3v3rkn0w1t!");
                connection = new SqlConnection(frLogin.connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                return;
            }

            isAscending = true;
            sortBy = "";
            this.model = _model;
            InitializeComponent();

            userScanComp = new usrScan(_model);
            tabScan.Controls.Add(userScanComp);
            userScanComp.Dock = DockStyle.Fill;

            userDataComp = new usrData(model);
            tabData.Controls.Add(userDataComp);
            userDataComp.Dock = DockStyle.Fill;

            this.Activate();
            this.ActiveControl = tabControl;
            tabControl.SelectTab("tabScan");

            tabControl.SelectedIndex = 0;

            userScanComp.ButtonClicked += ucScan_ButtonClicked;


        }

        private void frMain_Load(object sender, EventArgs e)
        {
            try
            {
                SmartCard_.getInstance().StartMonitoring();
            }
            catch (Exception ex)
            {

            }
            this.Activate();
            tabControl.SelectTab("tabScan");

            // Find the user control on the TabPage
            var userControl = tabControl.SelectedTab.Controls.Find("usrScan", true).FirstOrDefault() as UserControl;
            if (userControl != null)
            {
                // Find the TextBox control on the user control
                var textBox = userControl.Controls.Find("txtCCCD", true).FirstOrDefault() as MetroFramework.Controls.MetroTextBox;
                if (textBox != null)
                {
                    // Set focus to the TextBox control
                    textBox.Select();
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void frMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SmartCard_.getInstance().StopMonitoring();
            }
            catch (Exception ex)
            {

            }
        }

        private void tabData_Enter(object sender, EventArgs e)
        {
            
        }


        private void frMain_Shown(object sender, EventArgs e)
        {

        }

        private void tabControl_TabIndexChanged_1(object sender, EventArgs e)
        {

        }

        public List<ScanIdViewModel> toView()
        {
            using (SqlCommand command = new SqlCommand("SELECT DATA_ID, ID_NUMBER, FULL_NAME, DATE_OF_BIRTH, SEX, PLACE_OF_ORIGIN, PLACE_OF_RESIDENCE, DATE_EXPIRED, DATE_ISSUE FROM scan_id ORDER BY DATE_SCAN DESC", frMain.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<ScanIdViewModel> objects = new List<ScanIdViewModel>();

                    while (reader.Read())
                    {
                        ScanIdViewModel obj = new ScanIdViewModel
                        {
                            dataId = (string)reader["DATA_ID"],
                            idNumber = (string)reader["ID_NUMBER"],
                            fullName = (string)reader["FULL_NAME"],
                            dateOfBirth = ((DateTime)reader["DATE_OF_BIRTH"]).ToString("dd/MM/yyyy"),
                            sex = (int)reader["SEX"] == 1 ? "Nam" : "Nữ",
                            placeOfOrigin = (string)reader["PLACE_OF_ORIGIN"],
                            placeOfResidence = (string)reader["PLACE_OF_RESIDENCE"],
                            dateExpired = ((DateTime)reader["DATE_EXPIRED"]).ToString("dd/MM/yyyy"),
                            dateIssue = ((DateTime)reader["DATE_ISSUE"]).ToString("dd/MM/yyyy")
                        };

                        objects.Add(obj);
                    }
                    return objects;

                }
            }
        }

        private string decryptedString(string encryptedString, string encryptionKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] hashedKey = md5Crypto.ComputeHash(keyBytes);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(encryptedBytes[i] ^ hashedKey[i % hashedKey.Length]);
            }

            string decryptedString = Encoding.UTF8.GetString(encryptedBytes);
            return decryptedString;
        }

        private string encryptString(string inputString, string encryptionKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] hashedKey = md5Crypto.ComputeHash(keyBytes);

            byte[] connectionStringBytes = Encoding.UTF8.GetBytes(inputString);
            for (int i = 0; i < connectionStringBytes.Length; i++)
            {
                connectionStringBytes[i] = (byte)(connectionStringBytes[i] ^ hashedKey[i % hashedKey.Length]);
            }

            string encryptedString = Convert.ToBase64String(connectionStringBytes);
            return encryptedString;
        }

        private void ucScan_ButtonClicked()
        {
            userDataComp.UpdateGrid();
        }
    }
}
