using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BP.RB;
using BP.DA;
using BP.En;
using BP.SE;

namespace BP.RB.Frm
{
    public partial class FrmHome : BP.Win32.PageBase // System.Windows.Forms.Form // BP.Win32.PageBase
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
        }

        private void MBtn_RumPage_Click(object sender, EventArgs e)
        {
            FrmRun fr = new FrmRun();
            fr.ShowDialog();
        }

        private void webSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BP.RB.WebSites wss = new WebSites();
            wss.RetrieveAll();
            this.InvokUIEns(wss);
        }

        private void pageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pages wss = new Pages();
            wss.RetrieveAll();
             this.InvokUIEns(wss);
        }

        private void Btn_Do_Click(object sender, EventArgs e)
        {
            BP.RB.Pages pgs = new Pages();
            pgs.RetrieveAll(10000);
            foreach (Page pg in pgs)
            {
                pg.GenerEmails();
            }

            MessageBox.Show("Ö´ÐÐÍê±Ï¡£");
        }

        private void Btn_WebSite_Click(object sender, EventArgs e)
        {
            FrmWebSite fr = new FrmWebSite();
            fr.ShowDialog();
        }

        private void webSiteTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BP.SE.WebSiteTypes wss = new WebSiteTypes();
            wss.RetrieveAll();
            this.InvokUIEns(wss);
        }
    }
}