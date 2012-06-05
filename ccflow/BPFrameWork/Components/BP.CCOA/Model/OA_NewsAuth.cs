using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NewsAuthAttr : EntityNoNameAttr
    {
        public const string FK_News = "FK_News";
        public const string FK_Id = "FK_Id";
    }
    
    public partial class OA_NewsAuth : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 新闻外键
        /// </summary>
        public String FK_News
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAuthAttr.FK_News);
            }
            set
            {
                this.SetValByKey(OA_NewsAuthAttr.FK_News, value);
            }
        }
        
        /// <summary>
        /// 可以访问的主键
        /// </summary>
        public String FK_Id
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAuthAttr.FK_Id);
            }
            set
            {
                this.SetValByKey(OA_NewsAuthAttr.FK_Id, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_NewsAuth()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_NewsAuth(string No)
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
                Map map = new Map("OA_NewsAuth");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_NewsAuthAttr.No, null, "主键No", true, true, 0, 50, 50);
                map.AddTBString(OA_NewsAuthAttr.FK_News, null, "新闻外键", true, false, 0,  50, 50);
                map.AddTBString(OA_NewsAuthAttr.FK_Id, null, "可以访问的主键", true, false, 0,  4000, 4000);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_NewsAuths : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_NewsAuth(); }
        }
    }
}