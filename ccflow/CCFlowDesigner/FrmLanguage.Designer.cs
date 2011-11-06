namespace BP.Win32
{
    partial class FrmLanguage
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLanguage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_CH = new System.Windows.Forms.Button();
            this.Btn_B5 = new System.Windows.Forms.Button();
            this.Btn_EN = new System.Windows.Forms.Button();
            this.Btn_JP = new System.Windows.Forms.Button();
            this.Btn_RU = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 83);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "日本語";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Btn_CH
            // 
            this.Btn_CH.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_CH.Location = new System.Drawing.Point(122, 55);
            this.Btn_CH.Name = "Btn_CH";
            this.Btn_CH.Size = new System.Drawing.Size(92, 23);
            this.Btn_CH.TabIndex = 0;
            this.Btn_CH.Text = "简体中文";
            this.Btn_CH.UseVisualStyleBackColor = true;
            this.Btn_CH.Click += new System.EventHandler(this.Btn_CH_Click);
            // 
            // Btn_B5
            // 
            this.Btn_B5.Location = new System.Drawing.Point(122, 84);
            this.Btn_B5.Name = "Btn_B5";
            this.Btn_B5.Size = new System.Drawing.Size(92, 23);
            this.Btn_B5.TabIndex = 1;
            this.Btn_B5.Text = "繁体中文";
            this.Btn_B5.UseVisualStyleBackColor = true;
            this.Btn_B5.Click += new System.EventHandler(this.Btn_CH_Click);
            // 
            // Btn_EN
            // 
            this.Btn_EN.Location = new System.Drawing.Point(122, 113);
            this.Btn_EN.Name = "Btn_EN";
            this.Btn_EN.Size = new System.Drawing.Size(92, 23);
            this.Btn_EN.TabIndex = 2;
            this.Btn_EN.Text = "English";
            this.Btn_EN.UseVisualStyleBackColor = true;
            this.Btn_EN.Click += new System.EventHandler(this.Btn_CH_Click);
            // 
            // Btn_JP
            // 
            this.Btn_JP.Location = new System.Drawing.Point(122, 142);
            this.Btn_JP.Name = "Btn_JP";
            this.Btn_JP.Size = new System.Drawing.Size(92, 23);
            this.Btn_JP.TabIndex = 3;
            this.Btn_JP.Text = "日本语";
            this.Btn_JP.UseVisualStyleBackColor = true;
            this.Btn_JP.Click += new System.EventHandler(this.Btn_CH_Click);
            // 
            // Btn_RU
            // 
            this.Btn_RU.Location = new System.Drawing.Point(122, 171);
            this.Btn_RU.Name = "Btn_RU";
            this.Btn_RU.Size = new System.Drawing.Size(92, 23);
            this.Btn_RU.TabIndex = 4;
            this.Btn_RU.Text = "Русский";
            this.Btn_RU.UseVisualStyleBackColor = true;
            this.Btn_RU.Click += new System.EventHandler(this.Btn_CH_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Location = new System.Drawing.Point(26, 277);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // FrmLanguage
            // 
            this.AcceptButton = this.Btn_CH;
            this.CancelButton = this.Btn_CH;
            this.ClientSize = new System.Drawing.Size(341, 319);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Btn_RU);
            this.Controls.Add(this.Btn_JP);
            this.Controls.Add(this.Btn_EN);
            this.Controls.Add(this.Btn_B5);
            this.Controls.Add(this.Btn_CH);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLanguage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

         
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Btn_CH;
        private System.Windows.Forms.Button Btn_B5;
        private System.Windows.Forms.Button Btn_EN;
        private System.Windows.Forms.Button Btn_JP;
        private System.Windows.Forms.Button Btn_RU;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}