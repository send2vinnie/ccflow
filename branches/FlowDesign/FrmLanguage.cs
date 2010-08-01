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
            Button btn = sender as Button;
            string id = btn.Name.Replace("Btn_", "");
            BP.WF.Glo.Language = id;

            //string file = Application.StartupPath + "\\Data\\TestDesc.txt";
            //string doc = "";
            //if (System.IO.File.Exists(file))
            //{
            //}
            //else
            //{
            //    file = Application.StartupPath + "\\..\\..\\Data\\TestDesc.txt";
            //}
            //try
            //{
            //    doc = BP.DA.DataType.ReadTextFile(file);
            //    MessageBox.Show(doc, "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch
            //{
            //}
            //string path = Application.StartupPath + "\\Data\\";
            //if (System.IO.Directory.Exists(path) == false)
            //    path = Application.StartupPath + "\\..\\..\\Data\\";
            ////  System.IO.File.Copy(path+"ccs_"+id+".key",
            this.Close();
        }
        public void AlertTestNetErr()
        {
            string file = Application.StartupPath + "\\Data\\TestNetErr.txt";
            string doc = "";
            if (System.IO.File.Exists(file))
            {
            }
            else
            {
                file = Application.StartupPath + "\\..\\..\\Data\\TestNetErr.txt";
            }

            try
            {
                doc = BP.DA.DataType.ReadTextFile(file);
                MessageBox.Show(doc, "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {

            }

        }

        public void AlertUpdate()
        {
            string file = Application.StartupPath + "\\Data\\TestUpdate.txt";
            string doc = "";
            if (System.IO.File.Exists(file))
            {
            }
            else
            {
                file = Application.StartupPath + "\\..\\..\\Data\\TestUpdate.txt";
            }

            try
            {
                doc = BP.DA.DataType.ReadTextFile(file);
                MessageBox.Show(doc, "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
            }

            System.Diagnostics.Process.Start("IExplore.exe", "http://ccflow.cn/ftp/flow/ccflow.msi");
            System.Diagnostics.Process.Start("IExplore.exe", "http://ccflow.cn/");
            Application.Exit();
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {
            //string file = "D:\\WorkFlow\\Front\\Web.config";
            //if (System.IO.File.Exists(file))
            //{
            //    this.Close();
            //    return;
            //}

            #region 获取远程的ver
            //string ver = "";
            //try
            //{
            //    ver = BP.HTTP.HTTPClass.ReadContext("http://ccflow.cn/ftp/flow/Ver.txt", 20000, System.Text.Encoding.GetEncoding("GB2312") );
            //    if (ver == null)
            //        ver = BP.HTTP.HTTPClass.ReadContext("http://online863.cn/ftp/flow/Ver.txt", 20000, System.Text.Encoding.GetEncoding("GB2312"));
            //    if (ver == null)
            //        ver = BP.HTTP.HTTPClass.ReadContext("http://online863.com/ftp/flow/Ver.txt", 20000, System.Text.Encoding.GetEncoding("GB2312"));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion 获取远程的ver

            //if (ver == null || ver.Length != 7)
            //{
            //    this.AlertTestNetErr();
            //    Application.Exit();
            //    return;
            //}

            //string veroflocal = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //if (veroflocal != ver)
            //{
            //    this.AlertUpdate();
            //    Application.Exit();
            //    return;
            //}

            string cfg = Application.StartupPath + ".\\..\\..\\Data\\Cfg";
            string doc = "";
            try
            {
                doc = BP.DA.DataType.ReadTextFile(cfg).Trim().Replace("\t\n", "");
            }
            catch
            {
                doc = "CCS";
            }


            string log = Application.StartupPath + ".\\..\\..\\Data\\Img_" + doc + "\\log.gif";
            Image img = Image.FromFile(log);
            this.pictureBox1.ImageLocation = log;
            this.pictureBox1.Width = img.Width;
            this.pictureBox1.Height = img.Height; 

            this.Text = "Thanks for you chose " + doc + "Flow";

            BP.WF.Glo.OEM_Flag = doc;

            switch (BP.WF.Glo.OEM_Flag )
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
            switch (BP.WF.Glo.OEM_Flag )
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