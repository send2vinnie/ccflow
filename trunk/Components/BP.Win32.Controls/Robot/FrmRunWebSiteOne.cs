using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using BP.RB;
using System.Text.RegularExpressions;

namespace BP.RB.Frm
{
    public partial class FrmRunWebSiteOne : Form
    {
        public FrmRunWebSiteOne()
        {
            InitializeComponent();
        }
        private void FrmRunWebSiteOne_Load(object sender, EventArgs e)
        {
            this.bindEds();
        }
        public WebSite HisWebSite = null;

        public void AddMsg(string msg)
        {
            this.textBox1.Text += "\r\n" + msg;
        }

        public void bindEds()
        {
            RB.Encodes eds = new Encodes();
            eds.RetrieveAll();

            foreach (Encode ed in eds)
            {
                this.listBox1.Items.Add(ed.No);
            }


        }

        private void Btn_Run_Click(object sender, EventArgs e)
        {

            //运行前要做的工作。
            this.HisWebSite.DoBeforeRun();

            this.progressBar1.Step  = 1;
            this.progressBar1.Value = 0;

            string docs = PubClass.ReadContext(this.HisWebSite.Url, this.HisWebSite.HisEncode);
            this.AddMsg(PubClass.ReadContextMsg);
            this.AddMsg(docs);

            Hrefs hfs = PubClass.GetHrefs(this.HisWebSite.Url, this.HisWebSite.HisEncode);
            this.progressBar1.Maximum = hfs.Count;
            foreach (Href hf in hfs)
            {
                this.AddMsg(PubClass.ReadContextMsg);
                this.progressBar1.Value++;
                this.statusStrip1.Text = hf.Url;
                PubClass.DoPage(hf);
            }

            BP.DA.Log.OpenLogDir();


            //BP.DA.DBAccess.RunSQL("DELETE RB_Page WHERE HostName='" + this.HisWebSite.HostName + "'");
            //this.progressBar1.Step = 1;
            //this.progressBar1.Value = 0;

            //string docs = PubClass.ReadContext(this.HisWebSite.Url, this.HisWebSite.HisEncode);
            //this.AddMsg(PubClass.ReadContextMsg);
            //this.AddMsg(docs);

            //Hrefs hfs = PubClass.GetHrefs(docs, this.HisWebSite.HisEncode, this.HisWebSite.HostName);
            //this.progressBar1.Maximum = hfs.Count;
            //foreach (Href hf in hfs)
            //{
            //    this.AddMsg(PubClass.ReadContextMsg);

            //    this.progressBar1.Value++;
            //    this.statusStrip1.Text = hf.Url;
            //    PubClass.DoPage(hf);
            //}
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_TestCoding_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.AddMsg(this.HisWebSite.Url);
            if (this.listBox1.SelectedItem == null)
            {
                this.AddMsg("请选择 Encoding  类型。");
                return;
            }
            this.AddMsg("您选的格式:" + this.listBox1.SelectedItem.ToString() );

            Encoding ed = Encoding.GetEncoding(this.listBox1.SelectedItem.ToString()); 
            string docs = PubClass.ReadContext(this.HisWebSite.Url, ed);
            this.AddMsg(docs);
            this.AddMsg(PubClass.ReadContextMsg);
            this.AddMsg("msg run ok ...");
            this.HisWebSite.FK_Encode = this.listBox1.SelectedItem.ToString();
            this.HisWebSite.Update();
            return ;


            Uri PageUrl = new Uri( this.HisWebSite.Url ); 

            WebRequest request = WebRequest.Create(PageUrl); 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 
            Stream resStream = response.GetResponseStream(); 
            StreamReader sr; 
            System.Text.Encoding enc = Encoding.Default;  // 
            StreamReader streamReader = new StreamReader(resStream, enc);
            string str = streamReader.ReadToEnd();
            response.Close();
            streamReader.Close();

            Regex regEncoding = new Regex(@"charset=([\w-]*)\.*"); 
 
            Match m = regEncoding.Match(str);
            if (m.Success)
            {
                Encoding getenc = Encoding.GetEncoding(m.Groups[1].ToString());
                byte[] gbytes = enc.GetBytes(str);
                string c = getenc.GetString(gbytes);

                this.AddMsg(c);
                //  response.Write(c);
            }
        }

    }
}