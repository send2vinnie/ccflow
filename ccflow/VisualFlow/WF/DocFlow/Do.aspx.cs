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
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;
using BP.Web;
using BP;

public partial class GovDoc_Do : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
   
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = null;
        switch (this.DoType)
        {
            case "UnSend": // 执行撤消发送。
                try
                {
                    WorkFlow mwf = new WorkFlow(this.FK_Flow, this.WorkID);
                    str = mwf.DoUnSend();
                    this.Session["Msg"] = str;
                    this.Response.Redirect("DoClient.aspx?DoType=UnSend&FK_Flow=" + this.FK_Flow+"&WorkID="+this.WorkID, false);
                }
                catch (Exception ex)
                {
                    this.ToMsgPage("@执行撤消失败。@失败信息" + ex.Message);
                }
                return;
            case "DelCaogao":
                string  ptable = "ND" + int.Parse(this.Request.QueryString["FK_Flow"] + "01");
                string sql = "DELETE " + ptable + " WHERE OID=" + this.RefOID;
                BP.DA.DBAccess.RunSQL(sql);
                this.WinClose();
                break;
            default:
                break;
        }

    }
}
