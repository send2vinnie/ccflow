using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.TA
{
	/// <summary>
	/// 日志属性
	/// </summary>
	public class TaLogAttr:EntityOIDAttr
	{
		/// <summary>
		/// 标题
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// 备注
		/// </summary>
		public const string Note="Note"; 
		/// <summary>
		/// 日期
		/// </summary>
		public const string LogDate="LogDate"; 
		/// <summary>
		/// 时间
		/// </summary>
		public const string LogTime="LogTime";
		/// <summary>
		/// 持续时间
		/// </summary>
		public const string FK_TimeScope="FK_TimeScope";
		/// <summary>
		/// 年
		/// </summary>
		public const string FK_Year="FK_Year";
		/// <summary>
		/// 月
		/// </summary>
		public const string FK_Month="FK_Month";
		/// <summary>
		/// 共享类型 私有,公开.
		/// </summary>
		public const string SharingType="SharingType"; 
		/// <summary>
		/// 记录人
		/// </summary>
		public const string Recorder="Recorder"; 	
	}
	/// <summary>
	/// 日志
	/// </summary> 
    public class TaLog : EntityOID
    {
        #region  属性
        public string MyLogOpStr
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 /><a href=\"javascript:OpenLog('" + this.OID + "')\" >" + this.Title + "</a>" + BP.PubClass.FilesViewStr(this.ToString(), this.OID);
            }
        }
        public DateTime LogDatetime
        {
            get
            {
                return DataType.ParseSysDateTime2DateTime(this.LogDate + " " + this.LogTime);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Title);
            }
            set
            {
                SetValByKey(TaLogAttr.Title, value);
            }
        }
        public string TitleHtml
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 />" + this.GetValStringByKey(TaLogAttr.Title);
            }
            set
            {
                SetValByKey(TaLogAttr.Title, value);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Note);
            }
            set
            {
                SetValByKey(TaLogAttr.Note, value);
            }
        }
        /// <summary>
        /// 发生日期
        /// </summary>
        public string LogDate
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.LogDate);
            }
            set
            {
                SetValByKey(TaLogAttr.LogDate, value);
            }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public string LogTime
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.LogTime);
            }
            set
            {
                SetValByKey(TaLogAttr.LogTime, value);
            }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public string FK_TimeScope
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.FK_TimeScope);
            }
            set
            {
                SetValByKey(TaLogAttr.FK_TimeScope, value);
            }
        }
        public string FK_TimeScopeText
        {
            get
            {
                return this.GetValRefTextByKey(TaLogAttr.FK_TimeScope);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Recorder
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Recorder);
            }
            set
            {
                SetValByKey(TaLogAttr.Recorder, value);
            }
        }
        /// <summary>
        /// 共享类型 0，私有, 1公开。
        /// </summary>
        public int SharingType
        {
            get
            {
                return this.GetValIntByKey(TaLogAttr.SharingType);
            }
            set
            {
                SetValByKey(TaLogAttr.SharingType, value);
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
                //return base.HisUAC;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        public TaLog()
        {

        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="_No">No</param>
        public TaLog(int oid)
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

                Map map = new Map("TA_Log");
                map.EnDesc = "日志";
                //map.Icon="./images/log_s.ico";
                map.Icon = "../TA/Images/log_s.gif";

                map.AddTBIntPKOID();

                map.AddTBStringDoc(TaLogAttr.Title, null, "内容", true, false);
                map.AddTBStringDoc(TaLogAttr.Note, null, "备注", true, false);

                map.AddTBDate(TaLogAttr.LogDate, DataType.CurrentData, "日期", true, false);
                map.AddTBString(TaLogAttr.LogTime, DataType.CurrentTime, "时间", true, false, 0, 500, 60);
                map.AddDDLEntities(TaLogAttr.FK_TimeScope, "1", "持续时间", new TimeScopes(), true);
                map.AddDDLSysEnum(TaLogAttr.SharingType, 0, "共享类型", true, true, TaskAttr.SharingType, "@0=共享@1=私有");
                map.AddDDLEntities(TaLogAttr.FK_Year, DataType.CurrentYear, "年", new BP.Pub.NDs(), false);
                map.AddDDLEntities(TaLogAttr.FK_Month, DataType.CurrentMonth, "月", new BP.Pub.YFs(), false);
                map.AddTBString(TaLogAttr.Recorder, null, "记录人", false, false, 0, 20, 0);

                map.AddSearchAttr(TaLogAttr.SharingType);
                map.AttrsOfSearch.AddHidden(TaLogAttr.Recorder, "=", Web.WebUser.No);
                map.AddSearchAttr(TaLogAttr.FK_Year);
                map.AddSearchAttr(TaLogAttr.FK_Month);
                //map.AttrsOfSearch.AddFromTo("日期从",TaLogAttr.LogDate,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 日志s
	/// </summary> 
	public class TaLogs: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new TaLog();
			}
		}
		public TaLogs()
		{

		}
		
		/// <summary>
		/// 集合
		/// </summary>
		/// <param name="emp">人员</param>
		/// <param name="ny">年月</param>
		public TaLogs(string emp,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaLogAttr.LogDate, " like ", ny+"%");
			qo.addAnd();
			qo.AddWhere(TaLogAttr.Recorder,emp);
			if (emp==Web.WebUser.No)
			{
				
			}
			else
			{
				qo.addAnd();
				qo.AddWhere(TaLogAttr.SharingType,1);
			}
			qo.addOrderBy(TaLogAttr.LogDate,TaLogAttr.LogTime);
			qo.DoQuery();
		}
	}
}
 