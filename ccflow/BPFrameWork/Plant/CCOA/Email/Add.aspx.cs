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

public partial class CCOA_Email_Add : BasePage
{
    private string m_Category = "2";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string currentUser = BP.Web.WebUser.Name;
            currentUser = "wss";
            this.txtAddresser.Text = currentUser;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }

    private void Save()
    {
        string strErr = "";

        if (this.txtSubject.Text.Trim().Length == 0)
        {
            strErr += "主题不能为空！\\n";
        }
        //if (this.txtSelectedIds.Value.ToString() == string.Empty)
        //{
        //    strErr += "收件人不能为空！\\n";
        //}
        if (this.txtContent.Text.Trim().Length == 0)
        {
            strErr += "邮件内容不能为空！\\n";
        }
        if (this.chklstPriorityLevel.Text.Trim().Length == 0)
        {
            strErr += "类型：0-普通1-重要2-紧急不能为空！\\n";
        }

        if (strErr != "")
        {
            MessageBox.Show(this, strErr);
            return;
        }
        string EmailId = Guid.NewGuid().ToString();
        string Subject = this.txtSubject.Text;
        string Addresser = CurrentUser.No;
        string Addressee = this.txtAddressee.Text;
        string Content = this.txtContent.Text;
        string PriorityLevel = this.chklstPriorityLevel.SelectedValue;
        //string Category = "2";//发件箱
        string CreateTime = XTool.Now();
        string SendTime = XTool.Now();
        string UpDT = XTool.Now();

        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email();
        model.No = EmailId;
        model.Subject = Subject;
        model.Addresser = Addresser;
        model.Addressee = Addressee;
        model.Content = Content;
        model.PriorityLevel = PriorityLevel;
        model.Category = this.m_Category;
        model.CreateTime = CreateTime;
        model.SendTime = SendTime;
        model.IsDel = 0;
        model.UpDT = UpDT;

        model.Insert();

        //保存附件
        if (this.FileUpload1.HasFile)
        {
            string fileName = this.FileUpload1.FileName;
            string saveFilePath = XAttachmentTool.UploadFile(this.FileUpload1, Request.PhysicalApplicationPath, this, Server.HtmlEncode(fileName));
            string attachNo = string.Empty;
            XAttachmentTool.InsertAttachment(fileName, fileName, saveFilePath, CurrentUser.No, out attachNo);

            //插入公告附件表
            BP.CCOA.OA_EmailAttach emailAttach = new BP.CCOA.OA_EmailAttach();
            emailAttach.No = Guid.NewGuid().ToString();
            emailAttach.Email_Id = EmailId;
            emailAttach.Attachment_Id = attachNo;
            emailAttach.Insert();
        }

        //插入接收人
        XEmailTool.InsertEmailAuths(EmailId, this.txtSelectedIds.Value);

        //BP.CCOA.OA_Email bll = new BP.CCOA.OA_Email();
        //bll.Add(model);
        Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");
    }

    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        m_Category = "1";
        Save();
    }

    public void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inbox.aspx");
    }
}

