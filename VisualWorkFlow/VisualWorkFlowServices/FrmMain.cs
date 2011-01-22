using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMSServices
{
    public enum ScanSta
    {
        Working,
        Pause,
        Stop
    }
    public partial class FrmMain : Form
    {
        delegate void SetTextCallback(string text);
        public FrmMain()
        {
            InitializeComponent();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.notifyIcon1.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // this.toolStripStatusLabel1.Text = "服务停止状态...";
            this.toolStripStatusLabel1.Text = "服务暂停";

            this.textBox1.Text = "服务停止...";
            this.Btn_StartStop.Text = "启动";
        }

        Thread thread = null;
        private void Btn_StartStop_Click(object sender, EventArgs e)
        {
            if (this.Btn_StartStop.Text == "启动")
            {
                if (this.thread == null)
                {
                 //   Glo.ScanDT = null;
                    //  Glo.CurrPoll = this.HisPoll;
                    ThreadStart ts = new ThreadStart(ReadFiles);
                    thread = new Thread(ts);
                    thread.Start();
                    this.Btn_StartStop.Text = "暂停";
                }

                this.HisScanSta = ScanSta.Working;
                this.SetText("服务启动***********" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                this.Btn_StartStop.Text = "暂停";
                this.toolStripStatusLabel1.Text = "服务启动";
            }
            else
            {
                this.SetText("服务暂停***********"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                this.HisScanSta = ScanSta.Pause;
                this.Btn_StartStop.Text = "启动";
                this.toolStripStatusLabel1.Text = "服务暂停";

            }
        }
        public ScanSta HisScanSta = ScanSta.Pause;

        public void ReadFiles()
        {
            //BP.WF.Flow fl = new BP.WF.Flow();
            //fl.No = "009";
            //fl.Name = "sssss";
            //fl.Insert();


            //BP.WF.Flow fl2 = new BP.WF.Flow("009");

            // fl2.Name="sdsdsdsdsdsd";
            // fl2.FK_FlowSort = "01";
            // fl2.Update();

            // fl2.Delete();


            // BP.WF.Flow fl4 = new BP.WF.Flow();
            // fl4.No = "002";
            // fl4.Retrieve();

            // BP.WF.Flows ens = new BP.WF.Flows();
            // ens.RetrieveAll();
            // foreach (BP.WF.Flow en in ens)
            // {
            // }


            // BP.WF.Flows ens1111 = new BP.WF.Flows();
            // BP.En.QueryObject qo = new BP.En.QueryObject(ens1111);
            // qo.AddWhere("FK_FlowSort", "01");
            // qo.DoQuery();
            // foreach (BP.WF.Flow en in ens1111)
            // {
            // }



             





            HisScanSta = ScanSta.Working;
            int idx = 0;
            while (true)
            {
                while (this.HisScanSta == ScanSta.Pause)
                {
                    System.Threading.Thread.Sleep(3000);
                    if (this.checkBox1.Checked)
                        Console.Beep();
                    //this.SetText("暂停中...");
                }

                BP.TA.SMSs sms = new BP.TA.SMSs();
                BP.En.QueryObject qo = new BP.En.QueryObject(sms);
                //    qo.AddWhere(BP.TA.SMSAttr.SMSSta, (int)BP.TA.SMSSta.UnRun);
                sms.Retrieve(BP.TA.SMSAttr.SMSSta, (int)BP.TA.SMSSta.UnRun);

                foreach (BP.TA.SMS sm in sms)
                {
                    if (this.HisScanSta == ScanSta.Stop)
                        return;

                    while (this.HisScanSta == ScanSta.Pause)
                    {
                        if (this.HisScanSta == ScanSta.Stop)
                            return;

                        System.Threading.Thread.Sleep(3000);

                        if (this.checkBox1.Checked)
                            Console.Beep();
                        //this.SetText("暂停中...");
                    }

                    try
                    {
                        this.SetText("@执行：" + sm.Tel + " email: " + sm.Email);
                        this.SendMail(sm);

                        idx++;
                        this.SetText("已完成 , 第:" + idx + " 个.");
                        this.SetText("--------------------------------");

                        if (this.checkBox1.Checked)
                            Console.Beep();
                    }
                    catch (Exception ex)
                    {
                        this.SetText("@错误：" + ex.Message);
                    }
                }

                //   this.SetText("已执行:" + idx + " 数据.....");
                System.Threading.Thread.Sleep(1000);

                switch (this.toolStripStatusLabel1.Text)
                {
                    case "服务启动":
                        this.toolStripStatusLabel1.Text = "服务启动..";
                        break;
                    case "服务启动..":
                        this.toolStripStatusLabel1.Text = "服务启动....";
                        break;
                    case "服务启动....":
                        this.toolStripStatusLabel1.Text = "服务启动......";
                        break;
                    default:
                        this.toolStripStatusLabel1.Text = "服务启动";
                        break;
                }
            }
        }
        /// <summary>
        /// 发送邮件。
        /// </summary>
        /// <param name="sms"></param>
        public void SendMail(BP.TA.SMS sms)
        {


            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);
           // myEmail.From = new MailAddress("pengzhou86@gmail.com", "public", System.Text.Encoding.UTF8);


            myEmail.To.Add( sms.Email);
            myEmail.Subject = sms.EmailTitle;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码


            myEmail.Body = sms.EmailDoc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            myEmail.IsBodyHtml = true;//是否是HTML邮件

            myEmail.Priority = MailPriority.High;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("ccflow.cn@gmail.com", "www.ccflow.cn");

            //上述写你的GMail邮箱和密码
            client.Port = 587;//Gmail使用的端口
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;//经过ssl加密
            object userState = myEmail;
            try
            {
                //简单一点儿可以client.Send(msg);
                // MessageBox.Show("发送成功");

                client.SendAsync(myEmail, userState);

                sms.HisSMSSta = BP.TA.SMSSta.RunOK;
                sms.Update();
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);

                try
                {
                    this.Invoke(d, new object[] { text });
                }
                catch
                {
                }
            }
            else
            {
                this.textBox1.Text += "\r\n" + text;
                this.textBox1.SelectionStart = this.textBox1.TextLength;
                this.textBox1.ScrollToCaret();
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.HisScanSta = ScanSta.Stop;
            this.Close();
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1_Click(null, null);
        }
    }
}
