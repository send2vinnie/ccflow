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
using BP.GE;
using BP.Web.Controls;
using BP.Sys;
using BP.En;

public partial class GE_Video_VideoMore1 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Video1s ens = new Video1s();
        //行数、列数
        int cols = ens.GetEnsAppCfgByKeyInt("M_NumOfCol");
        int rows = ens.GetEnsAppCfgByKeyInt("M_NumOfRow");
        int pageSize = cols * rows;

        this.GeImage1.GloRepeatColumns = cols;
        this.GeImage1.PageSize = pageSize;
        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");
        string DefImgSrc = HttpContext.Current.Request.ApplicationPath + "/GE/Video/Images/Default.jpg";

        //左侧分类列表
        this.PubSort.AddTable("style='border-collapse:collapse;'");
        this.PubSort.AddTR();
        this.PubSort.AddTD("class ='" + groupTitle + "'", "分类");
        this.PubSort.AddTREnd();

        this.PubSort.AddTR();
        this.PubSort.AddTDBegin();

        VideoSort1s sorts = new VideoSort1s();
        sorts.RetrieveAll();
        string fk_type = "-1";
        string sortName = "";
        this.PubSort.MenuSelfVerticalBegin();
        //添加全部选项
        if (this.RefNo == null || this.RefNo == "")
        {
            this.PubSort.MenuSelfVerticalItemS("VideoMore1.aspx", "全部", null);
            sortName = "全部";
        }
        else
        {
            this.PubSort.MenuSelfVerticalItem("VideoMore1.aspx", "全部", null);
        }

        foreach (VideoSort1 sort in sorts)
        {
            if (this.RefNo == sort.No)
            {
                this.PubSort.MenuSelfVerticalItemS("VideoMore1.aspx?RefNo=" + sort.No, sort.Name, null);
                sortName = sort.Name;
                fk_type = sort.No;
            }
            else
            {
                this.PubSort.MenuSelfVerticalItem("VideoMore1.aspx?RefNo=" + sort.No, sort.Name, null);
            }
        }
        this.PubSort.MenuSelfVerticalEnd();

        //添加类别名称
        this.PubSearch.AddTD("class ='" + groupTitle + "' style='border-right:0;'", sortName);

        //添加搜索栏
        this.PubSearch.Add("<td class ='" + groupTitle + "' style='border-left:0;text-align:center;'>");
        TB tbKeyWords = new TB();
        tbKeyWords.ID = "tbKeyWords";
        this.PubSearch.Add("关键字：");
        this.PubSearch.Add(tbKeyWords);

        //添加搜索按钮
        Btn btnSearch = new Btn();
        btnSearch.Text = "搜索";
        btnSearch.Click += new EventHandler(btnSearch_Click);
        this.PubSearch.Add("&nbsp;&nbsp;");
        this.PubSearch.Add(btnSearch);
        this.PubSearch.AddTDEnd();

        this.PubSort.AddTDEnd();
        this.PubSort.AddTREnd();
        this.PubSort.AddTableEnd();

        //右侧视频列表
        DataTable dt = new DataTable();
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("Title");
        dt.Columns.Add("No");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");
        dt.Columns.Add("DefImgSrc");

        //绑定视频列表
        QueryObject qo = new QueryObject(ens);
        string keyWords = "";
        if (Session["KeyWords"] != null)
        {
            keyWords = Session["KeyWords"].ToString();
        }
        qo.AddWhere("1=1");
        if (keyWords != null && keyWords != "")
        {
            qo.addAnd();
            qo.AddWhere(Video1Attr.Name, "like", "%" + keyWords + "%");
            tbKeyWords.Text = keyWords;
        }
        if (this.RefNo != null && this.RefNo != "")
        {
            qo.addAnd();
            qo.AddWhere(Video1Attr.FK_Sort, this.RefNo);
        }
        qo.DoQuery();

        int imgWidth = ens.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = ens.GetEnsAppCfgByKeyInt("ImgHeight");
        foreach (Video1 en in ens)
        {
            dt.Rows.Add(en.WebPath, en.Name, en.No, imgWidth, imgHeight, DefImgSrc);
        }
        GeImage1.GloDBSource = dt;
        if (dt.Rows.Count == 0)
        {
            this.Pub3.Add("没有相关视频。");
        }
    }

    //搜索事件
    void btnSearch_Click(object sender, EventArgs e)
    {
        TB tbKeyWords = (TB)this.PubSearch.GetTBByID("tbKeyWords");

        if (tbKeyWords.Text.Trim() != "")
        {
            Session["KeyWords"] = tbKeyWords.Text.Trim();
        }
        else
        {
            Session["KeyWords"] = null;
        }

        //绑定视频列表
        this.Response.Redirect("VideoMore1.aspx?RefNo=" + this.RefNo, true);
    }
}
