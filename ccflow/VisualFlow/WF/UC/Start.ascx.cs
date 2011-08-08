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

public partial class WF_UC_Start : BP.Web.UC.UCBase3
{
    public void BindWap(Flows fls)
    {
        this.AddFieldSet("<img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/Start.gif' >" + this.ToE("Start", "发起"));
        this.AddUL();
        int i = 0;
        string fk_sort = null;
        foreach (Flow fl in fls)
        {
            if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
                continue;

            i++;
            fk_sort = fl.FK_FlowSort;
            this.AddLi("<a href='MyFlow.aspx?FK_Flow=" + fl.No + "&FK_Node="+int.Parse(fl.No)+"01' >" + fl.Name + "</a>&nbsp;<font style=\"color:#77c;font-size=4px\" >" + fl.FK_FlowSortText + "</font>");
        }
        this.AddULEnd();
        this.AddFieldSetEnd();
        return;

        //this.AddTable("width='60%' align=center");
        //this.AddTR();
        //this.Add("<TD class=TitleMsg colspan=2 align=left><img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/Start.gif' >" + this.ToE("Start", "发起") + "</TD>");
        //this.AddTREnd();

        //this.AddTR();
        //this.AddTDTitle("IDX");
        //this.AddTDTitle(this.ToE("Name", "名称"));
        //this.AddTREnd();

        //int i = 0;
        //bool is1 = false;
        //string fk_sort = null;
        //foreach (Flow fl in fls)
        //{
        //    if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
        //        continue;

        //    i++;
        //    is1 = this.AddTR(is1);
        //    this.AddTDIdx(i);
        //    if (fl.FK_FlowSort == fk_sort)
        //    {
        //    }
        //    else
        //    {
        //        this.AddTDB(fl.FK_FlowSortText);
        //        this.AddTREnd();
        //        fk_sort = fl.FK_FlowSort;
        //        continue;
        //    }
        //    fk_sort = fl.FK_FlowSort;
        //    this.AddTD("<a href='MyFlow.aspx?FK_Flow=" + fl.No + "' >" + fl.Name + "</a>");
        //    this.AddTREnd();
        //}
        //this.AddTRSum();
        //this.AddTD();
        //this.AddTD();
        //this.AddTREnd();
        //this.AddTableEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("StartWork", "工作发起");
        Flows fls = BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfEntities();
        if (WebUser.IsWap)
        {
            BindWap(fls);
            return;
        }

        int colspan = 5;
        this.AddTable("width='960px' align=center");
        this.AddTR();
        this.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Start.gif' > <b>" + this.ToE("Start", "发起") + "</b></TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle(this.ToE("IDX", "序"));
        this.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.AddTDTitle(this.ToE("Name", "名称"));
        this.AddTDTitle(this.ToE("FlowPict", "流程图"));
        this.AddTDTitle(this.ToE("Desc", "描述"));
        this.AddTREnd();

        string pageid = this.Request.RawUrl.ToLower();
        if (pageid.Contains("small"))
        {
            if (pageid.Contains("single"))
                pageid = "SmallSingle";
            else
                pageid = "Small";
        }
        else
            pageid = "";

        int i = 0;
        bool is1 = false;
        string fk_sort = null;
        foreach (Flow fl in fls)
        {
            if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
                continue;

            i++;
            is1 = this.AddTR(is1);
            this.AddTDIdx(i);
            if (fl.FK_FlowSort == fk_sort)
                this.AddTD();
            else
                this.AddTDB(fl.FK_FlowSortText);

            fk_sort = fl.FK_FlowSort;

            if (fl.StartListUrl == "")
                this.AddTD("<a href='MyFlow" + pageid + ".aspx?FK_Flow=" + fl.No + "&FK_Node="+int.Parse(fl.No)+"01' >" + fl.Name + "</a>");
            else
                this.AddTD("<a href=\"javascript:StartListUrl('" + fl.StartListUrl + "?FK_Flow=" + fl.No + "&FK_Node="+int.Parse(fl.No)+"01','" + fl.No + "','" + pageid + "')\" >" + fl.Name + "</a>");

            this.AddTD("<a href=\"javascript:WinOpen('Chart.aspx?FK_Flow=" + fl.No + "&DoType=Chart','sd');\"  >打开</a>");
            this.AddTD(fl.Note);
            this.AddTREnd();
        }
        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
