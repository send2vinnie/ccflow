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
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_Action : WebPage
{
    public int NodeID
    {
        get
        {
            return int.Parse( this.Request.QueryString["NodeID"] );
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(this.ToE("NodeAction", "事件接口"));

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("<b><a href='Action.aspx?DoType=Help&NodeID=" + this.NodeID + "' ><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("Help","帮助") + "</a></b>");
        this.Ucsys1.AddTDTitle("<b><a href='Action.aspx?DoType=WhenSave&NodeID=" + this.NodeID + "' ><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("WhenNodeSave", "当保存节点时") + "</a></b>");
        this.Ucsys1.AddTDTitle("<b><a href='Action.aspx?DoType=WhenSend&NodeID=" + this.NodeID + "' ><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("WhenNodeSend", "当发送时") + "</a></b>");
        this.Ucsys1.AddTDTitle("<b><a href='Action.aspx?DoType=WhenSendOK&NodeID=" + this.NodeID + "' ><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("WhenNodeSendOK", "当发送成功时") + "</a></b>");
        this.Ucsys1.AddTDTitle("<b><a href='Action.aspx?DoType=WhenSendError&NodeID=" + this.NodeID + "' ><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("WhenNodeSendErr", "当发送失败时") + "</a></b>");

        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.Add("<TD class=BigDoc colspan=5>");
        switch (this.DoType)
        {
            case "WhenSave":
                this.Ucsys1.Add( GetHtml("WhenSave") );
                break;
            case "WhenSend":
                this.Ucsys1.Add(GetHtml("WhenSend"));
                break;
            case "WhenSendOK":
                this.Ucsys1.Add(GetHtml("WhenSendOK"));
                break;
            case "WhenSendError":
                this.Ucsys1.Add(GetHtml("WhenSendError"));
                break;
            default:
                string s = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "/WF/Admin/Help/EditAction.htm");
                this.Ucsys1.Add(s);
                //this.Ucsys1.AddIframe("./Help/EditAction.htm");
                break;
        }

        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTable();
    }
    public string GetHtml(string flag)
    {
        string script = "";
        string proName = "ND" + this.NodeID + "_" + flag;
        //string sql = "SELECT text FROM user_source WHERE name=UPPER('" + proName + "') ORDER BY LINE ";
        string sql = "SELECT no FROM Port_Emp WHERE 1=2 ";

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
        {
            script += "下面是该事件的所要调用的存储过程模板， 如果您要在事件里编写您的业务逻辑请 Copy 到存储过程编辑器中执行。<hr>";
            script += "/* -- 存储过程<b> " + proName + "</b>  编写日期:"+BP.DA.DataType.CurrentDataTimeCN+" 编写人: xxx */ ";
            script += "<br>CREATE OR REPLACE PROCEDURE " + proName;
            script += "<br>(workid in integer,-- 工作ID ";
            script += "<br>userno varchar2, -- 当前操作员 ";
            script += "<br>workpara out varchar2 -- 参数集合。";
            script += "<br>)";
            script += "<br>AS";
            script += "<br>BEGIN";

            script += "<br>/*";
            script += "<font color=blue> <br>写给程序员:</b>  ";
            script += "在这里根据传递的参数，编写您的业务逻辑。<br>参数格式为:@属性1=属性值1@属性2=属性值2 .... ";
            script += "<br>比如：@FK_Emp=zhangsan@Age=24@Addr=山东济南@Weight=70.5";
            script += "<br>系统为您提供的函数:";
            script += "<br>获取字符串 GetValStrByKey( para, key) ，比如：GetValStrByKey(workpara,'Addr') 返回 山东济南 ";
            script += "<br>获取Int串 GetValIntByKey( para, key) ，比如：GetValIntByKey(workpara,'Age') 返回 24 ";
            script += "<br>获取Float串 GetValFloatByKey( para, key) ，比如：GetValFloatByKey(workpara,'Weight') 返回 70.5 ";

            script += "<br><br>如果您想阻止下一步的执行，您可以在这里抛出异常，这个异常信息直接抛给用户界面。";
            script += "<br></font>*/";

            script += "<br>RETURN;";
            script += "<br>END " + proName + ";";
            return script;
        }

        script =" <b>现有的存储过程内容如下，如果您想编辑它，请转到数据库执行。 </b><hr>";
        foreach (DataRow dr in dt.Rows)
            script += dr[0].ToString() + " <br>";

        return script;
    }


}

