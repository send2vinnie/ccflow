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

public partial class GE_ImgLink_ImgLinkContext : BP.Web.UC.UCBase3
{
    public string ImageSortNo
    {
        get
        {
            if (Request.QueryString["ImageSortNo"] == null || Request.QueryString["ImageSortNo"] == "")
            {
                try
                {
                    ImgLinks imgLinks = new ImgLinks();
                    imgLinks.RetrieveAll();
                    return imgLinks[0].No;
                }
                catch
                {
                    return null;
                }
            }
            else
                return Request.QueryString["ImageSortNo"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImgLinks imgLinks = new ImgLinks();
        //int imgWidth = imgLinks.GetEnsAppCfgByKeyInt("ImageW");

        this.GeImage1.GloRepeatColumns = imgLinks.GetEnsAppCfgByKeyInt("C_NumOfCol");
        this.GeImage1.PageSize = imgLinks.GetEnsAppCfgByKeyInt("C_PageSize");
        string ImageH = imgLinks.GetEnsAppCfgByKeyString("C_ImageH");
        string ImageW = imgLinks.GetEnsAppCfgByKeyString("C_ImageW");
        string groupTitle = imgLinks.GetEnsAppCfgByKeyString("GroupTitleCSS");
        this.Pub1.Add("<div class='" + groupTitle + "'>分类</div>");

        string appPath = this.Request.ApplicationPath;

        //string strSql = "select *, " + "WebPath" + " as imgSrc,'" + ImageH + "' as ImageH,'" + ImageW + "' as ImageW " + " from GE_ImgLink ";
        string strSql = "select No,Name,MyFileName,MyFilePath,FK_Sort,Url,case Target when 1 then '_blank' when 0 then '_self' end as Target, " + "WebPath" + " as imgSrc,'" + ImageH + "' as ImageH,'" + ImageW + "' as ImageW " + " from GE_ImgLink ";
        string sortName = "";
        if (this.ImageSortNo != null)
        {
            strSql += "where FK_Sort=" + this.ImageSortNo + "";
            ImgLinkSort en = new ImgLinkSort(this.ImageSortNo);
            sortName = en.Name;
        }

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
        GeImage1.GloDBSource = dt;
        if(dt.Rows.Count == 0)
        {
            this.Pub3.Add("没有相关信息。");
        }

        this.Pub2.AddTD("class='" + groupTitle + "'", sortName);
    }
}

