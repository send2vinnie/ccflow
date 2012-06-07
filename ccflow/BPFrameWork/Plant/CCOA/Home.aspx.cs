using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using BP.Port;
using BP.EIP;

public partial class CCOA_Home : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CurrentApp"] = "OA";
            this.CurrentUser = new CurrentUser();
            //Channel channel = new Channel();
            //channel.CheckPhysicsTable();

            //Article article = new Article();
            //article.CheckPhysicsTable();

            //ArticleType type = new ArticleType();
            //type.CheckPhysicsTable();

            ////EmpInfo empinfo = new EmpInfo();
            ////empinfo.CheckPhysicsTable();

            //AddrBook ab = new AddrBook();
            //ab.CheckPhysicsTable();

            //AddrBookDept abd = new AddrBookDept();
            //abd.CheckPhysicsTable();

            //BP.GPM.Menu menu = new BP.GPM.Menu();
            //menu.CheckPhysicsTable();

            //BP.CCOA.APP_ChangeLog cl = new APP_ChangeLog();
            //cl.CheckPhysicsTable();

            //BP.CCOA.APP_Domain domain = new APP_Domain();
            //domain.CheckPhysicsTable();

            //BP.CCOA.APP_Function f = new APP_Function();
            //f.CheckPhysicsTable();

            //BP.CCOA.APP_Role r = new APP_Role();
            //r.CheckPhysicsTable();

            //BP.CCOA.APP_Rule rule = new APP_Rule();
            //rule.CheckPhysicsTable();

            //BP.CCOA.APP_User user = new APP_User();
            //user.CheckPhysicsTable();

            //BP.CCOA.EIP_Emp emp = new EIP_Emp();
            //emp.CheckPhysicsTable();
            //new OA_News().CheckPhysicsTable();
            //new OA_Notice().CheckPhysicsTable();

            //BP.CCOA.OA_AddrBook ad = new OA_AddrBook();
            //ad.CheckPhysicsTable();

            //BP.CCOA.OA_AddrGrouping ag = new OA_AddrGrouping();
            //ag.CheckPhysicsTable();

            //BP.CCOA.OA_Attachment oa = new OA_Attachment();
            //oa.CheckPhysicsTable();

            //BP.CCOA.OA_Channel oa_channel = new OA_Channel();
            //oa_channel.CheckPhysicsTable();

            //BP.CCOA.OA_Meeting me = new OA_Meeting();
            //me.CheckPhysicsTable();

            //BP.CCOA.OA_Message message = new OA_Message();
            //message.CheckPhysicsTable();

            //BP.CCOA.OA_SMS sms = new OA_SMS();
            //sms.CheckPhysicsTable();

            //new BP.CCOA.EIP_Layout().CheckPhysicsTable();
            //new BP.CCOA.EIP_LayoutDetail().CheckPhysicsTable();

            //new BP.CCOA.OA_NewsAttach().CheckPhysicsTable();

            //new BP.CCOA.OA_ClickRecords().CheckPhysicsTable();
            //new BP.CCOA.OA_NoticeAuth().CheckPhysicsTable();
            //new BP.CCOA.OA_Notice().CheckPhysicsTable();
            new BP.CCOA.OA_NewsAuth().CheckPhysicsTable();
        }
    }
}