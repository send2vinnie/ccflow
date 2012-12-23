using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// ʵ��ʵ��֮��Ĺ���
	/// </summary>
	public class EnsRefAttr 
	{
		/// <summary>
		/// Ŀ¼ʵ��
		/// </summary>
		public const string CateEns="CateEns";
		/// <summary>
		/// ��ʵ��
		/// </summary>
		public const string SubEns="SubEns";
		/// <summary>
		/// ��������ʵ������
		/// </summary> 
		public const string RefSubEnKey="RefSubEnKey";	
	}
	 
	/// <summary>
	/// ʵ��ʵ��֮��Ĺ���
	/// </summary>
	public class SysEnsRef:Entity 
	{
		#region ��������		 
		/// <summary>
		/// Ŀ¼ʵ��
		/// </summary>
		public string CateEns
		{
			get
			{
				return this.GetValStringByKey(EnsRefAttr.CateEns ) ; 
			}
			set
			{
				this.SetValByKey(EnsRefAttr.CateEns,value) ; 
			}
		}
		/// <summary>
		/// ��ʵ��
		/// </summary>
		public string SubEns
		{
			get
			{
				return this.GetValStringByKey(EnsRefAttr.SubEns ) ; 
			}
			set
			{
				this.SetValByKey(EnsRefAttr.SubEns,value) ; 
			}
		}
		/// <summary>
		/// ������key
		/// </summary>
		public string RefSubEnKey
		{
			get
			{
				return this.GetValStringByKey(EnsRefAttr.RefSubEnKey ) ; 
			}
			set
			{
				this.SetValByKey(EnsRefAttr.RefSubEnKey,value) ; 
			}
		}
		 
		 
		#endregion

		#region ���췽��
		/// <summary>
		/// ϵͳʵ��
		/// </summary>
		public SysEnsRef()
		{
		}		 
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("Sys_EnsRef");
				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;
				map.EnType=EnType.Sys;
				map.EnDesc="ʵ����Ϣ";
				 
				map.EnType=EnType.Sys;
				map.AddTBStringPK(EnsRefAttr.CateEns,null,"Ŀ¼ʵ��",true,false,0,100,60);
				map.AddTBStringPK(EnsRefAttr.SubEns,null,"��ʵ��",true,true,0,90,10);
				map.AddTBStringPK(EnsRefAttr.RefSubEnKey,null,"����������",true,false,0,50,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ��ѯ����
		
		
		#endregion


	}
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class SysEnsRefs : Entities
	{		
		#region ����
		public SysEnsRefs()
		{}
		/// <summary>
		/// ������ʵ���ҳ�
		/// </summary>
		/// <param name="subEns"></param>
		public SysEnsRefs(string subEns)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EnsRefAttr.SubEns,subEns);
			qo.DoQuery();
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SysEnsRef();
			}

		}
		#endregion

		#region ��̬����
		/// <summary>
		/// ȡ��Ŀ¼ʵ������ʵ�������key
		/// ����������Ͼͷ���null.
		/// </summary>
		/// <param name="cateEns">Ŀ¼ʵ��</param>
		/// <param name="subEns">��ʵ��</param>
		/// <returns>�����ļ�</returns>
		public static string GetRefSubEnKey(string cateEns,string subEns)
		{
			//return "FK_Dept";
			SysEnsRefs ens = new SysEnsRefs();
			QueryObject qo =new QueryObject(ens);
			qo.AddWhere( EnsRefAttr.CateEns, cateEns);
			qo.addAnd();
			qo.AddWhere( EnsRefAttr.SubEns, subEns);
			
			int i = qo.DoQuery(); 
			if (i==0)
				return null;
			
			SysEnsRef en = (SysEnsRef)ens[0];
			return en.RefSubEnKey ;
			
			//return ens[0].GetValStringByKey(EnsRefAttr.RefSubEnKey);
		}
		#endregion

		#region ��ѯ����
		 
		#endregion
		
	}
}
