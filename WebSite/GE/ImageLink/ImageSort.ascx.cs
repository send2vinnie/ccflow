using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GE;

public partial class GE_ImageLink_ImageSort : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ImgLinkSorts imageSorts = new ImgLinkSorts();
        imageSorts.RetrieveAll();
        this.Add("<Table>");
        this.AddTR();
        this.Add("<td class='enSort' style='border:0;'>");
        this.AddUL();

        foreach (ImgLinkSort imageSort in imageSorts)
        {
            this.AddLi("<a href='ImgLinkContext.aspx?ImageSortNo=" + imageSort.No + "'>"+imageSort.Name +"</a>");
        }
        this.AddTDEnd();
        this.AddULEnd();

        this.AddTREnd();
        this.AddTableEnd();
    }
}
