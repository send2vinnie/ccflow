using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_AttachmentAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Suffix = "Suffix";
        public const string Uploader = "Uploader";
        public const string AttachmentName = "AttachmentName";
        public const string FileNeme = "FileNeme";
        public const string FilePath = "FilePath";
        public const string CreateTime = "CreateTime";
        public const string IsDel = "IsDel";
        public const string Remarks = "Remarks";
    }
    
    public partial class OA_Attachment : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.No);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.No, value);
            }
        }
        
        /// <summary>
        /// 后缀（类型）
        /// </summary>
        public String Suffix
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.Suffix);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.Suffix, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Uploader
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.Uploader);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.Uploader, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String AttachmentName
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.AttachmentName);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.AttachmentName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FileNeme
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.FileNeme);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.FileNeme, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FilePath
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.FilePath);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.FilePath, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String CreateTime
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.CreateTime);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.CreateTime, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int IsDel
        {
            get
            {
                return this.GetValIntByKey(OA_AttachmentAttr.IsDel);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.IsDel, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Remarks
        {
            get
            {
                return this.GetValStringByKey(OA_AttachmentAttr.Remarks);
            }
            set
            {
                this.SetValByKey(OA_AttachmentAttr.Remarks, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Attachment()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Attachment(string No)
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
                Map map = new Map("OA_Attachment");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_AttachmentAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(OA_AttachmentAttr.Suffix, null, "后缀（类型）", true, false, 0,  10, 10);
                map.AddTBString(OA_AttachmentAttr.Uploader, null, "", true, false, 0,  10, 10);
                map.AddTBString(OA_AttachmentAttr.AttachmentName, null, "", true, false, 0,  100, 100);
                map.AddTBString(OA_AttachmentAttr.FileNeme, null, "", true, false, 0,  200, 200);
                map.AddTBString(OA_AttachmentAttr.FilePath, null, "", true, false, 0,  1000, 1000);
                map.AddTBString(OA_AttachmentAttr.CreateTime, null, "", true, false, 0,  50, 50);
                map.AddTBInt(OA_AttachmentAttr.IsDel, 0, "", true, false);
                map.AddTBString(OA_AttachmentAttr.Remarks, null, "", true, false, 0,  1000, 1000);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Attachments : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Attachment(); }
        }
    }
}