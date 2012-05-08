using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// 流程一户式
    /// </summary>
    public class OneWorkXml : XmlEnNoName
    {
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(Web.WebUser.SysLang);
            }
        }

        #region 构造
        /// <summary>
        /// 节点扩展信息
        /// </summary>
        public OneWorkXml()
        {
        }
        /// <summary>
        /// 获取一个实例
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new OneWorkXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// 流程一户式s
    /// </summary>
    public class OneWorkXmls : XmlEns
    {
        #region 构造
        /// <summary>
        /// 流程一户式s
        /// </summary>
        public OneWorkXmls() { }
        #endregion

        #region 重写基类属性或方法。
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new OneWorkXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfData + "\\Xml\\WFAdmin.xml";
            }
        }
        /// <summary>
        /// 物理表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return "OneWork";
            }
        }
        public override Entities RefEns
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
