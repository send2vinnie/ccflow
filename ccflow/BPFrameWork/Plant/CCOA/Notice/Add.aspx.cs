﻿using System;
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
namespace Lizard.OA.Web.OA_Notice
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtNoticeTitle.Text.Trim().Length == 0)
            {
                strErr += "通告标题不能为空！\\n";
            }
            if (this.txtNoticeSubTitle.Text.Trim().Length == 0)
            {
                strErr += "副标题不能为空！\\n";
            }
            if (this.txtNoticeType.Text.Trim().Length == 0)
            {
                strErr += "通告类型不能为空！\\n";
            }
            if (this.txtNoticeContent.Text.Trim().Length == 0)
            {
                strErr += "通告内容不能为空！\\n";
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
            string NoticeId = Guid.NewGuid().ToString();
            string NoticeTitle = this.txtNoticeTitle.Text;
            string NoticeSubTitle = this.txtNoticeSubTitle.Text;
            string NoticeType = this.txtNoticeType.Text;
            string NoticeContent = this.txtNoticeContent.Text;
            string Author = this.txtAuthor.Text;
            DateTime CreateTime = DateTime.Parse(this.txtCreateTime.Text);

            //Lizard.OA.Model.OA_Notice model = new Lizard.OA.Model.OA_Notice();
            BP.CCOA.OA_Notice model = new BP.CCOA.OA_Notice();
            model.No = NoticeId;
            model.NoticeTitle = NoticeTitle;
            model.NoticeSubTitle = NoticeSubTitle;
            model.NoticeType = NoticeType;
            model.NoticeContent = NoticeContent;
            model.Author = Author;
            model.CreateTime = DateTime.Now;
            model.Clicks = 0;
            model.IsRead = 0;
            model.UpDT = DateTime.Now;
            model.UpUser = BP.Web.WebUser.No;
            model.Status = 1;

            model.Insert();
            //BP.CCOA.OA_Notice bll = new BP.CCOA.OA_Notice();
            //bll.Add(model);
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
