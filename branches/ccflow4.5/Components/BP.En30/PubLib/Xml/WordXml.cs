using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Pub.Xml
{
	/// <summary>
    /// ��������
	/// </summary>
    public class WordXmlAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
	/// ����
	/// </summary>
	public class WordXml:XmlEn
	{
		#region ����
        /// <summary>
        /// ���
        /// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(WordXmlAttr.No);
			}
		}
        /// <summary>
        /// ����
        /// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(WordXmlAttr.Name);
			}
		}
		#endregion

		#region ����
        /// <summary>
        /// ����
        /// </summary>
		public WordXml()
		{
		}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="no">���</param>
		public WordXml(string no)
		{
            this.RetrieveByPK(WordXmlAttr.No, no);
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new WordXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ����s
	/// </summary>
    public class WordXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ����s
        /// </summary>
        public WordXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new WordXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\Sys\\Word.xml";
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
