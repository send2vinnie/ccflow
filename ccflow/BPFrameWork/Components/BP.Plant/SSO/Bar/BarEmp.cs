using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// ��Ϣ����Ա
	/// </summary>
	public class BarEmpAttr  
	{
		#region ��������
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// ������Ա
		/// </summary>
		public const  string FK_Emp="FK_Emp";		 
		#endregion	
	}
	/// <summary>
    /// ��Ϣ����Ա ��ժҪ˵����
	/// </summary>
    public class BarEmp : Entity
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
                return this.GetValStringByKey(BarEmpAttr.FK_Bar);
            }
            set
            {
                SetValByKey(BarEmpAttr.FK_Bar, value);
            }
        }
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(BarEmpAttr.FK_Emp);
            }
        }
        /// <summary>
        ///������Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(BarEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(BarEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��Ϣ����Ա
        /// </summary> 
        public BarEmp() { }
        /// <summary>
        /// ��Ϣ����Ա
        /// </summary>
        /// <param name="_Temoid"></param>
        /// <param name="wsNo"></param>
        public BarEmp(string fk_bar, string FK_Emp)
        {
            this.FK_Bar = fk_bar;
            this.FK_Emp = FK_Emp;
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

                Map map = new Map("SSO_BarEmp");
                map.EnDesc = "��Ϣ����Ա";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarEmpAttr.FK_Bar, null, "��Ϣ��", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarEmpAttr.FK_Emp, null, "������Ա", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ����Ա 
	/// </summary>
	public class BarEmps : Entities
	{
		#region ����
		/// <summary>
        /// ��Ϣ����Ա
		/// </summary>
		public BarEmps()
		{
		}
		/// <summary>
        /// ��Ϣ����Աs
		/// </summary>
		public BarEmps(string EmpNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarEmpAttr.FK_Emp, EmpNo);
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
				return new BarEmp();
			}
		}	
		#endregion 
	}
}
