using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// TouGao
	/// </summary>
    public class TouGaoAttr : EntityNoNameAttr
    {
        public const string Author = "Author";
        public const string RDT = "RDT";
        public const string TouGaoSta = "TouGaoSta";

    }
	/// <summary>
	/// TouGao 的摘要说明。
	/// </summary>
    public class TouGao : EntityOIDName
    {
        #region 构造函数
        public string Author
        {
            get
            {
                return this.GetValStringByKey(TouGaoAttr.Author);
            }
            set
            {
                this.SetValByKey(TouGaoAttr.Author, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TouGaoAttr.RDT);
            }
            set
            {
                this.SetValByKey(TouGaoAttr.RDT, value);
            }
        }
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
        public TouGao() { }
        public TouGao(int no)
            : base(no)
        {
        }
        /// <summary>
        /// TouGaoMap
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
                map.PhysicsTable = "YG_TouGao";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "投稿";
                map.EnType = EnType.App;
                #endregion

                map.AddTBIntPKOID();

                map.AddDDLSysEnum(TouGaoAttr.TouGaoSta, 0, "状态", true, true, TouGaoAttr.TouGaoSta,"@0=未审核@1=审核通过@2=不通过");
                map.AddTBString(TouGaoAttr.Name, null, "稿件名称", true, false, 0, 50, 200);
                map.AddTBString(TouGaoAttr.Author, null, "作者", true, false, 0, 50, 200);
                map.AddTBDateTime(TouGaoAttr.RDT, null, "记录日期", true, false);

                map.AddSearchAttr(TouGaoAttr.TouGaoSta);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 投稿
	/// </summary>
	public class TouGaos : EntitiesNoName
    {
        #region 得到它的 Entity
        /// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new TouGao();
            }
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 客户s
		/// </summary>
		public TouGaos(){}
		#endregion
	}
	
}
