using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace BP.GE.GEMail
{
    /// <summary>
    /// Email发送类
    /// </summary>
    public class GEMail
    {
        private string _subject = "";
        private string _body = "";
        private string _attachmentPath = "";
        private string _smtpHost = "";
        private int _smtpPort = 25;
        private string _smtpAccount = "";
        private string _smtpPass = "";
        private bool _isSSL = false;
        private bool _isBodyHtml = false;
        private string _from = "";
        private string _fromName = "";
        private string _to = "";
        private Encoding _encoding = Encoding.GetEncoding(936);

        private SmtpClient smtp = null;
        private MailMessage mail = null;

        #region 属性
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }
        /// <summary>
        /// 附件文件路径
        /// </summary>
        public string AttachmentPath
        {
            get
            {
                return _attachmentPath;
            }
            set
            {
                _attachmentPath = value;
            }
        }
        /// <summary>
        /// SMTP服务器地址
        /// </summary>
        public string SmtpHost
        {
            get
            {
                return _smtpHost;
            }
            set
            {
                _smtpHost = value;
            }
        }
        /// <summary>
        /// SMTP服务器端口
        /// </summary>
        public int SmtpPort
        {
            get
            {
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
            }
        }
        /// <summary>
        /// 发信邮箱的email账号(@后面的邮箱域名一定要写上)
        /// </summary>
        public string SmtpAccount
        {
            get
            {
                return _smtpAccount;
            }
            set
            {
                _smtpAccount = value;
            }
        }
        /// <summary>
        /// 发信邮箱的email验证密码
        /// </summary>
        public string SmtpPass
        {
            get
            {
                return _smtpPass;
            }
            set
            {
                _smtpPass = value;
            }
        }
        /// <summary>
        /// smtp服务器是否启用SSL加密
        /// </summary>
        public bool IsSSL
        {
            get
            {
                return _isSSL;
            }
            set
            {
                _isSSL = value;
            }
        }
        /// <summary>
        /// 邮件内容是否以Html格式发送
        /// </summary>
        public bool IsBodyHtml
        {
            get
            {
                return _isBodyHtml;
            }
            set
            {
                _isBodyHtml = value;
            }
        }
        /// <summary>
        /// 发信人email地址，一般同smtp验证email一样
        /// </summary>
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }
        /// <summary>
        /// 发信人名称（显示在邮件头部）
        /// </summary>
        public string FromName
        {
            get
            {
                return _fromName;
            }
            set
            {
                _fromName = value;
            }
        }
        /// <summary>
        /// 收信人email地址
        /// </summary>
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }
        /// <summary>
        /// 邮件发送编码(GB2312是936,UTF-8是65001,BIG5是950)
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }
        #endregion

        #region 构造方法
        public GEMail()
        { }

        /// <summary>
        /// 构造电子邮件对象
        /// </summary>
        /// <param name="smtpAddr">smtp服务器地址（如：stmp.163.com）</param>
        /// <param name="smtpPort">smtp服务器端口（一般默认为25）</param>
        /// <param name="smtpAccount">smtp登录账号（一般为你的电子邮箱地址）</param>
        /// <param name="smtpPass">smtp登录密码（你的电子邮箱登录密码）</param>
        /// <param name="isSmtpSSL">smtp服务器是否支持SSL加密</param>
        public GEMail(string smtpAddr,int smtpPort,string smtpAccount,string smtpPass,bool isSmtpSSL)
        {
            this.SmtpHost = smtpAddr;
            this.SmtpPort = smtpPort;
            this.SmtpAccount = smtpAccount;
            this.SmtpPass = smtpPass;
            this.From = smtpAccount;
            this.IsSSL = isSmtpSSL;
        }

        /// <summary>
        /// 构造电子邮件对象
        /// </summary>
        /// <param name="smtpAddr">smtp服务器地址（如：stmp.163.com）</param>
        /// <param name="smtpAccount">smtp登录账号（一般为你的电子邮箱地址）</param>
        /// <param name="smtpPass">smtp登录密码（你的电子邮箱登录密码）</param>
        /// <param name="isSmtpSSL">smtp服务器是否支持SSL加密</param>
        /// <param name="fromName">显示在发信人栏里的发信人名字</param>
        /// <param name="to">收件人email地址</param>
        /// <param name="codePage">邮件的编码格式（默认：中文[936]）</param>
        public GEMail(string smtpAddr,string smtpAccount,string smtpPass,bool isSmtpSSL,string fromName,string to,int codePage)
        {
            this.SmtpHost = smtpAddr;
            this.SmtpAccount = smtpAccount;
            this.From = smtpAccount;
            this.SmtpPass = smtpPass;
            this.IsSSL = isSmtpSSL;
            this.FromName = fromName;
            this.To = to;
            this.Encoding = Encoding.GetEncoding(codePage);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化smtp服务器
        /// </summary>
        /// <returns>成功返回true</returns>
        private bool initSmtpServer()
        {
            try
            {
                if (smtp == null)
                {
                    smtp = new SmtpClient(this.SmtpHost, this.SmtpPort);
                }
                smtp.EnableSsl = this.IsSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(this.SmtpAccount, this.SmtpPass);
                smtp.Timeout = 120000;//2minutes
                return true;
            }
            catch(Exception ex)
            { return false; }
        }

        /// <summary>
        /// 发送email
        /// </summary>
        /// <returns>成功返回true</returns>
        public bool sendMail()
        {
            bool isok = false;
            try
            {
                if (!initSmtpServer())
                    throw new Exception();

                if (smtp != null)
                {
                    mail = new MailMessage();
                    mail.From = new MailAddress(this.From, this.FromName, this.Encoding);
                    mail.Sender = new MailAddress(this.From, this.FromName, this.Encoding);
                    mail.To.Add(this.To);
                    mail.IsBodyHtml = this.IsBodyHtml;
                    mail.Priority = MailPriority.High;
                    mail.SubjectEncoding = this.Encoding;
                    mail.BodyEncoding = this.Encoding;
                    mail.Subject = this.Subject;
                    mail.Body = this.Body;

                    smtp.Send(mail);
                    isok = true;
                }
            }
            catch(Exception ex)
            {
                isok = false;
            }

            return isok;
        }

        /// <summary>
        /// 发送email
        /// </summary>
        /// <param name="to">收件人email地址</param>
        /// <param name="subject">email标题</param>
        /// <param name="body">email内容</param>
        /// <param name="isBodyHtml">email内容是否以Html格式发送</param>
        /// <returns>成功返回true</returns>
        public bool sendMail(string to,string subject,string body,bool isBodyHtml)
        {
            bool isok = false;
            try
            {
                if (!initSmtpServer())
                    throw new Exception();

                if (smtp != null)
                {
                    mail = new MailMessage();
                    mail.From = new MailAddress(this.From, this.FromName, this.Encoding);
                    mail.Sender = new MailAddress(this.From, this.FromName, this.Encoding);
                    mail.To.Add(to);
                    mail.IsBodyHtml = isBodyHtml;
                    mail.Priority = MailPriority.High;
                    mail.SubjectEncoding = this.Encoding;
                    mail.BodyEncoding = this.Encoding;
                    mail.Subject = subject;
                    mail.Body = body;

                    smtp.Send(mail);
                    isok = true;
                }
            }
            catch(Exception ex)
            {
                isok = false;
            }

            return isok;
        }
        #endregion
    }
}
