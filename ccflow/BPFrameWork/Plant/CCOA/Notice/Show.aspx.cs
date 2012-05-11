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
namespace Lizard.OA.Web.OA_Notice
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					string NoticeId= strid;
					ShowInfo(NoticeId);
				}
			}
		}
		
	private void ShowInfo(string NoticeId)
	{
		BP.CCOA.OA_Notice bll=new BP.CCOA.OA_Notice();
		Lizard.OA.Model.OA_Notice model=bll.GetModel(NoticeId);
		this.lblNoticeId.Text=model.NoticeId;
		this.lblNoticeTitle.Text=model.NoticeTitle;
		this.lblNoticeSubTitle.Text=model.NoticeSubTitle;
		this.lblNoticeType.Text=model.NoticeType;
		this.lblNoticeContent.Text=model.NoticeContent;
		this.lblAuthor.Text=model.Author;
		this.lblCreateTime.Text=model.CreateTime.ToString();
		this.lblClicks.Text=model.Clicks.ToString();
		this.lblIsRead.Text=model.IsRead?"是":"否";
		this.lblUpDT.Text=model.UpDT.ToString();
		this.lblUpUser.Text=model.UpUser.ToString();
		this.lblStatus.Text=model.Status?"是":"否";

	}


    }
}
