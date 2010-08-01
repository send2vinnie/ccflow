using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
	/// <summary>
	/// 记事本属性
	/// </summary>
	public class NotepadFolderAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// 记录人
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// 记录日期
		/// </summary>
		public const string RDT="RDT";
	}
	/// <summary>
	/// 记事本
	/// </summary> 
	public class NotepadFolder : EntityOIDName
	{
		#region 基本属性
		public string Recorder
		{
			get
			{
				return this.GetValAppDateByKey(NotepadFolderAttr.Recorder); 
			}
			set
			{
				SetValByKey(NotepadFolderAttr.Recorder,value);
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
		/// 记事本
		/// </summary>
		public NotepadFolder()
		{
		  
		}
		/// <summary>
		/// 记事本
		/// </summary>
		/// <param name="_No">No</param>
		public NotepadFolder(int oid):base(oid)
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

				Map map = new Map("TA_NotepadFolder");
				map.EnDesc="记事本文件夹";
				map.Icon="../TA/Images/Folder.ico";
				//map.Icon="../TA/Images/log_s.ico";

				map.AddTBIntPKOID();

				map.AddTBString(NotepadFolderAttr.Name,null,"新建文件夹1",true,false,0,4000,300);
				map.AddTBString(NotepadFolderAttr.Recorder,WebUser.No,"记录人",false,false,0,4000,15);
				map.AttrsOfSearch.AddHidden(NotepadFolderAttr.Recorder,"=",Web.WebUser.No);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

	}
	/// <summary>
	/// 记事本s
	/// </summary> 
	public class NotepadFolders: EntitiesOIDName
	{
		public override int RetrieveAll()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( NotepadFolderAttr.Recorder, WebUser.No);
			return qo.DoQuery();
		}

		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NotepadFolder();
			}
		}
		/// <summary>
		/// NotepadFolders
		/// </summary>
		public NotepadFolders()
		{
		}
		/// <summary>
		/// NotepadFolders
		/// </summary>
		public NotepadFolders(string Recorder)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NotepadFolderAttr.Recorder, Recorder);
			qo.DoQuery();			
		}
	}
}
 