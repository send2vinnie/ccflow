using System;
using System.Reflection;
using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En;


namespace BP.Report
{
	/// <summary>
	/// 3D������ʽ
	/// </summary>
	public enum Rpt2DStyle
	{
		/// <summary>
		/// �������ʾD1,D2,�ұ߶�����ʾͳ����ֵ����
		/// </summary>
		LeftD1D2,
		/// <summary>
		/// �������ʾͳ����ֵ����,������ʾD1,D2
		/// </summary>
		TopD1D2,
		/// <summary>
		/// �������ʾD1,������ʾD2
		/// </summary>
		LeftD1,
		/// <summary>
		/// �������ʾD2,������ʾD1
		/// </summary>
		LeftD2
	}
	/// <summary>
	/// 2D����ʵ�壬�й�˵����ο�RptBase
	/// </summary>
	public class Rpt2DEntity : RptBase
	{
		public  Rpt2DEntity()
		{
		}
		private Rpt2DStyle _style = Rpt2DStyle.LeftD1;
		/// <summary>
		/// ������ʽ
		/// </summary>
		public  Rpt2DStyle Style 
		{
			get{ return _style;}
			set{ _style =value;}
		}


		#region Get HTML 
		/// <summary>
		/// ��ȡHtml����
		/// </summary>
		public override string GetHtmlCode()
		{
			switch( this.Style )
			{
				case Rpt2DStyle.LeftD1D2:
					return GetHtmlCode_LeftD1D2();

				case Rpt2DStyle.LeftD1:
					return GetHtmlCode_LeftD1();

				case Rpt2DStyle.LeftD2:
					return GetHtmlCode_LeftD2();

				case Rpt2DStyle.TopD1D2:
					return GetHtmlCode_TopD1D2();

				default:
					throw new Exception("����Style�����쳣��");
			}
		}
		public string GetHtmlCode_LeftD2()
		{
			this.SB.Remove(0,this.SB.Length);

			this.RWTitle(this.Title );
			//this.RWBR();
			this.RW("<div align='center'>") ; 
			this.RW("<table "+ this.RptTableStyle +" >");

			this.RW("<tr>");
			this.RWTH( this.TitleD2 ) ;
			
			foreach(Dimension en1 in this.D1Ens  ) 
			{
				this.RWTH( this.GetD1Content(en1)  ) ;
			}			 
			this.RW("</tr>");

			foreach(Dimension en2 in this.D2Ens)
			{
				this.RW("<tr>");
				this.RWTD( this.GetD2Content(en2) );
				foreach(Dimension en1 in this.D1Ens)		
				{
					this.RWTD( this.Get2DCellContent( en1.No,en1.Name ,en2.No,en2.Name));
				}
				this.RW("</tr>");
			}

			this.RW("</table>");
			this.RWCenter(this.Author+"&nbsp;&nbsp;&nbsp;&nbsp;"+this.ReportDate);
			this.RW("</div>");

			return this.SB.ToString();
		}
		
		public string GetHtmlCode_LeftD1()
		{
			this.SB.Remove(0,this.SB.Length);
			
			this.RWTitle(this.Title );
			//this.RWBR();
			this.RW("<div align='center'>") ; 
			this.RW("<table "+ this.RptTableStyle +" >");

			this.RW("<tr>");
			this.RWTH( this.TitleD1 ) ;
			foreach(Dimension en2 in this.D2Ens  ) 
			{
				this.RWTH( this.GetD2Content(en2)  ) ;
			}
			this.RW("</tr>");

			foreach(Dimension en1 in this.D1Ens ) 
			{
				this.RW("<tr>");
				this.RWTD( this.GetD1Content(en1) );
				 
				foreach(Dimension en2 in this.D2Ens)		
				{
					this.RWTD( this.Get2DCellContent( en1.No,en1.Name ,en2.No,en2.Name));
				}
				this.RW("</tr>");
			}

			this.RW("</table>");
			this.RWCenter(this.Author+"&nbsp;&nbsp;&nbsp;&nbsp;"+this.ReportDate);
			this.RW("</div>");

			return this.SB.ToString();
		}
		public string GetHtmlCode_TopD1D2()
		{
			this.SB.Remove(0,this.SB.Length);

			this.RWTitle(this.Title );
			//this.RWBR();
			this.RW("<div align='center'>") ; 
			this.RW("<table "+ this.RptTableStyle +" >");

			this.RW("<TR>");
			this.RW("<TH bgcolor='#C0C0C0'  rowspan='2' nowrap align='center'>"+ this.TitleVal0 +"</TH>");
			string tr2 = "";
			string tr3__ = "";
			foreach(Dimension en1 in this.D1Ens )
			{
				Dimensions ds2 = this.GetD2EnsBy1FK( en1.No);
				if (ds2.Count == 0)
					continue;
				this.RW("<TH bgcolor='#C0C0C0' colspan='" +ds2.Count+ "' nowrap>"+  this.GetD1Content(en1)  +"</TH>" ) ;

				foreach(Dimension en2 in ds2 ) 
				{
					tr2 +="<td nowrap align=center>" + this.GetD2Content(en2,en1) + "</td>";
					string cell = this.Get2DCellContent(en1.No,en1.Name ,en2.No,en2.Name);
					tr3__ +="<td nowrap align=center>" + cell + "</td>";
				}
			}
			this.RW("</TR>");

			this.RW("<TR>");
			this.RW( tr2 );
			this.RW("</TR>");

			this.RW("<TR>");
			this.RW("<td nowrap align=center>" + this.TitleVal0 + "</td>");
			this.RW( tr3__ );
			this.RW("</TR>");

			this.RW("</table>");
			this.RWCenter(this.Author+"&nbsp;&nbsp;&nbsp;&nbsp;"+this.ReportDate);
			this.RW("</div>");

			return this.SB.ToString();
		}
		public string GetHtmlCode_LeftD1D2()
		{
			this.SB.Remove(0,this.SB.Length);

			this.RWTitle(this.Title );
			//this.RWBR();
			this.RW("<div align='center'>") ; 
			this.RW("<table "+ this.RptTableStyle +" >");

			this.RW("<TR>");
			this.RW("<TH bgcolor='#C0C0C0'  colspan='2' nowrap align='center'>����</TH>");
			this.RW("<TH bgcolor='#C0C0C0' rowspan='2' nowrap>"+  this.TitleVal0  +"</TH>" ) ;
			this.RW("</TR>");

			this.RW("<TR>");
			this.RWTH( this.TitleD1 ) ;
			this.RWTH( this.TitleD2 ) ;
			this.RW("</TR>");

			#region ��������

			foreach(Dimension en1 in this.D1Ens  ) 
			{
				Dimensions ds2 = this.GetD2EnsBy1FK( en1.No);
				if (ds2.Count == 0)
					continue;

				this.RW("<TR>");

				this.RWTD( this.GetD1Content(en1) , ds2.Count );
				bool isFirst=true;
				foreach(Dimension en2 in ds2 )
				{
					this.RWTD( this.GetD2Content(en2,en1));
					this.RWTDCenter( this.Get2DCellContent(en1.No,en1.Name ,en2.No,en2.Name));

					if (isFirst)
					{
						this.RW("</TR>");
						isFirst=false;
					}
					else
					{
						this.RW("<TR>");
					}
				}
				this.RW("</TR>");
			}
			 
			#endregion
 
			this.RW("</table>");
			this.RWCenter(this.Author+"&nbsp;&nbsp;&nbsp;&nbsp;"+this.ReportDate);
			this.RW("</div>");

			return this.SB.ToString();
		}



		#endregion Get HTML

		#region �󶨼���ȡ��Ԫ������

		private Rpt2DCells _dataCells2D = new Rpt2DCells();
		/// <summary>
		/// ��ȡ2D����Դ
		/// </summary>
		public  Rpt2DCells DataCells2D
		{
			get{ return _dataCells2D;}
		}
		public override void BindDataCells( object d2Cells ,string url ,string target)
		{
			this.CellUrl    = url;
			this.CellTarget = target;
			this._dataCells2D = (Rpt2DCells)d2Cells;
		}

		public void BindDataByDV3( DataView dvWith3Cols )
		{
			BindDataByDV3( dvWith3Cols ,"","");
		}
		public void BindDataByDV3( DataView dvWith3Cols ,string url ,string target)
		{
			this.CellUrl    = url;
			this.CellTarget = target;
			foreach(DataRowView row in dvWith3Cols)
			{
				this.DataCells2D.Add( row[0].ToString().Trim()
					,row[1].ToString().Trim()
					,row[2]
					,url
					,target);
			}
		}
		

		public void BindDataByDV5( DataView dvWith5Cols ,bool fill3DData ,string url ,string target)
		{
			this.CellUrl    = url;
			this.CellTarget = target;
			if( fill3DData )
			{
				this.D1Ens.Clear();
				this.D2Ens.Clear();
				this.DataCells2D.Clear();

				foreach(DataRowView row in dvWith5Cols)
				{
					this.DataCells2D.Add( row[0].ToString().Trim()
						,row[2].ToString().Trim()
						,row[4]
						,url
						,target);

					this.D1Ens.Add(row[0].ToString().Trim()
						,row[1].ToString().Trim()
						,""
						,true);
				
					Dimension d2 = new Dimension();
					if( this.D2RefD1)
						d2.FK=row[0].ToString().Trim();
					else 
						d2.FK="";
					d2.No=row[2].ToString().Trim();
					d2.Name=row[3].ToString().Trim();
					this.D2Ens.Add( d2 ,true);
				}
			}
			else
			{
				foreach(DataRowView row in dvWith5Cols)
				{
					this.DataCells2D.Add( row[0].ToString().Trim()
						,row[2].ToString().Trim()
						,row[4]
						,url
						,target);
				}
			}
		}


		public object GetCellValue(string pk1, string pk2)
		{
			Rpt2DCell cell = this.DataCells2D.Get2DCell(pk1,pk2);
			if( cell==null)
				return null;
			else
				return cell.Value;
		}
		public float  GetCellValueToFloat(string pk1, string pk2)
		{
			object val = GetCellValue(pk1,pk2);
			if( val==null||val.Equals(DBNull.Value)||val.ToString().Trim()=="")
				return 0;
			else
			{
				return float.Parse( val.ToString());
			}
		}
		public string Get2DCellContent(string pk1,string name1, string pk2,string name2)
		{
			Rpt2DCell cell = this.DataCells2D.Get2DCell(pk1,pk2);
			if (cell==null||cell.Value==null||cell.Value.Equals(DBNull.Value))
				return "-";
			else if(cell.Url=="" 
				|| float.Parse( cell.Value.ToString())==0
				|| pk1=="sum"||pk2=="sum"
				)
				return cell.Value.ToString();
			else
				return "<a href='" + cell.Url
					+"?"+ this.D1EnsName + "="+cell.PK1
					+"&"+ this.D2EnsName + "="+cell.PK2
					+ CellAddHref
					+"' title='"+name1+","+name2+"'"
					+" Target='"+ cell.Target + "'>"
					+cell.Value+"</a>";
		}

		#endregion

		#region ���ӹ���
		/// <summary>
		/// ��ά��2���кϼ�
		/// </summary>
		/// <param name="sumTitle">�ϼ����ϵı���</param>
		public void AddSumD2(string sumTitle)
		{
			#region ���Ӻϼ�
			foreach(Dimension en1 in this.D1Ens  ) 
			{
				float sum = 0;
				Dimensions ds2 = this.GetD2EnsBy1FK( en1.No);
				if(ds2.Count<2)
					continue;
				foreach(Dimension en2 in ds2)
				{
					Rpt2DCell cell = this.DataCells2D.Get2DCell(en1.No,en2.No);
					if (cell==null||cell.Value==null||cell.Value.Equals(DBNull.Value))
					{}
					else if(float.Parse( cell.Value.ToString())==0)
					{}
					else
					{
						sum += float.Parse( cell.Value.ToString());
					}
				}
				if(this.D2RefD1)
					this.D2Ens.Add("sum",sumTitle,en1.No,true);
				else
					this.D2Ens.Add("sum",sumTitle,"",true);
				this.DataCells2D.Add( en1.No,"sum",sum,this.CellUrl,this.CellTarget);
			
			}
			#endregion ���Ӻϼ�
		}
		/// <summary>
		/// ��ά��1���кϼ�
		/// </summary>
		/// <param name="sumTitle">�ϼ����ϵı���</param>
		public void AddSumD1(string sumTitle)
		{
			#region ���Ӻϼ�
			this.D1Ens.Add("sum",sumTitle,"",true);
			foreach(Dimension en2 in this.D2Ens  ) 
			{
				float sum = 0;

				foreach(Dimension en1 in this.D1Ens)
				{
					Rpt2DCell cell = this.DataCells2D.Get2DCell(en1.No,en2.No);
					if (cell==null||cell.Value==null||cell.Value.Equals(DBNull.Value))
					{}
					else if(float.Parse( cell.Value.ToString())==0)
					{}
					else
					{
						sum += float.Parse( cell.Value.ToString());
					}
				}			
				this.DataCells2D.Add( "sum",en2.No,sum,this.CellUrl,this.CellTarget);
			}

			#endregion ���Ӻϼ�
		}
		#endregion ���ӹ���
	
	}

}