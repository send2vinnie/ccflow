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
using FtpSupport;


public partial class GE_ImageLink_OpenLink1 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImgLink1 en = new ImgLink1();
            en.No = this.RefNo;
            en.Retrieve();

            // 访问次数加1  
            en.ReadTimes += 1;
            en.Update();

            //Http链接
            if (en.HisUrlType == UrlType.Http)
            {
                this.Response.Redirect(en.Url, true);
                this.WinClose();
                return;
            }

            //Ftp链接
            if (en.HisUrlType == UrlType.Ftp)
            {
                PanelFTP.Visible = true;
                this.FtpMain1.FtpUrl = en.Url;
            }
        }
        catch(Exception ex)
        {
            this.Alert("信息初始化失败，详细信息：" + ex.Message );
        }
    }
}
