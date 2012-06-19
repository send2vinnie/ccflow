using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XMessageReadHelper : XReadHelperBase
    {
        public XMessageReadHelper()
        {

        }

        protected override string GetTableName()
        {
            return "OA_MESSAGE";
        }

        protected override void AddOtherConditions(string userId, IDictionary<string, object> whereValues)
        {
            
        }
    }
}
