using CCCD_Client.Model;
using DemoReader;
using Newtonsoft.Json;
using org.jmrtd.lds.icao;
using org.jmrtd.lds.iso19794;
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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client.Component
{
    public partial class usrScan : MetroFramework.Controls.MetroUserControl
    {
        private LoginModel model;

        private Identity data;
        public usrScan(LoginModel _model)
        {
            this.model = _model;
            InitializeComponent();
            lbName.Text = "Xin chào " + _model.username + "!";
            txtCCCD.Select();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                doScan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng kiểm tra số CCCD và thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnScan.Enabled = true;
                return;
            }
        }

        private void doScan()
        {

            String serial = GetSerialNumber();
            if (serial == "")
            {
                MessageBox.Show("Bạn chưa đặt thẻ, vui lòng đặt thẻ để tiến hành quét!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!frLogin.devices.Contains(serial))
            {
                MessageBox.Show("Thiết bị bạn đang sử dụng không hợp lệ\nVui lòng liên hệ với bộ phận CSKH của VinorSoft để được hỗ trợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnScan.Enabled = false;

            this.clearForm();

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

            pbAvatar.Image = null;
            String cccdId = txtCCCD.Text;
            String strMrz = "";


            CardService_ service = CardService_.getInstance(null);
            IdcardReader idReader = new IdcardReader(service);
            if (!idReader.ReadData(cccdId, strMrz))
            {
                //MessageBox.Show("Lỗi: " + idReader.getLastErrorMsg());
                //btnScan.Enabled = true;
                //return;
            }


            /////////
            //////// phần này để trích xuất ảnh, dùng cho cả có thời hạn và không thời hạn
            ////////
            FieldInfo field1 = typeof(IdcardReader).GetField("MmI15RGofD", BindingFlags.NonPublic |
                         BindingFlags.Instance);
            object value1 = field1.GetValue(idReader);
            DG2File c = (DG2File)value1;
            var faceinfo = c.getFaceInfos().toArray();
            FaceInfo testype = (FaceInfo)faceinfo[0];
            var abc = testype.getFaceImageInfos().toArray();
            FaceImageInfo fii = (FaceImageInfo)abc[0];

            byte[] imgBytes = fii.getEncoded();
            byte[] displayBytes = new ArraySegment<byte>(imgBytes, 32, imgBytes.Length - 32).ToArray();

            /////////
            ///////// kết thúc phần trích xuất ảnh, displayBytes là đoạn byte truyền xuống hàm hiển thị ảnh
            ////////

            clearForm();

            

            this.data = new Identity();

            var citi = idReader.getCitizenInfo();
            //Show infomation
            this.lb_CCCD.Text = "Thông tin CCCD số " + citi.getCitizenPid();
            this.lb_fullName.Text = citi.getFullName();
            this.lb_dateOfBirth.Text = citi.getBirthDate();
            this.lb_Sex.Text = citi.getGender();
            this.lb_placeOfOrigin.Text = citi.getHomeTown();
            this.lb_Ethnic.Text = citi.getEthnic();
            this.lb_Religion.Text = citi.getReligion();
            this.lb_Nation.Text = "Việt Nam";
            this.lb_placeOfResidence.Text = citi.getRegPlaceAddress();
            this.lb_Father.Text = citi.getFatherName();
            this.lb_Mother.Text = citi.getMotherName();
            this.lb_Mate.Text = citi.getHusBandName();
            this.lb_OldIdNumber.Text = citi.getOldIdentify();
            this.lb_DateIssue.Text = citi.getDateProvide();
            this.lb_DateExpired.Text = citi.getOutOfDate();
            this.lb_Identifycation.Text = citi.getIdentifyCharacteristics();
            this.lb_CCCD.Visible = true;
            this.lb_fullName.Visible = true;
            this.lb_dateOfBirth.Visible = true;
            this.lb_Sex.Visible = true;
            this.lb_placeOfOrigin.Visible = true;
            this.lb_Ethnic.Visible = true;
            this.lb_Religion.Visible = true;
            this.lb_Nation.Visible = true;
            this.lb_placeOfResidence.Visible = true;
            this.lb_Father.Visible = true;
            this.lb_Mother.Visible = true;
            this.lb_Mate.Visible = true;
            this.lb_OldIdNumber.Visible = true;
            this.lb_DateIssue.Visible = true;
            this.lb_DateExpired.Visible = true;
            this.lb_Identifycation.Visible = true;


            this.metroLabel24.Visible = true;
            this.metroLabel3.Visible = true;
            this.metroLabel5.Visible = true;
            this.metroLabel7.Visible = true;
            this.metroLabel9.Visible = true;
            this.metroLabel11.Visible = true;
            this.metroLabel13.Visible = true;
            this.metroLabel15.Visible = true;
            this.metroLabel17.Visible = true;
            this.metroLabel19.Visible = true;
            this.metroLabel21.Visible = true;
            this.metroLabel25.Visible = true;
            this.metroLabel27.Visible = true;
            this.metroLabel29.Visible = true;
            this.metroLabel31.Visible = true;

            btnScan.Enabled = true;

            data.idNumber = citi.getCitizenPid();
            data.oldIdNumber = citi.getOldIdentify() == null ? "" : citi.getOldIdentify();
            data.fullName = citi.getFullName();
            data.sex = citi.getGender().Equals("Nam") ? 1 : 0;
            data.ethnic = citi.getEthnic() == null ? "" : citi.getEthnic();
            data.religion = citi.getReligion() == null ? "" : citi.getReligion();
            data.identification = citi.getIdentifyCharacteristics() == null ? "" : citi.getIdentifyCharacteristics();
            data.dateOfBirth = DateTime.ParseExact(citi.getBirthDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            data.dateIssue = DateTime.ParseExact(citi.getDateProvide(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            try
            {
                data.dateExpired = DateTime.ParseExact(citi.getOutOfDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
            }
            data.placeOfOrigin = citi.getHomeTown() == null ? "" : citi.getHomeTown();
            data.placeOfResidence = citi.getRegPlaceAddress() == null ? "" : citi.getRegPlaceAddress();
            data.father = citi.getFatherName() == null ? "" : citi.getFatherName();
            data.mother = citi.getMotherName() == null ? "" : citi.getMotherName();
            data.mate = citi.getHusBandName() == null ? "" : citi.getHusBandName();
            string base64Image = Convert.ToBase64String(displayBytes);
            data.portraitImage = base64Image;
            data.dateScan = DateTime.Now;
            //WriteLog("Face image base64: " + citi.getPhotoBase64());
            try
            {
                // Convert base 64 string to byte[]
                //byte[] imageBytes = Convert.FromBase64String(citi.getPhotoBase64());
                // Convert byte[] to Image
                using (var ms = new System.IO.MemoryStream(displayBytes, 0, displayBytes.Length))
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

        private void clearForm()
        {
            btnPost.Enabled = false;
            lbError.Text = "";
            pbAvatar.Image = null;
            pbAvatar.Refresh();
            this.lb_CCCD.Visible = false;
            this.lb_fullName.Visible = false;
            this.lb_dateOfBirth.Visible = false;
            this.lb_Sex.Visible = false;
            this.lb_Sex.Refresh();
            this.lb_placeOfOrigin.Visible = false;
            this.lb_Ethnic.Visible = false;
            this.lb_Religion.Visible = false;
            this.lb_Nation.Visible = false;
            this.lb_placeOfResidence.Visible = false;
            this.lb_Father.Visible = false;
            this.lb_Mother.Visible = false;
            this.lb_Mate.Visible = false;
            this.lb_OldIdNumber.Visible = false;
            this.lb_DateIssue.Visible = false;
            this.lb_DateExpired.Visible = false;
            this.lb_Identifycation.Visible = false;

            this.metroLabel24.Visible = false;
            this.metroLabel3.Visible = false;
            this.metroLabel5.Visible = false;
            this.metroLabel7.Visible = false;
            this.metroLabel9.Visible = false;
            this.metroLabel11.Visible = false;
            this.metroLabel13.Visible = false;
            this.metroLabel15.Visible = false;
            this.metroLabel17.Visible = false;
            this.metroLabel19.Visible = false;
            this.metroLabel21.Visible = false;
            this.metroLabel25.Visible = false;
            this.metroLabel27.Visible = false;
            this.metroLabel29.Visible = false;
            this.metroLabel31.Visible = false;
        }

        private void usrScan_Load(object sender, EventArgs e)
        {
            try
            {
                SmartCard_.getInstance().StartMonitoring();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            ScanIdModel _model = getSingleId(txtCCCD.Text);
            if (_model != null)
            {
                DialogResult rs = MessageBox.Show("CCCD này đã có trong CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO scan_id VALUES (@dataId, @idNumber, @oldIdNumber, @fullName, @sex, @ethnic, @religion, @identification, " +
            "@dateOfBirth, @dateIssue, @dateExpired, @placeOfOrigin, @placeOfResident, @father, @mother, @mate, @poitraitImage, @dateScan, @frontId, @backId, @status, @userId)", frMain.connection))
                {
                    // Add parameters to the command to sanitize the data
                    command.Parameters.AddWithValue("@dataId", Guid.NewGuid().ToString());
                    command.Parameters.AddWithValue("@idNumber", data.idNumber);
                    command.Parameters.AddWithValue("@oldIdNumber", data.oldIdNumber);
                    command.Parameters.AddWithValue("@fullName", data.fullName);
                    command.Parameters.AddWithValue("@sex", data.sex);
                    command.Parameters.AddWithValue("@ethnic", data.ethnic);
                    command.Parameters.AddWithValue("@religion", data.religion);
                    command.Parameters.AddWithValue("@identification", data.identification);
                    command.Parameters.AddWithValue("@dateOfBirth", data.dateOfBirth);
                    command.Parameters.AddWithValue("@dateIssue", data.dateIssue);
                    command.Parameters.AddWithValue("@dateExpired", ((object)data.dateExpired) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@placeOfOrigin", data.placeOfOrigin);
                    command.Parameters.AddWithValue("@placeOfResident", data.placeOfResidence);
                    command.Parameters.AddWithValue("@father", data.father);
                    command.Parameters.AddWithValue("@mother", data.mother);
                    command.Parameters.AddWithValue("@mate", data.mate);
                    command.Parameters.AddWithValue("@poitraitImage", data.portraitImage);
                    command.Parameters.AddWithValue("@dateScan", data.dateScan);
                    command.Parameters.AddWithValue("@frontId", "");
                    command.Parameters.AddWithValue("@backId", "");
                    command.Parameters.AddWithValue("@status", 1);
                    command.Parameters.AddWithValue("@userId", model.id);


                    // Execute the command to insert the data
                    command.ExecuteNonQuery();
                }
                ButtonClicked?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu dữ liệu không thành công", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            MessageBox.Show("Lưu thành công vào CSDL", "Lưu thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clearForm();
            txtCCCD.Text = "";
            txtReCCCD.Text = "";
            return;
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    doScan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra. Vui lòng kiểm tra số CCCD và thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnScan.Enabled = true;
                    return;
                }
            }
        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            if (txtCCCD.Text.Length == 0)
            {
                txtReCCCD.Text = "";
                clearForm();
            }
            btnScan.Enabled = true;
        }

        public event Action ButtonClicked;

        private ScanIdModel getSingleId(string dataId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM scan_id WHERE ID_NUMBER = '" + dataId + "'", frMain.connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScanIdModel obj = new ScanIdModel
                            {
                                dataId = (string)reader["DATA_ID"],
                                idNumber = (string)reader["ID_NUMBER"],
                                oldIdNumber = (string)reader["OLD_ID_NUMBER"],
                                fullName = (string)reader["FULL_NAME"],
                                sex = (int)reader["SEX"],
                                ethnic = (string)reader["ETHNIC"],
                                religion = (string)reader["RELIGION"],
                                identification = (string)reader["IDENTIFICATION"],
                                dateOfBirth = (DateTime)reader["DATE_OF_BIRTH"],
                                dateIssue = (DateTime)reader["DATE_ISSUE"],
                                placeOfOrigin = (string)reader["PLACE_OF_ORIGIN"],
                                placeOfResidence = (string)reader["PLACE_OF_RESIDENCE"],
                                father = (string)reader["FATHER"],
                                mother = (string)reader["MOTHER"],
                                mate = (string)reader["MATE"],
                                portraitImage = (string)reader["PORTRAIT_IMAGE"],
                                dateScan = (DateTime)reader["DATE_SCAN"],
                                status = (int)reader["STATUS"]

                            };
                            obj.dateExpired = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("DATE_EXPIRED")))
                            {
                                obj.dateExpired = (DateTime)reader["DATE_EXPIRED"];
                            }
                            return obj;
                        }

                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public String GetSerialNumber()
        {
            var context = new SCardContext();
            context.Establish(SCardScope.System);
            var readerNames = context.GetReaders();
            var readerName = readerNames.FirstOrDefault();
            var readerName_cl = readerNames.LastOrDefault();

            if (readerName == null || readerName_cl == null)
            {
                Console.WriteLine("No reader found.");
                return "No device found";
            }
            int use = 1;
            using (var reader = new SCardReader(context))
            {
                reader.Connect(readerName, SCardShareMode.Shared, SCardProtocol.Any);
                if (!reader.IsConnected)
                {
                    reader.Connect(readerName_cl, SCardShareMode.Shared, SCardProtocol.Any);
                    use = 0;
                    if (!reader.IsConnected)
                    {
                        return "";
                    }
                }

                var receiveBuffer = new byte[5000];

                var serialNumber = reader.GetAttrib(SCardAttribute.VendorInterfaceDeviceTypeSerialNumber, out receiveBuffer);
                if (use == 1) reader.Disconnect(SCardReaderDisposition.Leave);
                if (use == 0) reader.Disconnect(SCardReaderDisposition.Eject);
                return Encoding.ASCII.GetString(receiveBuffer);
            }
        }

        private void txtReCCCD_TextChanged(object sender, EventArgs e)
        {
            btnScan.Enabled = true;
        }

        private void txtReCCCD_Focus(object sender, EventArgs e)
        {
            if (txtCCCD.Text.Length != 12)
            {
                return;
            }

            try
            {
                ScanIdModel _model = getSingleId(txtCCCD.Text);
                if (_model != null)
                {
                    DialogResult rs = MessageBox.Show("CCCD này đã được quét, vui lòng sử dụng lại thông tin từ CCCD lưu trữ!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (rs == DialogResult.OK)
                    {
                        this.lb_CCCD.Text = "Thông tin CCCD số " + _model.idNumber;
                        this.lb_fullName.Text = _model.fullName;
                        this.lb_dateOfBirth.Text = _model.dateOfBirth.ToString("dd-MM-yyyy");
                        this.lb_Sex.Text = _model.sex == 1 ? "Nam" : "Nữ";
                        this.lb_placeOfOrigin.Text = _model.placeOfOrigin;
                        this.lb_Ethnic.Text = _model.ethnic;
                        this.lb_Religion.Text = _model.religion;
                        this.lb_Nation.Text = "Việt Nam";
                        this.lb_placeOfResidence.Text = _model.placeOfResidence;
                        this.lb_Father.Text = _model.father;
                        this.lb_Mother.Text = _model.mother;
                        this.lb_Mate.Text = _model.mate;
                        this.lb_OldIdNumber.Text = _model.oldIdNumber;
                        this.lb_DateIssue.Text = _model.dateIssue.ToString("dd-MM-yyyy");
                        this.lb_DateExpired.Text = _model.dateExpired.Equals(null) ? "" : ((DateTime)_model.dateExpired).ToString("dd-MM-yyyy");
                        this.lb_Identifycation.Text = _model.identification;
                        try
                        {
                            // Convert base 64 string to byte[]
                            byte[] imageBytes = Convert.FromBase64String(_model.portraitImage);
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

                        this.metroLabel24.Visible = true;
                        this.metroLabel3.Visible = true;
                        this.metroLabel5.Visible = true;
                        this.metroLabel7.Visible = true;
                        this.metroLabel9.Visible = true;
                        this.metroLabel11.Visible = true;
                        this.metroLabel13.Visible = true;
                        this.metroLabel15.Visible = true;
                        this.metroLabel17.Visible = true;
                        this.metroLabel19.Visible = true;
                        this.metroLabel21.Visible = true;
                        this.metroLabel25.Visible = true;
                        this.metroLabel27.Visible = true;
                        this.metroLabel29.Visible = true;
                        this.metroLabel31.Visible = true;

                        this.lb_CCCD.Visible = true;
                        this.lb_fullName.Visible = true;
                        this.lb_dateOfBirth.Visible = true;
                        this.lb_Sex.Visible = true;
                        this.lb_Sex.Refresh();
                        this.lb_placeOfOrigin.Visible = true;
                        this.lb_Ethnic.Visible = true;
                        this.lb_Religion.Visible = true;
                        this.lb_Nation.Visible = true;
                        this.lb_placeOfResidence.Visible = true;
                        this.lb_Father.Visible = true;
                        this.lb_Mother.Visible = true;
                        this.lb_Mate.Visible = true;
                        this.lb_OldIdNumber.Visible = true;
                        this.lb_DateIssue.Visible = true;
                        this.lb_DateExpired.Visible = true;
                        this.lb_Identifycation.Visible = true;
                    }
                    else
                    {
                        txtCCCD.Text = "";
                        txtReCCCD.Text = "";
                        txtCCCD.Focus();
                        SendKeys.Send("{TAB}");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
