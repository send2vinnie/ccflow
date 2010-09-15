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
using BP.Web;
using BP.En;
using BP.DA;
using BP.GE;
using System.IO;

public partial class GE_ImgLink1 : BP.Web.UC.UCBase3
{
    //DoType不为空时，当前为更多页面
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    int ImgHeight = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.ImgLink1s", "ImageH");
    int ImgWidth = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.ImgLink1s", "ImageW");
    string DefImgSrc = HttpContext.Current.Request.ApplicationPath + "/GE/FDB/img/default.jpg";
    protected void Page_Load(object sender, EventArgs e)
    {
        ImgLink1s ens = new ImgLink1s();
        //属性设置
        string title = ens.GetEnsAppCfgByKeyString("Title");
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");

        int cols = 4;
        int rows = 2;
        //更多页面
        if (this.DoType != null)
        {
            this.GeImage1.ShowPage = true;
            cols = ens.GetEnsAppCfgByKeyInt("M_NumOfCol");
            rows = ens.GetEnsAppCfgByKeyInt("M_NumOfRow");
            this.Pub1.AddTD("class='" + groupTitle + "'", title);
            ens.RetrieveAll();
        }
        else
        {
            this.GeImage1.ShowPage = false;
            cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
            rows = ens.GetEnsAppCfgByKeyInt("NumOfRow");
            //更多页面的打开方式
            int target = ens.GetEnsAppCfgByKeyInt("M_Target");
            string strTarget = "_self";
            if (target == 1)
            {
                strTarget = "_blank";
            }

            this.Pub1.AddTD("class='" + groupTitle + "'", "<a target='" + strTarget + "' href='ImgLink1.aspx?DoType=ShowMore' class='gengduo' style='float:right'>更 多...</a>" + title);
            ens.RetrieveFocus();
        }
        this.GeImage1.GloRepeatColumns = cols;
        this.GeImage1.PageSize = cols * rows;

        DataTable dt = new DataTable();
        //图片路径
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("DefImgSrc");
        //链接跳转地址
        dt.Columns.Add("Url");
        dt.Columns.Add("Name");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");
        dt.Columns.Add("Target");

        ////绑定专辑资源(硬盘)
        //BindFDB(dt);

        //绑定专辑资源(链接)
        foreach (ImgLink1 en in ens)
        {
            dt.Rows.Add(en.WebPath, DefImgSrc, "OpenLink1.aspx?RefNo=" + en.No, en.Name, ImgWidth + "px", ImgHeight + "px", en.Target);
        }

        GeImage1.GloDBSource = dt;
        if (dt.Rows.Count <= 0)
        {
            this.Pub2.Add("没有相关信息。");
        }
    }
}
