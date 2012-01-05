using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// 修复表单物理表字段长度 的摘要说明
    /// </summary>
    public class ReLoadNDRpt : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public ReLoadNDRpt()
        {
            this.Title = "清除并重新装载流程报表";
            this.Help = "在节点表单发生重大变化后，用于修复数据，执行此功能不会影响数据但是会消耗时间比较长。";
            
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
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

            string msg = "";
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
              msg+=  fl.DoReloadRptData();
            }
            return "提示："+fls.Count+"个流程参与了体检，信息如下：@"+msg;
        }
    }
}
