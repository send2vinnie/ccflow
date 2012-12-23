using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ���� ����
	/// </summary>
    public class HungUpAttr:EntityMyPKAttr
    {
        #region ��������
        public const string Title = "Title";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ִ����
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ֪ͨ��
        /// </summary>
        public const string NoticeTo = "NoticeTo";
        /// <summary>
        /// ����ԭ��
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ������
        /// </summary>
        public const string Accepter = "Accepter";
        /// <summary>
        /// �Ӵ���������
        /// </summary>
        public const string RelData = "RelData";
        /// <summary>
        /// ����ʽ.
        /// </summary>
        public const string HungUpWay = "HungUpWay";
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
    public class HungUp:EntityMyPK
    {
        #region ����
        public int HungUpWay
        {
            get
            {
                return this.GetValIntByKey(HungUpAttr.HungUpWay);
            }
            set
            {
                this.SetValByKey(HungUpAttr.HungUpWay, value);
            }
        }
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(HungUpAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(HungUpAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(HungUpAttr.WorkID);
            }
            set
            {
                this.SetValByKey(HungUpAttr.WorkID, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string Title
        {
            get
            {
                string s= this.GetValStringByKey(HungUpAttr.Title);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�Ĺ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(HungUpAttr.Title, value);
            }
        }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string Note
        {
            get
            {
               return this.GetValStringByKey(HungUpAttr.Note);
            }
            set
            {
                this.SetValByKey(HungUpAttr.Note, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.Rec);
            }
            set
            {
                this.SetValByKey(HungUpAttr.Rec, value);
            }
        }
        public string RelData
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.RelData);
            }
            set
            {
                this.SetValByKey(HungUpAttr.RelData, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.RDT);
            }
            set
            {
                this.SetValByKey(HungUpAttr.RDT, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public HungUp()
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_HungUp");
                map.EnDesc = "����";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddTBInt(HungUpAttr.FK_Node, 0, "�ڵ�ID", true, true);
                map.AddTBInt(HungUpAttr.WorkID, 0, "WorkID", true, true);
                map.AddDDLSysEnum(HungUpAttr.HungUpWay, 0, "����ʽ", true, true, HungUpAttr.HungUpWay, 
                    "@0=���޹���@1=��ָ����ʱ��������֪ͨ���Լ�@2=��ָ����ʱ��������֪ͨ������");
                map.AddTBDateTime(HungUpAttr.RelData, null, "�ָ�����ʱ��", true, false);

            //    map.AddTBString(HungUpAttr.Accepter, null, "������", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(HungUpAttr.Note, null, "����ԭ��(����������֧�ֱ���)", true, false, true);

                map.AddTBString(HungUpAttr.Rec, null, "������", true, false, 0, 50, 10, true);
                map.AddTBDateTime(HungUpAttr.RDT, null, "����ʱ��", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ִ���ͷŹ���
        /// </summary>
        public void DoRelease()
        {
        }
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
	public class HungUps: EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new HungUp();
			}
		}
		/// <summary>
        /// ����
		/// </summary>
		public HungUps(){} 		 
		#endregion
	}
}
