
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// 运行记录 属性
	/// </summary>
    public class RunRecordAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 当前节点
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// workid
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 主机
        /// </summary>
        public const string HostIP = "HostIP";
        /// <summary>
        /// 主机
        /// </summary>
        public const string ClientIP = "ClientIP";
        /// <summary>
        /// 工作人员（候选)
        /// </summary>
        public const string FromSaveTime = "FromSaveTime";
        /// <summary>
        /// 工作人员个数（候选)
        /// </summary>
        public const string FromAfterNoteTime = "FromAfterNoteTime";
        /// <summary>
        /// day
        /// </summary>
        public const string FK_Day = "FK_Day";
        /// <summary>
        /// 年月
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// 工作人员（候选)
        /// </summary>
        public const string RDT = "RDT";

        #endregion
    }
	/// <summary>
	/// 运行记录
	/// </summary>
    public class RunRecord : Entity
    {
        #region 属性
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.RDT);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.RDT, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_NY, value);
            }
        }
        public string FK_Day
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_Day);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Day, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Emp, value);
            }
        }
        public string HostIP
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.HostIP);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.HostIP, value);
            }
        }
        public string ClientIP
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.ClientIP);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.ClientIP, value);
            }
        }
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(RunRecordAttr.WorkID);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.WorkID, value);
            }
        }

        public int FromSaveTime
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FromSaveTime);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FromSaveTime, value);
            }
        }
        public int FromAfterNoteTime
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FromAfterNoteTime);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FromAfterNoteTime, value);
            }
        }
        
        #endregion


        #region 构造函数
        /// <summary>
        /// RunRecord
        /// </summary>
        public RunRecord()
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
                Map map = new Map("WF_RunRecord");
                map.EnDesc = "运行记录";
                map.EnType = EnType.Admin;

                map.AddDDLEntitiesPK(RunRecordAttr.FK_Node, null, "节点", new NodeExts(), false);
                map.AddTBIntPK(RunRecordAttr.WorkID, 0, "WorkID", false, false);

                map.AddTBString(RunRecordAttr.HostIP, "", "HostIP", true, false, 0, 200, 10);
                map.AddTBString(RunRecordAttr.ClientIP, "", "ClientIP", true, false, 0, 200, 10);

                map.AddTBInt(RunRecordAttr.FromAfterNoteTime, 0, "发送", true, false);
                map.AddTBInt(RunRecordAttr.FromSaveTime, 0, "保存到发送", true, false);

                map.AddDDLEntities(RunRecordAttr.FK_Day, null, "日", new Pub.Days(), false);
                map.AddDDLEntities(RunRecordAttr.FK_NY, null, "月份", new Pub.YFs(), false);
                

                map.AddTBDateTime(RunRecordAttr.RDT, "RDT", true, false);
                map.AddTBIntMyNum();

                map.AddSearchAttr(RunRecordAttr.FK_NY);
                map.AddSearchAttr(RunRecordAttr.FK_Day);
                map.AddSearchAttr(RunRecordAttr.FK_Node);

               
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            DateTime now = DateTime.Now;
            this.FK_Day = now.ToString("dd");
            this.FK_NY = now.ToString("MM");
            this.RDT = now.ToString("yyyy-MM-dd hh:mm:ss");

            this.FK_Emp = Web.WebUser.No;
            return base.beforeUpdateInsertAction();
        }

    }
	/// <summary>
	/// 运行记录
	/// </summary>
	public class RunRecords: Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RunRecord();
			}
		}
		/// <summary>
		/// RunRecord
		/// </summary>
		public RunRecords(){} 		 
		#endregion
	}
	
}
