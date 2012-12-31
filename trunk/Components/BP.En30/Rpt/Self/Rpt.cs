using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.Rpt
{
	/// <summary>
	/// ����
	/// </summary>
	public class List :EntityNoName
	{
		public string SearchAttrs
		{
			get
			{
				return this.GetValStringByKey("SearchAttrs");
			}
			set
			{
				this.SetValByKey("SearchAttrs",value);
			}
		}

		#region ʵ�ֻ����ķ���
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("Rpt_List");
				map.EnDesc="����";
				map.EnType= EnType.Sys;
				map.AddTBStringPK("No",null,"���",true,false,1,100,100);
				map.AddTBString("Name",null,"����",true,false,1,100,200);
				map.AddTBString("SearchAttrs",null,"��ѯ����",true,false,0,100,200);
				map.AddDDLEntities("FK_Sort",null,"���", new BP.Rpt.RptSorts(),false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary> 
		public List()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="_No">������</param> 
		public List(string ensName )
		{
			this.No=ensName;
			if (this.Retrieve("No",ensName)==0)
			{
				this.Name= DA.ClassFactory.GetEns(ensName).GetNewEntity.EnDesc;
					this.Insert();
			}
		}
		#endregion 
	}
	/// <summary>
	/// ����
	/// </summary>
	public class Lists :EntitiesNoName
	{
		/// <summary>
		/// ����s
		/// </summary>
		public Lists(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new List();
			}
		}
	}
}
