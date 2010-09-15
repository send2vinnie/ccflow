using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEWebPlayer runat='server'></{0}:GEWebPlayer>")]
    public class GEWebPlayer : Control
    {
        private int _Height = 300;
        private Panel panel;
        private LinkButton lbtn;

        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        public int _Width = 320;
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        public string VideoSrc
        {
            get;
            set;
        }
        public bool AutoStart
        {
            get;
            set;
        }
        public string ImgSrc
        {
            get;
            set;
        }
        public bool IsShowHelp
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
        public GEWebPlayer()
        {
            this.Controls.Clear();
            panel = new Panel();
            //panel.ID = "tabcontent";
            panel.Attributes.Add("class", "tabcontent");
            this.Controls.Add(panel);

            lbtn = new LinkButton();
            //lbtn.ID = "lbtnDown";
            lbtn.Text = "这里";
            lbtn.Click += new EventHandler(lbtn_Click);
            this.Controls.Add(lbtn);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                WriteMediaPlayer(writer);

                if (IsShowHelp)
                {
                    WriterHelpInfo(writer);
                }
            }
            else
            {
                if (this.Visible == true)
                {
                    if (VideoSrc.ToLower().EndsWith(".flv"))
                    {
                        WriteFlvPlayer(writer);
                    }
                    else
                    {
                        WriteMediaPlayer(writer);
                    }
                    if (IsShowHelp)
                    {
                        WriterHelpInfo(writer);
                    }
                }
            }
        }
        private void WriteFlvPlayer(HtmlTextWriter writer)
        {
            writer.Write("<script type='text/javascript' src='" + ResUrl + "GE/WebPlayer/swfobject.js'></script>\r\n");
            panel.Attributes.Add("style", "width:" + Width.ToString() + "px; margin: 0px; border: solid 2px #b63961; color: #ffffff;");
            panel.RenderBeginTag(writer);
            panel.RenderEndTag(writer);
            writer.Write("<script type='text/javascript'>" +
                          "var s = new SWFObject('" + ResUrl + "GE/WebPlayer/FlvPlayer.swf" + "','playlist','" +
                           Width.ToString() + "','" + Height.ToString() + "','7');" +
                          "s.addParam('allowfullscreen','true');" +
                          "s.addVariable('autostart','" + AutoStart + "');" +
                          "s.addVariable('image','" + ImgSrc + "');" +
                          "s.addVariable('file','" + VideoSrc + "');" +
                          "s.addVariable('width','" + Width.ToString() + "');" +
                          "s.addVariable('height','" + Height.ToString() + "');" +
                          "s.write('" + panel.ClientID + "');" +
                          "</script>"
                                );
        }

        private void WriteMediaPlayer(HtmlTextWriter writer)
        {
            writer.AddAttribute("classid", "CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "5");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "border-color:#d8e6fa");
            writer.RenderBeginTag(HtmlTextWriterTag.Object);

            writer.AddAttribute("name", "AutoStart");
            writer.AddAttribute("value", AutoStart == true ? "-1" : "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "allowScriptAccess");
            writer.AddAttribute("value", "never");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "url");
            writer.AddAttribute("value", VideoSrc);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "rate");
            writer.AddAttribute("value", "1");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "balance");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "currentPosition");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "defaultFrame");
            writer.AddAttribute("value", string.Empty);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "playCount");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "currentMarker");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "invokeURLs");
            writer.AddAttribute("value", "-1");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "baseURL");
            writer.AddAttribute("value", string.Empty);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "volume");
            writer.AddAttribute("value", "50");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "mute");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "uiMode");
            writer.AddAttribute("value", "full");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "stretchToFit");
            writer.AddAttribute("value", "-1");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "windowlessVideo");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "enabled");
            writer.AddAttribute("value", "-1");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "enableContextMenu");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "fullScreen");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "SAMIStyle");
            writer.AddAttribute("value", string.Empty);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "SAMILang");
            writer.AddAttribute("value", string.Empty);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "SAMIFilename");
            writer.AddAttribute("value", string.Empty);
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "enableErrorDialogs");
            writer.AddAttribute("value", "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "_cx");
            writer.AddAttribute("value", "17198");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.AddAttribute("name", "_cy");
            writer.AddAttribute("value", "13229");
            writer.RenderBeginTag(HtmlTextWriterTag.Param);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        private void WriterHelpInfo(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "HelpInfo");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("如果视频不能正常播放请点击");
            lbtn.RenderControl(writer);
            writer.Write("下载解码器!");
            writer.RenderEndTag();
        }

        private void lbtn_Click(object sender, EventArgs e)
        {
            string filename = ResUrl + "GE/WebPlayer/RealPackCNGR.rar";
            FileInfo file = new FileInfo(this.Page.Server.MapPath(filename));
            if (file.Exists)
            {
                this.Page.Response.Clear();
                this.Page.Response.ClearHeaders();

                this.Page.Response.ContentType = "application/rar";
                this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

                this.Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + this.Page.Server.UrlPathEncode(file.Name));
                this.Page.Response.TransmitFile(filename);

                this.Page.Response.End();
            }
            else
            {
                BP.GE.GeFun.ShowMessage(this.Page, "JS1", "文件不存在!");
            }
        }
    }
}
