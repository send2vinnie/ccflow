using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NoticeAttachAttr : EntityNoNameAttr
    {
        public const string Notice_Id = "Notice_Id";
        public const string Accachment_Id = "Accachment_Id";
        public const string No = "No";
    }
    
    public partial class OA_NoticeAttach : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Notice_Id
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttachAttr.Notice_Id);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttachAttr.Notice_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Accachment_Id
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttachAttr.Accachment_Id);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttachAttr.Accachment_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttachAttr.No);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttachAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_NoticeAttach()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_NoticeAttach(string No)
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
                Map map = new Map("OA_NoticeAttach");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(OA_NoticeAttachAttr.Notice_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_NoticeAttachAttr.Accachment_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(OA_NoticeAttachAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_NoticeAttachs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_NoticeAttach(); }
        }
    }
}