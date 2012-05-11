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
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string NewsId= Request.Params["id"];
					ShowInfo(NewsId);
				}
			}
		}
			
	private void ShowInfo(string NewsId)
	{
		BP.CCOA.OA_News model=new BP.CCOA.OA_News(NewsId);

        this.lblNewsId.Text = model.No;
		this.txtNewsTitle.Text=model.NewsTitle;
		this.txtNewsSubTitle.Text=model.NewsSubTitle;
		this.txtNewsType.Text=model.NewsType;
		this.txtNewsContent.Text=model.NewsContent;
		this.txtAuthor.Text=model.Author;
		this.chkStatus.Checked=Convert.ToBoolean(model.Status);
	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtNewsTitle.Text.Trim().Length==0)
			{
				strErr+="新闻标题不能为空！\\n";	
			}
			if(this.txtNewsSubTitle.Text.Trim().Length==0)
			{
				strErr+="副标题不能为空！\\n";	
			}
			if(this.txtNewsType.Text.Trim().Length==0)
			{
				strErr+="新闻类型不能为空！\\n";	
			}
			if(this.txtNewsContent.Text.Trim().Length==0)
			{
				strErr+="新闻内容不能为空！\\n";	
			}
			if(this.txtAuthor.Text.Trim().Length==0)
			{
				strErr+="发布人不能为空！\\n";	
			}
            //if(!PageValidate.IsDateTime(txtCreateTime.Text))
            //{
            //    strErr+="发布时间格式错误！\\n";	
            //}
            //if(!PageValidate.IsNumber(txtClicks.Text))
            //{
            //    strErr+="点击量格式错误！\\n";	
            //}
            //if(!PageValidate.IsDateTime(txtUpDT.Text))
            //{
            //    strErr+="更新时间格式错误！\\n";	
            //}
            //if(!PageValidate.IsNumber(txtUpUser.Text))
            //{
            //    strErr+="更新人格式错误！\\n";	
            //}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string NewsId=this.lblNewsId.Text;
			string NewsTitle=this.txtNewsTitle.Text;
			string NewsSubTitle=this.txtNewsSubTitle.Text;
			string NewsType=this.txtNewsType.Text;
			string NewsContent=this.txtNewsContent.Text;
			string Author=this.txtAuthor.Text;
            //DateTime CreateTime=DateTime.Parse(this.txtCreateTime.Text);
            //int Clicks=int.Parse(this.txtClicks.Text);
            //bool IsRead=this.chkIsRead.Checked;
            DateTime UpDT = DateTime.Now;
            string UpUser = BP.Web.WebUser.No;
			bool Status=this.chkStatus.Checked;

            BP.CCOA.OA_News model = new BP.CCOA.OA_News(NewsId);
			
			model.NewsTitle=NewsTitle;
			model.NewsSubTitle=NewsSubTitle;
			model.NewsType=NewsType;
			model.NewsContent=NewsContent;
			model.Author=Author;
			model.UpDT=UpDT;
            model.UpUser = BP.Web.WebUser.No;

            model.Update();
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
