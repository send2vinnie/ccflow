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
namespace BP.EIP.Web.Port_StationDomain
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtNo.Text.Trim().Length==0)
			{
				strErr+="No不能为空！\\n";	
			}
			if(this.txtFK_Station.Text.Trim().Length==0)
			{
				strErr+="FK_Station不能为空！\\n";	
			}
			if(this.txtFK_Domain.Text.Trim().Length==0)
			{
				strErr+="FK_Domain不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.txtNo.Text;
			string FK_Station=this.txtFK_Station.Text;
			string FK_Domain=this.txtFK_Domain.Text;

			BP.EIP.Model.Port_StationDomain model=new BP.EIP.Model.Port_StationDomain();
			model.No=No;
			model.FK_Station=FK_Station;
			model.FK_Domain=FK_Domain;

			BP.EIP.BLL.Port_StationDomain bll=new BP.EIP.BLL.Port_StationDomain();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
