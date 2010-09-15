using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 视频类型
    /// </summary>
    public class VideoSortAttr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// 视频类型
    /// </summary>
    public class VideoSort : EntityNoName
    {
        #region 基本属性
        #endregion

        /// <summary>
        /// 视频类型
        /// </summary>
        public VideoSort(string no)
            : base(no)
        {
        }
        /// <summary>
        /// 视频类型
        /// </summary>
        public VideoSort()
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
                Map map = new Map("GE_VideoSort");
                map.EnType = EnType.Sys;
                map.EnDesc = "视频类型";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Videos' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Videos", "AppName") + "</a>";
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = true;
                map.IsAllowRepeatName = false; 
                map.CodeStruct = "2";

                map.AddTBStringPK(VideoSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(VideoSortAttr.Name, null, "名称", true, false, 1, 100, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    /// <summary>
    /// 视频类型s
    /// </summary>
    public class VideoSorts : EntitiesNoName
    {
        /// <summary>
        /// 视频类型s
        /// </summary>
        public VideoSorts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new VideoSort();
            }
        }
    }
}
