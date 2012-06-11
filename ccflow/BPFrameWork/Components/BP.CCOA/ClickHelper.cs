using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Interface;
using BP.CCOA.Enum;
using System.Data;
using BP.DA;

namespace BP.CCOA
{
    public static class ClickHelper
    {
        public static void ClickRecord(ClickObjType objectType, string objectId, string visitId)
        {
            BP.CCOA.OA_ClickRecords click = new OA_ClickRecords();
            click.No = Guid.NewGuid().ToString();
            click.ObjectId = objectId;
            click.ObjectType = (int)objectType;
            click.VisitId = visitId;
            click.VisitDate = DateTime.Now.ToString();

            click.Insert();
        }

        public static bool IsReaded(string objectId, string visitId)
        {
            throw new NotImplementedException();
        }

        public static string GetVisitTime(string objectId, string visitId)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetObjectRecord(string objectId)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetVisitedRecord(string visitId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置所有为已读
        /// </summary>
        /// <param name="visitId">阅读人</param>
        /// <param name="objectType">阅读类型</param>
        /// <returns></returns>
        public static bool SetAllRead(string visitId, ClickObjType objectType)
        {
            string guidFun = XFactoryManager.CreateFactory().GetGuidFunction();
            string timeFun = XFactoryManager.CreateFactory().GetServerTimeFunction();
            string sql = @"INSERT INTO OA_CLICKRECORDS(NO,OBJECTTYPE,OBJECTID,VISITDATE,CLICKS,VISITID) 
                           SELECT {0},{1},NO,{2},1,{3} 
                           FROM OA_NOTICE WHERE NO NOT IN (SELECT OBJECTID FROM OA_CLICKRECORDS WHERE VISITID='{3}')";
            sql = string.Format(sql, guidFun, (int)objectType, timeFun, visitId);
            return DBAccess.RunSQL(sql) > 0;
        }
    }
}
