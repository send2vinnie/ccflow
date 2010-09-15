using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    public enum PictSta
    {
        /// <summary>
        /// 删除
        /// </summary>
        Del,
        /// <summary>
        /// 普通的
        /// </summary>
        Ordinary,
        /// <summary> 
        /// 焦点的 
        /// </summary>
        Hot
    }
	/// <summary>
    /// 图片
	/// </summary>
    public class PictAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string PictSta = "PictSta";
    }
	/// <summary>
    /// 图片
	/// </summary>
    public class Pict : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(PictAttr.RDT);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string PictStaT
        {
            get
            {
                return this.GetValRefTextByKey(PictAttr.PictSta);
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(PictAttr.FK_Sort);
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
                return this.GetValStrByKey(PictAttr.FK_Sort);
            }
        }
        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 图片
        /// </summary>
        public Pict(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 图片
        /// </summary>
        public Pict()
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
                Map map = new Map("GE_Pict");
                map.EnType = EnType.Sys;
                map.EnDesc = "图片";
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.PictSorts' >图片类别</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = PictAttr.PictSta;

                map.AddTBStringPK(PictAttr.No, null, "编号", false, true, 6, 6, 6);
                map.AddDDLEntities(PictAttr.FK_Sort, null, "类别", new PictSorts(), true);
                map.AddDDLSysEnum(PictAttr.PictSta, 1, "状态", true, true, PictAttr.PictSta, "@0=不可用@1=普通@1=焦点");

                map.AddTBString(PictAttr.Name, null, "标题", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey("Name");
                attr.UIIsLine = true;

                map.AddTBStringDoc();
                attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddTBDate("RDT", "记录日期", true, true);
                map.AddMyFile();

                map.AddSearchAttr(PictAttr.FK_Sort);
                map.AddSearchAttr(PictAttr.PictSta);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 图片s
	/// </summary>
    public class Picts : EntitiesNoName
    {
        /// <summary>
        /// 图片s
        /// </summary>
        public Picts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Pict();
            }
        }
    }
}
