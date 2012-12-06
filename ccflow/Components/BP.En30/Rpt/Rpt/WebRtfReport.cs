using System;
using System.Collections;
using BP.Report;
using BP.Port;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// WebRtfReport ��ժҪ˵����
	/// </summary>
	public class WebRtfReport :RrfReport 
	{
		public WebRtfReport()
		{
		}
	
		#region ˽��
		private void SetConstValueByMap( Map map , string fieldWithoutAttr ,string val )
		{
			foreach( Attr att in map.Attrs)
			{
				string field = fieldWithoutAttr +"."+ att.Key ;
				this.SetValueByField( field , val ,false );//
			}
		}
		private void SetValueByEn( Entity en , string fieldWithoutAttr )
		{
//			if(!this.ContainsClass( en.GetType().Name) 
//				&& !this.ContainsClass( fieldWithoutAttr) )
//				return;
			
			Map map = en.EnMap;
			foreach( Attr att in map.Attrs)
			{
				string field = fieldWithoutAttr +"."+ att.Key ;
				string val = en.GetValStringByKey( att.Key );

				switch( att.MyDataType )
				{
					case BP.DA.DataType.AppDate:
					case BP.DA.DataType.AppDateTime:
						if(val.Length>=4)
							this.SetValueByField( field +".Year",val.Substring( 0,4),false );
						if(val.Length>=7)
							this.SetValueByField( field +".Month",val.Substring( 5,2),false);
						if(val.Length>=10)
							this.SetValueByField( field +".Day",val.Substring( 8,2),false);
						this.SetValueByField( field , val ,false);
						break;
					case BP.DA.DataType.AppDouble:
					case BP.DA.DataType.AppFloat:
					case BP.DA.DataType.AppInt:
					case BP.DA.DataType.AppMoney:
						this.SetValueByField( field , val ,false);
						break;
					default:
						this.SetValueByField( field , val );
						break;
				}
			}
		}
		private void SetValueByEns( Entities ens , int maxReplace)
		{
			string ensName = ens.GetType().Name;
			if(this.ContainsClass(ensName)==false)
				return;

			if( ens.Count ==0)
			{
				Entity en = ens.GetNewEntity;
				for( int i=0 ;i<maxReplace ;i++)
				{
					this.SetConstValueByMap( en.EnMap , ensName+i , "");
				}
				return ;
			}
			else
				for( int i=1 ;i<=maxReplace ;i++)
				{
					if(i>ens.Count)
					{
						this.SetConstValueByMap( ens[0].EnMap , ensName+i , "");
					}
					else
					{
						this.SetValueByEn( ens[i-1] ,ensName +i );
					}
				}
		}
		#endregion ˽��
		
		#region ����

		/// <summary>
		/// �� en ������
		/// </summary>
		public void BindEn( params Entity[] enArr )
		{
			if( enArr == null)
				return;
			foreach( Entity en in enArr)
			{
				if(en==null)
					return;

				string enName = en.GetType().Name;
				

				string clName=en.ToString();



				//GECheckStand schk = en as GECheckStand;  // ǿ��ת��Ϊ��׼��ˡ�

				if( clName == "BP.WF.GECheckStand" )
				{
					enName = "S" + en.GetValStringByKey("NodeID");  // ��׼���ID.
				}
				else
				{
					if( clName == "BP.WF.NumCheck" )
					{
						enName = "S" + en.GetValStringByKey("NodeID") ; // ������ˡ�
					}
					else 
					{
						if( clName == "BP.WF.NoteWork" )
						{
							enName = "NoteWork" + en.GetValStringByKey("NodeID") ; // ��ע�ڵ㡣��
						}
						else
						{
							this.BindDtlsOfEn( en ); // add dtl.
						}
					}
				}


				this.SetValueByEn( en ,enName );
			}
		}
		/// <summary>
		/// ��ens�е�en������
		/// </summary>
		public void BindEnInEns(Entities enSet )
		{
			foreach( Entity en in enSet)
			{
				this.BindEn( en );
			}
		}

		/// <summary>
		/// �󶨴ӱ�
		/// </summary>
		public void BindEns(Entities ens )
		{
			int maxReplace = ens.Count;
			this.BindEns( ens ,maxReplace);
		}
		public void BindEns( Entities ens ,int maxReplace)
		{
			if( ens==null )
				return;

			this.SetValueByEns( ens , maxReplace );
		}

		#endregion ����


		#region �ӱ�
		/// <summary>
		/// BindDtl
		/// </summary>
		/// <param name="ens"></param>
		public void BindDtl( Entities ens )
		{
			string clas = ens.GetType().Name;
			if(!this.ContainsClass(clas))
				return;
			DocParams pars = this.GetDocParams( clas );
			if( pars.Count>0 && this.ContainsDT(clas ) )
			{
				this.ListDT[clas] = ens.Count;
				foreach(DocParam par in pars)
				{
					for(int ie=0;ie<ens.Count;ie++)
					{
						DocParam addpar = new DocParam();
						addpar.Key   = par.Key + (ie+1).ToString().PadLeft(3,'0');
						addpar.Field = par.Field.Insert(par.Field.IndexOf('.'),(ie+1).ToString());
						addpar.Value = "";
						this.DocParamTable.Add( addpar);
					}
					if(ens.Count>0)
						this.DocParamTable.Remove(par);
				}
			}
			this.BindEns( ens );
		}
		/// <summary>
		/// �󶨴ӱ�����enslist
		/// </summary>
		public void BindDtlsInList( ArrayList enslist )
		{
			if( enslist==null || enslist.Count<=0)
				return;

			foreach( object oens in enslist)
			{
				Entities ens = oens as Entities;
				if( ens !=null )
				{
					BindDtl( ens );
				}
			}
		}
		/// <summary>
		/// ��en�����дӱ�
		/// </summary>
		public void BindDtlsOfEn( Entity en )
		{
			ArrayList enslist = en.GetDtlsDatasOfArrayList();
			this.BindDtlsInList( enslist );
		}

		#endregion �ӱ�

	}
}
