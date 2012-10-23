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
        public const string HungUpDays = "HungUpDays";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��������(������������)
        /// </summary>
        public const string SendDT = "SendDT";
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
    public class HungUp : Entity
    {
        #region ����
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No != "admin")
                {
                    uac.IsView = false;
                    return uac;
                }
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
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
                string s = this.GetValStringByKey(HungUpAttr.Note);
                if (string.IsNullOrEmpty(s))
                    s = "����@Rec�Ĺ�����Ϣ.";
                return s;
            }
            set
            {
                this.SetValByKey(HungUpAttr.Note, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string HungUpDays
        {
            get
            {
                string sql= this.GetValStringByKey(HungUpAttr.HungUpDays);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("��", "'");
                sql = sql.Replace("��", "'");
                sql = sql.Replace("''", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(HungUpAttr.HungUpDays, value);
            }
        }
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        public CtrlWay HisCtrlWay
        {
            get
            {
                return (CtrlWay)this.GetValIntByKey(HungUpAttr.NoticeTo);
            }
            set
            {
                this.SetValByKey(HungUpAttr.NoticeTo, (int)value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// HungUp
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
                Map map = new Map("WF_Node");
                map.EnDesc = "�������";
                map.EnType = EnType.Admin;
                map.AddTBString(NodeAttr.Name, null, "�ڵ�����", true, true, 0, 100, 10, true);
                map.AddTBIntPK(NodeAttr.NodeID, 0,"�ڵ�ID", true, true);

                map.AddDDLSysEnum(HungUpAttr.NoticeTo, 0, "���Ʒ�ʽ",true, true,"CtrlWay");
                map.AddTBString(HungUpAttr.HungUpDays, null, "SQL���ʽ", true, false, 0, 500, 10, true);
                map.AddTBString(HungUpAttr.Title, null, "�������", true, false, 0, 500, 10,true);
                map.AddTBStringDoc(HungUpAttr.Note, null, "����ԭ��(����������֧�ֱ���)", true, false,true);

                map.AddSearchAttr(HungUpAttr.NoticeTo);

              
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����
	/// </summary>
	public class HungUps: Entities
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
