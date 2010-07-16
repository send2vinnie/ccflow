using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
///本人网名：旋风，博客园号:博客园：http://xuanfeng.cnblogs.com/ 日期:2006.5.14.   请尊重个人信息,忽删!
namespace SendEmail
{
    public partial class Form1 : Form
    {
        SmtpClient SmtpClient_my = null;
        MailAddress MailAddress_from = null;
        MailAddress MailAddress_to = null;
        MailMessage MailMessage_my = null;
        Attachment Attachment_my = null;


        public Form1()
        {
            InitializeComponent();
        }

        #region 初始化Ｓmtp服务器
        private void SmtpClientInit(string ServerName, int Port, string UseName, string password)
        {
            try
            {

                string ip = null;
                IPAddress[] IpAddress = Dns.GetHostEntry(ServerName).AddressList;
                Ping ping = new Ping();
                PingReply pingReply = null;
                //取得smt服务器可用的ＩＰ
                foreach (IPAddress IP in IpAddress)
                {
                    pingReply = ping.Send(IP);
                    if (pingReply.Status == IPStatus.Success)
                    {
                        ip = IP.ToString();
                        break;
                    }

                }

                SmtpClient_my = new SmtpClient(ip, Port);
                SmtpClient_my.Timeout = 2000;
                //创建服务器认证
                NetworkCredential NetworkCredential_my = new NetworkCredential(UseName, password);
                SmtpClient_my.Credentials = NetworkCredential_my;

                SmtpClient_my.SendCompleted += new SendCompletedEventHandler(SmtpClient_my_SendCompleted);
            }
            catch (SocketException E)
            {

                MessageBox.Show(E.ToString());
                return;
            }

        } 
        #endregion

        #region 初始化邮件的附件
        private void Attachment_myInit(string path)
        {

            if (!File.Exists(path))
            {

                MessageBox.Show("{0}文件不存在！", path);

                return;
            }
            try
            {
                FileStream FileStream_my = new FileStream(path, FileMode.Open);
                string name = FileStream_my.Name;
                int size = (int)(FileStream_my.Length / 1024);
                //控制文件大小不大于10Ｍ
                if (size > 10240)
                {

                    MessageBox.Show("文件长度不能大于１０Ｍ！你选择的文件大小为{0}", size.ToString());
                    return;
                }

                FileStream_my.Close();

                Attachment_my = new Attachment(path, MediaTypeNames.Application.Octet);



                ContentDisposition ContentDisposition_my = Attachment_my.ContentDisposition;
                ContentDisposition_my.Size = size;
                ContentDisposition_my.FileName = name;
                ContentDisposition_my.CreationDate = File.GetCreationTime(path);
                ContentDisposition_my.ModificationDate = File.GetLastWriteTime(path);
                ContentDisposition_my.ReadDate = File.GetCreationTimeUtc(path);

                MailMessage_my.Attachments.Add(Attachment_my);


            }
            catch (IOException E)
            {
                MessageBox.Show(E.Message);
            }





        } 
        #endregion

        #region 发送邮件后所处理的函数
        void SmtpClient_my_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            { MessageBox.Show("发送已取消！"); }
            if (e.Error != null)
            {

                MessageBox.Show(e.UserState.ToString() + "发送错误：" + e.Error.Message, "发送错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("邮件成功发出!", "恭喜!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        } 
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化邮件信息
            MailMessage_my = new MailMessage();
        }

        private void Button_Name_Click(object sender, EventArgs e)
        {
            try
            {
                //创建Smtp服务器
                SmtpClientInit(Tb_SmtpServer.Text, int.Parse(Tb_Port.Text), Tb_UeserName.Text, Tb_PassWord.Text);
                //创建发件人与收件人的邮箱地址
                MailAddress_from = new MailAddress(Tb_Email_from.Text, Tb_Print.Text);
                MailAddress_to = new MailAddress(Tb_Email_to.Text);
                 //创建发送信息
                MailMessage_my.Subject = Tb_Content.Text;
                MailMessage_my.ReplyTo = MailAddress_from;
                MailMessage_my.Sender = MailAddress_from;
                MailMessage_my.From = MailAddress_from;
                MailMessage_my.To.Add(MailAddress_to);

                MailMessage_my.Body = Rtb_Message.Text;
            }
            catch (ArgumentException E)
            {

                MessageBox.Show(E.Message);
            }


            string userToken = "Well!";
            if (SmtpClient_my != null)
            {
                SmtpClient_my.SendAsync(MailMessage_my, userToken);
            }
            else {

                MessageBox.Show("邮件没有发送！Ｓmtp服务器没有初始化!");
            }


          



        }
        //选择附件
        private void Bt_Path_Click(object sender, EventArgs e)
        {
            
            this.openFileDialog1.Title = "请选择附件";
            this.openFileDialog1.Filter="所有文件类型（＊．＊）|*.*";
            this.openFileDialog1.Multiselect = false;
            DialogResult Result= this.openFileDialog1.ShowDialog();
            if (Result == DialogResult.OK)
            {

                Tb_Path.Text = this.openFileDialog1.FileName.Trim();
              
            }

            if(Tb_Path.Text!=string.Empty)
            {
                Attachment_myInit(Tb_Path.Text);
            
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
       
    }
}