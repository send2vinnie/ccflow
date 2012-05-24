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
namespace BP.EIP.Web.Port_Menu
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
            BP.EIP.Port_Menu model = new EIP.Port_Menu(No);
            this.lblNo.Text = model.No;
            this.lblMenuNo.Text = model.MenuNo;
            this.lblPid.Text = model.Pid;
            this.lblFK_Function.Text = model.FK_Function;
            this.lblMenuName.Text = model.MenuName;
            this.lblTitle.Text = model.Title;
            this.lblImg.Text = model.Img;
            this.lblUrl.Text = model.Url;
            this.lblPath.Text = model.Path;
            this.lblStatus.Text = model.Status.ToString();

        }


    }
}
