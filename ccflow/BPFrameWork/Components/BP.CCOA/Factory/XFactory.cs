using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
