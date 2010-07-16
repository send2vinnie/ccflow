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


public partial class WF_Admin_TestFlow : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string Lang
    {
        get
        {
            return this.Request.QueryString["Lang"];
        }
    }
    public void BindFlowList()
    {
        this.Title = " 流程测试&设计 - 感谢您选择驰骋工作流引擎 - 流程设计器下载";

        BP.WF.Flows fls = new BP.WF.Flows();
        fls.RetrieveAll();

        this.Left.AddFieldSet("系统流程列表");

        this.Left.AddTable();
        bool is1 = false;
        foreach (BP.WF.Flow fl in fls)
        {
            is1 = this.Left.AddTR(is1);
            this.Left.AddTD(fl.FK_FlowSortText);
            if (fl.No == this.FK_Flow)
                this.Left.AddTDB("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New'>" + fl.Name + "</a>");
            else
                this.Left.AddTD("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New'>" + fl.Name + "</a>");

            this.Left.AddTD("<a href='/Front/Data/FlowDesc/" + fl.No + ".gif' target=_blank>流程图</a>");
            this.Left.AddTD("<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK="+fl.No+"','s')\" >属性</a>");
            this.Left.AddTREnd();
        }

        this.Left.AddTableEnd();
        this.Left.AddFieldSetEnd();



        this.Left.AddFieldSet("系统管理");
        this.Left.AddUL();
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.Port.Stations')\">岗位维护</a>");
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.Port.Depts')\">部门维护</a>");
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.Port.Emps')\">人员维护</a>");
        this.Left.AddULEnd();
        this.Left.AddFieldSetEnd(); 

        this.Left.AddFieldSet("相关下载");
        this.Left.Add("<a href=http://flow.ccflow.cn/ > http://flow.ccflow.cn/ 相关下载</a>");
        this.Left.AddBR("<a href=/WF/Login.aspx > 直接登陆 (嵌入方式)</a>");
        this.Left.AddBR("<a href=/WF/Port/Signin.aspx > 直接登陆 (系统方式)</a>");
        this.Left.AddFieldSetEnd(); 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Emp emp1 = new Emp("admin");
        WebUser.SignInOfGener(emp1);


        this.BindFlowList();

        if (this.FK_Flow == null)
        {
            this.Ucsys1.AddFieldSet("关于流程测试");

            this.Ucsys1.AddUL();

            this.Ucsys1.AddLi("现在是流程测试状态，此功能紧紧提供给流程设计人员使用。");

            this.Ucsys1.AddLi("提供此功能的目的是，快速的让各个角色人员登录，以便减少登录的繁琐麻烦。");


            this.Ucsys1.AddLi("点左边的流程列表后，系统自动显示能够发起此流程的工作人员，点一个工作人员就直接登录了。");


            this.Ucsys1.AddULEnd();

            this.Ucsys1.AddFieldSetEnd(); 
            return;
        }
        



        BP.Web.WebUser.SysLang = this.Lang;
        if (this.RefNo != null)
        {
            Emp emp = new Emp(this.RefNo);
            BP.Web.WebUser.SignInOfGenerLang(emp, this.Lang);

            this.Session["FK_Flow"] = this.FK_Flow;

            if (this.Request.QueryString["Type"] != null)
            {
                this.Response.Redirect("../../WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow, true);
            }
            else
                this.Response.Redirect("../Port/Home.htm?FK_Flow=" + this.FK_Flow, true);
            return;
        }


        Flow fl = new Flow(this.FK_Flow);

        this.Ucsys1.AddFieldSet("流程:"+fl.Name);
        this.Ucsys1.AddUL();
        Nodes nds = new Nodes(this.FK_Flow);
        foreach (BP.WF.Node nd in nds)
        {
            this.Ucsys1.AddLi("第" + nd.Step + "步:<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + nd.NodeID + "')\">" + nd.Name + "</a>, <a href=\"javascript:WinOpen('../MapDef/MapDef.aspx?PK=ND" + nd.NodeID + "')\">设计表单</a>");
        }
        this.Ucsys1.AddULEnd();
        this.Ucsys1.AddFieldSetEnd(); 

        int nodeid = int.Parse(this.FK_Flow + "01");
        Emps emps = new Emps();
        emps.RetrieveInSQL_Order("select fk_emp from port_empstation where fk_station in (select fk_station from wf_nodestation where fk_node='" + nodeid + "' )", "FK_Dept");

        if (emps.Count == 0)
        {
            this.Ucsys1.AddMsgOfWarning("流程设计错误",
                this.ToE("StartError", "错误原因 <br>@1，可能是您没有正确的设置岗位、部门、人员。<br>@2，可能是没有给开始节点设置工作岗位。。"));
            return;
        }

        this.Ucsys1.AddFieldSet(this.ToE("ChoseStarter", "可发起流程的人员"));

        this.Ucsys1.Add("说明:点击用户名，您就可以以他的身份登录并发起流程。");
        this.Ucsys1.AddUL();
        foreach (Emp emp in emps)
        {
            this.Ucsys1.AddLi(emp.No + "&nbsp;&nbsp;&nbsp;&nbsp;<a href='TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  >" + emp.No + emp.Name + "</a>   " + emp.FK_DeptText);
        }
        this.Ucsys1.AddULEnd();
        this.Ucsys1.AddFieldSetEnd(); 



        
       // this.Ucsys1.Add("<a href='../../Data/FlowDesc/" + this.FK_Flow + ".gif' target=_blank><img border=0 src='../../Data/FlowDesc/" + this.FK_Flow + ".gif' width=300px height=300px ></a>");
    }
}
