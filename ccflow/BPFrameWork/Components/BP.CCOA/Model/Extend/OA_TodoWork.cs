using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    [Serializable]
    public class OA_TodoWork
    {
        /// <summary>
        /// 审批事项
        /// </summary>
        public string FlowName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 审批状态（节点名称）
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string Starter { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string RDT { get; set; }
        /// <summary>
        /// 路径地址
        /// </summary>
        public string Url { get; set; }
    }
}
