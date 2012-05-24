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
using Lizard.Common;
using LTP.Accounts.Bus;
namespace BP.EIP.Web.Port_EmpStation
{
    public partial class Modify : Page
    {       

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

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.lblNo.Text;
			string FK_Emp=this.lblFK_Emp.Text;
			string FK_Station=this.lblFK_Station.Text;


			BP.EIP.Model.Port_EmpStation model=new BP.EIP.Model.Port_EmpStation();
			model.No=No;
			model.FK_Emp=FK_Emp;
			model.FK_Station=FK_Station;

			BP.EIP.BLL.Port_EmpStation bll=new BP.EIP.BLL.Port_EmpStation();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
