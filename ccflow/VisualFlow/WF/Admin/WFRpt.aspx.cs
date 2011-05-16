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
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_View : WebPage
{
    /// <summary>
    /// FK_Flow
    /// </summary>
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    /// <summary>
    /// 执行新建
    /// </summary>
    /// <param name="rpt"></param>
    public void DoNew(WFRpt rpt)
    {
        this.Title = this.ToE("DesignRpt", "设计报表"); // "设计报表";

        this.Ucsys1.Clear();

        BP.WF.Flow flow = new BP.WF.Flow(this.FK_Flow);
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX("<a href='WFRpt.aspx?FK_Flow=" + this.FK_Flow + "' >" + flow.Name + "</a> - <img src='../../Images/Btn/New.gif' />"+this.ToE("New","新建")+" - " + BP.WF.Glo.GenerHelp("WFRpt"));

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(this.ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle(this.ToE("Gather", "采集"));
        this.Ucsys1.AddTDTitle(this.ToE("Desc","描述"));
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("ViewEName","英文视图名称"));
        TB tb = new TB();
        tb.ID = "TB_No";
        tb.Text = rpt.No;
        tb.Enabled = false;
        tb.ReadOnly = true;
        tb.ShowType = TBType.TB;

        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD(this.ToE("AutoGener", "系统自动生成"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("ViewLabel", "视图标签"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = rpt.Name;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " "+this.ToE("Save","保存")+" ";
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.Add(btn);
        if (rpt.No.Length > 1)
        {
            btn = new Button();
            btn.ID = "Btn_Del";
            btn.Text = " " + this.ToE("Del", "删除") + " ";
            this.Ucsys1.Add(btn);
            btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";

            btn.Click += new EventHandler(btn_Del_Click);
        }

        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();


        //this.Ucsys1.AddTRSum();
        //this.Ucsys1.AddTDBigDoc("colspan=3 class=BigDoc","提示：关于如何传递参数请打开帮助。");
        //this.Ucsys1.AddTREnd();

        //this.Ucsys1.AddTRSum();
        //this.Ucsys1.AddTD("colspan=3", "在当前节点上要执行的外部程序。比如：在此节点点调用sina.");
        //this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTable();
    }

    void btn_Click(object sender, EventArgs e)
    {
        WFRpt bt = new WFRpt();
        if (this.RefNo !=null)
        {
            bt.No = this.RefNo;
            bt.Retrieve();
        }

        bt = this.Ucsys1.Copy(bt) as WFRpt;
        bt.FK_Flow = this.FK_Flow ;
        if (this.RefNo == null)
        {
            bt.No = "V" + this.FK_Flow;
            int i = 0;
            while (bt.IsExits)
            {
                bt.No = "V" + this.FK_Flow + "_" + i;
            }
            bt.FK_Flow = this.FK_Flow;

            Flow f = new Flow(this.FK_Flow);
            bt.FK_FlowSort = f.FK_FlowSort;
            bt.Insert();
        }
        else
            bt.Update();
        this.Response.Redirect("WFRpt.aspx?FK_Flow=" + this.FK_Flow , true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        WFRpt t = new WFRpt();
        t.No = this.RefNo;
        t.Delete();
        this.Response.Redirect("WFRpt.aspx?FK_Flow=" + this.FK_Flow, true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
            "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


        switch (this.DoType)
        {
            case "New":
                WFRpt bk = new WFRpt();
                bk.FK_Flow = this.FK_Flow;
                this.DoNew(bk);
                return;
            case "Edit":
                WFRpt bk1 = new WFRpt(this.RefNo);
                bk1.FK_Flow = this.FK_Flow;
                this.DoNew(bk1);
                return;
            default:
                break;
        }

        WFRpts rpts = new WFRpts(this.FK_Flow);
        if (rpts.Count == 0)
        {
            this.Response.Redirect("WFRpt.aspx?FK_Flow=" + this.FK_Flow  + "&DoType=New", true);
            return;
        }

        if (rpts.Count ==1)
        {
            this.Response.Redirect("../../Comm/UIEn.aspx?EnName=BP.WF.WFRpt&PK="+rpts[0].GetValByKey("No"), true);
            return;
        }

        BP.WF.Flow flow = new BP.WF.Flow(this.FK_Flow);
        this.Title = this.ToE("DesignRpt", "设计报表"); // "设计报表";
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(this.Title + " - <a href='WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&DoType=New'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a>  - " + BP.WF.Glo.GenerHelp("WFRpt"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("IDX");
        this.Ucsys1.AddTDTitle(this.ToE("Flow", "流程"));
        this.Ucsys1.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.Ucsys1.AddTDTitle(this.ToE("Name","名称") );
        this.Ucsys1.AddTDTitle( );
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();
        int i = 0;
        foreach (WFRpt rpt in rpts)
        {
            i++;
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);
            this.Ucsys1.AddTD(flow.FK_FlowSortText);
            this.Ucsys1.AddTD(rpt.FK_Flow);

            this.Ucsys1.AddTD(rpt.No);
            this.Ucsys1.AddTD(rpt.Name);
            this.Ucsys1.AddTD("colspan=2", "<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.WFRpt&PK=" + rpt.No + "','ss');\"><img src='../../Images/Btn/Edit.gif' border=0/>" + this.ToE("Edit","编辑") + "</a>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }


}
