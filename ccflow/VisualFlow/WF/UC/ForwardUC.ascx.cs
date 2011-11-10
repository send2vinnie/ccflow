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
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ToolBar1.AddBtn(NamesOfBtn.Forward,"移交");
        this.ToolBar1.AddBtn(NamesOfBtn.Cancel, "取消");
        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Attributes["onclick"] = " return confirm('您确定要执行吗？');";
        this.ToolBar1.AddLab("ds","请选择移交人，输入移交原因，点移交按钮执行工作移交。");
        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Click += new EventHandler(WF_UC_Forward_Click);
        this.ToolBar1.GetBtnByID(NamesOfBtn.Cancel).Click += new EventHandler(WF_UC_Forward_Click);
        this.BindLB();
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
            if (this.Pub1.GetTextBoxByID("TB_Doc").Text == "请输入移交原因...")
                throw new Exception("@您必须输入移交原因。");

            string sql = "";
            sql = " SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_EMP FROM Port_EmpDept WHERE FK_Dept IN (SELECT FK_Dept FROM Port_EmpDept WHERE fk_emp='" + BP.Web.WebUser.No + "') ) or FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%'";
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
            ArrayList al = new ArrayList();
            al.Add(toEmp);

            // 删除当前非配的工作。
            // 已经非配或者自动分配的任务。
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            int nodeId = gwf.FK_Node;
            Int64 workId = this.WorkID;

            DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsEnable=0  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nodeId);
            string emps = "," + toEmp + ",";
            int i = DBAccess.RunSQL("UPDATE WF_GenerWorkerlist set IsEnable=1  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nodeId + " AND FK_Emp='" + toEmp + "'");
            Emp emp = new Emp(toEmp);
            if (i == 0)
            {
                /*说明: 用其它的岗位上的人来处理的，就给他增加待办工作。*/
                WorkerLists wls = new WorkerLists(this.WorkID, nodeId);
                WorkerList wl = wls[0] as WorkerList;
                wl.FK_Emp = toEmp.ToString();

                wl.FK_EmpText = emp.Name;

                wl.IsEnable = true;
                wl.Insert();
            }

            BP.WF.Node nd = new BP.WF.Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();
            wk.Emps = emps;
            wk.Rec = toEmp;
            wk.NodeState = NodeState.Forward;
            wk.Update();

            ForwardWork fw = new ForwardWork();
            fw.WorkID = this.WorkID;
            fw.FK_Node = nodeId;
            fw.ToEmp = emps;
            fw.ToEmpName = emp.Name;
            fw.Note = this.Pub1.GetTextBoxByID("TB_Doc").Text;
            fw.FK_Emp = WebUser.No;
            fw.FK_EmpName = WebUser.Name;
            fw.Insert();

            // 记录日志.
            WorkNode wn = new WorkNode(wk, nd);
            wn.AddToTrack(ActionType.Shift, toEmp, emp.Name, nd.NodeID, nd.Name, fw.Note);
            if (wn.HisNode.FocusField != "")
            {
                wn.HisWork.Update(wn.HisNode.FocusField, "");
            }

            this.Session["info"] = "@工作移交成功。@您已经成功的把工作移交给："+emp.No+" , "+emp.Name;
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
        sql = " SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_EMP FROM Port_EmpDept WHERE FK_Dept IN (SELECT FK_Dept FROM Port_EmpDept WHERE fk_emp='" + BP.Web.WebUser.No + "') ) or FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%'";
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
        WorkerLists wls = new WorkerLists();
        wls.Retrieve(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.IsEnable, 1,
            WorkerListAttr.IsPass, 0);

        int nodeID = 0;
        foreach (WorkerList wl in wls)
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
            wk.Retrieve();
            tb.Text = wk.GetValStringByKey(nd.FocusField);
        }
        this.Pub1.Add(tb);
    }
}

