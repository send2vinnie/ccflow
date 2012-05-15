using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public partial class XQueryTool
    {
        public static DataTable Query<T>(T entity, string[] columnNames, string value, IDictionary<string, object> whereValues = null) where T : EntityNoName
        {
            EntityNoName entityNoName = entity as EntityNoName;
            string tableName = entityNoName.EnMap.PhysicsTable;
            string sql = "SELECT * FROM " + tableName + " WHERE 1=1 ";
            if (whereValues != null)
            {
                string where = "";
                foreach (KeyValuePair<string, object> keyAndValue in whereValues)
                {
                    string fieldName = keyAndValue.Key;
                    object objValue = keyAndValue.Value;
                    where = GetQueryString(objValue);
                    where = string.Format(where, fieldName, objValue);
                }
                sql += where;
            }
            if (value != string.Empty && columnNames.Length > 0)
            {
                sql += " AND (";
                int loopNo = 0;
                foreach (string columnName in columnNames)
                {
                    loopNo += 1;
                    if (loopNo != 1)
                    {
                        sql += " OR ";
                    }
                    string strSql = " {0} LIKE '%{1}%' ";
                    strSql = string.Format(strSql, columnName, value);
                    sql += strSql;
                }

                sql += ")";
            }
            return DBAccess.RunSQLReturnTable(sql);
        }

        private static string GetQueryString(object objValue)
        {
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
