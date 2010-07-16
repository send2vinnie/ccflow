using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	///  
	/// </summary>
    public class SysEnAttr : EntityEnsNameAttr
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 实体名称
        /// </summary>
        public const string EnEnsName = "EnEnsName";
        /// <summary>
        /// 物理表
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// 实体类型
        /// </summary> 
        public const string EnType = "EnType";
    }
	/// <summary>
	/// SysEns
	/// </summary>
    public class SysEn : EntityEnsName
    {
        #region 基本属性
        public Entity En
        {
            get
            {
                return ClassFactory.GetEn(this.EnEnsName);
            }
        }
        public Entities Ens
        {
            get
            {
                return ClassFactory.GetEns(this.EnsEnsName);
            }
        }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EnEnsName
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.EnEnsName);
            }
            set
            {
                this.SetValByKey(SysEnAttr.EnEnsName, value);
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public string PTable
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.PTable);
            }
            set
            {
                this.SetValByKey(SysEnAttr.PTable, value);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.Name);
            }
            set
            {
                this.SetValByKey(SysEnAttr.Name, value);
            }
        }
        /// <summary>
        /// 实体类型 0 , 应用, 1, 管理员维护, 2, 预制实体.
        /// </summary>
        public int EnTypeOFInt
        {
            get
            {
                return this.GetValIntByKey(SysEnAttr.EnType);
            }
            set
            {
                this.SetValByKey(SysEnAttr.EnType, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 系统实体
        /// </summary>
        public SysEn()
        {
        }
        /// <summary>
        /// 系统实体
        /// </summary>
        /// <param name="EnsEnsName">类名称</param>
        public SysEn(string EnsEnsName)
        {
            this.EnsEnsName = EnsEnsName;
            if (this.IsExits == false)
            {
                Entities ens = ClassFactory.GetEns(this.EnsEnsName);
                Entity en = ens.GetNewEntity;
                this.Name = en.EnDesc;
                this.EnEnsName = en.ToString();
                this.EnTypeOFInt = (int)en.EnMap.EnType;
                this.PTable = en.EnMap.PhysicsTable;
                this.Insert();
            }
            else
            {
                this.Retrieve();
            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_Ens");
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "实体信息";

                map.EnType = EnType.Sys;
                map.AddTBString(SysEnAttr.Name, null, "实体名称", true, false, 0, 100, 60);
                map.AddTBStringPK(SysEnAttr.EnsEnsName, "EnsName", null, "实体类", true, true, 0, 90, 10);
                map.AddTBString(SysEnAttr.EnEnsName, "EnName", null, "实体名称", true, false, 0, 50, 20);
                map.AddDDLSysEnum(SysEnAttr.EnType, 0, "实体类型", true, false, "EnType");
                map.AddTBString(SysEnAttr.PTable, null, "数据源", true, false, 0, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 查询方法


        #endregion

    }
	
	/// <summary>
	/// 实体集合
	/// </summary>
	public class SysEns : EntitiesEnsName
	{		
		#region 构造
		public SysEns(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SysEn();
			}

		}
		#endregion

		#region 查询方法
		/// <summary>
		/// 按照实体的类型查询。
		/// </summary>
		/// <param name="type">实体的类型</param>
		/// <returns>返回查询的个数</returns>
		public int Retrieve(EnType type)
		{
			
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(SysEnAttr.EnType,(int)type);
			return qo.DoQuery();
		}
		#endregion
		
	}
}
