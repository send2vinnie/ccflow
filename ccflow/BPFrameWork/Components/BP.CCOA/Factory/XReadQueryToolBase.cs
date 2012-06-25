using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.DA;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class XReadQueryToolBase : XToolBase
    {

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <param name="columnNames"></param>
        /// <param name="value"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereValues"></param>
        /// <param name="rowNumFieldName"></param>
        /// <returns></returns>
        public virtual DataTable QueryAll(string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return new DataTable();
        }

        /// <summary>
        /// 获取所有已读
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <param name="columnNames"></param>
        /// <param name="value"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereValues"></param>
        /// <param name="rowNumFieldName"></param>
        /// <returns></returns>
        public virtual DataTable QueryReaded(string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize,
            IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return new DataTable();
        }

        /// <summary>
        /// 获取所有未读
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <param name="columnNames"></param>
        /// <param name="value"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereValues"></param>
        /// <param name="rowNumFieldName"></param>
        /// <returns></returns>
        public virtual DataTable QueryNotReaded(string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            return new DataTable();
        }


        public virtual int GetReadCount(string tableName, string pkColumnName, string userId, string[] columnNames, string value, ReadType readType, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            string sql = "SELECT COUNT(*) FROM " + tableName + " WHERE 1=1 ";
            sql = string.Format(sql, rowNumFieldName);
            if (readType != ReadType.All)
            {
                sql += " AND FUN_IS_READ(" + pkColumnName + ",'" + userId + "')=" + (int)readType;
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
                    sql += where;
                }
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
    }
}
