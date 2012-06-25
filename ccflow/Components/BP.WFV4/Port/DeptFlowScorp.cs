using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.WF.Port
{
    /// <summary>
    /// �������ݲ�ѯȨ��
    /// </summary>
    public class DeptFlowScorpAttr
    {
        #region ��������
        /// <summary>
        /// ������ԱID
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        #endregion
    }
    /// <summary>
    /// �������ݲ�ѯȨ�� ��ժҪ˵����
    /// </summary>
    public class DeptFlowScorp : Entity
    {
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsView = true;
                    uac.IsDelete = true;
                    uac.IsInsert = true;
                    uac.IsUpdate = true;
                    uac.IsAdjunct = true;
                }
                return uac;
            }
        }

        #region ��������
        /// <summary>
        /// ������ԱID
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DeptFlowScorpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptFlowScorpAttr.FK_Emp, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(DeptFlowScorpAttr.FK_Dept);
            }
        }
        /// <summary>
        ///����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptFlowScorpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptFlowScorpAttr.FK_Dept, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// �������ݲ�ѯȨ��
        /// </summary> 
        public DeptFlowScorp() { }
        /// <summary>
        /// �������ݲ�ѯȨ��
        /// </summary>
        /// <param name="_empoid">������ԱID</param>
        /// <param name="wsNo">���ű��</param> 	
        public DeptFlowScorp(string _empoid, string wsNo)
        {
            this.FK_Emp = _empoid;
            this.FK_Dept = wsNo;
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

                Map map = new Map("Port_DeptFlowScorp");
                map.EnDesc = "�������ݲ�ѯȨ��";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(DeptFlowScorpAttr.FK_Emp, null, "����Ա", true, true, 1, 50, 11);
                map.AddDDLEntitiesPK(DeptFlowScorpAttr.FK_Dept, null, "����", new BP.WF.Port.Depts(), true);
                // map.AddDDLEntitiesPK(DeptFlowScorpAttr.FK_Emp, null, "����Ա", new Emps(), true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ���ػ��෽��
        /// <summary>
        /// ����ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeInsert()
        {
            return base.beforeInsert();
        }
        /// <summary>
        /// ����ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeUpdate()
        {
            return base.beforeUpdate();
        }
        /// <summary>
        /// ɾ��ǰ�����Ĺ���
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeDelete()
        {
            return base.beforeDelete();
        }
        #endregion
    }
    /// <summary>
    /// �������ݲ�ѯȨ�� 
    /// </summary>
    public class DeptFlowScorps : Entities
    {
        #region ����
        /// <summary>
        /// �������ݲ�ѯȨ��
        /// </summary>
        public DeptFlowScorps() { }
        /// <summary>
        /// �������ݲ�ѯȨ��
        /// </summary>
        /// <param name="FK_Emp">FK_Emp</param>
        public DeptFlowScorps(string FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptFlowScorpAttr.FK_Emp, FK_Emp);
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
                return new DeptFlowScorp();
            }
        }
        #endregion

        #region ��ѯ����
        #endregion
    }
}
