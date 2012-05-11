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

public partial class CCOA_Email_Add : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
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
        if (this.txtCategory.Text.Trim().Length == 0)
        {
            strErr += "分类：0-收件箱1-草稿箱2-不能为空！\\n";
        }

        if (strErr != "")
        {
            MessageBox.Show(this, strErr);
            return;
        }
        string EmailId = Guid.NewGuid().ToString();
        string Subject = this.txtSubject.Text;
        string Addresser = this.txtAddresser.Text;
        string Addressee = this.txtAddressee.Text;
        string Content = this.txtContent.Text;
        string PriorityLevel = this.txtPriorityLevel.Text;
        string Category = this.txtCategory.Text;
        DateTime CreateTime = DateTime.Now;
        DateTime SendTime = DateTime.Now;
        DateTime UpDT = DateTime.Now;

        BP.CCOA.OA_Email model = new BP.CCOA.OA_Email();
        model.No = EmailId;
        model.Subject = Subject;
        model.Addresser = Addresser;
        model.Addressee = Addressee;
        model.Content = Content;
        model.PriorityLevel = PriorityLevel;
        model.Category = Category;
        model.CreateTime = CreateTime;
        model.SendTime = SendTime;
        model.IsDel = 0;
        model.UpDT = UpDT;

        model.Insert();
        //BP.CCOA.OA_Email bll = new BP.CCOA.OA_Email();
        //bll.Add(model);
        Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

    }

    public void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("list.aspx");
    }
}

