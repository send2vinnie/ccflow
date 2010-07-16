namespace BP.RB.Frm
{
    partial class FrmMail
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
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Smtp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_UserName = new System.Windows.Forms.TextBox();
            this.lab_oass = new System.Windows.Forms.Label();
            this.TB_Pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_Address = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_MailLab = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_Title = new System.Windows.Forms.TextBox();
            this.richTextBox_Doc = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn_View = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "执行进度表...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(57, 38);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(462, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(111, 552);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 23);
            this.Btn_Send.TabIndex = 2;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(251, 552);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(75, 23);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "smtp";
            // 
            // TB_Smtp
            // 
            this.TB_Smtp.Location = new System.Drawing.Point(103, 81);
            this.TB_Smtp.Name = "TB_Smtp";
            this.TB_Smtp.Size = new System.Drawing.Size(179, 21);
            this.TB_Smtp.TabIndex = 5;
            this.TB_Smtp.Text = "smtp.163.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "port";
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(338, 81);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(179, 21);
            this.TB_Port.TabIndex = 5;
            this.TB_Port.Text = "25";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "user";
            // 
            // TB_UserName
            // 
            this.TB_UserName.Location = new System.Drawing.Point(103, 120);
            this.TB_UserName.Name = "TB_UserName";
            this.TB_UserName.Size = new System.Drawing.Size(179, 21);
            this.TB_UserName.TabIndex = 5;
            this.TB_UserName.Text = "chichengsoft@163.com";
            // 
            // lab_oass
            // 
            this.lab_oass.AutoSize = true;
            this.lab_oass.Location = new System.Drawing.Point(288, 129);
            this.lab_oass.Name = "lab_oass";
            this.lab_oass.Size = new System.Drawing.Size(29, 12);
            this.lab_oass.TabIndex = 6;
            this.lab_oass.Text = "pass";
            // 
            // TB_Pass
            // 
            this.TB_Pass.Location = new System.Drawing.Point(338, 123);
            this.TB_Pass.Name = "TB_Pass";
            this.TB_Pass.Size = new System.Drawing.Size(179, 21);
            this.TB_Pass.TabIndex = 5;
            this.TB_Pass.Text = "public";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "address";
            // 
            // TB_Address
            // 
            this.TB_Address.Location = new System.Drawing.Point(103, 173);
            this.TB_Address.Name = "TB_Address";
            this.TB_Address.Size = new System.Drawing.Size(179, 21);
            this.TB_Address.TabIndex = 5;
            this.TB_Address.Text = "chichengsoft@163.com";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "lab";
            // 
            // TB_MailLab
            // 
            this.TB_MailLab.Location = new System.Drawing.Point(338, 173);
            this.TB_MailLab.Name = "TB_MailLab";
            this.TB_MailLab.Size = new System.Drawing.Size(179, 21);
            this.TB_MailLab.TabIndex = 5;
            this.TB_MailLab.Text = "华夏财税人";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.richTextBox_Doc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TB_Title);
            this.groupBox1.Location = new System.Drawing.Point(55, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 319);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送内容";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Title";
            // 
            // TB_Title
            // 
            this.TB_Title.Location = new System.Drawing.Point(64, 29);
            this.TB_Title.Name = "TB_Title";
            this.TB_Title.Size = new System.Drawing.Size(381, 21);
            this.TB_Title.TabIndex = 5;
            this.TB_Title.Text = "chichengsoft@163.com";
            // 
            // richTextBox_Doc
            // 
            this.richTextBox_Doc.Location = new System.Drawing.Point(19, 63);
            this.richTextBox_Doc.Name = "richTextBox_Doc";
            this.richTextBox_Doc.Size = new System.Drawing.Size(426, 207);
            this.richTextBox_Doc.TabIndex = 6;
            this.richTextBox_Doc.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "File";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 281);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(381, 21);
            this.textBox1.TabIndex = 7;
            // 
            // Btn_View
            // 
            this.Btn_View.Location = new System.Drawing.Point(389, 552);
            this.Btn_View.Name = "Btn_View";
            this.Btn_View.Size = new System.Drawing.Size(75, 23);
            this.Btn_View.TabIndex = 8;
            this.Btn_View.Text = "View";
            this.Btn_View.UseVisualStyleBackColor = true;
            // 
            // FrmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 587);
            this.Controls.Add(this.Btn_View);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lab_oass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_UserName);
            this.Controls.Add(this.TB_Address);
            this.Controls.Add(this.TB_MailLab);
            this.Controls.Add(this.TB_Pass);
            this.Controls.Add(this.TB_Port);
            this.Controls.Add(this.TB_Smtp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Name = "FrmMail";
            this.Text = "FrmMail";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Smtp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_UserName;
        private System.Windows.Forms.Label lab_oass;
        private System.Windows.Forms.TextBox TB_Pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_Address;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_MailLab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox_Doc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_Title;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Btn_View;
    }
}