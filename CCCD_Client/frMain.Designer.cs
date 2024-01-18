
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frMain));
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabScan = new MetroFramework.Controls.MetroTabPage();
            this.tabData = new MetroFramework.Controls.MetroTabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabScan);
            this.tabControl.Controls.Add(this.tabData);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 1;
            this.tabControl.Size = new System.Drawing.Size(1196, 692);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged_1);
            // 
            // tabScan
            // 
            this.tabScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabScan.HorizontalScrollbarBarColor = true;
            this.tabScan.HorizontalScrollbarSize = 0;
            this.tabScan.Location = new System.Drawing.Point(4, 35);
            this.tabScan.Name = "tabScan";
            this.tabScan.Size = new System.Drawing.Size(192, 61);
            this.tabScan.TabIndex = 0;
            this.tabScan.Text = "Quét CCCD";
            this.tabScan.VerticalScrollbarBarColor = true;
            this.tabScan.VerticalScrollbarSize = 0;
            // 
            // tabData
            // 
            this.tabData.HorizontalScrollbarBarColor = true;
            this.tabData.HorizontalScrollbarSize = 0;
            this.tabData.Location = new System.Drawing.Point(4, 35);
            this.tabData.Name = "tabData";
            this.tabData.Size = new System.Drawing.Size(1188, 653);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "Danh sách CCCD";
            this.tabData.VerticalScrollbarBarColor = true;
            this.tabData.VerticalScrollbarSize = 0;
            this.tabData.Enter += new System.EventHandler(this.tabData_Enter);
            // 
            // frMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1236, 772);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frMain";
            this.Resizable = false;
            this.Text = "Thông tin CCCD";
            this.TextAlign = System.Windows.Forms.VisualStyles.HorizontalAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frMain_FormClosing);
            this.Load += new System.EventHandler(this.frMain_Load);
            this.Shown += new System.EventHandler(this.frMain_Shown);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabScan;
        private MetroFramework.Controls.MetroTabPage tabData;
    }
}