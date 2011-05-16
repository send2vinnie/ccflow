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

public partial class WF_UC_Runing : BP.Web.UC.UCBase3
{
    public string _PageSamll = null;
    public string PageSmall
    {
        get
        {
            if (_PageSamll == null)
            {
                if (this.PageID.ToLower().Contains("smallsingle"))
                    _PageSamll = "SmallSingle";
                else if (this.PageID.ToLower().Contains("small"))
                    _PageSamll = "Small";
                else
                    _PageSamll = "";
            }
            return _PageSamll;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GenerWorkFlowExts gwfs = BP.WF.Dev2Interface.DB_GenerRuningOfEntities();

        if (gwfs.Count == 0)
        {
            this.AddFieldSet("提示");
            this.Add("没有在途工作");
            this.AddFieldSetEnd();
            return;
        }

        this.Page.Title = this.ToE("OnTheWayWork", "在途工作");
        if (WebUser.IsWap)
        {
            this.BindWap();
            return;
        }

        int colspan = 7;
        this.Pub1.AddTable("border=1px");
        //this.Pub1.AddTR();
        //this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        //this.Pub1.AddTREnd();
        this.Pub1.AddTR();

        if (WebUser.IsWap)
            this.Pub1.AddCaption("<img src='./Img/Home.gif' >&nbsp;<a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' >" + this.ToE("OnTheWayWork", "在途工作") );
        else
            this.Pub1.AddCaption("<img src='./Img/Runing.gif' >&nbsp;<b>" + this.ToE("OnTheWayWork", "在途工作") + "</b>");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Title", "标题"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("CurrNode", "当前节点"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Emp", "发起人"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.Pub1.AddTREnd();

        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);
            //   this.Pub1.AddTDA("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.Pub1.AddTD(gwf.Title);
            this.Pub1.AddTD(gwf.FK_NodeText);
            this.Pub1.AddTD(gwf.RDT);
            this.Pub1.AddTD(gwf.RecText);
            this.Pub1.AddTDBegin();
            this.Pub1.Add("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo" + this.PageSmall + ".aspx?DoType=UnSend&FID=" + gwf.FID + "&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Pub1.Add("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.Pub1.Add("<a href=\"javascript:WinOpen('./../WF/Chart.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='./Img/Track.gif' border=0 />" + this.ToE("WorkTrack", "工作轨迹") + "</a>");
            this.Pub1.AddTDEnd();
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

        this.AddFieldSet("<img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' >" + this.ToE("OnTheWayWork", "在途工作"));

        string sql = " SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B  WHERE A.WorkID=B.WorkID   AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1";
        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = true;
        //this.Add("<Table border=0 width='100%'>");
        this.AddUL();
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.AddTR(is1);
            this.AddTDBegin("border=0");

            //this.AddUL();
            this.AddLi("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title + gwf.FK_NodeText);
            this.Add("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&FID="+gwf.FID+"&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Add("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
        }
        this.AddULEnd();

        // this.AddTableEnd();
        //this.AddUL();
        //foreach (GenerWorkFlowExt gwf in gwfs)
        //{
        //    i++;
        //    is1 = this.AddTR(is1);
        //    this.AddLi("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);

        //    this.Add(gwf.FK_NodeText);
        //    //this.Add(gwf.RDT);
        //    //this.Add(gwf.RecText);
        //    this.AddBR("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
        //    this.Add("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
        //    //this.AddTREnd();
        //}
        //this.AddULEnd();
        this.AddFieldSetEnd();
    }
    public void BindWap_bal()
    {
        this.Clear();

        int colspan = 7;
        this.AddTable("width='100%' align=center");
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
            this.AddTD("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&FID=" + gwf.FID + "&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
