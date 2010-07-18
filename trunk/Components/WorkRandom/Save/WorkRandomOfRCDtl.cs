using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 阅读题考试attr
	/// </summary>
	public class WorkRandomOfRCDtlAttr:WorkRandomThemeBaseAttr
	{
		/// <summary>
		/// 阅读题
		/// </summary>
		public const string FK_RCDtl="FK_RCDtl";
		/// <summary>
		/// 值
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// 得分
		/// </summary>
		public const string Cent="Cent";
	}
	/// <summary>
	/// 阅读题考试
	/// </summary>
	public class WorkRandomOfRCDtl :WorkRandomThemeBase
	{
		#region attrs
		/// <summary>
		/// 得分
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfRCDtlAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_RCDtl
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfRCDtlAttr.FK_RCDtl);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.FK_RCDtl,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfRCDtlAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.Val,value);
			}
		}
		#endregion

		#region 实现基本的方法
		/// <summary>
		/// uac
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}

		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("GTS_WorkRandomOfRCDtl");
				map.EnDesc="试卷问答题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfRCDtlAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfRCDtlAttr.FK_WorkRandom,null,"随机作业",true,true,0,50,20);
				map.AddTBIntPK( WorkRandomOfRCDtlAttr.FK_RCDtl,0,"阅读问答题",true,false);

				map.AddTBString(WorkRandomOfRCDtlAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkRandomOfRCDtlAttr.Cent,0,"得分",true,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfEssayQuestionOne+this.CentOfEssayQuestionM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 阅读题考试
		/// </summary> 
		public WorkRandomOfRCDtl()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfRCDtl(string work,string empid, int fk_rcdtl )
		{
			this.FK_RCDtl =fk_rcdtl;
			this.FK_Emp=empid;
			this.FK_WorkRandom=work;

			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		#endregion 

		#region 逻辑处理
		
		#endregion
	}
	/// <summary>
	///  阅读题考试
	/// </summary>
	public class WorkRandomOfRCDtls :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfRCDtls
		/// </summary>
		public WorkRandomOfRCDtls(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfRCDtls(string FK_WorkRandom,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfRCDtlAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfRCDtlAttr.FK_WorkRandom, FK_WorkRandom);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfRCDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfRCDtl();
			}
		}
	}
}
