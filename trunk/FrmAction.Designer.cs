namespace BP.WF.Frm
{
    partial class FrmAction
    {
        public string ToE(string no,string chVal)
        {
            return BP.Sys.Language.GetValByUserLang(no,chVal);
        }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAction));
            this.TB_WhenSendError = new System.Windows.Forms.TabControl();
            this.tab_Help = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tab_WhenSave = new System.Windows.Forms.TabPage();
            this.TB_WhenSave = new System.Windows.Forms.TextBox();
            this.tab_WhenSend = new System.Windows.Forms.TabPage();
            this.TB_WhenSend = new System.Windows.Forms.TextBox();
            this.tab_WhenSendOK = new System.Windows.Forms.TabPage();
            this.TB_WhenSendOK = new System.Windows.Forms.TextBox();
            this.tab_WhenSendError = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_Help = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TB_WhenSendError.SuspendLayout();
            this.tab_Help.SuspendLayout();
            this.tab_WhenSave.SuspendLayout();
            this.tab_WhenSend.SuspendLayout();
            this.tab_WhenSendOK.SuspendLayout();
            this.tab_WhenSendError.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_WhenSendError
            // 
            this.TB_WhenSendError.Controls.Add(this.tab_Help);
            this.TB_WhenSendError.Controls.Add(this.tab_WhenSave);
            this.TB_WhenSendError.Controls.Add(this.tab_WhenSend);
            this.TB_WhenSendError.Controls.Add(this.tab_WhenSendOK);
            this.TB_WhenSendError.Controls.Add(this.tab_WhenSendError);
            this.TB_WhenSendError.Dock = System.Windows.Forms.DockStyle.Top;
            this.TB_WhenSendError.ImageList = this.imageList1;
            this.TB_WhenSendError.Location = new System.Drawing.Point(0, 0);
            this.TB_WhenSendError.Name = "TB_WhenSendError";
            this.TB_WhenSendError.SelectedIndex = 0;
            this.TB_WhenSendError.Size = new System.Drawing.Size(612, 359);
            this.TB_WhenSendError.TabIndex = 0;
            // 
            // tab_Help
            // 
            this.tab_Help.Controls.Add(this.webBrowser1);
            this.tab_Help.ImageIndex = 0;
            this.tab_Help.Location = new System.Drawing.Point(4, 23);
            this.tab_Help.Name = "tab_Help";
            this.tab_Help.Size = new System.Drawing.Size(604, 332);
            this.tab_Help.TabIndex = 4;
            this.tab_Help.Text = "帮助";
            this.tab_Help.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(604, 332);
            this.webBrowser1.TabIndex = 0;
            // 
            // tab_WhenSave
            // 
            this.tab_WhenSave.Controls.Add(this.TB_WhenSave);
            this.tab_WhenSave.ImageIndex = 1;
            this.tab_WhenSave.Location = new System.Drawing.Point(4, 23);
            this.tab_WhenSave.Name = "tab_WhenSave";
            this.tab_WhenSave.Padding = new System.Windows.Forms.Padding(3);
            this.tab_WhenSave.Size = new System.Drawing.Size(604, 332);
            this.tab_WhenSave.TabIndex = 0;
            this.tab_WhenSave.Text = "当节点保存时";
            this.tab_WhenSave.UseVisualStyleBackColor = true;
            // 
            // TB_WhenSave
            // 
            this.TB_WhenSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_WhenSave.Location = new System.Drawing.Point(3, 3);
            this.TB_WhenSave.Multiline = true;
            this.TB_WhenSave.Name = "TB_WhenSave";
            this.TB_WhenSave.Size = new System.Drawing.Size(598, 326);
            this.TB_WhenSave.TabIndex = 0;
            // 
            // tab_WhenSend
            // 
            this.tab_WhenSend.Controls.Add(this.TB_WhenSend);
            this.tab_WhenSend.ImageIndex = 1;
            this.tab_WhenSend.Location = new System.Drawing.Point(4, 23);
            this.tab_WhenSend.Name = "tab_WhenSend";
            this.tab_WhenSend.Padding = new System.Windows.Forms.Padding(3);
            this.tab_WhenSend.Size = new System.Drawing.Size(604, 332);
            this.tab_WhenSend.TabIndex = 1;
            this.tab_WhenSend.Text = "当节点发送时";
            this.tab_WhenSend.UseVisualStyleBackColor = true;
            // 
            // TB_WhenSend
            // 
            this.TB_WhenSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_WhenSend.Location = new System.Drawing.Point(3, 3);
            this.TB_WhenSend.Multiline = true;
            this.TB_WhenSend.Name = "TB_WhenSend";
            this.TB_WhenSend.Size = new System.Drawing.Size(598, 326);
            this.TB_WhenSend.TabIndex = 1;
            // 
            // tab_WhenSendOK
            // 
            this.tab_WhenSendOK.Controls.Add(this.TB_WhenSendOK);
            this.tab_WhenSendOK.ImageIndex = 1;
            this.tab_WhenSendOK.Location = new System.Drawing.Point(4, 23);
            this.tab_WhenSendOK.Name = "tab_WhenSendOK";
            this.tab_WhenSendOK.Size = new System.Drawing.Size(604, 332);
            this.tab_WhenSendOK.TabIndex = 3;
            this.tab_WhenSendOK.Text = "当节点发送成功";
            this.tab_WhenSendOK.UseVisualStyleBackColor = true;
            // 
            // TB_WhenSendOK
            // 
            this.TB_WhenSendOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_WhenSendOK.Location = new System.Drawing.Point(0, 0);
            this.TB_WhenSendOK.Multiline = true;
            this.TB_WhenSendOK.Name = "TB_WhenSendOK";
            this.TB_WhenSendOK.Size = new System.Drawing.Size(604, 332);
            this.TB_WhenSendOK.TabIndex = 1;
            // 
            // tab_WhenSendError
            // 
            this.tab_WhenSendError.Controls.Add(this.textBox1);
            this.tab_WhenSendError.ImageIndex = 1;
            this.tab_WhenSendError.Location = new System.Drawing.Point(4, 23);
            this.tab_WhenSendError.Name = "tab_WhenSendError";
            this.tab_WhenSendError.Size = new System.Drawing.Size(604, 332);
            this.tab_WhenSendError.TabIndex = 2;
            this.tab_WhenSendError.Text = "当节点发送失败";
            this.tab_WhenSendError.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(604, 332);
            this.textBox1.TabIndex = 1;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(385, 370);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(75, 23);
            this.Btn_Save.TabIndex = 1;
            this.Btn_Save.Text = this.ToE("Save","保存"); 
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Visible = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Location = new System.Drawing.Point(489, 370);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(75, 23);
            this.Btn_Exit.TabIndex = 2;
            this.Btn_Exit.Text = this.ToE("Exit", "退出");  
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // Btn_Help
            // 
            this.Btn_Help.Location = new System.Drawing.Point(12, 370);
            this.Btn_Help.Name = "Btn_Help";
            this.Btn_Help.Size = new System.Drawing.Size(75, 23);
            this.Btn_Help.TabIndex = 3;
            this.Btn_Help.Text = "帮助";
            this.Btn_Help.UseVisualStyleBackColor = true;
            this.Btn_Help.Click += new System.EventHandler(this.Btn_Help_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Help.gif");
            this.imageList1.Images.SetKeyName(1, "DataIO.gif");
            // 
            // FrmAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 405);
            this.Controls.Add(this.Btn_Help);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.TB_WhenSendError);
            this.Name = "FrmAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "流程运行事件";
            this.Load += new System.EventHandler(this.FrmAction_Load);
            this.TB_WhenSendError.ResumeLayout(false);
            this.tab_Help.ResumeLayout(false);
            this.tab_WhenSave.ResumeLayout(false);
            this.tab_WhenSave.PerformLayout();
            this.tab_WhenSend.ResumeLayout(false);
            this.tab_WhenSend.PerformLayout();
            this.tab_WhenSendOK.ResumeLayout(false);
            this.tab_WhenSendOK.PerformLayout();
            this.tab_WhenSendError.ResumeLayout(false);
            this.tab_WhenSendError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TB_WhenSendError;
        private System.Windows.Forms.TabPage tab_WhenSave;
        private System.Windows.Forms.TabPage tab_WhenSend;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.TabPage tab_WhenSendOK;
        private System.Windows.Forms.TabPage tab_WhenSendError;
        private System.Windows.Forms.TextBox TB_WhenSave;
        private System.Windows.Forms.TextBox TB_WhenSend;
        private System.Windows.Forms.TextBox TB_WhenSendOK;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Btn_Help;
        private System.Windows.Forms.TabPage tab_Help;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ImageList imageList1;
    }
}