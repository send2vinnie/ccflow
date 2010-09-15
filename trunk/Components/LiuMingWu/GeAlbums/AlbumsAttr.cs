using System;
using System.ComponentModel;

namespace BP.GE
{
    public class AlbumsAttr
    {
        public AlbumsAttr(bool isscale,string shortpic,string viewpic,string url,string title,string alt
            ,string describe,int viewwidth,int viewheight,int shortheight, int shortwidth,enumLR position)
        {
            this.IsScale = isscale;
            this.ShortPic = shortpic;
            this.ViewPic = viewpic;
            this.URL = url;
            this.Title = title;
            this.alt = alt;
            this.ViewWidth = viewwidth;
            this.ViewHeight = viewheight;
            this.ShortHeight = shortheight;
            this.Describe = describe;
            this.ShortWidth = shortwidth;
            this.NotePosition = position;
        }

        #region

        private bool _IsScale = true;
        [Bindable(true), DefaultValue(true), Description("是否按比例显示"), NotifyParentProperty(true)]
        public bool IsScale
        {
            get
            {
                return _IsScale;
            }
            set
            {
                _IsScale = value;
            }
        }

        [Bindable(true), DefaultValue(""), Description("绑定到缩略图的字段"), NotifyParentProperty(true)]
        public string ShortPic
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(""), Description("绑定到预览图片的字段"), NotifyParentProperty(true)]
        public string ViewPic
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(""), Description("绑定到URl连接的字段"), NotifyParentProperty(true)]
        public string URL
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(""), Description("标题"), NotifyParentProperty(true)]
        public string Title
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(""), Description("alt显示文字"), NotifyParentProperty(true)]
        public string alt
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(640), Description("预览图片的宽度"), NotifyParentProperty(true)]
        public int ViewWidth
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(400), Description("预览图片的高度"), NotifyParentProperty(true)]
        public int ViewHeight
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(50), Description("缩略图的高度"), NotifyParentProperty(true)]
        public int ShortHeight
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(80), Description("缩略图的宽度"), NotifyParentProperty(true)]
        public int ShortWidth
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(""), Description("描述文本"), NotifyParentProperty(true)]
        public string Describe
        {
            get;
            set;
        }

        [Bindable(true), DefaultValue(enumLR.Right), Description("注释文本显示的位置"), NotifyParentProperty(true)]
        public enumLR NotePosition
        {
            get;
            set;
        }
        #endregion
    }
}
