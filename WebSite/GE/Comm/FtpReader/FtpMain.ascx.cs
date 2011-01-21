using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FtpSupport;
using BP.GE;

public partial class GE_Comm_FtpMain : BP.Web.UC.UCBase3
{
    #region 属性：FTP地址
    private string ftpUrl = "";
    public string FtpUrl
    {
        get
        {
            return this.ftpUrl;
        }
        set
        {
            this.ftpUrl = value;
        }
    }
    #endregion

    //根目录
    public string FtpRootPath
    {
        get
        {
            string root = "";
            try
            {
                root = this.FtpUrl;
                string baseFtpUrl = "ftp://" + GloFTP.FtpIP;
                root = root.ToLower().Replace(baseFtpUrl, "");
                if (root.EndsWith("/"))
                {
                    root = root.Remove(root.LastIndexOf("/"));
                }
            }
            catch
            {
                root = GloFTP.FtpPath;
            }
            return root;
        }
    }
    //当前目录
    public string FtpCurrentPath
    {
        get
        {
            string s = this.Request.QueryString["FtpCurrentPath"];
            if (s == null)
                s = this.FtpRootPath;

            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.FtpDirReader1.FtpRootPath = this.FtpRootPath;
            //标题栏
            this.Pub2.AddTable("width='100%' height='100%'");
            //Title样式
            string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "GroupTitleCSS");
            this.Pub2.AddTR();
            this.Pub2.AddTD("colspan='5' class='" + groupTitle + "'", "资源列表");
            this.Pub2.AddTREnd();

            this.Pub2.AddTR();
            this.Pub2.AddTDBegin("style='height:500px;'");
            this.Pub2.Add(String.Format("<iframe name='fm' id='fm' class='iframe' scrolling=\"auto\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\" width=\"100%\" height='100%' src=\"{0}\"></iframe>",
                   this.Request.ApplicationPath + "/GE/Comm/FtpReader/FtpReader.aspx?FtpRootPath=" + HttpUtility.UrlEncode(this.FtpRootPath)
                   + "&FtpCurrentPath=" + HttpUtility.UrlEncode(this.FtpCurrentPath)));
            this.Pub2.AddTDEnd();
            this.Pub2.AddTREnd();
            this.Pub2.AddTableEnd();
        }
    }
}
