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
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
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
        this.ToolBar1.AddBtn(NamesOfBtn.Forward);
        this.ToolBar1.AddBtn(NamesOfBtn.Cancel, "取消");
        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Attributes["onclick"] = " return confirm('您确定要执行吗？');";

        this.ToolBar1.GetBtnByID(NamesOfBtn.Forward).Click += new EventHandler(WF_UC_Forward_Click);
        this.ToolBar1.GetBtnByID(NamesOfBtn.Cancel).Click += new EventHandler(WF_UC_Forward_Click);

        if (this.IsPostBack == false)
            this.BindLB();

        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 15;
        tb.Columns = 30;
        tb.ID = "TB_Doc";
        this.Pub1.Add(tb);
    }
    void WF_UC_Forward_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case NamesOfBtn.Cancel:
                this.Response.Redirect("MyFlow"+Glo.FromPageType+".aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID, true);
                return;
            default:
                break;
        }

        try
        {
            if (this.Pub1.GetTextBoxByID("TB_Doc").Text == "请输入转发原因..." )
                throw new Exception("@您必须输入转发原因。");

            ArrayList al = new ArrayList();
            foreach (ListItem li in this.CheckBoxList1.Items)
            {
                if (li.Selected)
                {
                    al.Add(li.Value);
                }
            }

            if (al.Count == 0)
            {
                this.Alert("请选择要转发的人员了。");
                return;
            }

            // 删除当前非配的工作。
            // 已经非配或者自动分配的任务。
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            int nodeId = gwf.FK_Node;
            int workId = this.WorkID;
            //WorkerLists wls = new WorkerLists(this.WorkID,nodeId);
            DBAccess.RunSQL("update WF_GenerWorkerlist SET IsEnable=0  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nodeId);
            string vals = "";
            string emps = "";
            foreach (Object obj in al)
            {
                emps += obj + ",";
                int i = DBAccess.RunSQL("update WF_GenerWorkerlist set IsEnable=1  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nodeId + " AND fk_emp='" + obj + "'");
                if (i == 0)
                {
                    /*说明: 用其它的岗位上的人来处理的，就给他增加待办工作。*/
                    WorkerLists wls = new WorkerLists(this.WorkID, nodeId);
                    WorkerList wl = wls[0] as WorkerList;
                    wl.FK_Emp = obj.ToString();
                    wl.IsEnable = true;
                    wl.Insert();
                }
            }
            BP.WF.Node nd = new BP.WF.Node(nodeId);
            Work wk = nd.HisWork;
           
                wk.OID = this.WorkID;
                wk.Retrieve();
             
            wk.Emps = emps;
            wk.Update();


            // CHOfNode ch = new CHOfNode(this.WorkID, nodeId, BP.Web.WebUser.No);
            // ch.Emps = emps;
            // ch.Update();

            ForwardWork fw = new ForwardWork();
            fw.WorkID = this.WorkID;
            fw.NodeId = nodeId;
            fw.Emps = emps;
            fw.Note = this.Pub1.GetTextBoxByID("TB_Doc").Text;
            fw.FK_Emp = BP.Web.WebUser.No;
            try
            {
                fw.Save();
            }
            catch
            {
                fw.Insert();
            }


            this.Session["info"] = "@工作转发成功。";
            this.Response.Redirect("MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=Msg&FK_Flow=" + this.FK_Flow, true);
            return;
        }
        catch (Exception ex)
        {
            //this.Alert(ex.Message);
            //this.Response.Write(ex.Message);
            Log.DebugWriteWarning(ex.Message);
            this.Alert("工作转发出错：" + ex.Message);
        }
    }
    public void BindLB()
    {
        this.CheckBoxList1.Items.Clear();
        // 当前用的员工权限。
        string sql = "";
        sql = " SELECT [No],Name FROM Port_Emp WHERE NO IN (SELECT FK_EMP FROM Port_EmpDept WHERE FK_Dept IN (  SELECT FK_Dept FROM Port_EmpDept WHERE fk_emp='" + BP.Web.WebUser.No + "') ) or FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%'";

        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["No"].ToString() == WebUser.No)
                continue;

            this.CheckBoxList1.Items.Add(new ListItem(dr["No"].ToString() + " " + dr["Name"].ToString(), dr["No"].ToString()));
        }
        // 已经非配或者自动分配的任务。
        GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
        int nodeId = gwf.FK_Node;
        WorkerLists wls = new WorkerLists(this.WorkID, nodeId);
        foreach (ListItem li in this.CheckBoxList1.Items)
        {
            foreach (WorkerList wl in wls)
            {
                if (wl.FK_Emp.ToString() == li.Value)
                {
                    li.Selected = false;
                    break;
                }
            }
        }
    }
}

