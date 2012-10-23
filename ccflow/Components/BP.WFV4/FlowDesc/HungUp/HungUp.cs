using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// 挂起 属性
	/// </summary>
    public class HungUpAttr:EntityMyPKAttr
    {
        #region 基本属性
        public const string Title = "Title";
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 执行人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 通知给
        /// </summary>
        public const string NoticeTo = "NoticeTo";
        /// <summary>
        /// 挂起原因
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 挂起天数
        /// </summary>
        public const string HungUpDays = "HungUpDays";
        /// <summary>
        /// 操作日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 发送日期(工作处理日期)
        /// </summary>
        public const string SendDT = "SendDT";
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
    public class HungUp : Entity
    {
        #region 属性
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No != "admin")
                {
                    uac.IsView = false;
                    return uac;
                }
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 挂起标题
        /// </summary>
        public string Title
        {
            get
            {
                string s= this.GetValStringByKey(HungUpAttr.Title);
                if (string.IsNullOrEmpty(s))
                    s = "来自@Rec的挂起信息.";
                return s;
            }
            set
            {
                this.SetValByKey(HungUpAttr.Title, value);
            }
        }
        /// <summary>
        /// 挂起原因
        /// </summary>
        public string Note
        {
            get
            {
                string s = this.GetValStringByKey(HungUpAttr.Note);
                if (string.IsNullOrEmpty(s))
                    s = "来自@Rec的挂起信息.";
                return s;
            }
            set
            {
                this.SetValByKey(HungUpAttr.Note, value);
            }
        }
        /// <summary>
        /// 挂起时间
        /// </summary>
        public string HungUpDays
        {
            get
            {
                string sql= this.GetValStringByKey(HungUpAttr.HungUpDays);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("‘", "'");
                sql = sql.Replace("’", "'");
                sql = sql.Replace("''", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(HungUpAttr.HungUpDays, value);
            }
        }
        /// <summary>
        /// 控制方式
        /// </summary>
        public CtrlWay HisCtrlWay
        {
            get
            {
                return (CtrlWay)this.GetValIntByKey(HungUpAttr.NoticeTo);
            }
            set
            {
                this.SetValByKey(HungUpAttr.NoticeTo, (int)value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// HungUp
        /// </summary>
        public HungUp()
        {
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
                Map map = new Map("WF_Node");
                map.EnDesc = "挂起规则";
                map.EnType = EnType.Admin;
                map.AddTBString(NodeAttr.Name, null, "节点名称", true, true, 0, 100, 10, true);
                map.AddTBIntPK(NodeAttr.NodeID, 0,"节点ID", true, true);

                map.AddDDLSysEnum(HungUpAttr.NoticeTo, 0, "控制方式",true, true,"CtrlWay");
                map.AddTBString(HungUpAttr.HungUpDays, null, "SQL表达式", true, false, 0, 500, 10, true);
                map.AddTBString(HungUpAttr.Title, null, "挂起标题", true, false, 0, 500, 10,true);
                map.AddTBStringDoc(HungUpAttr.Note, null, "挂起原因(标题与内容支持变量)", true, false,true);

                map.AddSearchAttr(HungUpAttr.NoticeTo);

              
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
	public class HungUps: Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new HungUp();
			}
		}
		/// <summary>
        /// 挂起
		/// </summary>
		public HungUps(){} 		 
		#endregion
	}
}
