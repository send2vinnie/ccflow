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
namespace Lizard.OA.Web.OA_AddrBook
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
					string AddrBookId= strid;
					ShowInfo(AddrBookId);
				}
			}
		}
		
	private void ShowInfo(string AddrBookId)
	{
		Lizard.OA.BLL.OA_AddrBook bll=new Lizard.OA.BLL.OA_AddrBook();
		Lizard.OA.Model.OA_AddrBook model=bll.GetModel(AddrBookId);
		this.lblAddrBookId.Text=model.AddrBookId;
		this.lblName.Text=model.Name;
		this.lblNickName.Text=model.NickName;
		this.lblSex.Text=model.Sex?"是":"否";
		this.lblBirthday.Text=model.Birthday.ToString();
		this.lblEmail.Text=model.Email;
		this.lblMobile.Text=model.Mobile;
		this.lblQQ.Text=model.QQ;
		this.lblWorkUnit.Text=model.WorkUnit;
		this.lblWorkPhone.Text=model.WorkPhone;
		this.lblWorkAddress.Text=model.WorkAddress;
		this.lblHomePhone.Text=model.HomePhone;
		this.lblHomeAddress.Text=model.HomeAddress;
		this.lblGrouping.Text=model.Grouping.ToString();
		this.lblStatus.Text=model.Status?"是":"否";
		this.lblRemarks.Text=model.Remarks;

	}


    }
}
