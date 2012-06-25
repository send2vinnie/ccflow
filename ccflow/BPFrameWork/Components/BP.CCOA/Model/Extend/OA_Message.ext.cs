using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class OA_Message
    {
        /// <summary>
        /// 设置所有已读
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetAllRead(string userId)
        {
            OA_ClickRecords clickRecords = new OA_ClickRecords();
            return ClickHelper.SetAllRead(userId, ClickObjType.Message);
        }
    }
}
