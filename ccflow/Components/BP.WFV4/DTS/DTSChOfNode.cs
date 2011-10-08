using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// Method 的摘要说明
    /// </summary>
    public class DTSCHOfNode : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public DTSCHOfNode()
        {
            this.Title = "同步考核数据到wf_chofNode物理表中。";
            this.Help = "为考核所用";
            this.Warning = "执行它需要一段时间请耐心等待。";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "您确定要执行吗？";
            //HisAttrs.AddTBString("P1", null, "原密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "新密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "确认", true, false, 0, 10, 10);
        }
        /// <summary>
        /// 当前的操纵员是否可以执行这个方法
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {
            CHOfNode cn = new CHOfNode();
            cn.CheckPhysicsTable();

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                string sql = "";
                sql += "INSERT INTO WF_CHOFNODE(MyPK,FK_Node,WorkID,FID,RDT,FK_NY,CDT,REC,Emps,NodeState,FK_Dept) ";
                sql += "  (SELECT MyPK,FK_Node,OID,FID,RDT,FK_NY,CDT,REC,Emps,NodeState,FK_Dept FROM V" + fl.No + " WHERE MyPK NOT IN (SELECT MYPK FROM WF_CHOFNODE)  )";
                DBAccess.RunSQL(sql);
            }
            return "执行成功....";
        }
    }
}
