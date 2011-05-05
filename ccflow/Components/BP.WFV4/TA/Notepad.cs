using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
	/// <summary>
	/// 便条属性
	/// </summary>
	public class NotepadAttr:EntityOIDAttr
	{
		/// <summary>
		/// 回复消息
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// 记录人
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// 回复时间
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// 文件夹
		/// </summary>
		public const string FK_Folder="FK_Folder";
	}
	/// <summary>
	/// 便条
	/// </summary> 
	public class Notepad : EntityOID
	{
		#region 基本属性
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(NotepadAttr.Doc);
			}
			set
			{
				SetValByKey(NotepadAttr.Doc,value);
			}
		}
		/// <summary>
		/// RDT
		/// </summary>
		public string RDT 
		{
			get
			{
				return this.GetValStringByKey(NotepadAttr.RDT);
			}
			set
			{
				SetValByKey(NotepadAttr.RDT,value);
			}
		}
		public string Recorder
		{
			get
			{
				return this.GetValAppDateByKey(NotepadAttr.Recorder); 
			}
			set
			{
				SetValByKey(NotepadAttr.Recorder,value);
			}
		}
		#endregion
 
		#region 构造函数
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
		/// 便条
		/// </summary>
		public Notepad()
		{
		  
		}
		/// <summary>
		/// 便条
		/// </summary>
		/// <param name="_No">No</param>
		public Notepad(int oid):base(oid)
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

				Map map = new Map("TA_Notepad");
				map.EnDesc="便条";
				//map.Icon="./Images/Notepad_s.ico";
				map.Icon="../TA/Images/Notepad_s.ico";

				map.AddTBIntPKOID();
				 
				map.AddTBString(NotepadAttr.Doc,null,"便条内容",true,false,0,4000,550);
				map.AddDDLEntities(NotepadAttr.FK_Folder,null,DA.DataType.AppInt, "文件夹", new NotepadFolders(),"OID","Name",true);
				map.AddTBDateTime(NotepadAttr.RDT,"记录时间",true,true );
				map.AddTBString(NotepadAttr.Recorder,WebUser.No,"记录人",false,false,0,4000,15);

				map.AttrsOfSearch.AddHidden(NotepadAttr.Recorder,"=",Web.WebUser.No);
				//map.AttrsOfSearch.AddFromTo("记录时间",NotepadAttr.RDT,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 便条s
	/// </summary> 
	public class Notepads: Entities
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Notepad();
			}
		}
		public override int RetrieveAll()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NotepadAttr.Recorder, WebUser.No);
			qo.addOrderBy(NotepadAttr.RDT);
			return qo.DoQuery();
		}
		/// <summary>
		/// Notepads
		/// </summary>
		public Notepads()
		{
		}
		/// <summary>
		/// Notepads
		/// </summary>
		public Notepads(string Recorder)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NotepadAttr.Recorder, Recorder);
			qo.DoQuery();			
		}
	}
}
 