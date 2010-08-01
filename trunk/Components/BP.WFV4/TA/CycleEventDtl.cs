using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.TA
{
	/// <summary>
	/// 定期事件属性
	/// </summary>
    public class CycleEventDtlAttr : EntityOIDAttr
    {
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 开始循环日期
        /// </summary>
        public const string RefDate = "RefDate";
        public const string FK_CycleEvent = "FK_CycleEvent";
        
    }
	/// <summary>
	/// 定期事件
	/// </summary> 
    public class CycleEventDtl : EntityMyPK
    {
        #region  属性
        public int FK_CycleEvent
        {
            get
            {
                return this.GetValIntByKey(CycleEventDtlAttr.FK_CycleEvent);
            }
            set
            {
                SetValByKey(CycleEventDtlAttr.FK_CycleEvent, value);
            }
        }
        public string RefDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventDtlAttr.RefDate);
            }
            set
            {
                SetValByKey(CycleEventDtlAttr.RefDate, value);
            }
        }
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
        /// 定期事件
        /// </summary>
        public CycleEventDtl()
        {
        }
        /// <summary>
        /// 定期事件
        /// </summary>
        /// <param name="_No">No</param>
        public CycleEventDtl(string oid)
            : base(oid)
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

                Map map = new Map("TA_CycleEventDtl");
                map.EnDesc = "定期事件处理";
                map.AddTBIntPKOID();
                map.Icon = "./Images/CycleEventDtl_s.ico";

                map.AddMyPK();

                map.AddTBInt(CycleEventDtlAttr.FK_CycleEvent, 0, "FK_CycleEvent", false, false);
                map.AddTBDate(CycleEventDtlAttr.RefDate, "发生日期", false, false);

                //map.AddTBString(CycleEventAttr.ToEmps, null, "参与人", true, false, 0, 1000, 15);
                //map.AddTBString(CycleEventAttr.ToEmpNames, null, "参与人名称", true, false, 0, 1000, 15);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 定期事件s
	/// </summary> 
	public class CycleEventDtls: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new CycleEventDtl();
			}
		}
		public CycleEventDtls()
		{

		}
		
		/// <summary>
		/// 集合
		/// </summary>
		/// <param name="emp">人员</param>
		/// <param name="RefDate">开始日期</param>
		public CycleEventDtls(int fk_cycle)
		{
			QueryObject qo = new QueryObject(this);
            qo.AddWhere(CycleEventDtlAttr.FK_CycleEvent, fk_cycle);
            qo.addOrderBy(CycleEventDtlAttr.RefDate);
			qo.DoQuery();
		}
	}
}
 