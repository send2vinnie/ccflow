using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
        public virtual DataTable QueryReaded(string tableName, string pkColumnName, string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
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

    }
}
