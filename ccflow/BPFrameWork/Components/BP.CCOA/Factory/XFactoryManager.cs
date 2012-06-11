using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA
{
    public partial class XFactoryManager
    {
        private static string DbType = SystemConfig.AppSettings["AppCenterDBType"];

        public static XFactory CreateFactory()
        {
            switch (DbType.ToUpper())
            {
                case "ORACLE":
                    return new XOracleFactory();
                case "SqlServer":
                    return new XSqlServerFactory();
                default:
                    return null;
            }
        }
    }
}
