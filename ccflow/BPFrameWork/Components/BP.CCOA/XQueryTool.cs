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
        public static DataTable Query<T>(T entity, string[] columnNames,
            string value, int pageIndex, int pageSize,
            IDictionary<string, object> whereValues = null,
            string rowNumFieldName = "No") where T : EntityNoName
        {
            EntityNoName entityNoName = entity as EntityNoName;
            string tableName = entityNoName.EnMap.PhysicsTable;

            string sql = "SELECT row_number () OVER (ORDER BY {0}) AS RowNum,* FROM " + tableName + " WHERE 1=1 ";
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

            sql = "SELECT * FROM ( " + sql + " ) T WHERE RowNum BETWEEN {0} AND {1}";
            sql = string.Format(sql, startNo, endNo);

            return DBAccess.RunSQLReturnTable(sql);
        }

        public static int GetRowCount<T>(T entity, string[] columnNames,
            string value,
            IDictionary<string, object> whereValues = null,
            string rowNumFieldName = "No") where T : EntityNoName
        {
            //获取记录条数
            EntityNoName entityNoName = entity as EntityNoName;
            string tableName = entityNoName.EnMap.PhysicsTable;

            string sql = "SELECT COUNT(*) FROM " + tableName + " WHERE 1=1 ";
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

            return DBAccess.RunSQLReturnValInt(sql);
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
