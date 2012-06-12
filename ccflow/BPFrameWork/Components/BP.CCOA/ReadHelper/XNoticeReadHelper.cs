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

        protected override string GetAuthoQueryCondtion(string userId)
        {
            return "FUN_IS_HAVE_NOTICE_AUTHON(T.NO,T.ACCESSTYPE,'" + userId + "')='1'";
        }
    }
}
