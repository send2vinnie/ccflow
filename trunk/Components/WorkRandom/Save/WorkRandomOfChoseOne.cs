using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 选择题考试attr
	/// </summary>
	public class WorkRandomOfChoseOneAttr:WorkRandomThemeBaseAttr
	{
		/// <summary>
		/// 选择题
		/// </summary>
		public const string FK_Chose="FK_Chose";
		/// <summary>
		/// 值
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// 答案
		/// </summary>
		public const string Answer="Answer";
	}
	/// <summary>
	/// 选择题考试
	/// </summary>
	public class WorkRandomOfChoseOne :WorkRandomThemeBase
	{
		/// <summary>
		/// 是否正确
		/// </summary>
		public bool IsRight
		{
			get
			{
				if (this.Val==this.Answer)
					return true;
				else
					return false;
			}
		}
		public string Answer
		{
			get
			{
				 
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.Answer,value);
			}
		}

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
				
				Map map = new Map("GTS_WorkRandomOfChoseOne");
				map.EnDesc="作业单项选择题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(WorkRandomOfChoseOneAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfChoseOneAttr.FK_WorkRandom,null,"作业",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfChoseOneAttr.FK_Chose,null,"选择题",true,true,0,50,20);
				map.AddTBString(WorkRandomOfChoseOneAttr.Val,null,"项目",true,true,0,50,20);
				map.AddTBString(WorkRandomOfChoseOneAttr.Answer,null,"答案",true,true,0,50,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 选择题考试
		/// </summary> 
		public WorkRandomOfChoseOne()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfChoseOne(string work,string empid,string fk_chose)
		{
			this.FK_Chose = fk_chose;
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
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.FK_Chose,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  选择题考试
	/// </summary>
	public class WorkRandomOfChoseOnes :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfChoseOnes
		/// </summary>
		public WorkRandomOfChoseOnes(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfChoseOnes(string FK_WorkRandom,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfChoseOneAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfChoseOneAttr.FK_WorkRandom,FK_WorkRandom);


			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfChoseOne
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfChoseOne();
			}
		}
	}
}
