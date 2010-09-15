using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.DA;
using BP.En;
using BP.Web;

public partial class FAQ_ZJList : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.AddTable("style='width:95%;margin:5px auto;'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("序");

        this.Pub1.AddTDTitle("章");
        this.Pub1.AddTDTitle("节");
        this.Pub1.AddTDTitle("单个共享");

        this.Pub1.AddTREnd();

        ZhangJies ens = new ZhangJies();
        
        ens.Retrieve(ZhangJieAttr.FK_Work, EduUser.CurrWorkStr, ZhangJieAttr.GradeNo);


        bool flag = false;
        ZhangJie zhang = new ZhangJie();
        int i = 0;
        foreach (ZhangJie en in ens)
        {
            if (en.GradeNo.Length == 2)
            {
                zhang = en;
                flag = true;
                continue;
            }
            this.Pub1.AddTRTX();
            i++;
            this.Pub1.AddTD(i);

            if (flag)
            {
                this.Pub1.AddTD(zhang.Name);
                flag = false;
            }
            else
                this.Pub1.AddTD("");

            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTD("width=10% align=center","<a href=\"javascript:DoShare('"+ en.MyPK + "')\" >请求资源</a>");

            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
}
