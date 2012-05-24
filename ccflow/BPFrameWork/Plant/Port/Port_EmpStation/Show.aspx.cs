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
namespace BP.EIP.Web.Port_EmpStation
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				string No = "";
				if (Request.Params["id0"] != null && Request.Params["id0"].Trim() != "")
				{
					No= Request.Params["id0"];
				}
				string FK_Emp = "";
				if (Request.Params["id1"] != null && Request.Params["id1"].Trim() != "")
				{
					FK_Emp= Request.Params["id1"];
				}
				string FK_Station = "";
				if (Request.Params["id2"] != null && Request.Params["id2"].Trim() != "")
				{
					FK_Station= Request.Params["id2"];
				}
				#warning 代码生成提示：显示页面,请检查确认该语句是否正确
				ShowInfo(No,FK_Emp,FK_Station);
			}
		}
		
	private void ShowInfo(string No,string FK_Emp,string FK_Station)
	{
		BP.EIP.BLL.Port_EmpStation bll=new BP.EIP.BLL.Port_EmpStation();
		BP.EIP.Model.Port_EmpStation model=bll.GetModel(No,FK_Emp,FK_Station);
		this.lblNo.Text=model.No;
		this.lblFK_Emp.Text=model.FK_Emp;
		this.lblFK_Station.Text=model.FK_Station;

	}


    }
}
