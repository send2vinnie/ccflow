using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BP.WF
{
     
    public class Work
    {
        public int NodeID = 0;
        public Work()
        {
        }
        /// <summary>
        /// 工作类
        /// </summary>
        /// <param name="fk_no"></param>
        public Work(int nodeId, int workid)
        {
            this.NodeID = nodeId;
            this.OID = workid;

            string sql = "SELECT * FROM " + this.PTable + " WHERE OID=" + workid;
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                throw new Exception("@无此节点数据.");

            this.FID = int.Parse(dt.Rows[0]["FID"].ToString());
            this.FK_Dept = dt.Rows[0]["FK_Dept"].ToString();
            this.RDT = dt.Rows[0]["RDT"].ToString();
            this.CDT = dt.Rows[0]["CDT"].ToString();
            this.Rec = dt.Rows[0]["Rec"].ToString();
            this.NodeState = int.Parse(dt.Rows[0]["NodeState"].ToString());
            this.Title = dt.Rows[0]["Title"].ToString();
        }
        /// <summary>
        /// 执行更新
        /// </summary>
        public void Update()
        {
            BP.DA.DBAccess.RunSQL("UPDATE " + this.PTable + " SET Title='" + this.Title + "', RDT='" + this.RDT + "' WHERE OID=" + this.OID);
        }
        /// <summary>
        /// 执行插入
        /// </summary>
        public int Insert()
        {
            this.OID = BP.DA.DBAccess.GenerOID();
            string sql = "INSERT INTO ND" + this.NodeID + " (OID,FID,FK_Dept,NodeState,RDT,CDT,Rec,Title) VALUES (" + this.OID + "," + this.FID + ",'" + this.FK_Dept + "'," + this.NodeState + ",'" + this.RDT + "','" + this.CDT + "','" + this.Rec + "','" + this.Title + "')";
            BP.DA.DBAccess.RunSQL(sql);
            return this.OID;
        }
        public int OID = 0;
        public int FID = 0;
        public string FK_Dept = null;
        public int NodeState = 0;
        public NodeState HisNodeState
        {
            get
            {
                return (NodeState)NodeState;
            }
            set
            {
                this.NodeState = (int)value;
            }
        }
        public string RDT = null;
        public string CDT = null;
        public string Rec = null;
        public string Title = null;
        public string PTable
        {
            get
            {
                return "ND" + this.NodeID.ToString();
            }
        }

    }
}
