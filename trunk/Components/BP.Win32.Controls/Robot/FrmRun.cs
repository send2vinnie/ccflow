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

namespace BP.RB.Frm
{
    public partial class FrmRun : Form
    {
        public FrmRun()
        {
            InitializeComponent();
        }

        private void FrmRun_Load(object sender, EventArgs e)
        {

        }
        public void AddMsg(string msg)
        {
            this.TB_Msg.Text += "\r\n" + msg;
        }
        public int NumOfAll = 0;
        public int NumOfThisSite = 0;

        public bool IsRun = true;

        private void Btn_Run_Click(object sender, EventArgs e)
        {
            this.TB_Msg.Text = "";

            WebSites wss = new WebSites();
            wss.RetrieveAll();

            this.progressBar1.Maximum = wss.Count;
            this.progressBar1.Step = 1;
            this.progressBar1.Value = 0;
            this.IsRun = true;

            foreach (WebSite ws in wss)
            {
                this.NumOfThisSite = 0;

                this.progressBar1.Value++;
                DateTime dtFrom = DateTime.Now;

                this.label1.Text = "正在执行网站:" + ws.Name;
                this.AddMsg("************* 开始执行： " + ws.Name + ws.Url + " ******************");

                string docs = PubClass.ReadContext(ws.Url, ws.HisEncode);
                this.AddMsg(PubClass.ReadContextMsg);

                Hrefs hrefs = PubClass.GetHrefs(ws.Url, ws.HisEncode);
                foreach (Href hf in hrefs)
                {
                    PubClass.DoPage(hf);
                }

                DateTime dtNow = DateTime.Now;
                TimeSpan ts = dtFrom - dtNow;

                ws.UDT = DataType.CurrentDataTime;

                ws.NumOfPage = this.NumOfThisSite;

                ws.UDTFrom = dtFrom.ToString(DataType.SysDataTimeFormat);
                ws.UDTTo = dtNow.ToString(DataType.SysDataTimeFormat);
                ws.S = -ts.Seconds;
                ws.Update();
            }
            MessageBox.Show(" run ok ...");
        }
        /// <summary>
        /// Btn_Stop_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            this.IsRun = !this.IsRun;
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}