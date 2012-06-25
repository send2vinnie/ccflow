using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class XEmailReadHelper : XReadHelperBase
    {
        private MailCategory m_EmailType;

        public XEmailReadHelper(string emailType)
        {
            this.m_EmailType = (MailCategory)int.Parse(emailType);
        }

        protected override string GetTableName()
        {
            return "OA_EMAIL";
        }

        protected override void AddOtherConditions(string userId, IDictionary<string, object> whereValues)
        {
            string key = string.Empty;
            string value = string.Empty;
            switch (m_EmailType)
            {
                //对于收件箱和垃圾箱需要通过删除标记来区分
                case MailCategory.InBox:
                    key = "FUN_EMAIL_IS_RECYCLE('" + userId + "',NO)";
                    value = "0";
                    this.AddConditionValue(whereValues, key, value);
                    key = "FUN_IS_HAVE_EMAIL_AUTHON(NO,'" + userId + "')";
                    value = "1";
                    this.AddConditionValue(whereValues, key, value);
                    break;
                case MailCategory.RecycleBox:
                    key = "FUN_IS_HAVE_EMAIL_AUTHON(NO,'" + userId + "')";
                    value = "1";
                    this.AddConditionValue(whereValues, key, value);
                    key = "FUN_EMAIL_IS_RECYCLE('" + userId + "',NO)";
                    value = "1";
                    this.AddConditionValue(whereValues, key, value);
                    //未从垃圾箱删除
                    key = "FUN_RECYCLE_EMAIL_IS_DELETE('" + userId + "',NO)";
                    value = "1";
                    this.AddConditionValue(whereValues, key, value);
                    break;
            }
        }

    }
}
