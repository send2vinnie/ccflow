using System;
////using System.Drawing;
// using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using BP.En ; 
using BP.DA;
using BP.Web ; 

/*
 * Edit by peng: 2004-09-23
 * 
 * �����ʵ�壺
 * 
 * 
 * */

namespace BP.Rpt
{	
	/// <summary>
	/// ����ʵ��Ļ������ԣ�
	/// ����2γ3λ�����ǴӴ˻����ϼ̳������ġ�
	/// </summary>
	abstract public class RptEntity
	{
		/// <summary>
		/// ��Ӧ��key1.
		/// </summary>
		public string Key1="Key1";
		/// <summary>
		/// key2
		/// </summary>
		public string Key2="Key2";
        /// <summary>
        /// Key3
        /// </summary>
		public string Key3="Key3";


		/// <summary>
		/// �����������������
		/// </summary>
		public AnalyseDataType HisADT=AnalyseDataType.AppInt;
		/// <summary>
		/// �Ƿ���ʾtooltips
		/// </summary>
		public bool IsShowTooltips=false;
		/// <summary>
		/// ����ʵ��
		/// </summary>
		public RptEntity(){}
		/// <summary>
		/// ��������
		/// </summary>
		public string CellUrlTarget="_self";

		#region  ��������
		/// <summary>
		/// ���汨�����
		/// </summary>
		public string Title="";

		/// <summary>
		/// ��Ŀ
		/// </summary>
		public string LeftTitle="��Ŀ";
		/// <summary>
		/// ����
		/// </summary>
		public string Author=WebUser.Name ;
		/// <summary>
		/// ��������
		/// </summary>
		public string ReportDate=DateTime.Now.ToString("yyyy-MM-dd") ; 
		#endregion
	}
	
	
}
