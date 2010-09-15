using System.Web.UI;
using System.ComponentModel;
using System.Data;
using BP.En;
using System;
using System.IO;
using System.Text;
using System.ComponentModel.Design;
using BP.GE;
using System.Configuration;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEMyView runat='server'></{0}:GEMyView>")]
    [PersistChildren(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [Designer(typeof(GeMyViewDesigner))]
    public class GEMyView : Control
    {

        [Bindable(true), Category("显示样式"), Description("编号")]
        public GeViewEntity MyView
        {
            get;
            set;
        }

        private int _MyHistoryNum = 5;
        [Bindable(true), Category("显示样式"), Description("显示我的浏览记录的条数")]
        public int MyHistoryNum
        {
            get
            {
                return _MyHistoryNum;
            }
            set
            {
                if (value > 20)
                {
                    _MyHistoryNum = 20;
                }
                else
                {
                    _MyHistoryNum = value;
                }
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
        private int _VistorNum = 5;
        [Bindable(true), Category("显示样式"), Description("显示历史访客的条数")]
        public int VistorNum
        {
            get
            {
                return _VistorNum;
            }
            set
            {
                if (value > 20)
                {
                    _VistorNum = 20;
                }
                else
                {
                    _VistorNum = value;
                }
            }
        }

        private int _RecommentNum = 5;
        [Bindable(true), Category("显示样式"), Description("显示系统推荐的条数")]
        public int RecommendNum
        {
            get
            {
                return _RecommentNum;
            }
            set
            {
                if (value > 20)
                {
                    _RecommentNum = 20;
                }
                else
                {
                    _RecommentNum = value;
                }
            }
        }
        [Description("Css文件名")]
        public string CssFile
        {
            get;
            set;
        }

        private int _WeeklySortNum = 5;
        [Bindable(true), Category("显示样式"), Description("本周排行的数目")]
        public int WeeklySortNum
        {
            get
            {
                return _WeeklySortNum;
            }
            set
            {
                if (value > 20)
                {
                    _WeeklySortNum = 20;
                }
                else
                {
                    _WeeklySortNum = value;
                }
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<link rel='stylesheet'type='text/css' href='" + ResUrl + "GE/MyView/" + CssFile + "'/> \n");
            if (this.DesignMode)
            {
                DesignView(writer);
            }
            else
            {
                if (MyView != null)
                {
                    MyView.Save();
                    RuntimeView(writer);
                }
                else
                {
                    throw new Exception("没有给MyView赋值!");
                }  
            }
        }
        #region 设计视图
        private void DesignView(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewTitle");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            if (MyHistoryNum > 0)
            {
                //最近浏览
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("最近浏览:");
                writer.RenderEndTag();
                for (int i = 0; i < MyHistoryNum; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.Write("[数据绑定]");
                    writer.RenderEndTag();
                }
            }
            if (VistorNum > 0)
            {
                //访问历史
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("历史访客");
                writer.RenderEndTag();
                for (int i = 0; i < VistorNum; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.Write("[数据绑定]");
                    writer.RenderEndTag();
                }
            }
            if (RecommendNum > 0)
            {
                //系统推荐
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("系统推荐");
                writer.RenderEndTag();
                for (int i = 0; i < RecommendNum; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.Write("[数据绑定]");
                    writer.RenderEndTag();
                }
            }
            if (WeeklySortNum > 0)
            {
                //每周排行
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("上周排行");
                writer.RenderEndTag();
                for (int i = 0; i < WeeklySortNum; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.Write("[数据绑定]");
                    writer.RenderEndTag();
                }
            }
            writer.RenderEndTag();
        }
        #endregion

        #region 运行视图
        private void RuntimeView(HtmlTextWriter writer)
        {
          
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewTitle");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            if (MyHistoryNum > 0)
            {
                //最近浏览
                string strSql = "SELECT TOP " + MyHistoryNum + " Title,Url FROM GE_MyView WHERE FK_Emp='" + MyView.FK_Emp + "' and RefGroup='" + MyView.RefGroup + "'  ORDER BY RDT DESC";
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("最近浏览:");
                writer.RenderEndTag();
                int j = MyHistoryNum > dt.Rows.Count ? dt.Rows.Count : MyHistoryNum;
                for (int i = 0; i < j; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, dt.Rows[i]["Url"].ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(dt.Rows[i]["Title"].ToString());
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
            if (VistorNum > 0)
            {
                //历史访客
                string strSql = "SELECT TOP " + VistorNum + " Fk_EmpT,RDT FROM GE_MyView WHERE REFOID='" + MyView.RefOID +
                                "' AND RefGroup='" + MyView.RefGroup + "' GROUP BY FK_EmpT,RDT ORDER BY RDT DESC ";
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("历史访客");
                writer.RenderEndTag();
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
                int j = VistorNum > dt.Rows.Count ? dt.Rows.Count : MyHistoryNum;
                for (int i = 0; i < j; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.Write(dt.Rows[i]["FK_EmpT"].ToString());
                    writer.RenderEndTag();
                }
            }
            if (RecommendNum > 0)
            {
                //系统推荐
                string strSql = "SELECT COUNT(REFOID) AS num,refoid,title,url FROM GE_MyView GROUP BY REFOID,title,URL ORDER BY num DESC";
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("系统推荐");
                writer.RenderEndTag();
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
                int j = _RecommentNum > dt.Rows.Count ? dt.Rows.Count : RecommendNum;
                for (int i = 0; i < j; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, dt.Rows[i]["Url"].ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(dt.Rows[i]["Title"].ToString());
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
            if (WeeklySortNum > 0)
            {
                //系统推荐
                string strSql = "SELECT COUNT(REFOID) AS num,refoid,title,url FROM GE_MyView GROUP BY REFOID,title,URL ORDER BY num DESC";
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewHead");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write("上周排行");
                writer.RenderEndTag();
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql);
                int j = _RecommentNum > dt.Rows.Count ? dt.Rows.Count : RecommendNum;
                for (int i = 0; i < j; i++)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "viewItem");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, dt.Rows[i]["Url"].ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(dt.Rows[i]["Title"].ToString());
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
            writer.RenderEndTag();
        }
        #endregion

    }

    public class GeMyViewActionList : DesignerActionList
    {
        private GEMyView _GeMyView;
        private DesignerActionUIService designerActionUISvc = null;
        // The constructor associates the control 
        // With the smart tag list
        public GeMyViewActionList(IComponent component)
            : base(component)
        {
            this._GeMyView = component as GEMyView;

            // Cache a reference to DesignerActionUIService, 
            // so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

        }

        // Helper method to retrieve control properties. Use of 
        // GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(_GeMyView)[propName];
            if (null == prop)
                throw new ArgumentException(
                     "Matching ColorLabel property not found!",
                      propName);
            else
                return prop;
        }
        /// <summary>
        /// 该控件的属性
        /// </summary>
        public int MyHistoryNum
        {
            get
            {
                return _GeMyView.MyHistoryNum;
            }
            set
            {
                GetPropertyByName("MyHistoryNum").SetValue(_GeMyView, value);

            }
        }
        /// <summary>
        /// 该控件的属性
        /// </summary>
        public int VistorNum
        {
            get
            {
                return _GeMyView.VistorNum;
            }
            set
            {
                GetPropertyByName("VistorNum").SetValue(_GeMyView, value);
            }
        }
        /// <summary>
        /// 控件的属性
        /// </summary>
        public int RecommendNum
        {
            get
            {
                return _GeMyView.RecommendNum;
            }
            set
            {
                GetPropertyByName("RecommendNum").SetValue(_GeMyView, value);
            }

        }
        /// <summary>
        /// 属性
        /// </summary>
        public int WeeklySortNum
        {
            get
            {
                return _GeMyView.WeeklySortNum;
            }
            set
            {
                GetPropertyByName("WeeklySortNum").SetValue(_GeMyView, value);
            }
        }
        /// <summary>
        /// 重写父类的方法，实现在智能标签显示的属性
        /// </summary>
        /// <returns></returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionHeaderItem("Appearance"));
            items.Add(new DesignerActionPropertyItem("MyHistoryNum", "最近浏览数目"));
            items.Add(new DesignerActionPropertyItem("VistorNum", "历史访问数目"));
            items.Add(new DesignerActionPropertyItem("RecommendNum", "系统推荐数目"));
            items.Add(new DesignerActionPropertyItem("WeeklySortNum", "本周排行数目"));
            items.Add(new DesignerActionMethodItem(this, "RevertDefault", "使用默认值"));
            return items;
        }

        public void RevertDefault()
        {
            GetPropertyByName("WeeklySortNum").SetValue(_GeMyView, 5);
            GetPropertyByName("RecommendNum").SetValue(_GeMyView, 5);
            GetPropertyByName("VistorNum").SetValue(_GeMyView, 5);
            GetPropertyByName("MyHistoryNum").SetValue(_GeMyView, 5);
            //designerActionUISvc.Refresh(Component);
            //designerActionUISvc.HideUI(this.Component);
        }
    }
    public class GeMyViewDesigner : System.Web.UI.Design.ControlDesigner
    {
        // Use pull model to populate smart tag menu.
        private DesignerActionListCollection _actionLists;
        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(new GeMyViewActionList(this.Component));
                }
                return _actionLists;
            }
        }
    }
}

