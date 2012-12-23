using System;
//using System.Drawing;
//// using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En ; 
using BP.DA;
using BP.Web ; 


namespace BP.Rpt
{
	/// <summary>
	/// 2���汨��ʵ��
	/// </summary>
	public class RptPlanarEntity : RptEntity
	{
		#region ���ԡ�
		
		/// <summary>
		/// ����
		/// </summary>
		public string DataProperty="����";		
		/// <summary>
		/// �Ƿ�Ҫ��ʾItem��Url��
		/// </summary>
		private int _IsShowItem1Url=-1;
		/// <summary>
		/// �ǲ�����Ҫ��ʾItem1 ��URL.
		/// </summary>
		public bool IsShowItem1Url
		{
			get
			{
				if (this._IsShowItem1Url==-1)
				{
					RefLinks ens = new RefLinks(this.SingleDimensionItem1.GetNewEntity.ToString()) ;
					if (ens.Count==0)
						_IsShowItem1Url=0;
					else
						_IsShowItem1Url=1;
				}

				if (_IsShowItem1Url==0)
					return false;
				else
					return true;		 
				
			}
		}
		/// <summary>
		/// �Ƿ�Ҫ��ʾItem2��Url��
		/// </summary>
		private int _IsShowItem2Url=-1;
		/// <summary>
		/// �ǲ�����Ҫ��ʾItem1 ��URL.
		/// </summary>
		public bool IsShowItem2Url
		{
			get
			{
				if (this._IsShowItem2Url==-1)
				{
					RefLinks ens = new RefLinks(this.SingleDimensionItem2.GetNewEntity.ToString()) ;
					if (ens.Count==0)
						_IsShowItem2Url=0;
					else
						_IsShowItem2Url=1;
				}
				if (_IsShowItem2Url==0)
					return false;
				else
					return true;				
			}
		}
		#endregion 

		#region ����
		/// <summary>
		/// ���汨��ʵ��
		/// </summary>
		public RptPlanarEntity()
		{
		}
		/// <summary>
		/// ���汨��ʵ��
		/// </summary>
		/// <param name="d1">һ������</param>
		/// <param name="d2">����һ������</param>
		/// <param name="cells">��Ԫ��</param>
		public RptPlanarEntity(Entities d1,Entities d2, RptPlanarCells cells)
		{
			this.SingleDimensionItem1 = d1;
			this.SingleDimensionItem2 = d2;
			this.PlanarCells = cells;
		}
		/// <summary>
		/// ���汨��ʵ��
		/// </summary>
		/// <param name="d1">γ��1����</param>
		/// <param name="d2">γ��2����</param>
		/// <param name="dt">���ݱ�</param>
		/// <param name="cellUrl">��Ԫ������</param>
		/// <param name="adt">��������</param>
		public RptPlanarEntity(Entities d1,Entities d2, 
			DataTable dt, string cellUrl, AnalyseDataType adt)
		{
			this.SingleDimensionItem1 = d1 ;
			this.SingleDimensionItem2 = d2 ;
			this.PlanarCells =  new RptPlanarCells(dt, cellUrl);
			this.HisADT = adt ;
		}
		public RptPlanarEntity(Entities d1,Entities d2, 
			DataTable dt)
		{
			this.SingleDimensionItem1 = d1 ;
			this.SingleDimensionItem2 = d2 ;
			this.PlanarCells =  new RptPlanarCells(dt);
		}
		#endregion
		 
		#region  ���� ����Ԫ��
		/// <summary>
		/// һάʵ��1
		/// </summary>
		public Entities SingleDimensionItem1=null;
		/// <summary>
		/// һάʵ��2
		/// </summary>
		public Entities SingleDimensionItem2=null;
		/// <summary>
		/// ��Ԫs
		/// </summary>
		public RptPlanarCells PlanarCells=null;
		#endregion

		#region ����
		/// <summary>
		/// ����γ��ɾ�����ܶ�Ӧ�ϵ�γ��
		/// </summary>
		public void CutNotRefD1()
		{
			EntitiesNoName ens =(EntitiesNoName)this.SingleDimensionItem1.CreateInstance();
			foreach(EntityNoName en in this.SingleDimensionItem1)
			{
				foreach(RptPlanarCell cell in this.PlanarCells)
				{
					if (cell.PK1==en.No)
					{
						ens.AddEntity(en);
						break;
					}
				}
			}
			this.SingleDimensionItem1 =ens ; 
		}
		/// <summary>
		/// ����γ��ɾ�����ܶ�Ӧ�ϵ�γ��
		/// </summary>
		public void CutNotRefD2()
		{
			EntitiesNoName ens =(EntitiesNoName)this.SingleDimensionItem2.CreateInstance();
			foreach(EntityNoName en in this.SingleDimensionItem2)
			{
				foreach(RptPlanarCell cell in this.PlanarCells)
				{
					if (cell.PK2==en.No)
					{
						ens.AddEntity(en);
						break;
					}
				}
			}
			this.SingleDimensionItem2 =ens ; 
		}
		/// <summary>
		/// �õ�HTML����ʾ����Val ����URL������ת��ΪLink.
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <returns>string</returns>
		public string GetCellContext(string pk1, string pk2)
		{
			RptPlanarCell cell = this.PlanarCells.GetCell(pk1,pk2);
			if (cell.Url=="")
				return cell.val.ToString();
			return "<a href='"+cell.Url+"?"+this.SingleDimensionItem1.GetNewEntity.ToString()+"="+cell.PK1+"&"+this.SingleDimensionItem2.GetNewEntity.ToString()+"="+cell.PK2+"' Target='"+CellUrlTarget+"' >"+cell.val+"</a>";
		}

		public string GetItem1Context(string pk1No,string pk1Name)
		{
			if (this.IsShowItem1Url)
				return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+"/Rpt/RptRefLink.aspx?EnsName="+this.SingleDimensionItem1.GetNewEntity.ToString()+"&val="+pk1No+"' > "+pk1Name+ " </a>";
			else
				return pk1Name;			 
		}
		public string GetItem2Context(string pk2No,string pk2Name)
		{
			if (this.IsShowItem2Url)
				return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+"/Rpt/RptRefLink.aspx?EnsName="+this.SingleDimensionItem2.GetNewEntity.ToString()+"&val="+pk2No+"' > "+pk2Name+ " </a>";
			else
				return pk2Name;
		}
		#endregion
	}
}
