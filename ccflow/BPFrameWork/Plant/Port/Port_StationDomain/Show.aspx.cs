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
namespace BP.EIP.Web.Port_StationDomain
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
					string No= strid;
					ShowInfo(No);
				}
			}
		}
		
	private void ShowInfo(string No)
	{
		BP.EIP.BLL.Port_StationDomain bll=new BP.EIP.BLL.Port_StationDomain();
		BP.EIP.Model.Port_StationDomain model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.lblFK_Station.Text=model.FK_Station;
		this.lblFK_Domain.Text=model.FK_Domain;

	}


    }
}
