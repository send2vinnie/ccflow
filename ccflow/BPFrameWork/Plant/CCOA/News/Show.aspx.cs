using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace Lizard.OA.Web.OA_News
{
    public partial class Show : BasePage
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    string NewsId = strid;
                    ShowInfo(NewsId);
                    AddClicks(NewsId);
                }
            }
        }

        private void AddClicks(string NewsId)
        {
            BP.CCOA.OA_News model = new BP.CCOA.OA_News(NewsId);
            model.Clicks = model.Clicks + 1;
            model.Update();
        }

        private void ShowInfo(string NewsId)
        {
            BP.CCOA.OA_News model = new BP.CCOA.OA_News(NewsId);
            //this.lblNewsId.Text = model.NewsId;
            this.lblNewsTitle.Text = model.NewsTitle;
            this.lblNewsTitle0.Text = model.NewsTitle;
            this.lblNewsSubTitle.Text = model.NewsSubTitle;
            this.lblNewsType.Text = model.NewsType;
            this.lblNewsContent.Text = model.NewsContent;
            //this.Content = model.NewsContent;
            this.lblAuthor.Text = model.Author;
            this.lblCreateTime.Text = model.CreateTime.ToString();
            this.lblClicks.Text = model.Clicks.ToString();
            this.lblIsRead.Text = model.IsRead ? "是" : "否";
            this.lblUpDT.Text = model.UpDT.ToString();
            this.lblUpUser.Text = model.UpUser.ToString();
            //this.lblStatus.Text = model.Status ? "是" : "否";
        }
    }
}
