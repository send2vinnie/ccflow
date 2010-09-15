using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 信息发布
    /// </summary>
    public class InfoListBaseAttr : EntityNoNameAttr
    {
        public const string RDT = "RDT";
        public const string InfoListSta = "InfoListSta";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string NumRead = "NumRead";
        public const string WebPath = "WebPath";
    }
    /// <summary>
    /// 信息发布
    /// </summary>
    abstract public class InfoListBase : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 标题图片路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.WebPath);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.WebPath, value);
            }
        }
        /// <summary>
        /// 标题图片名称
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// 标题图片扩展名
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// 标题图片宽度
        /// </summary>
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// 标题图片高度
        /// </summary>
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.RDT);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string InfoListSta
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.InfoListSta);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.InfoListSta, value);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string InfoListStaT
        {
            get
            {
                return this.GetValRefTextByKey(InfoListBaseAttr.InfoListSta);
            }
        }
        public int NumRead
        {
            get
            {
                return this.GetValIntByKey(InfoListBaseAttr.NumRead);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.NumRead, value);
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
        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoListBase(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoListBase()
        {
        }
        /// <summary>
        /// 数据库表
        /// </summary>
        public abstract string PTable
        {
            get;
        }

        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map(this.PTable);
                map.EnType = EnType.Sys;
                map.EnDesc = this.GetCfgValStr("AppName");
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = InfoListBaseAttr.InfoListSta;

                map.AddTBString(InfoListBaseAttr.Name, null, "标题", true, false, 1, 500, 10, true);
                map.AddTBStringPK(InfoListBaseAttr.No, null, "编号", false, true, 6, 6, 6);
                map.AddDDLSysEnum(InfoListBaseAttr.InfoListSta, 1, "状态", true, true, InfoListBaseAttr.InfoListSta, "@0=不可见@1=普通@2=焦点@3=图片信息");

                map.AddTBStringDoc();
                Attr attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddTBDate("RDT", "记录日期", true, true);
                map.AddTBInt(InfoListBaseAttr.NumRead, 0, "阅读次数", true, true);

                map.AddSearchAttr(InfoListBaseAttr.InfoListSta);

                //map.AddMyFile("标题图片", "InfoListBasePic");

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
    abstract public class InfoListBases : EntitiesNoName
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public InfoListBases()
        {
        }
        /// <summary>
        /// 排序(按日期):top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereNotIn("InfoListSta", "0");
            qo.addOrderByDesc("RDT");
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}
