using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;


namespace BP
{
	/// <summary>
	/// SystemConfigOfTax ��ժҪ˵����
	/// </summary>
	public class SystemConfigOfTax : SystemConfig
	{		

		#region �������̵�����
		/// <summary>
		/// ������ж��Ƿ����
		/// </summary>
		public static string WFSQLOfPCIsCollect
		{
			get{ return AppSettings["WFSQLOfPCIsCollect"]; }
		}
		
		#endregion
 
		/// <summary>
		/// ��λ����
		/// </summary>
		public static string UnitName
		{
			get{ return AppSettings["UnitName"]; }
		}
		/// <summary>
		/// ��λNo
		/// </summary>
		public static string UnitNo
		{
			get{ return AppSettings["UnitNo"]; }
		}
		public static string UnitNoInHisSystem
		{
			get{ return AppSettings["UnitNoInHisSystem"]; }
		}
		public static decimal QZD_YYS
		{
			get
			{ 
				string sql="SELECT VAL FROM SYS_Config WHERE PARAKEY='QZD_CGH'";
				return DA.DBAccess.RunSQLReturnValDecimal(sql,3000,2);
			}
		}
		/// <summary>
		/// �Ļ�������ҵ�ѵ�˰Ŀ��
		/// </summary>
		public static string SMsOfWHJS
		{
			get
			{ 
				string sql="SELECT VAL FROM SYS_Config WHERE PARAKEY='SMsOfWHJS'";
				//return DA.DBAccess.RunSQLReturnString(sql,"@05610300@05620300@05630300@");
				return DA.DBAccess.RunSQLReturnString(sql);

			}
		}
		/// <summary>
		/// ���Ӫҵ˰������
		/// </summary>
		public static decimal QZD_CGH
		{
			get
			{ 
				string sql="SELECT VAL FROM SYS_Config WHERE PARAKEY='QZD_CGH'";
				return DA.DBAccess.RunSQLReturnValDecimal(sql,3000,2);
			}
		}
        public static string QZD_GGH_TD_HYs
        {
            get
            {
                return null;

                //string sql = "SELECT VAL FROM SYS_Config WHERE PARAKEY='QZD_GGH_HYs'";
                //string s = DA.DBAccess.RunSQLReturnString(sql, null);
                //if (s == null)
                //    throw new Exception("������QZD_GGH_HYs ");

                //return s.Replace(" ", "");
            }
        }
		/// <summary>
		/// ���Ӫҵ˰������
		/// </summary>
		public static decimal QZD_GGH
		{
			get
			{
				string sql="SELECT VAL FROM SYS_Config WHERE PARAKEY='QZD_GGH'";
				return DA.DBAccess.RunSQLReturnValDecimal(sql,5000,2);
			}
		}
        /// <summary>
        /// �ض����ܻ�������
        /// </summary>
        public static decimal QZD_GGH_TD
        {
            get
            {
                string sql = "SELECT VAL FROM SYS_Config WHERE PARAKEY='QZD_GGH_TD'";
                return DA.DBAccess.RunSQLReturnValDecimal(sql, 3000, 2);
            }
        }
		/// <summary>
		/// �Ƿ���������
		/// </summary>
		public static bool IsWorkOn
		{
			get
			{
				if ( AppSettings["IsWorkOn"]=="0")
					return false;
				else
					return true;
			}
		}
        public static bool IsAutoGenerCHOfNode
		{
            get
            {
                if (AppSettings["IsAutoGenerCHOfNode"] == null
                    || AppSettings["IsAutoGenerCHOfNode"] == "0")
                    return false;
                else
                    return true;
            }
		}
		public static bool IsAutoDTSWork
		{
			get
			{
				if ( AppSettings["IsAutoDTSWork"]=="0")
					return false;
				else
					return true;
			}
		}

		/// <summary>
		/// ���ű��(���ű��.)
		/// </summary>
		public static string FK_Dept
		{
			get
			{ 
				if (AppSettings["FK_Dept"]!=null)
					return AppSettings["FK_Dept"];
				else
					return "zsjg";
			}
		}
	}
}
