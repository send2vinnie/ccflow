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

public partial class CCOA_Email_Modify : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                string EmailId = Request.Params["id"];
                ShowInfo(EmailId);
            }
        }
    }

    private void ShowInfo(string EmailId)
    {
        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email(EmailId);
        this.lblEmailId.Text = model.No;
        this.txtSubject.Text = model.Subject;
        this.txtAddresser.Text = model.Addresser;
        this.txtAddressee.Text = model.Addressee;
        this.txtContent.Text = model.Content;
        this.txtPriorityLevel.Text = model.PriorityLevel;
        this.txtCreateTime.Text = model.CreateTime.ToString();
        this.txtSendTime.Text = model.SendTime.ToString();
    }

    public void btnSave_Click(object sender, EventArgs e)
    {

        string strErr = "";
        if (this.txtSubject.Text.Trim().Length == 0)
        {
            strErr += "主题不能为空！\\n";
        }
        if (this.txtAddresser.Text.Trim().Length == 0)
        {
            strErr += "发件人不能为空！\\n";
        }
        if (this.txtAddressee.Text.Trim().Length == 0)
        {
            strErr += "收件人不能为空！\\n";
        }
        if (this.txtContent.Text.Trim().Length == 0)
        {
            strErr += "邮件内容不能为空！\\n";
        }
        if (this.txtPriorityLevel.Text.Trim().Length == 0)
        {
            strErr += "类型：0-普通1-重要2-紧急不能为空！\\n";
        }
        
        if (!PageValidate.IsDateTime(txtCreateTime.Text))
        {
            strErr += "创建时间格式错误！\\n";
        }
        if (!PageValidate.IsDateTime(txtSendTime.Text))
        {
            strErr += "发送时间格式错误！\\n";
        }
       

        if (strErr != "")
        {
            MessageBox.Show(this, strErr);
            return;
        }
        string EmailId = this.lblEmailId.Text;
        string Subject = this.txtSubject.Text;
        string Addresser = this.txtAddresser.Text;
        string Addressee = this.txtAddressee.Text;
        string Content = this.txtContent.Text;
        string PriorityLevel = this.txtPriorityLevel.Text;
        string SendTime = this.txtSendTime.Text;

        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email(EmailId);
       
        model.Subject = Subject;
        model.Addresser = Addresser;
        model.Addressee = Addressee;
        model.Content = Content;
        model.PriorityLevel = PriorityLevel;
        model.SendTime = SendTime;
        model.UpDT = XTool.Now();

        model.Update();
        Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "Inbox.aspx");

    }

    public void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }
}
