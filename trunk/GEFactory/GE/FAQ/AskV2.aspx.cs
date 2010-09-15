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
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using System.Collections.Generic;

public partial class FAQ_AskV2 : BP.Web.WebPage
{
    public string KMType
    {
        get
        {
            string s = this.Request.QueryString["KMType"];
            if (s == null)
                //s = EduUser.FK_KM;
                s = null;
            return s;
        }
    }
    public string ShowType
    {
        get
        {
            string s = this.Request.QueryString["ShowType"];
            if (s == null)
                s = "all";
            return s;
        }
    }
    public string SortType
    {
        get
        {
            string s = this.Request.QueryString["SortType"];
            if (s == null)
                s = "All";
            return s;
        }
    }
    public void BindTop()
    {


        this.Pub3.Add("<p style='margin-right:10px'>&nbsp;");
        BP.Port.LocalXmls xmls = new LocalXmls();
        xmls.RetrieveAll();

        foreach (LocalXml xml in xmls)
        {
            if (this.SortType == xml.No)
            {
                this.Pub3.Add("<span class=\"font_b\">");
                this.Pub3.Add("&nbsp" + xml.Name + "&nbsp");
                this.Pub3.Add("</span>");
            }
            else
            {
                //this.Pub3.Add("<li>");
                if (this.KMType == null)
                    this.Pub3.Add("<a href='AskV2.aspx?SortType=" + xml.No + "&ShowType=" + this.ShowType + "' class=\"link_blue_03\">&nbsp" + xml.Name + "</a>&nbsp");
                else
                    this.Pub3.Add("<a href='AskV2.aspx?KMType=" + KMType + "&SortType=" + xml.No + "&ShowType=" + this.ShowType + "' class=\"link_blue_03\">&nbsp" + xml.Name + "</a>&nbsp");
                //this.Pub3.Add("</li>");
            }
        }
        this.Pub3.Add("</p>");
        this.Pub3.Add("</div>");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Edu.EduUser.No) || BP.Edu.EduUser.No.Contains("admin"))
            Response.Write("<script> window.parent.location.href='../Port/SignIn.aspx?Jurl="+Request.RawUrl+"';</script>");
        //this.Pub1.AddTable("width=100%");
        //this.Pub1.AddTR();
        //this.Pub1.AddTDBegin();


        //this.Pub1.AddTDEnd();
        //this.Pub1.AddTREnd();
        //this.Pub1.AddTableEnd();
        getKM();

        setState();//设置请求状态
        BindTop();
        Questions ens = new Questions();
        QueryObject qo = new QueryObject(ens);
        qo.Top = 100;
        qo.AddWhere(QuestionAttr.IsRel, 1);

        switch (this.SortType)
        {
            case "BX":
                qo.addAnd();
                qo.AddWhere(QuestionAttr.FK_Dept, EduUser.FK_Dept);
                break;
            case "BQX":
                qo.addAnd();
                qo.AddWhere(QuestionAttr.FK_QX, EduUser.FK_QuXian);
                break;
            default:
                break;
        }
        if (this.ShowType == "init")
        {
            //if (this.SortType != "All")
                qo.addAnd();
            qo.AddWhere(QuestionAttr.Sta, "0");

        }
        else if (this.ShowType == "ok")
        {
            //if (this.SortType != "All")
                qo.addAnd();
            qo.AddWhere(QuestionAttr.Sta, "1");

        }

        if (KMType != null)
        {
            //if (ShowType != "all" || this.SortType != "All")
                qo.addAnd();
            qo.AddWhere(QuestionAttr.FK_KM, KMType);
        }

        qo.addOrderByDesc("OID");
        if (KMType != null)
        {
            this.Pub2.BindPageIdx(qo.GetCount(), Glo.PageSize, this.PageIdx, "AskV2.aspx?KMType=" + this.KMType + "&ShowType=" + this.ShowType + "&SortType=" + this.SortType,15);
        }
        else
        {
            this.Pub2.BindPageIdx(qo.GetCount(), Glo.PageSize, this.PageIdx, "AskV2.aspx?ShowType=" + this.ShowType + "&SortType=" + this.SortType,15);
        }
        qo.DoQuery("OID", Glo.PageSize, this.PageIdx);

        Bind(ens);
    }

    private void setState()
    {
        this.Pub3.Add("<div class=\"ttlm_Box clearfix\"><p class=\"l_ttlm_02\">");

        if (this.ShowType == "all")
        {
            //this.Pub3.Add("<li class=\"getItem\">");
            this.Pub3.Add("<span class=\"link_get\">全部请求</span>");
            //this.Pub3.Add("</li>");
        }
        else
        {
            if (this.KMType == null)
                this.Pub3.Add("<a href='AskV2.aspx?ShowType=all'>全部请求</a>");
            else
                this.Pub3.Add("<a href='AskV2.aspx?KMType=" + this.KMType + "&ShowType=all&SortType=" + this.SortType + "'>全部请求</a>");
            //this.Pub3.Add("</li>");
        }
        if (this.ShowType == "init")
        {
            //this.Pub3.Add("<li class=\"getItem\">");
            this.Pub3.Add("&nbsp<span class=\"link_get\">未解决</span>");
            //this.Pub3.Add("</li>");
        }
        else
        {
            //this.Pub3.Add("<li>");
            if (this.KMType == null)
                this.Pub3.Add("&nbsp<a href='AskV2.aspx?ShowType=init'>未解决</a> ");
            else
                this.Pub3.Add("&nbsp<a href='AskV2.aspx?KMType=" + this.KMType + "&ShowType=init&SortType = " + this.SortType + "'>未解决</a>");

            //this.Pub3.Add("</li>");
        }
        if (this.ShowType == "ok")
        {
            //this.Pub3.Add("<li class=\"getItem\">");
            this.Pub3.Add("&nbsp<span class=\"link_get\">已解决</span>");
            //this.Pub3.Add("</li>");
        }
        else
        {
            ////this.Pub3.Add("<li>");
            if (this.KMType == null)
                this.Pub3.Add("&nbsp<a href='AskV2.aspx?ShowType=ok'>已解决</a>");
            else
                this.Pub3.Add("&nbsp<a href='AskV2.aspx?KMType=" + this.KMType + "&ShowType=ok&SortType=" + this.SortType + "'>已解决</a>");
            //this.Pub3.Add("</li>");
        }

        this.Pub3.Add("</p>");
    }
    public void getKM()
    {
        this.Pub3.Add("<table class=\"tab_work\">");
        this.Pub3.Add("<tr><td colspan=\"3\"><img src=\"../Style/Img/work_t_l.gif\" alt=\"\"></td></tr>");
        this.Pub3.Add("<tr><td width=\"13\" class=\"line_l\"></td><td width=\"670\" ><ul class=\"list_work clearfix\">");
        Questions qs = new Questions();
        int i = qs.Retrieve(QuestionAttr.IsRel,1);
        if (this.KMType == null)
        {
            this.Pub3.Add("<li class=\"select\">");
            this.Pub3.Add("全部 (" + i + ")");
            this.Pub3.Add("</li>");
        }
        else
        {
            this.Pub3.Add("<li>");
            this.Pub3.Add("<a href='AskV2.aspx?ShowType=" + this.ShowType + "&SortType=" + this.SortType + "'>全部 (" + i + ")</a>");
            this.Pub3.Add("</li>");
        }


        foreach (KM km in EduUser.HisKMOfSet)
        {
            FAQ_KM_All fk = new FAQ_KM_All();
            fk.Retrieve(FAQ_KM_AllAttr.FK_KM, km.No,FAQ_KM_AllAttr.IsRel,1);
            string s = fk.Counts == "" ? "0" : fk.Counts;

            if (this.KMType == km.No)
            {
                this.Pub3.Add("<li class=\"select\">");
                this.Pub3.Add(km.Name + "(" + s + ")");
                this.Pub3.Add("</li>");
            }
            else
            {
                this.Pub3.Add("<li>");
                //this.Left1.Add("<a href='ListNew.aspx?ShowType=" + ShowType + "&KMType=" + km.No + "'>" + km.Name + "</a>");
                this.Pub3.Add("<a href='AskV2.aspx?KMType=" + km.No + "&ShowType=" + this.ShowType + "&SortType=" + this.SortType + "'>" + km.Name + "(" + s + ")</a>");
                this.Pub3.Add("</li>");
            }
        }

        this.Pub3.Add("</ul></td><td width=\"17\" class=\"line_r\">&nbsp;</td></tr><tr><td colspan=\"3\"><img src=\"../Style/Img/work_b_l.gif\" alt=\"\"></td></tr>");
        this.Pub3.Add("</table>");
    }
    public void Bind(Questions ens)
    {
        this.Pub1.Add("<div class=\"resBox\" style=\"text-align:center\"><div class=\"normalBox\" style=\"width:678px\"><table   class=\"tab_res\" width=\"100%\">");
        this.Pub1.Add("<tr style=\"text-align:center\">");
        this.Pub1.Add("<th>状态</th>");
        this.Pub1.Add("<th>标题</th>");
        this.Pub1.Add("<th>科目</th>");
        this.Pub1.Add("<th>年级</th>");
        this.Pub1.Add("<th>单位</th>");
        this.Pub1.Add("<th>阅读</th>");
        this.Pub1.Add("<th>回复</th>");
        this.Pub1.Add("<th>发布者</th>");
        this.Pub1.Add("<th>日期</th>");
        this.Pub1.Add("</tr>");
        foreach (Question en in ens)
        {
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td width=\"9%\">");
            this.Pub1.Add(en.StaImg);
            string LinkUrl = en.Sta == 1 ? "OKDesc.aspx" : "InitDesc.aspx";//判断解决状态
            this.Pub1.Add("<img src='Img/cent.gif' />" + en.Cent);
            this.Pub1.Add("</td>");
            string title = en.Title.Length > 10 ? en.Title.Substring(0, 10)+"..." : en.Title;
            this.Pub1.Add("<td style=\"text-align:left;width='20%';\">");
           //this.Pub1.Add("<a href=\"javascript:DoSelect('" + LinkUrl + "," + en.OID + "')\">" + title + "</a>");
            //this.Pub1.Add("<a href=\"en.Sta == 1 ?'OKDesc.aspx?RefOID='" + en.OID + " : 'InitDesc.aspx?RefOID='"+en.OID+";\">" + title + "</a>");
            if (en.Sta == 1)
                this.Pub1.Add("<a title='" + en.Title + "' target='frmBody' href='OKDesc.aspx?RefOID=" + en.OID + "'>" + title + "</a>");
            else
                this.Pub1.Add("<a title='" + en.Title + "' target='frmBody' href='InitDesc.aspx?RefOID=" + en.OID + "'>" + title + "</a>");

            

            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"10%\">");
            this.Pub1.Add(en.FK_KMText);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"10%\">");
            this.Pub1.Add(en.FK_NJText);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"15%\">");
            this.Pub1.Add(en.FK_DeptName);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"6%\">");
            this.Pub1.Add(en.NumOfRead.ToString());
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"6%\">");
            this.Pub1.Add(en.NumOfRe.ToString());
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width='15%'>");
            this.Pub1.Add(en.FK_EmpName);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width=\"10%\">");

            string rdt = null;

            if (en.RDT.Length == 0)
                rdt = en.RDT;
            else
                rdt = en.RDT.Substring(0,10);

            this.Pub1.Add(rdt);
            this.Pub1.Add("</td>");
            this.Pub1.Add("</tr>");
        }
        this.Pub1.Add("</table>");
        this.Pub1.Add("</div></div>");
        this.Pub1.Add("<p><img src=\"../Style/Img/res_b_l.gif\" alt=\"\" /></p>");
        if (ens.Count <= 5)
        {
            this.Pub1.AddB("本页共 " + ens.Count + "条数据");
            this.Pub1.Add("<img src=\"../Style/Img/spacer.gif\" alt=\"\" width=\"1\"  height=\"400\"/>");
        }
        else
        {
            this.Pub1.Add("<img src=\"../Style/Img/spacer.gif\" alt=\"\" width=\"1\"  height=\"20\"/>");
            this.Pub1.AddB("本页共 " + ens.Count + "条数据");
        }
    }
}
