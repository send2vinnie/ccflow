using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
    /// <summary>
    /// 项目状态
    /// </summary>
    public enum PrjState
    {
        /// <summary>
        /// 新建
        /// </summary>
        Init,
        /// <summary>
        /// 运行
        /// </summary>
        Runing,
        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }
	/// <summary>
	/// 项目属性列表
	/// </summary>
    public class PrjAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 单位
        /// </summary>
        public const string DW = "DW";
        /// <summary>
        /// 地址
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 造价
        /// </summary>
        public const string ZJ = "ZJ";
        /// <summary>
        /// 开发费用
        /// </summary>
        public const string KFFY = "KFFY";
        /// <summary>
        /// 申报日期
        /// </summary>
        public const string SBRQ = "SBRQ";
        /// <summary>
        /// 是否特办
        /// </summary>
        public const string IsTB = "IsTB";
        /// <summary>
        /// 项目状态
        /// </summary>
        public const string PrjState = "PrjState";
    }
	/// <summary>
	/// 项目
	/// </summary>
    public class Prj : EntityNoName
    {
        #region 基本属性
        /// <summary>
        /// 项目状态
        /// </summary>
        public PrjState HisPrjState
        {
            get
            {
                return (PrjState)this.GetValIntByKey(PrjAttr.PrjState);
            }
            set
            {
                this.SetValByKey(PrjAttr.PrjState, (int)value);
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.Addr);
            }
            set
            {
                this.SetValByKey(PrjAttr.Addr, value);
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string DW
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.DW);
            }
            set
            {
                this.SetValByKey(PrjAttr.DW, value);
            }
        }
        /// <summary>
        /// 造价
        /// </summary>
        public float ZJ
        {
            get
            {
                return this.GetValFloatByKey(PrjAttr.ZJ);
            }
            set
            {
                this.SetValByKey(PrjAttr.ZJ, value);
            }
        }
        /// <summary>
        /// 开发费用
        /// </summary>
        public float KFFY
        {
            get
            {
                return this.GetValFloatByKey(PrjAttr.KFFY);
            }
            set
            {
                this.SetValByKey(PrjAttr.KFFY, value);
            }
        }
        /// <summary>
        /// 申报日期
        /// </summary>
        public string SBRQ
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.SBRQ);
            }
            set
            {
                this.SetValByKey(PrjAttr.SBRQ, value);
            }
        }
        /// <summary>
        /// 是否特办
        /// </summary>
        public bool IsTB
        {
            get
            {
                return this.GetValBooleanByKey(PrjAttr.IsTB);
            }
            set
            {
                this.SetValByKey(PrjAttr.IsTB, value);
            }
        }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int PrjState
        {
            get
            {
                return this.GetValIntByKey(PrjAttr.PrjState);
            }
            set
            {
                this.SetValByKey(PrjAttr.PrjState, value);
            }
        }
        /// <summary>
        /// 是否特办标签
        /// </summary>
        public string PrjStateText
        {
            get
            {
                return this.GetValRefTextByKey(PrjAttr.PrjState);
            }
        }
        #endregion

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = true;
                uac.IsInsert = true;
                uac.IsUpdate = true;
                uac.IsView = true;
                return uac;
            }
        }
        /// <summary>
        /// 项目
        /// </summary>
        public Prj() { }
        /// <summary>
        /// strubg
        /// </summary>
        public Prj(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        public Prj(int no)
        {
            this.No = no.ToString();
            this.Retrieve();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Prj_Prj");
                map.EnDesc = "项目";

                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "4";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(PrjAttr.No, null, "编号", true, true, 4, 4, 4);
                map.AddTBString(PrjAttr.Name, null, "名称", true, false, 0, 60, 500);
               
                map.AddDDLSysEnum(PrjAttr.PrjState, 0, "项目状态", true, true, PrjAttr.PrjState,
                    "@0=新建@1=运行中@2=备案");

                map.AddTBString(PrjAttr.DW, null, "单位", true, false, 0, 60, 500);
                map.AddTBString(PrjAttr.Addr, null, "地址", true, false, 0, 60, 500);

                map.AttrsOfOneVSM.Add(new EmpPrjs(), new Emps(), EmpPrjAttr.FK_Prj, EmpPrjAttr.FK_Emp,
                    DeptAttr.Name, DeptAttr.No, "成员");

                  

                map.AddSearchAttr(PrjAttr.PrjState);

                RefMethod rm = new RefMethod();
                rm.Title = "成员岗位";
                rm.ClassMethodName = this.ToString() + ".DoEmpPrjStations";
                rm.IsCanBatch = true;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "资料树";
                rm.ClassMethodName = this.ToString() + ".DoDocTree";
                rm.IsCanBatch = true;
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "执行罚款";
                //rm.ClassMethodName = this.ToString() + ".DoFK";
                //rm.HisAttrs.AddTBDecimal("JE", 100, "输入金额", true, false);
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        public string DoEmpPrjStations()
        {
            PubClass.WinOpen("../Comm/PanelEns.aspx?EnsName=BP.PRJ.EmpPrjExts&FK_Prj=" + this.No, 800, 500);
            return null;
        }
      
        public string DoDocTree()
        {
            PubClass.WinOpen("../ExpandingApplication/PRJ/DocTree.aspx?No=" + this.No, 500, 500);
            return null;
        }
        protected override bool beforeInsert()
        {
            this.No = this.GenerNewNo;
            this.SBRQ = DataType.CurrentData;
            //this.PrjState = 1;
            //if (this.DW.Length < 1)
            //    throw new Exception("@dsdafasdfadsfsd");
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// 项目s
	/// </summary>
	public class Prjs : EntitiesNoName
	{	
		#region 构造方法
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Prj();
			}
		}
		/// <summary>
		/// 项目s 
		/// </summary>
		public Prjs(){}
		#endregion
	}
	
}
