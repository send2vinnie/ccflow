using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// InfoModel
	/// </summary>
    public class InfoModelAttr : EntityNoNameAttr
    {
    }
	/// <summary>
	/// InfoModel 的摘要说明。
	/// </summary>
    public class InfoModel : EntityNoName
    {
        #region 构造函数
        #endregion 构造函数

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 客户
        /// </summary>		
        public InfoModel() { }
        public InfoModel(string no)
            : base(no)
        {
        }
        /// <summary>
        /// InfoModelMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "YG_InfoModel";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "模块类型";
                map.EnType = EnType.App;
                #endregion

                map.AddTBStringPK(InfoModelAttr.No, null, "编号", true, false, 1, 20, 4);
                map.AddTBString(InfoModelAttr.Name, null, "名称", true, false, 0, 50, 200);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 模块类型
	/// </summary>
	public class InfoModels : EntitiesNoName
    {
        #region 得到它的 Entity
        /// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new InfoModel();
            }
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 客户s
		/// </summary>
		public InfoModels(){}
		#endregion
	}
	
}
