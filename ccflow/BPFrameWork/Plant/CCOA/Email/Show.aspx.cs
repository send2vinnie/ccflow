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

public partial class CCOA_Email_Show : BasePage
{
    public string strid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                strid = Request.Params["id"];
                string EmailId = strid;
                ShowInfo(EmailId);
            }
        }
    }

    private void ShowInfo(string EmailId)
    {
        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email(EmailId);

        this.lblEmailId.Text = model.No;
        this.lblSubject.Text = model.Subject;
        this.lblAddresser.Text = model.Addresser;
        this.lblAddressee.Text = model.Addressee;
        this.lblContent.Text = model.Content;
        this.lblPriorityLevel.Text = model.PriorityLevel;
        this.lblCategory.Text = model.Category;
        this.lblCreateTime.Text = model.CreateTime.ToString();
        this.lblSendTime.Text = model.SendTime.ToString();
        this.lblUpDT.Text = model.UpDT.ToString();
    }
}

