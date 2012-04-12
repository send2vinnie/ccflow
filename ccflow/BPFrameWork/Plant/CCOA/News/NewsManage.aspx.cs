using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_News_NewsManage : System.Web.UI.Page
{
    private string ArticleId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ArticleId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(ArticleId))
            {
                NewsForm1.ArticleId = this.ArticleId;
            }
        }
    }
}