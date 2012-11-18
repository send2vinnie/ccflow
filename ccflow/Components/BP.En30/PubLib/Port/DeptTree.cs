using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.Port
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DeptTreeAttr : EntityTreeAttr
    {
    }
    /// <summary>
    /// ����
    /// </summary>
    public class DeptTree : EntityTree
    {
        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public DeptTree() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id">���</param>
        public DeptTree(string id) : base(id) { }
        #endregion

        #region ��д����
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //���ӵ����Ǹ����ݿ���. (Ĭ�ϵ���: AppCenterDSN )
                map.PhysicsTable = "Port_DeptTree";
                map.EnType = EnType.Admin;
                map.EnDesc = "����"; // "����";// ʵ�������.
                map.DepositaryOfEntity = Depositary.Application; //ʵ��map�Ĵ��λ��.
                map.DepositaryOfMap = Depositary.Application;    // Map �Ĵ��λ��.
                map.CodeStruct = "22222222";
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.AdjunctType = AdjunctType.None;

                map.AddTBStringPK(DeptTreeAttr.No, null, this.ToE("No", "���"), true, false, 1, 20, 20);
                map.AddTBString(DeptTreeAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 30);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    ///�õ�����
    /// </summary>
    public class DeptTrees : EntitiesNoName
    {
        /// <summary>
        /// ��ѯȫ����
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {

            if (Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptTreeAttr.No, " like ", Web.WebUser.FK_DeptTree + "%");
            return qo11.DoQuery();
        }
        /// <summary>
        /// �õ�һ����ʵ��
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DeptTree();
            }
        }
        /// <summary>
        /// ���ż���
        /// </summary>
        public DeptTrees()
        {
        }
    }
}
