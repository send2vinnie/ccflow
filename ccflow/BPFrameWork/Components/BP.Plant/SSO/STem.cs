using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.SSO
{
    /// <summary>
    /// 单点登陆系统
    /// </summary>
    public class STemAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 系统类型
        /// </summary>
        public const string STemType = "STemType";
        /// <summary>
        /// 控制方法
        /// </summary>
        public const string CtrlWay = "CtrlWay";
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
    }
    /// <summary>
    /// 单点登陆系统
    /// </summary>
    public class STem : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 是否是ccSytem
        /// </summary>
        public int STemType
        {
            get
            {
                return this.GetValIntByKey(STemAttr.STemType);
            }
            set
            {
                this.SetValByKey(STemAttr.STemType, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(STemAttr.Idx);
            }
            set
            {
                this.SetValByKey(STemAttr.Idx, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 单点登陆系统
        /// </summary>
        public STem()
        {
        }
        /// <summary>
        /// 单点登陆系统
        /// </summary>
        /// <param name="mypk"></param>
        public STem(string no)
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
                Map map = new Map("SSO_STem");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(STemAttr.No, null, "编号", true, false, 2, 30, 20);
                map.AddTBString(STemAttr.Name, null, "名称", true, false, 0, 3900, 20);
                map.AddDDLSysEnum(STemAttr.STemType, 0, "系统类型", true, false,
                    STemAttr.STemType, "@0=外部系统@1=内部系统");

                map.AddDDLSysEnum(STemAttr.CtrlWay, 0, "控制方式", true, true,
                    STemAttr.CtrlWay, "@0=所有人员@1=按岗位@2=按部门@3=按人员");

                map.AddTBInt(STemAttr.Idx, 0, "显示顺序", true, false);


                map.AttrsOfOneVSM.Add(new ByStations(), new Stations(), ByStationAttr.RefObj, ByStationAttr.FK_Station, StationAttr.Name, StationAttr.No, "可访问的岗位");
                map.AttrsOfOneVSM.Add(new ByDepts(), new Depts(), ByStationAttr.RefObj, ByDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "可访问的部门");
                map.AttrsOfOneVSM.Add(new ByEmps(), new Emps(), ByStationAttr.RefObj, ByEmpAttr.FK_Emp, EmpAttr.Name, EmpAttr.No, "可访问的人员");

               // map.AttrsOfOneVSM.Add(new TemStations(), new Stations(), TemStationAttr.FK_STem, TemStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "可访问的岗位");
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 单点登陆系统s
    /// </summary>
    public class STems : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 单点登陆系统s
        /// </summary>
        public STems()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new STem();
            }
        }
        #endregion
    }
}
