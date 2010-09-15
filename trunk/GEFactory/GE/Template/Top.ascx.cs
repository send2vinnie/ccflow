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
using BP.GE;
using BP.Port;
using BP.DA;
using BP.Web;

public partial class GE_Template_Top : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = this.Request.ApplicationPath;

        this.AddTable("width='100%'");
        this.AddTR();
        this.AddTD("您好:" + WebUser.No + "," + WebUser.Name);
        this.AddTD("消息:(0/12) <img src='" + path + "/Images/Btn/Fav.gif' border=0 />我的收藏");
        this.AddTREnd();

        this.AddTR();
        this.AddTD("colspan=2", "<img src='" + path + "/GE/Template/Title.jpg' width='100%' height='200px' align=center />");
        this.AddTREnd();

        this.AddTableEnd();
    }
}
