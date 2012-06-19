using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class XReadHelpManager
    {
        public static XReadHelperBase GetReadHelper(ClickObjType objType, string innerType = "")
        {
            switch (objType)
            {
                case ClickObjType.Email:
                    return new XEmailReadHelper(innerType);
                case ClickObjType.News:
                    return new XNewsReadHelper();
                case ClickObjType.Notice:
                    return new XNoticeReadHelper();
                case ClickObjType.Message:
                    return new XMessageReadHelper();
                default:
                    throw new Exception(objType.ToString() + "尚未实现查询方法！");
            }
        }
    }
}
