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

public partial class WF_UC_EmpWorks : BP.Web.UC.UCBase3
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
    public void BindList()
    {
        DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
        int colspan = 9;
        this.Pub1.AddTable("border=1px align=center width='960px' ");
        this.Pub1.AddCaption("<img src='./Img/Runing.gif' >&nbsp;<b>" + this.ToE("OnTheWayWork", "待办工作") + "</b>");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Flow", "流程"));
        this.Pub1.AddTDTitle(this.ToE("NodeName", "节点"));
        this.Pub1.AddTDTitle(this.ToE("Title", "标题"));
        this.Pub1.AddTDTitle(this.ToE("Starter", "发起"));
        this.Pub1.AddTDTitle(this.ToE("RDT", "发起日期"));
        this.Pub1.AddTDTitle(this.ToE("ADT", "接受日期"));
        this.Pub1.AddTDTitle(this.ToE("SDT", "期限"));
        this.Pub1.AddTDTitle(this.ToE("Sta", "状态"));

        this.Pub1.AddTREnd();

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
              this.Pub1.AddTRTX(); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            //is1 = this.Pub1.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");

            i++;
            this.Pub1.AddTDIdx(i);
            this.Pub1.AddTD(dr["FlowName"].ToString());
            this.Pub1.AddTD(dr["NodeName"].ToString());
            this.Pub1.AddTD("<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
            this.Pub1.AddTD(dr["Starter"].ToString());
            this.Pub1.AddTD(dr["RDT"].ToString());
            this.Pub1.AddTD(dr["ADT"].ToString());
            this.Pub1.AddTD(dr["SDT"].ToString());
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                this.Pub1.AddTDCenter("<font color=red>逾期</font>");
            }
            else
            {
                this.Pub1.AddTDCenter("正常");
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        
        return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindList();
        return;  // 以下是备份,不在输出以流程类别了。


        //int colspan = 8;
        //#region  输出流程类别.
        //string sql = "SELECT FK_Flow, FlowName, COUNT(*) AS Num FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "' GROUP BY FK_Flow, FlowName";
        //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        //if (dt.Rows.Count == 0)
        //{
        //    this.Pub1.AddFieldSet("<b>" + this.ToE("Note", "提示") + ":</b> " + this.ToE("EW1", "当前您没有工作要去处理"));

        //    this.Pub1.AddUL();
        //    this.Pub1.AddLi("Start" + this.PageSmall + ".aspx", this.ToE("StartWork", "发起工作"));
        //    this.Pub1.AddLi("Runing" + this.PageSmall + ".aspx", this.ToE("OnTheWayWork", "在途工作"));
        //    this.Pub1.AddLi("FlowSearch" + this.PageSmall + ".aspx", this.ToE("FlowSearch", "工作查询"));
        //    this.Pub1.AddULEnd();
        //    this.Pub1.AddFieldSetEnd();
        //    //this.Pub1.DivInfoBlockBegin();
        //    //this.Pub1.Add("<b>" + this.ToE("Note", "提示") + ":</b> " + this.ToE("EW1", "当前您没有工作要去处理"));
        //    //this.Pub1.AddHR();
        //    //this.Pub1.AddUL();
        //    //this.Pub1.AddLi("Start"+this.PageSmall+".aspx", this.ToE("StartWork", "发起工作"));
        //    //this.Pub1.AddLi("Runing" + this.PageSmall + ".aspx", this.ToE("OnTheWayWork", "在途工作"));
        //    //this.Pub1.AddLi("FlowSearch" + this.PageSmall + ".aspx", this.ToE("FlowSearch", "工作查询"));
        //    //this.Pub1.AddULEnd();
        //    //this.Pub1.DivInfoBlockEnd();
        //    return;
        //}


        //this.Left.DivInfoBlockBegin();
        //this.Left.AddUL();
        //int num = 0;
        //if (this.FK_Flow == null)
        //{
        //    if (dt.Rows.Count > 0)
        //        this.FK_Flow = dt.Rows[0][0].ToString();
        //}
        //bool isShowNum = BP.WF.Glo.IsShowFlowNum;
        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (this.FK_Flow != dr["FK_Flow"] as string)
        //        this.Left.AddLi("EmpWorks" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"].ToString(), dr["FlowName"].ToString() + "-" + dr["Num"].ToString());
        //    else
        //        this.Left.AddLiB("EmpWorks" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"].ToString(), dr["FlowName"].ToString() + "-" + dr["Num"].ToString());
        //}
        //this.Left.AddULEnd();
        //this.Left.DivInfoBlockEnd();

        //#endregion  输出流程类别


        //#region  输出流程类别.
        ////this.Pub1.AddTable("width='100%'");
        ////this.Pub1.AddTR();
        ////this.Pub1.Add("<TD class=ToolBar colspan=" + colspan + " > " + this.ToE("OnTheWayWork", "待办工作") + "</TD>");
        ////this.Pub1.AddTREnd();
        ////this.Pub1.AddTableEnd();
        ////this.Pub1.AddTR();
        ////if (WebUser.IsWap)
        ////    this.Pub1.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>-<img src='./Img/EmpWorks.gif' > <b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
        ////else
        ////    this.Pub1.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/EmpWorks.gif' > <b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
        ////this.Pub1.AddTREnd();

        //this.Pub1.AddTable("width='100%'");
        //this.Pub1.AddTR();
        //    this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Runing.gif' >&nbsp;<b>" + this.ToE("OnTheWayWork", "待办工作") + "</b></TD>");
        //this.Pub1.AddTREnd();

        //this.Pub1.AddTR();
        //this.Pub1.AddTDTitle("ID");
        //this.Pub1.AddTDTitle(this.ToE("NodeName", "节点"));
        //this.Pub1.AddTDTitle(this.ToE("Title", "标题"));
        //this.Pub1.AddTDTitle(this.ToE("Starter", "发起"));
        //this.Pub1.AddTDTitle(this.ToE("RDT", "发起日期"));
        //this.Pub1.AddTDTitle(this.ToE("ADT", "接受日期"));
        //this.Pub1.AddTDTitle(this.ToE("SDT", "期限"));
        //this.Pub1.AddTREnd();
        //#endregion  输出流程类别

        //dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable(this.FK_Flow); 
        //int i = 0;
        //bool is1 = false;
        //DateTime cdt = DateTime.Now;
        //foreach (DataRow dr in dt.Rows)
        //{
        //    string sdt = dr["SDT"] as string;
        //    DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
        //    if (cdt >= mysdt)
        //    {
        //        this.Pub1.AddTRRed(); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
        //    }
        //    else
        //    {
        //        is1 = this.Pub1.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
        //    }
        //    i++;
        //    this.Pub1.AddTDIdx(i);

        //    this.Pub1.AddTD(dr["NodeName"].ToString());

        //    this.Pub1.AddTD("<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&FID="+dr["FID"]+"&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
        //    this.Pub1.AddTD(dr["Starter"].ToString());
        //    this.Pub1.AddTD(dr["RDT"].ToString());
        //    this.Pub1.AddTD(dr["ADT"].ToString());
        //    this.Pub1.AddTD(dr["SDT"].ToString());
        //    this.Pub1.AddTREnd();
        //}

        //this.Pub1.AddTRSum();
        //this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        //this.Pub1.AddTREnd();
        //this.Pub1.AddTableEnd();
        //return;
    }
}
