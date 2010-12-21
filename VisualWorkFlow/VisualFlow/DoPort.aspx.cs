using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.DA;
using BP.En;

public partial class DoPort : System.Web.UI.Page
{
    /// <summary>
    /// DoType
    /// </summary>
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string EnsName
    {
        get
        {
            return this.Request.QueryString["EnsName"];
        }
    }
    public string EnName
    {
        get
        {
            return this.Request.QueryString["EnName"];
        }
    }
    public string PK
    {
        get
        {
            return this.Request.QueryString["PK"];
        }
    }
    public string PassKey
    {
        get
        {
            return this.Request.QueryString["PassKey"];
        }
    }
    public string Lang
    {
        get
        {
            return this.Request.QueryString["Lang"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.PassKey != BP.SystemConfig.AppSettings["PassKey"])
            return;

        if (this.Lang == null || this.Lang == "")
            throw new Exception("语言编号错误。");

        BP.SystemConfig.DoClearCash();
        BP.Port.Emp emp = new BP.Port.Emp("admin");
        BP.Web.WebUser.SignInOfGenerLang(emp, this.Lang);

        string fk_flow = this.Request.QueryString["FK_Flow"];
        switch (this.DoType)
        {
            case "Ens":
                this.Response.Redirect("./Comm/Batch.aspx?EnsName=" + this.EnsName, true);
                break;
            case "En":
                switch (this.EnName)
                {
                    case "BP.WF.Flow":
                        Flow fl = new Flow(this.PK);
                        if (fl.FK_FlowSort == "00ddd")
                            this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowDoc&PK=" + this.PK, true);
                        else
                            this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + this.PK, true);
                        break;
                    case "BP.WF.Node":
                        Node nd = new Node(this.PK);
                        this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + this.PK, true);
                        break;
                    default:
                        this.Response.Redirect("./Comm/UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PK, true);
                        break;
                }
                break;
            case "StaDef":
                this.Response.Redirect("./Comm/UIEn1ToM.aspx?EnName=BP.WF.Ext.NodeO&AttrKey=BP.WF.NodeStations&PK=" + this.PK + "&NodeID=" + this.PK + "&RunModel=0&FLRole=0&FJOpen=0&r=" + this.PK, true);
                break;
            case "WFRpt":
                this.Response.Redirect("./WF/MapDef/WFRpt.aspx?PK=" + this.PK, true);
                break;
            case "MapDef":
                this.Response.Redirect("./WF/MapDef/MapDef.aspx?PK=" + this.PK, true);
                break;
            case "Dir":
                this.Response.Redirect("./WF/Admin/Cond.aspx?CondType=" + this.Request.QueryString["CondType"] + "&FK_Flow=" + this.Request.QueryString["FK_Flow"] + "&FK_MainNode=" + this.Request.QueryString["FK_MainNode"] + "&FK_Node=" + this.Request.QueryString["FK_Node"] + "&FK_Attr=" + this.Request.QueryString["FK_Attr"] + "&DirType=" + this.Request.QueryString["DirType"] + "&ToNodeID=" + this.Request.QueryString["ToNodeID"], true);
                break;
            case "RunFlow":
                //BP.WF.Flow fl = new BP.WF.Flow(fk_flow);
                //if (fl.HisFlowSheetType== BP.WF.FlowSheetType.DocFlow)
                //   this.Response.Redirect("./WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow+ "&Lang=" + BP.Web.WebUser.SysLang, true);
                // else
                this.Response.Redirect("./WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow + "&Lang=" + BP.Web.WebUser.SysLang, true);
                break;
            case "FlowCheck":
                this.Response.Redirect("./WF/Admin/DoType.aspx?RefNo=" + this.Request.QueryString["RefNo"] + "&DoType=" + this.DoType, true);
                break;
            default:
                break;
        }
    }
}
