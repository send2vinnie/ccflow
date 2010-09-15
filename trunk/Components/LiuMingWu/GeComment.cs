using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Design;
using System.Configuration;
using System.Reflection;

namespace BP.GE.Ctrl
{
    [PersistChildren(false)]
    [ParseChildren(true, "GloDBColumns")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [ToolboxData("<{0}:GEComment runat='server'></{0}:GEComment>")]
    public class GEComment : Control
    {

        #region 字段
        protected const string HTML1 = "<table cellpadding=0 cellspacing=0 width=100% border=0><tr><td>";
        protected const string HTML2 = "</td></tr><tr class=\"listtitle\"><td>";
        protected const string HTML3 = "</td><td align=right>";
        protected const string HTML4 = "</td></tr></table>";
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
        private int pageSize = 20;
        private int currentPageIndex;
        private int itemCount;
        private bool showpage = true;
        int start, size;
        private object _DBSource = null;
        private int rowNumber = 0;
        #endregion

        #region 属性集合
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
                return _DBSource;
            }
            set
            {
                _DBSource = value;
            }
        }

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
        /// 数据源类型
        /// </summary>
        private DBSourceType dbType;
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
            set
            {
                emptyText = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        //private eParentType _ParentType = eParentType.WebPage;
        //[Description("父容器的类型")]
        //public eParentType ParentType
        //{
        //    get 
        //    {
        //        return _ParentType;
        //    }
        //    set
        //    {
        //        _ParentType = value;
        //    }
        //}

        #endregion

        #region 属性,获取或设置TextBox的值

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("用户名"),
        Browsable(false)]
        public string FK_EmpT
        {
            get
            {
                return txtUserName.Text;
            }
            set
            {
                txtUserName.Text = value;
            }
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("用户名"),
        Browsable(false)]
        public string FK_Emp
        {
            get
            {
                return System.Web.HttpContext.Current.Session["No"] as string;
            }
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("验证码")]
        public string CheckCode
        {
            get
            {
                return txtCheckChode.Text;
            }
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue("标题")]
        public string Title
        {
            get
            {
                return txtTitle.Text;
            }
            set
            {
                txtTitle.Text = value;
            }
        }

        [Bindable(true),
        Browsable(false),
        Category("自定义设置"),
        DefaultValue("")]
        public string Content
        {
            get
            {
                return hidTxtContent.Value;
            }
            set
            {
                hidTxtContent.Value = value;
            }
        }
        private bool _ISAllowAnony = false;
        [Bindable(true),
        Category("自定义设置"),
        Description("是否允许匿名用户发表评论")]
        public bool ISAllowAnony
        {
            get
            {
                return _ISAllowAnony;
            }
            set
            {
                _ISAllowAnony = Convert.ToBoolean(value);
            }
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("登陆地址")]
        public string LoginURL
        {
            get;
            set;
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("注册地址")]
        public string RegistURL
        {
            get;
            set;
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("评论组别"),
        Browsable(true)]
        public string GroupKey
        {
            get;
            set;
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(""),
        Description("评论主题"),
        Browsable(true)]
        public string RefOID
        {
            get;
            set;
        }

        [Bindable(true),
        Category("自定义设置"),
        DefaultValue(eShowType.PopLayer),
        Description("评论内容的显示样式"),
        Browsable(true)]
        public eShowType ShowType
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
        #endregion

        #region 定义控件
        private TextBox txtTitle = new TextBox();
        private TextBox txtUserName = new TextBox();
        private TextBox txtCheckChode = new TextBox();
        private HiddenField hidTxtContent = new HiddenField();
        private Button btnSubmit = new Button();
        #endregion

        #region 初始化控件并把控件添加到页面

        public GEComment()
        {
            txtUserName.ID = "txtUserName";
            txtUserName.Width = 100;
            txtUserName.MaxLength = 10;
            txtUserName.Attributes.Add("check", @"^\S+$");
            txtUserName.Attributes.Add("warning", "用户名不能为空,且不能含有空格!");
            this.Controls.Add(txtUserName);

            txtCheckChode.ID = "txtCheckCode";
            txtCheckChode.Width = 100;
            txtCheckChode.MaxLength = 10;

            txtCheckChode.Attributes.Add("check", @"^\S+$");
            txtCheckChode.Attributes.Add("warning", "请输入验证码!");
            txtCheckChode.Text = string.Empty;
            this.Controls.Add(txtCheckChode);

            txtTitle.ID = "txtTitle";
            txtTitle.Width = 564;
            txtTitle.MaxLength = 45;
            txtTitle.Attributes.Add("check", @"^\S+$");
            txtTitle.Attributes.Add("warning", "标题不能为空,且不能含有空格!");
            this.Controls.Add(txtTitle);

            hidTxtContent.ID = "txtContent";
            this.Controls.Add(hidTxtContent);

            btnSubmit.ID = "btnSubmit";
            btnSubmit.OnClientClick = "return CheckForm(document.forms[0])";
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnSubmit.Text = "发表评论";
            this.Controls.Add(btnSubmit);
        }
        #endregion



        public void SetPage(int index)
        {
            OnPageIndexChanged(new DataGridPageChangedEventArgs(null, index));
        }

        protected override void OnLoad(EventArgs e)
        {
            txtUserName.Text = Convert.ToString(System.Web.HttpContext.Current.Session["UserName"]);
        }
        /// <summary>
        /// Overriden method to control how the page is rendered
        /// </summary>
        /// <param name="writer"></param>        
        protected override void Render(HtmlTextWriter writer)
        {
            MyRender(writer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="_UCType"></param>
        private void MyRender(HtmlTextWriter writer)
        {
            //输出样式文件
            writer.Write("<link rel='stylesheet'type='text/css' href='" + ResUrl + "GE/Comment/Stylesheet.css'/> ");
            writer.Write("<script type='text/javascript' src='" + ResUrl + "GE/Comment/popLayer.js'></script> ");
            writer.Write("<script type='text/javascript' src='" + ResUrl + "GE/Comment/CheckForm.js'></script>");
            //设计时的显示效果
            if (this.DesignMode)
            {
                RenderDesignView(writer);
            }
            //运行时的显示效果
            else
            {
                DataTable myDataSource = GetDataTable();
                ItemCount = myDataSource.Rows.Count;
                DataTable dt = GetPagedData(myDataSource);
                myDataSource.Dispose();
                RenderDataList(writer, dt);
                //显示分页
                if (ShowPage)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //Write out the first part of the control, the table header
                        writer.Write(HTML1);
                        // Write out a table row closure
                        ShowPager(writer);
                        //Write Out the end of the table
                        writer.Write(HTML4);
                    }
                }
                addCommentBox(writer);
            }
        }
        /// <summary>
        /// 设计视图
        /// </summary>
        /// <param name="writer"></param>
        private void RenderDesignView(HtmlTextWriter writer)
        {
            for (int i = 1; i <= 3; i++)
            {
                rowNumber++;
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_li");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_info");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "right_option");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(i.ToString() + "楼");
                writer.RenderEndTag();
                if (GloDBColumns.Count > 0)
                {
                    foreach (MyListItem li in GloDBColumns)
                    {
                        //表头
                        if (!string.IsNullOrEmpty(li.HeaderText))
                        {
                            if (!string.IsNullOrEmpty(li.HeaderStyle))
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, li.HeaderStyle);
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            }
                            writer.Write(li.HeaderText);
                            if (!string.IsNullOrEmpty(li.HeaderStyle))
                            {
                                writer.RenderEndTag();
                            }
                        }
                        //内容
                        if (!string.IsNullOrEmpty(li.DataTextField))
                        {
                            if (!string.IsNullOrEmpty(li.ItemStyle))
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, li.ItemStyle);
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            }
                            writer.Write(li.DataTextField);
                            if (!string.IsNullOrEmpty(li.ItemStyle))
                            {
                                writer.RenderEndTag();
                            }
                        }
                    }
                }
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_main");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(" [内 容] ");
                writer.RenderEndTag();

                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, "Input");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "test");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "我要发表评论");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        /// <summary>
        /// 运行视图-显示评论数据
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dt"></param>
        private void RenderDataList(HtmlTextWriter writer, DataTable dt)
        {
            rowNumber = (CurrentPageIndex - 1) * PageSize;
            foreach (DataRow dr in dt.Rows)
            {
                rowNumber++;
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_li");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_info");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "right_option");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(rowNumber.ToString() + "楼 ");
                writer.RenderEndTag();
                foreach (MyListItem li in GloDBColumns)
                {
                    //表头
                    if (!string.IsNullOrEmpty(li.HeaderText))
                    {
                        if (!string.IsNullOrEmpty(li.HeaderStyle))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, li.HeaderStyle);
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        }
                        writer.Write(li.HeaderText);
                        if (!string.IsNullOrEmpty(li.HeaderStyle))
                        {
                            writer.RenderEndTag();
                        }
                    }
                    //内容
                    if (!string.IsNullOrEmpty(li.DataTextField))
                    {
                        if (!string.IsNullOrEmpty(li.ItemStyle))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, li.ItemStyle);
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        }
                        //writer.Write(dr[li.DataTextField].ToString());
                        writer.Write(GetDateTextField(dr, li));
                        if (!string.IsNullOrEmpty(li.ItemStyle))
                        {
                            writer.RenderEndTag();
                        }
                    }
                }
                writer.RenderEndTag();
                if (dt.Columns.Contains("Doc"))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "gust_main");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write(dr["Doc"].ToString());
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
        }

        public string GetDateTextField2(DataRow dr, MyListItem li)
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
                if (1 == 1)
                {
                    object oValue = this.Page.GetType().GetMethod(FunName).Invoke(this.Page, FunArgs);
                    return oValue.ToString();
                }
                else
                {
                    Type tc = this.Page.FindControl(this.Parent.ClientID).GetType();
                    Control uc = this.Page.FindControl(this.Parent.ClientID);
                    System.Reflection.MethodInfo m = tc.GetMethod(FunName);
                    object oValue = m.Invoke(uc, FunArgs);
                    return oValue.ToString();
                }
            }
            else
            {
                return dr[strFun].ToString();
            }
        }
        public string GetDateTextField(DataRow dr, MyListItem li)
        {
            string strFun = li.DataTextField;
            if (strFun.Trim().ToLower() == "ip")
            {
                return fun(dr[strFun].ToString());
            }
            else
            {
                return dr[strFun].ToString();
            }
        }
        public string fun(string ip)
        {
            //127.0.0.1
            string[] ips = ip.Split('.');
            if (ips.Length == 4)
            {
                return ips[0] + "." + ips[1] + "." + "*" + "." + "*";
            }
            else
            {
                return ip;
            }
        }
        /// <summary>
        /// 获取所有数据项
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            string strSql = "Select * from GE_Comment where GroupKey='" + GroupKey + "' and RefOID='" + RefOID + "'";
            return BP.DA.DBAccess.RunSQLReturnTable(strSql);
        }
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
        /// 添加评论框
        /// </summary>
        /// <param name="writer"></param>
        private void addCommentBox(HtmlTextWriter writer)
        {
            //添加弹出层特效
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "Input");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "test");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "我要发表评论");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            if (ShowType == eShowType.PopLayer)
            {
                writer.AddAttribute("onclick", "showFloat()");
            }
            else
            {
                string url = ResUrl + "GE/Comment/Comment.aspx";
                url += "?GroupKey=" + GroupKey + "&RefOID=" + RefOID + "&ISAllowAnony=" + ISAllowAnony;
                writer.AddAttribute("onclick", "showWindow('" + url + "')");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            if (ShowType == eShowType.PopLayer)
            {
                addCommentBoxContent(writer);
            }
        }

        /// <summary>
        /// 弹出层的内容区
        /// </summary>
        /// <param name="writer"></param>
        private void addCommentBoxContent(HtmlTextWriter writer)
        {
            //弹出层
            //弹出层--1,遮罩层
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "doing");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "div_Doing");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "divLogin");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "div_Login");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "div_Content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //弹出层的内容
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tbl_Content");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tbl_Content_Td");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("标 题：");
            txtTitle.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            hidTxtContent.RenderControl(writer);
            //编辑器
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "eWebEditor1");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, "eWebEditor1");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:block");
            writer.AddAttribute("frameborder", "0");
            writer.AddAttribute("scrolling", "no");
            string strSrc = ResUrl + "GE/Comment/Edit/editor.htm?id=" + hidTxtContent.UniqueID + "&style=coolblue";
            writer.AddAttribute(HtmlTextWriterAttribute.Src, strSrc);
            writer.RenderBeginTag(HtmlTextWriterTag.Iframe);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            //"用户名  登陆  注册"
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tbl_Content_Td");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("用户名：");
            txtUserName.RenderControl(writer);
            writer.Write("　验证码：");
            txtCheckChode.RenderControl(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + "GE/Comment/CheckCode.aspx");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            //添加按钮
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "text-align:center");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            btnSubmit.RenderControl(writer);
            writer.Write(" ");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, " 关 闭 ");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "HiddenLayer()");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            //登陆  注册
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "Span_A");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.LoginURL);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "A_Login");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write("登陆");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.RegistURL);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "A_Login");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write("注册");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            //弹出层结束
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        /// <summary>
        /// 判断验证码是否正确
        /// </summary>
        /// <param name="page">控件所在页</param>
        /// <param name="strText">用户输入的验证码</param>
        /// <returns>验证结果</returns>
        public bool CheckCodeIsRight(string strText)
        {
            Page page = this.Page;
            if (Convert.ToString(System.Web.HttpContext.Current.Session["GeCheckCode"]).ToLower() == CheckCode.ToLower())
            {
                return true;
            }
            else
            {
                if (!page.ClientScript.IsStartupScriptRegistered("strScript"))
                {
                    string strScript = "alert('验证码输入错误!');";
                    page.ClientScript.RegisterStartupScript(page.GetType(), "strScript", strScript, true);
                }
                return false;
            }

        }

        #region 分页代码
        /// <summary>
        /// 显示分页样式
        /// </summary>
        /// <param name="writer"></param>
        private void ShowPager(HtmlTextWriter writer)
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Page page = this.Page;
            if (this.Content.Length > 1024)
            {
                if (!page.ClientScript.IsStartupScriptRegistered("strScript4"))
                {
                    string strScript = "alert('您的评论内容太长!');";
                    page.ClientScript.RegisterStartupScript(page.GetType(), "strScript4", strScript, true);
                }
                return;
            }
            if (CheckCodeIsRight(CheckCode))
            {
                int result = 0;
                if (ISAllowAnony == true)
                {
                    result = Save();
                }
                else
                {
                    if (string.IsNullOrEmpty(FK_Emp))
                    {
                        if (!page.ClientScript.IsStartupScriptRegistered("strScript2"))
                        {
                            string strScript = "alert('对不起请先登陆!');";
                            page.ClientScript.RegisterStartupScript(page.GetType(), "strScript2", strScript, true);
                        }
                    }
                    else
                    {
                        result = Save();
                    }
                }
                if (result > 0)
                {
                    GeFun.ShowMessage(this.Page, "StrScript3", "评论成功!");
                }
                else if (result == -99)
                {
                    GeFun.ShowMessage(this.Page, "StrScript4", "评论内容被禁止提交!");
                }
                else
                {
                    GeFun.ShowMessage(this.Page, "StrScript5", "评论失败!");
                }
            }
        }
        private int Save()
        {
            Comment comment = new Comment();
            comment.IP = GeFun.getIp();
            comment.Title = Title;
            comment.GroupKey = GroupKey;
            comment.RefOID = RefOID;
            comment.FK_Dept = Convert.ToString(System.Web.HttpContext.Current.Session["FK_Dept"]);
            comment.FK_Emp = FK_Emp;
            comment.FK_EmpT = FK_EmpT;
            
            //脏话过滤
            //@0=禁止提交@2=替换字串@3=不处理
            string StrContent = this.Content;
            BadWords badwords = new BadWords();
            badwords.RetrieveAll();
            foreach (BadWord badword in badwords)
            {
                if (StrContent.Contains(badword.Name))
                {
                    //处理方式
                    switch (Convert.ToInt32(badword.BadWordDealWay))
                    {
                        case 0:
                            return -99;
                        case 1:
                            StrContent = StrContent.Replace(badword.Name, badword.ReplaceWord);
                            break;
                        case 2:
                            break;
                    }
                }
            }
            comment.Doc = StrContent;
            comment.RDT = DateTime.Now.ToShortDateString();
            return comment.Save();
        }
    }
}