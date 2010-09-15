using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GeTab runat='server'></{0}:GeTab>")]
    public class GETab : Control, INamingContainer
    {
        private Panel panel;
        private Tabs items;

        [Browsable(false)]
        public Tabs Items
        {
            get
            {
                if (items == null)
                {
                    items = new Tabs();
                }
                return items;
            }
            set
            {
                items = value;
            }
        }

        public eMouseAction MouseAction
        {
            get;
            set;
        }
        private string _StrMore = string.Empty;
        public string StrMore
        {
            get
            {
                return _StrMore;
            }
            set
            {
                _StrMore = value;
            }
        }
        public int SelectedIndex
        {
            get;
            set;
        }
        public string _Title = string.Empty;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public int Height
        {
            get;
            set;
        }
        [Browsable(false)]
        public string ResUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ResUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["ResUrl"].ToString();
            }
        }
        private ColorStyle _Style = ColorStyle.Default;
        public ColorStyle ColorStyle
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
            }
        }
        private Pos _Pos = Pos.Top;
        public Pos Pos
        {
            get
            {
                return _Pos;
            }
            set
            {
                _Pos = value;
            }
        }
        [Browsable(false)]
        public string Style
        {
            get
            {
                return this.ColorStyle.ToString() + this.Pos.ToString();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string strCSS = "<link href='" + ResUrl + "GE/Tab/" + this.Style + ".css" + "' rel='stylesheet' type='text/css' />";
            string strJS = "<script src='" + ResUrl + "GE/Tab/Tab.js' type='text/javascript'></script>";
            writer.Write(strCSS + "\n");
            writer.Write(strJS + "\n");
            if (this.DesignMode)
            {
                DesignView(writer);
            }
            else
            {
                RunTimeView(writer);
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            panel = new Panel();
            panel.ID = "tabcontent";
            panel.Attributes.Add("class", this.Style + "_tabcontent");
            panel.Height = Height;
            this.Controls.Add(panel);
            this.ChildControlsCreated = true;
        }

        private void DesignView(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabmenu");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            if (Title.Length>0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabTitle");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(Title);
                writer.RenderEndTag();
            }
            //
            for (int i = 0; i <= 4; i++)
            {
                if (i == SelectedIndex)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_cli");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("[标题" + i.ToString() + "]");
                writer.RenderEndTag();
            }
            if (StrMore.Length>0)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(StrMore);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "tabcontent");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabcontent");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "height:" + Height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            for (int i = 0; i <= 4; i++)
            {
                if (i == SelectedIndex)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "tabul");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_hidden");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("[内容" + i.ToString() + "]");
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void RunTimeView(HtmlTextWriter writer)
        {
            string strMouseAction = "onmouseover";
            if (MouseAction == eMouseAction.onclick)
            {
                strMouseAction = "onclick";
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);                   //<div>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabmenu");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);                   //<div>
            //Header
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);                    //<ul>

            if (this.Title.Length>0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "tabTitle");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabTitle");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(Title);
                writer.RenderEndTag();
            }
            for (int i = 0; i < Items.Count; i++)
            {
                writer.AddAttribute(strMouseAction, "tabChange(this,'" + panel.ClientID + "','" + this.Style + "')");
                if (i == SelectedIndex)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_cli");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);                //<li>
                writer.Write(Items[i].Title);
                writer.RenderEndTag();                                      //</li>
            }
            if (this.StrMore.Length>0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_tabMore");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(this.StrMore);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();                                          //</ul>
            writer.RenderEndTag();                                          //</div>
            //writer.AddAttribute(HtmlTextWriterAttribute.Id, "tabcontent");
            //writer.RenderBeginTag(HtmlTextWriterTag.Div);
            panel.RenderBeginTag(writer);                                   //<div>

            if (this.Title.Length>0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_hidden");
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "tabul");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Style + "_hidden");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);        //<ul>
                writer.RenderBeginTag(HtmlTextWriterTag.Li);        //<li>
                writer.Write(Items[i].Content);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            panel.RenderEndTag(writer);
            writer.RenderEndTag();
        }
    }
}