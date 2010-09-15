using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Web;
using BP.En;
using BP.DA;
using System.Text;
using BP.GE;
using BP.Sys;
using System.Net;
using System.IO;

public partial class GE_Pict_PictMore : BP.Web.UC.UCBase3
{
    public string RefNo
    {
        get
        {
            if (this.Request.QueryString["RefNo"] == null || this.Request.QueryString["RefNo"] == "")
                return "01";
            return this.Request.QueryString["RefNo"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "员工风采";
        Picts pics = new Picts();
        int imgWidth = pics.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = pics.GetEnsAppCfgByKeyInt("ImgHeight");
        this.GeImage1.GloRepeatColumns = pics.GetEnsAppCfgByKeyInt("Cols");
        this.GeImage1.PageSize = pics.GetEnsAppCfgByKeyInt("PageSize");
        //找不到图片时的默认图片
        string DefImgSrc = this.Request.ApplicationPath + "/GE/Pict/img/default.jpg";

        string fk_type = this.RefNo;
        string sortName = "";
        PictSorts sorts = new PictSorts();
        sorts.RetrieveAll();

        if (sorts == null)
        {
            return;
        }

        //左侧分类列表
        //this.Pub1.Add("<div class=\"cont_left\"  style=\"float:left;width:20%\">");
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDGroupTitle("分类");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.Add("<marquee><td class='enSort' style='padding:5px'></marquee>");
        this.Pub1.AddUL();
        foreach (PictSort sort in sorts)
        {
            this.Pub1.AddLi("<a href='PictMore.aspx?RefNo=" + sort.No + "'>" + sort.Name + "</a>");
            if (fk_type == sort.No)
            {
                sortName = sort.Name;
            }
        }
        this.Pub1.AddULEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        //this.Pub1.AddDivEnd();

        Picts ens = new Picts();
        ens.Retrieve(PictAttr.FK_Sort, fk_type);


        //右侧视频列表
        this.Pub2.AddTDGroupTitle(sortName);
        DataTable dt = new DataTable();
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("DefImgSrc");
        dt.Columns.Add("Title");
        dt.Columns.Add("No");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");
        foreach (Pict en in ens)
        {
            dt.Rows.Add(en.WebPath, DefImgSrc, en.Name, en.No, imgWidth, imgHeight);
        }
        GeImage1.GloDBSource = dt;
        if (dt.Rows.Count == 0)
        {
            this.Pub3.Add("<span>该类别下没有相关信息。</span>");
        }

    }

    //public void BindMore()
    //{





    //    PictSorts psor = new PictSorts();
    //    psor.RetrieveAll();
    //    PictSort ps =new PictSort (this.SortNo);

    //    Picts pics = new Picts();
    //    QueryObject qo = new QueryObject(pics);
    //    qo.Top = 6;
    //    qo.AddWhere(PictAttr.FK_Sort, pic.FK_Sort);
    //    qo.DoQuery();

    //    this.AddTable();
    //    //分类title
    //    this.AddTR();
    //    this.Add("<td class='title_03' style='width:30%'>");
    //    this.Add("分类");
    //    this.Add("</td>");

    //    //当前类名
    //    this.Add("<td class='title_03'>");
    //    this.Add(ps.Name);
    //    this.AddTDEnd();
    //    this.AddTR();

    //    this.AddTR();
    //    //分类详细
    //    this.Add("<td style='padding-bottom: 5px; padding-left: 5px; padding-right: 5px; padding-top: 5px;'>");
    //    this.Add("<div>");
    //    this.AddUL();
    //    foreach (PictSort ps in psor)
    //    {
    //        this.AddLi(this.Request.ApplicationPath + "/PictMore.aspx?SortNo=" + ps.No, ps.Name);
    //    }
    //    this.AddULEnd();
    //    this.Add("/div");
    //    this.AddTDEnd();

    //    this.AddTREnd();
    //    this.AddTable();
    //}
}
