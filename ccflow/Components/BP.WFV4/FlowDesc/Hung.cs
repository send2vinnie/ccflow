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
    public class HungAttr:EntityMyPKAttr
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
        public const string HungDays = "HungDays";
        /// <summary>
        /// 操作日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 发送日期(工作处理日期)
        /// </summary>
        public const string SendDT = "SendDT";
        /// <summary>
        /// 节点ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 接受人
        /// </summary>
        public const string Accepter = "Accepter";
       
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
    public class Hung : EntityMyPK
    {
        #region 属性
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(HungAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(HungAttr.FK_Node, value);
            }
        }
         
        /// <summary>
        /// 挂起标题
        /// </summary>
        public string Title
        {
            get
            {
                string s= this.GetValStringByKey(HungAttr.Title);
                if (string.IsNullOrEmpty(s))
                    s = "来自@Rec的挂起信息.";
                return s;
            }
            set
            {
                this.SetValByKey(HungAttr.Title, value);
            }
        }
        /// <summary>
        /// 挂起原因
        /// </summary>
        public string Note
        {
            get
            {
                string s = this.GetValStringByKey(HungAttr.Note);
                if (string.IsNullOrEmpty(s))
                    s = "来自@Rec的挂起信息.";
                return s;
            }
            set
            {
                this.SetValByKey(HungAttr.Note, value);
            }
        }
        
        #endregion

        #region 构造函数
        /// <summary>
        /// 挂起
        /// </summary>
        public Hung()
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

                Map map = new Map("WF_Hung");
                map.EnDesc = "挂起";
                map.EnType = EnType.Admin;

                map.AddMyPK();

                map.AddTBInt(HungAttr.FK_Node, 0, "节点ID", true, true);
                map.AddTBInt(HungAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(HungAttr.HungDays, 0, "挂起天数", true, true);

                map.AddTBDateTime(HungAttr.SendDT, null, "发送时间", true, false);

                map.AddTBString(HungAttr.Accepter, null, "接受人", true, false, 0, 500, 10, true);
                map.AddTBString(HungAttr.Title, null, "挂起标题", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(HungAttr.Note, null, "挂起原因(标题与内容支持变量)", true, false, true);

                map.AddTBString(HungAttr.Rec, null, "挂起人", true, false, 0, 50, 10, true);
                map.AddTBDateTime(HungAttr.RDT, null, "挂起时间", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
	public class Hungs: EntitiesMyPK
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Hung();
			}
		}
		/// <summary>
        /// 挂起
		/// </summary>
		public Hungs(){} 		 
		#endregion
	}
}
