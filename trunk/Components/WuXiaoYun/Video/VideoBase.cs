using System;
using System.Collections.Generic;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;
namespace BP.GE
{
    /// <summary>
    /// 视频
    /// </summary>
    public class VideoBaseAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 视频描述
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// 更新日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 视频类型
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        /// <summary>
        /// 作者
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// 下载次数
        /// </summary>
        public const string DownTimes = "DownTimes";
        /// <summary>
        /// 浏览次数
        /// </summary>
        public const string ViewTimes = "ViewTimes";
        /// <summary>
        /// 推荐指数
        /// </summary>
        public const string RecomIdx = "RecomIdx";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        ///ICON路径
        /// </summary>
        public const string WebPath = "WebPath";
    }
    /// <summary>
    ///视频
    /// </summary>
    abstract public class VideoBase : EntityNoName
    {
        #region 属性
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// Author
        /// </summary>
        public string Author
        {
            get
            {
                return this.GetValStringByKey(VideoBaseAttr.Author);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.Author, value);
            }
        }
        /// <summary>
        /// ICON路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey(VideoBaseAttr.WebPath);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.WebPath, value);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(VideoBaseAttr.Tag);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.Tag, value);
            }
        }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownTimes
        {
            get
            {
                return this.GetValIntByKey(VideoBaseAttr.DownTimes);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.DownTimes, value);
            }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewTimes
        {
            get
            {
                return this.GetValIntByKey(VideoBaseAttr.ViewTimes);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.ViewTimes, value);
            }
        }
        /// <summary>
        /// 推荐指数
        /// </summary>
        public string RecomIdx
        {
            get
            {
                return this.GetValStringByKey(VideoBaseAttr.RecomIdx);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.RecomIdx, value);
            }
        }
        public string RecomIdxT
        {
            get
            {
                return this.GetValRefTextByKey(VideoBaseAttr.RecomIdx);
            }
        }
        /// <summary>
        /// 视频类型
        /// </summary>
        public string FK_Sort
        {
            get
            {
                return this.GetValStringByKey(VideoBaseAttr.FK_Sort);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.FK_Sort, value);
            }
        }
        public string FK_TypeT
        {
            get
            {
                return this.GetValRefTextByKey(VideoBaseAttr.FK_Sort);
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValHtmlStringByKey(VideoBaseAttr.Doc);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.Doc, value);
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(VideoBaseAttr.RDT);
            }
            set
            {
                this.SetValByKey(VideoBaseAttr.RDT, value);
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

        #endregion

        #region 构造方法
        /// <summary>
        ///获取视频
        /// </summary>
        public VideoBase()
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
                map.EnType = EnType.App;
                //map.EnDesc = "视频下载";
                map.EnDesc = this.GetCfgValStr("AppName");

                map.DepositaryOfEntity = Depositary.None;
                map.TitleExt = " - <a href='Ens.aspx?EnsName=" + SortEntity + "s' >类别</a>";
                map.CodeStruct = "6";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(VideoBaseAttr.No, null, "编号", true, true, 6, 6, 6);
                map.AddTBString(VideoBaseAttr.Name, null, "视频名称", true, false, 1, 300, 30);
                map.AddDDLEntities(VideoBaseAttr.FK_Sort, null, "视频类别", SortDDLEntities, true);
                map.AddTBString(VideoBaseAttr.Author, null, BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "Author"), true, false, 0, 30, 10);
                map.AddDDLSysEnum(VideoBaseAttr.RecomIdx, 1, "推荐指数", true, true, VideoBaseAttr.RecomIdx, "@0=0颗星@1=1颗星@2=2颗星@3=3颗星@4=4颗星@5=5颗星");
                map.AddTBInt(VideoBaseAttr.DownTimes, 0, "下载次数", false, true);
                map.AddTBInt(VideoBaseAttr.ViewTimes, 0, "浏览次数", true, true);
                map.AddTBString(VideoBaseAttr.Tag, null, BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "Tag"), true, false, 0, 500, 10, true);
                map.AddTBStringDoc(VideoBaseAttr.Doc, null, "描述", true, false);
                map.AddTBDate(VideoBaseAttr.RDT, "更新日期", true, false);

                Attr attr = map.GetAttrByKey(VideoBaseAttr.Doc);
                attr.UIIsLine = true;
                map.AddSearchAttr(VideoBaseAttr.FK_Sort);

                map.AddMyFile("视频图片");
                map.AddMyFile("视频", "Video");

                this._enMap = map;
                return this._enMap;
            }
        }

        protected override bool beforeUpdateInsertAction()
        {
            SysFileManager file = new SysFileManager();
            int i = file.Retrieve(SysFileManagerAttr.RefVal, this.No,
                SysFileManagerAttr.EnName, this.ToString(), SysFileManagerAttr.AttrFileNo, "ICON");

            if (i != 0)
            {
                this.WebPath = file.WebPath;
            }

            return base.beforeUpdateInsertAction();
        }
        #endregion
    }
    /// <summary>
    /// 视频s
    /// </summary>
    abstract public class VideoBases : EntitiesNoName
    {
        /// <summary>
        /// 视频s
        /// </summary>
        public VideoBases()
        {
        }
        /// <summary>
        /// 视频推荐:top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveByRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.addOrderByDesc(VideoBaseAttr.RecomIdx, VideoBaseAttr.RDT);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }

        /// <summary>
        /// 某一类别视频推荐:top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecomByType(string fk_type, int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(VideoBaseAttr.FK_Sort, fk_type);
            qo.addOrderByDesc(VideoBaseAttr.RecomIdx, VideoBaseAttr.RDT);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}
