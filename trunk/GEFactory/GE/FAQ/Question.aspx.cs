/*
 * 
 * 创建zcs
 * 
 * 
 * */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using System.IO;
public partial class FAQ_Question : BP.Web.WebPage
{

    public string ShowType
    {
        get
        {
            string s = this.Request.QueryString["ShowType"];
            if (s == null)
                s = "0";
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Questions questions = new Questions();
        QueryObject qo = new QueryObject(questions);
        qo.AddWhere(QuestionAttr.FK_Emp, BP.Edu.EduUser.No);
        qo.addAnd();
        qo.AddWhere(QuestionAttr.Sta, this.ShowType);
        qo.addOrderByDesc("OID");
        this.Pub3.BindPageIdx(qo.GetCount(), Glo.PageSize, this.PageIdx, "Question.aspx?ShowType=" + this.ShowType);
        int num=qo.DoQuery("OID", Glo.PageSize, this.PageIdx);
        this.BindLeft();
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=3%", "");
        this.Pub1.AddTDTitle("width=30% ", "标题");
        this.Pub1.AddTDTitle("width=10%", "科目");
        this.Pub1.AddTDTitle("width=10% ", "年级");
        this.Pub1.AddTDTitle("width=10%", "单位");
        this.Pub1.AddTDTitle("阅读");
        this.Pub1.AddTDTitle("回复");
        this.Pub1.AddTDTitle("width=10%", "发布者");
        this.Pub1.AddTDTitle("width=10%", "日期");
        this.Pub1.AddTREnd();

        foreach (Question question in questions)
        {
            this.Pub1.AddTR(" style='text-align:center;width:100%;'");
            this.Pub1.AddTD(question.Sta == 1 ? "<img src='Img/1.jpg' />" : "<img src='Img/0.jpg' />");
            string LinkUrl = question.Sta == 1 ? "OKDesc.aspx" : "InitDesc.aspx";//判断解决状态
            this.Pub1.AddTD("<img src='Img/cent.gif' />" + question.Cent);
            string title = question.Title.Length > 20 ? question.Title.Substring(0, 20) : question.Title;
            this.Pub1.AddTD(" style='text-align:left;'", "<a href=\"javascript:DoSelect('" + LinkUrl + "," + question.OID + "')\"  alt=\"" + question.Title + "\"\\>" + title + "</a>");
            this.Pub1.AddTD(question.FK_KMText);
            this.Pub1.AddTD(question.FK_NJText);
            this.Pub1.AddTD(question.FK_DeptName);
            this.Pub1.AddTD("style='text-align:center;width:5%'", question.NumOfRead);
            this.Pub1.AddTD("style='text-align:center;width:5%'", question.NumOfRe);
            this.Pub1.AddTD(question.FK_EmpName);

            string rdt = null;

            if (question.RDT.Length == 0)
                rdt = question.RDT;
            else
                rdt = question.RDT.Substring(0,10);
            this.Pub1.AddTD(rdt);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();
        if (num <= 5)
        {
            this.Pub1.AddB("本页共 " + num.ToString() + "条数据");
            this.Pub1.Add("<img src=\"../Style/Img/spacer.gif\" alt=\"\" width=\"1\"  height=\"400\"/>");
        }
        else
        {
            this.Pub1.Add("<img src=\"../Style/Img/spacer.gif\" alt=\"\" width=\"1\"  height=\"20\"/>");
            this.Pub1.AddB("本页共 " + num.ToString() + "条数据");
        }
        return;
    }
    public void BindLeft()
    {
        if (this.ShowType == "0")
        {
            this.Pub2.Add("<li class=\"getItem\">");
            this.Pub2.Add("未解决");
            this.Pub2.Add("</li>");
        }
        else
        {
            this.Pub2.Add("<li>");
            this.Pub2.Add("<a href='Question.aspx?ShowType=0'>未解决</a>");
            this.Pub2.Add("</li>");
        }
        if (this.ShowType == "1")
        {
            this.Pub2.Add("<li class=\"getItem\">");
            this.Pub2.Add("已解决");
            this.Pub2.Add("</li>");
        }
        else
        {
            this.Pub2.Add("<li>");
            this.Pub2.Add("<a href='Question.aspx?ShowType=1'>已解决</a>");
            this.Pub2.Add("</li>");
        }
    }
}
