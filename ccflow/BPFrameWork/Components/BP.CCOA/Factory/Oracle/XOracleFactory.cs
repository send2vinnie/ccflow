using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XOracleFactory : XFactory
    {
        public override OA_NoticeTool GetOANoticeTool()
        {
            return new OA_NoticeOracleTool();
        }

        public override XQueryToolBase GetQueryTool()
        {
            return new XOracleQueryTool();
        }

        public override string GetServerTimeFunction()
        {
            return "SysDate";
        }

        public override string GetGuidFunction()
        {
            return "SYS_GUID()";
        }

        public override XReadQueryToolBase GetReadQueryTool()
        {
            return new XOracleReadQueryTool();
        }
    }
}
