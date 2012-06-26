﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_GetTask : BP.Web.UC.UCBase3
{
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    public int ToNode
    {
        get
        {
            return int.Parse(this.Request.QueryString["ToNode"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public void BindWorkList()
    {
        string pageid = this.Request.RawUrl.ToLower();
        if (pageid.Contains("small"))
        {
            if (pageid.Contains("single"))
                pageid = "SmallSingle";
            else
                pageid = "Small";
            this.AddBR();
        }
        else
        {
            pageid = "";
        }
        int colspan = 10;
        this.AddTable("width='960px' align=center");
        this.AddTR();
        this.Add("<TD  colspan=" + colspan + " align=left><a href='GetTask"+pageid+".aspx'><img src='./Img/Start.gif' > <b>取回处理</b></a></TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle("序");
        this.AddTDTitle("标题");
        this.AddTDTitle("发起人");
        this.AddTDTitle("发起时间");
        this.AddTDTitle("停留节点");
        this.AddTDTitle("当前处理人");
        this.AddTDTitle("到达时间");
        this.AddTDTitle("应完成时间");
        this.AddTDTitle("操作");
        this.AddTREnd();

        // 根据发起人的权限来判断，是否具有操作此人员的权限。
        GetTasks jcs = new GetTasks(this.FK_Flow);
        string canDealNodes = "";
        int idx = 1;
        foreach (GetTask jc in jcs)
        {
            /* 判断我是否可以处理当前点数据？ */
            if (jc.Can_I_Do_It() == false)
                continue;

            canDealNodes += "''";
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_EmpWorks WHERE FK_Node IN ("+jc.CheckNodes+") AND FK_Flow='" + this.FK_Flow + "' AND FK_Dept LIKE '" + BP.Web.WebUser.FK_Dept + "%'");
            if (dt.Rows.Count == 0)
            {
                if (BP.Web.WebUser.FK_Dept.Length >= 4)
                    dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_EmpWorks WHERE FK_Node IN (" + jc.CheckNodes + ") AND FK_Flow='" + this.FK_Flow + "' AND FK_Dept LIKE '" + BP.Web.WebUser.FK_Dept.Substring(0, 2) + "%'");
                else
                    dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_EmpWorks WHERE FK_Node IN (" + jc.CheckNodes + ") AND FK_Flow='" + this.FK_Flow + "' AND FK_Dept LIKE '" + BP.Web.WebUser.FK_Dept + "%'");
            }

            this.AddTR();
            this.Add("<TD  class=Sum colspan=" + colspan + " align=left>"+jc.Name+" ;  =》可跳转审核的节点:"+jc.CheckNodes+"</TD>");
            this.AddTREnd();
            foreach (DataRow dr in dt.Rows)
            {
                this.AddTR();
                this.AddTDIdx(idx++);
                this.AddTD(dr["Title"].ToString());
                this.AddTD(dr["Starter"].ToString());
                this.AddTD(dr["RDT"].ToString());
                this.AddTD(dr["NodeName"].ToString());
                this.AddTD(dr["FK_EmpText"].ToString());
                this.AddTD(dr["ADT"].ToString());
                this.AddTD(dr["SDT"].ToString());
                this.AddTD("<a href=\"javascript:WinOpen('WFRpt.aspx?WorkID=" + dr["WorkID"] + "&FK_Flow=" + this.FK_Flow + "&FID=" + dr["FID"] + "')\">报告</a> - <a href=\"javascript:Tackback('"+this.FK_Flow+"','"+dr["FK_Node"]+"','"+jc.NodeID+"','"+dr["WorkID"]+"')\">取回</a>");
                this.AddTREnd();
            }
        }
        this.AddTableEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.DoType == "Tackback")
        {
            /* */
            try
            {
               string s= BP.WF.Dev2Interface.Node_Tackback(this.FK_Node, this.WorkID, this.ToNode);
               this.AddMsgGreen("取回成功", "工作已经放入您的待办里，<a target='_"+this.WorkID+"' href='MyFlowSmall.aspx?FK_Flow="+this.FK_Flow+"&FK_Node="+this.FK_Node+"&WorkID="+this.WorkID+"'>点这里请执行</a>。");
            }
            catch (Exception ex)
            {
                this.AddMsgOfWarning("错误", ex.Message);
            }
            return;
        }

        if (this.FK_Flow != null)
        {
            this.BindWorkList();
            return;
        }

        Flows fls = new Flows();
        fls.RetrieveAll();

        string pageid = this.Request.RawUrl.ToLower();
        if (pageid.Contains("small"))
        {
            if (pageid.Contains("single"))
                pageid = "SmallSingle";
            else
                pageid = "Small";
            this.AddBR();
        }
        else
        {
            pageid = "";
        }
       

        int colspan = 5;
        this.AddTable("width='960px' align=center");
        this.AddTR();
        this.Add("<TD class=Caption colspan=" + colspan + " align=left><img src='./Img/Start.gif' > <b>取回处理</b></TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDTitle(this.ToE("IDX", "序"));
        this.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.AddTDTitle(this.ToE("Name", "名称"));
        this.AddTDTitle(this.ToE("FlowPict", "流程图"));
        this.AddTDTitle(this.ToE("Desc", "描述"));
        this.AddTREnd();

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

            this.AddTD("<a href='GetTask" + pageid + ".aspx?FK_Flow=" + fl.No + "&FK_Node=" + int.Parse(fl.No) + "01' >" + fl.Name + "</a>");

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
