namespace BP.RB.Frm
{
    partial class FrmWebSite
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
            this.Btn_Run = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.dg1 = new BP.Win32.Controls.DG();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Run
            // 
            this.Btn_Run.Location = new System.Drawing.Point(26, 323);
            this.Btn_Run.Name = "Btn_Run";
            this.Btn_Run.Size = new System.Drawing.Size(75, 23);
            this.Btn_Run.TabIndex = 1;
            this.Btn_Run.Text = "Run";
            this.Btn_Run.UseVisualStyleBackColor = true;
            this.Btn_Run.Click += new System.EventHandler(this.Btn_Run_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(237, 323);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(75, 23);
            this.Btn_Close.TabIndex = 2;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.Location = new System.Drawing.Point(133, 323);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(75, 23);
            this.Btn_Edit.TabIndex = 3;
            this.Btn_Edit.Text = "Edit";
            this.Btn_Edit.UseVisualStyleBackColor = true;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // dg1
            // 
            this.dg1.DataMember = "";
            this.dg1.DGModel = BP.Win32.Controls.DGModel.None;
            this.dg1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dg1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dg1.IsDGReadonly = false;
            this.dg1.Location = new System.Drawing.Point(0, 0);
            this.dg1.Name = "dg1";
            this.dg1.Size = new System.Drawing.Size(370, 317);
            this.dg1.TabIndex = 4;
            // 
            // FrmWebSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 355);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.Btn_Edit);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Run);
            this.Name = "FrmWebSite";
            this.Text = "站点";
            this.Load += new System.EventHandler(this.FrmWebSite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Run;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Edit;
        private BP.Win32.Controls.DG  dg1;
    }
}