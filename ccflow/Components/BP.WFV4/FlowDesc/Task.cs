using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// 任务 属性
	/// </summary>
    public class TaskAttr : EntityMyPKAttr
    {
        #region 基本属性
        /// <summary>
        /// 发起人
        /// </summary>
        public const string Starter = "Starter";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 参数
        /// </summary>
        public const string Paras = "Paras";
        /// <summary>
        /// 任务状态
        /// </summary>
        public const string TaskSta = "TaskSta";
        #endregion
    }
	/// <summary>
	/// 任务
	/// </summary>
    public class Task : EntityMyPK
    {
        #region 属性
        /// <summary>
        /// 参数
        /// </summary>
        public string Paras
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Paras);
            }
            set
            {
                this.SetValByKey(TaskAttr.Paras, value);
            }
        }
        /// <summary>
        /// 发起人
        /// </summary>
        public string Starter
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Starter);
            }
            set
            {
                this.SetValByKey(TaskAttr.Starter, value);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TaskAttr.FK_Flow, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// Task
        /// </summary>
        public Task()
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
                Map map = new Map("WF_Task");
                map.EnDesc = "任务";
                map.EnType = EnType.Admin;
                map.AddMyPK();
                map.AddTBString(TaskAttr.FK_Flow, null, "流程编号", true, false, 0, 200, 10);
                map.AddTBString(TaskAttr.Starter, null, "发起人", true, false, 0, 200, 10);
                map.AddTBString(TaskAttr.Paras, null, "参数", true, false, 0, 4000, 10);
                map.AddTBInt(TaskAttr.TaskSta, 0, "TaskSta", true, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 任务
	/// </summary>
	public class Tasks: Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Task();
			}
		}
		/// <summary>
        /// 任务
		/// </summary>
		public Tasks(){} 		 
		#endregion
	}
}
