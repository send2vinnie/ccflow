using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BP.CCOA
{
    public abstract partial class XReadHelperBase
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
        public virtual DataTable QueryAll(string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            XReadQueryToolBase readQueryTool = XFactoryManager.CreateFactory().GetReadQueryTool();
            string tableName = this.GetTableName();
            string pkColumnName = this.GetPkColumnName();
            return readQueryTool.QueryAll(tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, whereValues, rowNumFieldName);
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
        public virtual DataTable QueryReaded(string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            XReadQueryToolBase readQueryTool = XFactoryManager.CreateFactory().GetReadQueryTool();
            string tableName = this.GetTableName();
            string pkColumnName = this.GetPkColumnName();
            return readQueryTool.QueryReaded(tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, whereValues, rowNumFieldName);
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
        public virtual DataTable QueryNotReaded(string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            XReadQueryToolBase readQueryTool = XFactoryManager.CreateFactory().GetReadQueryTool();
            string tableName = this.GetTableName();
            string pkColumnName = this.GetPkColumnName();
            return readQueryTool.QueryNotReaded(tableName, pkColumnName, userId, columnNames, value, pageIndex, pageSize, whereValues, rowNumFieldName);
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        protected abstract string GetTableName();

        /// <summary>
        /// 获取主键字段名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPkColumnName()
        {
            return "NO";
        }

    }
}
