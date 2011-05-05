
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port; 
using BP.Port; 
using BP.En;
using BP.Web;

 
namespace BP.WF
{
	/// <summary>
	/// 调度日志
	/// </summary>
    public class DTSLogAttr
    {
        /// <summary>
        /// 人员
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        public const string MsgKey = "MsgKey";
        public const string Note = "Note";
        public const string MyPK = "MyPK";
    }
	/// <summary>
	/// 调度日志
	/// </summary>
    public class DTSLog : Entity
    {

        #region 基本属性
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.MyPK);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.MyPK, value);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// 相关信息
        /// </summary>
        public string MsgKey
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.MsgKey);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.MsgKey, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.Note);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.Note, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 调度日志
        /// </summary>
        public DTSLog()
        {
        }
        public DTSLog(string pk)
        {
            //this.MyPK=pk;
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
                Map map = new Map("ZF_DTSLog");
                map.EnDesc = "调度日志";
                map.Icon = "../ZF/Images/Dot_s.gif";

                map.AddTBStringPK(DTSLogAttr.MyPK, null, "日期", true, true, 0, 500, 10);
                map.AddDDLEntities(DTSLogAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);
                map.AddTBString(DTSLogAttr.MsgKey, null, "消息", false, true, 0, 500, 10);
                map.AddTBString(DTSLogAttr.Note, null, "备注", false, true, 0, 500, 10);

                map.AddSearchAttr(DTSLogAttr.FK_Dept);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            this.MyPK = DateTime.Now.ToString("yy年MM月dd日HH时mm份ss秒");
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// 调度日志s
	/// </summary>
	public class DTSLogs : Entities
	{	
		#region 构造方法
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new DTSLog();
			}
		}
		/// <summary>
		/// 调度日志s 
		/// </summary>
		public DTSLogs(){}
		
		#endregion
	}
	
}
