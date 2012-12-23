using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Sys.Xml
{
	/// <summary>
	/// ����
	/// </summary>
	public class PanelEnsAttr
	{
		/// <summary>
		/// ������Ϊ
		/// </summary>
		public const string Attr="Attr";
		/// <summary>
		/// ���ʽ
		/// </summary>
		public const string URL="URL";
		/// <summary>
		/// ����
		/// </summary>
		public const string For="For";
	}
	/// <summary>
	/// PanelEns ��ժҪ˵����
	/// ���˹�����Ϊ������Ԫ��
	/// 1������ PanelEns ��һ����ϸ��
	/// 2������ʾһ������Ԫ�ء�
	/// </summary>
	public class PanelEns:XmlEn
	{

		#region ����
		public string Attr
		{
			get
			{
				return this.GetValStringByKey(PanelEnsAttr.Attr);
			}
		}
		public string For
		{
			get
			{
				return this.GetValStringByKey(PanelEnsAttr.For);
			}
		}
		public string URL
		{
			get
			{
				return this.GetValStringByKey(PanelEnsAttr.URL);
			}
		}
		#endregion

		#region ����
		public PanelEns()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new PanelEnss();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class PanelEnss:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public PanelEnss(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new PanelEns();
			}
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"\\Ens\\PanelEns.xml";
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
