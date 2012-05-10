using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_AddrGroupingAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string GroupingName = "GroupingName";
    }
    
    public partial class OA_AddrGrouping : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_AddrGroupingAttr.No);
            }
            set
            {
                this.SetValByKey(OA_AddrGroupingAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String GroupingName
        {
            get
            {
                return this.GetValStringByKey(OA_AddrGroupingAttr.GroupingName);
            }
            set
            {
                this.SetValByKey(OA_AddrGroupingAttr.GroupingName, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_AddrGrouping()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_AddrGrouping(string No)
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
                Map map = new Map("OA_AddrGrouping");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_AddrGroupingAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(OA_AddrGroupingAttr.GroupingName, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_AddrGroupings : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_AddrGrouping(); }
        }
    }
}