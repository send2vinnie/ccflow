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

public partial class WF_UC_MyFlowInfoWap : BP.Web.UC.UCBase3
{
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
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "DeleteFlow":
                string fk_flow = this.Request.QueryString["FK_Flow"];
                int workid = int.Parse(this.Request.QueryString["WorkID"]);
                WorkFlow wf = new WorkFlow(new Flow(fk_flow), workid);
                wf.DoDeleteWorkFlowByReal();
                this.Session["info"] = this.ToE("FlowDelOK", "流程删除成功");
                break;
            case "UnSend":
                try
                {
                    WorkFlow mwf = new WorkFlow(this.FK_Flow, this.WorkID);
                    string str = mwf.DoUnSend();
                    this.Session["info"] = str;
                }
                catch (Exception ex)
                {
                    this.Session["info"] = "@执行撤消失败。@失败信息" + ex.Message;
                }
                break;
            default:
                break;
        }


        string s = this.Session["info"] as string;
        this.Session["info"] = null;
        if (s == null)
            s = this.Application["info" + WebUser.No] as string;

        if (s == null)
            s = Glo.SessionMsg;

        if (s != null)
        {
            s = s.Replace("@@", "@");
            s = s.Replace("@", "<BR>@");

            this.AlertMsg_Info(this.ToE("Note", "操作提示"), s);
        }

        string sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "' ORDER BY WorkID ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);


        int colspan = 9;
        this.AddTable("border=1px align=center width='80%'");
        this.AddTR();
        this.AddTDTitle("ID");
        this.AddTDTitle(this.ToE("Flow", "流程"));
        this.AddTDTitle(this.ToE("NodeName", "节点"));
        this.AddTDTitle(this.ToE("Title", "标题"));
        this.AddTDTitle(this.ToE("Starter", "发起"));
        this.AddTDTitle(this.ToE("RDT", "发起日期"));
        this.AddTDTitle(this.ToE("ADT", "接受日期"));
        this.AddTDTitle(this.ToE("SDT", "期限"));
        this.AddTDTitle(this.ToE("Sta", "状态"));
        this.AddTREnd();

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            is1 = this.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            i++;
            this.AddTDIdx(i);
            this.AddTD(dr["FlowName"].ToString());
            this.AddTD(dr["NodeName"].ToString());
            this.AddTD("<a href=\"MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
            this.AddTD(dr["Starter"].ToString());
            this.AddTD(dr["RDT"].ToString());
            this.AddTD(dr["ADT"].ToString());
            this.AddTD(dr["SDT"].ToString());
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                this.AddTD("<font color=red>逾期</font>");
            }
            else
            {
                this.AddTD("正常");
            }
            this.AddTREnd();
        }
        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();

        
    }
}
