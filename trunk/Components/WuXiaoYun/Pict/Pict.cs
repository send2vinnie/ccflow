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
        public const string Tag = "Tag";
        public const string Doc = "Doc";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string WebPath = "WebPath";
        public const string ReadTimes = "ReadTimes";
    }
	/// <summary>
    /// 图片
	/// </summary>
    public class Pict : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 关注度
        /// </summary>
        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(PictAttr.ReadTimes);
            }
            set
            {
                this.SetValByKey(PictAttr.ReadTimes, value);
            }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(PictAttr.WebPath);
            }
            set
            {
                this.SetValByKey(PictAttr.WebPath, value);
            }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(PictAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// 图片扩展名
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(PictAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey(PictAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey(PictAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(PictAttr.Tag);
            }
        }
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValHtmlStringByKey(PictAttr.Doc);
            }
        }

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
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.PictSorts' >类别</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = PictAttr.PictSta;

                map.AddTBStringPK(PictAttr.No, null, "编号", false, true, 6, 6, 6);
                map.AddTBString(PictAttr.Name, null, "标题", true, false, 1, 500, 10, true);
                map.AddDDLEntities(PictAttr.FK_Sort, null, "类别", new PictSorts(), true);
                map.AddTBString(PictAttr.Tag, null, BP.Sys.EnsAppCfgs.GetValString("BP.GE.Picts", "Tag"), true, false, 0, 500, 10, true);
                map.AddDDLSysEnum(PictAttr.PictSta, 1, "状态", true, true, PictAttr.PictSta, "@0=不可用@1=普通@1=焦点");
                map.AddTBInt(PictAttr.ReadTimes, 0, "访问次数", true, true);
                map.AddTBDate("RDT", "记录日期", true, true);
                map.AddTBStringDoc(PictAttr.Doc, null, "详细信息", true, false);

                Attr attr = map.GetAttrByKey(PictAttr.Doc);
                attr.UIIsLine = true;

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
