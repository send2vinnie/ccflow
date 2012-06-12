using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class OA_Email
    {

        /// <summary>
        /// 设置所有已读
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetAllRead(string userId)
        {
            string sql = "UPDATE OA_EMAIL SET ISREAD=1 WHERE ISREAD<>1 AND CATEGORY=1 AND ADDRESSEE='{0}'";
            sql = string.Format(sql, userId);
            return DBAccess.RunSQL(sql) > 0;
        }

        /// <summary>
        /// 从收件箱删除到垃圾箱
        /// </summary>
        /// <param name="deleteIds">删除邮件ID</param>
        /// <returns></returns>
        public bool DeleteFromInputBox(string deleteIds)
        {
            string sql = "UPDATE OA_EMAIL SET CATEGORY='{0}' WHERE NO IN ({1})";
            sql = string.Format(sql, (int)MailCategory.RecycleBox, deleteIds);
            return DBAccess.RunSQL(sql) > 0;
        }

        /// <summary>
        /// 设置某个邮件已读
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool SetReaded(string emailId)
        {
            string sql = "UPDATE OA_EMAIL SET ISREAD=1 WHERE NO='{0}'";
            sql = string.Format(sql, emailId);
            return DBAccess.RunSQL(sql) > 0;
        }
    }
}
