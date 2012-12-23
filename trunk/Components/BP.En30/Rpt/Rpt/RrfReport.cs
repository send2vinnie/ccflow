using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;
using BP.En;
using BP.DA;


//
namespace BP.Report
{
	/// <summary>
	/// ���ݴ�ӡ���� 
	/// </summary>
	public class RrfReport
	{
		#region ���캯��
		public RrfReport()
		{
			BaseDir =  AppDomain.CurrentDomain.BaseDirectory;
            CyclostyleDir = "DataUser/CyclostyleFile";
			DirectoryInfo dir = Directory.CreateDirectory( BaseDir + CyclostyleDir);
			TempDir       = "Temp";
			dir = Directory.CreateDirectory( BaseDir + TempDir);

			_encoder = System.Text.Encoding.GetEncoding("GB2312") ;
		}
		#endregion


		#region ���ԣ�·��

		/// <summary>
		/// ��Ŀ¼
		/// </summary>
		public readonly string BaseDir ;
		/// <summary>
		/// ģ��Ŀ¼
		/// </summary>
		public readonly string CyclostyleDir ;
		/// <summary>
		/// ��ʱ�ļ�Ŀ¼
		/// </summary>
		public readonly string TempDir ="Temp";

		private  string _cyclostyleFilePath = "" ;
		/// <summary>
		/// ģ��·��
		/// </summary>
		public  string CyclostyleFilePath 
		{
			get
			{
				return _cyclostyleFilePath;
			}
		}
		private  string _tempFilePath = "" ;
		/// <summary>
		/// ��ʱ�ļ�·��
		/// </summary>
		public  string TempFilePath 
		{
			get
			{
				return _tempFilePath;
			}
		}
		
		private string _fileName ="";
		/// <summary>
		/// ������·�����ļ���
		/// </summary>
		public string FileName
		{
			get
			{
				return _fileName;
			}
		}
		
		#endregion ���ԣ�·��

		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="ens">Ҫ��������ļ���</param>
		/// <param name="cyclostyfilename">�����ļ����ơ�</param>
		public static void GenerRpt(Entities ens, string cyclostyfilename)
		{
			BP.WF.WebRtfReport rpt = new BP.WF.WebRtfReport();
			rpt.LoadCyclostyle( cyclostyfilename , DA.DBAccess.GenerOID()+".doc" );
			rpt.BindEnInEns( ens );
			rpt.Close();
			string url=   System.Web.HttpContext.Current.Request.ApplicationPath+"/"+ rpt.TempDir +"/"+ rpt.FileName ; 
			PubClass.WinOpen(url);
			return;
			//PubClass.OpenWordDoc(url,rpt.FileName);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="ens">Ҫ��������ļ���</param>
		/// <param name="cyclostyfilename">�����ļ����ơ�</param>
		public static void GenerRpt(Entity en, string cyclostyfilename)
		{
			BP.WF.WebRtfReport rpt = new BP.WF.WebRtfReport();
			rpt.LoadCyclostyle( cyclostyfilename , DA.DBAccess.GenerOID()+".Doc" );
			rpt.BindEn( en ); 
			rpt.Close();
			string url=   System.Web.HttpContext.Current.Request.ApplicationPath+"/"+ rpt.TempDir +"/"+ rpt.FileName ; 
			
			PubClass.WinOpen(url);
			return;
		//	PubClass.OpenWordDoc(url,rpt.FileName);
		}

		#region �ĵ����ر���ر�
		/// <summary>
		/// ����Rtfģ��
		/// </summary>
		/// <param name="cyclostyfilename">ģ���ļ���</param>
		/// <param name="saveFileName">��ʱ�ļ���</param>
		public void LoadCyclostyle( string cyclostyfilename ,string saveFileName)
		{
			this._cyclostyleFilePath = BaseDir+CyclostyleDir+"/" + cyclostyfilename.TrimStart('\\','/').ToUpper().Replace(".RTF","") +".RTF";
			this._fileName           = saveFileName.TrimStart('\\','/').ToUpper().Replace(".RTF","") +".RTF";
			this._tempFilePath       = BaseDir+TempDir+"/"       +  _fileName;
			//System.IO.File.Copy( this.CyclostyleFilePath , this.TempFilePath );
			try
			{
				ReadDocParamTable();
			}
			catch(Exception ex)
			{
				throw new Exception("��������д���� "+ ex.Message + _cyclostyleFilePath );
			}
		}
		public DocParams DocParamTable = null;
		
		/// <summary>
		/// װ�ز�����
		/// </summary>
		private void ReadDocParamTable()
		{
			string xlmpath = this.CyclostyleFilePath.ToUpper().Replace(".RTF" ,".XML");
			DataSet ds = new DataSet("ds");
			ds.ReadXml( xlmpath);
			DataTable tb = ds.Tables[0];
			this.DocParamTable = new DocParams( tb ); //����datatable ���ɲ������ϡ�

			this.ListDT = new Hashtable(); // ��ϸ������
			this.ListEnsNames = new Hashtable(); // �����б�
			foreach(DocParam par in this.DocParamTable)
			{
				int pos = par.Field.IndexOf('.');
				if( pos!=-1)
				{
					string clas = par.Field.Substring(0,pos);
					if(ListEnsNames.ContainsKey( clas)==false)
					{
						ListEnsNames.Add( clas ,clas); // ���������б�
					}
					if(par.Key.TrimStart('<').Substring(0,2)=="DT")
					{
						/*  dtl */
						if(this.ContainsDT( clas)==false)  // ������ϸ������
						{
							this.ListDT.Add( clas ,0);
						}
					}
				}
			}
			ds.Dispose();
		}
		public  Hashtable ListEnsNames;
		public  Hashtable ListDT;
		public  bool ContainsClass(string enName)
		{
			return this.ListEnsNames.ContainsKey( enName);
		}
		public  bool ContainsDT(string ensName)
		{
			return this.ListDT.ContainsKey( ensName);
		}
		/// <summary>
		/// ȡ�ò���
		/// </summary>
		/// <param name="enName">������</param>
		/// <returns>��������</returns>
		public DocParams GetDocParams(string enName)
		{
			DocParams pars = new DocParams();
			foreach(DocParam par in this.DocParamTable)
			{
				if(par.Field.IndexOf(enName)!=-1)
				{
					pars.Add( par);
				}
			}
			return pars;
		}
		/// <summary>
		/// �����ĵ���
		/// </summary>
		public  void MakeDocument()
		{
			System.IO.File.Copy( this.CyclostyleFilePath , this.TempFilePath ); //����һ����������ʱĿ¼���档

			StreamReader read = null;
			StreamWriter wr = null;
			try
			{
				read= new StreamReader( this.TempFilePath ,Encoding.ASCII); // �ļ���
				string str = read.ReadToEnd();  //��ȡ��ϡ�
				read.Close(); // �رա�
				wr = new StreamWriter( this.TempFilePath ,false,Encoding.ASCII);

				str= this.RepairLine(   str ); // �޸��ߡ�

				#region �ӱ�
				IDictionaryEnumerator dic= this.ListDT.GetEnumerator();
				 

				while(dic.MoveNext())
				{
					string ensName = dic.Key.ToString(); //DT
					DocParams pars = this.GetDocParams( ensName);
					int rowCount = (int)dic.Value;
					if(pars.Count>0)
					{
						string rowKey  = pars[0].Key.TrimStart('<').Substring(0,4); //find dt row model
						int pos_rowKey = str.IndexOf( rowKey);
						int row_start=-1 ,row_end=-1 ;
						if(pos_rowKey!=-1)
						{
							row_start = str.Substring(0,pos_rowKey).LastIndexOf("\\row");
							row_end = str.Substring(pos_rowKey).IndexOf("\\row");
						}
						if(row_start!=-1 && row_end!=-1 )
						{
							string row = str.Substring(row_start ,(pos_rowKey-row_start)+row_end);
							str = str.Replace( row,"");
							for(int ir=rowCount;ir>0;ir--)
							{
								string tmp = row;
								pars = this.GetDocParams(ensName+ir);
								foreach(DocParam par in pars)
								{
									string key ;
									if(par.Key[par.Key.Length-1]=='>')
										key= par.Key.Remove(par.Key.Length-4,3);
									else
										key= par.Key.Remove(par.Key.Length-3,3);
									if(key.Length>3) //��ȫ���
										tmp = tmp.Replace( key , par.Value );
								}
								str = str.Insert(row_start,tmp);
							}
						}
					}
				}
				#endregion �ӱ�

				#region ����
				foreach( DocParam par in this.DocParamTable )
				{
					str = str.Replace( par.Key , par.Value );
				}
				#endregion

				wr.Write( str ); 
			}
			catch( Exception ex)
			{
				if( read!=null)
					read.Close();
				if( wr!=null)
					wr.Close();
				throw new Exception( "�����ĵ�ʧ�ܣ�"+ex.Message); 
			}
			read.Close();
			wr.Close();
		}
		
		
		public  void Close()
		{
			this.MakeDocument();
			this.ListEnsNames.Clear();
			this.ListDT.Clear();
			this.DocParamTable.Clear();
		}

		#endregion �ĵ����ر���ر�

		#region �ؼ�����
		private System.Text.Encoding _encoder;
		/// <summary>
		/// ��string ת��rpt ����ʶ���
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string GetCode(string str)
		{
			if( str =="")
				return str;
			string rtn = "";
			byte[] rr = _encoder.GetBytes( str );
			foreach( byte b in rr )
			{
				if( b>122)
					rtn += "\\'" + b.ToString("x");
				else
					rtn += (char)b;
			}
			return rtn ;
		}
		/// <summary>
		/// �޸���
		/// </summary>
		/// <param name="line"></param>
		public string RepairLine(string line)//str
		{
			int start = line.IndexOf("<");
			int end   = line.IndexOf(">");
			while(start!=-1 && end>start )
			{
				#region ���� <parm> �е�����
				string pro_old = "";
				pro_old = line.Substring(start,end-start );

				string str_new = pro_old.Replace("{","");
				str_new = str_new.Replace("}","");
				if(str_new.Length>0)
				{
					int _xie_start = 0;   //��б�ܿ�ʼ����λ��
					int _xie=-1,_space=-1;//��б��λ�ã��ո�λ��
					_xie = str_new.IndexOf( '\\',_xie_start);
					while(_xie!=-1)
					{
						#region ���� \���� ����
						if(str_new.Length>(_xie+1) && str_new[_xie+1]=='\'')
						{
							_xie_start = _xie+1;
							_xie = str_new.IndexOf( '\\' ,_xie_start);
							continue;
						}
						_space = str_new.IndexOf(" ",_xie);
						if(_space!=-1)
						{
							str_new = str_new.Replace(str_new.Substring(_xie,_space-_xie),"");
						}
						_xie = str_new.IndexOf( '\\',_xie_start);
						#endregion ���� \���� ����
					}
					str_new = str_new.Replace(" ","");
				}
				line = line.Replace(pro_old ,str_new);
				start = end -(pro_old.Length-str_new.Length)+1;
				end=-1;
				if( line.Length>start)
				{
					end   = line.IndexOf(">" ,start);
					start = line.IndexOf("<" ,start);
				}
				#endregion ���� <parm> �е�����
			}
			return line;

		}


		#endregion 


		#region �����滻
		/// <summary>
		///  SetValueByField
		/// </summary>
		/// <param name="field">�ֶ���</param>
		/// <param name="val">Ҫ�滻��ֵ</param>
		public void SetValueByField( string field , string val)
		{
			DocParam par = this.DocParamTable.GetDocParamByField( field);

			if(par!=null )
			{
				if( par.NeedConvert ) 
					par.Value = this.GetCode( val );
				else
					par.Value =  val ;
			}
		}
		/// <summary>
		/// SetValueByField
		/// </summary>
		/// <param name="field"></param>
		/// <param name="val"></param>
		/// <param name="convertCode"></param>
		public void SetValueByField( string field , string val , bool convertCode)
		{
			DocParam par = this.DocParamTable.GetDocParamByField( field);
			if(par!=null )
			{
				if( convertCode ) 
					par.Value = this.GetCode( val );
				else
					par.Value =  val ;
			}
		}

		#endregion �����滻

	
	}

}
