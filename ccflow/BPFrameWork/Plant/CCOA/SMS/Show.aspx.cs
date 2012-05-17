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
namespace Lizard.OA.Web.OA_SMS
{
    public partial class Show : BasePage
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    string SmsId = strid;
                    ShowInfo(SmsId);
                }
            }
        }

        private void ShowInfo(string SmsId)
        {
            //BP.CCOA.OA_SMS bll=new BP.CCOA.OA_SMS();
            //Lizard.OA.Model.OA_SMS model=bll.GetModel(SmsId);
            BP.CCOA.OA_SMS model = new BP.CCOA.OA_SMS(SmsId);
            this.lblSmsId.Text = model.No;
            this.lblSenderNumber.Text = model.SenderNumber;
            this.lblReciveNumber.Text = model.ReciveNumber;
            this.lblSendContent.Text = model.SendContent;
            this.lblReciveConent.Text = model.ReciveConent;
            this.lblSendTime.Text = model.SendTime.ToString();
            this.lblReciveTime.Text = model.ReciveTime.ToString();

        }


    }
}
