using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BP.CCOA
{
    public abstract partial class OA_NoticeTool : XToolBase
    {
        public abstract DataTable QueryNotice(string userId, string[] columnNames,
              string value, int pageIndex, int pageSize,
              IDictionary<string, object> whereValues = null,
              string rowNumFieldName = "No");
    }
}
