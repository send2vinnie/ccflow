using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;
using BP.DTS;

namespace BP.TA
{
	/// <summary>
	/// PubClass 的摘要说明。
	/// </summary>
	public class PubClass
	{
		public PubClass()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
       
		/// <summary>
		/// 检查emps 否正确。
		/// </summary>
		/// <param name="emps"></param>
		public static string CheckEmps(string emps)
		{
			emps=emps.Replace(",," , "," );
			emps=emps.Replace(",,"  ,  "," );

			emps=emps.Replace(" "  ,  "" );
			emps=emps.Replace(" "  ,  "" );

			if (emps.Length==0)
				throw new Exception("接受人为空");

            string names = "";
			DataTable dt =DBAccess.RunSQLReturnTable("select No,Name from pub_emp");
			//Emps emps = new Emps();
			string errMsg="下列人员编号错误:";		 
			string[] strs= emps.Split(',');
			foreach(string str in strs)
			{
				if (str==null)
					continue;
				if (str.Length==0)
					continue;
				if (str==",")
					continue;

				bool isHave=false;
				foreach(DataRow dr in dt.Rows)
				{
					if (str==dr[0].ToString())
					{
                        names += dr[1].ToString()+",";
						isHave=true;
						break;
					}
				}

				if (isHave==false)
					errMsg+="<br>无此接受人"+str;
			}
			if (errMsg!="下列人员编号错误:")
				throw new Exception(errMsg);

            WebUser.Tag =names ;
			return emps;
		}
		 
	}
}
