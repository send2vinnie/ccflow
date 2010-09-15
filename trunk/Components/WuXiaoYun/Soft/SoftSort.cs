using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 软件类型
    /// </summary>
    public class SoftSortAttr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// 软件类型
    /// </summary>
    public class SoftSort : EntityNoName
    {
        #region 基本属性



        #endregion

        /// <summary>
        /// 软件类型
        /// </summary>
        public SoftSort(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 软件类型
        /// </summary>
        public SoftSort()
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
                Map map = new Map("GE_SoftSort");
                map.EnType = EnType.Sys;
                map.EnDesc = "软件类型";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Softs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Softs", "AppName") + "</a>";
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = true;
                map.IsAllowRepeatName = false; 
                map.CodeStruct = "2";

                map.AddTBStringPK(SoftSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(SoftSortAttr.Name, null, "名称", true, false, 1, 100, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    /// <summary>
    /// 软件类型s
    /// </summary>
    public class SoftSorts : EntitiesNoName
    {
        /// <summary>
        /// 软件类型s
        /// </summary>
        public SoftSorts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SoftSort();
            }
        }
    }
}
