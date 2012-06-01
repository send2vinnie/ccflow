using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;

namespace BP.EIP
{
    public partial class XDBHelper : DBAccess
    {
        /// <summary>
        /// 批量执行sql语句
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static int RunSql(IList<string> sqlList)
        {
            XDBHelper dbHelper = null;
            switch (AppCenterDBType)
            {
                case DBType.SQL2000_OK:
                    dbHelper = new XSqlServerHelper();
                    break;
                case DBType.Oracle9i:
                    dbHelper = new XOracleHelper();
                    break;
                case DBType.MySQL:
                //return RunSQL_200705_MySQL(sql);
                case DBType.Access:
                //return RunSQL_200705_OLE(sql);
                default:
                    dbHelper = null;
                    break;
            }
            if (dbHelper != null)
            {
                return dbHelper.RunSqlByTrans(sqlList);
            }
            else
            {
                throw new Exception("发现未知的数据库连接类型！");
            }
        }

        public virtual int RunSqlByTrans(IList<string> sqlList)
        {
            return -1;
        }
    }
}
