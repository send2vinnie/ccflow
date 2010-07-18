using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 选择题考试attr
	/// </summary>
	public class WorkOfChoseAttr:WorkThemeBaseAttr
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
	public class WorkOfChose :WorkThemeBase
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
				 
				return this.GetValStringByKey(WorkOfChoseAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.Answer,value);
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
				
				Map map = new Map("GTS_WorkOfChose");
				map.EnDesc="试卷选择题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfChoseAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkOfChoseAttr.FK_Work,null,"作业",true,true,0,50,20);
				map.AddTBStringPK(WorkOfChoseAttr.FK_Chose,null,"选择题",true,true,0,50,20);
				map.AddTBString(WorkOfChoseAttr.Val,null,"项目",true,true,0,50,20);
				map.AddTBString(WorkOfChoseAttr.Answer,null,"答案",true,true,0,50,20);
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
		public WorkOfChose()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfChose(string work,string empid,string fk_chose)
		{
			this.FK_Chose = fk_chose;
			this.FK_Emp=empid;
			this.FK_Work=work;
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
				return this.GetValStringByKey(WorkOfChoseAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.FK_Chose,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfChoseAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  选择题考试
	/// </summary>
	public class WorkOfChoses :WorkThemeBases
	{
		/// <summary>
		/// WorkOfChoses
		/// </summary>
		public WorkOfChoses(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfChoses(string fk_work,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkOfChoseAttr.FK_Work,fk_work);


			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfChose
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfChose();
			}
		}
	}
}
