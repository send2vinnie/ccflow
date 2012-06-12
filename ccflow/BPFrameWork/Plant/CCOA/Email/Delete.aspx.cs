using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using BP.CCOA.Enum;

public partial class CCOA_Email_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        string emailType = Request.QueryString["EmailType"];

        if (!string.IsNullOrEmpty(idList))
        {
            BP.CCOA.OA_Email OA_Email = new OA_Email();
            switch ((MailCategory)int.Parse(emailType))
            {
                case MailCategory.InBox:
                    //收件箱删除到垃圾箱
                    OA_Email.DeleteFromInputBox(idList);
                    break;
                case MailCategory.DraftBox:
                case MailCategory.OutBox:
                case MailCategory.RecycleBox:
                    XDeleteTool.DeleteByIds(OA_Email, idList);
                    break;
            }
            //XDeleteTool.DeleteByIds(OA_Email, idList);
        }
    }
}