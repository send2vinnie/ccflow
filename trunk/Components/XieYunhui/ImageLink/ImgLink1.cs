using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    public enum UrlType
    {
        Ftp,
        Http
    }
    /// <summary>
    /// 图片连接连接
    /// </summary>
    public class ImgLink1Attr : EntityNoNameAttr
    {
        public const string RDT = "RDT";
        public const string Url = "Url";
        public const string Target = "Target";
        /// <summary>
        /// 是否是焦点
        /// </summary>
        public const string IsFocus = "IsFocus";
        /// <summary>
        /// 资源类型(URL还是FTP)
        /// </summary>
        public const string UrlType = "UrlType";
       // public const string UrlHidden = "UrlHidden";
        public const string ReadTimes = "ReadTimes";
    }
    /// <summary>
    /// 图片连接
    /// </summary>
    public class ImgLink1 : EntityNoName
    {
        #region 属性

        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(ImgLink1Attr.ReadTimes);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.ReadTimes, value);
            }
        }

        public UrlType HisUrlType
        {
            get
            {
                return (UrlType)this.GetValIntByKey(ImgLink1Attr.UrlType);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.UrlType, (int)value);
            }
        }
        // 记录日期
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(ImgLink1Attr.RDT);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.RDT, value);
            }
        }

        // 是否设为焦点
        public bool IsFocus
        {
            get
            {
                return this.GetValBooleanByKey(ImgLink1Attr.IsFocus);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.IsFocus, value);
            }
        }

        // 图片的链接
        public string Url
        {
            get
            {
                return this.GetValStrByKey(ImgLink1Attr.Url);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.Url, value);
            }
        }
        //public string UrlHidden
        //{
        //    get
        //    {
        //        return this.GetValStrByKey(ImgLink1Attr.UrlHidden);
        //    }
        //    set
        //    {
        //        this.SetValByKey(ImgLink1Attr.UrlHidden, value);
        //    }
        //}

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

        public string WebPath
        {
            get
            {
                return this.GetValStrByKey("WebPath");
            }
        }


        // 是否在本窗口打开
        public string Target
        {
            get
            {
                switch (this.GetValIntByKey(ImgLink1Attr.Target))
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
        public ImgLink1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 图片连接
        /// </summary>
        public ImgLink1()
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
                Map map = new Map("GE_ImgLink1");

                map.EnType = EnType.Sys;
                map.EnDesc = BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "AppName");
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "3";

                map.AddTBStringPK(ImgLink1Attr.No, null, "编号", false, true, 3, 3, 3);
                map.AddTBString(ImgLink1Attr.Name, null, "标题", true, false, 1, 500, 10, true);
                map.AddTBString(ImgLink1Attr.Url, null, "链接", true, false, 1, 500, 100, true);
                //map.AddTBString(ImgLink1Attr.UrlHidden, null, "链接隐藏的", true, false, 0, 500, 100, true);
                map.AddBoolean(ImgLink1Attr.IsFocus, false, "是否是焦点", true, true);
                map.AddDDLSysEnum(ImgLink1Attr.UrlType, 0, "资源类型", true, true,
                    ImgLink1Attr.UrlType, "@0=FTP资源@1=Http链接");

                map.AddDDLSysEnum(ImgLink1Attr.Target, 1, "打开方式", true, true,
                    ImgLink1Attr.Target, "@0=本窗口打开@1=新窗口打开");
                map.AddTBInt(ImgLink1Attr.ReadTimes, 0, "浏览次数", true, true);
                map.AddMyFile("图片");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        #region 方法
        
        //打开FTP
        public void OpenFTP()
        {
            GloFTP.OpenFtp();
        }
        protected override bool beforeUpdateInsertAction()
        {
            //this.UrlHidden = this.Url;
            string startStr = "";
            //补足地址
            switch (this.HisUrlType)
            {
                //链接资源
                case UrlType.Http:
                    startStr = "http:";
                    break;
                //FTP资源
                case UrlType.Ftp:
                    startStr = "ftp:";

                    //this.UrlHidden= System.con
                    break;
                default:
                    break;
            }

            if (startStr != "")
            {
                if (!this.Url.ToLower().StartsWith(startStr))
                {
                    if (!this.Url.ToLower().StartsWith("//"))
                    {
                        this.Url = "//" + this.Url;
                    }
                    this.Url = startStr + this.Url;
                }
            }

            return base.beforeUpdateInsertAction();
        }
        #endregion

    }
    /// <summary>
    /// 图片连接s
    /// </summary>
    public class ImgLink1s : EntitiesNoName
    {
        /// <summary>
        /// 图片连接s
        /// </summary>
        public ImgLink1s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLink1();
            }
        }
        /// <summary>
        /// 得到设置为焦点的信息
        /// </summary>
        public int RetrieveFocus()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(ImgLink1Attr.IsFocus, true);
            return qo.DoQuery();
        }
    }
}
