using System;
using System.Reflection;
using System.Configuration;
namespace BP.EIP.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
	/// </summary>
	public sealed class DataAccess 
	{
        //private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
        private static readonly string AssemblyPath = "BP.EIP";        
		public DataAccess()
		{ }

        #region CreateObject 

		//不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// 记录错误日志
				return null;
			}			
			
        }
		//使用缓存
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// 写入缓存
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// 记录错误日志
				}
			}
			return objType;
		}
        #endregion

        #region 泛型生成
        ///// <summary>
        ///// 创建数据层接口。
        ///// </summary>
        //public static t Create(string ClassName)
        //{

        //    string ClassNamespace = AssemblyPath +"."+ ClassName;
        //    object objType = CreateObject(AssemblyPath, ClassNamespace);
        //    return (t)objType;
        //}
        #endregion

        #region CreateSysManage
        public static BP.EIP.IDAL.ISysManage CreateSysManage()
		{
			//方式1			
			//return (BP.EIP.IDAL.ISysManage)Assembly.Load(AssemblyPath).CreateInstance(AssemblyPath+".SysManage");

			//方式2 			
			string classNamespace = AssemblyPath+".SysManage";	
			object objType=CreateObject(AssemblyPath,classNamespace);
            return (BP.EIP.IDAL.ISysManage)objType;		
		}
		#endregion
             
   
		/// <summary>
		/// 创建EIP_Emp数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_Emp CreateEIP_Emp()
		{

			string ClassNamespace = AssemblyPath +".EIP_Emp";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_Emp)objType;
		}

		/// <summary>
		/// 创建EIP_EmpPost数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_EmpPost CreateEIP_EmpPost()
		{

			string ClassNamespace = AssemblyPath +".EIP_EmpPost";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_EmpPost)objType;
		}

		/// <summary>
		/// 创建EIP_Layout数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_Layout CreateEIP_Layout()
		{

			string ClassNamespace = AssemblyPath +".EIP_Layout";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_Layout)objType;
		}

		/// <summary>
		/// 创建EIP_LayoutDetail数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_LayoutDetail CreateEIP_LayoutDetail()
		{

			string ClassNamespace = AssemblyPath +".EIP_LayoutDetail";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_LayoutDetail)objType;
		}

		/// <summary>
		/// 创建EIP_UITheme数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_UITheme CreateEIP_UITheme()
		{

			string ClassNamespace = AssemblyPath +".EIP_UITheme";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_UITheme)objType;
		}

		/// <summary>
		/// 创建EIP_UserEPD数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IEIP_UserEPD CreateEIP_UserEPD()
		{

			string ClassNamespace = AssemblyPath +".EIP_UserEPD";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IEIP_UserEPD)objType;
		}

		/// <summary>
		/// 创建GPM_Menu数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IGPM_Menu CreateGPM_Menu()
		{

			string ClassNamespace = AssemblyPath +".GPM_Menu";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IGPM_Menu)objType;
		}

		/// <summary>
		/// 创建OA_AddrBook数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_AddrBook CreateOA_AddrBook()
		{

			string ClassNamespace = AssemblyPath +".OA_AddrBook";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_AddrBook)objType;
		}

		/// <summary>
		/// 创建OA_AddrBookDept数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_AddrBookDept CreateOA_AddrBookDept()
		{

			string ClassNamespace = AssemblyPath +".OA_AddrBookDept";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_AddrBookDept)objType;
		}

		/// <summary>
		/// 创建OA_AddrGrouping数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_AddrGrouping CreateOA_AddrGrouping()
		{

			string ClassNamespace = AssemblyPath +".OA_AddrGrouping";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_AddrGrouping)objType;
		}

		/// <summary>
		/// 创建OA_Attachment数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Attachment CreateOA_Attachment()
		{

			string ClassNamespace = AssemblyPath +".OA_Attachment";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Attachment)objType;
		}

		/// <summary>
		/// 创建OA_Category数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Category CreateOA_Category()
		{

			string ClassNamespace = AssemblyPath +".OA_Category";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Category)objType;
		}

		/// <summary>
		/// 创建OA_Channel数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Channel CreateOA_Channel()
		{

			string ClassNamespace = AssemblyPath +".OA_Channel";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Channel)objType;
		}

		/// <summary>
		/// 创建OA_Email数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Email CreateOA_Email()
		{

			string ClassNamespace = AssemblyPath +".OA_Email";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Email)objType;
		}

		/// <summary>
		/// 创建OA_EmailAttach数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_EmailAttach CreateOA_EmailAttach()
		{

			string ClassNamespace = AssemblyPath +".OA_EmailAttach";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_EmailAttach)objType;
		}

		/// <summary>
		/// 创建OA_Meeting数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Meeting CreateOA_Meeting()
		{

			string ClassNamespace = AssemblyPath +".OA_Meeting";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Meeting)objType;
		}

		/// <summary>
		/// 创建OA_MeetingAttach数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_MeetingAttach CreateOA_MeetingAttach()
		{

			string ClassNamespace = AssemblyPath +".OA_MeetingAttach";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_MeetingAttach)objType;
		}

		/// <summary>
		/// 创建OA_Message数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Message CreateOA_Message()
		{

			string ClassNamespace = AssemblyPath +".OA_Message";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Message)objType;
		}

		/// <summary>
		/// 创建OA_News数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_News CreateOA_News()
		{

			string ClassNamespace = AssemblyPath +".OA_News";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_News)objType;
		}

		/// <summary>
		/// 创建OA_NewsAttach数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_NewsAttach CreateOA_NewsAttach()
		{

			string ClassNamespace = AssemblyPath +".OA_NewsAttach";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_NewsAttach)objType;
		}

		/// <summary>
		/// 创建OA_Notice数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_Notice CreateOA_Notice()
		{

			string ClassNamespace = AssemblyPath +".OA_Notice";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_Notice)objType;
		}

		/// <summary>
		/// 创建OA_NoticeAttach数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_NoticeAttach CreateOA_NoticeAttach()
		{

			string ClassNamespace = AssemblyPath +".OA_NoticeAttach";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_NoticeAttach)objType;
		}

		/// <summary>
		/// 创建OA_SMS数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IOA_SMS CreateOA_SMS()
		{

			string ClassNamespace = AssemblyPath +".OA_SMS";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IOA_SMS)objType;
		}

		/// <summary>
		/// 创建Port_App数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IPort_App CreatePort_App()
		{

			string ClassNamespace = AssemblyPath +".Port_App";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_App)objType;
		}

		/// <summary>
		/// 创建Port_ChangeLog数据层接口。
		/// </summary>
		public static BP.EIP.IDAL.IPort_ChangeLog CreatePort_ChangeLog()
		{

			string ClassNamespace = AssemblyPath +".Port_ChangeLog";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_ChangeLog)objType;
		}

		/// <summary>
		/// 创建Port_Dept数据层接口。部门
		/// </summary>
		public static BP.EIP.IDAL.IPort_Dept CreatePort_Dept()
		{

			string ClassNamespace = AssemblyPath +".Port_Dept";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Dept)objType;
		}

		/// <summary>
		/// 创建Port_Domain数据层接口。部门
		/// </summary>
		public static BP.EIP.IDAL.IPort_Domain CreatePort_Domain()
		{

			string ClassNamespace = AssemblyPath +".Port_Domain";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Domain)objType;
		}

		/// <summary>
		/// 创建Port_Emp数据层接口。用户
		/// </summary>
		public static BP.EIP.IDAL.IPort_Emp CreatePort_Emp()
		{

			string ClassNamespace = AssemblyPath +".Port_Emp";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Emp)objType;
		}

		/// <summary>
		/// 创建Port_EmpDept数据层接口。操作员与工作部门
		/// </summary>
		public static BP.EIP.IDAL.IPort_EmpDept CreatePort_EmpDept()
		{

			string ClassNamespace = AssemblyPath +".Port_EmpDept";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_EmpDept)objType;
		}

		/// <summary>
		/// 创建Port_EmpDomain数据层接口。操作员与工作部门
		/// </summary>
		public static BP.EIP.IDAL.IPort_EmpDomain CreatePort_EmpDomain()
		{

			string ClassNamespace = AssemblyPath +".Port_EmpDomain";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_EmpDomain)objType;
		}

		/// <summary>
		/// 创建Port_EmpStation数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_EmpStation CreatePort_EmpStation()
		{

			string ClassNamespace = AssemblyPath +".Port_EmpStation";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_EmpStation)objType;
		}

		/// <summary>
		/// 创建Port_Function数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Function CreatePort_Function()
		{

			string ClassNamespace = AssemblyPath +".Port_Function";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Function)objType;
		}

		/// <summary>
		/// 创建Port_FunctionOperate数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_FunctionOperate CreatePort_FunctionOperate()
		{

			string ClassNamespace = AssemblyPath +".Port_FunctionOperate";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_FunctionOperate)objType;
		}

		/// <summary>
		/// 创建Port_Menu数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Menu CreatePort_Menu()
		{

			string ClassNamespace = AssemblyPath +".Port_Menu";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Menu)objType;
		}

		/// <summary>
		/// 创建Port_MenuOperate数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_MenuOperate CreatePort_MenuOperate()
		{

			string ClassNamespace = AssemblyPath +".Port_MenuOperate";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_MenuOperate)objType;
		}

		/// <summary>
		/// 创建Port_Operate数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Operate CreatePort_Operate()
		{

			string ClassNamespace = AssemblyPath +".Port_Operate";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Operate)objType;
		}

		/// <summary>
		/// 创建Port_Post数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Post CreatePort_Post()
		{

			string ClassNamespace = AssemblyPath +".Port_Post";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Post)objType;
		}

		/// <summary>
		/// 创建Port_PostDept数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_PostDept CreatePort_PostDept()
		{

			string ClassNamespace = AssemblyPath +".Port_PostDept";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_PostDept)objType;
		}

		/// <summary>
		/// 创建Port_Rule数据层接口。人员岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Rule CreatePort_Rule()
		{

			string ClassNamespace = AssemblyPath +".Port_Rule";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Rule)objType;
		}

		/// <summary>
		/// 创建Port_Station数据层接口。岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_Station CreatePort_Station()
		{

			string ClassNamespace = AssemblyPath +".Port_Station";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_Station)objType;
		}

		/// <summary>
		/// 创建Port_StationDomain数据层接口。岗位
		/// </summary>
		public static BP.EIP.IDAL.IPort_StationDomain CreatePort_StationDomain()
		{

			string ClassNamespace = AssemblyPath +".Port_StationDomain";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.IPort_StationDomain)objType;
		}

		/// <summary>
		/// 创建Sys_Enum数据层接口。岗位
		/// </summary>
		public static BP.EIP.IDAL.ISys_Enum CreateSys_Enum()
		{

			string ClassNamespace = AssemblyPath +".Sys_Enum";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.ISys_Enum)objType;
		}

		/// <summary>
		/// 创建sysdiagrams数据层接口。1
		/// </summary>
		public static BP.EIP.IDAL.Isysdiagrams Createsysdiagrams()
		{

			string ClassNamespace = AssemblyPath +".sysdiagrams";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (BP.EIP.IDAL.Isysdiagrams)objType;
		}

}
}