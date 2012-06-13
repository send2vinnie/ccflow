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
            switch (m_EmailType)
            {
                //对于收件箱和垃圾箱需要通过删除标记来区分
                case MailCategory.InBox:
                    whereValues.Add("FUN_EMAIL_IS_RECYCLE('" + userId + "',T.NO)", "0");
                    whereValues.Add("FUN_IS_HAVE_EMAIL_AUTHON(T.NO,'" + userId + "')", "1");
                    break;
                case MailCategory.RecycleBox:
                    whereValues.Add("FUN_IS_HAVE_EMAIL_AUTHON(T.NO,'" + userId + "')", "1");
                    whereValues.Add("FUN_EMAIL_IS_RECYCLE('" + userId + "',T.NO)", "1");
                    //未从垃圾箱删除
                    whereValues.Add("FUN_RECYCLE_EMAIL_IS_DELETE('" + userId + "',T.NO)", "1");
                    break;
            }
        }

    }
}
