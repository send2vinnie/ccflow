using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.DevDemo
{
    /// <summary>
    /// 学生
    /// </summary>
    public class StudentAttr : EntityNoNameAttr
    {
        /// <summary>
        /// Addr
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_SF = "FK_SF";
        /// <summary>
        /// Email
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 性别
        /// </summary>
        public const string Sex = "Sex";
        /// <summary>
        /// 出生年月
        /// </summary>
        public const string BDT = "BDT";
        /// <summary>
        /// Age
        /// </summary>
        public const string Age = "Age";
    }
    /// <summary>
    /// 学生
    /// </summary>
    public class Student : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 出生日期
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
        /// 性别
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
        /// 籍贯
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
        /// 地址
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
        /// 邮件
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
        /// 年龄
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

        #region 构造方法
        /// <summary>
        /// 学生
        /// </summary>
        public Student()
        {
        }
        /// <summary>
        /// 学生
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
                map.EnDesc = "学生";

                // 增加主键,
                map.AddTBStringPK(StudentAttr.No, null, "编号", true, false, 4, 4, 4); 
                map.AddTBString(StudentAttr.Name, null, "姓名", true, false, 0, 500, 20);

                map.AddDDLEntities(StudentAttr.FK_SF, null, "籍贯", new BP.CN.SFs(),true);
                map.AddDDLSysEnum(StudentAttr.Sex, 0, "性别", false, false, StudentAttr.Sex, "@0=女@1=男");

                map.AddTBDate(StudentAttr.BDT, null, "出生年月", true, false);
                map.AddTBInt(StudentAttr.Age, 0, "年龄", true, false);

                map.AddTBString(StudentAttr.Addr, null, "地址", true, false, 0, 500, 20,true);
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
        /// 重写方法处理业务逻辑,可以抛出异常来.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            // 计算年龄.
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
    /// 学生s
    /// </summary>
    public class Students : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 学生s
        /// </summary>
        public Students()
        {
        }
        /// <summary>
        /// 得到它的 Entity
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
