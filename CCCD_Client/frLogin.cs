using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCCD_Client.Model;
using MetroFramework;
using Newtonsoft.Json;
using PCSC;

namespace CCCD_Client
{
    public partial class frLogin : MetroFramework.Forms.MetroForm
    {
        static HttpClient client = new HttpClient();

        List<ScanIdViewModel> listToSort = new List<ScanIdViewModel>();

        public static string Host { get; set; }
        public static string connectionString { get; set; }
        public static List<string> devices { get; set; }
        public static DateTime dateLicense { get; set; }

        public frLogin()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Vinorsoft";

            if(!File.Exists(filePath + @"\Template.docx"))
            {
                try
                {
                    File.Copy("Template.docx", filePath + @"\Template.docx");
                }
                catch
                {
                    MessageBox.Show("Không tồn tại file mẫu. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                }
            }

            if (!File.Exists(filePath + @"\host.conf"))
            {
                DialogResult res = MessageBox.Show("File cấu hình chưa tồn tại. Bạn cần thiết lập cấu hình!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    DirectoryInfo directory = new DirectoryInfo(filePath);
                    directory.Create();
                    FileStream file = File.Create(filePath + @"\host.conf");
                    file.Close();
                }
                else
                {
                    this.Dispose();
                }

                var process = Process.Start(filePath + @"\host.conf");
                process.WaitForExit();
            }
            bool isValidConfig = false;
            while (!isValidConfig)
            {
                try
                {
                    string encryptedData = File.ReadAllText(filePath + @"\host.conf") ;
                    string[] separators = { "$!$" };
                    string[] data = decryptedString(encryptedData, "th1sk3yisv3ry5tr0n9@ndy0un3v3rkn0w1t!").Split(separators, StringSplitOptions.None);
                    connectionString = data.FirstOrDefault();

                    SqlConnection cnn = new SqlConnection(connectionString);
                    try
                    {
                        cnn.Open();
                        cnn.Close();
                    }
                    catch (Exception ex)
                    {
                        DialogResult res = MessageBox.Show("Thông tin kết nỗi database bị lỗi.\n Bạn cần chỉnh sửa file cấu hình!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {
                            var process = Process.Start("notepad.exe", filePath + @"\host.conf");
                            process.WaitForExit();
                            isValidConfig = false;
                            continue;
                        }
                        else
                        {
                            this.Dispose();
                        }
                    }

                    Host = data.LastOrDefault();
                    isValidConfig = true;
                    InitializeComponent();
                }
                catch (Exception ex)
                {

                    DialogResult res = MessageBox.Show("Thông tin kết nỗi bị lỗi.\nVui lòng kiểm tra lại file cấu hình!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        var process = Process.Start("notepad.exe", filePath + @"\host.conf");
                        process.WaitForExit();
                        isValidConfig = false;
                        continue;
                    }
                    else
                    {
                        this.Dispose();
                    }
                }
                isValidConfig = true;
            }


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            doLogin();
        }

        private void doLogin()
        {
            lbErrLogin.Text = "";
            if (txtUsername.Text == "")
            {
                lbErrLogin.Text = "Tên đăng nhập không được để trống!";
                return;
            }

            if (txtPassword.Text == "")
            {
                lbErrLogin.Text = "Mật khẩu không được để trống!";
                return;
            }


            string path = Host + "/authentication/login";
            loginValue login = new loginValue();
            login.username = txtUsername.Text;
            login.password = txtPassword.Text;

            try
            {
                var loginResult = LoginAsync(path, login);
                if (loginResult.code == 403)
                {
                    lbErrLogin.Text = "Tài khoản của bạn chưa được kích hoạt.\nVui lòng liên hệ với bộ phận CSKH của Vinorsoft!";
                    return;
                }

                if (loginResult.status == 401)
                {
                    lbErrLogin.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    return;
                }

                //Xử lý chuỗi valid
                string[] separators = { "$!$" };
                string[] data = decryptedString(loginResult.data, "th1sk3yisv3ry5tr0n9@ndy0un3v3rkn0w1t!").Split(separators, StringSplitOptions.None);
                DateTime dateLicense = DateTime.ParseExact(data[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                devices = new List<string>();
                for (int i = 1; i < data.Length; i++)
                {
                    devices.Add(data[i]);
                }

                DateTime localDateTime;
                var client_time = new TcpClient("time.nist.gov", 13);
                using (var streamReader = new StreamReader(client_time.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    var utcDateTimeString = response.Substring(7, 17);
                    localDateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }



                if (dateLicense < localDateTime)
                {
                    MessageBox.Show("Bản quyền hết hạn. Vui lòng liên hệ với bộ phận CSKH của Vinorsoft để được hỗ trợ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Hide();
                frMain _frmain = new frMain(loginResult);
                _frmain.ShowDialog();
                this.Close();

            }
            catch (Exception ex)
            {
                lbErrLogin.Text = "Đăng nhập thất bại. Vui lòng thử lại!";
            }
        }


        static LoginModel LoginAsync(string path, loginValue login)
        {
            var requestBodyJson = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(path, httpContent).Result;
            var loginModel = JsonConvert.DeserializeObject<LoginModel>(response.Content.ReadAsStringAsync().Result);
            return loginModel;
        }

        private void frLogin_Shown(object sender, EventArgs e)
        {


        }

        private void frLogin_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    doLogin();
            //}
        }

        private void frLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                doLogin();
            }
        }

        private void frLogin_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.txtUsername.Focus();
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
    }
}
