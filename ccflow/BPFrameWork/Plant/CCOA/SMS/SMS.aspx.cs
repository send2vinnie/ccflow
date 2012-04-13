using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.SMS;

public partial class CCOA_SMS_SMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string telephoneNumber = txtPhoneNumber.Text;
        string content = txtContent.Text;
        SMSMessage.Send(telephoneNumber, content, "");

        string strMsg = "发送成功！";

        ScriptManager.RegisterStartupScript(
            Page, 
            typeof(Page), 
            "SS", 
            "alert('" + strMsg + "')", 
            true);
    }
}