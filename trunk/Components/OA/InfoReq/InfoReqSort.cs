

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class InfoReqSortAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// 类别
	/// </summary>
    public class InfoReqSort : EntityNoName
    {
        #region 构造函数
        /// <summary>
        /// 类别
        /// </summary>
        public InfoReqSort()
        {
        }
        public InfoReqSort(string no)
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
                Map map = new Map("GE_InfoReqSort");
                map.EnDesc = "类别";
                //map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.ImgLinks' >类别</a>";

                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(InfoReqSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(InfoReqSortAttr.Name, null, "名称", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 类别 
	/// </summary>
    public class InfoReqSorts : EntitiesNoName
    {
        /// <summary>
        /// 获取类别
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoReqSort();
            }
        }

        #region 构造函数
        /// <summary>
        /// 类别
        /// </summary>
        public InfoReqSorts()
        {
        }
        #endregion

    }
}
 

