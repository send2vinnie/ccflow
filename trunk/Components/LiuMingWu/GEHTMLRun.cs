using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Configuration;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEHTMLRun runat='server'></{0}:GEHTMLRun>")]
    public class GEHTMLRun : Control
    {
        private ImageRunItems items;
        [Browsable(false)]
        public ImageRunItems Items
        {
            get
            {
                if (items == null)
                    items = new ImageRunItems();
                return items;
            }
            set
            {
                items = value;
            }
        }

        public eDirection Direction
        {
            get;
            set;
        }
        private int _Width;
        public int Width
        {
            get
            {
                if (_Width <= 0)
                    _Width = 200;
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        private int _Height;
        public int Height
        {
            get
            {
                if (_Height <= 0)
                    _Height = 200;
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        [Browsable(false)]
        public string ResUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ResUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["ResUrl"].ToString();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, "#");
                writer.AddAttribute(HtmlTextWriterAttribute.Width, Width.ToString() + "px");
                writer.AddAttribute(HtmlTextWriterAttribute.Height, Height.ToString() + "px");
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            else
            {
                string strCSS = string.Empty;
                string strJS = string.Empty;
                if (Direction == eDirection.TopToBottom)
                {
                    strCSS = "<link href='" + ResUrl + "GE/ImageRun/ImageRun_Top.css' rel='stylesheet' type='text/css' />";
                    strJS = "<script type='text/javascript' src='" + ResUrl + "GE/ImageRun/ImageRun_Top.js'></script>";
                }
                else if (Direction == eDirection.BottomToTop)
                {
                    strCSS = "<link href='" + ResUrl + "GE/ImageRun/ImageRun_Bottom.css' rel='stylesheet' type='text/css' />";
                    strJS = "<script type='text/javascript' src='" + ResUrl + "GE/ImageRun/ImageRun_Bottom.js'></script>";
                }
                else if (Direction == eDirection.LeftToRight)
                {
                    strCSS = "<link href='" + ResUrl + "GE/ImageRun/ImageRun_Left.css' rel='stylesheet' type='text/css' />";
                    strJS = "<script type='text/javascript' src='" + ResUrl + "GE/ImageRun/ImageRun_Left.js'></script>";
                }
                else if (Direction == eDirection.RightToLeft)
                {
                    strCSS = "<link href='"+ResUrl+"GE/ImageRun/ImageRun_Right.css' rel='stylesheet' type='text/css' />";
                    strJS = "<script type='text/javascript' src='" + ResUrl + "GE/ImageRun/ImageRun_Right.js'></script>";
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "demo");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, string.Format("height:{0}px;width:{1}px;", Height.ToString(), Width.ToString()));
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                if (Direction == eDirection.LeftToRight || Direction == eDirection.RightToLeft)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "indemo");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "demo1");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (ImageRunItem item in Items)
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Href, item.strHref);
                    //writer.RenderBeginTag(HtmlTextWriterTag.A);
                    //writer.AddAttribute(HtmlTextWriterAttribute.Src, item.strImgSrc);
                    //writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                    //writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    //writer.RenderEndTag();
                    //writer.RenderEndTag();

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, item.strHref);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(item.strContent);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "demo2");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
                if (Direction == eDirection.LeftToRight || Direction == eDirection.RightToLeft)
                {
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.Write(strCSS + "\n");
                writer.Write(strJS + "\n");
            }
        }
    }
}
