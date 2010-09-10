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
        this.BindTools();

        this.Page.Title = "流程工具";
        int colspan = 1;

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
    }

    public void BindPass()
    {
        this.Pub1.AddFieldSet("密码修改");

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle();
        this.Pub1.AddTDTitle();
        this.Pub1.AddTDTitle();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("原密码");
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass1";
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("新密码");
         tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass2";
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("重输新密码");
        tb = new TextBox();
        tb.TextMode = TextBoxMode.Password;
        tb.ID = "TB_Pass3";
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD("");

        Btn btn = new Btn();
        btn.Text = "确定";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.AddTD(btn);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        this.Pub1.AddFieldSetEnd(); 

    }
    public void BindProfile()
    {
        BP.WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(WebUser.No);

        this.Pub1.AddFieldSet("修改个人信息");

        this.Pub1.AddTable();

        this.Pub1.AddTR();
        this.Pub1.AddTD("用于接受短信的手机");
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.SingleLine;
        tb.ID = "TB_Tel";
        tb.Text = emp.Tel;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("用于接受短信的Email");
        tb = new TextBox();
        tb.TextMode = TextBoxMode.SingleLine;
        tb.ID = "TB_Email";
        tb.Text = emp.Email;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD("信息接收方式");
        DDL ddl = new DDL();
        ddl.ID = "DDL_Way";
        ddl.Items.Add(new ListItem("不接收", "0"));
        ddl.Items.Add(new ListItem("手机短信", "1"));
        ddl.Items.Add(new ListItem("邮件", "2"));
        ddl.Items.Add(new ListItem("手机短信+邮件", "3"));
        this.Pub1.AddTD(ddl);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("");

        Btn btn = new Btn();
        btn.Text = " 保存 ";
        btn.Click += new EventHandler(btn_Profile_Click);
        this.Pub1.AddTD(btn);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        this.Pub1.AddFieldSetEnd();
    }
    void btn_Profile_Click(object sender, EventArgs e)
    {
        string tel = this.Pub1.GetTextBoxByID("TB_Tel").Text;
        string mail = this.Pub1.GetTextBoxByID("TB_Email").Text;
        int way = this.Pub1.GetDDLByID("DDL_Way").SelectedItemIntVal;

        BP.WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(WebUser.No);
        emp.Tel = tel;
        emp.Email = mail;

        emp.HisAlertWay = (BP.WF.Port.AlertWay)way;

        try
        {
            emp.Update();
            this.Alert("设置生效，谢谢使用。");
        }
        catch(Exception ex)
        {
            this.Alert("设置生效，谢谢使用。");
        }
    }
    void btn_Click(object sender, EventArgs e)
    {
        string p1=this.Pub1.GetTextBoxByID("TB_Pass1").Text;
        string p2=this.Pub1.GetTextBoxByID("TB_Pass2").Text;
        string p3=this.Pub1.GetTextBoxByID("TB_Pass3").Text;

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

        this.Pub1.AddTable();

        //  this.Pub1.AddCaptionLeft("第1步：选择要分析的流程");
        //  this.Pub1.AddTR();
        ////  this.Pub1.AddTDTitle("ID");
        //  this.Pub1.AddTDTitle("流程类别");
        //  this.Pub1.AddTDTitle("流程/流程");
        // // this.Pub1.AddTDTitle("操作");
        //  this.Pub1.AddTDTitle("工作数");
        //  this.Pub1.AddTDTitle("平均用天");
        //  this.Pub1.AddTREnd();

        foreach (FlowSort sort in sorts)
        {
            this.Pub1.AddTRSum();
            this.Pub1.AddTDB(sort.Name);
            this.Pub1.AddTD("");
            this.Pub1.AddTD();
            this.Pub1.AddTD();
            this.Pub1.AddTREnd();

            foreach (Flow fl in fls)
            {
                if (sort.No != fl.FK_FlowSort)
                    continue;

                this.Pub1.AddTRSum();
                this.Pub1.AddTD();
                this.Pub1.AddTDB(fl.Name);
                //  this.Pub1.AddTD("<a href='Tools.aspx?DoType=Times&FK_Flow=" + fl.No + "'>分析</a>");
                this.Pub1.AddTD("工作数");
                this.Pub1.AddTD("平均天" + fl.AvgDay.ToString("0.00"));

                this.Pub1.AddTD("我参与的工作数");
                this.Pub1.AddTD("工作总数");

                this.Pub1.AddTREnd();

                decimal avgDay = 0;
                foreach (BP.WF.Node nd in nds)
                {
                    if (nd.FK_Flow != fl.No)
                        continue;

                    this.Pub1.AddTR();
                    this.Pub1.AddTD();
                    this.Pub1.AddTD(nd.Name);
                    //  this.Pub1.AddTD("<a href='Tools.aspx?DoType=Times&FK_Node=" + nd.NodeID + "'>分析</a>");
                    string sql = "";
                    
                        sql = "SELECT  COUNT(*) FROM ND" + nd.NodeID;

                    try
                    {
                        int num = DBAccess.RunSQLReturnValInt(sql);
                        this.Pub1.AddTD(num);
                    }
                    catch (Exception ex)
                    {
                            nd.CheckPhysicsTable();
                        this.Pub1.AddTD("无效");
                    }


                  
                        sql = "SELECT AVG( DateDiff(d, cast(RDT as datetime),  cast(CDT as datetime) ) ) FROM ND" + nd.NodeID;

                    try
                    {
                        decimal day = DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                        avgDay += day;
                        this.Pub1.AddTD(day.ToString("0.00"));
                    }
                    catch (Exception ex)
                    {
                            nd.CheckPhysicsTable();
                        this.Pub1.AddTD("无效");
                    }

                    // day = DBAccess.RunSQLReturnValDecimal(sql, 0, 2);
                    //this.Pub1.AddTD(DBAccess.RunSQLReturnValInt(""));
                    this.Pub1.AddTD("无效");

                    this.Pub1.AddTREnd();
                }

                if (avgDay != fl.AvgDay)
                {
                    fl.AvgDay = avgDay;
                    fl.Update();
                }
            }
        }
        this.Pub1.AddTableEnd();
    }
    public void BindTimesFlow()
    {
    }
    public void BindTimesND()
    {
        int nodeid = int.Parse(this.Request.QueryString["FK_Node"]);
        BP.WF.Node nd = new BP.WF.Node(nodeid);
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("<a href='Tools.aspx?DoType=Times&FK_Flow=" + nd.FK_Flow + "'>" + nd.FlowName + "</a> => " + nd.Name);
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("序号");
        this.Pub1.AddTDTitle("人员");
        this.Pub1.AddTDTitle("平均用时");
        this.Pub1.AddTDTitle("参与次数");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    public void BindAutoLog()
    {
        string sql = "SELECT a.No + a.Name as Empstr,AuthorDate FROM WF_Emp a WHERE Author='" + WebUser.No + "' AND AuthorIsOK=1";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        if (dt.Rows.Count == 0)
        {
            this.Pub1.AddMsgGreen("提示：", "没有同事授权给您，您不能授权方式登陆。");
            return; 
        }
        this.Pub1.AddFieldSet("下列同事授权给您");
        this.Pub1.Add("<ul>");
        foreach (DataRow dr in dt.Rows)
        {
            this.Pub1.AddLi("<a href=\"javascript:LogAs('" + dr[0] + "')\">授权人:" + dr["Empstr"] + " - 授权日期:" + dr["AuthorDate"] + "</a>");
        }
        this.Pub1.Add("</ul>");
        this.Pub1.AddFieldSetEnd();
    }
    public void BindAuto()
    {
        string sql = "SELECT a.No,a.Name,b.Name as DeptName FROM Port_Emp a, Port_Dept b WHERE a.FK_Dept=b.No AND a.FK_Dept LIKE '" + WebUser.FK_Dept + "%' ORDER  BY a.FK_Dept ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        this.Pub1.AddFieldSet("请选择您要授权的人员");
        string deptName = null;

        this.Pub1.AddTable("width=80% align=center");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("部门");
        this.Pub1.AddTDTitle("要执行授权的人员");
        this.Pub1.AddTREnd();

        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string fk_emp = dr["No"].ToString();
            if (fk_emp == "admin" || fk_emp == WebUser.No)
                continue;


            idx++;
            if (dr["DeptName"].ToString() != deptName)
            {
                deptName = dr["DeptName"].ToString();
                this.Pub1.AddTRSum();
                this.Pub1.AddTDIdx(idx);
                this.Pub1.AddTD(deptName);
            }
            else
            {
                this.Pub1.AddTR();
                this.Pub1.AddTDIdx(idx);
                this.Pub1.AddTD();
            }


            if (Glo.IsShowUserNoOnly)
                this.Pub1.AddTD("<a href=\"javascript:DoAutoTo('" + fk_emp + "','')\" >" + fk_emp + "</a>");
            else
                this.Pub1.AddTD("<a href=\"javascript:DoAutoTo('" + fk_emp + "," + dr["Name"] + "','" + dr["Name"] + "')\" >" + fk_emp + " - " + dr["Name"] + "</a>");

            this.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        this.Pub1.AddFieldSetEnd();
    }
    public void BindPer()
    {
        if (WebUser.Auth != null)
        {
            this.Pub1.AddMsgOfInfo("提示：", "您的登陆是授权模式，您不能查看个人信息。<br> <a href=\"javascript:ExitAuth('" + WebUser.Auth + "')\">退出授权模式</a>。");
            return;
        }

        this.Pub1.AddFieldSet("基本信息" + WebUser.Auth);
        this.Pub1.Add("用户帐号：" + WebUser.No);
        this.Pub1.AddBR("用户名：" + WebUser.Name);

        this.Pub1.AddBR(" 电子签字：<img src='../Data/Siganture/" + WebUser.No + ".jpg' border=0 onerror=\"this.src='../Data/Siganture/UnName.jpg'\"/>");

        this.Pub1.AddBR();

        this.Pub1.Add("所在部门：<font color=green>" + WebUser.FK_DeptName+"</font>");

        this.Pub1.AddBR();

        BP.WF.Port.WFEmp au = new BP.WF.Port.WFEmp(WebUser.No);
        if (au.RetrieveFromDBSources() == 0 || au.AuthorIsOK == false)
        {
            this.Pub1.Add("授权情况：未授权，<a href='Tools.aspx?RefNo=Auto' >执行授权</a>。");
        }
        else
        {
            this.Pub1.Add("授权情况：授权给：<font color=green>" + au.Author + "</font>，授权日期：<font color=green>" + au.AuthorDate + "</font> <a href=\"javascript:TakeBack('" + au.Author + "')\" >取消授权</a>");
        }


        this.Pub1.AddBR();

        this.Pub1.Add("安全：<a href='Tools.aspx?RefNo=Pass'>修改密码</a>");


        this.Pub1.AddBR("<hr><b>信息提示：</b><a href='Tools.aspx?RefNo=Profile'>设置/修改</a>");
        this.Pub1.Add("<br>接受短消息提醒手机号：<font color=green>" + au.TelHtml + "</font>");
        this.Pub1.Add("<br>接受短E-mail提醒：<font color=green>" + au.EmailHtml + "</font>");

        this.Pub1.AddHR();
        Stations sts = WebUser.HisStations;
        this.Pub1.AddB("岗位/部门-权限");

        this.Pub1.AddBR("岗位权限");
        foreach (Station st in sts)
        {
            this.Pub1.Add(" - <font color=green>" + st.Name+"</font>");
        }

        Depts depts = WebUser.HisDepts;
        this.Pub1.AddBR();
        this.Pub1.Add("部门权限");
        foreach (Dept st in depts)
        {
            this.Pub1.Add(" - <font color=green>" + st.Name + "</font>");
        }
        this.Pub1.AddFieldSetEnd();
    }

    public void BindTools()
    {
        BP.WF.XML.Tools tools = new BP.WF.XML.Tools();
        tools.RetrieveAll();

        string refno = this.RefNo;
        if (refno == null)
            refno = "Per";



        //this.Left.MenuSelfBegin();
        //foreach (BP.WF.XML.Tool tool in tools)
        //{
        //    if (tool.No == refno)
        //        this.Left.MenuSelfItemS("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
        //    else
        //        this.Left.MenuSelfItem("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
        //}
        //this.Left.MenuSelfEnd();


        this.Left.DivInfoBlockBegin();

        this.Left.AddUL();
        foreach (BP.WF.XML.Tool tool in tools)
        {
            if (tool.No == refno)
                this.Left.AddLi("<b>" + tool.Name + "</b>");
            else
                this.Left.AddLi("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
        }
        this.Left.AddULEnd();

        this.Left.DivInfoBlockEnd();

    }

}
