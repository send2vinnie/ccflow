using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using BP.En;
using BP.GE.Ctrl;

namespace BP.GE
{
    [PersistChildren(false)]
    [ParseChildren(true, "GloDBColumns")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public abstract class CtrlBaseDB :Control,INamingContainer
    {
        public CtrlBaseDB()
        { 

        }

        #region 字段
        public const string HTML1 = "<table cellpadding=0 cellspacing=0 width=100% border=0><tr><td>";
        public const string HTML2 = "</td></tr><tr class=\"listtitle\"><td>";
        public const string HTML3 = "</td><td align=right>";
        public const string HTML4 = "</td></tr></table>";
        private static readonly Regex RX = new Regex(@"^&PageIdx=\d+", RegexOptions.Compiled);
        private const string LINK_PREV = "<a href=?PageIdx={0}>[上一页]</a>";
        private const string LINK_PREV1 = "<font color=#c0c0c0>[上一页]</font>";
        private const string LINK_MORE = "<a href=?PageIdx={0}>[下一页]</a>";
        private const string LINK_MORE1 = "<font color=#c0c0c0>[下一页]</font>";
        private const string KEY_PAGE = "PageIdx";
        private const string COMMA = "?";
        private const string AMP = "&";
        private const string NBSP = "&nbsp;";
        protected string emptyText = "<div align=center><br><br>对不起!没有找到相应的数据 ^_^</div>";
        public int pageSize = 20;
        private int currentPageIndex;
        private int itemCount;
        private bool showpage = true;
        int start, size;
        private DBSourceType dbType;
        private object dbSource;
        private int RepeatColumns = 1;
        #endregion

        #region 属性集合

        /// <summary>
        /// 列表项
        /// </summary>
        private Items items;
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Category("全局属性")]
        [TypeConverter(typeof(CollectionConverter))]
        [Description("列集合")]
        public Items GloDBColumns
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new Items();
                }
                return this.items;
            }
        }
        /// <summary>
        /// 列数
        /// </summary>
        [Category("全局属性"),
        Description("要显示的列数")]
        public int GloRepeatColumns
        {
            get
            {
                return RepeatColumns;
            }
            set
            {
                RepeatColumns = value;
            }
        }
        /// <summary>
        /// 数据源类型
        /// </summary>
        [DefaultValue("SQL"),
        Category("全局属性"),
        Description("数据源类型"),
        NotifyParentProperty(true),
        BrowsableAttribute(true),
        Localizable(true)]
        public DBSourceType GloDBType
        {
            get
            {
                return dbType;
            }
            set
            {
                dbType = value;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [DefaultValue(""),
        Category("全局属性"),
        Description("数据源"),
        NotifyParentProperty(true),
        BrowsableAttribute(true),
        Localizable(true)]
        public object GloDBSource
        {
            get
            {
                return dbSource;
            }
            set
            {
                dbSource = value;
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        protected int PageCount
        {
            get
            {
                if (ItemCount % pageSize == 0)
                    return ItemCount / pageSize;
                else
                    return ItemCount / pageSize + 1;
            }
        }

        virtual protected int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        virtual public int CurrentPageIndex
        {
            get { return Math.Max(1, currentPageIndex); }
            set
            {
                if (value > PageCount)
                    currentPageIndex = PageCount;
                else
                    currentPageIndex = value;
            }
        }

        public bool ShowPage
        {
            get
            {
                return showpage;
            }
            set
            {
                showpage = Convert.ToBoolean(value);
            }
        }

        public string EmptyText
        {
            set { emptyText = value; }
        }

        #endregion


        public void SetPage(int index)
        {
            OnPageIndexChanged(new DataGridPageChangedEventArgs(null, index));
        }

        protected override void OnLoad(EventArgs e)
        {
        }
        protected override void Render(HtmlTextWriter writer)
        {
            MyRender(writer);
        }
        /// <summary>
        /// 自定义显示
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="_UCType"></param>
        public virtual void MyRender(HtmlTextWriter writer)
        {
            //设计时的显示效果
            if (this.DesignMode)
            {
                RenderDesignView(writer);
            }
            //运行时的显示效果
            else
            {
                DataTable dt = GetData(writer);
                //显示分页
                if (ShowPage)
                {
                    if (dt.Rows.Count > 0)
                    {
                        WritePager(writer);
                    }
                }
            }
        }

        protected virtual void WritePager(HtmlTextWriter writer)
        {
            //Write out the first part of the control, the table header
            writer.Write(HTML1);
            // Write out a table row closure
            ShowPager(writer);
            //Write Out the end of the table
            writer.Write(HTML4);
        }

        protected DataTable GetData(HtmlTextWriter writer)
        {
            DataTable myDataSource = new DataTable();
            switch (GloDBType)
            {
                case DBSourceType.SQL:
                    myDataSource = BP.DA.DBAccess.RunSQLReturnTable(GloDBSource as string);
                    ItemCount = myDataSource.Rows.Count;
                    DataTable dt;
                    if (ItemCount > 0)
                    {
                        dt = GetPagedData(myDataSource);
                        myDataSource.Dispose();
                        RenderDataList(writer, dt);
                    }
                    break;
                case DBSourceType.Xml:
                    string xmlName = Convert.ToString(GloDBSource);
                    BP.XML.XmlEns xmlensName = BP.DA.ClassFactory.GetXmlEns(xmlName);
                    xmlensName.RetrieveAll();
                    dt = xmlensName.GetTable();
                    RenderDataList(writer, dt);
                    break;
                case DBSourceType.DataTable:
                    myDataSource = GloDBSource as DataTable;
                    ItemCount = myDataSource.Rows.Count;
                    if (ItemCount > 0)
                    {
                        dt = GetPagedData(myDataSource);
                        RenderDataList(writer, dt);
                    }
                    break;
                case DBSourceType.DataSet:
                    myDataSource = (GloDBSource as DataSet).Tables[0];
                    ItemCount = myDataSource.Rows.Count;
                    if (ItemCount > 0)
                    {
                        dt = GetPagedData(myDataSource);
                        RenderDataList(writer, dt);
                    }
                    break;
                case DBSourceType.Ens:
                    myDataSource = ((Entities)GloDBSource).ToDataTableField();
                    ItemCount = myDataSource.Rows.Count;
                    if (ItemCount > 0)
                    {
                        dt = GetPagedData(myDataSource);
                        RenderDataList(writer, dt);
                    }
                    break;
            }
            return myDataSource;
        }
        /// <summary>
        /// 显示设计视图
        /// </summary>
        /// <param name="writer"></param>
        public abstract void RenderDesignView(HtmlTextWriter writer);
        /// <summary>
        /// 显示运行视图(分页数据)
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dt"></param>
        public abstract void RenderDataList(HtmlTextWriter writer, DataTable dt);
        /// <summary>
        /// 获取所有数据项
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// 把数据分页
        /// </summary>
        /// <param name="DataSource">要分页的数据源</param>
        /// <returns></returns>
        private DataTable GetPagedData(DataTable DataSource)
        {
            //string page = Context.Request.Url.Query.Replace("?page=", "");
            string strPageIdx = Convert.ToString(Context.Request.QueryString["PageIdx"]);
            int index = (!string.IsNullOrEmpty(strPageIdx)) ? int.Parse(strPageIdx) : 1;
            CurrentPageIndex = index;
            SetPage(index);
            //string page = Context.Request[PAGE];            
            start = (CurrentPageIndex - 1) * pageSize;
            size = Math.Max(Math.Min(pageSize, ItemCount - start), 0);
            DataTable dt = new DataTable();
            dt = DataSource.Clone();
            for (int i = 0; i < size; i++)
            {
                dt.ImportRow(DataSource.Rows[start + i]);
            }
            return dt;
        }

        /// <summary>
        /// 获取URL参数值
        /// </summary>
        /// <param name="dr">当前行</param>
        /// <param name="li">列</param>
        /// <returns>把参数替换后的URL</returns>
        public string GetUrlParaValue(DataRow dr, MyListItem li)
        {
            string strValue = GetDateTextField(dr, li);
            string strReturnValue = string.Format(li.DataFormatString, strValue);
            foreach (UrlList Para in li.UrlListItems)
            {
                switch (Para.ValueFrom)
                {
                    case ParamType.QueryString:
                        strValue = Convert.ToString(Context.Request.QueryString[Para.ParaName]);
                        break;
                    case ParamType.DataRow:
                        strValue = Convert.ToString(dr[Para.ParaName]);
                        break;
                    case ParamType.Session:
                        strValue = Convert.ToString(Context.Session[Para.ParaName]);
                        break;
                    case ParamType.Property:
                        Type myType = this.Parent.Page.GetType();
                        System.Reflection.PropertyInfo myPI = myType.GetProperty(Para.ParaName);
                        strValue = myPI.GetValue(this.Parent.Page, null).ToString();
                        break;
                }
                strReturnValue = strReturnValue.Replace("@" + Para.ParaName, strValue);
            }
            return strReturnValue;
        }

        /// <summary>
        /// 获取URL参数值
        /// </summary>
        /// <param name="dr">当前行</param>
        /// <param name="li">列</param>
        /// <returns>把参数替换后的URL</returns>
        public string GetUrlParaValue(DataRow dr,MyListItem li,string strText)
        {
            string strReturnValue = string.Format(li.DataFormatString, strText);
            string strValue = string.Empty;
            foreach (UrlList Para in li.UrlListItems)
            {
                switch (Para.ValueFrom)
                {
                    case ParamType.QueryString:
                        strValue = Convert.ToString(Context.Request.QueryString[Para.ParaName]);
                        break;
                    case ParamType.DataRow:
                        strValue = Convert.ToString(dr[Para.ParaName]);
                        break;
                    case ParamType.Session:
                        strValue = Convert.ToString(Context.Session[Para.ParaName]);
                        break;
                    case ParamType.Property:
                        Type myType = this.Parent.Page.GetType();
                        System.Reflection.PropertyInfo myPI = myType.GetProperty(Para.ParaName);
                        strValue = myPI.GetValue(this.Parent.Page, null).ToString();
                        break;
                }
                strReturnValue = strReturnValue.Replace("@" + Para.ParaName, strValue);
            }
            return strReturnValue;
        }

        public string GetDateTextField(DataRow dr, MyListItem li)
        {
            //The format of  li.DataTextField is "<%=DO(AA,BB,CC)%>"
            string strFun = li.DataTextField;
            if (strFun.Contains("<%=") && strFun.Contains("%>"))
            {
                strFun = strFun.Replace("<%=", string.Empty).Replace("%>", string.Empty);
                int IntDel = strFun.IndexOf("(");
                string FunName = strFun.Substring(0, IntDel);
                string FunArg = strFun.Substring(IntDel + 1, strFun.Length - IntDel - 2);
                string[] FunArgs = FunArg.Split(',');
                for (int i = 0; i < FunArgs.Length; i++)
                {
                    FunArgs[i] = dr[FunArgs[i]].ToString();
                }
                object oValue = this.Parent.Page.GetType().GetMethod(FunName).Invoke(this.Parent.Page, FunArgs);
                return oValue.ToString();
            }
            else
            {
                return dr[strFun].ToString();
            }
        }

        #region 分页代码
        /// <summary>
        /// 显示分页样式
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void ShowPager(HtmlTextWriter writer)
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
            writer.Write("document.write ('<table border=\"0\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" align=\"center\" >');\n");
            writer.Write("document.write ('<tr><td valign=\"middle\">');\n");
            writer.Write("document.write ('页次：<b>" + CurrentPageIndex + " </b>/<b> " + PageCount + "</b>页 本页<b>" + size + " </b> 主题数<b>   " +
                          ItemCount + " </b></td>');\n");
            writer.Write("document.write ('<td valign=\"middle\" align=right>分页：');\n");
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
            //writer.Write("document.write ('&nbsp;&nbsp;转到:</td><td valign=\"middle\" width=50 align=right><nobr/><input type=text name=\"PageIdx\" id=\"PageIdx\" size=3 maxlength=10  value=\"'+PageIdx+'\"><input type=button onclick=\"Javascript:GoTO();\" value=Go name=submit>');\n");
            writer.Write("document.write ('</td></tr>');\n");
            writer.Write("document.write ('</table>');\n");
            writer.Write("function GoTO(){\n");
            writer.Write("location.href = \"" + strUrl + "\" +document.all(\"PageIdx\").value;\n");
            writer.Write("}\n");
            writer.Write("</script>\n");
        }
        #endregion

        public event DataGridPageChangedEventHandler PageIndexChanged;

        virtual protected void OnPageIndexChanged(DataGridPageChangedEventArgs e)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }

    }
}