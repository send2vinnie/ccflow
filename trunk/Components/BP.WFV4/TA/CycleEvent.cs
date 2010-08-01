using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.TA
{
	/// <summary>
	/// 定期事件属性
	/// </summary>
    public class CycleEventAttr : EntityOIDAttr
    {
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 参与人
        /// </summary>
        public const string ToEmps = "ToEmps";
        /// <summary>
        /// ToEmpNames
        /// </summary>
        public const string ToEmpNames = "ToEmpNames";
        /// <summary>
        /// 开始循环日期
        /// </summary>
        public const string StartDate = "StartDate";
        /// <summary>
        /// 结束循环日期
        /// </summary>
        public const string EndDate = "EndDate";
        /// <summary>
        /// 是否有结束日期
        /// </summary>
        public const string NoEndDate = "NoEndDate";
        /// <summary>
        /// 事件开始时间
        /// </summary>
        public const string StartTime = "StartTime";
        /// <summary>
        /// 持续时间
        /// </summary>
        public const string FK_TimeScope = "FK_TimeScope";
        /// <summary>
        /// 拥有人
        /// </summary>
        public const string Recorder = "Recorder";
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";

        /// <summary>
        /// 天s
        /// </summary>
        public const string Days = "Days";
        /// <summary>
        /// 周s
        /// </summary>
        public const string Weeks = "Weeks";
        /// <summary>
        /// 月份s
        /// </summary>
        public const string Monthes = "Monthes";
        /// <summary>
        /// 循环方式
        /// </summary>
        public const string CycleWay = "CycleWay";
    }
	/// <summary>
	/// 循环方式
	/// </summary>
    public enum CycleWay
    {
        /// <summary>
        /// 未设置
        /// </summary>
        UnSet,
        /// <summary>
        /// 按周
        /// </summary>
        ByWeek,
        /// <summary>
        /// 按月
        /// </summary>
        ByMonth,
        /// <summary>
        /// 按年
        /// </summary>
        ByYear
    }
	/// <summary>
	/// 定期事件
	/// </summary> 
    public class CycleEvent : EntityOID
    {
        /// <summary>
        /// 产生当期的日期
        /// </summary>
        public string GenerCurrRefDate
        {
            get
            {
                switch (this.MyCycleWay)
                {
                    case CycleWay.ByYear:
                        return DataType.CurrentYear + "-" + this.Monthes.ToString().PadLeft(2,'0') + "-" + this.Days.ToString().PadLeft(2,'0');
                    case CycleWay.ByMonth:
                        return DataType.CurrentYear + "-" + DataType.CurrentMonth + "-" + this.Days.ToString().PadLeft(2,'0');
                    case CycleWay.ByWeek:
                        DayOfWeek week = (DayOfWeek)this.Weeks;
                        DateTime dt = DateTime.Now;
                        while (true)
                        {
                            if (dt.DayOfWeek == week)
                                return dt.ToString("yyyy-MM-dd");
                            dt = dt.AddDays(1);
                        }
                    case CycleWay.UnSet:
                        return "unset";
                    default:
                        return "unset";
                }
            }
        }

        #region  属性
        //		public DateTime LogDatetime
        //		{
        //			get
        //			{
        //				return DataType.ParseSysDateTime2DateTime(this.LogDate+" "+this.LogTime);
        //			}
        //		}
        public string Recorder
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Recorder);
            }
            set
            {
                SetValByKey(CycleEventAttr.Recorder, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Title);
            }
            set
            {
                SetValByKey(CycleEventAttr.Title, value);
            }
        }
        public string TitleHtml
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 />" + this.StartTime + "<a href=\"javascript:WinOpen('CycleEventV.aspx?RefOID=" + this.OID + "')\"  >" + this.GetValStringByKey(CycleEventAttr.Title) + "</a>";
            }
        }
        /// <summary>
        /// 参与人
        /// </summary>
        public string ToEmps
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.ToEmps);
            }
            set
            {
                string strs = value+",";
                if (strs.Contains("," + BP.Web.WebUser.No + "," ) == false)
                    strs = strs + "," + Web.WebUser.No;

                strs = PubClass.CheckEmps(strs);
            
                SetValByKey(CycleEventAttr.ToEmps, strs);
                SetValByKey(CycleEventAttr.ToEmpNames, Web.WebUser.Tag);
            }
        }
        public string ToEmpNames
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.ToEmpNames);
            }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.StartDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.StartDate, value);
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.EndDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.EndDate, value);
            }
        }
        public DateTime EndDate_S
        {
            get
            {
                return DataType.ParseSysDate2DateTime(this.EndDate);
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.StartTime);
            }
            set
            {
                SetValByKey(CycleEventAttr.StartTime, value);
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string FK_TimeScope
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.FK_TimeScope);
            }
            set
            {
                SetValByKey(CycleEventAttr.FK_TimeScope, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Doc);
            }
            set
            {
                SetValByKey(CycleEventAttr.Doc, value);
            }
        }
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CycleEventAttr.Doc);
            }
        }
        /// <summary>
        /// 是否有结束日期
        /// </summary>
        public bool NoEndDate
        {
            get
            {
                return this.GetValBooleanByKey(CycleEventAttr.NoEndDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.NoEndDate, value);
            }
        }
        /// <summary>
        /// 循环方式
        /// </summary>
        public CycleWay MyCycleWay
        {
            get
            {
                return (CycleWay)this.GetValIntByKey(CycleEventAttr.CycleWay);
            }
            set
            {
                this.SetValByKey(CycleEventAttr.CycleWay, (int)value);
            }
        }
        public string MyCycleWayText
        {
            get
            {
                return this.GetValRefTextByKey(CycleEventAttr.CycleWay);
            }
        }
        public int Weeks
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Weeks);
            }
            set
            {
                SetValByKey(CycleEventAttr.Weeks, value);
            }
        }
        public int Days
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Days);
            }
            set
            {
                SetValByKey(CycleEventAttr.Days, value);
            }
        }
        public int Monthes
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Monthes);
            }
            set
            {
                SetValByKey(CycleEventAttr.Monthes, value);
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
        /// 定期事件
        /// </summary>
        public CycleEvent()
        {
        }
        /// <summary>
        /// 定期事件
        /// </summary>
        /// <param name="_No">No</param>
        public CycleEvent(int oid)
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

                Map map = new Map("TA_CycleEvent");
                map.EnDesc = "定期事件";
                map.Icon = "./Images/CycleEvent_s.ico";

                map.AddTBIntPKOID();
                map.AddTBString(CycleEventAttr.Title, null, "标题", true, false, 0, 500, 15);

                map.AddTBString(CycleEventAttr.ToEmps, null, "参与人", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.ToEmpNames, null, "参与人名称", true, false, 0, 1000, 15);


                map.AddTBDate(CycleEventAttr.StartDate, "开始日期", false, false);
                map.AddTBDate(CycleEventAttr.EndDate, "2200-12-31", "结束日期", false, false);

                map.AddBoolean(CycleEventAttr.NoEndDate, true, "无结束日期", false, false);

                map.AddTBString(CycleEventAttr.StartTime, "08:00", "事件开始时间", true, false, 5, 5, 5);
                map.AddDDLEntities(CycleEventAttr.FK_TimeScope, "01", "持续时间段", new TimeScopes(), false);

                map.AddDDLEntities(CycleEventAttr.Recorder, null, "建立人", new Emps(), false);

                map.AddTBString(CycleEventAttr.Doc, null, "备注", true, false, 0, 1000, 15);
                map.AddDDLSysEnum(CycleEventAttr.CycleWay, (int)CycleWay.ByMonth, "循环方式", true, true, "CycleWay", "@0=未设置@1=按周@2=按月@3=按年");

                map.AddTBString(CycleEventAttr.Days, "1", "天", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.Weeks, "1", "周", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.Monthes, "1", "月份", true, false, 0, 1000, 15);

                map.AddSearchAttr(CycleEventAttr.CycleWay);
                map.AttrsOfSearch.AddHidden(CycleEventAttr.Recorder, "=", Web.WebUser.No);
                map.AttrsOfSearch.AddFromTo("开始日期", CycleEventAttr.EndDate, DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat), DataType.CurrentData, 8);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 定期事件s
	/// </summary> 
	public class CycleEvents: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new CycleEvent();
			}
		}
		public CycleEvents()
		{

		}
		
		/// <summary>
		/// 集合
		/// </summary>
		/// <param name="emp">人员</param>
		/// <param name="startDate">开始日期</param>
		public CycleEvents(string emp,string ny)
		{
			QueryObject qo = new QueryObject(this);

			qo.addLeftBracket();
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%,"+emp+",%");
			qo.addOr();
			qo.AddWhere(CycleEventAttr.Recorder,emp);
			qo.addRightBracket();

			qo.addAnd();


			qo.addLeftBracket();
			qo.AddWhere(CycleEventAttr.StartDate, " <= " , ny+"%" );
			qo.addAnd();
			qo.AddWhere(CycleEventAttr.EndDate, " >= " , ny+"%" );
			qo.addRightBracket();

			qo.addOrderBy(CycleEventAttr.StartTime );
			qo.DoQuery();
		}
		/// <summary>
		/// 查询出我的全部定期事件
		/// </summary>
		/// <param name="emp"></param>
		public CycleEvents(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%,"+emp+",%");

			qo.addOrderBy(CycleEventAttr.StartTime );

			qo.DoQuery();
		}
		public int re()
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%"+Web.WebUser.No+"%");
			return qo.DoQuery();
		}
	}
}
 