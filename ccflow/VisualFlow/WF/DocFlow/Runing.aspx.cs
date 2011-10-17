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
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

public partial class GovDoc_Runing : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ToE("OnTheWayWork", "在途工作");

        int colspan = 6;
        this.Pub2.AddTable("width='600px'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/Runing.gif'/><b>" + this.ToE("OnTheWayWork", "在途工作") + "</b></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDTitle(this.ToE("IDX", "序"));
        this.Pub2.AddTDTitle(this.ToE("Name", "名称"));
        this.Pub2.AddTDTitle(this.ToE("CurrNode", "当前节点"));
        this.Pub2.AddTDTitle(this.ToE("StartDate", "发起日期"));
        this.Pub2.AddTDTitle(this.ToE("StartDate", "发起人"));
        this.Pub2.AddTDTitle(this.ToE("Oper", "操作"));
        this.Pub2.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerList B, WF_Flow C  WHERE A.WorkID=B.WorkID AND A.FK_Flow=C.No AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1 AND C.FK_FlowSort='00' ";
        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.Pub2.AddTR(is1);
            this.Pub2.AddTD(i);
            this.Pub2.AddTD(gwf.FK_FlowText);
            this.Pub2.AddTD(gwf.FK_NodeText);
            this.Pub2.AddTD(gwf.RDT);
            this.Pub2.AddTD(gwf.RecText);
            // this.Pub2.AddTD("[<a href=\"javascript:WinOpen('MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "','flow');\" ><img src='../images/btn/do.gif' border=0 />" + this.ToE("Do", "执行") + "</a>][<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','Do.aspx?ActionType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>][<a href=\"javascript:WinOpen('WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>]");
            this.Pub2.AddTD("[<a href='DoClient.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&DoType=OpenFlow' ><img src='../images/btn/do.gif' border=0 />" + this.ToE("Do", "执行") + "</a>][<a href=\"javascript:ToDo('" + this.ToE("AYS", "您确认吗？") + "','Do.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "收回") + "</a>][<a href=\"javascript:WinOpen('../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>][<a href=\"javascript:WinOpen('../WF/Chart.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='./Img/Track.gif' border=0 />" + this.ToE("Track", "轨迹") + "</a>]");
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTRSum();
        this.Pub2.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();

    }
}
