using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Pub.Xml
{
	/// <summary>
    /// ��ɫ����
	/// </summary>
	public class ColorXmlAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string No="No";
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
	}
	/// <summary>
	/// ��ɫ
	/// </summary>
	public class ColorXml:XmlEn
	{
		#region ����
        /// <summary>
        /// ���
        /// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(ColorXmlAttr.No);
			}
		}
        /// <summary>
        /// ����
        /// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(ColorXmlAttr.Name);
			}
		}
		#endregion

		#region ����
        /// <summary>
        /// ��ɫ
        /// </summary>
		public ColorXml()
		{
		}
        /// <summary>
        /// ��ɫ
        /// </summary>
        /// <param name="no">���</param>
		public ColorXml(string no)
		{
            this.RetrieveByPK(ColorXmlAttr.No, no);
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new ColorXmls();
			}
		}
		#endregion
	}
	/// <summary>
    /// ��ɫs
	/// </summary>
	public class ColorXmls:XmlEns
	{
		#region ����
		/// <summary>
		/// ��ɫs
		/// </summary>
		public ColorXmls(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new ColorXml();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Sys\\Color.xml";
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
