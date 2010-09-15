

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
	public class CtrlMangAttr:EntityNoNameAttr
	{
        public const string Note = "Note";
        public const string Val = "Val";
        public const string IsPJ = "IsPJ";
        public const string IsPL = "IsPL";
        public const string IsViewH = "IsViewH";
	}
	/// <summary>
	/// 组件管理
	/// </summary>
    public class CtrlMang : EntityNoName
    {
        #region 构造函数
        /// <summary>
        /// 组件管理
        /// </summary>
        public CtrlMang()
        {
        }
        public CtrlMang(string no)
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
                Map map = new Map("GE_CtrlMang");

                map.EnDesc = "组件管理";
                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(CtrlMangAttr.No, null, "应用标记", true, false, 2, 59, 2);
                map.AddTBString(CtrlMangAttr.Name, null, "应用名称", true, false, 0, 50, 300);

                map.AddDDLSysEnum(CtrlMangAttr.IsPJ, 0, "评价", true, false, "OpenOff", "@0=关闭@1=打开");
                /* 增加一些评价的属性, 增加类型放在这里，比如星级评价、 心情。。。。 */


                map.AddDDLSysEnum(CtrlMangAttr.IsPL, 0, "评论", true, false, "OpenOff", "@0=关闭@1=打开");
                /* 增加一些评论的属性  比如分页数量， 显示ip ,日期， 的格式化=== 。 */

                map.AddDDLSysEnum(CtrlMangAttr.IsViewH, 0, "浏览", true, false, "OpenOff", "@0=关闭@1=打开");
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "最近浏览显示数量(0不显示)", true, false);
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "系统推荐显示数量(0不显示)", true, false);
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "本周推荐显示数量(0不显示)", true, false);

                //     map.AddTBString(CtrlMangAttr.FK_Dept, null, "分组", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 组件管理 
	/// </summary>
	public class CtrlMangs: EntitiesNoName
	{
		/// <summary>
		/// 获取组件管理
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CtrlMang();
			}
		}

		#region 构造函数		
		/// <summary>
		/// 组件管理
		/// </summary>
		public CtrlMangs()
		{
		}		 
		#endregion
		 
		
	}
}
 

