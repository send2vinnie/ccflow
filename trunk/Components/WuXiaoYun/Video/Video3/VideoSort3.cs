using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 视频类型
    /// </summary>
    public class VideoSort3Attr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// 视频类型
    /// </summary>
    public class VideoSort3 : EntityNoName
    {
        #region 基本属性
        #endregion

        /// <summary>
        /// 视频类型
        /// </summary>
        public VideoSort3(string no)
            : base(no)
        {
        }
        /// <summary>
        /// 视频类型
        /// </summary>
        public VideoSort3()
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
                Map map = new Map("GE_VideoSort3");
                map.EnType = EnType.Sys;
                map.EnDesc = "视频类型";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Video3s' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Video3s", "AppName") + "</a>";
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = true;
                map.IsAllowRepeatName = false; 
                map.CodeStruct = "2";

                map.AddTBStringPK(VideoSort3Attr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(VideoSort3Attr.Name, null, "名称", true, false, 1, 100, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    /// <summary>
    /// 视频类型s
    /// </summary>
    public class VideoSort3s : EntitiesNoName
    {
        /// <summary>
        /// 视频类型s
        /// </summary>
        public VideoSort3s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new VideoSort3();
            }
        }
    }
}
