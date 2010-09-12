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

public partial class WF_UC_EmpWorksWap : BP.Web.UC.UCBase3
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
        int colspan = 8;
        this.AddTable("width='90%' align=center");
        this.AddTR();
        this.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.AddTREnd();

        this.AddTR();
        if (WebUser.IsWap)
            this.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' > <b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
        else
        this.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/EmpWorks.gif' > <b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
            
        this.AddTREnd();
        

        #region  输出流程类别.
        this.AddTR();
        this.AddTDTitle("ID");
       // this.AddTDTitle(this.ToE("NodeName", "节点"));
        this.AddTDTitle(this.ToE("Title", "标题"));
        this.AddTDTitle(this.ToE("Starter", "发起人"));
        this.AddTDTitle(this.ToE("RDT", "发起日期"));
        this.AddTDTitle(this.ToE("ADT", "接受日期"));
        this.AddTDTitle(this.ToE("SDT", "期限"));

        this.AddTREnd();
        #endregion  输出流程类别

        string sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "' ORDER BY WorkID ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                this.AddTRRed(); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            else
            {
                is1 = this.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            i++;
            this.AddTD(i);

           // this.AddTD(dr["NodeName"].ToString());

            this.AddTD("<a href=\"MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString()+"</a>"+dr["NodeName"].ToString());
            this.AddTD(dr["Starter"].ToString());
            this.AddTD(dr["RDT"].ToString());
            this.AddTD(dr["ADT"].ToString());
            this.AddTD(dr["SDT"].ToString());
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("colspan=" + colspan, "&nbsp;");
        this.AddTREnd();
        this.AddTableEnd();
        return;
    }
}
