using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using BP.CCOA.Enum;
using BP.EIP;

public partial class CCOA_Email_Delete : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        string emailType = Request.QueryString["EmailType"];
        string currentUser = CurrentUser.No;
        currentUser = "wss";
        if (!string.IsNullOrEmpty(idList))
        {
            BP.CCOA.OA_Email OA_Email = new OA_Email();
            switch ((MailCategory)int.Parse(emailType))
            {
                case MailCategory.InBox:
                    //收件箱删除到垃圾箱
                    OA_Email.DeleteFromInputBox(idList, currentUser);
                    break;
                case MailCategory.DraftBox:
                case MailCategory.OutBox:
                    XDeleteTool.DeleteByIds(OA_Email, idList);
                    break;
                case MailCategory.RecycleBox:
                    OA_Email.DeleteFromRecycleBox(idList, currentUser);
                    break;
            }
            //XDeleteTool.DeleteByIds(OA_Email, idList);
        }
    }
}