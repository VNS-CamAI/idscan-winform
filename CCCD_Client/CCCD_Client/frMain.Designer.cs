
namespace CCCD_Client
{
    partial class frMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInformation = new MetroFramework.Controls.MetroTextBox();
            this.txtCCCD = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtReCCCD = new MetroFramework.Controls.MetroTextBox();
            this.btnScan = new MetroFramework.Controls.MetroButton();
            this.pbAvatar = new System.Windows.Forms.PictureBox();
            this.lbError = new MetroFramework.Controls.MetroLabel();
            this.btnPost = new MetroFramework.Controls.MetroButton();
            this.lbName = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInformation
            // 
            this.txtInformation.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtInformation.ForeColor = System.Drawing.Color.White;
            this.txtInformation.Location = new System.Drawing.Point(223, 201);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.ReadOnly = true;
            this.txtInformation.Size = new System.Drawing.Size(761, 422);
            this.txtInformation.TabIndex = 0;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(365, 75);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(381, 30);
            this.txtCCCD.TabIndex = 1;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(225, 78);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(90, 20);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Nhập CCCD:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(225, 122);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(110, 20);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Nhập lại CCCD:";
            this.metroLabel2.Click += new System.EventHandler(this.metroLabel2_Click);
            // 
            // txtReCCCD
            // 
            this.txtReCCCD.Location = new System.Drawing.Point(365, 119);
            this.txtReCCCD.Name = "txtReCCCD";
            this.txtReCCCD.Size = new System.Drawing.Size(381, 30);
            this.txtReCCCD.TabIndex = 3;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(817, 75);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(124, 30);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "Bắt đầu quét";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pbAvatar
            // 
            this.pbAvatar.Location = new System.Drawing.Point(23, 201);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(166, 210);
            this.pbAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAvatar.TabIndex = 6;
            this.pbAvatar.TabStop = false;
            // 
            // lbError
            // 
            this.lbError.Location = new System.Drawing.Point(366, 153);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(380, 30);
            this.lbError.TabIndex = 7;
            // 
            // btnPost
            // 
            this.btnPost.Enabled = false;
            this.btnPost.Location = new System.Drawing.Point(817, 119);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(124, 30);
            this.btnPost.TabIndex = 8;
            this.btnPost.Text = "Lưu vào CSDL";
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbName.Location = new System.Drawing.Point(23, 603);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(122, 20);
            this.lbName.TabIndex = 9;
            this.lbName.Text = "Xin chào admin!";
            // 
            // frMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 635);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.pbAvatar);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.txtReCCCD);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.txtInformation);
            this.Name = "frMain";
            this.Text = "Thông tin CCCD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frMain_FormClosing);
            this.Load += new System.EventHandler(this.frMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtInformation;
        private MetroFramework.Controls.MetroTextBox txtCCCD;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtReCCCD;
        private MetroFramework.Controls.MetroButton btnScan;
        private System.Windows.Forms.PictureBox pbAvatar;
        private MetroFramework.Controls.MetroLabel lbError;
        protected MetroFramework.Controls.MetroButton btnPost;
        private MetroFramework.Controls.MetroLabel lbName;
    }
}