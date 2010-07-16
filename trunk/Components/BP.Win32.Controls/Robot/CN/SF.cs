using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.CN
{
	/// <summary>
	/// 省份
	/// </summary>
	public class SFAttr: EntityNoNameAttr
	{
		#region 基本属性
		public const  string FK_PQ="FK_PQ";
		#endregion
	}
	/// <summary>
    /// 省份
	/// </summary>
	public class SF :EntityNoName
	{	
		#region 基本属性
        public string FK_PQ
        {
            get
            {
                return this.GetValStrByKey(SFAttr.FK_PQ);
            }
        }

		 
		#endregion 

		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// 省份
		/// </summary>		
		public SF(){}
		public SF(string no):base(no)
		{
		}

		
		/// <summary>
		/// Map
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
                map.PhysicsTable = "CN_SF";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "省份";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region 字段
                map.AddTBStringPK(SFAttr.No, null, "编号", true, false, 0, 50, 50);
                map.AddTBString(SFAttr.Name, null, "名称", true, false, 0, 50, 200);

                map.AddDDLEntities(SFAttr.FK_PQ, null, "片区", new PQs(), true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
		 
	}
	/// <summary>
	/// 省份
	/// </summary>
	public class SFs : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SF();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 省份s
		/// </summary>
		public SFs(){}
		#endregion
	}
	
}
