using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF.Demo
{
    /// <summary>
    /// ����Ա ����
    /// </summary>
    public class EmpDemoAttr:EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// �绰
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// �ʼ�
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// �Ա�
        /// </summary>
        public const string XB = "XB";
        /// <summary>
        /// ��ַ
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsEnable = "IsEnable";
        #endregion
    }
    /// <summary>
    /// ����Ա
    /// </summary>
    public class EmpDemo : EntityNoName
    {
        #region ����
        public string Email
        {
            get
            {
                return this.GetValStringByKey(EmpDemoAttr.Email);
            }
            set
            {
                this.SetValByKey(EmpDemoAttr.Email, value);
            }
        }
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(EmpDemoAttr.Addr);
            }
            set
            {
                this.SetValByKey(EmpDemoAttr.Addr, value);
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(EmpDemoAttr.Tel);
            }
            set
            {
                this.SetValByKey(EmpDemoAttr.Tel, value);
            }
        }
        public int XB
        {
            get
            {
                return this.GetValIntByKey(EmpDemoAttr.XB);
            }
            set
            {
                this.SetValByKey(EmpDemoAttr.XB, value);
            }
        }
        public string XB_Text
        {
            get
            {
                return this.GetValRefTextByKey(EmpDemoAttr.XB);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(EmpDemoAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(EmpDemoAttr.FK_Dept, value);
            }
        }
        public string FK_Dept_Text
        {
            get
            {
                return this.GetValStringByKey("FK_Dept");
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����Ա
        /// </summary>
        public EmpDemo()
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
                Map map = new Map("Demo_EmpDemo");
                map.EnDesc = "����Ա";

                map.AddTBStringPK(EmpDemoAttr.No,null,"���",true,false,1,40,4);
                map.AddTBString(EmpDemoAttr.Name, null, "name", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Tel, null, "�绰", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Email, null, "Email", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Addr, null, "Addr", true, false, 0, 200, 10);
                map.AddBoolean(EmpDemoAttr.IsEnable, true, "�Ƿ�����", true, true);
                map.AddDDLSysEnum(EmpDemoAttr.XB, 0, "�Ա�", true,true,"XB","@0=Ů@1=��");
                map.AddDDLEntities(EmpDemoAttr.FK_Dept, null, "����", new BP.Port.Depts(), true);

                map.AddTBInt("Age", 90, "Age", true, false);

                map.AddSearchAttr(EmpDemoAttr.XB);
                map.AddSearchAttr(EmpDemoAttr.FK_Dept);

                //RefMethod rm = new RefMethod();
                //rm.Title = "��sina";
                //map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ����Աs
    /// </summary>
    public class EmpDemos : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EmpDemo();
            }
        }
        /// <summary>
        /// ����Աs
        /// </summary>
        public EmpDemos() { }
        #endregion
    }
}