using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;

namespace BP.CCOA
{
    public partial class OA_NoticeSqlServerTool : OA_NoticeTool
    {
        public override System.Data.DataTable QueryNotice(string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            string sql = "SELECT row_number () OVER (ORDER BY {0}) AS RowNum,* FROM OA_NOTICE WHERE 1=1 ";
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
    }
}
