using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF.Demo
{
    /// <summary>
    /// 操作员 属性
    /// </summary>
    public class EmpDemoAttr:EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 电话
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// 邮件
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 性别
        /// </summary>
        public const string XB = "XB";
        /// <summary>
        /// 地址
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 是否启用
        /// </summary>
        public const string IsEnable = "IsEnable";
        #endregion
    }
    /// <summary>
    /// 操作员
    /// </summary>
    public class EmpDemo : EntityNoName
    {
        #region 属性
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

        #region 构造函数
        /// <summary>
        /// 操作员
        /// </summary>
        public EmpDemo()
        {
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Demo_EmpDemo");
                map.EnDesc = "操作员";

                map.AddTBStringPK(EmpDemoAttr.No,null,"编号",true,false,1,40,4);
                map.AddTBString(EmpDemoAttr.Name, null, "name", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Tel, null, "电话", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Email, null, "Email", true, false, 0, 200, 10);
                map.AddTBString(EmpDemoAttr.Addr, null, "Addr", true, false, 0, 200, 10);
                map.AddBoolean(EmpDemoAttr.IsEnable, true, "是否启用", true, true);
                map.AddDDLSysEnum(EmpDemoAttr.XB, 0, "性别", true,true,"XB","@0=女@1=男");
                map.AddDDLEntities(EmpDemoAttr.FK_Dept, null, "部门", new BP.Port.Depts(), true);

                map.AddTBInt("Age", 90, "Age", true, false);

                map.AddSearchAttr(EmpDemoAttr.XB);
                map.AddSearchAttr(EmpDemoAttr.FK_Dept);

                //RefMethod rm = new RefMethod();
                //rm.Title = "打开sina";
                //map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 操作员s
    /// </summary>
    public class EmpDemos : EntitiesNoName
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EmpDemo();
            }
        }
        /// <summary>
        /// 操作员s
        /// </summary>
        public EmpDemos() { }
        #endregion
    }
}