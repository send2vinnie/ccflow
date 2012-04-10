using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA.Enum
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public enum TypeOfArticle : int
    {
        /// <summary>
        /// 原创型文章
        /// </summary>
        NormalArticle = 1,

        /// <summary>
        /// 引用文章
        /// </summary>
        LinkArticle = 8,

        /// <summary>
        /// 共享文章
        /// </summary>
        ShareArticle = 10,

        /// <summary>
        /// WAP文章
        /// </summary>
        WapArticle = 16

    }
}
