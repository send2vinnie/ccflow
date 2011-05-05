using System;
using System.Collections.Generic;
using System.Text;
using BP.Port;

namespace BP.OA
{
    public class TAUser:BP.Web.WebUser
    {
        /// <summary>
        /// 当前共享的用户
        /// </summary>
        public static string ShareUserNo
        {
            get
            {
                return GetSessionByKey("ShareUserNo", Web.WebUser.No);
            }
            set
            {
                Emp emp = new Emp(value);
                SetSessionByKey("ShareUserNo", emp.No);
                SetSessionByKey("ShareUserName", emp.Name);
            }
        }
        /// <summary>
        /// 当前共享用户名称
        /// </summary>
        public static string ShareUserName
        {
            get
            {
                return GetSessionByKey("ShareUserName", Web.WebUser.Name);
            }
        }
        /// <summary>
        /// (OA 共享信息用到 )是否是当前的用户
        /// </summary>
        public static bool IsCurrUser
        {
            get
            {
                if (Web.WebUser.No == ShareUserNo)
                    return true;
                else
                    return false;
            }
        }
    }
}
