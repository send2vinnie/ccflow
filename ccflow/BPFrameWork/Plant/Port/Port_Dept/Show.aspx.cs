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
namespace BP.EIP.Web.Port_Dept
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
            BP.EIP.Port_Dept model = new EIP.Port_Dept(No);
            this.lblNo.Text = model.No;
            this.lblName.Text = model.Name;
            this.lblFullName.Text = model.FullName;
            this.lblPid.Text = model.Pid;
            this.lblStatus.Text = model.Status.ToString();
        }


    }
}
