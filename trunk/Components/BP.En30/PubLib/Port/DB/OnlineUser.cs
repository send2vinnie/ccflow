using System;
using System.Collections;
using System.Web.Hosting;

namespace BP.Web
{
	/// <summary>
	/// 在线用户
	/// </summary>
	public class OnlineUserManager
	{
		public static OnlineUsers GetOnlineUsers()
		{			 
			OnlineUsers users= (OnlineUsers)System.Web.HttpContext.Current.Application["OnlineUsers"];
			if (users==null)
			{
				users= new OnlineUsers();
				System.Web.HttpContext.Current.Application["OnlineUsers"]=users;
			}
			return users;
		} 
	  
		  
	}
	/// <summary>
	/// OnlineUser 的摘要说明。
	/// </summary>
	public class OnlineUser
	{
		#region 属性
		 
		private string  _EmpNo="";
		private string  _Name="";
		private string  _DeptName="";
		private string  _IP="";
		private string  _LoginDateTime=DateTime.Now.ToString("yyyy-MM-dd hh:mm");
		//private string  _AccessDateTime=_LoginDateTime ;

		
		/// <summary>
		/// 工作人员编号
		/// </summary>
		public string No
		{
			get
			{
				return this._EmpNo;
			}
			set
			{
				_EmpNo=value;
			}
		}	 
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				_IP=value;
			}
		}	 
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				_Name=value;
			}
		}
		/// <summary>
		/// 部门
		/// </summary>
		public string DeptName
		{
			get
			{
				return this._DeptName;
			}
			set
			{
				_DeptName=value;
			}
		}
		/// <summary>
		/// 登陆日期
		/// </summary>
		public string LoginDateTime
		{
			get
			{
				return this._LoginDateTime;
			}
			set
			{
				_LoginDateTime=value;
			}
		}
//		/// <summary>
//		/// 访问日期
//		/// </summary>
//		public string AccessDateTime
//		{
//			get
//			{
//				return this._AccessDateTime;
//			}
//			set
//			{
//				_AccessDateTime=value;
//			}
//		}
		#endregion

		/// <summary>
		/// 再线用户
		/// </summary>
		public OnlineUser()
		{

		}		 
	}
	/// <summary>
	/// 消息集合
	/// </summary>
	public class OnlineUsers : ArrayList
	{
		#region 增加消息
		/// <summary>
		/// 增加消息
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="nodeId"></param>
		/// <param name="toEmpId"></param>
		/// <param name="info"></param>
		public void AddOnlineUser(  string empNo, string empName, string deptName, string ip)
		{
			OnlineUser user = new OnlineUser();
			user.No=empNo;
			user.Name=empName;
			user.IP=ip;
			user.DeptName=deptName;

			this.AddOnlineUser(user);
		}
		/// <summary>
		/// 增加消息
		/// </summary>
		/// <param name="OnlineUser">消息</param>
		public void AddOnlineUser(OnlineUser user)
		{			 
			if (this.IsExites(user.No)==false)
				this.Add(user);
		}
		public bool IsExites(string  empNo)
		{
			foreach(OnlineUser ou in this)
			{
				if (ou.No==empNo)
					return true;
			}
			return false;
		}
		#endregion 

		#region 关于消息集合的操作		
		#endregion

		public OnlineUsers()
		{
		}
		/// <summary>
		/// 根据位置取得数据
		/// </summary>
		public new OnlineUser this[int index]
		{
			get 
			{
				return (OnlineUser)this[index];
			}
		}
	}
}
