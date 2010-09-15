using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;

public partial class FAQ_ShowMsg : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "Ask":


                this.Pub1.AddDivRound();
                this.Pub1.AddB("提交成功！<br/><br/>");
                this.Pub1.AddB("您已经获得<em class='iconP'>1</em> 感谢您的贡献<br/><br/><br/><br/>");
                this.Pub1.AddB("<p class='link'> <a href=javascript:DoAsk()> 继续提问</a>&nbsp&nbsp&nbsp&nbsp");
                this.Pub1.AddB("<a href=\"javascript:DoOpen('" + this.RefOID + "')\">预览您请求的资源</a></p>");
                this.Pub1.AddDivRoundEnd();



                //this.Pub1.AddMsgInfo("成功执行", "您的问题已经提交，<li>" + "<a href=\"javascript:DoOpen('" + this.RefOID + "')\">查看提出的问题</a></li><li>" + "<a   href=javascript:window.close()>返回</a></li>");
                break;
            case "Del":
                this.Pub1.AddMsgInfo("删除成功", "您的操作已被系统记录，<li>" + "<a href='Question.aspx'>查看……</a></li><li>" + "<a href='ListNew.aspx'>返回……</a></li>");
                break;
            case "SH":


                this.Pub1.AddDivRound();
                this.Pub1.AddB("审核成功！<br/><br/>");
                this.Pub1.AddB("<br/><br/><br/><br/>");
                this.Pub1.AddB("<p class='link'> <a href=javascript:window.close()> 继续审核</a>&nbsp&nbsp&nbsp&nbsp");
                this.Pub1.AddB("<a   href=javascript:window.close()>关闭窗口</a></p>");
                this.Pub1.AddDivRoundEnd();



                //this.Pub1.AddMsgInfo("成功执行", "您的审核信息已经提交，<li>" + "<a href=\"javascript:DoSH()\">继续审核</a></li><li><a   href=javascript:window.close()>关闭窗口</a></li>");
                //this.WinClose();
                break;
            default:
                break;

        }


    }
}
