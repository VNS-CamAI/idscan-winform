using CCCD_Client.Model;
using DemoReader;
using MetroFramework.Forms;
using Newtonsoft.Json;
using sharp.reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client
{
    public partial class frMain : MetroForm
    {
        private LoginModel model;

        private Identity data;

        public frMain(LoginModel _model)
        {
            InitializeComponent();
            this.model = _model;
            lbName.Text = "Xin chào " + model.username + "!";
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void WriteLog(String log)
        {
            txtInformation.Text += log + "\r\n";
            Application.DoEvents();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {

            if (txtCCCD.Text == "")
            {
                lbError.Text = "Xin mời nhập số CCCD!";
                return;
            }

            if (txtCCCD.Text.Length != 12)
            {
                lbError.Text = "Số CCCD không hợp lệ!";
                return;
            }

            if (txtCCCD.Text != txtReCCCD.Text)
            {
                lbError.Text = "Xác nhận CCCD không đúng!";
                return;
            }
            lbError.Text = "";

            WriteLog("Đọc thẻ CCCD gắn chip, vui lòng chờ...");
            pbAvatar.Image = null;
            String cccdId = txtCCCD.Text;
            String strMrz = "";
           
            CardService_ service = CardService_.getInstance(null);
            IdcardReader idReader = new IdcardReader(service);
            if (!idReader.ReadData(cccdId, strMrz))
            {
                MessageBox.Show("Lỗi: " + idReader.getLastErrorMsg());
                return;
            }

            this.data = new Identity();

            var citi = idReader.getCitizenInfo();
            //Show infomation
            WriteLog("Số CCCD: " + citi.getCitizenPid());
            WriteLog("Số CMND cũ: " + citi.getOldIdentify());
            WriteLog("Họ tên: " + citi.getFullName());
            WriteLog("Giới tính: " + citi.getGender());
            WriteLog("Dân tộc: " + citi.getEthnic());
            WriteLog("Tôn giáo: " + citi.getReligion());
            WriteLog("Đặc điểm nhận dạng: " + citi.getIdentifyCharacteristics());
            WriteLog("Ngày sinh: " + citi.getBirthDate());
            WriteLog("Ngày cấp CCCD: " + citi.getDateProvide());
            WriteLog("Ngày hết hạn: " + citi.getOutOfDate());
            WriteLog("Quê quán: " + citi.getHomeTown());
            WriteLog("Hộ khẩu TT: " + citi.getRegPlaceAddress());
            WriteLog("Bố: " + citi.getFatherName());
            WriteLog("Mẹ: " + citi.getMotherName());
            WriteLog("Vợ/Chồng: " + citi.getHusBandName());
            WriteLog("Quê quán: " + citi.getHomeTown());


            data.idNumber = citi.getCitizenPid();
            data.oldIdNumber = citi.getOldIdentify();
            data.fullName = citi.getFullName();
            data.sex = citi.getGender().Equals("Nam") ? 1 : 0;
            data.ethnic = citi.getEthnic();
            data.religion = citi.getReligion();
            data.identification = citi.getIdentifyCharacteristics();
            data.dateOfBirth = DateTime.ParseExact(citi.getBirthDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            data.dateIssue = DateTime.ParseExact(citi.getDateProvide(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            data.dateExpired = DateTime.ParseExact(citi.getOutOfDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            data.placeOfOrigin = citi.getHomeTown();
            data.placeOfResidence = citi.getRegPlaceAddress();
            data.father = citi.getFatherName();
            data.mother = citi.getMotherName();
            data.mate = citi.getHusBandName();
            data.portraitImage = citi.getPhotoBase64();
            //WriteLog("Face image base64: " + citi.getPhotoBase64());
            try
            {
                // Convert base 64 string to byte[]
                byte[] imageBytes = Convert.FromBase64String(citi.getPhotoBase64());
                // Convert byte[] to Image
                using (var ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pbAvatar.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            btnPost.Enabled = true;
        }


        static responseNoData SendDataAsync(string path, Identity entity, string token)
        {
            HttpClient client = new HttpClient();
            var requestBodyJson = JsonConvert.SerializeObject(entity);
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = client.PostAsync(path, httpContent).Result;
            string a = response.Content.ReadAsStringAsync().Result;
            var loginModel = JsonConvert.DeserializeObject<responseNoData>(response.Content.ReadAsStringAsync().Result);
            return loginModel;
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
            Application.Exit();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            string path = Program.host + "/identity";

            try
            {
                var result = SendDataAsync(path, this.data,this.model.accessToken);
                if (result.code == 200)
                {
                    MessageBox.Show(result.message, "Lưu thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPost.Enabled = false;
                    lbError.Text = "";
                    pbAvatar.Image = null;
                    txtInformation.Text = "";
                    txtCCCD.Text = "";
                    txtReCCCD.Text = "";
                    return;
                }
                if (result.code == 400)
                {
                    MessageBox.Show(result.message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
