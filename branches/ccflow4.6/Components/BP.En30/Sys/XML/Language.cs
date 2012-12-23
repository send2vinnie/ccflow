using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;

namespace BP.Sys
{
	/// <summary>
	/// ����
	/// </summary>
	public class LanguageAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string No="No";
        /// <summary>
        /// ����
        /// </summary>
        public const string CH = "CH";
		/// <summary>
		/// Ӣ��
		/// </summary>
        public const string EN = "EN";
        /// <summary>
        /// B5
        /// </summary>
        public const string B5 = "B5";
	}
    /// <summary>
    /// ����
    /// </summary>
	public class Language:XmlEn
	{
		#region ����
		public string No
		{
			get
			{
				return this.GetValStringByKey(LanguageAttr.No);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
        public string CH
		{
			get
			{
                return this.GetValStringByKey(LanguageAttr.CH);
			}
		}
        public string EN
		{
			get
			{
                return this.GetValStringByKey(LanguageAttr.EN);
			}
		}
        public string B5
        {
            get
            {
                return this.GetValStringByKey(LanguageAttr.B5);
            }
        }
		#endregion

		#region ����
        /// <summary>
        /// ����
        /// </summary>
		public Language()
		{
		}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="no"></param>
        public Language(string no)
        {
            //  this.No = no;
            int i = this.RetrieveByPK("No", no);
            if (i == 0)
            {
                BP.SystemConfig.DoClearCash();
                Log.DebugWriteWarning(" not set values language no= " + no + "");
                throw new Exception("��ţ�No=" + no + " ����" + this.ToString());
            }
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Languages();
			}
		}
		#endregion

        public static string Turn2Traditional(string s)
        {
            try
            {
                return ChineseConverter.Convert(s, ChineseConversionDirection.SimplifiedToTraditional);
            }
            catch
            {
                return s;
            }
        }
        /// <summary>
        /// ��ȡֵ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="chVal"></param>
        /// <returns></returns>
        public static string GetValByUserLang(string no, string chVal)
        {
            if (no == null)
                throw new Exception("û�и����ֵ��");
            string lang = BP.Web.WebUser.SysLang;
            switch (lang)
            {
                case "CH":
                    return chVal;
                case "B5":
                    return Turn2Traditional(chVal);
                default:
                    break;
            }

            Language en = new Language(no);
            return en.GetValStringByKey(lang);
        }
        /// <summary>
        /// ��ȡֵ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="ltype"></param>
        /// <param name="chVal"></param>
        /// <returns></returns>
        public static string GetVal(string no, string ltype, string chVal)
        {
            switch (ltype)
            {
                case "CH":
                    return chVal;
                case "B5":
                    return Turn2Traditional(chVal);
                default:
                    break;
            }
            Language en = new Language(no);
            return en.GetValStringByKey(ltype);
        }
	}
	/// <summary>
    /// ����
	/// </summary>
	public class Languages:XmlEns
	{
		#region ����
		/// <summary>
        /// ����
		/// </summary>
		public Languages()
        {
        }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Language();
			}
		}
		public override string File
		{
            get
            {
                if (SystemConfig.IsBSsystem)
                    return SystemConfig.PathOfData + "\\Language";
                else
                {
                    //  AppDomain.CurrentDomain.BaseDirectory + ".\\..\\..\\..\\.VisualFlow\\Data\\Language";
                    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\VisualFlow\\Data\\Language");
                    return info.FullName;
                    // return info.Name;
                    // return AppDomain.CurrentDomain.BaseDirectory + ".\\..\\..\\..\\VisualFlow\\Data\\Language";
                }
            }
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Item";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		#endregion
		 
	}
}
