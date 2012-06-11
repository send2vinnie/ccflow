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
            OA_ClickRecords clickRecords = new OA_ClickRecords();
            int objectType = 1;
            return clickRecords.InsertClickRecords(userId, objectType);
        }
    }
}
