using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_News_uc_NewsForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        Article article = new Article();
        article.Id = Guid.NewGuid().ToString();
        article.Title = txtTitle.Text;
        article.Keyword = txtKeyWords.Text;
        article.Content = txtContent.Text;
        article.Description = txtDescribe.Text;
        article.SequenceIndex = txtOrder.Text;
        article.SubTitle = txtSubTitle.Text;
        article.State = int.Parse(ddlState.SelectedValue);
        article.OwnerId = ddrChannel.Text;
        article.ContentType = int.Parse(ddrType.Text);

        article.Insert();
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {

    }
}