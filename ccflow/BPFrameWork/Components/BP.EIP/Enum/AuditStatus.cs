using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.EIP.Enum
{
    /// <summary>
    /// 授权状态
    /// </summary>
    public enum AuditStatus
    {
        Processing = 0,
        Passed = 1,
        Reject = 2,
        Expire = 3
    }
}
