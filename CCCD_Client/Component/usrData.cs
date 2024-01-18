using CCCD_Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCCD_Client.Component
{
    public partial class usrData : MetroFramework.Controls.MetroUserControl
    {
        LoginModel model;
        public usrData(LoginModel _model)
        {
            this.model = _model;
            InitializeComponent();
            rbSearchName.Checked = true;
            rbSearchCCCD.Checked = false;
            frMain.listDataToSort = toView(false, null, "", new DateTime(2000,01,01), DateTime.Now.AddDays(1));
            grvData.DataSource = frMain.listDataToSort;

        }

        public void UpdateGrid()
        {
            frMain.listDataToSort = toView(false, null, "", new DateTime(2000, 01, 01), DateTime.Now.AddDays(1));
            grvData.DataSource = frMain.listDataToSort;
            grvData.Refresh();
        }

        public List<ScanIdViewModel> toView(bool isSearch, int? searchType, string keyWord, DateTime? fromDate, DateTime? toDate)
        {
            string commandString = "";
            if (isSearch)
            {
                if (searchType == 0)
                    commandString = "SELECT DATA_ID, ID_NUMBER, FULL_NAME, DATE_OF_BIRTH, SEX, PLACE_OF_ORIGIN, PLACE_OF_RESIDENCE, DATE_EXPIRED, DATE_ISSUE FROM scan_id " +
                        "WHERE UPPER(FULL_NAME) LIKE N'%" + keyWord.ToUpper() + "%' AND (DATE_SCAN BETWEEN '" + fromDate?.ToString("yyyy-MM-dd") + "' AND '" + toDate?.ToString("yyyy-MM-dd") + "') ORDER BY DATE_SCAN DESC";
                if (searchType == 1)
                    commandString = "SELECT DATA_ID, ID_NUMBER, FULL_NAME, DATE_OF_BIRTH, SEX, PLACE_OF_ORIGIN, PLACE_OF_RESIDENCE, DATE_EXPIRED, DATE_ISSUE FROM scan_id " +
                        "WHERE UPPER(ID_NUMBER) LIKE N'%" + keyWord.ToUpper() + "%' AND (DATE_SCAN BETWEEN '" + fromDate?.ToString("yyyy-MM-dd") + "' AND '" + toDate?.ToString("yyyy-MM-dd") + "') ORDER BY DATE_SCAN DESC";

            }
            else
            {
                commandString = "SELECT DATA_ID, ID_NUMBER, FULL_NAME, DATE_OF_BIRTH, SEX, PLACE_OF_ORIGIN, PLACE_OF_RESIDENCE, DATE_EXPIRED, DATE_ISSUE FROM scan_id WHERE (DATE_SCAN BETWEEN '" + fromDate?.ToString("yyyy-MM-dd") + "' AND '" + toDate?.ToString("yyyy-MM-dd") + "') ORDER BY DATE_SCAN DESC";
            }

            using (SqlCommand command = new SqlCommand(commandString, frMain.connection))
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

                            dateIssue = ((DateTime)reader["DATE_ISSUE"]).ToString("dd/MM/yyyy")
                        };
                        obj.dateExpired = "Không giới hạn";
                        if (!reader.IsDBNull(reader.GetOrdinal("DATE_EXPIRED")))
                        {
                            DateTime dateTimeExpired = (DateTime)reader["DATE_EXPIRED"];
                            obj.dateExpired = dateTimeExpired.ToString("dd/MM/yyyy");
                        }

                        objects.Add(obj);
                    }
                    return objects;

                }
            }
        }

        private ScanIdModel getSingleId(string dataId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM scan_id WHERE DATA_ID = '" + dataId + "'", frMain.connection))
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
                                status = (int)reader["STATUS"],
                                userId = (int)reader["USER_ID"]

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

        private void grvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            grvData.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void grvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            try
            {
                DataGridViewRow clickedRow = grvData.Rows[e.RowIndex];
                string dataId = clickedRow.Cells["dataId"].Value.ToString();
                Details _detail = new Details(getSingleId(dataId), model.accessToken);
                _detail.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void rbSearchName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSearchCCCD.Checked)
            {
                rbSearchName.Checked = false;
            }
            else
            {
                rbSearchName.Checked = true;
            }
        }

        private void cbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDate.Checked)
            {
                dtpDate.Enabled = true;
                dtpAfter.Enabled = true;
            }
            else
            {
                dtpDate.Enabled = false;
                dtpAfter.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Search();
            }
        }

        public void Search()
        {
            List<ScanIdViewModel> result = new List<ScanIdViewModel>();
            DateTime fromDate = new DateTime(2000, 01, 01);
            DateTime toDate = DateTime.Today.AddDays(1);
            if (cbDate.Checked)
            {
                if (dtpDate.Value.Date > dtpAfter.Value.Date)
                {
                    MessageBox.Show("Khoảng thời gian không đúng. Vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                fromDate = dtpDate.Value;
                toDate = dtpAfter.Value.AddDays(1);
            }
            int type;
            if (rbSearchName.Checked)
            {
                type = 0;
            }
            else
            {
                type = 1;
            }

            try
            {
                result = toView(true, type, txtSearch.Text, fromDate, toDate);
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi khi kết nối đến CSDL. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            frMain.listDataToSort = result;
            grvData.DataSource = frMain.listDataToSort;
            grvData.Refresh();
        }

        private void usrData_Load(object sender, EventArgs e)
        {
            txtSearch.Select();
            txtSearch.Focus();
        }

        private void grvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = grvData.Columns[e.ColumnIndex];
            if (frMain.sortBy.Equals(column.Name)) frMain.isAscending = !frMain.isAscending;
            frMain.sortBy = column.Name;
            if (column.Name == "fullName")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.fullName.Split(' ').LastOrDefault()).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.fullName.Split(' ').LastOrDefault()).Reverse().ToList();
            }
            if (column.Name == "idNumber")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.idNumber).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.idNumber).Reverse().ToList();
            }
            if (column.Name == "dateOfBirth")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => DateTime.ParseExact(p.dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => DateTime.ParseExact(p.dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Reverse().ToList();
            }
            if (column.Name == "sex")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.sex).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.sex).Reverse().ToList();
            }
            if (column.Name == "placeOfOrigin")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.placeOfOrigin).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.placeOfOrigin).Reverse().ToList();
            }
            if (column.Name == "dateIssue")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => DateTime.ParseExact(p.dateIssue, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => DateTime.ParseExact(p.dateIssue, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Reverse().ToList();
            }
            if (column.Name == "dateExpired")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p =>
                    {
                        if (p.dateExpired.Equals("Không giới hạn"))
                        {
                            return DateTime.MaxValue;
                        }
                        return DateTime.ParseExact(p.dateExpired, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p =>
                {
                    if (p.dateExpired.Equals("Không giới hạn"))
                    {
                        return DateTime.MaxValue;
                    }
                    return DateTime.ParseExact(p.dateExpired, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }).Reverse().ToList();
            }
            if (column.Name == "placeOfResidence")
            {
                if (frMain.isAscending)
                    frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.placeOfResidence).ToList();
                else frMain.listDataToSort = frMain.listDataToSort.OrderBy(p => p.placeOfResidence).Reverse().ToList();
            }

            grvData.DataSource = frMain.listDataToSort;
            grvData.Refresh();
            return;
        }
    }
}
