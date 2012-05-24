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
namespace BP.EIP.Web.Port_FunctionOperate
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
                    strid = Request.Params["id"];
                    string No = strid;
                    ShowInfo(No);
                }
            }
        }

        private void ShowInfo(string No)
        {
            BP.EIP.Port_FunctionOperate model = new EIP.Port_FunctionOperate(No);
            this.lblNo.Text = model.No;
            this.lblFK_Function.Text = model.FK_Function;
            this.lblOperateName.Text = model.OperateName;
            this.lblOperateDesc.Text = model.OperateDesc;
            this.lblControl_Name.Text = model.Control_Name;

        }


    }
}
