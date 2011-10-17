using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.WF
{
    public class DoType
    {
        public const string OpenDoc = "OpenDoc";
        /// <summary>
        /// 启动流程
        /// </summary>
        public const string DoStartFlowByTemple = "DoStartFlowByTemple";
        /// <summary>
        /// 编辑草稿
        /// </summary>
        public const string DoStartFlow = "DoStartFlow";
        /// <summary>
        /// 打开流程
        /// </summary>
        public const string OpenFlow = "OpenFlow";
        public const string Send = "Send";
        public const string DelFlow = "DelFlow";

        
    }
    /// <summary>
    /// 启动标记
    /// </summary>
    public class StartFlag
    {
        public const string DoNewFlow = "DoNewFlow";
        public const string DoOpenFlow = "DoOpenFlow";
        public const string DoOpenDoc = "DoOpenDoc";


    }
}
