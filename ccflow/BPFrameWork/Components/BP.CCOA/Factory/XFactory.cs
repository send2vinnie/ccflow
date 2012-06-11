using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Interface;

namespace BP.CCOA
{
    public abstract class XFactory
    {
        public abstract OA_NoticeTool GetOANoticeTool();

        public abstract XQueryToolBase GetQueryTool();

        /// <summary>
        /// 获取数据库时间函数
        /// </summary>
        /// <returns></returns>
        public abstract string GetServerTimeFunction();

        /// <summary>
        /// 获取Guid函数
        /// </summary>
        /// <returns></returns>
        public abstract string GetGuidFunction();

        /// <summary>
        /// 获取阅读查询工具
        /// </summary>
        /// <returns></returns>
        public abstract XReadQueryToolBase GetReadQueryTool();
    }
}
