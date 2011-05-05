using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace BP.Sys
{
    public class SysLogAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 关联的key
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// FK_Emp
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// Title
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 文件大小
        /// </summary>
        public const string FK_TypeName = "FK_TypeName";
        /// <summary>
        /// FK_Type
        /// </summary>
        public const string FK_Type = "FK_Type";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Doc = "Doc";
        public const string Time = "Time";
        public const string FK_NY = "FK_NY";
    }
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SysLog : BP.En.EntityMyPK
    {
        #region 实现基本属性

        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Dept, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Emp, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.Title);
            }
            set
            {
                this.SetValByKey(SysLogAttr.Title, value);
            }
        }
        public string FK_TypeName
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_TypeName);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_TypeName, value);
            }
        }
        public string FK_Type
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Type);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Type, value);
            }
        }

        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(SysLogAttr.RDT, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.Doc);
            }
            set
            {
                this.SetValByKey(SysLogAttr.Doc, value);
            }
        }
        #endregion

        #region 构造方法

        public SysLog() { }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="_OID"></param>
        public SysLog(string _OID)
            : base(_OID)
        {
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_Log");
                map.EnDesc = "系统日志";
                map.CodeStruct = "3";

                map.AddMyPK();

                map.AddTBString(SysLogAttr.RDT, null, "日期", true, false, 0, 50, 20);
                map.AddTBString(SysLogAttr.Time, null, "时间", true, false, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_NY, null, "月份", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Type, null, "类型", true, false, 0, 500, 20);
                map.AddTBString(SysLogAttr.FK_TypeName, null, "类型名称", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.Title, null, "标题", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.Doc, null, "详细信息", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Emp, null, "人员", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Dept, null, "部门", false, true, 0, 50, 20);

                //   map.AddTBString(SysLogAttr.Doc, null, "备注", true, false, 0, 200, 30);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// 写日志的方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="title">标题</param>
        /// <param name="doc">内容</param>
        public static void WriteLog(string type, string typeName, string title, string doc)
        {
            SysLog log = new SysLog();
            //log.MyPK = BP.DA.DBAccess.GenerOID();
        }
    }
	/// <summary>
	/// 系统日志 
	/// </summary>
	public class SysLogs :EntitiesMyPK
	{
        /// <summary>
        /// 系统日志
        /// </summary>
		public SysLogs(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysLog();
			}
		}
	}
}
