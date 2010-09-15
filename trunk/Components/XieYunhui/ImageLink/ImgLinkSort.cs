

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class ImgLinkSortAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// 类别
	/// </summary>
    public class ImgLinkSort : EntityNoName
    {
        #region 构造函数
        /// <summary>
        /// 类别
        /// </summary>
        public ImgLinkSort()
        {
        }
        public ImgLinkSort(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {

            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_ImgLinkSort");
                map.EnDesc = "类别";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.ImgLinks' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLinks", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(ImgLinkSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(ImgLinkSortAttr.Name, null, "名称", true, false, 0, 50, 300);
                //  map.AddBoolean(ImgLinkSortAttr.IsShowDtl,true, "可用否？", true,false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 类别 
	/// </summary>
    public class ImgLinkSorts : EntitiesNoName
    {
        /// <summary>
        /// 获取类别
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLinkSort();
            }
        }

        #region 构造函数
        /// <summary>
        /// 类别
        /// </summary>
        public ImgLinkSorts()
        {
        }
        #endregion

    }
}
 

