using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public partial class XReadHelpManager
    {
        public static XReadHelperBase GetReadHelper(ClickObjType objType)
        {
            switch (objType)
            {
                case ClickObjType.Email:
                    return new XEmailReadHelper();
                case ClickObjType.News:
                    return new XNewsReadHelper();
                case ClickObjType.Notice:
                    return new XNoticeReadHelper();
                default:
                    throw new Exception(objType.ToString() + "尚未实现查询方法！");
            }
        }
    }
}
