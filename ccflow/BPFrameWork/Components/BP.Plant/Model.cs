using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Port.Xml
{
	/// <summary>
	/// ����
	/// </summary>
    public class ModelAttr 
    {
        /// <summary>
        /// Url
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// Img
        /// </summary>
        public const string Img = "Img";

    }
	/// <summary>
	/// Model ��ժҪ˵�������Ե����á�
	/// </summary>
	public class Model:XmlEnNoName
	{
		#region ����
        public string Url
		{
			get
			{
				return this.GetValStringByKey(ModelAttr.Url);
			}
		}
        public string Img
        {
            get
            {
                return this.GetValStringByKey(ModelAttr.Img);
            }
        }
        public bool IsEnable
        {
            get
            {
                string s = this.GetValStringByKey(ModelAttr.IsEnable);
                if (s == null || s == "" || s=="0")
                    return false;

                return true;
            }
        }
		#endregion

		#region ����
		public Model()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Models();
			}
		}
		#endregion
	}
	/// <summary>
	/// ���Լ���
	/// </summary>
	public class Models:XmlEns
	{
		#region ����
		/// <summary>
		/// ���˹�����Ϊ������Ԫ��
		/// </summary>
		public Models()
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
				return new Model();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\Port\\Model.xml";
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
