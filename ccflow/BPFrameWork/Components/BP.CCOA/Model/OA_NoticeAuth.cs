using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NoticeAuthAttr : EntityNoNameAttr
    {
        public const string FK_Notice = "FK_Notice";
        public const string FK_Ids = "FK_Ids";
    }
    
    public partial class OA_NoticeAuth : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// Notice公告外键
        /// </summary>
        public String FK_Notice
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAuthAttr.FK_Notice);
            }
            set
            {
                this.SetValByKey(OA_NoticeAuthAttr.FK_Notice, value);
            }
        }
        
        /// <summary>
        /// 可以访问的主键集合，以|分隔
        /// </summary>
        public String FK_Ids
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAuthAttr.FK_Ids);
            }
            set
            {
                this.SetValByKey(OA_NoticeAuthAttr.FK_Ids, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_NoticeAuth()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_NoticeAuth(string No)
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
                Map map = new Map("OA_NoticeAuth");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_NoticeAuthAttr.No, null, "主键No", true, true, 0, 50, 50);
                map.AddTBString(OA_NoticeAuthAttr.FK_Notice, null, "Notice公告外键", true, false, 0,  50, 50);
                map.AddTBString(OA_NoticeAuthAttr.FK_Ids, null, "可以访问的主键集合，以|分隔", true, false, 0,  4000, 4000);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_NoticeAuths : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_NoticeAuth(); }
        }
    }
}