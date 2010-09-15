using System;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEMenu runat=server></{0}:GEMenu>")]
    public class GEMenu : Control
    {
        public GEMenu()
        {
            ItemWidth = 200;
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(eDisplayMode.Horizontal)]
        [Localizable(true)]
        [Description("显示方向")]
        [NotifyParentProperty(true)]
        [BrowsableAttribute(true)]
        public eDisplayMode DisplayMode
        {
            get;
            set;
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("200")]
        [Localizable(true)]
        [Description("菜单项的宽度")]
        [NotifyParentProperty(true)]
        [BrowsableAttribute(true)]
        public int ItemWidth
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

        protected DataTable GetDataTable()
        {
            string strSql = "select * from Navigate order by no";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
            return dt;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<link href='" + ResUrl + "GE/Menu/Stylesheet.css' rel='stylesheet' type='text/css' />");
            writer.WriteLine();
            if (this.DesignMode)
            {
                MyRenderAtDesignMode(writer);
            }
            else
            {
                MyRenderAtRuntime(writer);
            }
            writer.WriteLine();
            writer.Write(" <script src='" + ResUrl + "GE/Menu/ocscript.js' type='text/javascript'></script>");
        }

        #region 设计时呈现样式

        private void MyRenderAtDesignMode(HtmlTextWriter writer)
        {
            if (this.DisplayMode == eDisplayMode.Horizontal)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "z-index: 999999; position: relative;");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "z-index: 999999; position: relative;width:" + ItemWidth.ToString() + "px;");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imrcmain0 imgl");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imcm imde");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "imouter0");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "imenus0");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imatm");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 145px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeam");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("Who We Are");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imatm");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 145px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeam");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("Who We Are");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imatm");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 145px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeam");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("Who We Are");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imatm");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 145px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeam");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("Who We Are");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        #endregion

        #region 运行时显示呈现样式
        /// <summary>
        /// 自定义运行时呈现样式
        /// </summary>
        /// <param name="writer">writer</param>
        private void MyRenderAtRuntime(HtmlTextWriter writer)
        {
            DataTable dt = GetDataTable();
            //  <div class="imrcmain0 imgl" style="width: 100%; z-index: 999999; position: relative;">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imrcmain0 imgl");

            if (this.DisplayMode == eDisplayMode.Horizontal)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "z-index: 999999; position: relative");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "z-index: 999999; position: relative;width:" + ItemWidth.ToString() + "px;");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //  <div class="imcm imde" id="imouter0">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imcm imde");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "imouter0");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            //  <ul id="imenus0">
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "imenus0");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            write(writer, dt, string.Empty);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        #region 生成菜单项
        /// <summary>
        /// 循环递归生成菜单项
        /// </summary>
        /// <param name="writer">Writer</param>
        /// <param name="dt">菜单数据</param
        /// <param name="strParent">父节点</param>
        private void write(HtmlTextWriter writer, DataTable dt, string strParent)
        {
            DataRow[] drs = dt.Select("[Parent]='" + strParent + "'");
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    string strNo = drs[i]["No"].ToString().Trim();
                    DataRow[] drs2 = dt.Select("[Parent]='" + strNo + "'");
                    //  <li class="imatm" style="width: 145px;">
                    if (this.DisplayMode == eDisplayMode.Horizontal)
                    {
                        if (strNo.Length <= 3)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imatm");
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + ItemWidth.ToString() + "px");
                        }
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    //  <a class="" href="#">
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, drs[i]["Href"].ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    //  <span class="imea imeam"><span></span></span>
                    if (drs2.Length > 0)
                    {
                        if (this.DisplayMode == eDisplayMode.Horizontal)
                        {
                            if (strNo.Length <= 3)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeam");
                            }
                            else
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeas");
                            }
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "imea imeas");
                        }
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.RenderEndTag();
                    writer.RenderEndTag();//</span>
                    writer.Write(drs[i]["Name"].ToString());
                    writer.RenderEndTag();//</a>
                    if (drs2.Length > 0)
                    {
                        //  <div class="imsc">
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "imsc");
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        //  <div class="imsubc" style="width: 140px; top: -1px; left: 0px;">
                        //  <div class="imsubc" style="width: 140px; top: -20px; left: 133px;">
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "imsubc");
                        int Left = ItemWidth - 5;
                        if (this.DisplayMode == eDisplayMode.Horizontal)
                        {
                            if (strNo.Length <= 3)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: " + ItemWidth + "px; top: -1px; left: 0px;");
                            }
                            else
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + ItemWidth + "px; top: -20px; left: " + Left + "px;");
                            }
                        }
                        else
                        {
                            if (strNo.Length <= 3)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: " + ItemWidth + "px; top: -20px; left: " + Left + "px;");
                            }
                            else
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + ItemWidth + "px; top: -20px; left: " + Left + "px;");
                            }
                        }
                        //  <div class="imunder">
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "imunder");
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                        write(writer, dt, drs[i]["No"].ToString().Trim());
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();//</li>
                }
            }
        }
        #endregion
        #endregion


    }
}