using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;


namespace BP.WF
{
    /// <summary>
    /// Method 的摘要说明
    /// </summary>
    public class MDCheck : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public MDCheck()
        {
            this.Title = "修复所有的节点数据。";
            this.Help = "用于节点升级调用";
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
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            // 让其执行二次。
            foreach (Node nd in nds)
                nd.RepariMap();

            foreach (Node nd in nds)
                nd.Update();

            // 让其执行二次。
            foreach (Node nd in nds)
                nd.Update();

            foreach (Node nd in nds)
            {
                

                Work wk = nd.HisWork;
                wk.CheckPhysicsTable();

                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + wk.NodeID);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();
                }
            }
            return "执行成功";
        }
    }
}
