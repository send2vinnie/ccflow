using System;
using System.Data;
using System.Collections;
using BP.DA;

namespace BP.Rpt.Doc
{
	/// <summary>
	/// ά��Ԫ��
	/// </summary>
	public class Dimension 
	{
		public Dimension()
		{
		}
		/// <summary>
		/// ����ά��Ԫ��
		/// </summary>
		/// <param name="no">����</param>
		/// <param name="name">����</param>
		/// <param name="fk">�����û������""</param>
		public Dimension(string no ,string name ,string fk)
		{
			this.No = no;
			this.Name = name;
			this.FK = fk;
		}
		private string _no = "";
		/// <summary>
		/// ����
		/// </summary>
		public string No
		{
			get{ return _no;}
			set{ _no =value;}
		}
		private string _name = "";
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get{ return _name;}
			set{ _name =value;}
		}
		private string _fk = "";
		/// <summary>
		/// ���
		/// </summary>
		public string FK
		{
			get{ return (_fk==null)?"":_fk;}
			set{ _fk =value;}
		}
	}

	/// <summary>
	/// ά��Ԫ�ؼ��ϣ����ڱ�����ĳһά�ϵ�Ԫ��
	/// </summary>
	public class Dimensions :CollectionBase
	{
 
		#region ��������
		public  void BindDataCols3(DataView dv)
		{
			this.Clear();
			foreach(DataRowView dr in dv)	
			{
				this.Add( dr[0].ToString().Trim()
					,dr[1].ToString().Trim()
					,dr[2].ToString().Trim()
					,true) ; 
			}
		}
		public  void BindDataCols2(DataView dv)
		{
			this.Clear();
			foreach(DataRowView dr in dv)	
			{
				this.Add( dr[0].ToString().Trim()
					,dr[1].ToString().Trim()
					,""
					,true) ; 
			}
		}
		public  void BindDataEns(CollectionBase ens 
			,string noKey
			,string nameKey
			,string fkKey)
		{
			this.Clear();
			foreach(object en in ens)
			{
				if(fkKey==null||fkKey=="")
				{
					this.Add( ClassFactory.GetValueToStr(en,noKey)
						,ClassFactory.GetValueToStr(en,nameKey)
						,""
						,true) ; 
				}
				else
				{
					this.Add( ClassFactory.GetValueToStr(en,noKey)
						,ClassFactory.GetValueToStr(en,nameKey)
						,ClassFactory.GetValueToStr(en,fkKey)
						,true) ; 
				}
			}
		}


		public bool ContainsDim( Dimension dim,bool ignoreCase)
		{
			return ContainsDim( dim.No ,dim.FK ,ignoreCase);
		}
		public bool ContainsDim(string no ,string fk ,bool ignoreCase)
		{
			foreach(Dimension dim in this)
			{
				if (string.Compare(dim.No,no,ignoreCase)==0 &&string.Compare(dim.FK,fk,ignoreCase)==0)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// ͨ�����뼰�����ȡά��Ԫ��
		/// </summary>
		/// <param name="no">����</param>
		/// <param name="fk">�����û�����ʱΪ""</param>
		/// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
		/// <returns>����ά��Ԫ��</returns>
		public Dimension GetDim(string no ,string fk ,bool ignoreCase)
		{
			foreach(Dimension dim in this)		
			{
				if (string.Compare(dim.No,no,ignoreCase)==0 &&string.Compare(dim.FK,fk,ignoreCase)==0)
				{
					return dim;
				}
			}
			return null;
		}


		/// <summary>
		/// ���ά��Ԫ��
		/// </summary>
		/// <param name="no">����</param>
		/// <param name="name">����</param>
		/// <param name="fk">�����""��ʾ��</param>
		/// <param name="ignoreCase">Ϊtrueʱ��ʾ���벻���ִ�Сд</param>
		/// <returns>��Ӳ��ɹ���ʾ�Ѿ�������ͬ��Ԫ�أ������������Ϊ������</returns>
		public bool Add(string no,string name,string fk ,bool ignoreCase)
		{
			if( this.ContainsDim( no ,fk ,ignoreCase))
				return false;
			else
			{
				Dimension dim = new Dimension(no ,name ,fk);
				this.InnerList.Add(dim);
				return true;
			}
		}
		/// <summary>
		/// ���ά��Ԫ��
		/// </summary>
		/// <param name="add">ά��Ԫ��</param>
		/// <param name="ignoreCase">Ϊtrueʱ��ʾ���벻���ִ�Сд</param>
		/// <returns>��Ӳ��ɹ���ʾ�Ѿ�������ͬ��Ԫ�أ������������Ϊ������</returns>
		public bool Add(Dimension add ,bool ignoreCase)
		{
			if( this.ContainsDim(add , ignoreCase))
				return false;
			else
			{
				this.InnerList.Add(add);
				return true;
			}
		}


		public Dimension this[int index]
		{
			get
			{	
				return this.InnerList[index] as Dimension;
			}
		}


		public string GenerHtmlStrBy(string pk1, string pk2 )
		{
			return "";
		}


		/// <summary>
		/// �޸�ά�ȼ����е�����Ԫ�ص����ƣ�ȥ�������п�ʼλ�õ�ĳЩ�ַ����ο� string.TrimStart(chars)����ȥ����01�·ݡ��еġ�0��
		/// </summary>
		/// <param name="chars">Ҫȥ�����ַ�</param>
		public void TrimStart(params char[] chars)
		{
			foreach(Dimension dim in this)
			{
				dim.Name = dim.Name.TrimStart(chars);
			}
		}

		/// <summary>
		/// �޸�ά�ȼ����е�����Ԫ�ص����ƣ�ȥ�������н���λ�õ�ĳЩ�ַ����ο� string.TrimEnd(chars)
		/// </summary>
		/// <param name="chars">Ҫȥ�����ַ�</param>
		public void TrimEnd(params char[] chars)
		{
			foreach(Dimension dim in this)
			{
				dim.Name = dim.Name.TrimEnd(chars);
			}
		}
		/// <summary>
		/// �޸�ά�ȼ����е�ÿһ��Ԫ�ص�����,ȥ����ʼ�����λ�õ��ַ����ο� string.Trim()
		/// </summary>
		/// <param name="chars">Ҫȥ�����ַ�</param>
		public void Trim(params char[] chars)
		{
			foreach(Dimension dim in this)
			{
				dim.Name = dim.Name.Trim(chars);
			}
		}
		/// <summary>
		/// ���ָ����ȵ��·�
		/// </summary>
		/// <param name="Year4">��ţ�����Ϊyyyy��ʽ</param>
		public void FillMonth( string Year4)
		{
			this.Clear();
			for(int i=1; i<=12;i++)
			{
				string no = Year4 +"-"+ i.ToString().PadLeft(2,'0');
				this.Add(no,i.ToString()+"�·�","",true);
			}
		}

		/// <summary>
		/// ���ָ������·ݵ�����
		/// </summary>
		/// <param name="Year4">��ţ�����Ϊyyyy��ʽ</param>
		public void FillDay( int year,int month)
		{
			this.Clear();
			int days = System.DateTime.DaysInMonth( year ,month);
			string yyyyMM = year.ToString() +"-"+ month.ToString().PadLeft(2,'0');
			for(int i=1;i<=days;i++)
			{
				string no = yyyyMM +"-"+ i.ToString().PadLeft(2,'0');
				string name = i.ToString()+"��";
				this.Add( no ,name ,"" ,true);
			}
		}


		/// <summary>
		/// �������״̬
		/// </summary>
		public void FillBreedState()
		{
			this.Clear();
			BP.Sys.SysEnums ses =new BP.Sys.SysEnums("WFState");
			foreach(BP.Sys.SysEnum se in ses)
			{
				this.Add(se.IntKey.ToString() ,se.Lab ,"",true);
			}
		}
		/// <summary>
		/// ��伾��
		/// </summary>
		public void FillQuarter()
		{
			this.Clear();
			BP.Sys.SysEnums ses =new BP.Sys.SysEnums("Quarter");
			foreach(BP.Sys.SysEnum se in ses)
			{
				this.Add(se.IntKey.ToString() ,se.Lab ,"",true);
			}
		}



		#endregion 
	}
}
