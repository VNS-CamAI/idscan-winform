using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCCD_Client.Model;
using MetroFramework;
using Newtonsoft.Json;

namespace CCCD_Client
{
    public partial class frLogin : MetroFramework.Forms.MetroForm
    {
        static HttpClient client = new HttpClient();
        public frLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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


            string path = Program.host + "/authentication/login";
            loginValue login = new loginValue();
            login.username = txtUsername.Text;
            login.password = txtPassword.Text;

            try
            {
                Console.WriteLine(path);
                Console.WriteLine(login);
                var loginResult = LoginAsync(path, login);
                if (loginResult.status == 401)
                {
                    lbErrLogin.Text = "Đăng nhập thất bại. Vui lòng thử lại!";
                    return;
                }
                this.Hide();
                frMain _frmain = new frMain(loginResult);
                _frmain.ShowDialog();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

    }
}
