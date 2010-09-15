using System.ComponentModel;
using System.Web.UI;

namespace BP.GE.Ctrl
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UrlList
    {
        /// <summary>
        /// URL参数名称
        /// </summary>
        [Description("URL参数名称")]
        [NotifyParentProperty(true)]
        public string ParaName
        {
            get;
            set;
        }
        /// <summary>
        /// URL参数类型
        /// </summary>
        [Description("URL参数类型")]
        [NotifyParentProperty(true)]
        public ParamType ValueFrom
        {
            get;
            set;
        }
    }
}