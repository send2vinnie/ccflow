using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// �ƽ���¼
	/// </summary>
    public class ForwardWorkAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ƽ�ԭ��
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// �ƽ���
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �ƽ�������
        /// </summary>
        public const string FK_EmpName = "FK_EmpName";
        /// <summary>
        /// �ƽ�ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// �ƽ���
        /// </summary>
        public const string ToEmp = "ToEmp";
        /// <summary>
        /// �ƽ�����Ա����
        /// </summary>
        public const string ToEmpName = "ToEmpName";
        #endregion
    }
	/// <summary>
	/// �ƽ���¼
	/// </summary>
	public class ForwardWork : EntityMyPK
	{		
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(ForwardWorkAttr.WorkID);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// �����ڵ�
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(ForwardWorkAttr.FK_Node);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.FK_Node,value);
			}
		}
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public bool IsRead
        {
            get
            {
                return this.GetValBooleanByKey(ForwardWorkAttr.IsRead);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.IsRead, value);
            }
        }
        /// <summary>
        /// ToEmpName
        /// </summary>
        public string ToEmpName
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.ToEmpName);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.ToEmpName, value);
            }
        }
        /// <summary>
        /// �ƽ�������.
        /// </summary>
        public string FK_EmpName
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.FK_EmpName);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.FK_EmpName, value);
            }
        }
        /// <summary>
        /// �ƽ�ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.RDT);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.RDT, value);
            }
        }
        /// <summary>
        /// �ƽ����
        /// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(ForwardWorkAttr.Note);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.Note,value);
			}
		}
        /// <summary>
        /// �ƽ����html��ʽ
        /// </summary>
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ForwardWorkAttr.Note);
            }
        }
        /// <summary>
        /// �ƽ���
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.FK_Emp);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// �ƽ���
        /// </summary>
        public string ToEmp
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.ToEmp);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.ToEmp, value);
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// �ƽ���¼
		/// </summary>
		public ForwardWork()
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

                Map map = new Map("WF_ForwardWork");
                map.EnDesc = "�ƽ���¼";
                map.EnType = EnType.App;
                map.AddMyPK();

                map.AddTBInt(ForwardWorkAttr.WorkID, 0, "����ID", true, true);
                map.AddTBInt(ForwardWorkAttr.FK_Node, 0, "FK_Node", true, true);
                map.AddTBString(ForwardWorkAttr.FK_Emp, null, "�ƽ���", true, true, 0, 40, 10);
                map.AddTBString(ForwardWorkAttr.FK_EmpName, null, "�ƽ�������", true, true, 0, 40, 10);

                map.AddTBString(ForwardWorkAttr.ToEmp, null, "�ƽ���", true, true, 0, 40, 10);
                map.AddTBString(ForwardWorkAttr.ToEmpName, null, "�ƽ�������", true, true, 0, 40, 10);

                map.AddTBDateTime(ForwardWorkAttr.RDT, null, "�ƽ�ʱ��", true, true);
                map.AddTBString(ForwardWorkAttr.Note, null, "�ƽ�ԭ��", true, true, 0, 2000, 10);

                map.AddTBInt(ForwardWorkAttr.IsRead, 0, "�Ƿ��ȡ��", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Emp + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            this.RDT = DataType.CurrentDataTime;
            return base.beforeUpdateInsertAction();
        }
		#endregion	 
	}
	/// <summary>
	/// �ƽ���¼s 
	/// </summary>
	public class ForwardWorks : Entities
	{	 
		#region ����
		/// <summary>
		/// �ƽ���¼s
		/// </summary>
		public ForwardWorks()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ForwardWork();
			}
		}
		#endregion
	}
	
}
