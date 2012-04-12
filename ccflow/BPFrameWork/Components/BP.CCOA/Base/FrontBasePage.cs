using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BP.CCOA.Base
{
    public class FrontBasePage : Page
    {
        protected virtual string GoHandler { get { return ""; } }

        protected virtual string ColumnMode { get { return ""; } }

        /// <summary>
        /// 模板路径
        /// </summary>
        protected virtual string TemplatePath
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        protected virtual void Initialize()
        {
        }
    }
}
