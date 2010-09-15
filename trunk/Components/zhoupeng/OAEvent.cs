using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
 
	/// <summary>
    /// 招生简章备案
	/// </summary>
    public class OAEventAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string PictSta = "PictSta";
    }
	/// <summary>
    /// 招生简章备案
	/// </summary>
    public class OAEvent : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(OAEventAttr.RDT);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string PictStaT
        {
            get
            {
                return this.GetValRefTextByKey(OAEventAttr.PictSta);
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(OAEventAttr.FK_Sort);
            }
        }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
        }
        /// <summary>
        /// 文件个数
        /// </summary>
        public int MyFileNum
        {
            get
            {
                return this.GetValIntByKey("MyFileNum");
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey("Doc");
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(OAEventAttr.FK_Sort);
            }
        }
        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 招生简章备案
        /// </summary>
        public OAEvent(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 招生简章备案
        /// </summary>
        public OAEvent()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("GE_OAEvent");
                map.EnType = EnType.Sys;
                map.EnDesc = "招生简章备案";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";

                map.AddTBStringPK(OAEventAttr.No, null, "编号", false, true, 6, 6, 6);
                map.AddDDLEntities(OAEventAttr.FK_Sort, null, "类别", new PictSorts(), true);
                map.AddDDLSysEnum(OAEventAttr.PictSta, 1, "状态", true, true, OAEventAttr.PictSta, "@0=不可用@1=普通@1=焦点");

                map.AddTBString(OAEventAttr.Name, null, "标题", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey("Name");
                attr.UIIsLine = true;

                map.AddTBStringDoc();
                attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;


                map.AddMyFile("身份证复印件","ID");
                map.AddMyFile("公司章程","Inc");

                map.AddSearchAttr(OAEventAttr.FK_Sort);
                map.AddSearchAttr(OAEventAttr.PictSta);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 招生简章备案s
	/// </summary>
    public class OAEvents : EntitiesNoName
    {
        /// <summary>
        /// 招生简章备案s
        /// </summary>
        public OAEvents()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new OAEvent();
            }
        }
    }
}
