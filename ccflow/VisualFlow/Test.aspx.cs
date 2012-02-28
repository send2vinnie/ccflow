using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;

public partial class TestFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.repariIt();
        return;

        // 把流程运行到最后的节点上去，并且结束流程。
        string file = @"C:\aa\开票流程2.xls";
        string info = BP.WF.Glo.LoadFlowDataWithToSpecEndNode(file);
        this.Response.Write(info);
        return;

        // 把流程运行到指定的节点上去，并且不结束流程。
        string file1 = @"C:\aa\开票流程1.xls";
        string info1 = BP.WF.Glo.LoadFlowDataWithToSpecNode(file1);
        this.Response.Write(info1);
        return;
 
    }

    public void repariIt()
    {
        string sql = "SELECT * FROM WF_GenerWorkFlow WHERE Title LIKE '%2月28日%' and FK_Flow='079'";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        string xlsFile = @"C:\aa\开票流程2.xls";
        DataTable dtFrom = BP.DBLoad.GetTableByExt(xlsFile);

        Node mynd = new Node(7901);
        string info = "";
        foreach (DataRow dr in dt.Rows)
        {
            int fk_node = int.Parse(dr["FK_Node"].ToString());
            Node nd = new Node(fk_node);
            if (nd.IsEndNode == false)
                continue;

            int workid = int.Parse(dr["WorkID"].ToString());
            sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE ispass=0 and WORKID=" + workid + " AND FK_Node=" + fk_node;
            string currEmp = BP.DA.DBAccess.RunSQLReturnString(sql);
            if (currEmp == null)
                throw new Exception(sql + "没有找到人员.");

            Work mywkStart = mynd.HisWork;
            mywkStart.OID = workid;
            if (mywkStart.RetrieveFromDBSources() == 0)
                continue;

            Work mywkEnd = nd.HisWork;
            mywkEnd.OID = workid;
            if (mywkEnd.RetrieveFromDBSources() == 0)
                continue;

            //处理没有复制上的数据。
            foreach (DataRow mydr in dtFrom.Rows)
            {
                string flowpk = mydr["FlowPK"].ToString();

                if (flowpk.Contains(mywkStart.GetValStrByKey("FlowPK")) == false)
                    continue;
                foreach (DataColumn dc in dtFrom.Columns)
                {
                    mywkStart.SetValByKey(dc.ColumnName, mydr[dc.ColumnName]);
                    mywkEnd.SetValByKey(dc.ColumnName, mydr[dc.ColumnName]);
                }
                mywkStart.DirectUpdate();
                mywkEnd.DirectUpdate();
            }

            WorkNode wn = new WorkNode(workid, fk_node);
            info += wn.AfterNodeSave();
        }

        this.Response.Write(info);
    }
}