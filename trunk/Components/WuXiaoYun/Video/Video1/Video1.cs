using System;
using System.Collections.Generic;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    /// <summary>
    /// 视频
    /// </summary>
    public class Video1Attr :VideoBaseAttr
    {
    }
    /// <summary>
    ///视频
    /// </summary>
    public class Video1 : VideoBase
    {
        #region 属性
        /// <summary>
        /// 数据库主表
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Video1";
            }
        }
        /// <summary>
        /// 数据库类别实体类
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.VideoSort1";
            }
        }
        /// <summary>
        /// 类别DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new VideoSort1s();
            }
        }

        #endregion 属性
    }
    /// <summary>
    /// 视频s
    /// </summary>
    public class Video1s :VideoBases
    {
        /// <summary>
        /// 视频s
        /// </summary>
        public Video1s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Video1();
            }
        }
    }
}
