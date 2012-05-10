using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_MenuOperateAttr : EntityNoNameAttr
    {
        public const string Menu_Id = "Menu_Id";
        public const string Operate_Id = "Operate_Id";
        public const string No = "No";
    }
    
    public partial class EIP_MenuOperate : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Menu_Id
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuOperateAttr.Menu_Id);
            }
            set
            {
                this.SetValByKey(EIP_MenuOperateAttr.Menu_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Operate_Id
        {
            get
            {
                return this.GetValIntByKey(EIP_MenuOperateAttr.Operate_Id);
            }
            set
            {
                this.SetValByKey(EIP_MenuOperateAttr.Operate_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuOperateAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_MenuOperateAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_MenuOperate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_MenuOperate(string No)
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
                Map map = new Map("EIP_MenuOperate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(EIP_MenuOperateAttr.Menu_Id, null, "", true, false, 0,  10, 10);
                map.AddTBInt(EIP_MenuOperateAttr.Operate_Id, 0, "", true, false);
                map.AddTBStringPK(EIP_MenuOperateAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_MenuOperates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_MenuOperate(); }
        }
    }
}