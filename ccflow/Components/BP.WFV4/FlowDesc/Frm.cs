using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// Frm属性
	/// </summary>
    public class FrmAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 节点
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";
    }
	/// <summary>
	/// Frm
	/// </summary>
	public class Frm :EntityNoName
	{
		#region 基本属性
		/// <summary>
		///节点
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(FrmAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(FrmAttr.FK_Node,value);
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// Frm
		/// </summary>
		public Frm(){}
		/// <summary>
		/// 重写基类方法
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Frm");
                map.EnDesc = "Frm";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(FrmAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FrmAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddTBInt(FrmAttr.FK_Node, 0, null, true, false);
                map.AddTBInt(FrmAttr.Idx, 0, null, true, false);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	/// <summary>
	/// Frm
	/// </summary>
    public class Frms : EntitiesMyPK
    {
        /// <summary>
        /// Frm
        /// </summary>
        public Frms() { }
        /// <summary>
        /// Frm
        /// </summary>
        /// <param name="fk_node"></param>
        public Frms(int fk_node)
        {
            this.Retrieve(FrmAttr.FK_Node, fk_node, FrmAttr.Idx);
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Frm();
            }
        }
    }
}
