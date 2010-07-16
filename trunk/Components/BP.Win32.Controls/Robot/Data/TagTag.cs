using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.RB
{
	/// <summary>
	/// 标签vs标签
	/// </summary>
    public class TagTagAttr
    {
        #region 基本属性
        /// <summary>
        /// T1
        /// </summary>
        public const string T1 = "T1";
        /// <summary>
        /// T2
        /// </summary>
        public const string T2 = "T2";
        #endregion
    }
	/// <summary>
	/// 标签vs标签
	/// </summary>
    public class TagTag : Entity
    {
        #region 基本属性
        public string T1
        {
            get
            {
                return this.GetValStringByKey(TagTagAttr.T1);
            }
            set
            {
                this.SetValByKey(TagTagAttr.T1, value);
            }
        }
        public string T2
        {
            get
            {
                return this.GetValStringByKey(TagTagAttr.T2);
            }
            set
            {
                this.SetValByKey(TagTagAttr.T2, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
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
        /// TagTag
        /// </summary>
        public TagTag()
        {
        }
        /// <summary>
        /// EnMap
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
                map.PhysicsTable = "SE_TagTag";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "标签vs标签";
                map.EnType = EnType.App;


                map.AddTBIntPK(TagTagAttr.T1, 0, "T1", true, true);
                map.AddTBIntPK(TagTagAttr.T2, 0, "T2", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
    }
	/// <summary>
	/// 标签vs标签
	/// </summary>
	public class TagTags : Entities
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TagTag();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 标签vs标签
		/// </summary>
		public TagTags(){}
		#endregion
	}
	
}
