using System;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En ; 
using BP.DA;
using BP.Web ; 

namespace BP.Rpt
{
	/// <summary>
	/// 3ά���汨��ʵ��
	/// ���������Ƕ���һ���������ͽ��з�����
	/// </summary>
	public class  Rpt3DEntity:RptEntity
	{
	//	public void 
	
		#region ɾ����Ӧ���ϵ�γ��
		/// <summary>
		/// ����γ��ɾ�����ܶ�Ӧ�ϵ�γ��
		/// </summary>
		public void CutNotRefD1()
		{
			EntitiesNoName ens ;
			try
			{
				ens=(EntitiesNoName)this.SingleDimensionItem1.CreateInstance();
			}
			catch(Exception ex)
			{
				throw new Exception("γ��1ʵ�岻��EntityNoName ���ͣ�"+this.SingleDimensionItem1.ToString() + " exception="+ex.Message);
			}
			foreach(Rpt3DCell cell in this.HisCells) 
			{
				foreach(EntityNoName en in this.SingleDimensionItem1)
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
			
			EntitiesNoName ens ;
			try
			{
				ens=(EntitiesNoName)this.SingleDimensionItem2.CreateInstance();
			}
			catch(Exception ex)
			{
				throw new Exception("γ��2ʵ�岻��EntityNoName ���ͣ�"+this.SingleDimensionItem2.ToString()+ " exception="+ex.Message);
			}

			foreach(EntityNoName en in this.SingleDimensionItem2)
			{
				foreach(Rpt3DCell cell in this.HisCells)			
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
		/// ����γ��ɾ�����ܶ�Ӧ�ϵ�γ��
		/// </summary>
		public void CutNotRefD3()
		{
			EntitiesNoName ens ;
			try
			{
				ens=(EntitiesNoName)this.SingleDimensionItem3.CreateInstance();
			}
			catch(Exception ex)
			{
				throw new Exception("γ��3ʵ�岻��EntityNoName ���ͣ�"+this.SingleDimensionItem3.ToString()+ " exception="+ex.Message);
			}

			//EntitiesNoName ens =(EntitiesNoName)this.SingleDimensionItem3.CreateInstance();
			foreach(EntityNoName en in this.SingleDimensionItem3)
			{
				foreach(Rpt3DCell cell in this.HisCells)			
				{
					if (cell.PK3==en.No)
					{
						ens.AddEntity(en);
						break;
					}
				}
			}
			this.SingleDimensionItem3 =ens ; 
		}
		#endregion

		private string _D2D3RefKey="";
		/// <summary>
		/// d2d3�������key
		/// </summary>
		public string D2D3RefKey
		{
			get
			{
				if (_D2D3RefKey=="")
				{
					_D2D3RefKey=BP.Sys.SysEnsRefs.GetRefSubEnKey(this.D2ClassesName,this.D3ClassesName) ; 
				}
				return _D2D3RefKey;
			}
			set
			{
				_D2D3RefKey=value;
			}
		}

		public string LeftTitle1="&nbsp;";

		#region  ������
		private decimal _HisSum=-1 ;
		/// <summary>
		/// �ϼ�
		/// </summary>
		public decimal HisSum
		{
			get
			{
				if (_HisSum==-1)
				{
					 _HisSum=0;
					foreach(Rpt3DCell cell in this.HisCells)
					{
						_HisSum+= cell.valOfDecimal;
					}
				}
				return _HisSum;
			}
		}
		/// <summary>
		/// ���ݵ�1γ��ȡ��������Ϣ.
		/// </summary>
		/// <param name="d1">γ��key</param>
		/// <returns></returns>
		public decimal GetSumByD2D3(string d2, string d3)
		{
			decimal sumd1=0;
			foreach(Rpt3DCell cell in this.HisCells)
			{
				if (cell.PK2==d2 && cell.PK3==d3)
					sumd1+= cell.valOfDecimal ;
			}
			return sumd1;
		}
		/// <summary>
		/// ���ݵ�1γ��ȡ��������Ϣ.
		/// </summary>
		/// <param name="d1">γ��key</param>
		/// <returns></returns>
		public decimal GetSumByD1(string d1)
		{
			decimal sumd1=0;
			foreach(Rpt3DCell cell in this.HisCells)
			{
				if (cell.PK1==d1)
					sumd1+= cell.valOfDecimal ;
			}
			return sumd1;
		}
		public decimal GetSumByD2(string d2)
		{
			decimal sumd2=0;
			foreach(Rpt3DCell cell in this.HisCells)
			{
				if (cell.PK2==d2)
					sumd2+= cell.valOfDecimal ;
			}
			return sumd2;
		}
		#endregion

		#region ��������������Ŀ��ʾ���⡣
		
		/// <summary>
		/// �õ�HTML����ʾ����Val ����URL������ת��ΪLink.
		/// </summary>
		/// <param name="pk1">����1</param>
		/// <param name="pk2">����2</param>
		/// <param name="pk3">����3</param>
		/// <param name="adt">���ݷ�������</param>
		/// <returns>link</returns>
		public string GetCellContext(string pk1, string pk2, string pk3, AnalyseDataType adt)
		{
			Rpt3DCell cell = this.HisCells.GetCell(pk1,pk2,pk3);
			if (cell.Url=="")				
				return cell.val.ToString();

			//string val = cell.val.ToString();
			switch(adt)
			{
				case AnalyseDataType.AppFloat:
					return "<a href=\"javascript:openit('"+cell.Url+"&abc=xyz&"+ this.Key1 +"="+cell.PK1+"&"+this.Key2+"="+cell.PK2+"&"+this.Key3+"="+cell.PK3+"')\" >"+float.Parse(cell.val.ToString()) +"</a>";
					 
				case AnalyseDataType.AppInt:
					return "<a href=\"javascript:openit('"+cell.Url+"&abc=xyz&"+this.Key1+"="+cell.PK1+"&"+this.Key2+"="+cell.PK2+"&"+this.Key3+"="+cell.PK3+"')\" >"+  int.Parse(cell.val.ToString() ) +"</a>";
				 
				case AnalyseDataType.AppMoney:
					return "<a href=\"javascript:openit('"+cell.Url+"&abc=xyz&"+this.Key1+"="+cell.PK1+"&"+this.Key2+"="+cell.PK2+"&"+this.Key3+"="+cell.PK3+"')\" >"+decimal.Parse( cell.val.ToString() ).ToString("0.00")+"</a>";
					 
				default:
					throw new Exception("error adt");
			}

			
			//return "<a href=\"javascript:openit('"+cell.Url+"&abc=xyz&"+this.D1ClassesName+"="+cell.PK1+"&"+this.D2ClassesName+"="+cell.PK2+"&"+this.D3ClassesName+"="+cell.PK3+"')\" >"+cell.val+"</a>";
			//return "<a href='javascript:window.open('"+cell.Url+"&abc=xyz&"+this.D1ClassesName+"="+cell.PK1+"&"+this.D2ClassesName+"="+cell.PK2+"&"+this.D3ClassesName+"="+cell.PK3+"','"+CellUrlTarget+"','toolbar=false')' >"+cell.val+"</a>";
			//return "<a href='"+cell.Url+"&abc=xyz&"+this.D1ClassesName+"="+cell.PK1+"&"+this.D2ClassesName+"="+cell.PK2+"&"+this.D3ClassesName+"="+cell.PK3+"' Target='"+CellUrlTarget+"' >"+cell.val+"</a>";

		}
		public string GetCellContextOfRightSum(string pk1, string pk2 )
		{
			float f= this.HisCells.GetSumByPK1(pk1,pk2);
			return "";
			 
			//			if (_self==null)
			//			{
			///	return "<a href='"+cell.Url+"&abc=xyer&"+this.D1ClassesName+"="+cell.PK1+"&"+this.D2ClassesName+"="+cell.PK2+"' Target='"+CellUrlTarget+"' >"+f+"</a>";
			//}
		}
		/// <summary>
		/// �õ�HTML����ʾ����Val ����URL������ת��ΪLink.
		/// </summary>
		/// <param name="pk1"></param>
		/// <param name="pk2"></param>
		/// <returns></returns>
		public string GetCellContext_(string pk1, string pk1Name, string pk2 ,string pk2Name,  string pk3, string pk3Name)
		{
			Rpt3DCell cell = this.HisCells.GetCell(pk1,pk2,pk3);
			if (cell.Url=="")				
				return cell.val.ToString();
			return "<a href='"+cell.Url+"?"+this.D1ClassesName+"="+cell.PK1+"&"+this.D2ClassesName+"="+cell.PK2+"&"+this.D3ClassesName+"="+cell.PK3+"' Target='"+CellUrlTarget+"' >"+cell.val+"</a>";
		}
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
					RefLinks ens = new RefLinks(this.D1ClassesName) ;
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
		private int _IsShowItem2Url=-1;
		/// <summary>
		/// �ǲ�����Ҫ��ʾItem2 ��URL.
		/// </summary>
		public bool IsShowItem2Url
		{
			get
			{
				if (this._IsShowItem2Url==-1)
				{
					RefLinks ens = new RefLinks(this.D2ClassesName) ;
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
		private int _IsShowItem3Url=-1;
		/// <summary>
		/// �ǲ�����Ҫ��ʾItem3 ��URL.
		/// </summary>
		public bool IsShowItem3Url
		{
			get
			{
				if (this._IsShowItem3Url==-1)
				{
					RefLinks ens = new RefLinks(this.D3ClassesName) ;
					if (ens.Count==0)
						_IsShowItem3Url=0;
					else
						_IsShowItem3Url=1;
				}
				if ( _IsShowItem3Url==0)
					return false;
				else
					return true;				
			}
		}
		#endregion 

		#region ����
		/// <summary>
		/// 3ά���汨��ʵ��
		/// </summary>
		public Rpt3DEntity()
		{

		}

		/// <summary>
		/// 3ά���汨��ʵ��
		/// </summary>
		/// <param name="d1">d1</param>
		/// <param name="d2">d2</param>
		/// <param name="d3">d3</param>
		/// <param name="dt">dt</param>
		public Rpt3DEntity(Entities d1, Entities d2,Entities d3, DataTable dt,string url)
		{
			this.SingleDimensionItem1=d1;
			this.SingleDimensionItem2=d2;
			this.SingleDimensionItem3=d3;
			Rpt3DCells cells = new Rpt3DCells(dt,url);
			this.HisCells = cells ; 
		}
		public Rpt3DEntity(Entities d1, Entities d2,Entities d3, DataTable dt )
		{
			this.SingleDimensionItem1=d1;
			this.SingleDimensionItem2=d2;
			this.SingleDimensionItem3=d3;
			Rpt3DCells cells = new Rpt3DCells(dt);
			this.HisCells = cells;
		}
		#endregion
		 
		#region ����
		public string DataProperty="����";
		/// <summary>
		/// һάʵ��(һ������)
		/// </summary>
		public Entities SingleDimensionItem1=null;
		/// <summary>
		/// άʵ�� (1����Ŀ)
		/// </summary>
		public Entities  SingleDimensionItem2 =null;
		/// <summary>
		/// �༶Ԫ��
		/// </summary>
		public Entities SingleDimensionItem3=null;
		/// <summary>
		/// Cell����
		/// </summary>
		public Rpt3DCells HisCells=null;
		/// <summary>
		/// ����άҪ�������ֶΡ�
		/// </summary>
		public string Item3RefKey="";

		public Entities GetItem3ByKey(string key)
		{
			if (Item3RefKey=="")
			{
				return this.SingleDimensionItem3 ;
			}
			else
			{
				DBSimpleNoNames ens = new DBSimpleNoNames();
				foreach(EntityNoName en in this.SingleDimensionItem3)
				{
					if (en.GetValStringByKey(this.Item3RefKey)==key)
					{
						DBSimpleNoName bn = new DBSimpleNoName();
						bn.No = en.No;
						bn.Name =en.Name ;
						ens.AddEntity(bn);
					}
				}
				return ens;
			}
		}
		#endregion

		#region ��������

		#region class name
		/// <summary>
		/// 1γ�ȵ�������
		/// </summary>
		private string _d1EnsName=null;
		/// <summary>
		/// 1γ�ȵ������ơ�
		/// </summary>
		public string D1ClassesName
		{
			get
			{
				if (_d1EnsName==null)
				{
					_d1EnsName=this.SingleDimensionItem1.ToString();
				}
				return _d1EnsName;
			}
		}
		/// <summary>
		/// 2γ�ȵ�������
		/// </summary>
		private string _d2EnsName=null;
		/// <summary>
		/// 2γ�ȵ������ơ�
		/// </summary>
		public string D2ClassesName
		{
			get
			{
				if (_d2EnsName==null)
				{
					_d2EnsName=this.SingleDimensionItem2.ToString();
				}
				return _d2EnsName;
			}
		}
		/// <summary>
		/// 3γ�ȵ�������
		/// </summary>
		private string _d3EnsName=null;
		/// <summary>
		/// 3γ�ȵ������ơ�
		/// </summary>
		public string D3ClassesName
		{
			get
			{
				if (_d3EnsName==null)
				{
					_d3EnsName=this.SingleDimensionItem3.ToString();
				}
				return _d3EnsName;
			}
		}
		#endregion


		public string GetD1Context(EntityNoName en)
		{
			return this.GetD1Context(en.No,en.Name);
		}
		public string GetD2Context(EntityNoName en)
		{
			return this.GetD2Context(en.No,en.Name);
		}
		public string GetD3Context(EntityNoName en)
		{
			return this.GetD3Context(en.No,en.Name);
		}
		/// <summary>
		/// �˷�������Ϊ��HTML ʹ�á�
		/// </summary>
		/// <param name="pk1No">No ֵ</param>
		/// <param name="name">����</param>
		/// <returns>html</returns>
		public string GetD1Context(string no,string name)
		{
			if (this.IsShowItem1Url)
				return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Rpt/RptRefLink.aspx?EnsName="+this.D1ClassesName+"&val="+no+"' > "+name+ " </a>";
			else
				return name;
		}
		/// <summary>
		/// �˷�������Ϊ��HTML ʹ�á�
		/// </summary>
		/// <param name="pk1No">No ֵ</param>
		/// <param name="name">����</param>
		/// <returns>html</returns>
		public string GetD2Context(string no,string name)
		{
			if (this.IsShowItem2Url)
				return "<a href=\"window.open('"+System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Rpt/RptRefLink.aspx?EnsName="+this.D2ClassesName+"&val="+no+"')\" > "+name+ " </a>";
			else
				return name; 
		  
			
		}
		/// <summary>
		/// �˷�������Ϊ��HTML ʹ�á�
		/// </summary>
		/// <param name="pk1No">No ֵ</param>
		/// <param name="name">����</param>
		/// <returns>html</returns>
		public string GetD3Context(string no,string name)
		{
			if (this.IsShowItem3Url)
				return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Rpt/RptRefLink.aspx?EnsName="+this.D3ClassesName+"&val="+no+"' > "+name+ " </a>";
			else
				return name;
		}
		public string GetItem3Context(EntityNoName en)
		{
			return this.GetD3Context(en.No,en.Name) ; 
		}
		public string GetItem2Context(EntityNoName en)
		{
			return this.GetD2Context(en.No,en.Name); 
		}
		public string GetItem1Context(EntityNoName en)
		{
			return this.GetD1Context(en.No,en.Name) ; 
		}
		#endregion 
	}
}
