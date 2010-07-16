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

public partial class Face_EmpWorks : WebPage
{
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (s == null)
                return this.ViewState["FK_Flow"] as string;
            return s;
        }
        set
        {
            this.ViewState["FK_Flow"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindV2();
    }

    public void BindV2()
    {
        int colspan = 8;
        this.Pub2.AddTable("width='90%'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/EmpWorks.gif' > <b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
        this.Pub2.AddTREnd();

        #region  输出流程类别.
        this.Pub2.AddTR();
        this.Pub2.AddTDBegin("align=right nowarp=0 colspan=" + colspan);

        string sql = "SELECT FK_Flow, FlowName, COUNT(*) Num FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "' GROUP BY FK_Flow, FlowName";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        int num = 0;
        if (this.FK_Flow == null)
        {
            if (dt.Rows.Count > 0)
                this.FK_Flow = dt.Rows[0][0].ToString();
        }

        bool isShowNum = BP.WF.Glo.IsShowFlowNum;
        foreach (DataRow dr in dt.Rows)
        {
            if (isShowNum)
            {
                if (this.FK_Flow != dr["FK_Flow"] as string)
                    this.Pub2.Add("<a href='EmpWorks.aspx?FK_Flow=" + dr["FK_Flow"] + "'>" + dr["FlowName"] + "-" + dr["Num"] + "</a>&nbsp;&nbsp;");
                else
                    this.Pub2.AddB(dr["FlowName"].ToString() + "&nbsp;&nbsp;");
            }
            else
            {
                if (this.FK_Flow != dr["FK_Flow"] as string)
                    this.Pub2.Add("<a href='EmpWorks.aspx?FK_Flow=" + dr["FK_Flow"] + "'>" + dr["FlowName"] + "</a>&nbsp;&nbsp;");
                else
                    this.Pub2.AddB(dr["FlowName"].ToString() + "&nbsp;&nbsp;");
            }
        }
        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();

        #endregion  输出流程类别


        #region  输出流程类别.
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("ID");
        this.Pub2.AddTDTitle(this.ToE("NodeName", "节点"));
        this.Pub2.AddTDTitle(this.ToE("Title", "标题"));
        this.Pub2.AddTDTitle(this.ToE("Starter", "发起人"));
        this.Pub2.AddTDTitle(this.ToE("RDT", "发起日期"));
        this.Pub2.AddTDTitle(this.ToE("ADT", "接受日期"));
        //this.Pub2.AddTDTitle(this.ToE("WorkID", "WorkID"));
        this.Pub2.AddTDTitle(this.ToE("SDT", "期限"));

        this.Pub2.AddTREnd();
        #endregion  输出流程类别

        sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "' ORDER BY WorkID ";
        dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                this.Pub2.AddTRRed(); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            else
            {
                is1 = this.Pub2.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            i++;
            this.Pub2.AddTD(i);

            this.Pub2.AddTD(dr["NodeName"].ToString());

            this.Pub2.AddTD("<a href=\"MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
            this.Pub2.AddTD(dr["Starter"].ToString());
            this.Pub2.AddTD(dr["RDT"].ToString());
            this.Pub2.AddTD(dr["ADT"].ToString());
            this.Pub2.AddTD(dr["SDT"].ToString());
            //this.Pub2.AddTD(dr["WorkID"].ToString());

            this.Pub2.AddTREnd();
        }

        this.Pub2.AddTRSum();
        this.Pub2.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub2.AddTREnd();

        this.Pub2.AddTableEnd();
        return;
    }
}
