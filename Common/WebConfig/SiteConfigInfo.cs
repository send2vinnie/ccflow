using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Tax666.Common
{
    /// <summary>
    /// 网站基本设置描述类, 加[Serializable]标记为可序列化IConfigInfo
    /// </summary>
    [Serializable]
    public class SiteConfigInfo : IConfigInfo
    {
        #region 私有字段
        /// <summary>
        /// 网站的基本配置
        /// </summary>
        private string m_weburl = "http://www.tax666.com/";           //网站url地址
        private string m_webtitle = "天道管理、税务咨询高端服务门户-面向高端企业管理、税务咨询服务专家";         //网站的名称

        private int m_siteenabled = 0;                                    //1-网站被授权正常使用;0-未授权无法使用；
        private int m_closed = 0;		                            //1-关闭维护；0-正常；
        private string m_reason = "抱歉!网站正在进行维护,暂时关闭,请稍后访问.";          //网站关闭提示信息
        private string defaultkey = "foxnet990812";                       //DES加密的默认密钥
        private string m_seotitle = "天道管理、税务咨询高端服务门户-企业管理咨询,税务咨询,高端专业服务,管理培训,税务考试辅导,教学课件";         //标题附加字
        private string m_seokeywords = "天道,管理,税务,咨询,高端,服务门户,企业管理,税务咨询,培训,考试辅导,专业,tax,税务资讯专题,培训视频,教学课件,法规,财税,热点,咨询,投资,税收筹划,专家,答疑,纳税,辅导,会计,实务,政策,解读,独家";   //Meta Keywords
        private string m_seodescription = "天道管理、税务咨询高端服务门户,面向高端企业管理税务业务咨询及服务的专家";      //Meta Description

        private int m_themeid = 1;
        private string m_themedesc = "默认风格";
        private string m_themepath = "Default";
        private string authentinfo = "申请中...";

        /// <summary>
        /// 客户服务信息
        /// </summary>
        private string m_serviceqq = "863858965";
        private string m_saleqq = "863858965";
        private string m_telphone = "86-0755-83279705";
        private string m_fax = "86-0755-83279705";
        private string m_email = "qifl23702570@163.com";

        private int m_nocacheheaders = 0;                                    //禁止浏览器缓冲

        /// <summary>
        /// 用户注册访问权限配置
        /// </summary>
        private int m_regstatus = 1;                                    //是否允许新用户注册
        private int m_doublee = 0;                                    //允许同一 Email 注册不同用户
        private int m_regverify = 0;                                    //新用户注册验证。0－无；1－Email验证；2－人工审核；
        private string m_censoruser = "admin";                             //用户信息保留关键字

        private int m_regctrl = 0;            //IP 注册间隔限制(小时)
        private string m_ipregctrl = "";           //特殊 IP 注册限制
        private string m_ipdenyaccess = "";           //IP禁止访问列表
        private string m_ipaccess = "";           //IP访问列表
        private string adminipdenyaccess = "";	        //代理商直销商IP访问列表；
        private string adminipaccess = "";		    //代理商直销商IP禁止访问列表

        private int m_welcomemsg = 1;            //发送欢迎短消息
        private string m_msg = "";           //欢迎短消息内容
        private int m_sendemil = 0;            //发送欢迎短消息
        private string m_emailcontent = "";

        private string smtp = "mail.163.com";           //smtp 地址
        private int port = 25;           //端口号
        private string sysemail = "qfl@163.net";           //系统邮件地址
        private string username = "qfl";           //邮件帐号
        private string password = "12345";           //邮件密码

        private int m_searchctrl = 0;            //搜索时间限制(秒)
        private int m_maxspm = 5;            //60 秒最大搜索次数
        private int m_maxonlines = 10000;         //最大在线人数
        private string m_visitbanperiods = "";          //禁止访问时间段
        private string m_searchbanperiods = "";         //禁止全文搜索时间段

        #endregion

        #region 属性

        /// <summary>
        /// 网站基本配置
        /// </summary>
        public string Webtitle
        {
            get { return m_webtitle; }
            set { m_webtitle = value; }
        }

        /// <summary>
        /// 网站url地址
        /// </summary>
        public string Weburl
        {
            get { return m_weburl; }
            set { m_weburl = value; }
        }

        /// <summary>
        /// 0-网站正常;1-关闭维护；
        /// </summary>
        public int SiteEnabled
        {
            get { return m_siteenabled; }
            set { m_siteenabled = value; }
        }

        public int Closed
        {
            get { return m_closed; }
            set { m_closed = value; }
        }

        /// <summary>
        /// 网站关闭提示信息
        /// </summary>
        public string Reason
        {
            get { return m_reason; }
            set { m_reason = value; }
        }

        /// <summary>
        /// DES加密的默认密钥
        /// </summary>
        public string DefaultKey
        {
            get { return defaultkey; }
            set { defaultkey = value; }
        }

        /// <summary>
        /// 标题附加字
        /// </summary>
        public string Seotitle
        {
            get { return m_seotitle; }
            set { m_seotitle = value; }
        }

        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string Seokeywords
        {
            get { return m_seokeywords; }
            set { m_seokeywords = value; }
        }

        /// <summary>
        /// Meta Description
        /// </summary>
        public string Seodescription
        {
            get { return m_seodescription; }
            set { m_seodescription = value; }
        }

        public int ThemeID
        {
            get { return m_themeid; }
            set { m_themeid = value; }
        }

        public string Themedesc
        {
            get { return m_themedesc; }
            set { m_themedesc = value; }
        }

        public string Themepath
        {
            get { return m_themepath; }
            set { m_themepath = value; }
        }

        /// <summary>
        /// 禁止浏览器缓冲
        /// </summary>
        public int Nocacheheaders
        {
            get { return m_nocacheheaders; }
            set { m_nocacheheaders = value; }
        }

        public string AuthentInfo
        {
            get { return authentinfo; }
            set { authentinfo = value; }
        }

        /// <summary>
        /// 客户服务信息
        /// </summary>
        public string ServiceQQ
        {
            get { return m_serviceqq; }
            set { m_serviceqq = value; }
        }

        public string SaleQQ
        {
            get { return m_saleqq; }
            set { m_saleqq = value; }
        }

        public string Telphone
        {
            get { return m_telphone; }
            set { m_telphone = value; }
        }

        public string Fax
        {
            get { return m_fax; }
            set { m_fax = value; }
        }

        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }

        /// <summary>
        /// 是否允许新用户注册
        /// </summary>
        public int Regstatus
        {
            get { return m_regstatus; }
            set { m_regstatus = value; }
        }

        /// <summary>
        /// 允许同一 Email 注册不同用户
        /// </summary>
        public int Doublee
        {
            get { return m_doublee; }
            set { m_doublee = value; }
        }

        /// <summary>
        /// 新用户注册验证
        /// </summary>
        public int Regverify
        {
            get { return m_regverify; }
            set { m_regverify = value; }
        }

        /// <summary>
        /// 用户信息保留关键字
        /// </summary>
        public string Censoruser
        {
            get { return m_censoruser; }
            set { m_censoruser = value; }
        }

        /// <summary>
        /// IP 注册间隔限制(小时)
        /// </summary>
        public int Regctrl
        {
            get { return m_regctrl; }
            set { m_regctrl = value; }
        }

        /// <summary>
        /// 特殊 IP 注册限制
        /// </summary>
        public string Ipregctrl
        {
            get { return m_ipregctrl; }
            set { m_ipregctrl = value; }
        }

        /// <summary>
        /// IP禁止访问列表
        /// </summary>
        public string Ipdenyaccess
        {
            get { return m_ipdenyaccess; }
            set { m_ipdenyaccess = value; }
        }

        /// <summary>
        /// IP允许访问列表
        /// </summary>
        public string Ipaccess
        {
            get { return m_ipaccess; }
            set { m_ipaccess = value; }
        }

        /// <summary>
        /// 代理商直销商IP禁止访问列表
        /// </summary>
        public string AdminIpdenyaccess
        {
            get { return adminipdenyaccess; }
            set { adminipdenyaccess = value; }
        }

        /// <summary>
        /// 代理商直销商IP允许访问列表
        /// </summary>
        public string AdminIpaccess
        {
            get { return adminipaccess; }
            set { adminipaccess = value; }
        }

        /// <summary>
        /// smtp服务器
        /// </summary>
        public string Smtp
        {
            get { return smtp; }
            set { smtp = value; }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 系统Email地址
        /// </summary>
        public string Sysemail
        {
            get { return sysemail; }
            set { sysemail = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 发送欢迎短消息
        /// </summary>
        public int Welcomemsg
        {
            get { return m_welcomemsg; }
            set { m_welcomemsg = value; }
        }

        /// <summary>
        /// 欢迎短消息内容
        /// </summary>
        public string Msg
        {
            get { return m_msg; }
            set { m_msg = value; }
        }

        public int SendEmail
        {
            get { return m_sendemil; }
            set { m_sendemil = value; }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Emailcontent
        {
            get { return m_emailcontent; }
            set { m_emailcontent = value; }
        }

        /// <summary>
        /// 搜索时间限制(秒)
        /// </summary>
        public int Searchctrl
        {
            get { return m_searchctrl; }
            set { m_searchctrl = value; }
        }

        /// <summary>
        /// 60 秒最大搜索次数
        /// </summary>
        public int Maxspm
        {
            get { return m_maxspm; }
            set { m_maxspm = value; }
        }

        /// <summary>
        /// 最大在线人数
        /// </summary>
        public int Maxonlines
        {
            get { return m_maxonlines; }
            set { m_maxonlines = value; }
        }

        /// <summary>
        /// 禁止访问时间段
        /// </summary>
        public string Visitbanperiods
        {
            get { return m_visitbanperiods; }
            set { m_visitbanperiods = value; }
        }

        /// <summary>
        /// 禁止全文搜索时间段
        /// </summary>
        public string Searchbanperiods
        {
            get { return m_searchbanperiods; }
            set { m_searchbanperiods = value; }
        }

        #endregion
    }
}
