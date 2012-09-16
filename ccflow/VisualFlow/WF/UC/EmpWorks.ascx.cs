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
    public string GroupBy
    {
        get
        {
            string s = this.Request.QueryString["GroupBy"];
            if (s == null)
            {
                if (this.DoType == "CC")
                    s = "Rec";
                else
                    s = "FlowName";
            }
            return s;
        }
    }
    public void BindList()
    {
        if (this.GroupBy == "PRI")
        {
            this.BindList_PRI();
            return;
        }

        DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
        bool isPRI = Glo.IsEnablePRI;
        string groupVals = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (groupVals.Contains("@" + dr[this.GroupBy].ToString() + ","))
                continue;
            groupVals += "@" + dr[this.GroupBy].ToString() + ",";
        }

        int colspan = 9;
        if (this.PageSmall != "")
            this.Pub1.AddBR();
        this.Pub1.AddTable("width='960px' align=center");
        this.Pub1.AddCaptionLeft("<img src='./Img/Runing.gif' >&nbsp;<b>待办工作</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input name='string1' type='text' size=20 onChange='n = 0;' > <input type='button' onclick=\"javascript:alert('ss');findInPage('s');\" value='搜索' /> ");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Title", "标题"));

        if (this.GroupBy != "FlowName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=FlowName' >" + this.ToE("Flow", "流程") + "</a>");

        if (this.GroupBy != "NodeName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=NodeName' >" + this.ToE("NodeName", "节点") + "</a>");

        if (this.GroupBy != "StarterName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=StarterName' >" + this.ToE("Starter", "发起人") + "</a>");

        if (isPRI)
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=PRI' >优先级</a>");

        this.Pub1.AddTDTitle("发起日期");
        this.Pub1.AddTDTitle("接受日期");
        this.Pub1.AddTDTitle("期限");
        this.Pub1.AddTDTitle("状态");
        this.Pub1.AddTREnd();

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        string[] gVals = groupVals.Split('@');
        int gIdx = 0;
        foreach (string g in gVals)
        {
            if (string.IsNullOrEmpty(g))
                continue;

            gIdx++;

            this.Pub1.AddTR();
            if (this.GroupBy == "Rec")
                this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='./Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<b>" + g.Replace(",", "") + "</b>");
            else
                this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='./Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<b>" + g.Replace(",", "") + "</b>");
            this.Pub1.AddTREnd();


            foreach (DataRow dr in dt.Rows)
            {
                if (dr[this.GroupBy].ToString() + "," != g)
                    continue;

                string sdt = dr["SDT"] as string;
                this.Pub1.AddTR("ID='" + gIdx + "_" + i + "'");
                i++;
                this.Pub1.AddTDIdx(i);
                if (Glo.IsWinOpenEmpWorks == true)
                    this.Pub1.AddTD("Class=TTD", "<a href=\"javascript:WinOpenIt('MyFlowSmall.aspx?FK_Flow=" + dr["FK_Flow"] + "&FK_Node=" + dr["FK_Node"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "');\"  >" + dr["Title"].ToString());
                else
                    this.Pub1.AddTD("Class=TTD", "<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&FK_Node=" + dr["FK_Node"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());

                if (this.GroupBy != "FlowName")
                    this.Pub1.AddTD(dr["FlowName"].ToString());

                if (this.GroupBy != "NodeName")
                    this.Pub1.AddTD(dr["NodeName"].ToString());

                if (this.GroupBy != "StarterName")
                    this.Pub1.AddTD(dr["Starter"].ToString() + " " + dr["StarterName"]);

                if (isPRI)
                    this.Pub1.AddTD("<img src='./Img/PRI/" + dr["PRI"].ToString() + ".png' class=ImgPRI />");

                this.Pub1.AddTD(dr["RDT"].ToString());
                this.Pub1.AddTD(dr["ADT"].ToString());
                this.Pub1.AddTD(dr["SDT"].ToString());

                DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
                if (cdt >= mysdt)
                {
                    if (cdt.ToString("yyyy-MM-dd") == mysdt.ToString("yyyy-MM-dd"))
                        this.Pub1.AddTDCenter("正常");
                    else
                        this.Pub1.AddTDCenter("<font color=red>逾期</font>");
                }
                else
                {
                    this.Pub1.AddTDCenter("正常");
                }
                this.Pub1.AddTREnd();
            }
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        return;
    }
    public void BindList_PRI()
    {
        DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
        int colspan = 9;
        if (this.PageSmall != "")
            this.Pub1.AddBR();

        this.Pub1.AddTable("width='960px' align=center");
        this.Pub1.AddCaptionLeft("<img src='./Img/Runing.gif' >&nbsp;<b>待办工作</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input name='string1' type='text' size=20 onChange='n = 0;' > <input type='button' onclick=\"javascript:alert('ss');findInPage('s');\" value='搜索' /> ");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Title", "标题"));

        if (this.GroupBy != "FlowName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=FlowName' >" + this.ToE("Flow", "流程") + "</a>");

        if (this.GroupBy != "NodeName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=NodeName' >" + this.ToE("NodeName", "节点") + "</a>");

        if (this.GroupBy != "StarterName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=StarterName' >" + this.ToE("Starter", "发起人") + "</a>");

        this.Pub1.AddTDTitle("优先级");

        this.Pub1.AddTDTitle(this.ToE("RDT", "发起日期"));

        this.Pub1.AddTDTitle(this.ToE("ADT", "接受日期"));
        this.Pub1.AddTDTitle(this.ToE("SDT", "期限"));
        this.Pub1.AddTDTitle(this.ToE("Sta", "状态"));
        this.Pub1.AddTREnd();

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        int gIdx = 0;
        SysEnums ses = new SysEnums("PRI");
        foreach (SysEnum se in ses)
        {

            gIdx++;
            this.Pub1.AddTR();
            this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='./Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<img src='./Img/PRI/" + se.IntKey + ".png' class=ImgPRI />");
            this.Pub1.AddTREnd();

            string pri = se.IntKey.ToString();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PRI"].ToString() != pri)
                    continue;

                string sdt = dr["SDT"] as string;
                this.Pub1.AddTR("ID='" + gIdx + "_" + i + "'");
                i++;
                this.Pub1.AddTDIdx(i);
                if (Glo.IsWinOpenEmpWorks == true)
                    this.Pub1.AddTD("Class=TTD", "<a href=\"javascript:WinOpenIt('MyFlowSmall.aspx?FK_Flow=" + dr["FK_Flow"] + "&FK_Node=" + dr["FK_Node"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "');\"  >" + dr["Title"].ToString());
                else
                    this.Pub1.AddTD("Class=TTD", "<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&FK_Node=" + dr["FK_Node"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());

                if (this.GroupBy != "FlowName")
                    this.Pub1.AddTD(dr["FlowName"].ToString());

                if (this.GroupBy != "NodeName")
                    this.Pub1.AddTD(dr["NodeName"].ToString());

                if (this.GroupBy != "StarterName")
                    this.Pub1.AddTD(dr["Starter"].ToString() + " " + dr["StarterName"]);



                this.Pub1.AddTD(dr["RDT"].ToString());
                this.Pub1.AddTD(dr["ADT"].ToString());
                this.Pub1.AddTD(dr["SDT"].ToString());

                DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
                if (cdt >= mysdt)
                {
                    if (cdt.ToString("yyyy-MM-dd") == mysdt.ToString("yyyy-MM-dd"))
                        this.Pub1.AddTDCenter("正常");
                    else
                        this.Pub1.AddTDCenter("<font color=red>逾期</font>");
                }
                else
                {
                    this.Pub1.AddTDCenter("正常");
                }
                this.Pub1.AddTREnd();
            }
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
    }
}
