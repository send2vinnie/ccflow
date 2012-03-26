using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.DevDemo
{
    /// <summary>
    /// ѧ��
    /// </summary>
    public class StudentAttr : EntityNoNameAttr
    {
        /// <summary>
        /// Addr
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_SF = "FK_SF";
        /// <summary>
        /// Email
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// �Ա�
        /// </summary>
        public const string Sex = "Sex";
        /// <summary>
        /// ��������
        /// </summary>
        public const string BDT = "BDT";
        /// <summary>
        /// Age
        /// </summary>
        public const string Age = "Age";
    }
    /// <summary>
    /// ѧ��
    /// </summary>
    public class Student : EntityNoName
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public string BDT
        {
            get
            {
                return this.GetValStringByKey(StudentAttr.BDT);
            }
            set
            {
                this.SetValByKey(StudentAttr.BDT, value);
            }
        }
        /// <summary>
        /// �Ա�
        /// </summary>
        public int Sex
        {
            get
            {
                return this.GetValIntByKey(StudentAttr.Sex);
            }
            set
            {
                this.SetValByKey(StudentAttr.Sex, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_SF
        {
            get
            {
                return this.GetValStrByKey(StudentAttr.FK_SF);
            }
            set
            {
                this.SetValByKey(StudentAttr.FK_SF, value);
            }
        }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStrByKey(StudentAttr.Addr);
            }
            set
            {
                this.SetValByKey(StudentAttr.Addr, value);
            }
        }
        /// <summary>
        /// �ʼ�
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(StudentAttr.Email);
            }
            set
            {
                this.SetValByKey(StudentAttr.Email, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Age
        {
            get
            {
                return this.GetValIntByKey(StudentAttr.Age);
            }
            set
            {
                this.SetValByKey(StudentAttr.Age, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ѧ��
        /// </summary>
        public Student()
        {
        }
        /// <summary>
        /// ѧ��
        /// </summary>
        /// <param name="mypk"></param>
        public Student(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Demo_Student");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ѧ��";

                // ��������,
                map.AddTBStringPK(StudentAttr.No, null, "���", true, false, 4, 4, 4); 
                map.AddTBString(StudentAttr.Name, null, "����", true, false, 0, 500, 20);

                map.AddDDLEntities(StudentAttr.FK_SF, null, "����", new BP.CN.SFs(),true);
                map.AddDDLSysEnum(StudentAttr.Sex, 0, "�Ա�", false, false, StudentAttr.Sex, "@0=Ů@1=��");

                map.AddTBDate(StudentAttr.BDT, null, "��������", true, false);
                map.AddTBInt(StudentAttr.Age, 0, "����", true, false);

                map.AddTBString(StudentAttr.Addr, null, "��ַ", true, false, 0, 500, 20,true);
                map.AddTBString(StudentAttr.Email, null, "Email", true, false, 0, 20, 20,true);

                //map.AddTBString(StudentAttr.BDT, "black", "BDT", true, false, 0, 50, 20);
                //map.AddTBString(StudentAttr.FontName, null, "FontName", true, false, 0, 50, 20);
                //map.AddTBString(StudentAttr.FontStyle, "normal", "FontStyle", true, false, 0, 50, 20);
                //map.AddTBInt(StudentAttr.Age, 0, "Age", false, false);
                //map.AddTBInt(StudentAttr.IsItalic, 0, "IsItalic", false, false);

                map.AddSearchAttr(StudentAttr.FK_SF);
                map.AddSearchAttr(StudentAttr.Age);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ��д��������ҵ���߼�,�����׳��쳣��.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            // ��������.
            string bdt = this.BDT.Substring(0, 4);
            string t = DateTime.Now.ToString("yy");

            if (bdt.Substring(0, 2) == "19")
                this.Age = 2000 - int.Parse(t) + int.Parse(t);
            else
                this.Age = int.Parse(t) - int.Parse(bdt);

            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ѧ��s
    /// </summary>
    public class Students : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ѧ��s
        /// </summary>
        public Students()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Student();
            }
        }
        #endregion
    }
}
