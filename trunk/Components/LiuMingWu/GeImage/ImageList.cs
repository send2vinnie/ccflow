using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace BP.GE
{
    public class ImageList
    {
        //表格样式
        private string tableStyle;
        //图片样式
        private string picStyle;
        //项目样式
        private string itemStyle;
        //图片位置
        private TitleDirection titleDirection;
        //是否显示表头
        private bool isShowHeader;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Style"></param>
        /// <param name="ISShowHeader"></param>
        /// <param name="ISShowFooter"></param>
        public ImageList(String TableStyle, String PicStyle,String ItemStyle, TitleDirection TitleDirection, Boolean ISShowHeader)
        {
            tableStyle = TableStyle;
            picStyle = PicStyle;
            itemStyle = ItemStyle;
            titleDirection = TitleDirection;
            isShowHeader = ISShowHeader;
        }
        //实现属性
        [Bindable(true), DefaultValue(false), Description("表格样式"), NotifyParentProperty(true)]
        public string TableStyle
        {
            get { return tableStyle; }
            set { tableStyle = value; }
        }
        //实现属性
        [Bindable(true), DefaultValue(""), Description("图片样式"), NotifyParentProperty(true)]
        public string PicStyle
        {
            get { return picStyle; }
            set { picStyle = value; }
        }
        //实现属性
        [Bindable(true), DefaultValue(""), Description("列表项的样式"), NotifyParentProperty(true)]
        public string ItemStyle
        {
            get { return itemStyle; }
            set { itemStyle = value; }
        }
        //实现属性
        [Bindable(true), DefaultValue(""), Description("文本位置"), NotifyParentProperty(true)]
        public TitleDirection ImgTitlePosition
        {
            get { return titleDirection; }
            set { titleDirection = value; }
        }
        //实现属性
        [Bindable(true), DefaultValue(false), Description("是否显示表头"), NotifyParentProperty(true)]
        public bool ISShowHeader
        {
            get { return isShowHeader; }
            set { isShowHeader = value; }
        }
    }
}