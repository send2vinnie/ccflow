using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Lizard.Common;
using LTP.Accounts.Bus;
namespace Lizard.OA.Web.OA_News
{
    public partial class Add : BP.Web.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropDownList();
            }
        }

        private void BindDropDownList()
        {
            BP.CCOA.OA_Categorys list = new BP.CCOA.OA_Categorys();
            list.RetrieveByAttr("Type","0");
            this.ddlNewsType.DataSource = list;
            this.ddlNewsType.DataTextField = "CategoryName";
            this.ddlNewsType.DataValueField = "No";
            this.ddlNewsType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.txtNewsTitle.Text.Trim().Length == 0)
            {
                strErr += "新闻标题不能为空！\\n";
            }
           
            if (this.ddlNewsType.SelectedValue.Trim().Length == 0)
            {
                strErr += "新闻类型不能为空！\\n";
            }
            if (this.xtxtReader.Text.Trim().Length == 0)
            {
                strErr += "发布对象不能为空！\\n";
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
            string NewsType = this.ddlNewsType.SelectedValue;
            string NewsContent = this.txtNewsContent.Text;
            string Author = this.txtAuthor.Text;
            //int Clicks = int.Parse(this.txtClicks.Text);
            bool IsRead = false;
            //DateTime UpDT = XTool.Now();
            string UpUser = BP.Web.WebUser.No;
            bool Status = true;
            string AccessType = this.hfAccessType.Value;
            if (AccessType=="dept")
            {
                AccessType = "部门";
            }
            if (AccessType == "user")
            {
                AccessType = "用户";
            }
            if (AccessType == "role")
            {
                AccessType = "角色";
            }

            BP.CCOA.OA_News model = new BP.CCOA.OA_News();
            model.No = NewsId;
            model.NewsTitle = NewsTitle;
            model.NewsSubTitle = NewsSubTitle;
            model.NewsType = NewsType;
            model.NewsContent = NewsContent;
            model.Author = Author;
            model.CreateTime = XTool.Now();
            model.Clicks = 0;
            model.IsRead = IsRead;
            model.UpDT = XTool.Now();
            model.UpUser = UpUser;
            model.Status = Status ? 1 : 0;
            model.AccessType = AccessType;
            model.Insert();

            string selectIds = hfSelects.Value;
            if (selectIds.Length>0)
            {
                string[] ids = selectIds.Split(',');
                foreach (string id in ids)
                {
                    BP.CCOA.OA_NewsAuth auth = new BP.CCOA.OA_NewsAuth();
                    auth.No = Guid.NewGuid().ToString();
                    auth.FK_News = model.No;
                    auth.FK_Id = id;
                    auth.Insert();
                }
            }

            if (FileUpload1.HasFile)
            {
                //先上传，再保存
                string uploadFile = UploadFile();

                BP.CCOA.OA_Attachment attachment = new BP.CCOA.OA_Attachment();
                attachment.No = Guid.NewGuid().ToString();
                attachment.AttachmentName = FileUpload1.FileName;
                attachment.FileNeme = FileUpload1.FileName;
                attachment.FilePath = uploadFile;
                attachment.CreateTime = DateTime.Now;
                attachment.Suffix = Path.GetExtension(FileUpload1.FileName);
                attachment.Uploader = BP.Web.WebUser.No;
                attachment.Remarks = "";
                attachment.Insert();

                BP.CCOA.OA_NewsAttach na = new BP.CCOA.OA_NewsAttach();
                na.No = Guid.NewGuid().ToString();
                na.News_Id = model.No;
                na.Attachment_Id = attachment.No;
                na.Insert();
            }

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //UploadFile();
        }

        private string UploadFile()
        {
            string[] extension = new string[] { ".doc", ".docx", ".txt", ".png", ".jpg", ".bmp", ".pdf", ".xml", ".dwg", ".ppt" };
            string fileName = FileUpload1.FileName;
            string ext = Path.GetExtension(fileName);
            if (!extension.Contains(ext))
            {
                Lizard.Common.MessageBox.Show(this, "文件类型错误");
                //this.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + "文件类型错误" + "');</script>");
                return "";
            }
            if (FileUpload1.HasFile)
            {
                string saveDir = @"CCOA\Upload\uploadfiles\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                string appPath = Request.PhysicalApplicationPath;
                string path = appPath + saveDir;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string savefile = DateTime.Now.ToFileTimeUtc().ToString() + ext;
                string savePath = path + Server.HtmlEncode(savefile);

                FileUpload1.SaveAs(savePath);

                return savePath;
            }
            else
            {
                return "";
            }
        }
    }
}
