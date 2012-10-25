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
    public class HungAttr:EntityMyPKAttr
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
        public const string HungDays = "HungDays";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��������(������������)
        /// </summary>
        public const string SendDT = "SendDT";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ������
        /// </summary>
        public const string Accepter = "Accepter";
       
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
    public class Hung : EntityMyPK
    {
        #region ����
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(HungAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(HungAttr.FK_Node, value);
            }
        }
         
        /// <summary>
        /// �������
        /// </summary>
        public string Title
        {
            get
            {
                string s= this.GetValStringByKey(HungAttr.Title);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�Ĺ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(HungAttr.Title, value);
            }
        }
        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string Note
        {
            get
            {
                string s = this.GetValStringByKey(HungAttr.Note);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�Ĺ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(HungAttr.Note, value);
            }
        }
        
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public Hung()
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

                Map map = new Map("WF_Hung");
                map.EnDesc = "����";
                map.EnType = EnType.Admin;

                map.AddMyPK();

                map.AddTBInt(HungAttr.FK_Node, 0, "�ڵ�ID", true, true);
                map.AddTBInt(HungAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(HungAttr.HungDays, 0, "��������", true, true);

                map.AddTBDateTime(HungAttr.SendDT, null, "����ʱ��", true, false);

                map.AddTBString(HungAttr.Accepter, null, "������", true, false, 0, 500, 10, true);
                map.AddTBString(HungAttr.Title, null, "�������", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(HungAttr.Note, null, "����ԭ��(����������֧�ֱ���)", true, false, true);

                map.AddTBString(HungAttr.Rec, null, "������", true, false, 0, 50, 10, true);
                map.AddTBDateTime(HungAttr.RDT, null, "����ʱ��", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
	public class Hungs: EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Hung();
			}
		}
		/// <summary>
        /// ����
		/// </summary>
		public Hungs(){} 		 
		#endregion
	}
}
