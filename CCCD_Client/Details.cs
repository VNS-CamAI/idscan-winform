using CCCD_Client.Model;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Spire.Barcode;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client
{
    public partial class Details : MetroFramework.Forms.MetroForm
    {
        private string access_token;
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Vinorsoft";

        private ScanIdModel scanid;
        public Details(ScanIdModel _model, string _access_token)
        {
            InitializeComponent();
            this.scanid = _model;
            this.access_token = _access_token;
            this.Text = "Thông tin CCCD số " + _model.idNumber;
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
            this.lb_DateExpired.Text = _model.dateExpired.Equals(null) ? "Không giới hạn" : ((DateTime)_model.dateExpired).ToString("dd-MM-yyyy");
            this.lb_Identifycation.Text = _model.identification;
            try
            {
                // Convert base 64 string to byte[]
                byte[] imageBytes = Convert.FromBase64String(_model.portraitImage);
                // Convert byte[] to Image
                using (var ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pb_Avatar.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btn_docx_Click(object sender, EventArgs e)
        {
            string filePath = CreateDoc(scanid, "doc");
            if (filePath == "")
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (filePath.Equals("missing"))
            {
                return;
            }
            try
            {
                Process.Start(filePath);
            }
            catch (Exception)
            {

            }
        }


        private string CreateDoc(ScanIdModel _model, string type)
        {

            String sex = scanid.sex == 1 ? "Nam" : "Nữ";
            Document document = new Document();
            try
            {
                document.LoadFromFile(this.filePath + @"\Template.docx");
            }
            catch (Exception e)
            {
                MessageBox.Show("Không tồn tại file mẫu. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "missing";
            }

            try
            {
                document.Replace("fullName", _model.fullName, false, true);
                document.Replace("dateOfBirth", _model.dateOfBirth.ToString("dd/MM/yyyy"), false, true);
                document.Replace("idNumber", _model.idNumber, false, true);
                document.Replace("sex", sex, false, true);
                document.Replace("placeOfOrigin", _model.placeOfOrigin, false, true);
                document.Replace("ethnic", _model.ethnic, false, true);
                document.Replace("religion", _model.religion, false, true);
                document.Replace("placeOfResidence", _model.placeOfResidence, false, true);
                document.Replace("father", _model.father, false, true);
                document.Replace("mother", _model.mother, false, true);
                document.Replace("mate", _model.mate, false, true);
                document.Replace("dateIssue", _model.dateIssue.ToString("dd/MM/yyyy"), false, true);
                document.Replace("dateExpired", _model.dateExpired.Equals(null) ? "Không giới hạn" : ((DateTime)_model.dateExpired).ToString("dd-MM-yyyy"), false, true);
                document.Replace("oldIdNumber", _model.oldIdNumber, false, true);
                document.Replace("identification", _model.identification, false, true);
                DateTime dateNow = DateTime.Now;
                document.Replace("dateCreate", "ngày " + dateNow.Day + " tháng " + dateNow.Month + " năm " + dateNow.Year, false, true);

                String QRtext = scanid.idNumber + "|" + scanid.oldIdNumber + "|" + scanid.fullName + " |"
                    + scanid.dateOfBirth.ToString("dd/MM/yyyy") + "|" + sex + "|" + scanid.placeOfResidence
                    + "|" + scanid.dateIssue.ToString("dd/MM/yyyy");
                Image QRImg = CreteQR(QRtext);
                document.Replace("qrText", QRtext, false, true);

                foreach (Paragraph paragraph in document.Sections[0].Paragraphs)
                {
                    //Loop through the child elements of paragraph
                    foreach (DocumentObject docObj in paragraph.ChildObjects)
                    {
                        if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                        {
                            DocPicture picture = docObj as DocPicture;
                            if (picture.Height / picture.Width > 1.2)
                            {
                                try
                                {
                                    // Convert base 64 string to byte[]
                                    byte[] imageBytes = Convert.FromBase64String(_model.portraitImage);
                                    // Convert byte[] to Image
                                    using (var ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length))
                                    {
                                        Image temp = Image.FromStream(ms, true);
                                        Image resize_img = resizeImage(temp, Convert.ToInt32(picture.Height * 132 / 100), Convert.ToInt32(picture.Width * 132 / 100));
                                        picture.LoadImage(resize_img);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }
                            }

                            if (picture.Height / picture.Width < 1.1)
                            {
                                try
                                {
                                    Image resize_img = resizeImage(QRImg, Convert.ToInt32(picture.Height * 132 / 100), Convert.ToInt32(picture.Width * 132 / 100));
                                    picture.LoadImage(resize_img);
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                return "";
            }
            if (type == "doc")
            {
                document.SaveToFile(this.filePath + @"\Replace.docx", FileFormat.Docx);
                byte[] fileBytes = File.ReadAllBytes(this.filePath + @"\Replace.docx");
                string filename = RemoveVietnameseTone(scanid.fullName).Replace(' ', '_') + DateTime.Now.ToString("_yyyy-MM-dd-fff") + ".docx";
                string fileName = ShowSaveDocxFileDialog(filename);
                if (fileName != null)
                {
                    File.WriteAllBytes(fileName, fileBytes);
                    File.Delete(this.filePath + @"\Replace.docx");
                    return fileName;
                }
            }

            if (type == "pdf")
            {
                document.SaveToFile(this.filePath + @"\Replace.pdf", FileFormat.PDF);
                byte[] fileBytes = File.ReadAllBytes(this.filePath + @"\Replace.pdf");
                string filename = RemoveVietnameseTone(scanid.fullName).Replace(' ', '_') + DateTime.Now.ToString("_yyyy-MM-dd-ssfff") + ".pdf";
                string fileName = ShowSavePdfFileDialog(filename);
                if (fileName != null)
                {
                    File.WriteAllBytes(fileName, fileBytes);
                    File.Delete(this.filePath + @"\Replace.pdf");
                    return fileName;
                }
            }


            return "nonsave";
        }

        private Image CreteQR(string data)
        {
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;
            settings.Unit = GraphicsUnit.Pixel;
            settings.ShowText = false;
            settings.ResolutionType = ResolutionType.UseDpi;
            settings.X = 7;
            settings.Data = data;
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            Image QRbarcode = generator.GenerateImage();

            return QRbarcode;
        }


        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }


        public static string RemoveVietnameseTone(string text)
        {
            string result = text;
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {
            string filePath = CreateDoc(scanid, "pdf");
            if (filePath == "")
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Process.Start(filePath);
            }
            catch (Exception)
            {

            }
        }

        private string ShowSaveDocxFileDialog(string filename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "docx";
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx";
            saveFileDialog.FileName = filename;
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }

        private string ShowSavePdfFileDialog(string filename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = filename;
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }

        private void btn_EditTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(this.filePath + @"\Template.docx");
            }
            catch (Exception)
            {
                MessageBox.Show("Không tồn tại file mẫu. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
