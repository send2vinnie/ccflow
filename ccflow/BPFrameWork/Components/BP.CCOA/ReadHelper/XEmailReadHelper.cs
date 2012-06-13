using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XEmailReadHelper : XReadHelperBase
    {
        protected override string GetTableName()
        {
            return "OA_EMAIL";
        }

        protected override string GetAuthoQueryCondtion(string userId)
        {
            return "FUN_IS_HAVE_EMAIL_AUTHON(T.NO,'" + userId + "')='1'";
        }
    }
}
