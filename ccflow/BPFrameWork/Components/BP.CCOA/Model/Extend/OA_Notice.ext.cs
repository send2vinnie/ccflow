using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.DA;

namespace BP.CCOA
{
    public partial class OA_Notice
    {
        public DataTable QueryNotice(string userId, string[] columnNames, string value, int pageIndex, int pageSize, IDictionary<string, object> whereValues = null, string rowNumFieldName = "No")
        {
            OA_NoticeTool noticeTool = XFactoryManager.CreateFactory().GetOANoticeTool();
            return noticeTool.QueryNotice(userId, columnNames, value, pageIndex, pageSize, whereValues, rowNumFieldName);
        }

        /// <summary>
        /// 设置所有已读
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetAllRead(string userId)
        {
            string guidFun = XFactoryManager.CreateFactory().GetGuidFunction();
            string timeFun = XFactoryManager.CreateFactory().GetServerTimeFunction();
            string objectType = "1";
            string sql = "INSERT INTO OA_CLICKRECORDS(NO,OBJECTTYPE,OBJECTID,VISITDATE,CLICKS,VISITID) SELECT {0},{1},NO,{2},1,{3} FROM OA_NOTICE WHERE NO NOT IN (SELECT OBJECTID FROM OA_CLICKRECORDS WHERE VISITID='{3}')";
            sql = string.Format(sql, guidFun, objectType, timeFun, userId);
            return DBAccess.RunSQL(sql) > 0;
        }
    }
}
