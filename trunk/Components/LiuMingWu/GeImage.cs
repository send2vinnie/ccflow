using System.Web.UI;
using System.Data;
using System;
using BP;
using System.ComponentModel;
using BP.GE;
using System.Configuration;

namespace BP.GE.Ctrl
{

    [ToolboxData("<{0}:GEImage runat='server'></{0}:GEImage>")]
    [PersistChildren(false)]
    [ParseChildren(true, "GloDBColumns")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class GEImage : CtrlBaseDB
    {
        private ImageList imagelist = null;
        /// <summary>
        /// 数据显示类型
        /// </summary> 
        [Bindable(true), Category("显示样式"),
        Description("ImageList显示样式"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ImageList ImageListStyle
        {
            get
            {
                if (imagelist == null)
                {
                    imagelist = new ImageList(string.Empty, string.Empty, string.Empty, TitleDirection.Bottom, false);
                }
                return imagelist;
            }
        }

        private ePagerStyle pagerstyle = ePagerStyle.Style1;
        [Bindable(true),Category("显示样式"),Description("分页显示样式")]
        public ePagerStyle PagerStyle
        {
            get
            {
                return pagerstyle;
            }
            set
            {
                this.pagerstyle = value;
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
        /// <summary>
        /// 设计视图
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderDesignView(HtmlTextWriter writer)
        {
            #region ImageList
            if (GloDBColumns.Count > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                if (!string.IsNullOrEmpty(ImageListStyle.TableStyle))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.TableStyle);
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                for (int i = 0; i < 3; i++)
                {
                    #region
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    for (int j = 0; j < GloRepeatColumns; j++)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        if (ImageListStyle.ImgTitlePosition == TitleDirection.Right)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        if (!string.IsNullOrEmpty(GloDBColumns[0].DataFormatString) && GloDBColumns[0].DataFormatString.Contains("img"))
                        {
                            writer.Write(GloDBColumns[0].DataFormatString.ToString());
                        }
                        else if (!string.IsNullOrEmpty(ImageListStyle.PicStyle))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.PicStyle);
                            writer.AddAttribute(HtmlTextWriterAttribute.Src, "[数据绑定]");
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();//</img>
                        }
                        else
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();//</img>
                        }
                        writer.RenderEndTag();//</div>

                        if (ImageListStyle.ImgTitlePosition == TitleDirection.Right)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
                        }
                        if (!string.IsNullOrEmpty(ImageListStyle.ItemStyle))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.ItemStyle);
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        foreach (MyListItem li in GloDBColumns)
                        {
                            if (li == GloDBColumns[0])
                                continue;
                            //标题
                            if (ImageListStyle.ISShowHeader)
                            {
                                if (!string.IsNullOrEmpty(li.HeaderStyle))
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Class, li.HeaderStyle);
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(li.HeaderText);
                                writer.RenderEndTag();
                            }
                            //内容
                            if (!string.IsNullOrEmpty(li.ItemStyle))
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, li.ItemStyle);
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            if (!string.IsNullOrEmpty(li.DataFormatString))//&& li.DataFormatString.Contains("{0}")
                            {
                                writer.Write(string.Format(li.DataFormatString, "[数据绑定]") + "<br/>");
                            }
                            else
                            {
                                writer.Write("[数据绑定]" + "<br/>");
                            }
                            writer.RenderEndTag(); //</span>
                        }
                        writer.RenderEndTag(); //</div>
                        writer.RenderEndTag(); //</td>
                    }
                    writer.RenderEndTag();
                    #endregion
                }
                writer.RenderEndTag();
            }
            #endregion
        }
        /// <summary>
        /// 运行视图
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dt"></param>
        public override void RenderDataList(HtmlTextWriter writer, System.Data.DataTable dt)
        {
            #region
            if (GloDBColumns.Count > 0 && dt != null)
            {
                int z = 0;
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                if (!string.IsNullOrEmpty(ImageListStyle.TableStyle))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.TableStyle);
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                foreach (DataRow dr in dt.Rows)
                {
                    if (z == 0)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    }
                    z++;
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);

                    //标题
                    foreach (MyListItem li in GloDBColumns)
                    {

                        if (GloDBColumns[0] == li)
                        {
                            if (ImageListStyle.ImgTitlePosition == TitleDirection.Right)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);

                            if (!string.IsNullOrEmpty(li.DataFormatString))//&& li.DataFormatString.Contains("{0}")
                            {
                                //string strImg = "<img src='" + dr[li.DataTextField].ToString() + "'";
                                //if (!string.IsNullOrEmpty(ImageListStyle.PicStyle))
                                //{
                                //    strImg += " class='" + ImageListStyle.PicStyle + "'";
                                //}
                                //strImg += " />";
                                //writer.Write(GetUrlParaValue(dr, li, strImg));
                                writer.Write(GetUrlParaValue(dr, li));
                            }
                            else
                            {
                                string strValue = GetDateTextField(dr, li);
                                writer.AddAttribute(HtmlTextWriterAttribute.Src, strValue);
                                if (!string.IsNullOrEmpty(ImageListStyle.PicStyle))
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.PicStyle);
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                            }
                            writer.RenderEndTag();//</div>
                        }
                        else
                        {
                            if (GloDBColumns[1] == li)
                            {
                                if (ImageListStyle.ImgTitlePosition == TitleDirection.Right)
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
                                }
                                if (!string.IsNullOrEmpty(ImageListStyle.ItemStyle))
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Class, ImageListStyle.ItemStyle);
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            }
                            //标题
                            if (ImageListStyle.ISShowHeader)
                            {
                                if (!string.IsNullOrEmpty(li.HeaderStyle))
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Class, li.HeaderStyle);
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(li.HeaderText);
                                writer.RenderEndTag();
                            }
                            //内容
                            if (!string.IsNullOrEmpty(li.ItemStyle))
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, li.ItemStyle);
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            if (!string.IsNullOrEmpty(li.DataFormatString))
                            {
                                writer.Write(GetUrlParaValue(dr, li));
                            }
                            else
                            {
                                string strValue = GetDateTextField(dr, li);
                                writer.Write(strValue);
                            }
                            writer.RenderEndTag();
                            writer.WriteBreak();
                            if (GloDBColumns[GloDBColumns.Count - 1] == li)
                            {
                                writer.RenderEndTag();
                            }
                        }
                    }
                    writer.RenderEndTag();//</td>

                    if (z >= GloRepeatColumns)
                    {
                        writer.RenderEndTag();
                        z = 0;
                    }
                }
                if (z != 0)
                {
                    while (GloRepeatColumns - z > 0)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.Write("&nbsp;");
                        writer.RenderEndTag();
                        z++;
                    }
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.WriteLine();
            }
            #endregion
        }

        protected override void WritePager(HtmlTextWriter writer)
        {
            //Write out the first part of the control, the table header
            writer.Write(HTML1);
            // Write out a table row closure
            if (PagerStyle == ePagerStyle.Style1)
            {
                this.ShowPager(writer);
            }
            else
            {
                base.ShowPager(writer);
            }
            //Write Out the end of the table
            writer.Write(HTML4);
        }

        /// <summary>
        /// 显示分页样式
        /// </summary>
        /// <param name="writer"></param>
        protected override void ShowPager(HtmlTextWriter writer)
        {
            string strUrl = Context.Request.Url.Query;
            if (strUrl.Length > 0 && !strUrl.Contains("?PageIdx="))
            {
                if (strUrl.Contains("&PageIdx="))
                {
                    strUrl = strUrl.Remove(strUrl.IndexOf("&PageIdx="), strUrl.Length - strUrl.IndexOf("&PageIdx="));
                }
                strUrl += "&PageIdx=";
            }
            else
            {
                strUrl = "?PageIdx=";
            }
            writer.Write(HTML2);
            writer.Write("<Script Language=JavaScript>\n");
            writer.Write("//分页\n");
            writer.Write("Board_Setting26=Math.floor(" + pageSize + ");\n");
            writer.Write("TopicNum=Math.floor(" + ItemCount + ");\n");
            writer.Write("PageIdx=Math.floor(" + CurrentPageIndex + ");\n");
            writer.Write("var n,p;\n");
            writer.Write("var alertcolor;\n");
            writer.Write("alertcolor='#FF0000';\n");
            writer.Write("if ((PageIdx-1)%10==0)\n");
            writer.Write("{\n");
            writer.Write("p=(PageIdx-1) /10\n");
            writer.Write("}\n");
            writer.Write("else\n");
            writer.Write("{\n");
            writer.Write("p=(((PageIdx-1)-(PageIdx-1)%10)/10)\n");
            writer.Write("}\n");
            writer.Write("if(TopicNum%Board_Setting26==0)\n");
            writer.Write("{\n");
            writer.Write("n=TopicNum/Board_Setting26;\n");
            writer.Write("}\n");
            writer.Write("else\n");
            writer.Write("{\n");
            writer.Write("n=(TopicNum-TopicNum%Board_Setting26)/Board_Setting26+1;\n");
            writer.Write("}\n");
            writer.Write("document.write ('<div style=\"100%\">');\n");
            writer.Write("if (PageIdx==1)\n");
            writer.Write("{\n");
            writer.Write("document.write ('<font face=webdings color=\"'+alertcolor+'\">9</font>');\n");
            writer.Write("}\n");
            writer.Write("else\n");
            writer.Write("{\n");
            writer.Write("document.write ('<a href=\"" + strUrl + "1\" title=\"首页\"><font face=webdings>9</font></a>');\n");
            writer.Write("}\n");
            writer.Write("if (p*10 > 0)\n");
            writer.Write("{\n");
            writer.Write("document.write ('   <a href=\"" + strUrl + "'+p*10+'\" title=\"上十页\"><font face=webdings>7</font></a> ');\n");
            writer.Write("}\n");
            writer.Write("document.write ('<b>');\n");
            writer.Write("for (var i=p*10+1;i<p*10+11;i++)\n");
            writer.Write("{\n");
            writer.Write("if (i==PageIdx)\n");
            writer.Write("{\n");
            writer.Write("document.write (' <font color=\"'+alertcolor+'\">'+i+'</font> ');\n");
            writer.Write("}\n");
            writer.Write("else\n");
            writer.Write("{\n");
            writer.Write("document.write (' <a href=\"" + strUrl + "'+i+'\" title=\"转到第' + i + '页\">'+i+'</a> ');\n");
            writer.Write("}\n");
            writer.Write("if (i==n) break;\n");
            writer.Write("}\n");
            writer.Write("document.write ('</b>');\n");
            writer.Write("if (i<n)\n");
            writer.Write("{\n");
            writer.Write("document.write ('<a href=\"" + strUrl + "'+i+'\" title=\"下十页\"><font face=webdings>8</font></a>   ');\n");
            writer.Write("}\n");
            writer.Write("if (PageIdx==n)\n");
            writer.Write("{\n");
            writer.Write("document.write ('<Font face=webdings color=\"'+alertcolor+'\">:</font>');\n");
            writer.Write("}\n");
            writer.Write("else\n");
            writer.Write("{\n");
            writer.Write("document.write ('<a href=\"" + strUrl + "'+n+'\" title=\"尾页\"><font face=webdings>:</font></a>  ');\n");
            writer.Write("}\n");
            writer.Write("document.write ('</div>');\n");
            writer.Write("function GoTO(){\n");
            writer.Write("location.href = \"" + strUrl + "\" +document.all(\"PageIdx\").value;\n");
            writer.Write("}\n");
            writer.Write("</script>\n");
        }
    }
}