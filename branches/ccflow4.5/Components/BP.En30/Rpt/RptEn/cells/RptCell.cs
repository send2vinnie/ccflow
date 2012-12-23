using System;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En; 
using BP.DA;
/*
 * Edit by peng 2004-09-24
 * RptCell��
 * ����Ļ���Ԫ�أ�������ɱ���Ļ������֡�
 * ��Ϊ2γ��3γ������Ԫ�� 
 * 
 * */

namespace BP.Rpt
{
	public class Rpt1DCell
	{
		
		public Rpt1DCell()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pk"></param>
		/// <param name="val"></param>
		public Rpt1DCell(string pk, Object val)
		{
			this.PK = pk ;
			this.val = val;
		}

		/// <summary>
		/// PK1
		/// </summary>
		public string PK=null;
		/// <summary>
		/// ֵ
		/// </summary>
		public Object val=null;

		public decimal valOfDecimal
		{
			get
			{
				try
				{
					return decimal.Round( decimal.Parse(val.ToString()), 4);
				}
				catch
				{
					return 0;
				}
			}
		}
		public float valOfFloat
		{
			get
			{
				try
				{
					return float.Parse(val.ToString());
				}
				catch
				{
					return 0;
				}
				 
			}
		}
	}
	public class Rpt1DCells:System.Collections.CollectionBase
	{
		#region ���췽��
		/// <summary>
		/// ���췽��
		/// </summary>
		public Rpt1DCells()
		{
		}
		/// <summary>
		/// ����һ��Table ���죬���Table ������3���С�
		/// ����˳���������γ�ȵ�˳��һ�¡� 
		/// </summary>
		/// <param name="dt">DataTable</param>
		public Rpt1DCells(DataTable dt)
		{
			this.BindWithDataTable(dt);						
		}
		 
		#endregion

		#region ��������
		/// <summary>
		/// ����һ��Table ���죬���Table ������3���С�
		/// ����˳���������γ�ȵ�˳��һ�¡�
		/// </summary>
		/// <param name="dt">3���е� DataTable</param>
		public void BindWithDataTable(DataTable dt)
		{
			foreach(DataRow dr in dt.Rows)			
				this.Add( new Rpt1DCell( dr[0].ToString(), dr[1] ) );
		}
		/// <summary>
		/// ����һ��Table ���죬���Table ������3���С�
		/// ����˳���������γ�ȵ�˳��һ�¡�
		/// </summary>
		/// <param name="dt">3���е� DataTable</param>
		public void BindWithDataTable(DataTable dt,string url)
		{
			foreach(DataRow dr in dt.Rows)			
				this.Add( new Rpt1DCell( dr[0].ToString(), dr[1] ) );
		}
		 
		/// <summary>
		/// ����һ���µ�Ԫ��
		/// </summary>
		/// <param name="myen">Cellʵ��</param>
		public virtual void Add(Rpt1DCell myen)
		{
			//�ж����ʵ���ǲ��Ǵ���
			foreach(Rpt1DCell en in this)		
			{
				if (en.PK == myen.PK)				 
					return ;
				 
			}
			// �������ʵ�塣
			this.InnerList.Add(myen);
			return;
		}
		/// <summary>
		/// Rpt3DCell
		/// </summary>
		public Rpt1DCell this[int index]
		{
			get
			{	
				return (Rpt1DCell)this.InnerList[index];
			}
		}
		/// <summary>
		/// ͨ��3��ֵȡ������cell.
		/// ���û�о�New һ�����ء�
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <returns>Rpt3DCell</returns>
		public Rpt1DCell GetCell(string pk1)
		{
			foreach(Rpt1DCell en in this)
				if (en.PK == pk1 )
					return en;

			return new Rpt1DCell(pk1,0);
		}
		public decimal GetRate(string pk1)
		{
			decimal sum=0;		 
			foreach(Rpt1DCell en in this)			 
				sum+= en.valOfDecimal;

			if (sum==0)
				return 0;

			decimal pkval= this.GetCell(pk1).valOfDecimal ;
			decimal rate= pkval/sum*100;
			return decimal.Round(rate,2);
		}
		#endregion		 
	}

	/// <summary>
	/// Cell ����
	/// ��Ԫ��
	/// </summary>
	abstract public class RptCell
	{
		/// <summary>
		/// PK1
		/// </summary>
		public string PK1=null;
		/// <summary>
		/// PK2
		/// </summary>
		public string PK2=null;
		/// <summary>
		/// ֵ
		/// </summary>
		public Object val=null;
		public string valOfString
		{
			get
			{
				return val.ToString();
			}
		}
		public int valOfInt
		{
			get
			{
				try
				{
					return int.Parse(val.ToString()) ;
				}
				catch
				{
					return 0;
				}
			}
		}
		public decimal valOfDecimal
		{
			get
			{
				try
				{
					return decimal.Parse(val.ToString()) ;
				}
				catch
				{
					return 0;
				}
			}
		}
		public float valOfFloat
		{
			get
			{
				return float.Parse(val.ToString()) ;
			}
		}
		/// <summary>
		/// ���ӵ�Url��
		/// </summary>
		public string Url="";
		/// <summary>
		/// URl ��ʾ����ʾ��
		/// </summary>
		public string ToolTip="";
		/// <summary>
		/// Ŀ��_top�� _blank �� _parent
		/// </summary>
		public string Target="_self";
		 
	}
	/// <summary>
	///  2 ά��Ԫ��
	///  ʵ��
	/// </summary>
	public class RptPlanarCell :RptCell
	{
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="PK1">PK1</param>
		/// <param name="PK2">PK2</param>
		/// <param name="val">Val</param>
		public RptPlanarCell(string _PK1, string _PK2  ,Object val)
		{
			this.PK1 =_PK1;
			this.PK2 =_PK2;
			this.val = val;
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="PK1">PK1</param>
		/// <param name="PK2">PK2</param>
		/// <param name="val">Val</param>
		/// <param name="_Url">Ҫ���ӵ�URL</param>
		public RptPlanarCell(string _PK1, string _PK2, Object _val, string _Url)
		{
			this.PK1 = _PK1;
			this.PK2 = _PK2;
			this.val = _val;
			this.Url = _Url;
		}	 
	}
	/// <summary>
	///  2 ά��Ԫ��s
	///  ʵ�弯��
	/// </summary>
	public class RptPlanarCells : System.Collections.CollectionBase
	{
		#region ����
		private float _SumOfFloat=-1;
		public float SumOfFloat
		{
			get
			{
				if (_SumOfFloat==-1)
				{
					_SumOfFloat = 0;
					foreach(RptPlanarCell cel in this)
						_SumOfFloat+=cel.valOfFloat;
				}
				return _SumOfFloat;
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// ���췽��
		/// </summary>
		public RptPlanarCells()
		{
		}
		/// <summary>
		/// ����Table����һ�����ϡ�
		/// </summary>
		/// <param name="dt">���DataTableҪ��3�У���1��PK1, ��2��PK2, ��3��Val</param>
		public RptPlanarCells(DataTable dt)
		{
			foreach(DataRow dr in dt.Rows)	
			{
				string pk1=dr[0].ToString();
				string pk2=dr[1].ToString();
				object obj=dr[2];

				this.Add( new RptPlanarCell( pk1,pk2,obj )) ;					 
			}
		}
		/// <summary>
		/// ����Table����һ�����ϡ�
		/// </summary>
		/// <param name="dt">���DataTableҪ��3�У���1��PK1, ��2��PK2, ��3��Val</param>
		public RptPlanarCells(DataTable dt, string cellurl)
		{
			foreach(DataRow dr in dt.Rows)	
			{
				string pk1=dr[0].ToString();
				string pk2=dr[1].ToString();
				object obj=dr[2];

				if (cellurl==null)
				{
					this.Add( new RptPlanarCell( pk1,pk2,obj )) ;	
				}
				else
				{
					this.Add( new RptPlanarCell( pk1,pk2,obj, cellurl )) ;	
				}
			}
		}
		#endregion

		#region ��������
		/// <summary>
		/// ����һ���µ�ʵ�� with url
		/// </summary>
		/// <param name="pk1">PK1</param>
		/// <param name="pk2">PK2</param>
		/// <param name="val">����PK����ֵ</param>
		/// <param name="url">����</param>
		public void Add(string pk1,string pk2,object val,string url)
		{
			RptPlanarCell en = new RptPlanarCell(pk1,pk2,val,url) ;
			this.Add(en);
		}
		/// <summary>
		/// ����һ���µ�ʵ��
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <param name="val">����PK����ֵ</param>
		public void Add(string pk1,string pk2,object val )
		{
			RptPlanarCell en = new RptPlanarCell(pk1,pk2,val);
			this.Add(en);
		}
		/// <summary>
		/// ����һ���µ�Ԫ��
		/// </summary>
		/// <param name="myen">Cellʵ��</param>
		public virtual void Add(RptPlanarCell myen)
		{
			//�ж����ʵ���ǲ��Ǵ���			 
			foreach(RptPlanarCell en in this)
			{
				if (en.PK1 == myen.PK1 && en.PK2 == myen.PK2)
				{				
					en.val =  myen.valOfFloat+myen.valOfFloat ; 					
					return;
				}
			}
			// �������ʵ�塣
			this.InnerList.Add(myen);
			return;
		}
		/// <summary>
		/// ������������
		/// </summary>
		public RptPlanarCell this[int index]
		{
			get
			{	
				return (RptPlanarCell)this.InnerList[index];
			}
		}
		/// <summary>
		/// �õ�һ����Ԫ��
		/// </summary>
		/// <param name="pk1">PK1</param>
		/// <param name="pk2">PK2</param>
		/// <returns>RptPlanarCell</returns>
		public RptPlanarCell GetCell(string pk1, string pk2)
		{
			foreach(RptPlanarCell en in this)
				if (en.PK1 == pk1 && en.PK2 == pk2)
					return en;

			return new RptPlanarCell(pk1,pk2,0);
		}
		/// <summary>
		/// �õ�val.
		/// </summary>
		/// <param name="pk1"></param>
		/// <param name="pk2"></param>
		/// <returns>val</returns>
		public decimal GetVal(string pk1,string pk2)
		{
			return this.GetCell(pk1,pk2).valOfDecimal ;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pk2"></param>
		/// <returns></returns>
		public decimal GetRateByPK2(string pk2)
		{
			decimal sum=this.SumOfDecimal;
			if (sum==0)
				return 0;
			else
				return this.GetSumByPK2(pk2)/sum;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pk2"></param>
		/// <returns></returns>
		public decimal GetRateByPK1(string pk2)
		{
			decimal sum=this.GetSumByPK2(pk2) ; // һ��γ�� == 2 ��
			if (sum==0)
				return 0;
			else
				return this.GetSumByPK1(pk2)/ sum ;
		}
		/// <summary>
		/// �õ�һ��Rate������
		/// </summary>
		/// <param name="pk1">PK1</param>
		/// <param name="pk2">PK2</param>
		/// <returns>RptPlanarCell</returns>
		public decimal GetRateVertical(string pk1, string pk2)
		{
			decimal sum=this.GetSumByPK1(pk1);
			if (sum==0)
				return 0;
			else
				return this.GetVal(pk1,pk2)/sum;
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="pk1"></param>
		/// <param name="pk2"></param>
		/// <returns></returns>
		public decimal GetRateTransverse(string pk1, string pk2)
		{
			decimal sum=this.GetSumByPK2(pk2);
			if (sum==0)
				return 0;
			else
				return this.GetVal(pk1,pk2)/sum;
		}
		private decimal _Sum=-1;
		private int _SumOfInt=-1;

		/// <summary>
		/// �ϼ�
		/// </summary>
		public decimal SumOfDecimal
		{
			get
			{
				if (_Sum==-1)
				{
					_Sum=0 ;
					foreach(RptPlanarCell en in this)
						_Sum+= en.valOfDecimal;
				}
				return _Sum;
			}
		} 
		public int SumOfInt
		{
			get
			{
				if (_SumOfInt==-1)
				{
					_SumOfInt=0 ;
					foreach(RptPlanarCell en in this)
						_SumOfInt+= en.valOfInt;
				}
				return _SumOfInt;
			}
		} 
		/// <summary>
		/// �õ�Sum
		/// </summary>
		/// <param name="pk1">pk</param>
		/// <returns>sum</returns>
		public decimal GetSumByPK1(string pk1)
		{
			decimal dec=0 ;
			foreach(RptPlanarCell en in this)
				if (en.PK1 == pk1)
					dec+= en.valOfDecimal;
			return dec;
		}
		/// <summary>
		/// �õ�Sum
		/// </summary>
		/// <param name="pk1">pk</param>
		/// <returns>sum</returns>
		public decimal GetSumByPK2(string pk2)
		{
			decimal dec=0 ;
			foreach(RptPlanarCell en in this)
				if (en.PK2 == pk2)
					dec+= en.valOfDecimal;
			return dec;
		}
		/// <summary>
		/// �õ�һ����Ԫ���ڵ��ַ���
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <returns>html ���</returns>
		public string GenerHtmlStrBy(string pk1, string pk2 )
		{
			RptPlanarCell en = this.GetCell(pk1,pk2) ;
			if (en.Url=="")
			{
				return en.val.ToString();
			}
			string url=en.Url+"&PK1="+pk1+"&PK2="+pk2;
			return "<A href='javascript:openit('"+url+"')'  >"+float.Parse( en.val.ToString() ).ToString("0")+"</A>";

			//return "<A href=\""+en.Url+"&PK1="+pk1+"&PK2="+pk2+"\" target='_blank'>"+en.val+"</A>";
		}
		/// <summary>
		/// �õ�һ����Ԫ���ڵ��ַ���
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <returns>html ���</returns>
		public string GenerHtmlStrBy(string d1EnsName, string pk1, string d2EnsName, string pk2 , AnalyseDataType adt)
		{
			RptPlanarCell en = this.GetCell(pk1,pk2) ;
			if (en.Url=="")
			{
				return en.val.ToString();
			}

			try
			{
				switch(adt)
				{
					case AnalyseDataType.AppFloat:
						return "<A href=\"javascript:openit('"+en.Url+"&"+d1EnsName+"="+pk1+"&"+d2EnsName+"="+pk2+"')\" >"+float.Parse(en.val.ToString())+"</A>";
					case AnalyseDataType.AppInt:
						return "<A href=\"javascript:openit('"+en.Url+"&"+d1EnsName+"="+pk1+"&"+d2EnsName+"="+pk2+"')\" >"+int.Parse(en.val.ToString())+"</A>";
					case AnalyseDataType.AppMoney:
						return "<A href=\"javascript:openit('"+en.Url+"&"+d1EnsName+"="+pk1+"&"+d2EnsName+"="+pk2+"')\" >"+float.Parse(en.val.ToString()).ToString("0.00")+"</A>";
					default:
						throw new Exception("adt ���ʹ���.");
				}
			}
			catch 
			{
				return "0";
			}
		}
		#endregion
	}
	
	

	
}
