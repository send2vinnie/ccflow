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
namespace BP.EIP.Web.PORT_STAFF
{
    public partial class Show : Page
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string No = Request.Params["id"];
                    ShowInfo(No);
                }
            }
        }

        private void ShowInfo(string No)
        {
            //BP.EIP.BLL.PORT_STAFF bll = new BP.EIP.BLL.PORT_STAFF();
            //BP.EIP.Model.PORT_STAFF model = bll.GetModel();
            BP.EIP.Port_Staff model = new Port_Staff(No);
            this.lblNO.Text = model.No;
            this.lblEMPNO.Text = model.EmpNo;
            this.lblAGE.Text = model.Age.ToString();
            this.lblIDCARD.Text = model.IDCard;
            this.lblPHONE.Text = model.Phone;
            this.lblEMAIL.Text = model.Email;
            this.lblUPUSER.Text = model.UpUser;
            this.lblFK_DEPT.Text = model.Fk_Dept;
            this.lblEMPNAME.Text = model.EmpName;
            this.lblSEX.Text = model.Sex.ToString();
            this.lblBIRTHDAY.Text = model.Birthday;
            this.lblADDRESS.Text = model.Address;
            this.lblCREATEDATE.Text = model.CreateDate;
        }
    }
}
