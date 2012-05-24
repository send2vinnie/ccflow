using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_StationAttr : EntityNoNameAttr
    {
        public const string StaGrade = "StaGrade";
        public const string Description = "Description";
        public const string Status = "Status";
    }
    
    public partial class Port_Station : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 类型,枚举类型:1 高层岗;2 中层岗;3 执行岗;
        /// </summary>
        public int StaGrade
        {
            get
            {
                return this.GetValIntByKey(Port_StationAttr.StaGrade);
            }
            set
            {
                this.SetValByKey(Port_StationAttr.StaGrade, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(Port_StationAttr.Description);
            }
            set
            {
                this.SetValByKey(Port_StationAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(Port_StationAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_StationAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 岗位
        /// </summary>
        public Port_Station()
        {
        }
        /// <summary>
        /// 岗位
        /// </summary>
        /// <param name="No"></param>
        public Port_Station(string No)
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
                Map map = new Map("Port_Station");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "岗位";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_StationAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_StationAttr.Name, null, "名称", true, false, 0,  100, 100);
                map.AddTBInt(Port_StationAttr.StaGrade, 0, "类型,枚举类型:1 高层岗;2 中层岗;3 执行岗;", true, false);
                map.AddTBString(Port_StationAttr.Description, null, "", true, false, 0,  1000, 1000);
                map.AddTBInt(Port_StationAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Stations : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Station(); }
        }
    }
}