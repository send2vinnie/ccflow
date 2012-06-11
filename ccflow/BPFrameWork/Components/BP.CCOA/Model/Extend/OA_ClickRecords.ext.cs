using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;

namespace BP.CCOA
{
    public partial class OA_ClickRecords
    {
        /// <summary>
        /// 插入点击记录
        /// </summary>
        /// <param name="visitId">阅读人</param>
        /// <param name="objectType">阅读类型</param>
        /// <returns></returns>
        public bool InsertClickRecords(string visitId, int objectType)
        {
            string guidFun = XFactoryManager.CreateFactory().GetGuidFunction();
            string timeFun = XFactoryManager.CreateFactory().GetServerTimeFunction();
            string sql = "INSERT INTO OA_CLICKRECORDS(NO,OBJECTTYPE,OBJECTID,VISITDATE,CLICKS,VISITID) SELECT {0},{1},NO,{2},1,{3} FROM OA_NOTICE WHERE NO NOT IN (SELECT OBJECTID FROM OA_CLICKRECORDS WHERE VISITID='{3}')";
            sql = string.Format(sql, guidFun, objectType, timeFun, visitId);
            return DBAccess.RunSQL(sql) > 0;
        }
    }
}
