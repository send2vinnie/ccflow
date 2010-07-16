using System;
using System.Data;
using System.IO;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// 网页内容
	/// </summary>
    public class PageMailAttr : EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 发送时间
        /// </summary>
        public const string SendTimes = "SendTimes";
        /// <summary>
        /// SDT
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// 是否效
        /// </summary>
        public const string IsEnable = "IsEnable";
        #endregion
    }
	/// <summary>
	/// 网页内容
	/// </summary>
    public class PageMail : EntityNoName
	{	
		#region 基本属性
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(PageMailAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(PageMailAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// 发送次数
        /// </summary>
        public int SendTimes
        {
            get
            {
                return this.GetValIntByKey(PageMailAttr.SendTimes);
            }
            set
            {
                this.SetValByKey(PageMailAttr.SendTimes, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageMailAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageMailAttr.RDT, value);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(PageMailAttr.SDT);
            }
            set
            {
                this.SetValByKey(PageMailAttr.SDT, value);
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
		/// 网页内容
		/// </summary>		
		public PageMail()
		{
		}
		/// <summary>
		/// 网页内容
		/// </summary>
        /// <param name="no"></param>
        public PageMail(string no)
            : base(no)
        {
        }
		/// <summary>
		/// PageMailMap
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

                map.PhysicsTable = "RB_PageMail";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "EPageMail";
                map.EnType = EnType.App;

                #endregion

                #region 类属性

                map.AddTBStringPK(PageMailAttr.No, null, "EPageMail", true, true, 1, 100, 4);
                map.AddTBString(PageMailAttr.Name, null, "用户", true, true, 0, 100, 30);

                map.AddTBDate(PageMailAttr.RDT, "收录日期", true, true);

                map.AddTBDate(PageMailAttr.SDT, "最近发送日期", true, true);
                map.AddTBInt(PageMailAttr.SendTimes, 0, "发送次数", true, true);
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
	/// 网页内容
	/// </summary>
    public class PageMails : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PageMail();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 网页内容
		/// </summary>
		public PageMails(){}
		 
		
		#endregion

	 
	}
	
}
