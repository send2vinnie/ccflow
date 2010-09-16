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
	public class PaperExamOfFillBlankAttr:EntityOIDAttr
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

	}
	/// <summary>
	/// 填空题考试
	/// </summary>
	public class PaperExamOfFillBlank :Entity
	{

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
				
				Map map = new Map("GTS_PaperExamOfFillBlank");
				map.EnDesc="试卷填空题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_Paper,null,"考卷",true,true,0,50,20);
				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_FillBlank,null,"填空题",true,true,0,50,20);
				map.AddTBIntPK(PaperExamOfFillBlankAttr.IDX,-1,"序号",true,false);
				map.AddTBString(PaperExamOfFillBlankAttr.Val,null,"val",true,true,0,5000,20);

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
		public PaperExamOfFillBlank()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfFillBlank(string paper,string empNo,string fk_FillBlank, int idx)
		{
			this.FK_FillBlank = fk_FillBlank;
			this.FK_Emp=empNo;
			this.FK_Paper=paper;
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
				return this.GetValIntByKey(PaperExamOfFillBlankAttr.IDX);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.IDX,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_FillBlank
		/// </summary>
        public string FK_FillBlank
        {
            get
            {
                return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_FillBlank);
            }
            set
            {
                this.SetValByKey(PaperExamOfFillBlankAttr.FK_FillBlank, value);
            }
        }
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  填空题考试
	/// </summary>
	public class PaperExamOfFillBlanks :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfFillBlanks
		/// </summary>
		public PaperExamOfFillBlanks(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfFillBlanks(string fk_paper, string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(PaperExamOfChoseAttr.FK_Paper,fk_paper);

			qo.addOrderBy(PaperExamOfFillBlankAttr.FK_FillBlank, PaperExamOfFillBlankAttr.IDX );
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfFillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfFillBlank();
			}
		}
	}
}
