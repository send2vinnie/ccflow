using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;    
using System.Web.UI.WebControls;
using BP.GPM;
using BP.Web;

public partial class SSO_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        STems ens = new STems();
        ens.RetrieveAll();  

        this.Pub1.AddTable();
        foreach (STem en in ens)
        {
            if (en.IsEnable == false)
                continue;

            if (en.No == "SSO"  )
                continue;
          

            this.Pub1.AddTR();
            this.Pub1.AddTD("<a href='"+en.Url+"' target="+en.OpenWay+" ><img src='"+en.ICON+"' border=0 >"+en.Name+"</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        this.Pub1.AddBR();
        this.Pub1.AddBR();

        this.Pub1.Add("<a href='STemSettingPage.aspx'>单点登陆密码设置 </a>");
        this.Pub1.AddBR();
        this.Pub1.Add("<a href='STemSettingPage.aspx'>密码修改</a>");
        this.Pub1.AddBR();
        this.Pub1.Add("<a href='Default.aspx?DoType=Setting'>信息块定义</a>");



    }
}
