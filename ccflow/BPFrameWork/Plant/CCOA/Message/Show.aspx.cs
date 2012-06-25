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
using BP.CCOA;
using BP.CCOA.Enum;
namespace Lizard.OA.Web.OA_Message
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
                    ShowInfo(strid);
                    AddClicks(strid);
                }
            }
        }

        private void AddClicks(string messageId)
        {
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(messageId);
            model.Clicks = model.Clicks + 1;
            model.Update();

            ClickHelper.ClickRecord(ClickObjType.Message, strid, CurrentUser.No);
        }

        private void ShowInfo(string MessageId)
        {
            //BP.CCOA.OA_Message bll=new BP.CCOA.OA_Message();
            //Lizard.OA.Model.OA_Message model=bll.GetModel(MessageId);
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            this.Label1.Text = model.Author;
            this.Label2.Text = model.UpDT;
            this.lblMessageTitle.Text = model.MessageName;
            this.lblMessageName.Text = model.MessageName;
            this.lblMeaageType.Text = model.MeaageType.ToString();
            this.lblAuthor.Text = model.Author.ToString();
            this.lblCreateTime.Text = model.CreateTime.ToString();
            this.lblMessageContent.Text = model.MessageContent;
        }


    }
}
