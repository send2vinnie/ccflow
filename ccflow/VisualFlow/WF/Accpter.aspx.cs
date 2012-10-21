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
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;
using BP.WF;
using BP.Web.Controls;

public partial class WF_Accpter : WebPage
{
    public int ToNode
    {
        get
        {
            if (this.Request.QueryString["ToNode"] == null)
                return 0;
            return int.Parse(this.Request["ToNode"].ToString());
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request["FK_Node"].ToString());
        }
    }
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request["WorkID"].ToString());
        }
    }
    public string FK_Dept
    {
        get
        {
            string s = this.Request.QueryString["FK_Dept"];
            if (s == null)
                s = WebUser.FK_Dept;
            return s;
        }
    }
    public string FK_Station
    {
        get
        {
            return this.Request.QueryString["FK_Station"];
        }
    }
    public int MyToNode = 0;
    public DataTable GetTable()
    {
        if (this.MyToNode == 0)
            throw new Exception("@流程设计错误，没有转向的节点。举例说明: 当前是A节点。如果您在A点的属性里启用了[接受人]按钮，那么他的转向节点集合中(就是A可以转到的节点集合比如:A到B，A到C, 那么B,C节点就是转向节点集合)，必须有一个节点是的节点属性的[访问规则]设置为[由上一步发送人员选择]");

        NodeStations stas = new NodeStations(this.MyToNode);
        if (stas.Count == 0)
        {
            BP.WF.Node toNd = new BP.WF.Node(this.MyToNode);
            throw new Exception("@流程设计错误：设计员没有设计节点[" + toNd.Name + "]，接受人的岗位范围。");
        }

        string sql = "";

        // 优先解决本部门的问题。
        if (this.FK_Dept == WebUser.FK_Dept)
        {
            sql = "SELECT A.No,A.Name, A.FK_Dept, B.Name as DeptName FROM Port_Emp A,Port_Dept B WHERE A.FK_Dept=B.No AND a.NO IN ( ";
            sql += "SELECT FK_EMP FROM Port_EmpSTATION WHERE FK_STATION ";
            sql += "IN (SELECT FK_STATION FROM WF_NodeStation WHERE FK_Node=" + MyToNode + ") ";
            sql += ") AND a.No IN (SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept ='" + WebUser.FK_Dept + "')";
            sql += " ORDER BY FK_DEPT ";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count != 0)
                return dt;
        }

        sql = "SELECT A.No,A.Name, A.FK_Dept, B.Name as DeptName FROM Port_Emp A,Port_Dept B WHERE A.FK_Dept=B.No AND a.NO IN ( ";
        sql += "SELECT FK_EMP FROM Port_EmpSTATION WHERE FK_STATION ";
        sql += "IN (SELECT FK_STATION FROM WF_NodeStation WHERE FK_Node=" + MyToNode + ") ";
        sql += ") ORDER BY FK_DEPT ";
        return BP.DA.DBAccess.RunSQLReturnTable(sql);
    }
    private BP.WF.Node _HisNode = null;
    /// <summary>
    /// 它的节点
    /// </summary>
    public BP.WF.Node HisNode
    {
        get
        {
            if (_HisNode == null)
                _HisNode = new BP.WF.Node(this.FK_Node);
            return _HisNode;
        }
    }
    /// <summary>
    /// 是否多分支
    /// </summary>
    public bool IsMFZ
    {
        get
        {
            Nodes nds = this.HisNode.HisToNodes;
            int num = 0;
            foreach (BP.WF.Node mynd in nds)
            {
                if (mynd.HisDeliveryWay == DeliveryWay.BySelected)
                {
                    this.MyToNode = mynd.NodeID;
                    num++;
                }
            }
            if (num == 0)
                return false;
            if (num == 1)
                return false;
            return true;
        }
    }
    /// <summary>
    /// 绑定多分支
    /// </summary>
    public void BindMStations()
    {
        #region 判断是否有岗位.
        if (this.ToNode == 0)
        {
            Nodes nds = this.HisNode.HisToNodes;
            int num = 0;
            foreach (BP.WF.Node mynd in nds)
            {
                if (mynd.HisDeliveryWay == DeliveryWay.BySelected)
                {
                    this.MyToNode = mynd.NodeID;
                    num++;
                }
            }

            if (this.MyToNode == 0)
            {
                this.WinCloseWithMsg("流程设计错误：\n\n 当前节点的所有分支节点没有一个接受人员规则为按照选择接受。");
                return;
            }
            this.Response.Redirect("Accpter.aspx?FK_Node=" + this.FK_Node + "&ToNode=" + this.MyToNode + "&type=1&WorkID=" + this.WorkID, true);
        }
        else
        {
            this.MyToNode = this.ToNode;
        }
        #endregion 判断是否有岗位

        this.BindIt();

        Nodes mynds = this.HisNode.HisToNodes;
        this.Left.AddFieldSet("选择方向:不同的方向列出下一个岗位的不同人员列表");
        string str = "<p>";
        foreach (BP.WF.Node mynd in mynds)
        {
            if (mynd.HisDeliveryWay != DeliveryWay.BySelected)
                continue;
            if (this.ToNode == mynd.NodeID)
                str += "&nbsp;&nbsp;<b><font color='red' >" + mynd.Name + "</font></B>";
            else
                str += "&nbsp;&nbsp;<b><a href='Accpter.aspx?FK_Node=" + this.FK_Node + "&type=1&ToNode=" + mynd.NodeID + "&WorkID=" + this.WorkID + "' >" + mynd.Name + "</a></b>";
        }
        this.Left.Add(str + "</p>");
        this.Left.AddFieldSetEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "选择下一步骤接受的人员";

        try
        {
            /* 首先判断是否有多个分支的情况。*/
            if (this.IsMFZ)
            {
                this.BindMStations();
                return;
            }

            this.BindIt();
        }
        catch(Exception ex)
        {
            this.Pub1.Clear();
            this.Pub1.AddMsgOfWarning("错误", ex.Message);
        }
    }
    public void BindIt()
    {
        DataTable dt = this.GetTable(); //获取人员列表。
        SelectAccpers accps = new SelectAccpers();
        accps.Retrieve(SelectAccperAttr.FK_Node, this.FK_Node,
            SelectAccperAttr.WorkID, this.WorkID);

        Dept dept = new Dept();
        string fk_dept = "";
        this.Pub1.AddTable("width=100%");

        string info = "";
        if (WebUser.FK_Dept.Length > 2)
        {
            if (this.FK_Dept == WebUser.FK_Dept)
                info = "<b><a href='Accpter.aspx?ToNode=" + this.ToNode + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "&type=1&FK_Dept=" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "'>更多人员...</b></a>";
            else
                info = "<b><a href='Accpter.aspx?ToNode=" + this.ToNode + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "&type=1&FK_Dept=" + WebUser.FK_Dept + "'>本部门人员...</a></b>";
        }
      //  string sql = "select tonode from wf_direction where node =" + FK_Node;
//        var toNodeID = DBAccess.RunSQLReturnVal(sql);
//#warning 刘文辉 下一步流程名
//        BP.WF.Node nd = new BP.WF.Node();
//        QueryObject qo = new QueryObject(nd);
//        qo.AddWhere("NodeID", toNodeID);
//        qo.DoQuery();

        BP.WF.Node toNode = new BP.WF.Node(this.MyToNode);
        this.Pub1.AddCaptionLeft("<span style='color:red'>选择 [" + toNode.Name + "]</span>  可选择范围：" + dt.Rows.Count + " 位。" + info);
        if (dt.Rows.Count > 50)
        {
            /*多于一定的数，就显示导航。*/
            this.Pub1.AddTRSum();
            this.Pub1.Add("<TD class=BigDoc colspan=5>");
            foreach (DataRow dr in dt.Rows)
            {
                if (fk_dept != dr["FK_Dept"].ToString())
                {
                    fk_dept = dr["FK_Dept"].ToString();
                    dept = new Dept(fk_dept);
                    dr["DeptName"] = dept.Name;
                    this.Pub1.Add("<a href='#d" + dept.No + "' >" + dept.Name + "</a>&nbsp;");
                }
            }
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }

        int idx = -1;
        bool is1 = false;
        foreach (DataRow dr in dt.Rows)
        {
            idx++;

            if (fk_dept != dr["FK_Dept"].ToString())
            {
                switch (idx)
                {
                    case 0:
                        break;
                    case 1:
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                        break;
                    case 2:
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                        break;
                    case 3:
                        this.Pub1.AddTD();
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                        break;
                    case 4:
                        this.Pub1.AddTD();
                        this.Pub1.AddTREnd();
                        break;
                    default:
                        throw new Exception("error");
                }

                this.Pub1.AddTRSum();
                fk_dept = dr["FK_Dept"].ToString();
                string deptName = dr["DeptName"].ToString();
                this.Pub1.AddTD("colspan=5 aligen=left  class=FDesc ", "<a name='d" + dept.No + "' >" + deptName + "</a>");
                this.Pub1.AddTREnd();
                is1 = false;
                idx = 0;
            }

            string no = dr["No"].ToString();
            string name = dr["Name"].ToString();

            CheckBox cb = new CheckBox();
            if (BP.WF.Glo.IsShowUserNoOnly)
                cb.Text = no;
            else
                cb.Text = no + " " + name;

            cb.ID = "CB_" + no;
            if (accps.Contains("FK_Emp", no))
                cb.Checked = true;
            this.Pub1.AddTR();
            switch (idx)
            {
                case 0:
                    is1 = this.Pub1.AddTR(is1);
                    this.Pub1.AddTD(cb);
                    break;
                case 1:
                case 2:
                case 3:
                    this.Pub1.AddTD(cb);
                    break;
                case 4:
                    this.Pub1.AddTD(cb);
                    this.Pub1.AddTREnd();
                    idx = -1;
                    break;
                default:
                    throw new Exception("error");
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        this.Pub1.AddHR();
        Button btn = new Button();
        btn.Text = this.ToE("Save", "保存");
        btn.ID = "Btn_Save";
        btn.CssClass = "Btn";
        //btn.Width = 80;
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btn_Save_Click(object sender, EventArgs e)
    {
        DataTable dt = this.GetTable();
        string emps = "";
        foreach (DataRow dr in dt.Rows)
        {
            CheckBox cb = this.Pub1.GetCBByID("CB_" + dr["No"].ToString());
            if (cb.Checked == false)
                continue;
            emps += dr["No"].ToString() + ",";
        }

        if (emps.Length < 2)
        {
            this.Alert("您没有选择人员。");
            return;
        }

        SelectAccpers ens = new SelectAccpers();
        ens.Delete(SelectAccperAttr.FK_Node, this.FK_Node, SelectAccperAttr.WorkID, this.WorkID);

        string[] strs = emps.Split(',');
        foreach (string str in strs)
        {
            if (str == null || str == "")
                continue;

            SelectAccper en = new SelectAccper();
            en.MyPK = this.FK_Node + "_" + this.WorkID + "_" + str;
            en.FK_Emp = str;
            en.FK_Node = this.FK_Node;
            en.WorkID = this.WorkID;
            en.Insert();
        }
#warning 刘文辉 保存收件人后调用发送按钮
        //this.WinCloseWithMsg("接受人的范围选择成功。");
        if (Request.QueryString["type"] == null || Request.QueryString["type"] == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "", "this.close();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "", "send();", true);
        }
    }
}
