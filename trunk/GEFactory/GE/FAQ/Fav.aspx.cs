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

public partial class FAQ_Fav : BP.Web.WebPage
{
    string[] ss = new string[] { "小学", "初中", "高中" };
    public string KMType
    {
        get
        {
            string s = this.Request.QueryString["KMType"];
            if (s == null)
                s = EduUser.FK_KM;
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        getKM();
        SCs ens = new SCs();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(SCAttr.FK_Type, "FAQ");
        qo.addAnd();
        qo.AddWhere(SCAttr.FK_Emp, EduUser.No);
        int num = qo.DoQuery();

        this.Pub1.AddTable("style={width:95%;}");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=3%", "");
        this.Pub1.AddTDTitle("width=20%", "标题");
        this.Pub1.AddTDTitle("阅读");
        this.Pub1.AddTDTitle("回复");
        this.Pub1.AddTDTitle("width=10%", "单位");
        this.Pub1.AddTDTitle("width=5%", "作者");
        this.Pub1.AddTDTitle("width=5%", "删除");
        this.Pub1.AddTREnd();
        foreach (SC en in ens)
        {
            Question q = new Question();
            QueryObject qo2 = new QueryObject(q);
            qo2.AddWhere(QuestionAttr.OID, en.RefOBJ);
            qo2.addAnd();
            qo2.AddWhere(QuestionAttr.FK_KM, this.KMType);

            num = qo2.DoQuery();

            if (num == 0)
            {
                //this.Alert("数据异常--你的收藏已被删除");
                //en.Delete();
            }
            else
            {
                this.Pub1.AddTR("style={text-align:center;width:100%;}");
                this.Pub1.AddTD(q.Sta == 1 ? "<img src='Img/1.jpg' />" : "<img src='Img/0.jpg' />");
                this.Pub1.AddTD("<img src='Img/cent.gif' />" + q.Cent);
                string LinkUrl = q.Sta == 1 ? "OKDesc.aspx" : "InitDesc.aspx";//判断解决状态
                string title = q.Title.Length > 30 ? q.Title.Substring(0, 30) : q.Title;
                this.Pub1.AddTD("style={text-align:left;}", "<a href=\"javascript:DoSelect('" + LinkUrl + "," + q.OID + "')\">" + title + "</a>");
                this.Pub1.AddTD(" style={text-align:center;width:5%}", q.NumOfRead);
                this.Pub1.AddTD(" style={text-align:center;width:5%}", q.NumOfRe);
                this.Pub1.AddTD(q.FK_DeptName);
                this.Pub1.AddTD(q.FK_EmpName);
                this.Pub1.AddTD("<a href=\"javascript:DoDelFav('" + en.RefOBJ + "')\">删除</a>");
                this.Pub1.AddTREnd();
            }
        }
        this.Pub1.AddTableEnd();
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
    public void getKM()
    {

        foreach (KM km in EduUser.HisKMOfSet)
        {
            if (this.KMType == km.No)
            {
                this.Pub2.Add("<li class=\"getItem\">");
                this.Pub2.Add(km.Name);
                this.Pub2.Add("</li>");
            }
            else
            {
                this.Pub2.Add("<li>");
                this.Pub2.Add("<a href='Fav.aspx?KMType=" + km.No + "'>" + km.Name + "</a>");
                this.Pub2.Add("</li>");
            }
        }
    }
    /// <summary>
    /// 科目分组
    /// </summary>
    /// <param name="no"></param>
    public void setkm(string no)
    {
        KMs kms = new KMs();
        QueryObject qo = new QueryObject(kms);
        string sql = string.Empty;
        switch (no)
        {
            case "小学":
                sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'a%'))";
                break;
            case "初中":
                sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'b%'))";
                break;
            case "高中":
                sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'c%'))";
                break;
            default:
                break;
        }
        qo.AddWhereInSQL("No", sql);
        qo.addOrderBy("name");
        int num = qo.DoQuery();
        foreach (KM km in kms)
        {
            if (this.KMType == km.No)
            {
                this.Pub2.AddB(km.Name + "<br><br>");
            }
            else
                this.Pub2.AddB("<a href='Fav.aspx?KMType=" + km.No + "'>" + km.Name + "</a><br><br>");
        }
    }
}
