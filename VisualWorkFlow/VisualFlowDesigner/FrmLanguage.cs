using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BP.Win32
{
    public partial class FrmLanguage : Form
    {
        public FrmLanguage()
        {
            InitializeComponent();
        }
        private void Btn_CH_Click(object sender, EventArgs e)
        {
         
            //VisualFlowDesigner.Form1 fm = new VisualFlowDesigner.Form1();
            //fm.Show();
            //return;
            Button btn = sender as Button;
            string id = btn.Name.Replace("Btn_", "");
            BP.WF.Glo.Language = id;
            this.Close();
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {
            string doc = "CCS";
            string log =  BP.WF.Global.PathOfBase +"\\Data\\Img_" + doc + "\\log.gif";
            Image img = Image.FromFile(log);
            this.pictureBox1.ImageLocation = log;
            this.pictureBox1.Width = img.Width;
            this.pictureBox1.Height = img.Height; 

            this.Text = "Thanks for you chose Visual Workflow";

            BP.WF.Glo.OEM_Flag = doc;
            switch (BP.WF.Glo.OEM_Flag)
            {
                case "Engg":
                    this.linkLabel1.Tag = "s";
                    this.linkLabel1.Text = "http://www.quickcad.com.tw";
                    break;
                default:
                    this.linkLabel1.Tag = "s";
                    this.linkLabel1.Text = "http://www.ccflow.cn";
                    break;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (BP.WF.Glo.OEM_Flag)
            {
                case "Engg":
                    System.Diagnostics.Process.Start("http://www.quickcad.com.tw");
                    break;
                default:
                    System.Diagnostics.Process.Start("http://www.ccflow.cn");
                    break;
            }
        }
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1_LinkClicked(sender, e);
        }
    }
}