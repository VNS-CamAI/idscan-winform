
using System.Windows.Forms;

namespace CCCD_Client.Component
{
    partial class usrData
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.grvData = new System.Windows.Forms.DataGridView();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placeOfOrigin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateIssue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateExpired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placeOfResidence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.rbSearchName = new MetroFramework.Controls.MetroRadioButton();
            this.rbSearchCCCD = new MetroFramework.Controls.MetroRadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cbDate = new MetroFramework.Controls.MetroCheckBox();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.dtpAfter = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.grvData);
            this.panel1.Location = new System.Drawing.Point(12, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1158, 501);
            this.panel1.TabIndex = 0;
            // 
            // grvData
            // 
            this.grvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.fullName,
            this.idNumber,
            this.dateOfBirth,
            this.sex,
            this.placeOfOrigin,
            this.dateIssue,
            this.dateExpired,
            this.placeOfResidence,
            this.dataId});
            this.grvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvData.Location = new System.Drawing.Point(0, 0);
            this.grvData.Name = "grvData";
            this.grvData.RowHeadersWidth = 51;
            this.grvData.RowTemplate.Height = 24;
            this.grvData.Size = new System.Drawing.Size(1158, 501);
            this.grvData.TabIndex = 2;
            this.grvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvData_CellClick);
            this.grvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grvData_ColumnHeaderMouseClick);
            this.grvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grvData_RowPrePaint);
            // 
            // seq
            // 
            this.seq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.seq.FillWeight = 5F;
            this.seq.HeaderText = "Số TT";
            this.seq.MinimumWidth = 40;
            this.seq.Name = "seq";
            this.seq.Width = 40;
            // 
            // fullName
            // 
            this.fullName.DataPropertyName = "fullName";
            this.fullName.FillWeight = 95F;
            this.fullName.HeaderText = "Họ và tên";
            this.fullName.MinimumWidth = 6;
            this.fullName.Name = "fullName";
            // 
            // idNumber
            // 
            this.idNumber.DataPropertyName = "idNumber";
            this.idNumber.HeaderText = "Số CCCD";
            this.idNumber.MinimumWidth = 6;
            this.idNumber.Name = "idNumber";
            // 
            // dateOfBirth
            // 
            this.dateOfBirth.DataPropertyName = "dateOfBirth";
            this.dateOfBirth.FillWeight = 80F;
            this.dateOfBirth.HeaderText = "Ngày Sinh";
            this.dateOfBirth.MinimumWidth = 6;
            this.dateOfBirth.Name = "dateOfBirth";
            // 
            // sex
            // 
            this.sex.DataPropertyName = "sex";
            this.sex.FillWeight = 50F;
            this.sex.HeaderText = "Giới tính";
            this.sex.MinimumWidth = 6;
            this.sex.Name = "sex";
            // 
            // placeOfOrigin
            // 
            this.placeOfOrigin.DataPropertyName = "placeOfOrigin";
            this.placeOfOrigin.HeaderText = "Quê quán";
            this.placeOfOrigin.MinimumWidth = 6;
            this.placeOfOrigin.Name = "placeOfOrigin";
            // 
            // dateIssue
            // 
            this.dateIssue.DataPropertyName = "dateIssue";
            this.dateIssue.FillWeight = 80F;
            this.dateIssue.HeaderText = "Ngày cấp";
            this.dateIssue.MinimumWidth = 6;
            this.dateIssue.Name = "dateIssue";
            // 
            // dateExpired
            // 
            this.dateExpired.DataPropertyName = "dateExpired";
            this.dateExpired.FillWeight = 80F;
            this.dateExpired.HeaderText = "Ngày hết hạn";
            this.dateExpired.MinimumWidth = 6;
            this.dateExpired.Name = "dateExpired";
            // 
            // placeOfResidence
            // 
            this.placeOfResidence.DataPropertyName = "placeOfResidence";
            this.placeOfResidence.HeaderText = "Nơi thường trú";
            this.placeOfResidence.MinimumWidth = 6;
            this.placeOfResidence.Name = "placeOfResidence";
            // 
            // dataId
            // 
            this.dataId.DataPropertyName = "dataId";
            this.dataId.HeaderText = "dataId";
            this.dataId.MinimumWidth = 6;
            this.dataId.Name = "dataId";
            this.dataId.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(124, 67);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PromptText = "Nhập để tìm kiếm .....";
            this.txtSearch.Size = new System.Drawing.Size(287, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(40, 67);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(66, 20);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Tìm kiếm";
            // 
            // rbSearchName
            // 
            this.rbSearchName.AutoSize = true;
            this.rbSearchName.Location = new System.Drawing.Point(124, 34);
            this.rbSearchName.Name = "rbSearchName";
            this.rbSearchName.Size = new System.Drawing.Size(97, 17);
            this.rbSearchName.TabIndex = 3;
            this.rbSearchName.TabStop = true;
            this.rbSearchName.Text = "Tìm theo tên";
            this.rbSearchName.UseVisualStyleBackColor = true;
            this.rbSearchName.CheckedChanged += new System.EventHandler(this.rbSearchName_CheckedChanged);
            // 
            // rbSearchCCCD
            // 
            this.rbSearchCCCD.AutoSize = true;
            this.rbSearchCCCD.Location = new System.Drawing.Point(251, 34);
            this.rbSearchCCCD.Name = "rbSearchCCCD";
            this.rbSearchCCCD.Size = new System.Drawing.Size(112, 17);
            this.rbSearchCCCD.TabIndex = 4;
            this.rbSearchCCCD.TabStop = true;
            this.rbSearchCCCD.Text = "Tìm theo CCCD";
            this.rbSearchCCCD.UseVisualStyleBackColor = true;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(626, 29);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(157, 22);
            this.dtpDate.TabIndex = 5;
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(529, 33);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(71, 17);
            this.cbDate.TabIndex = 7;
            this.cbDate.Text = "Từ ngày";
            this.cbDate.UseVisualStyleBackColor = true;
            this.cbDate.CheckedChanged += new System.EventHandler(this.cbDate_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(529, 67);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpAfter
            // 
            this.dtpAfter.CustomFormat = "dd/MM/yyyy";
            this.dtpAfter.Enabled = false;
            this.dtpAfter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAfter.Location = new System.Drawing.Point(911, 29);
            this.dtpAfter.Name = "dtpAfter";
            this.dtpAfter.Size = new System.Drawing.Size(157, 22);
            this.dtpAfter.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(817, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "đến ngày";
            // 
            // usrData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpAfter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.rbSearchCCCD);
            this.Controls.Add(this.rbSearchName);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.panel1);
            this.Name = "usrData";
            this.Size = new System.Drawing.Size(1188, 632);
            this.Load += new System.EventHandler(this.usrData_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroRadioButton rbSearchName;
        private MetroFramework.Controls.MetroRadioButton rbSearchCCCD;
        private System.Windows.Forms.DateTimePicker dtpDate;
        public DataGridView grvData;
        private MetroFramework.Controls.MetroCheckBox cbDate;
        private MetroFramework.Controls.MetroButton btnSearch;
        private DataGridViewTextBoxColumn seq;
        private DataGridViewTextBoxColumn fullName;
        private DataGridViewTextBoxColumn idNumber;
        private DataGridViewTextBoxColumn dateOfBirth;
        private DataGridViewTextBoxColumn sex;
        private DataGridViewTextBoxColumn placeOfOrigin;
        private DataGridViewTextBoxColumn dateIssue;
        private DataGridViewTextBoxColumn dateExpired;
        private DataGridViewTextBoxColumn placeOfResidence;
        private DataGridViewTextBoxColumn dataId;
        private DateTimePicker dtpAfter;
        private Label label1;
    }
}
