using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XToolBase
    {
        public virtual string GetQueryString(object objValue)
        {
            if (objValue == null)
            {
                return " AND {0}='{1}'";
            }
            string type = objValue.GetType().ToString();
            string where = " AND {0}='{1}'";
            switch (type)
            {
                case "System.Int32":
                case "System.Int16":
                case "System.Int64":
                case "System.Decimal":
                case "System.Double":
                    where = " AND {0}={1}";
                    break;
            }
            return where;
        }
    }
}
