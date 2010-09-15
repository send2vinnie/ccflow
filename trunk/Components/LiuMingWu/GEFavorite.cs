using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.GE;
using System.Web.UI;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI.WebControls;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEFavorite runat='server'></{0}:GEFavorite>")]
    public class GEFavorite : Control
    {
        [Browsable(false)]
        public string ResUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ResUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["ResUrl"].ToString();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<script type='text/javascript' src='" + ResUrl + "GE/Favorite/JScript.js'></script> \n");
            writer.Write("<link rel='stylesheet'type='text/css' href='" + ResUrl + "GE/Favorite/Stylesheet.css'/> \n");
            if (this.DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "GE/Favorite/favorate.gif");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "添加收藏");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "FavImgBtn");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            else
            {
                if (string.IsNullOrEmpty(BP.Web.WebUser.No) || string.IsNullOrEmpty(BP.Web.WebUser.Name))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "GE/Favorite/favorate.gif");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "PromptInfo()");
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, "添加收藏");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "FavImgBtn");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                }
                else
                {
                    string strWinUrl = ResUrl + "GE/Favorite/Favorite.aspx";
                    string strRefUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "GE/Favorite/favorate.gif");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "Save(\"" + strWinUrl + "\",\"" + strRefUrl + "\")");
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, "添加收藏");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "FavImgBtn");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                }
            }
        }
    }
}