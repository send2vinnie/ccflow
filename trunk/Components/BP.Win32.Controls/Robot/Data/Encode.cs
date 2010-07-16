using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// 编码类型
	/// </summary>
    public class EncodeAttr : EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// Docs
        /// </summary>
        public const string Docs = "Docs";
        #endregion
    }
	/// <summary>
	/// 编码类型
	/// </summary>
    public class Encode : EntityNoName
    {
        #region 基本属性
        #endregion

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// 编码类型
        /// </summary>		
        public Encode()
        {

        }
        /// <summary>
        /// 编码类型
        /// </summary>
        /// <param name="no"></param>
        public Encode(string no)
            : base(no)
        {
        }
        /// <summary>
        /// EncodeMap
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
                map.PhysicsTable = "RB_Encode";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "网站编码类型";
                map.EnType = EnType.App;
                #endregion

                #region 基本属性

                map.AddTBStringPK(EncodeAttr.No, null, "编号", true, true, 2, 2, 4);
                map.AddTBString(EncodeAttr.Name, null, "名称", true, true, 0, 4000, 30);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 重载方法
        #endregion
    }
	/// <summary>
	/// 编码类型
	/// </summary>
    public class Encodes : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Encode();
            }
        }	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 编码类型
		/// </summary>
		public Encodes()
        {
        }
		#endregion

	 
	}
	
}
