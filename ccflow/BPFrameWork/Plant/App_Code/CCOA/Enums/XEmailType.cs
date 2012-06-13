using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum XEmailType
{
    /// <summary>
    /// 收件箱
    /// </summary>
    InBox = 0,
    /// <summary>
    /// 草稿箱
    /// </summary>
    DraftBox = 1,
    /// <summary>
    /// 发件箱
    /// </summary>
    OutBox = 2,
    /// <summary>
    /// 垃圾箱
    /// </summary>
    RecycleBox = 3
}