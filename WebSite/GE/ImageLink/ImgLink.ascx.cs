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

public partial class GE_ImgLink_ImgLink : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ImgLinks imgLinks = new ImgLinks();
        this.GeImage1.GloRepeatColumns = imgLinks.GetEnsAppCfgByKeyInt("NumOfCol");
        this.GeImage1.PageSize = imgLinks.GetEnsAppCfgByKeyInt("PageSize");
        string  ImageH = imgLinks.GetEnsAppCfgByKeyString  ("ImageH");
        string ImageW = imgLinks.GetEnsAppCfgByKeyString("ImageW");
        string groupTitle = imgLinks.GetEnsAppCfgByKeyString("GroupTitleCSS");

        this.Pub1.AddTD("class='"+groupTitle +"'", "<a href='ImgLinkContext.aspx' class='gengduo'>更多</a>" + GetTabTitle());
           
        //string appPath = this.Request.ApplicationPath;
        //string strSrc = "'" + appPath + "/Data/BP.GE.ImgLink/'+" + "No" + "+'.'+" + "MyFileExt";
        //string strSql = "select *, " + strSrc + " as imgSrc,'" + ImageH + "' as ImageH,'" + ImageW + "' as ImageW " + " from GE_ImgLink ";
        string strSql = "select No,Name,MyFileName,MyFilePath,FK_Sort,Url,case Target when 1 then '_blank' when 0 then '_self' end as Target, " + "WebPath" + " as imgSrc,'" + ImageH + "' as ImageH,'" + ImageW + "' as ImageW " + " from GE_ImgLink ";

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
        GeImage1.GloDBSource = dt;
    }

    public string GetTabTitle()
    {
        ImgLinks imgLinks = new ImgLinks();
        string tabTitle = imgLinks.GetEnsAppCfgByKeyString("TabTitle");
        return tabTitle;
    }
}
