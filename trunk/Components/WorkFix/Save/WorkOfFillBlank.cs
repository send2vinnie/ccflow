using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 填空题考试attr
	/// </summary>
	public class WorkOfFillBlankAttr:WorkThemeBaseAttr
	{
		/// <summary>
		/// 填空题
		/// </summary>
		public const string FK_FillBlank="FK_FillBlank";
		/// <summary>
		/// 序号
		/// </summary>
		public const string IDX="IDX";
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
	/// 填空题考试
	/// </summary>
	public class WorkOfFillBlank :WorkThemeBase
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
				
				Map map = new Map("GTS_WorkOfFillBlank");
				map.EnDesc="试卷填空题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfFillBlankAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkOfFillBlankAttr.FK_Work,null,"作业",true,true,0,50,20);
				map.AddTBStringPK(WorkOfFillBlankAttr.FK_FillBlank,null,"填空题",true,true,0,50,20);
				map.AddTBIntPK(WorkOfFillBlankAttr.IDX,-1,"序号",true,false);
				map.AddTBString(WorkOfFillBlankAttr.Val,null,"val",true,true,0,500,20);
				map.AddTBString(WorkOfFillBlankAttr.Answer,null,"答案",true,true,0,500,20);


				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfFillBlank+this.CentOfFillBlankOne+this.CentOfFillBlankM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 填空题考试
		/// </summary> 
		public WorkOfFillBlank()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfFillBlank(string work,string empNo,string fk_FillBlank, int idx)
		{
			this.FK_FillBlank = fk_FillBlank;
			this.FK_Emp=empNo;
			this.FK_Work=work;
			this.IDX=idx;
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

		#region attrs
		/// <summary>
		/// 序号
		/// </summary>
		public int IDX
		{
			get
			{
				return this.GetValIntByKey(WorkOfFillBlankAttr.IDX);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.IDX,value);
			}
		}
		/// <summary>
		/// FK_FillBlank
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkOfFillBlankAttr.FK_FillBlank);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.FK_FillBlank,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfFillBlankAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  填空题考试
	/// </summary>
	public class WorkOfFillBlanks :WorkThemeBases
	{
		/// <summary>
		/// WorkOfFillBlanks
		/// </summary>
		public WorkOfFillBlanks(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfFillBlanks(string fk_work, string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkOfChoseAttr.FK_Work,fk_work);

			qo.addOrderBy(WorkOfFillBlankAttr.FK_FillBlank, WorkOfFillBlankAttr.IDX );
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfFillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfFillBlank();
			}
		}
	}
}
