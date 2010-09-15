using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 信息发布
    /// </summary>
    public class InfoBaseAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string InfoSta = "InfoSta";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string NumRead = "NumRead";
        public const string WebPath = "WebPath";
        public const string Author = "Author";
    }
    /// <summary>
    /// 信息发布
    /// </summary>
    abstract public class InfoBase : EntityNoName
    {
        #region 属性
        public string Author
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.Author);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.Author, value);
            }
        }
        /// <summary>
        /// 标题图片路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.WebPath);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.WebPath, value);
            }
        }
        /// <summary>
        /// 标题图片名称
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// 标题图片扩展名
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// 标题图片宽度
        /// </summary>
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// 标题图片高度
        /// </summary>
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.RDT, value);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string InfoSta
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.InfoSta);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.InfoSta, value);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string InfoStaT
        {
            get
            {
                return this.GetValRefTextByKey(InfoBaseAttr.InfoSta);
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(InfoBaseAttr.FK_Sort);
            }
        }
        public int NumRead
        {
            get
            {
                return this.GetValIntByKey(InfoBaseAttr.NumRead);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.NumRead, value);
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
                return this.GetValStrByKey(InfoBaseAttr.FK_Sort);
            }
        }
        /// <summary>
        /// 数据库主表
        /// </summary>
        public abstract string PTable
        {
            get;
        }
        /// <summary>
        /// 数据库类别表
        /// </summary>
        public abstract string SortEntity
        {
            get;
        }
        /// <summary>
        /// 类别DDLEntitees
        /// </summary>
        public abstract EntitiesNoName SortDDLEntities
        {
            get;
        }

        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoBase(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoBase()
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
                Map map = new Map(PTable);
                map.EnType = EnType.Sys;
                map.EnDesc = this.GetCfgValStr("AppName");
                //map.EnDesc = BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "AppName");
                map.TitleExt = " - <a href='Ens.aspx?EnsName=" + SortEntity + "s' >类别</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = InfoBaseAttr.InfoSta;

                map.AddTBStringPK(InfoBaseAttr.No, null, "编号", false, true, 6, 6, 6);
                map.AddTBString(InfoBaseAttr.Name, null, "标题", true, false, 1, 500, 10, true);
                map.AddDDLEntities(InfoBaseAttr.FK_Sort, null, "类别", SortDDLEntities, true);
                map.AddDDLSysEnum(InfoBaseAttr.InfoSta, 1, "状态", true, true, InfoBaseAttr.InfoSta, "@0=不可见@1=普通@2=焦点@3=图片信息");
                map.AddTBDate(InfoBaseAttr.RDT, "记录日期", true, true);
                map.AddTBString(InfoBaseAttr.Author, null, "作者", true, false, 0, 20, 20);
                map.AddTBInt(InfoBaseAttr.NumRead, 0, "阅读次数", true, true);

                map.AddTBStringDoc();
                Attr attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddSearchAttr(InfoBaseAttr.FK_Sort);
                map.AddSearchAttr(InfoBaseAttr.InfoSta);

                map.AddMyFile();
                map.AddMyFileS();

                this._enMap = map;
                return this._enMap;
            }
        }

        #endregion
    }
    /// <summary>
    /// 信息发布s
    /// </summary>
    abstract public class InfoBases : EntitiesNoName
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public InfoBases()
        {

        }
        /// <summary>
        /// 排序(按日期):top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereNotIn(InfoBaseAttr.InfoSta, "0");
            qo.addOrderByDesc(InfoBaseAttr.No);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();

        }

        /// <summary>
        /// 某一类别排序(按日期):top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveByType(string fk_type, int top)
        {
            // return this.RetrieveFromCash(InfoAttr.FK_Sort, fk_type, InfoAttr.InfoSta, 0, "No", false);
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(InfoBaseAttr.FK_Sort, fk_type);
            qo.addAnd();
            qo.AddWhereNotIn(InfoBaseAttr.InfoSta, "0");
            qo.addOrderByDesc(InfoBaseAttr.No);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}
