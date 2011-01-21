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
using BP.Web;
using BP.En;
using BP.DA;
using BP.GE;


public partial class GE_FDB_Do : WebPage
{
    //文件目录
    public string FileDir
    {
        get
        {
            string fileDir = this.Request.QueryString["FileDir"];
            //return this.Server.UrlDecode(fileDir);
            return HttpUtility.UrlDecode(fileDir);

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "DownLoad":
                GloFTP.DownLoadFile(this.FileDir);
                this.WinClose();
                break;
            default:
                this.WinClose();
                break;
        }

    }
}
