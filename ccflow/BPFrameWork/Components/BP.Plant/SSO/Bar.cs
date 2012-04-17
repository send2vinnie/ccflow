using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.SSO
{
    /// <summary>
    /// 信息块
    /// </summary>
    public class BarAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 信息块类型
        /// </summary>
        public const string BarType = "BarType";
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// tag1
        /// </summary>
        public const string Tag1 = "Tag1";
        public const string Tag2 = "Tag2";
        public const string Tag3 = "Tag3";
        /// <summary>
        /// 是否可删除
        /// </summary>
        public const string IsDel = "IsDel";
        /// <summary>
        /// 控制方式
        /// </summary>
        public const string CtrlWay = "CtrlWay";
    }
    /// <summary>
    /// 信息块
    /// </summary>
    public class Bar : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 是否是ccSytem
        /// </summary>
        public int BarType
        {
            get
            {
                return this.GetValIntByKey(BarAttr.BarType);
            }
            set
            {
                this.SetValByKey(BarAttr.BarType, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(BarAttr.Idx);
            }
            set
            {
                this.SetValByKey(BarAttr.Idx, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 信息块
        /// </summary>
        public Bar()
        {
        }
        /// <summary>
        /// 信息块
        /// </summary>
        /// <param name="mypk"></param>
        public Bar(string no)
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
                Map map = new Map("SSO_Bar");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "信息块";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(BarAttr.No, null, "编号", true, true, 2, 30, 20);
                map.AddTBString(BarAttr.Name, null, "名称", true, false, 0, 3900, 20);

                map.AddTBString(BarAttr.Title, null, "标题", true, false, 0, 3900, 20);
                map.AddDDLSysEnum(BarAttr.BarType, 0, "信息块类型", true, false, 
                    BarAttr.BarType, "@0=标题消息列表@1=其它");

                map.AddTBString(BarAttr.Tag1, null, "Tag1", true, false, 0, 3900, 20,true);
                map.AddTBString(BarAttr.Tag2, null, "Tag2", true, false, 0, 3900, 20, true);
                map.AddTBString(BarAttr.Tag3, null, "Tag3", true, false, 0, 3900, 20, true);

                map.AddBoolean(BarAttr.IsDel, true, "用户是否可删除",true,true,true);

                map.AddDDLSysEnum(BarAttr.CtrlWay, 0, "控制方式", true, true,
                    BarAttr.CtrlWay, "@0=所有人员@1=按岗位@2=按部门@3=按人员");

                map.AddTBInt(BarAttr.Idx, 0, "显示顺序", false, true);

                map.AttrsOfOneVSM.Add(new ByStations(), new Stations(), ByStationAttr.RefObj, ByStationAttr.FK_Station, StationAttr.Name, StationAttr.No, "可访问的岗位");
                map.AttrsOfOneVSM.Add(new ByDepts(), new Depts(), ByStationAttr.RefObj, ByDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "可访问的部门");
                map.AttrsOfOneVSM.Add(new ByEmps(), new Emps(), ByStationAttr.RefObj, ByEmpAttr.FK_Emp, EmpAttr.Name, EmpAttr.No, "可访问的人员");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 信息块s
    /// </summary>
    public class Bars : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 信息块s
        /// </summary>
        public Bars()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Bar();
            }
        }
        #endregion
    }
}
