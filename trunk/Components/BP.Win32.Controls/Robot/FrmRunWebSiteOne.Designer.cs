namespace BP.RB.Frm
{
    partial class FrmRunWebSiteOne
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Btn_Run = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn_TestCoding = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(653, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 342);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(726, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Btn_Run
            // 
            this.Btn_Run.Location = new System.Drawing.Point(163, 12);
            this.Btn_Run.Name = "Btn_Run";
            this.Btn_Run.Size = new System.Drawing.Size(75, 23);
            this.Btn_Run.TabIndex = 2;
            this.Btn_Run.Text = "Run";
            this.Btn_Run.UseVisualStyleBackColor = true;
            this.Btn_Run.Click += new System.EventHandler(this.Btn_Run_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(525, 12);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(75, 23);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(187, 105);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(527, 223);
            this.textBox1.TabIndex = 4;
            // 
            // Btn_TestCoding
            // 
            this.Btn_TestCoding.Location = new System.Drawing.Point(334, 13);
            this.Btn_TestCoding.Name = "Btn_TestCoding";
            this.Btn_TestCoding.Size = new System.Drawing.Size(75, 23);
            this.Btn_TestCoding.TabIndex = 5;
            this.Btn_TestCoding.Text = "Enconding";
            this.Btn_TestCoding.UseVisualStyleBackColor = true;
            this.Btn_TestCoding.Click += new System.EventHandler(this.Btn_TestCoding_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 105);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(155, 220);
            this.listBox1.TabIndex = 6;
            // 
            // FrmRunWebSiteOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 364);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Btn_TestCoding);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Run);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRunWebSiteOne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRunWebSiteOne";
            this.Load += new System.EventHandler(this.FrmRunWebSiteOne_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button Btn_Run;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Btn_TestCoding;
        private System.Windows.Forms.ListBox listBox1;
    }
}