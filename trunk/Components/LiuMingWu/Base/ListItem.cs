using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace BP.GE.Ctrl
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [PersistChildren(false)]
    [ParseChildren(true, "UrlListItems")]  
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [ToolboxItem(false)]
    public class MyListItem:Control
    {
        #region 自定义样式
        //绑定到显示文本的字段
        [Description("绑定要显示的字段")]
        [NotifyParentProperty(true)]
        public string DataTextField
        {
            get;
            set; 
        }
        /// <summary>
        /// 绑定到Text属性的值的格式设置.例如,"Hello:{0}"
        /// </summary>
        [Description(" 绑定到Text属性的值的格式设置.例如,\"Hello:{0}\"")]
        [NotifyParentProperty(true)]
        public string DataFormatString
        {
            get;
            set;
        }

        //列样式
        [Description("列样式")]
        [NotifyParentProperty(true)]
        public string ItemStyle
        {
            get;
            set;
        }

        //头标题
        [Description("表头内文本")]
        [NotifyParentProperty(true)]
        public string HeaderText
        {
            get;
            set;
        }
        //头样式
        [Description("表头样式")]
        [NotifyParentProperty(true)]
        public string HeaderStyle
        {
            get;
            set;
        }
        /// <summary>
        /// URL参数类型
        /// </summary>
        private UrlItems items;
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UrlItems UrlListItems
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new UrlItems();
                }
                return items;
            }
            set
            {
                items = value;
            }
        }
        #endregion
    }
}