
using System;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;

namespace BP.Rpt
{
	/// <summary>
	/// ������������
	/// </summary>
	public enum AnalyseDataType
	{
		/// <summary>
		/// ����
		/// </summary>
		AppInt,
		/// <summary>
		/// ����
		/// </summary>
		AppMoney,
		/// <summary>
		/// ����
		/// </summary>
		AppFloat
	}
	/// <summary>
	/// ����Ŀ��
	/// </summary>
	public class AnalyseObj
	{
		/// <summary>
		/// ����Ŀ��
		/// </summary>
		/// <param name="dp">����</param>
		/// <param name="oc">������</param>
		/// <param name="adt">��������������</param>
		public AnalyseObj(string dp, string oc, AnalyseDataType adt)
		{
			this.DataProperty=dp;
			this.OperationColumn=oc;
			this.HisADT = adt;
		}
		/// <summary>
		/// ����
		/// </summary>
		public string DataProperty="����";
		/// <summary>
		/// ����
		/// </summary>
		public string OperationColumn="COUNT(*)";
		/// <summary>
		/// ��������������
		/// </summary>
		public AnalyseDataType HisADT=AnalyseDataType.AppInt;
	}
	/// <summary>
	/// ����Ŀ��s
	/// </summary>
	public class AnalyseObjs:CollectionBase
	{
		/// <summary>
		/// ����Ŀ��s
		/// </summary>
		public AnalyseObjs(){}
		/// <summary>
		/// ����Ŀ��
		/// </summary>
		public AnalyseObj this[int index]
		{
			get
			{
				return (AnalyseObj)this.InnerList[index];
			}
		}
		/// <summary>
		/// ����һ����������
		/// </summary>
		/// <param name="DataProperty">��������</param>
		/// <param name="OperationColumn">������</param>
		/// <param name="adp">��������</param>
		/// <returns>�������ӵ�λ��</returns>
		public virtual int AddAnalyseObj(string  DataProperty, string OperationColumn, AnalyseDataType adp)
		{
			AnalyseObj ao = new AnalyseObj(DataProperty,OperationColumn,adp);
			return this.InnerList.Add(ao);
		}
		/// <summary>
		/// �õ�һ���������󣬸���Ҫ��������
		/// </summary>
		/// <param name="oc">Ҫ��������</param>
		/// <returns>��������</returns>
		public AnalyseObj GetAnalyseObjByOperationColumn(string oc)
		{

			foreach(AnalyseObj ao in this)
			{
				if (ao.OperationColumn==oc)
					return ao;
			}
			throw new Exception("û���ҵ�OperationColumn="+oc+"�ķ�������");
		}
	}
	/// <summary>
	/// Entity ��ժҪ˵����
	/// </summary>	
	[Serializable]
	abstract public class Rpt3D
	{
		#region ����
		/// <summary>
		/// �õ�γ�ȵ�ʵ�弯�ϣ�ͨ�����ԡ�
		/// </summary>
		/// <param name="attrOfD">γ������</param>
		/// <returns>EntitiesNoName</returns>
		protected Entities GetEntitiesByAttrKey(string attrOfD)
		{
			Attr attr = this.HisEns.GetNewEntity.EnMap.GetAttrByKey(attrOfD);
			if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum)
			{
				/*�����Enum ���ͣ�*/
				SysEnums sysEnums = new SysEnums(attr.UIBindKey);
				return sysEnums;
			}
			else 
			{
                Entities ens = attr.HisFKEns; // ClassFactory.GetEns(attr.UIBindKey);
				ens.RetrieveAll();
				return ens;
				//	return ens.ToEntitiesNoName(attr.UIRefKeyValue,attr.UIRefKeyText);
			}

		}
		protected EntitiesNoName GetEntitiesNoNameByAttrKey_del(string attrOfD)
		{
			Attr attr = this.HisEns.GetNewEntity.EnMap.GetAttrByKey(attrOfD);

			
			if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum)
			{
				/*�����Enum ���ͣ�*/
				SysEnums sysEnums = new SysEnums(attr.UIBindKey);
				return sysEnums.ToEntitiesNoName();
			}
			else 
			{
                Entities ens = attr.HisFKEns; 
				ens.RetrieveAll();

				return ens.ToEntitiesNoName(attr.UIRefKeyValue,attr.UIRefKeyText);
			}

		}
		/// <summary>
		/// γ��ʵ�弯��
		/// </summary>
		public Entities GetDEns(string attrKey)
		{
			return GetEntitiesByAttrKey(attrKey);
			/*
			if (attrKey=="BP.Port.Depts")
			{
				BP.Port.Depts Depts=(BP.Port.Depts)ens;
				Depts.RetrieveAllNoXJ();
				return Depts;
			}
			return ens;
			*/

		}
		/// <summary>
		/// γ��2��ʵ�弯��
		/// </summary>
		public Entities DEns2
		{
			get
			{
				//return null;
				return GetEntitiesByAttrKey(this.AttrOfD2) ;
			}
		}
		/// <summary>
		/// γ��3��ʵ�弯��
		/// </summary>
		public Entities DEns3
		{
			get
			{
				//return null;

				return GetEntitiesByAttrKey(this.AttrOfD3) ;
			}
		}
		#endregion

		#region ��ѯ����
		/// <summary>
		/// �������ԵĲ�ѯ���Լ���
		/// </summary>
		private AttrsOfSearch _HisAttrsOfSearch=null;
		/// <summary>
		/// �������ԵĲ�ѯ���Լ��ϡ�
		/// </summary>
		public AttrsOfSearch HisAttrsOfSearch
		{
			get
			{
				if (_HisAttrsOfSearch==null)
				{
					_HisAttrsOfSearch = new AttrsOfSearch();
				}
				return  _HisAttrsOfSearch;
			}
		}
		/// <summary>
		/// �����ѯ����
		/// </summary>
		private Attrs _HisFKSearchAttrs=null;
		/// <summary>
		/// �����ѯ���ԡ�
		/// </summary>
		public Attrs HisFKSearchAttrs
		{
			get
			{
				if (_HisFKSearchAttrs==null)
				{
					_HisFKSearchAttrs = new Attrs();
				}
				return  _HisFKSearchAttrs;
			}
			set
			{
				_HisFKSearchAttrs=value;
			}
		}
		/// <summary>
		/// ���������ѯ����
		/// </summary>
		/// <param name="key">��ѯ����key</param>
		public void AddFKSearchAttrs(string key)
		{
			this.HisFKSearchAttrs.Add(this.HisEn.EnMap.GetAttrByKey(key),false,false);
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������.
		/// </summary>
		public int DataType=BP.DA.DataType.AppInt;
		
		public bool _IsShowSum=false;
		/// <summary>
		/// �Ƿ������ʾ�ϼ�(default true)
		/// </summary>
		public bool IsShowSum
		{
			get
			{
				if ( IsShowRate)
					return true;
				else
					return _IsShowSum;
			}
			set
			{
				_IsShowSum =value;
			}
		}
		/// <summary>
		/// ��ʾ����(default true)
		/// </summary>
		public bool IsShowRate=true;

		/// <summary>
		/// Entity
		/// </summary>
		private Entity _HisEn=null;
		public Entity HisEn
		{
			get
			{
				if (this._HisEn==null)
				{
					this._HisEn = this.HisEns.GetNewEntity;

				}
				return this._HisEn ;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		private Entities _HisEns=null;
		/// <summary>
		/// ��������
		/// </summary>
		public Entities HisEns
		{
			get
			{
				return _HisEns;
			}
			set
			{
				_HisEns=value;
			}
		}
		/// <summary>
		/// 1γ������
		/// </summary>
		public string AttrOfD1=null;
		 
		/// <summary>
		/// 2γ������
		/// </summary>
		public  string AttrOfD2=null;
		 
		/// <summary>
		/// 3γ������
		/// </summary>
		public string AttrOfD3 =null;
		
		/// <summary>
		/// 2γ��3γ�ȹ������ԡ�
		/// </summary>
		private string _D2D3RefKey="";
		/// <summary>
		/// 3γ������
		/// </summary>
		public string D2D3RefKey
		{
			get
			{
				if (_D2D3RefKey=="")
				{
					Attr d2=this.HisEn.EnMap.GetAttrByKey(this.AttrOfD2) ;
					Attr d3=this.HisEn.EnMap.GetAttrByKey(this.AttrOfD3) ;

					_D2D3RefKey=BP.Sys.SysEnsRefs.GetRefSubEnKey(d2.UIBindKey,d3.UIBindKey) ;


					
				}
				return _D2D3RefKey;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Title="3γ����";
		/// <summary>
		/// �Ƿ�����ʾ�ؼ��ֲ�ѯ��
		/// </summary>
		public bool IsShowSearchKey=false;		 
		#endregion

		#region γ��1�Ļ������ԡ�
		private Attrs _DAttrs=null;
		/// <summary>
		/// γ�ȵ����ԣ���Щγ�ȿ��Թ��û�ѡ��
		/// </summary>
		public  Attrs  DAttrs
		{
			get
			{
				if (_DAttrs==null)				 
					_DAttrs =  new Attrs() ;
				return _DAttrs;
			}
		}
		/// <summary>
		/// ����һ��γ�����ԡ�
		/// </summary>
		/// <param name="attrKey">����</param>
		public void AddDAttrByKey(string attrKey)
		{
			DAttrs.Add(this.HisEn.EnMap.GetAttrByKey(attrKey),false,false);
		}
		#endregion

		#region ����Ŀ������
		/// <summary>
		/// ����Ŀ��s
		/// </summary>
		protected AnalyseObjs _HisAnalyseObjs=null;
		/// <summary>
		/// ����Ŀ��s
		/// </summary>
		public AnalyseObjs HisAnalyseObjs
		{
			get
			{
				if (this._HisAnalyseObjs==null)
				{
					_HisAnalyseObjs= new AnalyseObjs();
					_HisAnalyseObjs.AddAnalyseObj("����","COUNT(*)", BP.Rpt.AnalyseDataType.AppInt );
				}
				return _HisAnalyseObjs;
			}
		}
		

		
		/// <summary>
		/// �������ʡ�
		/// </summary>
		public string DataProperty="����";		
		/// <summary>
		/// ������
		/// ��Ĭ�� COUNT(*) ��.
		/// AVG(filed).
		/// </summary>
		public string OperationColumn="COUNT(*)";
		#endregion

		#region ����
		/// <summary>
		/// ʵ��
		/// </summary>
		public Rpt3D()
		{
			 
		}	 
		#endregion

	}
	/// <summary>
	/// EnObj ��ժҪ˵����
	/// </summary>
	abstract public class Rpt3Ds :CollectionBase
	{
		#region ����
		/// <summary>
		/// ���췽��
		/// </summary>
		public Rpt3Ds()
		{
			 
		}
//		/// <summary>
//		/// Rpt3Ds
//		/// </summary>
//		public Rpt3Ds this[int index]
//		{
//			return null ;
//			 
//
//		}
		#endregion
	}
}
