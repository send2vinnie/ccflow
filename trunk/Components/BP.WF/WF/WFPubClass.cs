using System;
using System.Web;
using BP.En;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;

namespace BP.WF
{
	/// <summary>
	/// WFPubClass ��ժҪ˵����
	/// </summary>
	public class WFPubClass
	{
		/// <summary>
		/// ��һ�������ڵ㷢����Ϣ
		/// </summary>
		/// <param name="wns"></param>
		/// <param name="title"></param>
		/// <param name="docs"></param>
		public static void SendMsg(WorkNodes wns, string title , string docs)
		{
			return ;
		}
		/// <summary>
		/// Ĭ�ϵĹ���ID authorize
		/// </summary>
        public static Int64 DefaultWorkID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DefaultWorkID"] == null)
                    return 0;
                Int64 val = (Int64)System.Web.HttpContext.Current.Session["DefaultWorkID"];
                return val;
            }
            set
            {
                System.Web.HttpContext.Current.Session["DefaultWorkID"] = value;
            }
        }
		/// <summary>
		/// Ĭ�ϵ���˽��
		/// </summary>
		public static float DefaultCheckNum
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["DefaultCheckNum"]==null)
					return 0 ; 
				float val= (float)System.Web.HttpContext.Current.Session["DefaultCheckNum"];
				//DefaultCheckNum=0;
				return val;
			}
			set
			{
				System.Web.HttpContext.Current.Session["DefaultCheckNum"]=value;
			}
		}
		/// <summary>
		/// �ο���Ϣ
		/// </summary>
		public static string DefaultRefMsg
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["DefaultRefMsg"]==null)
					return ""; 
				string val= (string)System.Web.HttpContext.Current.Session["DefaultRefMsg"];
				//DefaultRefMsg="";
				return val;
			}
			set
			{
				System.Web.HttpContext.Current.Session["DefaultRefMsg"]=value;
			}
		}	 
	}
}
