using System;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En; 
using BP.DA;
namespace BP.Rpt
{
	/// <summary>
	/// ��άʵ��
	/// </summary>
	public class Rpt3DCell : RptCell
	{
		#region ����
		/// <summary>
		/// 3 γ����
		/// </summary>
		public string PK3=null;
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����һ��3άʵ��
		/// </summary>
		/// <param name="PK1">pk1</param>
		/// <param name="PK2">pk2</param>
		/// <param name="PK3">pk3</param>
		/// <param name="val">3��PK����ֵ</param>
		public Rpt3DCell(string PK1, string PK2, string PK3 ,Object _val)
		{
			this.PK1 = PK1;
			this.PK2 = PK2;
			this.PK3 = PK3;
			this.val = _val;
		}
		/// <summary>
		/// ����һ��3άʵ��
		/// </summary>
		/// <param name="PK1">pk1</param>
		/// <param name="PK2">pk2</param>
		/// <param name="PK3">pk3</param>
		/// <param name="url">Ҫ���ӵ���Ŀ��</param>
		/// <param name="val">3��PK����ֵ</param>
		/// <param name="_Target">Ŀ��</param>
		public Rpt3DCell(string PK1, string PK2, string PK3 ,Object _val, string url, string _Target )
		{
			this.PK1 = PK1;
			this.PK2 = PK2;
			this.PK3 = PK3;
			this.val = _val;
			this.Url= url;
			this.Target = _Target;
		}	
		#endregion
		 
	}
	/// <summary>
	/// 3γ������
	/// </summary>
	public class Rpt3DCells : System.Collections.CollectionBase
	{
		#region sum it .
		private float _sum=-1;
		/// <summary>
		/// �ܼ�
		/// </summary>
		public float Sum
		{
			get
			{
				if (_sum==-1)
				{
					_sum =0;
					foreach(Rpt3DCell cell in this)
					{
						_sum+=cell.valOfFloat;
					}
				}
				return _sum;
			}
		}
		/// <summary>
		/// ����Pk1,ȡ������sum.
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <returns>�ϼ�</returns>
		public float GetSumByPK1(string pk1)
		{
			float x= 0;
			foreach(RptCell cell in this)
			{
				if (cell.PK1==pk1)				 
					x+=float.Parse(cell.val.ToString());
			}
			return x;
		}
		/// <summary>
		/// ����Pk1,pk2 ȡ������sum.
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <returns>�ϼ�</returns>
		public float GetSumByPK1(string pk1,string pk2)
		{
			float x= 0;
			foreach(RptCell cell in this)
			{
				if (cell.PK1==pk1 && cell.PK2==pk2 )
					x+=float.Parse(cell.val.ToString());
			}
			return x;
		}
		#endregion
		
		#region ���췽��
		/// <summary>
		/// ���췽��
		/// </summary>
		public Rpt3DCells()
		{
		}
		/// <summary>
		/// ����һ��Table ���죬���Table ������3���С�
		/// ����˳���������γ�ȵ�˳��һ�¡� 
		/// </summary>
		/// <param name="dt">DataTable</param>
		public Rpt3DCells(DataTable dt)
		{
			this.BindWithDataTable(dt);						
		}
		/// <summary>
		/// ����һ��Table ���죬���Table ������3���С�
		/// ����˳���������γ�ȵ�˳��һ�¡�
		/// </summary>
		/// <param name="dt">3���е� DataTable</param>
		/// <param name="url">����</param>
		public Rpt3DCells(DataTable dt,string url)
		{
			this.BindWithDataTable(dt,url,"");						
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
				this.Add( new Rpt3DCell(dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),dr[3] )) ; 
		}
		/// <summary>
		/// ����һ��Table ���죬���Table ������3����, ����˳����pk1, pk2,pk3.
		/// ����˳���������γ�ȵ�˳��һ�¡�
		/// </summary>
		/// <param name="dt">3���е� DataTable</param>
		/// <param name="url">����</param>
		/// <param name="target">���ӵ� _self , _blank</param>
		public void BindWithDataTable(DataTable dt, string url, string target)
		{
			foreach(DataRow dr in dt.Rows)			
				this.Add( new Rpt3DCell(dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),dr[3],url,target )) ; 
		}
		/// <summary>
		/// ����һ���µ�Ԫ��
		/// </summary>
		/// <param name="myen">Cellʵ��</param>
		public virtual void Add(Rpt3DCell myen)
		{
			//�ж����ʵ���ǲ��Ǵ���
			foreach(Rpt3DCell en in this)		
			{
				if (en.PK1 == myen.PK1 && en.PK2 == myen.PK2 && en.PK3 == myen.PK3)				
				{
					try
					{
						en.val =  myen.valOfFloat+ myen.valOfFloat ; 
					}
					catch
					{
					}
					return;
				}
			}
			// �������ʵ�塣
			this.InnerList.Add(myen);
			return;
		}
		/// <summary>
		/// Rpt3DCell
		/// </summary>
		public Rpt3DCell this[int index]
		{
			get
			{	
				return (Rpt3DCell)this.InnerList[index];
			}
		}
		/// <summary>
		/// ͨ��3��ֵȡ������cell.
		/// ���û�о�New һ�����ء�
		/// </summary>
		/// <param name="pk1">pk1</param>
		/// <param name="pk2">pk2</param>
		/// <param name="pk3">pk3</param>
		/// <returns>Rpt3DCell</returns>
		public Rpt3DCell GetCell(string pk1, string pk2, string pk3)
		{
			foreach(Rpt3DCell en in this)
				if (en.PK1 == pk1 && en.PK2 == pk2 && en.PK3 == pk3)
					return en;

			return new Rpt3DCell(pk1,pk2,pk3,0);
		}
		#endregion 
	}
}
