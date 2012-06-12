using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using System.Data;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class XOracleReadQueryTool : XReadQueryToolBase
    {
        public override System.Data.DataTable QueryAll(string authonQueryCondition, string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return this.Query(authonQueryCondition, tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, ReadType.All, whereValues, rowNumFieldName);
        }

        public override System.Data.DataTable QueryNotReaded(string authonQueryCondition, string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return this.Query(authonQueryCondition, tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, ReadType.NotRead, whereValues, rowNumFieldName);
        }

        public override DataTable QueryReaded(string authonQueryCondition, string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return this.Query(authonQueryCondition, tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, ReadType.Readed, whereValues, rowNumFieldName);
        }

        private DataTable Query(string authonQueryCondition, string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, ReadType readType, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            string sql = "SELECT T.*, FUN_IS_READ(T." + pkColumnName + ",'" + userId + "') ReadFlag FROM " + tableName + " T WHERE 1=1 ";
            sql = string.Format(sql, rowNumFieldName);
            sql += " AND " + authonQueryCondition;
            if (readType != ReadType.All)
            {
                sql += " AND FUN_IS_READ(T." + pkColumnName + ",'" + userId + "')=" + (int)readType;
            }
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
            sql = "SELECT * FROM ( " + sql + ") WHERE ROW_NUM>={0}";
            sql = string.Format(sql, startNo);

            return DBAccess.RunSQLReturnTable(sql);
        }
    }
}
