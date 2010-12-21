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
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;
using BP.WF;
using BP.Web.Controls;

public partial class WF_Accpter : WebPage
{
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request["FK_Node"].ToString());
        }
    }
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request["WorkID"].ToString());
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
    public DataTable GetTable()
    {

        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        if (nd.HisToNodes.Count > 1)
            throw new Exception("@流程设计错误，下一个节点发送到那里不能确定，您不能选择接受人员。");

        Nodes nds = nd.HisToNodes;
        int nodeID = nds[0].GetValIntByKey("NodeID");

        NodeStations stas = new NodeStations(nodeID);
        if (stas.Count == 0)
        {
            BP.WF.Node toNd = new BP.WF.Node(nodeID);
            throw new Exception("@流程设计错误：设计员没有设计节点[" + toNd.Name + "]，接受人的岗位范围。");
        }

        string sql = "SELECT No,Name,FK_Dept, '' as DeptName FROM Port_Emp WHERE NO IN ( ";
        sql += "SELECT FK_EMP FROM Port_EmpSTATION WHERE FK_STATION ";
        sql += "IN (SELECT FK_STATION FROM WF_NodeStation WHERE FK_Node=" + nodeID + ") ";
        sql += ") ORDER BY FK_DEPT";
        return BP.DA.DBAccess.RunSQLReturnTable(sql);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "选择下一步骤接受的人员";

        DataTable dt = this.GetTable(); //获取人员列表。
        SelectAccpers accps = new SelectAccpers();
        accps.Retrieve(SelectAccperAttr.FK_Node, this.FK_Node,
            SelectAccperAttr.WorkID, this.WorkID);

        Dept dept = new Dept();
        string fk_dept = "";
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX(this.Title+"，可选择范围："+dt.Rows.Count+" 位。");

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
               string  deptName = dr["DeptName"].ToString();

               this.Pub1.AddTD("colspan=5 class=FDesc", "<a name='d" + dept.No + "' >" + deptName + "</a>");
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

            switch (idx)
            {
                case 0:
                   is1= this.Pub1.AddTR(is1);
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
        }
        this.Pub1.AddTableEnd();


        this.Pub1.AddHR();
        Button btn = new Button();
        btn.Text = this.ToE("Save", " 保存 ");
        btn.ID = "Btn_Save";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
    }

    void btn_Click(object sender, EventArgs e)
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

        this.Alert("接受人的范围选择成功。");
        //this.WinClose();
    }
}
