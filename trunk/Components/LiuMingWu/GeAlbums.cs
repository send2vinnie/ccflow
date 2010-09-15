using System.Web.UI;
using System.Data;
using System;
using BP;
using System.ComponentModel;
using System.Configuration;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEAlbums runat='server'></{0}:GEAlbums>")]
    [PersistChildren(false)]
    [ParseChildren(true, "UrlParas")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class GEAlbums : CtrlNoPagingDB
    {
        private AlbumsAttr _MyAttribute;
        [Bindable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AlbumsAttr MyAttribute
        {
            get
            {
                if (_MyAttribute == null)
                {
                    _MyAttribute = new AlbumsAttr(true, string.Empty, string.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, 640, 400, 50, 80, enumLR.Right);
                }
                return _MyAttribute;
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
        public override void RenderDesignView(HtmlTextWriter writer)
        {

        }

        public override void RenderRuntimeView(HtmlTextWriter writer, DataTable dt)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "tbody");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "txt_1");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(MyAttribute.Title);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "mainbody");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, "#");
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, "alt");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "mainphoto");
            writer.AddAttribute(HtmlTextWriterAttribute.Width, MyAttribute.ViewWidth.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Height, MyAttribute.ViewHeight.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Rel, "http://www.badiu.com");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "http://www.163.com");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();

            //左翻页
            writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "Ge/Albums/images/goleft.gif");
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "11px");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, "56px");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "goleft");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            //右翻页
            writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "Ge/Albums/images/goright.gif");
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "11px");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, "56px");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "goright");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "photos");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "showArea");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //SRC: 缩略图地址 
            //REL: 大图地址 
            //NAME: 网址 

            foreach (DataRow dr in dt.Rows)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, Convert.ToString(dr[MyAttribute.ShortPic]));
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, Convert.ToString(dr[MyAttribute.alt]));
                writer.AddAttribute(HtmlTextWriterAttribute.Width, MyAttribute.ShortWidth.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Height, MyAttribute.ShortHeight.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Rel, Convert.ToString(dr[MyAttribute.ViewPic]));
                writer.AddAttribute(HtmlTextWriterAttribute.Name, Convert.ToString(dr[MyAttribute.URL]));
                if (!string.IsNullOrEmpty(MyAttribute.Describe))
                {
                    string strValue = MyAttribute.Describe;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string strName = "@" + dc.ColumnName;
                        strValue = strValue.Replace(strName, dr[dc].ToString());
                    }
                    writer.AddAttribute("showNote", "true");
                    writer.AddAttribute("Note", strValue);
                    if (MyAttribute.NotePosition == enumLR.Right)
                    {
                        writer.AddAttribute("NotePosition", "Right");
                    }
                    else 
                    {
                        writer.AddAttribute("NotePosition", "Left");
                    }
                }
                else
                {
                    writer.AddAttribute("showNote", "false");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            //显示注释信息的层
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "Note");
            if (MyAttribute.NotePosition == enumLR.Right)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "NoteOnRight");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "NoteOnLeft");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();

            writer.Write("\r\n <link href='" + ResUrl + "GE/Albums/Stylesheet.css' rel='stylesheet' type='text/css'/> ");
            writer.Write("\r\n <script type='text/javascript' src='" + ResUrl + "GE/Albums/Albums.js'></script> ");
        }
    }
}