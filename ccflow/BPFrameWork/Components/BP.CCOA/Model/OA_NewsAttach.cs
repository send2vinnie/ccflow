using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NewsAttachAttr : EntityNoNameAttr
    {
        public const string News_Id = "News_Id";
        public const string Attachment_Id = "Attachment_Id";
        public const string No = "No";
    }
    
    public partial class OA_NewsAttach : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String News_Id
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttachAttr.News_Id);
            }
            set
            {
                this.SetValByKey(OA_NewsAttachAttr.News_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Attachment_Id
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttachAttr.Attachment_Id);
            }
            set
            {
                this.SetValByKey(OA_NewsAttachAttr.Attachment_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttachAttr.No);
            }
            set
            {
                this.SetValByKey(OA_NewsAttachAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_NewsAttach()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_NewsAttach(string No)
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
                Map map = new Map("OA_NewsAttach");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(OA_NewsAttachAttr.News_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_NewsAttachAttr.Attachment_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(OA_NewsAttachAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_NewsAttachs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_NewsAttach(); }
        }
    }
}