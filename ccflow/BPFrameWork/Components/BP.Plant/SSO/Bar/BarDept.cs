using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// ��Ϣ�鲿��
	/// </summary>
	public class BarDeptAttr  
	{
		#region ��������
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";		 
		#endregion	
	}
	/// <summary>
    /// ��Ϣ�鲿�� ��ժҪ˵����
	/// </summary>
    public class BarDept : Entity
    {
        #region ����Ȩ�޿���
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion ����Ȩ�޿���

        #region ��������
        /// <summary>
        /// ��Ϣ��
        /// </summary>
        public string FK_Bar
        {
            get
            {
                return this.GetValStringByKey(BarDeptAttr.FK_Bar);
            }
            set
            {
                SetValByKey(BarDeptAttr.FK_Bar, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(BarDeptAttr.FK_Dept);
            }
        }
        /// <summary>
        ///����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(BarDeptAttr.FK_Dept);
            }
            set
            {
                SetValByKey(BarDeptAttr.FK_Dept, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��Ϣ�鲿��
        /// </summary> 
        public BarDept() { }
        /// <summary>
        /// ��Ϣ�鲿��
        /// </summary>
        /// <param name="_Temoid"></param>
        /// <param name="wsNo"></param>
        public BarDept(string fk_bar, string FK_Dept)
        {
            this.FK_Bar = fk_bar;
            this.FK_Dept = FK_Dept;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("SSO_BarDept");
                map.EnDesc = "��Ϣ�鲿��";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarDeptAttr.FK_Bar, null, "��Ϣ��", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarDeptAttr.FK_Dept, null, "����", new Depts(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ�鲿�� 
	/// </summary>
	public class BarDepts : Entities
	{
		#region ����
		/// <summary>
        /// ��Ϣ�鲿��
		/// </summary>
		public BarDepts()
		{
		}
		/// <summary>
        /// ��Ϣ�鲿��s
		/// </summary>
		public BarDepts(string DeptNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarDeptAttr.FK_Dept, DeptNo);
			qo.DoQuery();
		}		 
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BarDept();
			}
		}	
		#endregion 
	}
}
