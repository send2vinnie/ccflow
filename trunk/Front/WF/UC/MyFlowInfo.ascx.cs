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

public partial class WF_UC_MyFlowInfo : BP.Web.UC.UCBase3
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


        this.Pub1.AddFieldSet("操作提示", s);

        //if (this.FK_Type.ToLower() == "warning")
        //    this.Pub1.AddMsgOfWarning("提示", s);
        //else
        //    this.Pub1.AddMsgGreen("提示", s);


        string sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "' ORDER BY WorkID ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
            return;

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;

        string color = "";
        this.Pub1.AddFieldSet("相关待办工作 ("+dt.Rows.Count+") 个");
        this.Pub1.Add("<ul>");
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
            //this.Pub1.AddLi("sdsdsdsdsdsdsdsds");

            int workid = int.Parse(dr["WorkID"].ToString()); 
            if (workid==this.WorkID)
            this.Pub1.AddB("<li><font color='" + color + "' ><a href='MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "' >"+dr["NodeName"] +","+ dr["Title"].ToString() + ", 发起人:" + dr["Starter"].ToString() + "</a></font></li>");
            else
                this.Pub1.Add("<li><font color='" + color + "' ><a href='MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "' >" + dr["NodeName"] + "," + dr["Title"].ToString() + ", 发起人:" + dr["Starter"].ToString() + "</a></font></li>");

        }

        this.Pub1.Add("</ul>");

        this.Pub1.AddFieldSetEnd(); // ; ("此流程待办工作");
    }
}
