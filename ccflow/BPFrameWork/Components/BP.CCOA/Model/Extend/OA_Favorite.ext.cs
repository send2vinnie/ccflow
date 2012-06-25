using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using System.Data;

namespace BP.CCOA
{
    public partial class OA_Favorite : EntityNoName
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select No,Name,Seq,SiteUrl,Target,Description ");
            strSql.Append(" FROM OA_Favorite WHERE FK_EMP='" + userId + "' ");

            return DBAccess.RunSQLReturnTable(strSql.ToString());
        }
    }
}