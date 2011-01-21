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

public partial class GE_Comm_Msg : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        Response.AddHeader("P3P", "CP=CAO PSA OUR");
        this.DivInfoBlock("提示:", this.Msg);
        this.Session["Info"] = null;
        this.Session["info"] = null;
    }
    private string Msg
    {
        get
        {
            string msg = this.Session["info"] as string;
            if (msg == null)
                msg = this.Application["info" + WebUser.No] as string;
            if (msg == null)
            {
                msg = this.ToE("InfoLost", "@提示信息丢失。"); // "@没有找到信息，请在在途工作中找到它。";
            }
            return msg;
        }
    }
}
