using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// 等级
	/// </summary>
    public class LevelAttr : EntityNoNameAttr
    {
    }
	/// <summary>
    /// 等级 的摘要说明。
	/// </summary>
    public class Level : EntityNoName
    {
        #region 等级
        #endregion 等级

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
        /// 等级
        /// </summary>		
        public Level() { }
        public Level(string no)
            : base(no)
        {
        }
        /// <summary>
        /// LevelMap
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
                map.PhysicsTable = "YG_Level";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "等级";
                map.EnType = EnType.App;
                #endregion

                map.AddTBStringPK(LevelAttr.No, null, "编号", true, false, 1, 20, 4);
                map.AddTBString(LevelAttr.Name, null, "名称", true, false, 0, 50, 200);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 等级s
	/// </summary>
	public class Levels : EntitiesNoName
    {
        #region 得到它的 Entity
        /// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new Level();
            }
		}	
		#endregion 

		#region 构造方法
		/// <summary>
        /// 等级s
		/// </summary>
		public Levels(){}
		#endregion
	}
	
}
