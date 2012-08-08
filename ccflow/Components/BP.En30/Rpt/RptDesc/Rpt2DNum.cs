
using System;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;

namespace BP.Rpt
{
	/// <summary>
	/// Entity ��ժҪ˵����
	/// ���౨��ʵ�����ڶ�һ���ض����м��㡣
	/// </summary>	
	[Serializable]
	abstract public class Rpt2DNum
	{
		#region ����
		/// <summary>
		/// �õ�γ�ȵ�ʵ�弯�ϣ�ͨ�����ԡ�
		/// </summary>
		/// <param name="attrOfD">γ������</param>
		/// <returns>EntitiesNoName</returns>
		protected EntitiesNoName GetEntitiesNoNameByAttrKey(string attrOfD)
		{
			Attr attr = this.HisEns.GetNewEntity.EnMap.GetAttrByKey(attrOfD) ;
			EntitiesNoName ens = (EntitiesNoName)attr.HisFKEns ;
			ens.RetrieveAll();
			return ens;
		}
		/// <summary>
		/// γ��1��ʵ�弯��
		/// </summary>
		public EntitiesNoName GetDEns(string attrKey)
		{
			return GetEntitiesNoNameByAttrKey( attrKey ) ;
		}  
		#endregion

		#region ��ѯ����
		public bool IsShowSearchKey=false;	 
		/// <summary>
		/// ����ʵ���ѯ���ԡ�
		/// </summary>
		private AttrsOfSearch _HisAttrsOfSearch=null;
		public AttrsOfSearch  HisAttrsOfSearch
		{
			get
			{
				if (_HisAttrsOfSearch==null)
					_HisAttrsOfSearch=new AttrsOfSearch();
				return _HisAttrsOfSearch ;
			}
		}		 
		/// <summary>
		/// �����ѯ���ԡ�
		/// </summary>
		private Attrs _HisFKSearchAttrs;
		public Attrs HisFKSearchAttrs
		{
			get
			{
				if (_HisFKSearchAttrs==null)
					_HisFKSearchAttrs=new Attrs();
				return _HisFKSearchAttrs;
			}
		}
		public void AddFKSearchAttrs(string fk)
		{
			HisFKSearchAttrs.Add(this.HisEn.EnMap.GetAttrByKey(fk),false, false); 
		}
		#endregion

		#region abstract ��������
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
		private Entities _HisEns=null;
		public Entities HisEns
		{
			get
			{
				if (_HisEns==null)
					throw new Exception("@û��ָ��Ҫ������ʵ�����ԡ�");
				return _HisEns;
			}
			set
			{
				this._HisEns=value;
			}
		}		 
		/// <summary>
		/// ����
		/// </summary>
		public  string Title="@xyer����";
		public  string LeftTitle="��Ŀ";
		#endregion

		#region γ��1�Ļ������ԡ�
		private Attrs _AttrsOfD1=null;
		/// <summary>
		/// ��1γ�ȵ����ԡ�
		/// </summary>
		public  Attrs  AttrsOfD1
		{
			get
			{
				if (_AttrsOfD1==null)
				{
					_AttrsOfD1 =  new Attrs() ;
				}
				return _AttrsOfD1;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="attrKey">����</param>
		public void AddAttrOfD1ByKey(string attrKey)
		{
			AttrsOfD1.Add(this.HisEn.EnMap.GetAttrByKey(attrKey),false,false);
		}
		#endregion

		#region γ�� ��ֵ �����ԡ�
		//public WorkWay WorkWay=WorkWay.Sum;
		/// <summary>
		/// Ҫ�������ֵ���ԡ�
		/// </summary>
		private DAttrs _DAttrs=null;
		/// <summary>
		/// ��1γ�ȵ����ԡ�
		/// </summary>
		public  DAttrs  DAttrs
		{
			get
			{
				if (_DAttrs==null)
				{
					_DAttrs =  new DAttrs();					
				}
				return _DAttrs;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="attrKey">����</param>
		public void AddDAttr(string attrKey, WorkWay ww,bool IsCutIfIsZero)
		{
			DAttr attr = new DAttr(this.HisEn.EnMap.GetAttrByKey(attrKey), ww,null, IsCutIfIsZero);
			this.DAttrs.Add(attr);
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="attrKey">����</param>
		/// <param name="ww"></param>
		/// <param name="tag"></param>
		public void AddDAttrSelf(string attrKey, string tag, bool IsCutIfIsZero)
		{
			DAttr attr = new DAttr(this.HisEn.EnMap.GetAttrByKey(attrKey),WorkWay.Self,tag,IsCutIfIsZero);
			this.DAttrs.Add(attr);
		}
		#endregion

		#region ����
		/// <summary>
		/// ʵ��
		/// </summary>
		public Rpt2DNum()
		{
		}	 
		#endregion

	}
	 
}
