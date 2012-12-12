﻿using System;
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
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;
public partial class WF_UC_Forward_UC : BP.Web.UC.UCBase3
{
    /// <summary>
    /// 工作ID
    /// </summary>
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    /// <summary>
    /// 流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FlowNo"];
            if (s == null)
                s = this.Request.QueryString["FK_Flow"];
            return s;
        }
    }
    public string FK_Dept
    {
        get
        {
            string s= this.Request.QueryString["FK_Dept"];
            if (string.IsNullOrEmpty(s))
                return WebUser.FK_Dept;
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ToolBar1.AddBtn(NamesOfBtn.Forward,"移交");
        this.ToolBar1.AddBtn(NamesOfBtn.Cancel, "取消");
        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Attributes["onclick"] = " return confirm('您确定要执行吗？');";

        Depts depts = new Depts();
        depts.RetrieveAllFromDBSource();
        this.ToolBar1.AddDDL("DDL_Dept");
        DDL ddl = this.ToolBar1.GetDDLByID("DDL_Dept");
        ddl.AutoPostBack = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        foreach (Dept dept in depts)
        {
            ListItem li = new ListItem();
            li.Text = BP.DA.DataType.GenerSpace(dept.Grade - 1) + dept.Name;
            li.Text = li.Text.Replace("&nbsp;", "_");
            li.Value = dept.No;
            ddl.Items.Add(li);

            if ( this.FK_Dept == li.Value)
                li.Selected = true;
        }

        this.ToolBar1.AddLab("ds","请选择移交人，输入移交原因，点移交按钮执行工作移交。");
        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Click += new EventHandler(WF_UC_Forward_Click);
        this.ToolBar1.GetBtnByID(NamesOfBtn.Cancel).Click += new EventHandler(WF_UC_Forward_Click);
        this.BindLB();
    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDL ddl =sender as DDL;
        this.Response.Redirect(this.PageID+".aspx?WorkID="+this.WorkID+"&FK_Node="+this.Request.QueryString["FK_Node"]+"&FK_Flow="+this.Request.QueryString["FK_Flow"]+"&FK_Dept="+ddl.SelectedItemStringVal,true);
    }
    void WF_UC_Forward_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case NamesOfBtn.Cancel:
                this.Response.Redirect("MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID, true);
                return;
            default:
                break;
        }

        try
        {
            string msg = this.Pub1.GetTextBoxByID("TB_Doc").Text;
            if (msg == "请输入移交原因...")
                throw new Exception("@您必须输入移交原因。");

            string sql = "";
            sql = " SELECT No,Name FROM Port_Emp WHERE FK_Dept='" + this.FK_Dept + "'";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            string toEmp = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["No"].ToString() == WebUser.No)
                    continue;
                RadioButton rb = this.Top.GetRadioButtonByID("RB_" + dr["No"]);
                if (rb == null || rb.Checked == false)
                    continue;
                toEmp = dr["No"].ToString();
            }

            if (toEmp == "")
            {
                this.Alert("请选择要移交的人员。");
                return;
            }
            string info = BP.WF.Dev2Interface.Node_Forward(this.WorkID, toEmp, msg);
            this.Session["info"] = info;
            this.Response.Redirect("MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=Msg&FK_Flow=" + this.FK_Flow, true);
            return;
        }
        catch (Exception ex)
        {
            ForwardWork fw = new ForwardWork();
            fw.CheckPhysicsTable();

            Log.DebugWriteWarning(ex.Message);
            this.Alert("工作移交出错：" + ex.Message);
        }
    }
    /// <summary>
    /// 绑定
    /// </summary>
    public void BindLB()
    {
        // 当前用的员工权限。
        string sql = "";
       // sql = " SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_EMP FROM Port_EmpDept WHERE FK_Dept IN (SELECT FK_Dept FROM Port_EmpDept WHERE fk_emp='" + BP.Web.WebUser.No + "') ) or FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%'";
        sql = " SELECT No,Name FROM Port_Emp WHERE FK_Dept='"+this.FK_Dept+"'";

        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        int colIdx = -1;

        this.Top.AddTable();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["No"].ToString() == WebUser.No)
                continue;

            colIdx++;
            if (colIdx == 0)
                this.Top.AddTR();

            string no = dr["No"].ToString();
            string name = dr["Name"].ToString();
            RadioButton rb = new RadioButton();
            rb.ID = "RB_" + no;
            rb.Text = no + " " + name;
            rb.GroupName = "s";
            this.Top.AddTD(rb);

            if (colIdx == 2)
            {
                colIdx = -1;
                this.Top.AddTREnd();
            }
        }
        this.Top.AddTableEnd();

        // 已经非配或者自动分配的任务。
        GenerWorkerLists wls = new GenerWorkerLists();
        wls.Retrieve(GenerWorkerListAttr.WorkID, this.WorkID, GenerWorkerListAttr.IsEnable, 1,
            GenerWorkerListAttr.IsPass, 0);

        int nodeID = 0;
        foreach (GenerWorkerList wl in wls)
        {
            RadioButton cb = this.Top.GetRadioButtonByID("RB_" + wl.FK_Emp);
            if (cb != null)
                cb.Checked = true;

            nodeID = wl.FK_Node;
        }
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 10;
        tb.Columns = 70;
        tb.ID = "TB_Doc";


        BP.WF.Node nd = new BP.WF.Node(nodeID);
        if (nd.FocusField != "")
        {
            Work wk = nd.HisWork;
            wk.OID = this.WorkID;
            wk.RetrieveFromDBSources();
            tb.Text = wk.GetValStringByKey(nd.FocusField);
        }
        this.Pub1.Add(tb);
    }
}

