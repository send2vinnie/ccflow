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
using BP.Port;
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
    public void BindFlowStep1()
    {
        WorkerLists wls = new WorkerLists(this.WorkID, this.FK_Flow);
        Flow fl = new Flow(this.FK_Flow);
        this.Add("<Table border=0 wdith=100% align=left>");
        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar  >" + fl.Name + "</TD>");
        this.AddTREnd();

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
                    this.Add("<font color=green>第" + nd.Step + "步:<a href=FlowSearch.aspx?FK_Node=" + nd.NodeID + ">" + nd.Name + "</a>");
                    this.Add("<BR>执行人:" + wl.FK_EmpText);
                    this.Add("<br>接受日期:" + wl.RDT + "</font> ");
                    this.Add("<a href='WFRpt.aspx?WorkID=" + wl.WorkID + "&FID=0&FK_Flow=" + this.FK_Flow + "' target=_blank >详细..</a><hr>");
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
    public void BindFlowStep_bak1()
    {
        WorkerLists wls = new WorkerLists(this.WorkID, this.FK_Flow);
        Flow fl = new Flow(this.FK_Flow);
        this.Add("<Table border=0 wdith=100% align=left>");
        this.AddTR();
        this.Add("<TD colspan=1 class=ToolBar  >" + fl.Name + "</TD>");
        this.AddTREnd();

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
                    this.Add("<font color=green>第" + nd.Step + "步:<a href=FlowSearch.aspx?FK_Node=" + nd.NodeID + ">" + nd.Name + "</a>");
                    this.Add("<BR>执行人:" + wl.FK_EmpText);
                    this.Add("<br>接受日期:" + wl.RDT + "</font> ");
                    this.Add("<a href='WFRpt.aspx?WorkID=" + wl.WorkID + "&FID=0&FK_Flow=" + this.FK_Flow + "' target=_blank >详细..</a><hr>");
                    isHave = true;
                    break;
                }
            }

            if (isHave)
                continue;

            this.Add("第" + nd.Step + "步:<a href=FlowSearch.aspx?FK_Node=" + nd.NodeID + ">" + nd.Name + "</a>");
            //  this.Add("<br>岗位:" + nd.HisStationsStr + "<hr>");
            this.Add("<hr>");
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
        if (this.FK_Node == null)
            tab = "ND" + int.Parse(this.FK_Flow) + "01";
        else
            tab = "ND" + this.FK_Node;


        this.AddTR();
        this.Add("<TD colspan=1 class=BigDoc align=left >");
        this.AddUL();

        int nodeid = 0;


        string sql = "SELECT count(workid) FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "'";
        this.AddLi("MyFlow.aspx?FK_Node=" + this.FK_Node + "&DoType=Warting&FK_Flow=" + this.FK_Flow, "待办工作（" + DBAccess.RunSQLReturnValInt(sql) + "）");

        sql = "SELECT COUNT(WorkID) AS Num FROM  WF_GenerWorkerList WHERE FK_Emp='" + WebUser.No + "' AND IsCurr=0 AND IsEnable=1 AND FK_Flow='" + this.FK_Flow + "'";
        this.AddLi("MyFlow.aspx?FK_Node=" + this.FK_Node + "&DoType=Runing&FK_Flow=" + this.FK_Flow,  "在途工作（" + DBAccess.RunSQLReturnValInt(sql) + "）");


        sql = "SELECT COUNT(OID) AS Num FROM  " + tab + "  WHERE Rec='" + WebUser.No + "' AND WFState=1 ";
        this.AddLi("MyFlow.aspx?FK_Node=" + this.FK_Node + "&DoType=History&FK_Flow=" + this.FK_Flow,  "历史工作（" + DBAccess.RunSQLReturnValInt(sql) + "）");

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

        //#region 菜单
        //this.Add("<Table border=0 wdith=100% align=left>");
        //this.AddTR();
        //this.Add("<TD colspan=1 class=ToolBar nowarp=true >我的工作</TD>");
        //this.AddTREnd();

        //this.AddTR();
        //this.Add("<TD colspan=1 class=BigDoc align=left >");
        //this.AddUL();
        //this.AddLi("待处理（2）");
        //this.AddLi("已处理（2）");
        //this.AddULEnd();
        //this.Add("</TD>");
        //this.AddTREnd();
      

        //this.AddTableEnd();
        //#endregion 菜单



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
            this.Add("第" + nd.Step + "步:<a href=FlowSearch.aspx?FK_Node=" + nd.NodeID + ">" + nd.Name + "</a>");
            if (nd.HisStationsStr.Length > 20)
                this.Add("<br>岗位:<a href=\"javascript:alert('" + nd.HisStationsStr + "')\">更多....</a><hr/>");
            else
                this.Add("<br>岗位:" + nd.HisStationsStr + "<hr>");
        }
        this.AddFieldSetEnd();

        this.AddFieldSet("相关功能");

        this.AddUL();
        this.AddLi("<a href='FlowSearch.aspx?FK_Flow=" + this.FK_Flow + "' >流程数据查询</a>");
        this.AddLi("<a href='FlowSearch.aspx?FK_Flow=" + this.FK_Flow + "' >流程报表</a>");
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
        this.Add("<a href='FlowSearch.aspx?FK_Flow=" + this.FK_Flow + "' >流程查询</a></TD>");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
