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

    private BP.CCOA.OA_Email m_OAEmail = new BP.CCOA.OA_Email();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                strid = Request.Params["id"];
                string EmailId = strid;
                ShowInfo(EmailId);
                //BP.CCOA.OA_Email email = new BP.CCOA.OA_Email();
                //email.SetReaded(EmailId);
                this.SetRead(strid);
            }
        }
    }

    private void SetRead(string emailId)
    {
        BP.CCOA.OA_ClickRecords clickRecord = new BP.CCOA.OA_ClickRecords();
        clickRecord.No = Guid.NewGuid().ToString();
        clickRecord.VisitId = CurrentUser.No;
        clickRecord.VisitDate = XTool.Now();
        clickRecord.ObjectId = emailId;
        clickRecord.Clicks = 1;
        BP.CCOA.Enum.ClickObjType clickObjType = BP.CCOA.Enum.ClickObjType.Email;
        clickRecord.ObjectType = (int)clickObjType;
        clickRecord.Save();
    }

    private void ShowInfo(string EmailId)
    {
        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email(EmailId);

        this.lblEmailId.Text = model.No;
        this.lblSubject.Text = model.Subject;
        this.lblAddresser.Text = model.Addresser;
        //收件人
        //this.lblAddressee.Text = model.Addressee;
        this.lblAddressee.Text = this.m_OAEmail.GetEmailReceiver(EmailId);
        this.lblContent.Text = model.Content;
        this.lblPriorityLevel.Text = model.PriorityLevel;
        this.lblCategory.Text = model.Category;
        this.lblCreateTime.Text = model.CreateTime.ToString();
        this.lblSendTime.Text = model.SendTime.ToString();
        this.lblUpDT.Text = model.UpDT.ToString();

        if (model.Category == "1")
        {
            this.btnSave.Visible = true;
        }
        else
        {
            this.btnSave.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email(Request.Params["id"]);
        model.Category = "2";
        if (model.Update() > 0)
        {
            Lizard.Common.MessageBox.ShowAndRedirect(this, "发送成功！", "DraftBox.aspx");
        }
    }
}

