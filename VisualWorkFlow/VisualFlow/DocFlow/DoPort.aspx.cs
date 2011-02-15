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

public partial class GovDoc_DoPort : System.Web.UI.Page
{
    #region 属性
    public string UserNo
    {
        get
        {
            return this.Request.QueryString["UserNo"];
        }
    }
    public string SID
    {
        get
        {
            return this.Request.QueryString["SID"];
        }
    }
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
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
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 处理用户权限
        if (this.UserNo != null && this.UserNo != BP.Web.WebUser.No)
        {
            if (1 == 2)
            {
                this.Pub1.AddMsgOfWarning("错误：", "非法的访问，请与管理员联系。");
                return;
            }
            else
            {
                Emp em = new Emp(this.UserNo);
                WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
                WebUser.Token = this.Session.SessionID;
            }
        }
        #endregion

        string str = "";
        switch (this.DoType)
        {
            case "History":
                this.Response.Redirect("History.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "View":
                this.Response.Redirect("FlowSearch.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "Runing":
                this.Response.Redirect("Runing.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "Rpt":
                this.Response.Redirect("../WF/WFRpt.aspx?DoType=Return&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "Start":
                this.Response.Redirect("Start.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "Send":
                this.Response.Redirect("MyFlow.aspx?DoType=Sheet&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            case "Return":
                this.Response.Redirect("../WF/ReturnWork.aspx?DoType=Return&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&IsClient=1", true);
                break;
            case "FW":
                this.Response.Redirect("../WF/Forward.aspx?DoType=Return&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&IsClient=1", true);
                break;
            case "EmpWorks":
                this.Response.Redirect("EmpWorks.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&IsClient=1", true);
                break;
            case "DelFlow": //执行删除流程。
                str = "../DocFlow/DoClient.aspx?DoType=DelFlow&WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow;
                this.ToUrl(str);
                break;
            case "UnSend": //撤消发送。
                //this.Response.Redirect("../WF/Forward.aspx?DoType=Return&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                str = "../DocFlow/Do.aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow;
                this.ToUrl(str);
                //this.Response.Redirect("../WF/Forward.aspx?DoType=Return&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
                break;
            default:
                throw new Exception("未约定命令。" + this.DoType);
        }
    }
    public void ToUrl(string url)
    {
        this.Response.Redirect(url, true);
        return;
    }
}
