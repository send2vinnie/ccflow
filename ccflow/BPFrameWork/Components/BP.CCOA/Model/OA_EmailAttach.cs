using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_EmailAttachAttr : EntityNoNameAttr
    {
        public const string Email_Id = "Email_Id";
        public const string Attachment_Id = "Attachment_Id";
        public const string No = "No";
    }
    
    public partial class OA_EmailAttach : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Email_Id
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttachAttr.Email_Id);
            }
            set
            {
                this.SetValByKey(OA_EmailAttachAttr.Email_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Attachment_Id
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttachAttr.Attachment_Id);
            }
            set
            {
                this.SetValByKey(OA_EmailAttachAttr.Attachment_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttachAttr.No);
            }
            set
            {
                this.SetValByKey(OA_EmailAttachAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_EmailAttach()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_EmailAttach(string No)
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
                Map map = new Map("OA_EmailAttach");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(OA_EmailAttachAttr.Email_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_EmailAttachAttr.Attachment_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(OA_EmailAttachAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_EmailAttachs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_EmailAttach(); }
        }
    }
}