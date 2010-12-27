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

public partial class WF_UC_Runing : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("OnTheWayWork", "在途工作");
        if (WebUser.IsWap)
        {
            this.BindWap();
            return;
        }

        this.Left.DivInfoBlockBegin();
        this.Left.AddB(this.ToE("Ring1" ,"什么是在途工作?") );
        this.Left.AddHR();
        this.Left.Add(this.ToE("Ring2", "在途工作就是我参与流程中的一个节点的工作，但是这条流程还没有完成，并且我不能处理当前的工作环节。"));
        this.Left.DivInfoBlockEnd();


        int colspan = 7;
        this.Pub1.AddTable("width='100%'");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();

        if (WebUser.IsWap)
            this.Pub1.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/Home.gif' >&nbsp;<a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' >" + this.ToE("OnTheWayWork", "在途工作") + "</TD>");
        else
            this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Runing.gif' >&nbsp;<b>" + this.ToE("OnTheWayWork", "在途工作") + "</b></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("CurrNode", "当前节点"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Emp", "发起人"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Rpt", "报告"));

        this.Pub1.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B  WHERE A.WorkID=B.WorkID   AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1 AND B.IsCurr=0 ";
        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);
         //   this.Pub1.AddTDA("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.Pub1.AddTD( gwf.Title);

            this.Pub1.AddTD(gwf.FK_NodeText);
            this.Pub1.AddTD(gwf.RDT);
            this.Pub1.AddTD(gwf.RecText);
            this.Pub1.AddTD("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Pub1.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();


    }
    public void BindWap()
    {
        this.Clear();

        int colspan = 7;
        this.AddTable("width='80%' align=center");
        this.AddTR();
        this.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.AddTREnd();
        this.AddTR();

        if (WebUser.IsWap)
            this.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' >" + this.ToE("OnTheWayWork", "在途工作") + "</TD>");
        else
            this.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Runing.gif' ><b>" + this.ToE("OnTheWayWork", "在途工作") + "</b></TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.AddTDTitle("nowarp=true", this.ToE("Name", "名称"));
        this.AddTDTitle("nowarp=true", this.ToE("CurrNode", "当前节点"));
        this.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.AddTDTitle("nowarp=true", this.ToE("Emp", "发起人"));
        this.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.AddTDTitle("nowarp=true", this.ToE("Rpt", "报告"));

        this.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B  WHERE A.WorkID=B.WorkID   AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1";
        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.AddTR(is1);
            this.AddTD(i);
            this.AddTDA("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.AddTD(gwf.FK_NodeText);
            this.AddTD(gwf.RDT);
            this.AddTD(gwf.RecText);
            this.AddTD("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
