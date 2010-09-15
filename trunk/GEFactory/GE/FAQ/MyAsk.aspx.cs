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
public partial class FAQ_MyAsk : BP.Web.WebPage
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Edu.EduUser.No) || BP.Edu.EduUser.No.Contains("admin"))
        {
            Response.Write("<script> window.parent.location.href='../Port/SignIn.aspx?Jurl="+Request.RawUrl+"';</script>");
            return;
        }
        getKM();

        //this.Pub1.Add("<div class=\"ttlm_Box clearfix\">");
        //this.Pub1.Add("<p class=\"l_ttlm\">请求列表</p>");
        //this.Pub1.Add("<p class=\"r_link\">  </p>");
        //this.Pub1.Add("</div>");

        setState();//设置请求状态
        Questions ens = new Questions();
        QueryObject qo = new QueryObject(ens);
        qo.Top = 100;
        if (this.ShowType == "init")
        {
            qo.AddWhere(QuestionAttr.FK_Emp, EduUser.No);
            qo.addAnd();
            qo.AddWhere(QuestionAttr.Sta, "0");

        }
        else if (this.ShowType == "ok")
        {
            qo.AddWhere(QuestionAttr.FK_Emp, EduUser.No);
            qo.addAnd();
            qo.AddWhere(QuestionAttr.Sta, "1");
        }
        else
        {
            qo.AddWhere(QuestionAttr.FK_Emp, EduUser.No);
        }

        if (KMType != null)
        {
            qo.addAnd();
            qo.AddWhere(QuestionAttr.FK_KM, KMType);
        }

        qo.addOrderByDesc("OID");
        if (KMType != null)
        {
            this.Pub2.BindPageIdx(qo.GetCount(), Glo.PageSize, this.PageIdx, "MyAsk.aspx?KMType=" + this.KMType + "&ShowType=" + this.ShowType,15);
        }
        else
        {
            this.Pub2.BindPageIdx(qo.GetCount(), Glo.PageSize, this.PageIdx, "MyAsk.aspx?ShowType=" + this.ShowType,15);
        }
        qo.DoQuery("OID", Glo.PageSize, this.PageIdx);
        Bind(ens);
    }

    private void setState()
    {

        this.Pub1.Add("<div class=\"ttlm_Box clearfix\">");
        this.Pub1.Add("<p class=\"l_ttlm\">请求列表</p>");
        this.Pub1.Add("<p class=\"r_link_02\">");
        //this.Pub1.Add("<p class=\"l_newhot\">");
        //this.Pub1.Add("</p>");
        //this.Pub1.Add("<span class=\"hot\">");
        //this.Pub1.Add("</p>");

        if (this.ShowType == "all")
        {
            this.Pub1.Add("<span class=\"link_get\">");
            this.Pub1.Add("全部请求");
            this.Pub1.Add("</span>");
        }
        else
        {

            if (this.KMType == null)
                this.Pub1.Add("<a href='MyAsk.aspx?ShowType=all' >全部请求</a>");
            else
                this.Pub1.Add("<a href='MyAsk.aspx?KMType=" + this.KMType + "&ShowType=all'>全部请求</a>");

        }

        if (this.ShowType == "init")
        {
            this.Pub1.Add("<span class=\"link_get\">");
            this.Pub1.Add("未解决");
            this.Pub1.Add("</span>");
        }
        else
        {

            if (this.KMType == null)
                this.Pub1.Add("<a href='MyAsk.aspx?ShowType=init'>未解决</a>");
            else
                this.Pub1.Add("<a href='MyAsk.aspx?KMType=" + this.KMType + "&ShowType=init'>未解决</a>");


        }
        if (this.ShowType == "ok")
        {
            this.Pub1.Add("<span class=\"link_get\">");
            this.Pub1.Add("已解决");
            this.Pub1.Add("</span>");
        }
        else
        {

            if (this.KMType == null)
                this.Pub1.Add("<a href='MyAsk.aspx?ShowType=ok'>已解决</a>");
            else
                this.Pub1.Add("<a href='MyAsk.aspx?KMType=" + this.KMType + "&ShowType=ok'>已解决</a>");

        }

        this.Pub1.Add("</p>");
        this.Pub1.Add("</div>");
    }
    public void getKM()
    {

        this.Pub1.Add("<table class=\"tab_setwork\" >");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td><p><img src=\"../Style/Img/ttlm_selkemu.gif\" alt=\"请选择科目\" /></p></td>");
        this.Pub1.Add("<td align='right'><a class=\"link_qa\" href=\"javascript:DoAsk() \">我要请求</a></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");

        this.Pub1.Add("<table class=\"tab_work\" >");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td colspan=\"3\"><img src=\"../Style/Img/work_t_l.gif\" alt=\"\"></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width=\"13\" class=\"line_l\"></td>");
        this.Pub1.Add("<td width=\"670\" ><ul class=\"list_work clearfix\">");

        FAQ_KMs fks = new FAQ_KMs();
        fks.Retrieve(FAQ_KMAttr.FK_Emp, EduUser.No);

        Question q = new Question();
        int i = q.Retrieve(QuestionAttr.FK_Emp, EduUser.No);


        if (this.KMType == null)
        {
            this.Pub1.Add("<li class=\"select\">");
            this.Pub1.Add("全部" + "(" + i + ")");
            this.Pub1.Add("</li>");
        }
        else
        {
            this.Pub1.Add("<li>");
            this.Pub1.Add("<a href='MyAsk.aspx?ShowType=" + this.ShowType + "'>全部(" + i + ")</a>");
            this.Pub1.Add("</li>");
        }

        foreach (FAQ_KM fk in fks)
        {
            KM km = new KM();
            km.Retrieve(KMAttr.No, fk.FK_KM);
            if (this.KMType == km.No)
            {
                this.Pub1.Add("<li class=\"select\">");
                this.Pub1.Add(km.Name + "(" + fk.Counts + ")");
                this.Pub1.Add("</li>");
            }
            else
            {
                this.Pub1.Add("<li>");
                this.Pub1.Add("<a href='MyAsk.aspx?KMType=" + km.No + "&ShowType=" + this.ShowType + "'>" + km.Name + "(" + fk.Counts + ")</a>");
                this.Pub1.Add("</li>");
            }
        }

        this.Pub1.Add("</ul></td>");
        this.Pub1.Add("<td width=\"17\" class=\"line_r\">&nbsp;</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td colspan=\"3\"><img src=\"../Style/Img/work_b_l.gif\" alt=\"\"></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
    }
    public void Bind(Questions ens)
    {

        this.Pub1.Add("<div  class=\"resBox\" style=\"text-align:center\">");
        this.Pub1.Add("<div class=\"normalBox\" style=\"width:678px\">");
        this.Pub1.Add("<table class=\"tab_res\" width=\"98%\">");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th >状态</th>");
        this.Pub1.Add("<th ></th>");
        this.Pub1.Add("<th >标题</th>");
        this.Pub1.Add("<th >科目</th>");
        this.Pub1.Add("<th >年级</th>");
        this.Pub1.Add("<th >单位</th>");
        this.Pub1.Add("<th >阅读</th>");
        this.Pub1.Add("<th >回复</th>");
        this.Pub1.Add("<th >日期</th>");

        this.Pub1.Add("</tr>");


        foreach (Question en in ens)
        {
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.StaImg);
            this.Pub1.Add("</td>");
            string LinkUrl = en.Sta == 1 ? "OKDesc.aspx" : "InitDesc.aspx";//判断解决状态
            this.Pub1.Add("<td>");
            this.Pub1.Add("<img src='Img/cent.gif' />" + en.Cent);
            this.Pub1.Add("</td>");
            string title = en.Title.Length > 15 ? en.Title.Substring(0, 15)+"..." : en.Title;
            this.Pub1.Add("<td style=\"text-align:left\">");
            if (en.Sta == 1)
                this.Pub1.Add("<a title='"+en.Title+"' target='frmBody' href='OKDesc.aspx?RefOID=" + en.OID + "'>" + title + "</a>");
            else
                this.Pub1.Add("<a title='" + en.Title + "' target='frmBody' href='InitDesc.aspx?RefOID=" + en.OID + "'>" + title + "</a>"); this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.FK_KMText);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.FK_NJText);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.FK_DeptName);
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.NumOfRead.ToString());
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");
            this.Pub1.Add(en.NumOfRe.ToString());
            this.Pub1.Add("</td>");
            this.Pub1.Add("<td>");

            string s = null;
            if (en.RDT.Length == 0)
                s = en.RDT;
            else
                s = en.RDT.Substring(0,10);

            this.Pub1.Add(s);
            this.Pub1.Add("</td>");
            Pub1.Add("</tr>");
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
