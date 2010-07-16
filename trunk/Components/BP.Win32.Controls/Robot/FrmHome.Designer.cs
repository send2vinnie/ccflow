namespace BP.RB.Frm
{
    partial class FrmHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Btn_File = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.数据维护ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.webSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MBtn_RumPage = new System.Windows.Forms.ToolStripButton();
            this.Btn_Do = new System.Windows.Forms.ToolStripButton();
            this.Btn_WebSite = new System.Windows.Forms.ToolStripButton();
            this.TBtn_Exit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.webSiteTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_File,
            this.数据维护ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Btn_File
            // 
            this.Btn_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Exit});
            this.Btn_File.Name = "Btn_File";
            this.Btn_File.Size = new System.Drawing.Size(41, 20);
            this.Btn_File.Text = "File";
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(94, 22);
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // 数据维护ToolStripMenuItem1
            // 
            this.数据维护ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webSiteToolStripMenuItem,
            this.webSiteTypeToolStripMenuItem,
            this.pageToolStripMenuItem});
            this.数据维护ToolStripMenuItem1.Name = "数据维护ToolStripMenuItem1";
            this.数据维护ToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.数据维护ToolStripMenuItem1.Text = "Data";
            // 
            // webSiteToolStripMenuItem
            // 
            this.webSiteToolStripMenuItem.Name = "webSiteToolStripMenuItem";
            this.webSiteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.webSiteToolStripMenuItem.Text = "WebSite";
            this.webSiteToolStripMenuItem.Click += new System.EventHandler(this.webSiteToolStripMenuItem_Click);
            // 
            // pageToolStripMenuItem
            // 
            this.pageToolStripMenuItem.Name = "pageToolStripMenuItem";
            this.pageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pageToolStripMenuItem.Text = "Page";
            this.pageToolStripMenuItem.Click += new System.EventHandler(this.pageToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MBtn_RumPage,
            this.Btn_Do,
            this.Btn_WebSite,
            this.TBtn_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(644, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MBtn_RumPage
            // 
            this.MBtn_RumPage.Image = ((System.Drawing.Image)(resources.GetObject("MBtn_RumPage.Image")));
            this.MBtn_RumPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MBtn_RumPage.Name = "MBtn_RumPage";
            this.MBtn_RumPage.Size = new System.Drawing.Size(73, 22);
            this.MBtn_RumPage.Text = "执行网页";
            this.MBtn_RumPage.Click += new System.EventHandler(this.MBtn_RumPage_Click);
            // 
            // Btn_Do
            // 
            this.Btn_Do.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Do.Image")));
            this.Btn_Do.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Do.Name = "Btn_Do";
            this.Btn_Do.Size = new System.Drawing.Size(103, 22);
            this.Btn_Do.Text = "执行产生Email";
            this.Btn_Do.Click += new System.EventHandler(this.Btn_Do_Click);
            // 
            // Btn_WebSite
            // 
            this.Btn_WebSite.Image = ((System.Drawing.Image)(resources.GetObject("Btn_WebSite.Image")));
            this.Btn_WebSite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_WebSite.Name = "Btn_WebSite";
            this.Btn_WebSite.Size = new System.Drawing.Size(73, 22);
            this.Btn_WebSite.Text = "网站管理";
            this.Btn_WebSite.Click += new System.EventHandler(this.Btn_WebSite_Click);
            // 
            // TBtn_Exit
            // 
            this.TBtn_Exit.Image = ((System.Drawing.Image)(resources.GetObject("TBtn_Exit.Image")));
            this.TBtn_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBtn_Exit.Name = "TBtn_Exit";
            this.TBtn_Exit.Size = new System.Drawing.Size(49, 22);
            this.TBtn_Exit.Text = "退出";
            this.TBtn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 379);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(644, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // webSiteTypeToolStripMenuItem
            // 
            this.webSiteTypeToolStripMenuItem.Name = "webSiteTypeToolStripMenuItem";
            this.webSiteTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.webSiteTypeToolStripMenuItem.Text = "WebSiteType";
            this.webSiteTypeToolStripMenuItem.Click += new System.EventHandler(this.webSiteTypeToolStripMenuItem_Click);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 401);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Btn_File;
        private System.Windows.Forms.ToolStripMenuItem Btn_Exit;
        private System.Windows.Forms.ToolStripMenuItem 数据维护ToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton MBtn_RumPage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton TBtn_Exit;
        private System.Windows.Forms.ToolStripMenuItem webSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton Btn_Do;
        private System.Windows.Forms.ToolStripButton Btn_WebSite;
        private System.Windows.Forms.ToolStripMenuItem webSiteTypeToolStripMenuItem;
    }
}