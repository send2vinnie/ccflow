using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷分布attr
	/// </summary>
    public class PaperRandomFBAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Type
        /// </summary>
        public const string FK_Type = "FK_Type";
        /// <summary>
        /// FK_Sort
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        /// <summary>
        /// HisNum
        /// </summary>
        public const string HisNum = "HisNum";
    }
	/// <summary>
	/// 试卷分布
	/// </summary>
	public class PaperRandomFB :EntityNoName
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
				
				Map map = new Map("GTS_PaperRandomFB");
				map.EnDesc="随机试卷分布设计";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

                map.AddTBStringPK(PaperRandomFBAttr.No, null, "No", true, true, 4, 4, 4);
                map.AddTBString(PaperRandomFBAttr.Name, null, "Name", true, true, 4, 4, 4);

			 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷分布
		/// </summary> 
		public PaperRandomFB()
		{
		}
		/// <summary>
		/// 试卷分布
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public PaperRandomFB(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  试卷分布
	/// </summary>
	public class PaperRandomFBs :EntitiesNoName
	{
		/// <summary>
		/// PaperRandomFBs
		/// </summary>
		public PaperRandomFBs(){}
		/// <summary>
		/// PaperRandomFB
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperRandomFB();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaperRandomFB(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}
