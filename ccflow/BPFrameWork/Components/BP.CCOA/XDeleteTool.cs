using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public partial class XDeleteTool
    {
        public static bool DeleteByIds(EntityNoName entity, string ids, string pkName = "No")
        {
            string tableName = entity.EnMap.PhysicsTable;
            string sql = "DELETE FROM {0} WHERE {1} IN ({2})";
            sql = string.Format(sql, tableName, pkName, ids);
            return DBAccess.RunSQL(sql) > 0 ? true : false;
            //Paras paras = new Paras();
            //Para para = new Para("@Ids", System.Data.DbType.String, ids);
            //paras.Add(para);
            //string tableName = entity.EnMap.PhysicsTable;
            //string sql = "DELETE FROM {0} WHERE {1} IN (@Ids)";
            //sql = string.Format(sql, tableName, pkName);
            //return DBAccess.RunSQL(sql, paras) > 0 ? true : false;
        }
    }
}
