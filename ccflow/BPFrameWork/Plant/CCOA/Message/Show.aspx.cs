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
namespace Lizard.OA.Web.OA_Message
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
					int MessageId=(Convert.ToInt32(strid));
					ShowInfo(MessageId);
				}
			}
		}
		
	private void ShowInfo(int MessageId)
	{
		BP.CCOA.OA_Message bll=new BP.CCOA.OA_Message();
		Lizard.OA.Model.OA_Message model=bll.GetModel(MessageId);
		this.lblMessageId.Text=model.MessageId.ToString();
		this.lblMessageName.Text=model.MessageName;
		this.lblMeaageType.Text=model.MeaageType.ToString();
		this.lblAuthor.Text=model.Author.ToString();
		this.lblCreateTime.Text=model.CreateTime.ToString();
		this.lblUpDT.Text=model.UpDT.ToString();
		this.lblStatus.Text=model.Status?"是":"否";

	}


    }
}
