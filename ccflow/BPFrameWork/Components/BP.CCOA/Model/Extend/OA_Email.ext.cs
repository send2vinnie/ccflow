using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.CCOA.Enum;
using System.Data;

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
        public bool DeleteFromInputBox(string deleteIds, string userId)
        {
            string sql = "UPDATE OA_EMAILAUTH SET ISDELETE='1' WHERE FK_EMAIL IN ({0}) AND FK_ID='{1}'";
            sql = string.Format(sql, deleteIds, userId);
            return DBAccess.RunSQL(sql) > 0;
        }

        /// <summary>
        /// 从垃圾箱删除邮件
        /// </summary>
        /// <param name="deleteIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteFromRecycleBox(string deleteIds, string userId)
        {
            string sql = "UPDATE OA_EMAILAUTH SET STATUS='0' WHERE FK_EMAIL IN ({0}) AND FK_ID='{1}'";
            sql = string.Format(sql, deleteIds, userId);
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

        /// <summary>
        /// 获得邮件的收件人
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public string GetEmailReceiver(string emailId)
        {
            string sql = "SELECT NAME FROM PORT_EMP WHERE NO IN (SELECT FK_ID FROM OA_EMAILAUTH WHERE FK_EMAIL = '{0}')";
            sql = string.Format(sql, emailId);
            DataTable empTable = DBAccess.RunSQLReturnTable(sql);

            string receiver = string.Empty;

            foreach (DataRow empRow in empTable.Rows)
            {
                receiver += empRow[EIP.Port_EmpAttr.Name] + ",";
            }
            return receiver.TrimEnd(',');
        }

    }
}
