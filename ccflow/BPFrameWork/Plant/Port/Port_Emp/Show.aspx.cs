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
namespace BP.EIP.Web.Port_Emp
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
		BP.EIP.BLL.Port_Emp bll=new BP.EIP.BLL.Port_Emp();
		BP.EIP.Model.Port_Emp model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.lblName.Text=model.Name;
		this.lblPass.Text=model.Pass;
		this.lblFK_Dept.Text=model.FK_Dept;
		this.lblPID.Text=model.PID;
		this.lblPIN.Text=model.PIN;
		this.lblKeyPass.Text=model.KeyPass;
		this.lblIsUSBKEY.Text=model.IsUSBKEY;
		this.lblFK_Emp.Text=model.FK_Emp;
		this.lblStatus.Text=model.Status?"是":"否";

	}


    }
}
