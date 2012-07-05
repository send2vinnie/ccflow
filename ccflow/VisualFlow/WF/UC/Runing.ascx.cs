﻿using System;
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
    public string GroupBy
    {
        get
        {
            string s = this.Request.QueryString["GroupBy"];
            if (s == null)
                s = "FlowName";
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = BP.WF.Dev2Interface.DB_GenerRuningOfDataTable();
        this.Page.Title = this.ToE("OnTheWayWork", "在途工作");
        if (WebUser.IsWap)
        {
            this.BindWap();
            return;
        }

        int colspan = 6;
        if (this.PageSmall != "")
            this.Pub1.AddBR();

        this.Pub1.AddTable("border=1px align=center width='960px'");

        if (WebUser.IsWap)
            this.Pub1.AddCaption("<img src='./Img/Home.gif' >&nbsp;<a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' >" + this.ToE("OnTheWayWork", "在途工作") );
        else
            this.Pub1.AddCaptionLeft("<img src='./Img/Runing.gif' >&nbsp;<b>" + this.ToE("OnTheWayWork", "在途工作") + "</b>");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Title", "标题"));


        if (this.GroupBy != "FlowName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=FlowName' >" + this.ToE("Flow", "流程") + "</a>");

        if (this.GroupBy != "NodeName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=NodeName' >" + this.ToE("NodeName", "当前节点") + "</a>");

        if (this.GroupBy != "RecName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=RecName' >" + this.ToE("RecName", "发起人") + "</a>");

        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.Pub1.AddTREnd();

        string groupVals = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (groupVals.Contains("@" + dr[this.GroupBy]))
                continue;
            groupVals += "@" + dr[this.GroupBy];
        }


        int i = 0;
        bool is1 = false;
        string title = null;
        string workid = null;
        string fk_flow = null;
         int gIdx = 0;
         string[] gVals = groupVals.Split('@');
         foreach (string g in gVals)
         {
             if (string.IsNullOrEmpty(g))
                 continue;

             gIdx++;
             this.Pub1.AddTR();
             this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='./Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<b>" + g + "</b>");
             this.Pub1.AddTREnd();

             foreach (DataRow dr in dt.Rows)
             {
                 if (dr[this.GroupBy].ToString() != g)
                     continue;
                 i++;
                 this.Pub1.AddTR("ID='" + gIdx + "_" + i + "'");
                 this.Pub1.AddTDIdx(i);

                 title = dr["Title"].ToString();
                 workid = dr["WorkID"].ToString();
                 fk_flow = dr["FK_Flow"].ToString();

                 this.Pub1.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + workid + "&FK_Flow=" + fk_flow + "&FID=" + dr["FID"] + "')\" >" + title + "</a>");
              
                 //  this.Pub1.AddTDDoc(title, 50, title);

                 if (this.GroupBy != "FlowName")
                     this.Pub1.AddTD(dr["FlowName"].ToString());

                 if (this.GroupBy != "NodeName")
                     this.Pub1.AddTD(dr["NodeName"].ToString());

                 if (this.GroupBy != "RecName")
                     this.Pub1.AddTD(dr["RecName"].ToString());

                 this.Pub1.AddTD(dr["RDT"].ToString());
                 this.Pub1.AddTDBegin();
                 this.Pub1.Add("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo" + this.PageSmall + ".aspx?DoType=UnSend&FID=" + dr["FID"] + "&WorkID=" + workid + "&FK_Flow=" + fk_flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
                 this.Pub1.Add("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + workid + "&FK_Flow=" + fk_flow + "&FID=" + dr["FID"] + "')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
               //  this.Pub1.Add("<a href=\"javascript:WinOpen('./../WF/Chart.aspx?WorkID=" + workid + "&FK_Flow=" + fk_flow + "&FID=" + dr["FID"] + "')\" ><img src='./Img/Track.gif' border=0 />" + this.ToE("WorkTrack", "工作轨迹") + "</a>");
                 this.Pub1.AddTDEnd();
                 this.Pub1.AddTREnd();
             }
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
        GenerWorkFlows gwfs = new GenerWorkFlows();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = true;
        //this.Add("<Table border=0 width='100%'>");
        this.AddUL();
        foreach (GenerWorkFlow gwf in gwfs)
        {
            i++;
            is1 = this.AddTR(is1);
            this.AddTDBegin("border=0");

            //this.AddUL();
          //  this.AddLi("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title + gwf.NodeName);
            this.AddLi(  gwf.Title + gwf.NodeName);

            this.Add("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&FID=" + gwf.FID + "&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Add("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
        }
        this.AddULEnd();

       
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
        GenerWorkFlows gwfs = new GenerWorkFlows();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlow gwf in gwfs)
        {
            i++;
            is1 = this.AddTR(is1);
            this.AddTD(i);
            this.AddTDA("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.AddTD(gwf.NodeName);
            this.AddTD(gwf.RDT);
            this.AddTD(gwf.RecName);
            this.AddTD("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&FID=" + gwf.FID + "&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
