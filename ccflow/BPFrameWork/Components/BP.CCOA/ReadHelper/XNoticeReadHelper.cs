using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XNoticeReadHelper : XReadHelperBase
    {
        protected override string GetTableName()
        {
            return "OA_NOTICE";
        }

        protected override void AddOtherConditions(string userId, IDictionary<string, object> whereValues)
        {
            whereValues.Add("FUN_IS_HAVE_NOTICE_AUTHON(T.NO,T.ACCESSTYPE,'" + userId + "')", "1");
        }

    }
}
