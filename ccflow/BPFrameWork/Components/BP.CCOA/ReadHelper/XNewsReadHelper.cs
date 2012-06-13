using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XNewsReadHelper : XReadHelperBase
    {
        protected override string GetTableName()
        {
            return "OA_NEWS";
        }

        protected override void AddOtherConditions(string userId, IDictionary<string, object> whereValues)
        {

        }
    }
}
