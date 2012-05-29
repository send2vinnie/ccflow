using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_EmpStationAttr : EntityNoNameAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string FK_Station = "FK_Station";
    }

    public partial class Port_EmpStation : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Emp
        {
            get
            {
                return this.GetValStringByKey(Port_EmpStationAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(Port_EmpStationAttr.FK_Emp, value);
            }
        }
        
        /// <summary>
        /// 工作岗位, 主外键:对应物理表:Port_Station,表描述:岗位
        /// </summary>
        public String FK_Station
        {
            get
            {
                return this.GetValStringByKey(Port_EmpStationAttr.FK_Station);
            }
            set
            {
                this.SetValByKey(Port_EmpStationAttr.FK_Station, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 人员岗位
        /// </summary>
        public Port_EmpStation()
        {
        }
        /// <summary>
        /// 人员岗位
        /// </summary>
        /// <param name="No"></param>
        public Port_EmpStation(string No)
        {
            this.No = No;
            this.Retrieve();
        }
        #endregion
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_EmpStation");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "人员岗位";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_EmpStationAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_EmpStationAttr.FK_Emp, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_EmpStationAttr.FK_Station, null, "工作岗位, 主外键:对应物理表:Port_Station,表描述:岗位", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_EmpStations : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_EmpStation(); }
        }
    }
}