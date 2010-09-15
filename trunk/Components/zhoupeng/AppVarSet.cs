

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
	public class AppVarSetAttr:EntityNoNameAttr
	{
        public const string Note = "Note";
        public const string Val = "Val";
	}
	/// <summary>
	/// 系统变量设置
	/// </summary>
    public class AppVarSet : EntityNoName
    {
        #region 构造函数
        /// <summary>
        /// 系统变量设置
        /// </summary>
        public AppVarSet()
        {
        }
        public AppVarSet(string no)
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
                Map map = new Map("GE_AppVarSet");

                map.EnDesc = "系统变量设置";
                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(AppVarSetAttr.No, null, "Key", true, false, 2, 59, 2);
                map.AddTBString(AppVarSetAttr.Name, null, "名称", true, false, 0, 50, 300);
                map.AddTBString(AppVarSetAttr.Note, null, "备注", true, false, 0, 50, 300);
                map.AddMyFile("附件");

                //     map.AddTBString(AppVarSetAttr.FK_Dept, null, "分组", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 系统变量设置 
	/// </summary>
	public class AppVarSets: EntitiesNoName
	{
		/// <summary>
		/// 获取系统变量设置
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new AppVarSet();
			}
		}

		#region 构造函数		
		/// <summary>
		/// 系统变量设置
		/// </summary>
		public AppVarSets()
		{
		}		 
		#endregion
		 
		
	}
}
 

