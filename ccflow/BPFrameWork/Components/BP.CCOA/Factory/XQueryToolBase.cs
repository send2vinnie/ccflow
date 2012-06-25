using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public class XQueryToolBase
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="columnNames"></param>
        /// <param name="value"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereValues"></param>
        /// <param name="rowNumFieldName"></param>
        /// <returns></returns>
        public virtual DataTable Query<T>(T entity, string[] columnNames,
              string value, int pageIndex, int pageSize,
              IDictionary<string, object> whereValues = null,
              string rowNumFieldName = "No") where T : EntityNoName
        {
            return null;
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="columnNames"></param>
        /// <param name="value"></param>
        /// <param name="whereValues"></param>
        /// <param name="rowNumFieldName"></param>
        /// <returns></returns>
        public virtual int GetRowCount<T>(T entity, string[] columnNames,
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

        public virtual string GetQueryString(object objValue)
        {
            if (objValue==null)
            {
                return " AND {0}='{1}'";
            }
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
