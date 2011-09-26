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
using BP.En;
using BP.Port;
using BP.Web;

public partial class WF_SelfRpt : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeft(this.ToE("SelfRpt", "自定义报表") + " - <a href=\"javascript:WinOpen('../Comm/PanelEns.aspx?EnsName=BP.WF.CHOfFlows')\" >" + this.ToE("FlowSearch", "流程查询") + "</a>");

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("IDX");
        this.Ucsys1.AddTDTitle(this.ToE("Sort", "类别"));
        this.Ucsys1.AddTDTitle(this.ToE("FlowName", "流程名称"));
        this.Ucsys1.AddTDTitle(this.ToE("RptSort", "报表类别"));
        this.Ucsys1.AddTDTitle("colspan=4", ""); //选择报表类型
        this.Ucsys1.AddTREnd();

        WFRpts rpts = new WFRpts();
        QueryObject qo = new QueryObject(rpts);
        qo.addOrderBy(WFRptAttr.FK_FlowSort, WFRptAttr.FK_Flow);
        qo.DoQuery();

        int i = 0;
        string fk_flow = "";
        string fk_flowSort = "";
        foreach (WFRpt rpt in rpts)
        {
            i++;

            if (this.FK_Flow == rpt.FK_Flow)
                this.Ucsys1.AddTRSum();
            else
                this.Ucsys1.AddTR();


            this.Ucsys1.AddTDIdx(i);
            if (fk_flowSort == rpt.FK_FlowSort)
                this.Ucsys1.AddTD("");
            else
                this.Ucsys1.AddTD(rpt.FK_FlowSortT);

            if (fk_flow == rpt.FK_Flow)
                this.Ucsys1.AddTD("");
            else
                this.Ucsys1.AddTD(rpt.FK_FlowT);
            this.Ucsys1.AddTD(rpt.Name);

            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('../Comm/PanelEns.aspx?EnsName=" + rpt.No + "','s" + rpt.No + "');\" ><img src='../Images/Btn/Table.gif' border=0/>" + this.ToE("Search", "查询") + "</a>");
            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('../Comm/GroupEnsMNum.aspx?EnsName=" + rpt.No + "','b" + rpt.No + "');\" ><img src='../Images/Btn/DataGroup.gif' border=0/>" + this.ToE("GroupFX", "分组分析") + "</a>");
            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('../Comm/Contrast.aspx?EnsName=" + rpt.No + "','c" + rpt.No + "');\" ><img src='../Images/Btn/Table.gif' border=0/>" + this.ToE("DBFX", "对比分析") + "</a>");
            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('../Comm/PanelEns.aspx?EnsName=" + rpt.No + "','d" + rpt.No + "');\" ><img src='../Images/Btn/Table.gif' border=0/>" + this.ToE("MRpt", "多纬报表") + "</a>");
            this.Ucsys1.AddTREnd();

            fk_flowSort = rpt.FK_FlowSort;
            fk_flow = rpt.FK_Flow;
        }

        this.Ucsys1.AddTRSum();
        this.Ucsys1.AddTD("colspan=8", ""); // 以上是流程设计人员为您定义的报表，如果您还有其它的需要，可以告知他为您定义。
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTableEnd();
    }
}
