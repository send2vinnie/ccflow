using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.DA;

namespace BP.CCOA
{
    public partial class EIP_Menu
    {
        #region Extend Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetJsonList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT TOP 1000 [id],[pid],[text],[iconCls],[url]");
            strSql.Append(" FROM [TJPLD_OA].[dbo].[V_MENU]");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY [id]");
            return DBAccess.RunSQLReturnDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select No,MenuNo,MenuName,Title,Img,Url,Path,Pid,Status ");
            strSql.Append(" FROM EIP_Menu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBAccess.RunSQLReturnDataSet(strSql.ToString());
        }
        #endregion
    }
}
