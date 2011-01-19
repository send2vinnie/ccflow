using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using Tax666.Common;

namespace Tax666.WebControls
{
    /// <summary>
    /// 头部菜单web控件
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("ButtonList"),
    ParseChildren(true, "ButtonList"),
    ToolboxData("<{0}:HeadMenuWebControls runat=\"server\"> </{0}:HeadMenuWebControls>"),
    Description("头部菜单web控件")
    ]
    public class HeadMenuWebControls : WebControl
    {
        /// <summary>
        /// 构造函数,不能少如果用泛型需要初始化
        /// </summary>
        public HeadMenuWebControls()
        {
            _ButtonList = new List<HeadMenuButtonItem>();
        }

        #region 私有变量
        private List<HeadMenuButtonItem> _ButtonList;
        private string HeadMenuTemplateTxt =
        @"<!-- 头部菜单 Start -->
	        <table border='0' cellpadding='0' cellspacing='0' width='100%' align='center'>
              <tr>
                <td class='menubar_title'><img border='0' src='{0}' align='absmiddle' hspace='4' vspace='4'>&nbsp;{1}</td>
                <td class='menubar_readme_text' valign='bottom'><img src='{2}' align='absMiddle' border='0' />&nbsp;{3}</td>
              </tr>
              <tr>
                <td height='27px' class='menubar_function_text'>目前操作功能：{4}</td>
                <td class='menubar_menu_td' align='right'>{5}</td>
              </tr>
              <tr><td height='5px' colspan='2'></td></tr>
            </table>
        <!-- 头部菜单 End -->
        ";
        private string _HeadIconPath = WebRequests.GetWebUrl() + "Manager/images/Icon/";
        private string _HeadTitleIcon = "default.gif";
        private string _HeadTitleTxt = "标题";
        private string _HeadHelpIcon = "HelpIco.gif";
        private string _HeadHelpTxt = "帮助？";
        private string _HeadOPTxt = "";


        private string CreateButtonHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (_ButtonList != null && _ButtonList.Count > 0)
            {
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                string OnUrlJs = "";
                string ButtonIcon = "";
                string ButtonTxt = "";
                int DispButtonNum = 0;
                for (int i = 0; i < _ButtonList.Count; i++)
                {
                    if (_ButtonList[i].ButtonVisible)
                    {
                        DispButtonNum++;
                        OnUrlJs = "";
                        ButtonIcon = "";
                        ButtonTxt = "";
                        switch (_ButtonList[i].ButtonUrlType)
                        {
                            case UrlType.Href:
                                OnUrlJs = string.Format("JavaScript:window.location.href='{0}';", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.JavaScript:
                                OnUrlJs = string.Format("JavaScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.VBScript:
                                OnUrlJs = string.Format("VBScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                        }
                        if (_ButtonList[i].ButtonIcon != string.Empty)
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonIcon;
                        }
                        else
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonPopedom.ToString() + ".gif";
                            switch (_ButtonList[i].ButtonPopedom)
                            {
                                case PopedomType.Valid:
                                    ButtonTxt = "有效/无效";
                                    break;
                                case PopedomType.Audit:
                                    ButtonTxt = "审核";
                                    break;
                                case PopedomType.Delete:
                                    ButtonTxt = "删除";
                                    break;
                                case PopedomType.Edit:
                                    ButtonTxt = "修改";
                                    break;
                                case PopedomType.List:
                                    ButtonTxt = "列表";
                                    break;
                                case PopedomType.Orderby:
                                    ButtonTxt = "排序";
                                    break;
                                case PopedomType.New:
                                    ButtonTxt = "添加";
                                    break;
                                case PopedomType.Print:
                                    ButtonTxt = "打印";
                                    break;
                                case PopedomType.Other:
                                    ButtonTxt = "";
                                    ButtonIcon = "";
                                    break;
                            }
                        }
                        sb.AppendFormat("<td class=\"menubar_button\" id=\"button_{1}\" OnClick=\"{0}\" OnMouseOut=\"javascript:MenuOnMouseOver(this);\" OnMouseOver=\"javascript:MenuOnMouseOut(this);\">", OnUrlJs, i);
                        sb.AppendFormat("<img border=\"0\" align=\"texttop\" src=\"{0}\">&nbsp;", ButtonIcon);
                        sb.AppendFormat("{0}{1}</td>", ButtonTxt, _ButtonList[i].ButtonName);
                    }
                }
                if (DispButtonNum == 0)
                    sb.Append("<td>&nbsp;</td>");
                sb.Append("</tr></table>");
            }
            if (sb.ToString() == string.Empty)
                sb.Append("&nbsp");
            return sb.ToString();
        }


        #endregion

        #region 属性
        /// <summary>
        /// 读取/设置头部菜单路径
        /// </summary>
        [Description("读取/设置头部菜单路径"), Category("外观"), DefaultValue("~/images/icon/")]
        public string HeadIconPath
        {
            get
            {
                object m = ViewState["HeadIconPath"];
                return m == null ? ResolveClientUrl(_HeadIconPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["HeadIconPath"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题Icon图片名
        /// </summary>
        [Description("读取/设置标题Icon图片名"), Category("外观"), DefaultValue("default.gif")]
        public string HeadTitleIcon
        {
            get
            {
                object m = ViewState["HeadTitleIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadTitleIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadTitleIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题名称
        /// </summary>
        [Description("读取/设置标题名称"), Category("外观"), DefaultValue("标题名称")]
        public string HeadTitleTxt
        {
            get
            {
                object m = ViewState["HeadTitleTxt"];
                return m == null ? _HeadTitleTxt : m.ToString();
            }
            set
            {
                ViewState["HeadTitleTxt"] = value;
            }
        }

        /// <summary>
        /// 读取/设置帮助Icon名称
        /// </summary>
        [Description("读取/设置帮助Icon图片名"), Category("外观"), DefaultValue("HelpIco.gif")]
        public string HeadHelpIcon
        {
            get
            {
                object m = ViewState["HeadHelpIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadHelpIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadHelpIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置帮助文字
        /// </summary>
        [Description("读取/设置帮助文字"), Category("外观"), DefaultValue("帮助？")]
        public string HeadHelpTxt
        {
            get
            {
                object m = ViewState["HeadHelpTxt"];
                return m == null ? _HeadHelpTxt : m.ToString();
            }
            set
            {
                ViewState["HeadHelpTxt"] = value;
            }
        }
        /// <summary>
        /// 读取/设置操作说明
        /// </summary>
        [Description("读取/设置操作说明"), Category("外观"), DefaultValue("")]
        public string HeadOPTxt
        {
            get
            {
                object m = ViewState["HeadOPTxt"];
                return m == null ? _HeadOPTxt : m.ToString();
            }
            set
            {
                ViewState["HeadOPTxt"] = value;
            }
        }
        /// <summary>
        /// 按钮集合
        /// </summary>
        [
        Category("Behavior"),
        Description("按钮集合"),
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<HeadMenuButtonItem> ButtonList
        {
            get
            {
                //object m = ViewState["ButtonList"];
                //return m == null ? _ButtonList : (List<HeadMenuButtonItem>)m;
                return _ButtonList;
            }
            //set
            //{
            //    ViewState["ButtonList"] = value;
            //}
        }

        /// <summary>
        /// 重写RenderContents方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(HeadMenuTemplateTxt, HeadTitleIcon, HeadTitleTxt, HeadHelpIcon, HeadHelpTxt, HeadOPTxt, CreateButtonHtml());
        }
        #endregion
    }
}