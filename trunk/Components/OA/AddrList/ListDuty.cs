

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class ListDutyAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// 联系人职务
	/// </summary>
    public class ListDuty : EntityNoName
    {
        #region 构造函数
        /// <summary>
        /// 联系人职务
        /// </summary>
        public ListDuty()
        {
        }
        public ListDuty(string no)
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
                Map map = new Map("GE_ListDuty");
                map.EnDesc = "联系人职务";

                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                //  map.IsAutoGenerNo = true;

                map.AddTBStringPK(PictSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(ListDutyAttr.Name, null, "名称", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 联系人职务 
	/// </summary>
	public class ListDutys: EntitiesNoName
	{
		/// <summary>
		/// 获取联系人职务
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ListDuty();
			}
		}

		#region 构造函数		
		/// <summary>
		/// 联系人职务
		/// </summary>
		public ListDutys()
		{
		}		 
		#endregion
		 
		
	}
}
 

