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

public partial class GE_ImgLink_ImgLinkDtl : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ImgLink1s imgLinks = new ImgLink1s();

        this.GeImage1.GloRepeatColumns = imgLinks.GetEnsAppCfgByKeyInt("NumOfCol");
        int cols = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.ImgLink1s", "NumOfCol");
        int rows = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.ImgLink1s", "NumOfRow");
        this.GeImage1.GloRepeatColumns = cols;
        this.GeImage1.PageSize = cols * rows;
        string ImageH = imgLinks.GetEnsAppCfgByKeyString("ImageH");
        string ImageW = imgLinks.GetEnsAppCfgByKeyString("ImageW");
        string groupTitle = imgLinks.GetEnsAppCfgByKeyString("GroupTitleCSS");

        string strSql = "select No,Name,Url,case Target when 1 then '_blank' when 0 then '_self' end as Target, " + "WebPath" + " as imgSrc,'" + ImageH + "' as ImageH,'" + ImageW + "' as ImageW " + " from GE_ImgLink1 ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
        GeImage1.GloDBSource = dt;
    }
}

