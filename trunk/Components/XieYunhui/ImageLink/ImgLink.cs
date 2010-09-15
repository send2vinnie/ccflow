using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	/// <summary>
    /// 图片连接连接
	/// </summary>
    public class ImgLinkAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string Url = "Url";
        public const string Target = "Target";
        /// <summary>
        /// 是否是焦点
        /// </summary>
        public const string IsFocus = "IsFocus";
    }
	/// <summary>
    /// 图片连接
	/// </summary>
    public class ImgLink : EntityNoName
    {
        #region 属性
        // 记录日期
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.RDT);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.RDT, value);
            }
        }

        // 是否设为焦点
        public string IsFocus
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.IsFocus);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.IsFocus, value);
            }
        }

        // 图片的链接
        public string Url
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.Url);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.Url, value);
            }
        }

        // 图片后缀
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
        }

        // 图片名
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey("MyFileName");
            }
        }

        // 图片的高度
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey("MyFileH");
            }
        }

        // 图片宽度
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey("MyFileW");
            }
        }

        // 图片大小
        public string MyFileSize
        {
            get
            {
                return this.GetValStrByKey("MyFileSize");
            }
        }

        // 类别
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.FK_Sort);
            }
        }

        // 是否在本窗口打开
        public string Target
        {
            get
            {
                switch (this.GetValIntByKey(ImgLinkAttr.Target))
                {
                    case 0:
                        return "_self";
                    default:
                        return "_blank";
                }
            }
        }
        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 图片连接
        /// </summary>
        public ImgLink(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 图片连接
        /// </summary>
        public ImgLink()
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
                Map map = new Map("GE_ImgLink");

                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.ImgLinkSorts' >类别</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.ImgLinks')\">属性设置</a>";

                map.EnType = EnType.Sys;
                map.EnDesc = "图片连接";
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "2";

                map.AddTBStringPK(ImgLinkAttr.No, null, "编号", false, true, 2, 2, 2);
                map.AddDDLEntities(ImgLinkAttr.FK_Sort, null, "类别", new ImgLinkSorts(), true);

                map.AddTBString(ImgLinkAttr.Name, null, "标题", true, false, 0, 500, 10, true);
                map.AddTBString(ImgLinkAttr.Url, null, "连接", true, false, 0, 500, 100, true);

                map.AddBoolean(ImgLinkAttr.IsFocus, false, "是否是焦点", true, true);
                map.AddDDLSysEnum(ImgLinkAttr.Target, 1, "打开方式", true, true, ImgLinkAttr.Target, "@0=本窗口打开@1=新窗口打开");
                map.AddMyFile("图片");
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 图片连接s
	/// </summary>
    public class ImgLinks : EntitiesNoName
    {
        /// <summary>
        /// 图片连接s
        /// </summary>
        public ImgLinks()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLink();
            }
        }
    }
}
