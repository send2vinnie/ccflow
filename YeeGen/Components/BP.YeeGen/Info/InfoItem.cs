using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// InfoItem
	/// </summary>
    public class InfoItemAttr : EntityNoNameAttr
    {
        public const string FK_Model = "FK_Model";
    }
	/// <summary>
	/// InfoItem 的摘要说明。
	/// </summary>
    public class InfoItem : EntityNoName
    {
        #region 构造函数
        public string FK_Model
        {
            get
            {
                return this.GetValStrByKey(InfoItemAttr.FK_Model);
            }
            set
            {
                this.SetValByKey(InfoItemAttr.FK_Model, value);
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
        public InfoItem() { }
        public InfoItem(string no)
            : base(no)
        {
        }
        /// <summary>
        /// InfoItemMap
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
                map.PhysicsTable = "YG_InfoItem";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "模块类型";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                map.AddTBStringPK(InfoItemAttr.No, null, "编号", true, false, 4, 4, 4);
                map.AddTBString(InfoItemAttr.Name, null, "名称", true, false, 0, 50, 200);
                map.AddDDLEntities(InfoItemAttr.FK_Model, null, "模块", new InfoModels(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 模块类型
	/// </summary>
	public class InfoItems : EntitiesNoName
    {
        #region 得到它的 Entity
        /// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new InfoItem();
            }
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 客户s
		/// </summary>
		public InfoItems(){}
		#endregion
	}
	
}
