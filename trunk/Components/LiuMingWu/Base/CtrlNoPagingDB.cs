using System.Web.UI;
using System.Data;
using System;
using BP;
using System.ComponentModel;
using BP.En;
using BP.GE.Ctrl;

namespace BP.GE
{
    [ParseChildren(true, "UrlParas")] 
    public abstract class CtrlNoPagingDB : Control
    {
        #region

        private object _DBSource;
        /// <summary>
        /// 数据源
        /// </summary>
        [DefaultValue(""),
        Category("全局属性"),
        Description("数据源")]
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
        /// 数据源类型
        /// </summary>
        private DBSourceType dbType;
        [DefaultValue("SQL"),
        Category("全局属性"),
        Description("数据源类型")]
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
        /// 数据源类型
        /// </summary>
        private string _Url;
        [DefaultValue(""),
        Category("全局属性"),
        Description("Url")]
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }
         /// <summary>
        /// URL参数类型
        /// </summary>
        private UrlItems items;
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Category("全局属性")]
        public UrlItems UrlParas
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new UrlItems();
                }
                return items;
            }
            set
            {
                items = value;
            }
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                RenderDesignView(writer);
            }
            else
            {
                DataTable dt = GetData();
                RenderRuntimeView(writer, dt);
            }
        }


        /// <summary>
        /// 设计视图
        /// </summary>
        /// <param name="writer"></param>
        public abstract void RenderDesignView(HtmlTextWriter writer);
        /// <summary>
        /// 运行视图
        /// </summary>
        /// <param name="writer"></param>
        public abstract void RenderRuntimeView(HtmlTextWriter writer, DataTable dt);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            switch (GloDBType)
            {
                case DBSourceType.SQL:
                    dt = BP.DA.DBAccess.RunSQLReturnTable(Convert.ToString(GloDBSource));
                    return dt;
                case DBSourceType.Xml:
                    string xmlName = Convert.ToString(GloDBSource);
                    BP.XML.XmlEns xmlensName = BP.DA.ClassFactory.GetXmlEns(xmlName);
                    xmlensName.RetrieveAll();
                    dt = xmlensName.GetTable();
                    return dt;
                case DBSourceType.DataTable:
                    dt = GloDBSource as DataTable;
                    return dt;
                case DBSourceType.DataSet:
                    dt = (GloDBSource as DataSet).Tables[0];
                    return dt;
                case DBSourceType.Ens:
                    dt = ((Entities)GloDBSource).ToDataTableField();
                    return dt;
                default:
                    return dt;
            }
            
        }

        /// <summary>
        /// 获取URL参数值
        /// </summary>
        /// <param name="dr">当前行</param>
        /// <param name="li">列</param>
        /// <returns>把参数替换后的URL</returns>
        public string GetUrl(DataRow dr,string href)
        {
            string strReturnValue = href;
            string strValue = string.Empty;
            foreach (UrlList Para in UrlParas)
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
    }
}