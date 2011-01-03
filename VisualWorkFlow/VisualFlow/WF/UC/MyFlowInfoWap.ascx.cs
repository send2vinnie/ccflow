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
                    string str = mwf.UnSend(WebUser.No);
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
        if (s != null)
        {
            s = s.Replace("@@", "@");
            s = s.Replace("@", "<BR>@");
        }

        if (WebUser.IsWap)
            this.AddFieldSet("<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>Home</a>-<a href='EmpWorks.aspx'><img src='./Img/Home.gif' border=0/>" + this.ToE("PendingWork", "待办") + "</a>-" + this.ToE("Note", "操作提示"), "" + s + "<br><br>");
        else
            this.AddFieldSet( this.ToE("Note", "操作提示"), s);

        //if (this.FK_Type.ToLower() == "warning")
        //    this.AddMsgOfWarning("提示", s);
        //else
        //    this.AddMsgGreen("提示", s);


        string sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "' ORDER BY WorkID ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
            return;

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;

        string color = "";
        this.AddFieldSet(this.ToE("PendingWork","待办工作") + " (" + dt.Rows.Count + ") 个");
        this.Add("<ul>");
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                color = "red";
            }
            else
            {
                color = "";
            }


            int workid = int.Parse(dr["WorkID"].ToString());
            if (workid == this.WorkID)
                this.AddB("<li><font color='" + color + "' ><a href='MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "' >" + dr["NodeName"] + "," + dr["Title"].ToString() + ", 发起人:" + dr["Starter"].ToString() + "</a></font></li>");
            else
                this.Add("<li><font color='" + color + "' ><a href='MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "' >" + dr["NodeName"] + "," + dr["Title"].ToString() + ", 发起人:" + dr["Starter"].ToString() + "</a></font></li>");
        }

        this.Add("</ul>");

        this.AddFieldSetEnd(); // ; ("此流程待办工作");
    }
}
