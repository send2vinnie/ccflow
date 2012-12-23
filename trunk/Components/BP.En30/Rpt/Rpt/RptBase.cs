using System;
using System.Reflection;
using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;


namespace BP.Report
{
	/// <summary>
	/// ����ʵ��Ļ��࣬����������2ά�ģ����Ի�������ά��1��ά��2
	/// ά������ʱ���̳л���Ȼ�����ά�ȣ����ά����ط��������HTML����ķ�������
	/// </summary>
	public class RptBase
	{
		public RptBase()
		{
			SB = new StringBuilder();
		}
		private string _rptTableStyle =" border=1 cellspacing=0 style='font-size: 9.5pt' cellpadding=0 bordercolordark='#FFFFFF' bordercolorlight='#000000'";
		/// <summary>
		/// �����ʽ
		/// </summary>
		public  string RptTableStyle
		{
			get{ return _rptTableStyle;}
			set{ _rptTableStyle =value;}
		}

		#region  �������� �������

		private string _title="BP����[�����]";
		/// <summary>
		/// �����
		/// </summary>
		public  string Title
		{
			get{ return _title; }
			set{ _title =value; }
		}
		
		private string _titleD1="D1����";
		/// <summary>
		/// ά��1�ı���
		/// </summary>
		public string TitleD1
		{
			get{ return _titleD1; }
			set{ _titleD1 =value; }
		}

		private string _titleD2="D2����";
		/// <summary>
		/// ά��2�ı���
		/// </summary>
		public string TitleD2
		{
			get{ return _titleD2; }
			set{ _titleD2 =value; }
		}
		
		private string _titleVal0="V0��";//"V0����";
		/// <summary>
		/// ͳ����ֵ����
		/// </summary>
		public string TitleVal0
		{
			get{ return _titleVal0; }
			set{ _titleVal0 =value; }
		}

		private string _author = "";//BP.Web.WebUser.Name ;
		/// <summary>
		/// ������
		/// </summary>
		public string Author
		{
			get{ return _author; }
			set{ _author =value; }
		}
		
		private string _reportDate = DateTime.Now.ToString("yyyy��MM��dd��") ; 
		/// <summary>
		/// ��������
		/// </summary>
		public string ReportDate
		{
			get{ return _reportDate; }
			set{ _reportDate =value; }
		}
		#endregion


		#region  ά�ȼ����ݰ�

		private Dimensions _d1Ens=null;
		/// <summary>
		/// ά��1��Ԫ�ؼ���
		/// </summary>
		public  Dimensions D1Ens
		{
			get{ return _d1Ens;}
		}
		private string _d1EnsName ="EnsName";
		/// <summary>
		/// ά��1�������ƣ�������ҳ�洫����ʱ�õ�
		/// </summary>
		public  string D1EnsName 
		{
			get{ return _d1EnsName;}
		}
		private string _d1AddHref ="";
		/// <summary>
		/// ά��1��������ҳ�洫����ʱ�ĸ������ݣ���ҪΪ�˴����ⲿ����
		/// </summary>
		public  string D1AddHref 
		{
			get{ return _d1AddHref;}
			set{ _d1AddHref =value;}
		}
		private string _d1URL ="";// System.Web.HttpContext.Current.Request.ApplicationPath+"/Rpt/DimensionRefLink.aspx";
		/// <summary>
		/// ά��1������
		/// </summary>
		public  string D1URL 
		{
			get{ return _d1URL;}
			set
			{
				_d1URL =value;
			}
		}
		private string _d1Target ="_d1Target";
		/// <summary>
		/// ά��1�����ӵ�Ŀ�괰��
		/// </summary>
		public  string D1Target 
		{
			get{ return _d1Target;}
			set{ _d1Target =value;}
		}

		private Dimensions _d2Ens=null;
		/// <summary>
		/// ά��2��Ԫ�ؼ���
		/// </summary>
		public  Dimensions D2Ens
		{
			get{ return _d2Ens;}
		}
		private bool _d2RefD1 =false;
		/// <summary>
		/// ά��2�Ƿ������ά��1
		/// </summary>
		public  bool D2RefD1 
		{
			get{ return _d2RefD1;}
			set{ _d2RefD1 =value;}
		}
		private string _d2EnsName ="EnsName";
		/// <summary>
		/// ά��2�������ƣ�������ҳ�洫����ʱ�õ�
		/// </summary>
		public  string D2EnsName 
		{
			get{ return _d2EnsName;}
		}
		private string _d2AddHref ="";
		/// <summary>
		/// ά��2��������ҳ�洫����ʱ�ĸ������ݣ���ҪΪ�˴����ⲿ����
		/// </summary>
		public  string D2AddHref 
		{
			get{ return _d2AddHref;}
			set{ _d2AddHref =value;}
		}
		private string _d2URL = "";//System.Web.HttpContext.Current.Request.ApplicationPath+"/Rpt/DimensionRefLink.aspx";
		/// <summary>
		/// ά��2������
		/// </summary>
		public  string D2URL 
		{
			get{ return _d2URL;}
			set{ _d2URL =value;}
		}
		private string _d2Target ="_d2Target";
		/// <summary>
		/// ά��2�����ӵ�Ŀ�괰��
		/// </summary>
		public  string D2Target 
		{
			get{ return _d2Target;}
			set{ _d2Target =value;}
		}
		
		/// <summary>
		/// ����ά��1
		/// </summary>
		/// <param name="titleD1">ά��1�ı���</param>
		/// <param name="d1Ens">ά��1��Ԫ�ؼ��ϣ�Ϊnullʱ���Զ������յļ���</param>
		/// <param name="d1EnsName">ά��1��������</param>
		public void BindD1(string titleD1 ,Dimensions d1Ens ,string d1EnsName)
		{
			this._titleD1     = titleD1;
			if(d1Ens==null)
				this._d1Ens = new Dimensions();
			else
				this._d1Ens = d1Ens;
			this._d1EnsName = d1EnsName;
		}
		/// <summary>
		/// ��ά��1��Ԫ�ؼ������������Ԫ��
		/// </summary>
		/// <param name="d1">ά��Ԫ��</param>
		/// <returns>�Ƿ���ӳɹ������ɹ����ʾ�Ѿ����ڼ�ֵ��ͬ��Ԫ��</returns>
		public bool D1AddDim(Dimension d1)
		{
			return this.D1Ens.Add( d1 ,true);
		}
		
		/// <summary>
		/// ����ά��2
		/// </summary>
		/// <param name="titleD2">ά��2�ı���</param>
		/// <param name="d2Ens">ά��2��Ԫ�ؼ��ϣ�Ϊnullʱ���Զ������յļ���</param>
		/// <param name="d2RefD1">ά��2�Ƿ������ά��1</param>
		/// <param name="d2EnsName">ά��2��������</param>
		public void BindD2(string titleD2 ,Dimensions d2Ens ,bool d2RefD1 ,string d2EnsName)
		{
			this._titleD2  = titleD2;
			
			if(d2Ens==null)
				this._d2Ens = new Dimensions();
			else
				this._d2Ens = d2Ens;

			this._d2RefD1 = d2RefD1;
			this._d2EnsName = d2EnsName;
		}
		/// <summary>
		/// ��ά��2��Ԫ�ؼ������������Ԫ��
		/// </summary>
		/// <param name="d2">ά��Ԫ��</param>
		/// <returns>�Ƿ���ӳɹ������ɹ����ʾ�Ѿ����ڼ�ֵ��ͬ��Ԫ��</returns>
		public bool D2AddDim(Dimension d2)
		{
			return this.D2Ens.Add( d2 ,true);
		}


		#endregion
	
		#region ��ȡά������

		/// <summary>
		/// ��ȡά��1��HTML����
		/// </summary>
		/// <param name="en1">ά��Ԫ��</param>
		/// <returns>��װ��en1��HTML����</returns>
		public string GetD1Content(Dimension en1)
		{
			if (this.D1URL.Trim()!="" && en1.No!="sum")
				return "<a href='"+this.D1URL
					+"?EnsName="+ this.D1EnsName+"&Val="+en1.No
					+ this.D1AddHref +"'"
					+" Title='"+ en1.Name+"["+en1.No+"]'"
					+" Target='"+ this.D1Target +"'>"+ en1.Name +"</a>";
			else
				return en1.Name;
		}


		/// <summary>
		/// ��ȡά��2��HTML����
		/// </summary>
		/// <param name="en2">ά��2��Ԫ��</param>
		/// <returns>HTML����</returns>
		public string GetD2Content(Dimension en2 )
		{
			return GetD2Content( en2,null);
		}
		/// <summary>
		/// ��ȡά��2��HTML���ݣ����ݿ��ܰ���ά��1
		/// </summary>
		/// <param name="en2">ά��2��Ԫ��</param>
		/// <param name="en1">ά��1��Ԫ�أ�en1��Ϊ��ʱ������ͬʱ��װ����ά��1</param>
		/// <returns>HTML����</returns>
		public string GetD2Content(Dimension en2 ,Dimension en1)
		{
			if (this.D2URL.Trim()!="" && en2.No!="sum")
			{
				string href = "<a href='"+this.D2URL;
				href +="?EnsName=" + this.D2EnsName+ "&Val=" + en2.No;
				if( en1!=null)
					href +="&RefD1EnsName="+ this.D1EnsName + "&RefD1Val=" +en1.No;
				href += this.D2AddHref +"' ";
				href +=" Title='";
				if( en1!=null)
					href += en1.Name+"["+en1.No+"]"+",";
				href +=en2.Name+"["+en2.No+"]" +"'";
				href +=" Target='"+ this.D2Target +"'>"+ en2.Name + "</a>";
				return href;
			}
			else
				return en2.Name;
		}

		/// <summary>
		/// ͨ��ĳά��1Ԫ�صļ�ֵ����ά��2�����л�ȡ���ϣ���ά��2��������ά��1ʱ����������ά��2����
		/// </summary>
		/// <param name="en1No">��ֵ����ά��1Ԫ�صı���</param>
		/// <returns></returns>
		public Dimensions GetD2EnsBy1FK(string en1No)
		{
			if(!this.D2RefD1)//�ǹ���ʱ��������������
			{
				return this.D2Ens ;
			}
			else//�ǹ���ʱ���Ƚϼ�ֵ�ɹ�����뷵�ؼ���
			{
				#region
				Dimensions refds = new Dimensions();
				foreach(Dimension en2 in this.D2Ens)
				{
					if (en2 != null && en2.FK==en1No)
					{
						refds.Add( en2 ,true);
					}
				}
				#endregion
				return refds;
			}
		}

		
		#endregion


		#region �󶨼���ȡ��Ԫ������

		private string _celUrl="";
		/// <summary>
		/// ���ݸ������
		/// </summary>
		public string CellUrl
		{
			get
			{
				return _celUrl;
			}
			set
			{
				this._celUrl = value;
			}
		}
		private string _cellTarget="_self";
		/// <summary>
		/// ���ݸ�����ӵ�Ŀ�괰��
		/// </summary>
		public string CellTarget
		{
			get
			{
				return _cellTarget;
			}
			set
			{
				this._cellTarget = value;
			}
		}

		private string _cellAddHref="";
		/// <summary>
		/// ���ݸ���������ҳ�洫����ʱ�ĸ������ݣ���ҪΪ�˴����ⲿ����
		/// </summary>
		public  string CellAddHref
		{
			get{return _cellAddHref;}
			set{_cellAddHref =value;}
		}
		public virtual void BindDataCells( object cells ,string url ,string target)
		{
		}
		#endregion 


		#region ��װΪHTML����
		
		/// <summary>
		/// ����ַ�
		/// </summary>
		/// <param name="str"></param>
		public void RW(string str)
		{
			this.WriterHtmlText( str);
		}
		/// <summary>
		/// �����Ԫ��
		/// </summary>
		/// <param name="str"></param>  
		public void RWTD(string str)
		{
			this.WriterHtmlText( "<TD nowrap >"+str+"</TD>");
		}
		/// <summary>
		/// �����Ԫ��
		/// </summary>
		/// <param name="str"></param>  
		public void RWTDCenter(string str)
		{
			this.WriterHtmlText( "<TD nowrap align=center>"+str+"</TD>");
		}
		/// <summary>
		/// �����Ԫ��
		/// </summary>
		/// <param name="str"></param>  
		public void RWTD(string str, int rowspan)
		{
			this.WriterHtmlText("<TD rowspan='"+rowspan.ToString()+"' >"+str+"</TD>");
		}
		/// <summary>
		/// ������ⵥԪ��
		/// </summary>
		/// <param name="str"></param>
		public void RWTH(string str)
		{
			this.WriterHtmlText("<TH nowrap bgcolor='#C0C0C0' >"+str+"</TH>");
		}
		/// <summary>
		/// ������м�
		/// </summary>
		/// <param name="str"></param>
		public void RWCenter(string str)
		{
			this.WriterHtmlText("<p align='center'> "+str+"</P>");
		}
		/// <summary>
		/// ������м䲢�� Blod
		/// </summary>
		/// <param name="str"></param>
		public void RWCenterAndB(string str)
		{
			this.WriterHtmlText("<p align='center'> <b>"+str+"</b></P>");
		}
		/// <summary>
		/// ������һ���
		/// </summary>
		/// <param name="str"></param>
		public void RWLine(string str)
		{
			this.WriterHtmlText("<br>"+str);
		}
		/// <summary>
		/// ���BR
		/// </summary>
		public void RWBR()
		{
			this.WriterHtmlText("<br>");
		}
		/// <summary>
		/// ����ո�
		/// </summary>
		/// <param name="i">����ո����</param>
		public void RWBlank(int i )
		{	      
			for( int ii = 0 ; ii< i; ii++ )
			{
				this.RWBlank();
			}
		}
		public void RWBlank()
		{
			this.WriterHtmlText("&nbsp;");
		}

		public void RWAlignAndB(string str ,string align)
		{
			this.WriterHtmlText("<p align='"+ align +"'> <b>"+str+"</b></P>");
		}
		public void RWTitle(string str )
		{
			this.WriterHtmlText("<div align='center'><b><font face='����' size='4'>"+str+"</font></b></div>");
		}
		public void RWRedError(string str,string align)
		{
			this.WriterHtmlText("<p align='"+ align +"'> <b><font face='����' color='red'>"+str+"</font></b></P>");
		}

		public void RWLineBold0(string str)
		{
			this.RW("<div align='center'>") ;
			this.WriterHtmlText("<b>"+str+"</b>");
			this.RW("</div>") ; 
		}
		public void WriterHtmlText( string text)
		{
			this.SB.Append( text );
		}

		protected readonly StringBuilder SB;

		#endregion

		#region Get HTML 
		/// <summary>
		/// ��ȡHtml����
		/// </summary>
		public virtual string GetHtmlCode()
		{
			return this.SB.ToString();
		}

		#endregion 
	}


}
