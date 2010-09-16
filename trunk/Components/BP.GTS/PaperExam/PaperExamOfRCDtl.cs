using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 阅读题考试attr
	/// </summary>
	public class PaperExamOfRCDtlAttr:EntityOIDAttr
	{
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// 学生
		/// </summary>
		public const string FK_Emp="FK_Emp";
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
	public class PaperExamOfRCDtl :Entity
	{
		#region attrs
		/// <summary>
		/// 得分
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfRCDtlAttr.Cent);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.Cent,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_RCDtl
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfRCDtlAttr.FK_RCDtl);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_RCDtl,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.Val,value);
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
				
				Map map = new Map("GTS_PaperExamOfRCDtl");
				map.EnDesc="试卷问答题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfRCDtlAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfRCDtlAttr.FK_Paper,null,"考卷",true,true,0,50,20);
				map.AddTBIntPK( PaperExamOfRCDtlAttr.FK_RCDtl,1,"阅读问答题",true,false);

				map.AddTBString(PaperExamOfRCDtlAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(PaperExamOfRCDtlAttr.Cent,1,"得分",true,true);
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
		public PaperExamOfRCDtl()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfRCDtl(string paper,string empid, int fk_rcdtl )
		{
			this.FK_RCDtl =fk_rcdtl;
			this.FK_Emp=empid;
			this.FK_Paper=paper;

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
	public class PaperExamOfRCDtls :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfRCDtls
		/// </summary>
		public PaperExamOfRCDtls(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfRCDtls(string fk_paper,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfRCDtlAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(PaperExamOfRCDtlAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfRCDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfRCDtl();
			}
		}
	}
}
