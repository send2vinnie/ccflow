using System;
using System.Collections;
using BP.Report;
using BP.Port;
using BP.En;
using System.Text;
using System.IO;
using System.Data;

namespace BP.Rpt
{
	/// <summary>
	/// BPRpt ��ժҪ˵����
	/// </summary>
	public class Rtf
	{
		public Rtf()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public string CyclostyleFilePath =null;
		public string TempFilePath =null;
		public string FileName =null;
		/// <summary>
		/// ����Rtfģ��
		/// </summary>
		/// <param name="cyclostyfilename">ģ���ļ���</param>
		/// <param name="saveFileName">��ʱ�ļ���</param>
		public void LoadCyclostyle( string cyclostyfilename ,string saveFileName)
		{
			this.FileName           = saveFileName.TrimStart('\\','/').ToUpper().Replace(".RTF","") +".RTF";
			this.TempFilePath       = BP.SystemConfig.PathOfWebApp+"\\Temp\\" + FileName;


			this.CyclostyleFilePath =BP.SystemConfig.PathOfDataUser+"/CyclostyleFile/" + cyclostyfilename.TrimStart('\\','/').ToUpper().Replace(".RTF","") +".RTF";
		}
		/// <summary>
		/// ����dataens.
		/// </summary>
		public Entities DataEns= new Emps();
		 
		#region make docs
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
		/// <summary>
		/// ���� �ļ�
		/// </summary>
		public void MakeDocument()
		{
			System.IO.File.Copy( this.CyclostyleFilePath , this.TempFilePath ); //����һ����������ʱĿ¼���档
			StreamReader read = null;
			StreamWriter wr = null;
			try
			{
				read= new StreamReader( this.TempFilePath ,Encoding.ASCII); // �ļ���
				string str = read.ReadToEnd();  //��ȡ��ϡ�
				read.Close(); // �رա�
				//wr = new StreamWriter( this.TempFilePath ,false,Encoding.ASCII);
				//wr = new StreamWriter( this.TempFilePath ,false,Encoding.ASCII.ToString() );

				wr = new StreamWriter( this.TempFilePath ,false ,Encoding.ASCII );
				str= this.RepairLine(   str ); // �޸��ߡ�

				

				#region ����
				 
				char[] chars = str.ToCharArray();
				string para="";
				foreach(char c in chars)
				{
					if (c=='>')
					{
						try
						{
							/* ��������󣬾Ϳ�ʼִ���滻*/
							//str=str.Replace(" ","");
							str=str.Replace( "<"+para+">", this.GetCode( this.GetValueByKey(para) ) ) ;
						}
						catch(Exception ex)
						{
							throw new Exception("ȡ����["+para+"]���ִ���������������´˴���;1����Textȡֵʱ�䣬�����Բ��������2,���޴����ԡ�<br>����ϸ����Ϣ��<br>"+ex.Message);
						}
					}

					if (c=='<')
						para=""; // ��������� '<' ��ʼ��¼
					else
					{
						if (c.ToString()=="" )
							continue;

						para+=c.ToString();
					}
				}
				#endregion

				#region �ӱ�
				/*
				IDictionaryEnumerator dic= this.ListDT.GetEnumerator();
				while(dic.MoveNext())
				{
					string ensName = dic.Key.ToString();//DT
					DocParams pars = this.GetDocParams( ensName);
					int rowCount = (int)dic.Value;
					if(pars.Count>0)
					{
						string rowKey  = pars[0].Key.TrimStart('<').Substring(0,4); //find dt row model
						int pos_rowKey = str.IndexOf( rowKey);
						int row_start=-1 ,row_end=-1 ;
						if(pos_rowKey!=-1)
						{//0   start   50  end  100
							row_start = str.Substring(0,pos_rowKey).LastIndexOf("\\row");
							row_end = str.Substring(pos_rowKey).IndexOf("\\row");
						}
						if(row_start!=-1 &&row_end!=-1)
						{
							string row = str.Substring(row_start ,(pos_rowKey-row_start)+row_end);
							str = str.Replace( row,"");
							//if(rowCount==0)
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
				*/
				#endregion �ӱ�


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

		private System.Text.Encoding _encoder= System.Text.Encoding.GetEncoding("GB2312") ;
		//	_encoder =  ;


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
		/// ��˽ڵ�ı�ʾ������ C.�ڵ�ID.Attr.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetValueByKey(string key)
		{
			key=key.Replace(" ","");
			key=key.Replace("\r\n","");

			string[] strs=key.Split('.');
			if (strs[0]=="C")
			{
				/* �������˽ڵ� */
				return this.GetValueByKeyOfCheckNode( strs );
			}
			foreach(Entity en in this.DataEns)
			{
				 
				if ( key.IndexOf( en.GetType().Name ) < 0 )
					continue;
				 
				/*˵����������ֶ���*/
				if (strs.Length==1)
					throw new Exception("�������ô���strs.length=1 ��"+key);
				if (strs.Length==2)
					return en.GetValStringByKey(strs[1].Trim() );

				if (strs.Length==3)
				{
					string val=en.GetValStringByKey(strs[1].Trim() );
					switch( strs[2].Trim() )
					{
						case "Text":
							if ( val=="0")
								return "��";
							else
								return "��";
						case "Year":
							return val.Substring(0,4);
						case "Month":
							return val.Substring(5,2);
						case "Day":
							return val.Substring(8,2);
						case "NYR":
							return DA.DataType.ParseSysDate2DateTime(val).ToString("yyyy��MM��dd��");
						case "RMB":
							return float.Parse(val).ToString("0.00");
						case "RMBDX":
							return DA.DataType.ParseFloatToCash( float.Parse(val)) ;
						default:
							throw new Exception("�������ô������ⷽʽȡֵ����"+key);
					}
				}
			} // end for .
			throw new Exception("�������ô��� GetValueByKey ��"+key);
		}
		/// <summary>
		/// GetValueByKeyOfCheckNode
		///  case S.9001002.Rec
		/// S.9001002.RDT.Year
		/// </summary>
		/// <param name="key"></param>
		public string GetValueByKeyOfCheckNode(string[] strs )
		{
			foreach(Entity en in this.DataEns)
			{
				if (en.ToString()=="BP.WF.NumCheck" || en.ToString()=="BP.WF.GECheckStand" || en.ToString()=="BP.WF.NoteWork"  )
				{
					if (en.GetValStringByKey("NodeID")==strs[1])
					{
					}
					else
						continue;
				}
				else
				{
					continue;
				}

				string val=en.GetValStringByKey(strs[2]);

				switch(strs.Length)
				{
					case 1:
					case 2:
						throw new Exception("step1�������ô���"+strs.ToString());
					case 3: // S.9001002.Rec
						return val;
					case 4: // S.9001002.RDT.Year
					switch(strs[3])
					{
						case "Text":
							if ( val=="0")
								return "��";
							else
								return "��";
						case "Year":
							return val.Substring(0,4);
						case "Month":
							return val.Substring(5,2);
						case "Day":
							return val.Substring(8,2);
						case "NYR":
							return DA.DataType.ParseSysDate2DateTime(val).ToString("yyyy��MM��dd��");
						case "RMB":
							return float.Parse(val).ToString("0.00");
						case "RMBDX":
							return DA.DataType.ParseFloatToCash( float.Parse(val)) ;
						default:
							throw new Exception("step2�������ô���"+strs );
					}
					default:
						throw new Exception("step3�������ô���"+strs );
				}
			} 

			throw new Exception("step4�������ô���"+strs  );
		}
		#endregion make doc.
 
	}
}
