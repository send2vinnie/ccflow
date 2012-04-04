using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
	public enum MailState
	{
		/// <summary>
		/// ��ʼ��
		/// </summary>
		Init,
		/// <summary>
		/// �ݸ�
		/// </summary>
		Draft,
		/// <summary>
		/// �ѷ���
		/// </summary>
		Send
	}
	/// <summary>
	/// �ʼ�����
	/// </summary>
	public class MailAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// ��λ
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// ����
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// 
		/// </summary>
		public const string MailState="MailState";
		/// <summary>
		/// �Ӱ���
		/// </summary>
		public const string Accpter="Accpter";
		/// <summary>
		/// AccptStation
		/// </summary>
		public const string AccptStation="AccptStation";
		public const string AccptDept="AccptDept";
		/// <summary>
		/// ������
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// ��¼ʱ��
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// �Ķ�ʱ��
		/// </summary>
		public const string ReadDateTime="ReadDateTime";
		/// <summary>
		/// PIR
		/// </summary>
		public const string PRI="PRI";
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public const string Attachments="Attachments";
	}
	/// <summary>
	/// �ʼ�
	/// </summary> 
	public class Mail : EntityOID
	{
		#region ��������
		/// <summary>
		/// MailState
		/// </summary>
		public MailState MailState 
		{
			get
			{
				return (MailState)this.GetValIntByKey(MailAttr.MailState);
			}
			set
			{
				SetValByKey(MailAttr.MailState,(int)value);
			}
		}
		/// <summary>
		/// RDT
		/// </summary>
		public string RDT 
		{
			get
			{
				return this.GetValStringByKey(MailAttr.RDT);
			}
			set
			{
				SetValByKey(MailAttr.RDT,value);
			}
		}
		public string Sender
		{
			get
			{
				return this.GetValStringByKey(MailAttr.Sender); 
			}
			set
			{
				SetValByKey(MailAttr.Sender,value);
			}
		}
		public string SenderText
		{
			get
			{
				return this.GetValRefTextByKey(MailAttr.Sender); 
			}
		}
		public string Accpter
		{
			get
			{
				return this.GetValStringByKey(MailAttr.Accpter); 
			}
			set
			{
				SetValByKey(MailAttr.Accpter,value);
			}
		}
		/// <summary>
		/// ���ܸ�λ
		/// </summary>
		public string AccptStation
		{
			get
			{
				return this.GetValStringByKey(MailAttr.AccptStation); 
			}
			set
			{
				SetValByKey(MailAttr.AccptStation,value);
			}
		}
		public string AccptDept
		{
			get
			{
				return this.GetValStringByKey(MailAttr.AccptDept); 
			}
			set
			{
				SetValByKey(MailAttr.AccptDept,value);
			}
		}
		public string Title
		{
			get
			{
				return this.GetValStringByKey(MailAttr.Title); 
			}
			set
			{
				SetValByKey(MailAttr.Title,value);
			}
		}
		public int PRI
		{
			get
			{
				return this.GetValIntByKey(MailAttr.PRI); 
			}
			set
			{
				SetValByKey(MailAttr.PRI,value);
			}
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(MailAttr.Doc); 
			}
			set
			{
				SetValByKey(MailAttr.Doc,value);
			}
		}
		public int Attachments
		{
			get
			{
				return this.GetValIntByKey(MailAttr.Attachments); 
			}
			set
			{
				SetValByKey(MailAttr.Attachments,value);
			}
		}
		#endregion
 
		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}
		/// <summary>
		/// �ʼ�
		/// </summary>
		public Mail()
		{
		  
		}
		/// <summary>
		/// �ʼ�
		/// </summary>
		/// <param name="_No">No</param>
		public Mail(int oid):base(oid)
		{
		}
      
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_Mail");
				map.EnDesc="�ѷ����ʼ�";
				map.Icon="../TA/Images/Mail_s.ico";
				map.AddTBIntPKOID();
				map.AddTBString(MailAttr.Title,null,"����",true,false,0,300,10);
				map.AddTBString(MailAttr.Doc,null,"����",true,false,0,4000,10);
				map.AddDDLEntities(MailAttr.Sender,null,"������",new Emps(),false);
				map.AddTBString(MailAttr.Accpter,null,"������",true,false,0,3000,10);
				map.AddTBString(MailAttr.AccptStation,null,"���ܸ�λ",true,false,0,3000,10);
				map.AddTBString(MailAttr.AccptDept,null,"���ܲ���",true,false,0,3000,10);

				map.AddTBString(MailAttr.RDT,DataType.CurrentDataTimeCN,"��¼ʱ��",true,false,0,50,10);

                map.AddDDLSysEnum(MailAttr.PRI, 0, "�����̶�", false, true, MailAttr.PRI,"@0=��@1=��@2=��");
                map.AddDDLSysEnum(MailAttr.MailState, 0, "״̬", false, true, "MailState", "@0=��ʼ��@1=�ѷ���@2=δ����");

				map.AddTBInt(MailAttr.Attachments,0,"��������",true,false);

				map.AddSearchAttr(MailAttr.MailState);
				//map.AddSearchAttr(MailAttr.Accpter);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region mail
		protected override bool beforeUpdate()
		{
			string sql="SELECT COUNT(*) FROM  Sys_FileManager WHERE RefKey='"+this.OID+"' AND RefTable='"+this.ToString()+"' ";
			this.Attachments = DBAccess.RunSQLReturnValInt(sql);
			return base.beforeUpdate ();
		}

		public static Mail GetInitMail()
		{
			Mail mail = new Mail();
			QueryObject qo = new QueryObject(mail);
			qo.AddWhere(MailAttr.MailState,(int)MailState.Init);
			qo.addAnd();
			qo.AddWhere(MailAttr.Sender,WebUser.No);
			if (qo.DoQuery()==0)
			{
				mail.Sender=WebUser.No;
				mail.Insert();
			}
			return mail;
		}
		#endregion

		#region mail 
		/// <summary>
		/// δ�Ķ�����
		/// </summary>
		public static int NumOfUnRead
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_MailDtl WHERE Accpter='"+WebUser.No+"' AND MailDtlState="+(int)MailDtlState.UnRead;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int NumOfRead
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_MailDtl WHERE Accpter='"+WebUser.No+"' AND MailDtlState="+(int)MailDtlState.Read;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int NumOfDelete
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_MailDtl WHERE Accpter='"+WebUser.No+"' AND MailDtlState="+(int)MailDtlState.Delete ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int NumOfSend
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_Mail WHERE Sender='"+WebUser.No+"' AND MailState="+(int)MailState.Send ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int NumOfDraft
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_Mail WHERE Sender='"+WebUser.No+"' AND MailState="+(int)MailState.Draft ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		#endregion
	}
	/// <summary>
	/// �ʼ�s
	/// </summary> 
	public class Mails: Entities
	{
		/// <summary>
		/// ִ�з���
		/// </summary>
		/// <param name="emps"></param>
		/// <param name="title"></param>
		/// <param name="docs"></param>
		public  static void DoSend(Emps emps,Mail mail )
		{
			string strs="@";
			foreach(Emp emp in emps)
			{

				if (strs.IndexOf( "@"+emp.No+"@" ) != -1 )
					continue;
				strs+=emp.No+"@";
				DoSend( emp.No, mail );
			}
		}
		/// <summary>
		/// ִ�з���
		/// </summary>
		/// <param name="emps"></param>
		/// <param name="title"></param>
		/// <param name="docs"></param>
		public  static void DoSend(string[] emps, Mail mail )
		{
			string strs="@";
			int i = 0;
			foreach(string emp in emps)
			{
				if (strs.IndexOf( "@"+emp+"@" ) != -1 )
					continue;
				strs+=emp+"@";

				if (emp=="," || emp.Trim().Length==0  )
					continue;

				DoSend(emp, mail);
				i++;
			}

			if (i==0)
				throw new Exception("������ԱΪ�ա�");
		}
		/// <summary>
		/// ִ�з���
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="title"></param>
		/// <param name="docs"></param>
		public  static void DoSend(string emp, Mail mail )
		{
			if (emp==null || emp.Length==0)
				return;

			MailDtl dtl = new MailDtl();
			dtl.RefMailID=mail.OID;			
			dtl.Title=mail.Title;
			dtl.Doc=mail.Doc;
			dtl.Accpter= emp ;
			dtl.Sender=mail.Sender;
			dtl.Attachments= mail.Attachments;
			dtl.Insert();
		}
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Mail();
			}
		}
		/// <summary>
		/// Mails
		/// </summary>
		public Mails()
		{
		}
		/// <summary>
		/// Mails
		/// </summary>
		public Mails(string Rec)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailAttr.Accpter, Rec);
			qo.DoQuery();			
		}
		public int SearchSend(string sender)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailAttr.Sender, sender);
			
			return qo.DoQuery();
		}
		public int Search(string accepter, int mailstate)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailAttr.Accpter, accepter);
			qo.addAnd();
			qo.AddWhere(MailAttr.MailState, mailstate);
			return qo.DoQuery();
		}
		public int Search(string accepter, int mailstate1, int mailstate2 )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailAttr.Accpter, accepter);
			qo.addAnd();
			qo.addLeftBracket();
			qo.AddWhere(MailAttr.MailState, mailstate1);
			qo.addOr();
			qo.AddWhere(MailAttr.MailState, mailstate2);
			qo.addRightBracket();
			return qo.DoQuery();
		}
		public int Search(string accepter, int mailstate1, int mailstate2 , int mailstate3 )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailAttr.Accpter, accepter);
			qo.addAnd();
			qo.addLeftBracket();
			qo.AddWhere(MailAttr.MailState, mailstate1);
			qo.addOr();
			qo.AddWhere(MailAttr.MailState, mailstate2);
			qo.addOr();
			qo.AddWhere(MailAttr.MailState, mailstate3);
			qo.addRightBracket();
			return qo.DoQuery();
		}
	}
}
 