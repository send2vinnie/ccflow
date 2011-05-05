using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
 	/// <summary>
	/// 工作模板状态
	/// </summary>
	public enum FK_WTT
	{
		/// <summary>
		/// 申请类
		/// </summary>
		Apply,
		/// <summary>
		/// 报告类
		/// </summary>
		Report,
		/// <summary>
		/// 其它类
		/// </summary>
		Etc
	}
	/// <summary>
	/// 工作模板属性
	/// </summary>
    public class WorkTemplateAttr : WorkDtlBaseAttr
    {
        /// <summary>
        /// 退回原因
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 接受人意见
        /// </summary>
        public const string PRI = "PRI";
        public const string IsRe = "IsRe";
        public const string SpanDays = "SpanDays";
        /// <summary>
        /// 工作模板状态
        /// </summary>
        public const string FK_WTT = "FK_WTT";

        public const string Doc = "Doc";
        public const string CheckWay = "CheckWay";
    }
	/// <summary>
	/// 工作模板
	/// </summary> 
	public class WorkTemplate : EntityNoName
	{
		#region 基本属性

        public CheckWay HisCheckWay
        {
            get
            {
                return (CheckWay)this.GetValIntByKey(WorkTemplateAttr.CheckWay);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.CheckWay, value);
            }
        }

        public string Doc
        {
            get
            {
                return this.GetValStrByKey(WorkTemplateAttr.Doc);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.Doc, value);
            }
        }

        public bool IsRe
        {
            get
            {
                return this.GetValBooleanByKey(WorkTemplateAttr.IsRe);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.IsRe, value);
            }
        }


        public string FK_WTT
        {
            get
            {
                return this.GetValStrByKey(WorkTemplateAttr.FK_WTT);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.FK_WTT, value);
            }
        }
     
     
		public string FK_WTTText
		{
			get
			{
				return this.GetValRefTextByKey(WorkTemplateAttr.FK_WTT);
			}
		}
		#endregion
 
		#region 构造函数
		/// <summary>
		/// 工作模板
		/// </summary>
		public WorkTemplate()
		{
		  
		}
		/// <summary>
		/// 工作模板
		/// </summary>
		/// <param name="_No">No</param>
		public WorkTemplate(string oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_WorkTemplate");
				map.EnDesc="工作模板";

                map.AddTBStringPK("No", null, "编号", true, true, 3, 3, 3);
                map.AddTBString(WorkTemplateAttr.Name, null, "标题", true, false, 0, 500, 15);


                map.AddDDLEntities(WorkTemplateAttr.FK_WTT, "99", "类型", new WorkTemplateTypes(), true);

              //  map.AddDDLSysEnum(WorkTemplateAttr.FK_WTT, 2, "类型", true, true, "FK_WTT", "@0=申请类@1=通报类@2=咨询类@3=回报类@4=其它类");
                map.AddDDLSysEnum(WorkTemplateAttr.PRI, 0, "默认的PRI", true, true);
                map.AddBoolean(WorkTemplateAttr.IsRe, false, "默认是否需要回复", true, true);
                map.AddTBInt(WorkTemplateAttr.SpanDays, 0, "默认完成天数",true, false);
				map.AddTBStringDoc();
               // map.AddTBStringNote();
                map.AddDDLSysEnum(WorkAttr.CheckWay, 0, "默认考核类型", true, true);

                RefMethod rm = new RefMethod();
                rm.Title = "读取Txt模板";
                rm.ClassMethodName = this.ToString() + ".DoReadTxt";
                rm.Warning = "您确定要执行吗？";

                map.AddRefMethod(rm);

                map.AddSearchAttr(WorkTemplateAttr.FK_WTT);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

        public string DoReadTxt()
        {
            string dir = "D:\\WebApp\\OA\\Data\\TemplateWork\\";

            string[] fls = System.IO.Directory.GetFiles(dir);
            foreach (string f in fls)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(f);
                WorkTemplate en = new WorkTemplate();
                en.Name = fi.Name.Substring(1);
                en.Name = en.Name.Replace(".txt", "");

                if (en.Retrieve(WorkTemplateAttr.Name, fi.Name) == 0)
                {
                    en.No = en.GenerNewNo;
                    en.Insert();
                }

                en.Doc = DataType.ReadTextFile(f);
                en.Update();
                //string pk = 
            }

            return "执行成功。";
        }
	}
	/// <summary>
	/// 工作模板s
	/// </summary> 
	public class WorkTemplates: EntitiesNoName
	{
		 
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkTemplate();
			}
		}
		/// <summary>
		/// WorkTemplates
		/// </summary>
		public WorkTemplates()
		{

		}
		public WorkTemplates(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(WorkTemplateAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(WorkTemplateAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(WorkTemplateAttr.PRI, " LIKE ", ny+"%");
			qo.DoQuery();
		}
		
	}
}
 