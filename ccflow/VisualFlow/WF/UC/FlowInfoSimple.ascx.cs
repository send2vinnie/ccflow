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
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_FlowInfoSimple : BP.Web.UC.UCBase3
{
    /// <summary>
    /// 当前的工作ID
    /// </summary>
    public Int64 WorkID
    {
        get
        {
            if (ViewState["WorkID"] == null)
            {
                if (this.Request.QueryString["WorkID"] == null)
                    return 0;
                else
                    return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            else
                return Int64.Parse(ViewState["WorkID"].ToString());
        }
        set
        {
            ViewState["WorkID"] = value;
        }
    }
    /// <summary>
    /// 当前的流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (s == null)
                s = "012";
            return s;
        }
    }
    public string FK_Node
    {
        get
        {
            string s= this.Request.QueryString["FK_Node"];
            if (s == "")
                return null;
            return s;
        }
    }
    public string _PageSamll = null;
    public string PageSmall
    {
        get
        {
            if (_PageSamll == null)
            {
                if (this.PageID.ToLower().Contains("small"))
                    _PageSamll = "Small";
                else
                    _PageSamll = "";
            }
            return _PageSamll;
        }
    }
    public void BindFlowStep1()
    {
        WorkerLists wls = new WorkerLists(this.WorkID, this.FK_Flow);
        Flow fl = new Flow(this.FK_Flow);
        this.Add("<Table border=0 wdith=100% align=left>");
        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar  >" + fl.Name + "</TD>");
        this.AddTREnd();

        //this.BindWarting();

        this.AddInbox();

        Nodes nds = fl.HisNodes;
        this.AddTR();
        this.Add("<TD colspan=1 class=BigDoc align=left >");
        foreach (BP.WF.Node nd in nds)
        {
            bool isHave = false;
            foreach (WorkerList wl in wls)
            {
                if (wl.FK_Node == nd.NodeID)
                {
                    this.Add("<font color=green>Step:" + nd.Step + nd.Name);
                    //this.Add("<BR>执行人:" + wl.FK_EmpText);
                    this.Add("<BR>: <img src='../DataUser/Siganture/" + wl.FK_Emp + ".jpg' border=0 onerror=\"this.src='../DataUser/Siganture/UnName.jpg'\"/>");

                    this.Add("<br>" + wl.RDT + "</font><hr>");
                    //this.Add("<a href='WFRpt.aspx?WorkID=" + wl.WorkID + "&FID=0&FK_Flow=" + this.FK_Flow + "' target=_blank >详细..</a><hr>");
                    isHave = true;
                    break;
                }
            }

            if (isHave)
                continue;
        }
        this.AddTDEnd();
        this.AddTREnd();
        this.AddTableEnd();
    }
    public void AddInbox()
    {
        if (this.FK_Flow == null)
            return;

        string tab = "";
        tab = "ND" + int.Parse(this.FK_Flow) + "Rpt";

        this.AddTR();
        this.Add("<TD colspan=1 class=BigDoc align=left >");
        this.AddUL();

        string sql = "SELECT count(workid) FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "'";

        if (this.DoType == "Warting")
            this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=Warting&FK_Flow=" + this.FK_Flow, "<b>" + this.ToE("PendingWork", "待办工作") + "（" + DBAccess.RunSQLReturnValInt(sql) + "）</b>");
        else
            this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=Warting&FK_Flow=" + this.FK_Flow, this.ToE("PendingWork", "待办工作") + "（" + DBAccess.RunSQLReturnValInt(sql) + "）");

        sql = "SELECT Count(distinct workid) AS Num FROM  WF_GenerWorkerList WHERE FK_Emp='" + WebUser.No + "' AND IsPass=1 AND IsEnable=1 AND FK_Flow='" + this.FK_Flow + "'";

        if (this.DoType == "Runing")
            this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=Runing&FK_Flow=" + this.FK_Flow, "<b>" + this.ToE("OnTheWayWork", "在途工作") + "（" + DBAccess.RunSQLReturnValInt(sql) + "）</b>");
        else
            this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=Runing&FK_Flow=" + this.FK_Flow, this.ToE("OnTheWayWork", "在途工作") + "（" + DBAccess.RunSQLReturnValInt(sql) + "）");


        sql = "SELECT COUNT(OID) AS Num FROM  " + tab + "  WHERE Rec='" + WebUser.No + "' AND WFState=1 ";

        try
        {
            if (this.DoType == "History")
                this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=History&FK_Flow=" + this.FK_Flow, this.ToE("HistoryWork", "历史工作") + "<b>（" + DBAccess.RunSQLReturnValInt(sql) + "）</b>");
            else
                this.AddLi("MyFlow" + PageSmall + ".aspx?FK_Node=" + this.FK_Node + "&DoType=History&FK_Flow=" + this.FK_Flow, this.ToE("HistoryWork", "历史工作") + "（" + DBAccess.RunSQLReturnValInt(sql) + "）");
        }
        catch
        {
            Flow fl = new Flow(this.FK_Flow);
            fl.DoCheck();
        }
        //this.AddLi("EmpWorks.aspx?FK_Flow=" + this.FK_Flow, "已处理（" + DBAccess.RunSQLReturnValInt("SELECT COUNT(OID) AS Num FROM " + tab + " WHERE Rec='" + WebUser.No + "' AND NodeState=1 ") + "）");

        this.AddULEnd();
        this.Add("</TD>");
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar nowarp=true ></TD>");
        this.AddTREnd();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.RawUrl.Contains("WAP"))
            return;

        if (this.Request.RawUrl.Contains("Single") )
        {

            return;
        }

        if (this.WorkID != 0)
        {
            this.BindFlowStep1();
            return;
        }

        Flow fl = new Flow(this.FK_Flow);
        this.Add("<Table border=0 wdith=100% align=left>");
        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar nowarp=true >" + fl.Name + "</TD>");
        this.AddTREnd();

        this.AddInbox();

        this.AddTR();
        this.Add("<TD colspan=1 class=BigDoc align=left >" + fl.NoteHtml + "</TD>");
        this.AddTREnd();

        this.AddTableEnd();
        return;


        this.AddFieldSet(fl.Name);
        Nodes nds = fl.HisNodes;
        foreach (BP.WF.Node nd in nds)
        {
            this.Add("第" + nd.Step + "步:<a href=FlowSearch" + PageSmall + ".aspx?FK_Node=" + nd.NodeID + ">" + nd.Name + "</a>");
            if (nd.HisStationsStr.Length > 20)
                this.Add("<br>岗位:<a href=\"javascript:alert('" + nd.HisStationsStr + "')\">更多....</a><hr/>");
            else
                this.Add("<br>岗位:" + nd.HisStationsStr + "<hr>");
        }
        this.AddFieldSetEnd();

        this.AddFieldSet("相关功能");

        this.AddUL();
        this.AddLi("<a href='FlowSearch" + PageSmall + ".aspx?FK_Flow=" + this.FK_Flow + "' >流程数据查询</a>");
        this.AddLi("<a href='FlowSearch" + PageSmall + ".aspx?FK_Flow=" + this.FK_Flow + "' >流程报表</a>");
        this.AddULEnd();

        this.AddFieldSetEnd();
    }
    public void BindWorkStep222()
    {

        Flow fl = new Flow(this.FK_Flow);
        this.Add("<Table border=0 wdith=100% align=left>");
        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar >" + fl.Name + "</TD>");
        this.AddTREnd();

        Nodes nds = fl.HisNodes;

        this.AddTR();
        this.Add("<TD colspan=1 class=BigDoc >");
        foreach (BP.WF.Node nd in nds)
        {
            this.Add("第:" + nd.Step + "步:" + nd.Name);
            if (nd.HisStationsStr.Length > 20 )
                this.Add("<br>岗位:....<hr tip=" + nd.HisStationsStr + " />");
            else
                this.Add("<br>岗位:" + nd.HisStationsStr + "<hr>");
        }
        this.Add("<a href='FlowSearch" + PageSmall + ".aspx?FK_Flow=" + this.FK_Flow + "' >流程查询</a></TD>");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
