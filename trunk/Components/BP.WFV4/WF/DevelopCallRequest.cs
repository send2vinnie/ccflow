using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// 开发者调用接口
    /// </summary>
    public class DevelopCallRequest
    {
        /// <summary>
        /// 执行发送
        /// </summary>
        /// <param name="fk_user">操作员</param>
        /// <param name="fk_node">操作的节点ID</param>
        /// <param name="ht">当前节点采集的信息以 Key,Value 存储里面。</param>
        /// <returns>发送的结果/如果发送失败抛出异常信息</returns>
        public static string SendTo(string fk_user, int fk_node, Hashtable ht)
        {
            Emp emp = new Emp(fk_user);
            Web.WebUser.SignInOfGener(emp);
            Node nd = new Node(fk_node);
            Work wk = nd.HisWork;
            Int32 workid = 0;
            if (ht.ContainsKey("WorkID"))
            {
                workid = Int32.Parse(ht["WorkID"].ToString());
            }
            wk.OID = workid;
            wk.Retrieve();


            WorkNode wn = new WorkNode(wk, nd);
            try
            {
                wk.BeforeSend(); // 发送前作逻辑检查。
            }
            catch (Exception ex)
            {
                wk.CheckPhysicsTable();
                throw ex;
            }

            //返回调的信息，注意：这里有可能会抛出异常。
            return wn.AfterNodeSave();
        }
    }
}
