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
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Tools : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "流程工具";
        int colspan = 1;
        this.AddTable("width='90%' align=center");
        this.AddTR();
        this.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD class=TitleMsg colspan=" + colspan + ">");
        this.BindTools();
        this.Add("</TD>");
        this.AddTREnd();

        this.AddTR();
        this.AddTDBegin("align=left");

        this.AddBR();

        switch (this.RefNo)
        {
            case "AutoLog":
                BindAutoLog();
                break;
            case "Pass":
                BindPass();
                break;
            case "Profile":
                BindProfile();
                break;
            case "Auto":
                BindAuto();
                break;
            case "Times": // 时效分析
                BindTimes();
                break;
            case "Per":
            default:
                BindPer();
                break;
        }
        this.AddBR();
        this.AddBR();

        this.AddTDEnd();
        this.AddTREnd();
        this.AddTableEnd();

    }

     

    public void BindPass()
    {
        this.AddFieldSet("密码修改");

        this.AddTable();
        this.AddTR();
        this.AddTDTitle();
        this.AddTDTitle();
        this.AddTDTitle();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("原密码");
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass1";
        this.AddTD(tb);
        this.AddTD();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("新密码");
         tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass2";
        this.AddTD(tb);
        this.AddTD();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("重输新密码");
        tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass3";
        this.AddTD(tb);
        this.AddTD();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("");

        Btn btn = new Btn();
        btn.Text = "确定";
        btn.Click += new EventHandler(btn_Click);
        this.AddTD(btn);
        this.AddTD();
        this.AddTREnd();
        this.AddTableEnd();

        this.AddFieldSetEnd(); 

    }
    public void BindProfile()
    {
        BP.WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(WebUser.No);

        this.AddFieldSet("修改个人信息");

        this.AddTable();
        this.AddTR();
        this.AddTDTitle();
        this.AddTDTitle();
        this.AddTDTitle();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("用于接受短信的手机");
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.SingleLine;
        tb.ID = "TB_Tel";
        tb.Text = emp.Tel;
        this.AddTD(tb);
        this.AddTD();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("用于接受短信的Email");
        tb = new TextBox();
        tb.TextMode = TextBoxMode.SingleLine;
        tb.ID = "TB_Email";
        tb.Text = emp.Email;
        this.AddTD(tb);
        this.AddTD();
        this.AddTREnd();


        this.AddTR();
        this.AddTD("");

        Btn btn = new Btn();
        btn.Text = "确定";
        btn.Click += new EventHandler(btn_Profile_Click);
        this.AddTD(btn);
        this.AddTD();
        this.AddTREnd();
        this.AddTableEnd();
        this.AddFieldSetEnd();

    }
    void btn_Profile_Click(object sender, EventArgs e)
    {
        string tel = this.GetTextBoxByID("TB_Tel").Text;
        string mail = this.GetTextBoxByID("TB_Email").Text;
        BP.WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(WebUser.No);
        emp.Tel = tel;
        emp.Email = mail;
        emp.Update();
        this.Alert("修改成功。");
    }
    void btn_Click(object sender, EventArgs e)
    {
        string p1=this.GetTextBoxByID("TB_Pass1").Text;
        string p2=this.GetTextBoxByID("TB_Pass2").Text;
        string p3=this.GetTextBoxByID("TB_Pass3").Text;

        if (p2.Length ==0 || p1.Length==0 )
        {
            this.Alert("密码不能为空");
            return;
        }

        if (p2 != p3)
        {
            this.Alert("两次密码不一致。");
            return;
        }


        Emp emp = new Emp(WebUser.No);
        if (emp.Pass ==p1 )
        {
            emp.Pass = p2;
            emp.Update();
            this.Alert("密码修改成功，请牢记新密码。");
        }
        else
        {
            this.Alert("老密码错误，不允许您修改它。");
        }
    }
    /// <summary>
    /// 时效分析
    /// </summary>
    public void BindTimes()
    {
        if (this.Request.QueryString["FK_Node"] != null)
        {
            this.BindTimesND();
            return;
        }
        if (this.Request.QueryString["FK_Flow"] != null)
        {
            this.BindTimesFlow();
            return;
        }

        FlowSorts sorts = new FlowSorts();
        sorts.RetrieveAll();

        Flows fls = new Flows();
        fls.RetrieveAll();

        Nodes nds = new Nodes();
        nds.RetrieveAll();

        this.AddTable();

        //  this.AddCaptionLeft("第1步：选择要分析的流程");
        //  this.AddTR();
        ////  this.AddTDTitle("ID");
        //  this.AddTDTitle("流程类别");
        //  this.AddTDTitle("流程/流程");
        // // this.AddTDTitle("操作");
        //  this.AddTDTitle("工作数");
        //  this.AddTDTitle("平均用天");
        //  this.AddTREnd();

        foreach (FlowSort sort in sorts)
        {
            this.AddTRSum();
            this.AddTDB(sort.Name);
            this.AddTD("");
            this.AddTD();
            this.AddTD();
            this.AddTREnd();

            foreach (Flow fl in fls)
            {
                if (sort.No != fl.FK_FlowSort)
                    continue;

                this.AddTRSum();
                this.AddTD();
                this.AddTDB(fl.Name);
                //  this.AddTD("<a href='Tools.aspx?DoType=Times&FK_Flow=" + fl.No + "'>分析</a>");
                this.AddTD("工作数");
                this.AddTD("平均天" + fl.AvgDay.ToString("0.00"));
                this.AddTREnd();

                decimal avgDay = 0;
                foreach (BP.WF.Node nd in nds)
                {
                    if (nd.FK_Flow != fl.No)
                        continue;

                    this.AddTR();
                    this.AddTD();
                    this.AddTD(nd.Name);
                    //  this.AddTD("<a href='Tools.aspx?DoType=Times&FK_Node=" + nd.NodeID + "'>分析</a>");
                    string sql = "";
                    if (nd.IsCheckNode)
                        sql = "SELECT count(*) FROM WF_GECheckStand where NodeID=" + nd.NodeID;
                    else
                        sql = "SELECT  count(*) FROM ND" + nd.NodeID;

                    try
                    {
                        int num = DBAccess.RunSQLReturnValInt(sql);
                        this.AddTD(num);
                    }
                    catch (Exception ex)
                    {
                        if (nd.IsCheckNode == false)
                            nd.CheckPhysicsTable();
                        this.AddTD("无效");
                    }


                    if (nd.IsCheckNode)
                        sql = "SELECT AVG( DateDiff(d, cast(RDT as datetime),  cast(CDT as datetime) ) ) FROM WF_GECheckStand where NodeID=" + nd.NodeID;
                    else
                        sql = "SELECT AVG( DateDiff(d, cast(RDT as datetime),  cast(CDT as datetime) ) ) FROM ND" + nd.NodeID;

                    try
                    {
                        decimal day = DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                        avgDay += day;
                        this.AddTD(day.ToString("0.00"));
                    }
                    catch (Exception ex)
                    {
                        if (nd.IsCheckNode == false)
                            nd.CheckPhysicsTable();
                        this.AddTD("无效");
                    }
                    this.AddTREnd();
                }

                if (avgDay != fl.AvgDay)
                {
                    fl.AvgDay = avgDay;
                    fl.Update();
                }
            }
        }
        this.AddTableEnd();
    }
    public void BindTimesFlow()
    {

    }
    public void BindTimesND()
    {
        int nodeid = int.Parse(this.Request.QueryString["FK_Node"]);
        BP.WF.Node nd = new BP.WF.Node(nodeid);
        this.AddTable();
        this.AddCaptionLeft("<a href='Tools.aspx?DoType=Times&FK_Flow=" + nd.FK_Flow + "'>" + nd.FlowName + "</a> => " + nd.Name);
        this.AddTR();
        this.AddTDTitle("序号");
        this.AddTDTitle("人员");
        this.AddTDTitle("平均用时");
        this.AddTDTitle("参与次数");
        this.AddTREnd();
        this.AddTableEnd();
    }
    public void BindAutoLog()
    {
        string sql = "SELECT a.No + a.Name as Empstr,AuthorDate FROM WF_Emp a WHERE Author='" + WebUser.No + "' AND AuthorIsOK=1";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        if (dt.Rows.Count == 0)
        {
            this.AddMsgGreen("提示：", "没有同事授权给您，您不能授权方式登陆。");
            return; 
        }
        this.AddFieldSet("下列同事授权给您");
        this.Add("<ul>");
        foreach (DataRow dr in dt.Rows)
        {
            this.AddLi("<a href=\"javascript:LogAs('" + dr[0] + "')\">授权人:" + dr["Empstr"] + " - 授权日期:" + dr["AuthorDate"] + "</a>");
        }
        this.Add("</ul>");
        this.AddFieldSetEnd();
    }
    public void BindAuto()
    {
        string sql = "SELECT a.No,a.Name,b.Name as DeptName FROM Port_Emp a, Port_Dept b WHERE a.FK_Dept=b.No and a.FK_Dept LIKE '" + WebUser.FK_Dept + "%'";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        this.AddFieldSet("请选择您要授权的人员");

        //this.Add("请选择您要授权的人员");
        //this.AddHR();

        this.Add("<ul>");
        string deptName = null;
        foreach (DataRow dr in dt.Rows)
        {
            string fk_emp = dr["No"].ToString();
            if (fk_emp == "admin" || fk_emp == WebUser.No)
                continue;

            if (dr["DeptName"].ToString() != deptName)
            {
                deptName = dr["DeptName"].ToString();

                this.AddB(deptName);
                this.AddBR();
            }
            if (Glo.IsShowUserNoOnly)
                this.AddLi("<a href=\"javascript:DoAutoTo('" + fk_emp + "','')\" >" + fk_emp + "</a>");
            else
                this.AddLi("<a href=\"javascript:DoAutoTo('" + fk_emp + "','" + dr["Name"] + "')\" >" + fk_emp + " - " + dr["Name"] + "</a>");


        }
        this.Add("</ul>");
        this.AddFieldSetEnd();
    }
    public void BindPer()
    {
        if (WebUser.Auth != null)
        {
            this.AddMsgOfInfo("提示：", "您的登陆是授权模式，您不能查看个人信息。<br> <a href=\"javascript:ExitAuth('" + WebUser.Auth + "')\">退出授权模式</a>。");
            return;
        }

        this.AddFieldSet("基本信息" + WebUser.Auth);
        this.AddBR("用户帐号：" + WebUser.No);
        this.AddBR("用户名：" + WebUser.Name);

        this.AddBR(" 电子签字：<img src='../Data/Siganture/" + WebUser.No + ".jpg' border=0 onerror=\"this.src='../Data/Siganture/UnName.jpg'\"/>" );

        this.AddBR();

        this.Add("部门编号：" + WebUser.FK_Dept);
        this.Add("部门名称：" + WebUser.FK_DeptName);

        this.AddBR();

        BP.WF.Port.WFEmp au = new BP.WF.Port.WFEmp(WebUser.No);
        if (au.RetrieveFromDBSources() == 0 || au.AuthorIsOK == false)
        {
            this.Add("授权情况：未授权。<a href='Tools.aspx?RefNo=Auto' >执行授权</a>");
        }
        else
        {
            this.Add("授权情况：授权给：" + au.Author + "，授权日期：" + au.AuthorDate + " <a href=\"javascript:TakeBack('" + au.Author + "')\" >取消授权</a>");
        }


        this.AddBR();

        this.Add("安全：<a href='Tools.aspx?RefNo=Pass'>修改密码</a>");

        
        this.AddBR("<hr><b>信息提示：</b><a href='Tools.aspx?RefNo=Profile'>修改</a>");
        this.Add("<br>接受短消息提醒手机号："+au.Tel);
        this.Add("<br>接受短E-mail提醒：" + au.Email);


        this.AddFieldSetEnd();

        this.AddBR();

        Stations sts = WebUser.HisStations;
        this.AddFieldSet("岗位权限");
        foreach (Station st in sts)
        {
            this.Add(st.No + " - " + st.Name);
        }
        this.AddFieldSetEnd();

        this.AddBR();

        Depts depts = WebUser.HisDepts;
        this.AddFieldSet("部门权限");
        foreach (Dept st in depts)
        {
            this.Add(st.No + " - " + st.Name);
        }
        this.AddFieldSetEnd();
    }

    public void BindTools()
    {
        BP.WF.XML.Tools tools = new BP.WF.XML.Tools();
        tools.RetrieveAll();

        string refno = this.RefNo;
        if (refno == null)
            refno = "Per";

        this.MenuSelfBegin();
        foreach (BP.WF.XML.Tool tool in tools)
        {
            if (tool.No == refno)
                this.MenuSelfItemS("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
            else
                this.MenuSelfItem("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
        }
        this.MenuSelfEnd();
    }

}
