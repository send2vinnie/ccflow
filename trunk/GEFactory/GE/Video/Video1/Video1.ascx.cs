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
using BP.Web;
using BP.Sys;
using System.Text;

public partial class GE_Video_Video1 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Video1s ens = new Video1s();

        //行数、列数
        int rows = ens.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");
        string title = ens.GetEnsAppCfgByKeyString("Title");
        //更多页面的打开方式
        int target = ens.GetEnsAppCfgByKeyInt("M_Target");
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }
        this.PubUp.AddTD("class='" + groupTitle + "'", "<a class='gengduo' style='float:right' target='" + strTarget + "' href='VideoMore1.aspx' >更 多..</a>" + title);

        //图片的宽度和高度
        int imgWidth = ens.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = ens.GetEnsAppCfgByKeyInt("ImgHeight");

        //一列显示的图片个数
        this.GeImage1.GloRepeatColumns = cols;
        this.GeImage1.pageSize = rows * cols;

        DataTable dt = new DataTable();
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("Title");
        dt.Columns.Add("No");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");
        dt.Columns.Add("DefImgSrc");
        string DefImgSrc = HttpContext.Current.Request.ApplicationPath + "/GE/Video/Images/Default.jpg";
        ens.RetrieveByRecom(pageSize);
        foreach (Video1 en in ens)
        {
            dt.Rows.Add(en.WebPath, en.Name, en.No, imgWidth, imgHeight, DefImgSrc);
        }
        GeImage1.GloDBSource = dt;
        if (dt.Rows.Count == 0)
        {
            this.Pub1.Add("没有相关信息。");
        }
    }
}
