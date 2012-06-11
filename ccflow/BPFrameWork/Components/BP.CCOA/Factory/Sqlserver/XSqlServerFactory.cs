using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XSqlServerFactory : XFactory
    {
        public override OA_NoticeTool GetOANoticeTool()
        {
            return new OA_NoticeSqlServerTool();
        }

        public override XQueryToolBase GetQueryTool()
        {
            return new XSqlServerQueryTool();
        }

        public override string GetServerTimeFunction()
        {
            return "GetDate()";
        }

        public override string GetGuidFunction()
        {
            return "";
        }
    }
}
