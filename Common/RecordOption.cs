using System;
using System.Collections.Generic;
using System.Text;

namespace Tax666.Common
{
     /// <summary>
    /// OptionMode 对记录操作模式的定义。
    /// </summary>
    public enum RecordOption
    {
        /// <summary>
        /// 添加新记录的模式；
        /// </summary>
        AddMode = 0,
        /// <summary>
        /// 修改已有的记录模式；
        /// </summary>
        ModifyMode = 1,
        /// <summary>
        /// 浏览记录的模式；
        /// </summary>
        BrowseMode = 2,
        /// <summary>
        /// 删除记录操作模式；
        /// </summary>
        DeleteMode = 3,
        /// <summary>
        /// 搜索摸索；
        /// </summary>
        SearchMode = 4,
        /// <summary>
        /// 排序－升序；
        /// </summary>
        SortAscend = 5,
        /// <summary>
        /// 排序－降序；
        /// </summary>
        SortDescend = 6
    }
}
