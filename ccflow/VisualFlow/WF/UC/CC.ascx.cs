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

public partial class WF_UC_CC : BP.Web.UC.UCBase3
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
    public string Sta
    {
        get
        {
            return this.Request["Sta"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.Request.QueryString["Sta"])
        {
            case "0":
                this.Bind(BP.WF.Dev2Interface.DB_CCList_UnRead(WebUser.No));
                break;
            case "1":
                this.Bind(BP.WF.Dev2Interface.DB_CCList_Read(WebUser.No));
                break;
            case "2":
            default:
                this.Bind(BP.WF.Dev2Interface.DB_CCList_Delete(WebUser.No));
                break;
        }
    }
    public string GenerMenu()
    {
        string msg = "<a href='" + this.PageID + ".aspx?Sta=0' >未读</a> - <a href='" + this.PageID + ".aspx?Sta=1' >已读</a> - <a href='" + this.PageID + ".aspx?Sta=2' >删除</a>";
        return msg;
    }
    /// <summary>
    /// 绑定
    /// </summary>
    public void Bind(DataTable dt)
    {
        string groupVals = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (groupVals.Contains("@" + dr[this.GroupBy].ToString() + ","))
                continue;
            groupVals += "@" + dr[this.GroupBy].ToString() + ",";
        }

        if (this.PageSmall != "")
            this.Pub1.AddBR();
        int colspan = 9;
        this.Pub1.AddTable("border=1px align=center width='960px' ");
        this.Pub1.AddCaptionLeft("<img src='./Img/Runing.gif' >&nbsp;" + this.GenerMenu());
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Title", "标题"));
        this.Pub1.AddTDTitle("内容");

        if (this.GroupBy != "FlowName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=FlowName&DoType=CC' >" + this.ToE("Flow", "流程") + "</a>");

        if (this.GroupBy != "NodeName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=NodeName&DoType=CC' >" + this.ToE("NodeName", "节点") + "</a>");

        if (this.GroupBy != "Rec")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=Rec&DoType=CC' >" + this.ToE("Rec", "发起人") + "</a>");

        this.Pub1.AddTDTitle("抄送日期");
        if (this.Sta=="1")
        this.Pub1.AddTDTitle("删除");
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
            this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='./Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<b>" + g.Replace(",", "") + "</b>");
            this.Pub1.AddTREnd();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[this.GroupBy].ToString() + "," != g)
                    continue;

                this.Pub1.AddTR("ID='" + gIdx + "_" + i + "'");
                i++;
                bool isRead = false;
                if (dr["IsRead"].ToString() == "1")
                    isRead = true;

                this.Pub1.AddTDIdx(i);
                if (isRead == false)
                    this.Pub1.AddTDB("<a href=\"javascript:WinOpen('" + dr["MyPK"] + "','" + dr["FK_Flow"] + "','" + dr["FK_Node"] + "','" + dr["RefWorkID"] + "','" + dr["FID"] + "','" + dr["Sta"] + "');\" >" + dr["Title"] + "</a>");
                else
                    this.Pub1.AddTD("<a href=\"javascript:WinOpen('" + dr["MyPK"] + ",'" + dr["FK_Flow"] + "','" + dr["FK_Node"] + "','" + dr["RefWorkID"] + "','" + dr["FID"] + "','" + dr["Sta"] + "');\" >" + dr["Title"] + "</a>");

                this.Pub1.AddTD( DataType.ParseText2Html( dr["Doc"].ToString() ));

                if (this.GroupBy != "FlowName")
                {
                    if (isRead == false)
                        this.Pub1.AddTDB(dr["FlowName"].ToString());
                    else
                        this.Pub1.AddTD(dr["FlowName"].ToString());
                }

                if (this.GroupBy != "NodeName")
                {
                    if (isRead == false)
                        this.Pub1.AddTDB(dr["NodeName"].ToString());
                    else
                        this.Pub1.AddTD(dr["NodeName"].ToString());
                }

                if (this.GroupBy != "Rec")
                    this.Pub1.AddTD(dr["Rec"].ToString());

                this.Pub1.AddTD(dr["RDT"].ToString());
                if (this.Sta == "1")
                    this.Pub1.AddTD("<a href=\"javascript:DoDelCC('" + dr["MyPK"] + "');\"><img src='" + this.Request.ApplicationPath + "/Images/Btn/Delete.gif' /></a>");
                this.Pub1.AddTREnd();
            }
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
}
