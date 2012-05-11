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
namespace Lizard.OA.Web.OA_News
{
    public partial class Add : BP.Web.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
           
            if (this.txtNewsTitle.Text.Trim().Length == 0)
            {
                strErr += "新闻标题不能为空！\\n";
            }
            if (this.txtNewsSubTitle.Text.Trim().Length == 0)
            {
                strErr += "副标题不能为空！\\n";
            }
            if (this.txtNewsType.Text.Trim().Length == 0)
            {
                strErr += "新闻类型不能为空！\\n";
            }
            if (this.txtNewsContent.Text.Trim().Length == 0)
            {
                strErr += "新闻内容不能为空！\\n";
            }
            if (this.txtAuthor.Text.Trim().Length == 0)
            {
                strErr += "发布人不能为空！\\n";
            }
            if (!PageValidate.IsDateTime(txtCreateTime.Text))
            {
                strErr += "发布时间格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string NewsId = Guid.NewGuid().ToString();
            string NewsTitle = this.txtNewsTitle.Text;
            string NewsSubTitle = this.txtNewsSubTitle.Text;
            string NewsType = this.txtNewsType.Text;
            string NewsContent = this.txtNewsContent.Text;
            string Author = this.txtAuthor.Text;
            //int Clicks = int.Parse(this.txtClicks.Text);
            bool IsRead = false;
            DateTime UpDT = DateTime.Now;
            string UpUser = BP.Web.WebUser.No;
            bool Status = true;

            BP.CCOA.OA_News model = new BP.CCOA.OA_News();
            model.No = NewsId;
            model.NewsTitle = NewsTitle;
            model.NewsSubTitle = NewsSubTitle;
            model.NewsType = NewsType;
            model.NewsContent = NewsContent;
            model.Author = Author;
            model.CreateTime = DateTime.Now;
            model.Clicks = 0;
            model.IsRead = IsRead;
            model.UpDT = UpDT;
            model.UpUser = UpUser;
            model.Status = Status ? 1 : 0;

            //BP.CCOA.OA_News bll = new BP.CCOA.OA_News();
            //bll.Add(model);
            model.Insert();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
