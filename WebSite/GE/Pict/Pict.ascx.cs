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
using BP.GE;
using BP.Web;

public partial class GE_Pict_Pict : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Picts ens = new Picts();
        //显示图片的总个数
        int topNum = ens.GetEnsAppCfgByKeyInt("PageSize");

        //一列显示的图片个数
       this.GeImage1.GloRepeatColumns = ens.GetEnsAppCfgByKeyInt("Cols");
      //  this.GeImage1.GloRepeatColumns = 2; // ens.GetEnsAppCfgByKeyInt("Cols");

        string title = ens.GetEnsAppCfgByKeyString("Title");
        int imgWidth = ens.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = ens.GetEnsAppCfgByKeyInt("ImgHeight");
        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");
        //找不到图片时的默认图片
        string DefImgSrc = this.Request.ApplicationPath + "/GE/Pict/img/default.jpg";

        ens.RetrieveAll();
        if (ens.Count > topNum)
        {
            this.Pub1.AddTD("class ='" + groupTitle + "'", "<a href='PictMore.aspx' class='gengduo' style='float:right' style='float:right' >更多...</a>" + title);
        }
        else
        {
            this.Pub1.AddTD("class ='" + groupTitle + "'", title);
        }

        DataTable dt = new DataTable();
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("DefImgSrc");
        dt.Columns.Add("Name");
        dt.Columns.Add("No");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");

        int count = 1;
        foreach (Pict en in ens)
        {
            if (count > topNum || en.WebPath == null || en.WebPath == "")
            {
                break;
            }
            dt.Rows.Add(en.WebPath, DefImgSrc, en.Name, en.No, imgWidth, imgHeight);
            count++;
        }
        GeImage1.GloDBSource = dt;

        if (dt.Rows.Count == 0)
        {
            this.Pub2.Add("没有相关信息");
        }
    }
}
