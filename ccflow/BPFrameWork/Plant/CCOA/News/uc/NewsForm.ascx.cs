using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using System.Data;

public partial class CCOA_News_uc_NewsForm : System.Web.UI.UserControl
{
    public string ArticleId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            this.ddrChannel.DataSource = DTChannels;
            this.ddrChannel.DataBind();

            if (!string.IsNullOrEmpty(ArticleId))
            {
                SetData();
            }
        }
    }

    public DataTable DTChannels
    {
        get
        {
            Channels channels = new Channels();
            return channels.RetrieveAllToTable();
        }
    }

    protected void SetData()
    {
        Article article = new Article(ArticleId);
        //article.RetrieveByNo(ArticleId);

        txtTitle.Text = article.Title;
        txtContent.Text = article.Content;
        txtDescribe.Text = article.Description;
        txtKeyWords.Text = article.Keyword;
        txtOrder.Text = article.SequenceIndex;
        txtSubTitle.Text = article.SubTitle;
        ddrChannel.SelectedValue = article.ChannelFullUrl;
        ddlState.SelectedValue = article.State.ToString();
        ddrType.SelectedValue = article.ContentType.ToString();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        Article article = new Article();
        //article.Id = Guid.NewGuid().ToString();
        article.No = Guid.NewGuid().ToString();
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

        btnCommit.Enabled = false;
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewsList.aspx");
    }
}