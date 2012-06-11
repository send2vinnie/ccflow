using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public partial class XOracleQueryTool : XQueryToolBase
    {
        public override System.Data.DataTable Query<T>(T entity, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            EntityNoName entityNoName = entity as EntityNoName;
            string tableName = entityNoName.EnMap.PhysicsTable;

            string sql = "SELECT * FROM " + tableName + " WHERE 1=1 ";
            sql = string.Format(sql, rowNumFieldName);
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
            if (!string.IsNullOrEmpty(value) && columnNames.Length > 0)
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

            int startNo = (pageIndex - 1) * pageSize + 1;
            int endNo = startNo + pageSize - 1;

            sql = "SELECT T.*,ROWNUM ROW_NUM FROM ( " + sql + " ) T WHERE ROWNUM<={0}";
            sql = string.Format(sql, endNo);
            sql = "SELECT * FROM ( "+sql+") WHERE ROW_NUM>={0}";
            sql = string.Format(sql, startNo);

            return DBAccess.RunSQLReturnTable(sql);
        }
    }
}
