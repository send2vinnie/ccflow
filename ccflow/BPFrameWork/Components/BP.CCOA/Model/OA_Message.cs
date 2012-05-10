using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_MessageAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string MeaageType = "MeaageType";
        public const string Author = "Author";
        public const string MessageName = "MessageName";
        public const string CreateTime = "CreateTime";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
    }
    
    public partial class OA_Message : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.No);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.No, value);
            }
        }
        
        /// <summary>
        /// 消息类型
        /// </summary>
        public String MeaageType
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.MeaageType);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.MeaageType, value);
            }
        }
        
        /// <summary>
        /// 发布人
        /// </summary>
        public String Author
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.Author);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.Author, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String MessageName
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.MessageName);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.MessageName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String CreateTime
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.CreateTime);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.CreateTime, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAttr.UpDT);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(OA_MessageAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_MessageAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Message()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Message(string No)
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
                Map map = new Map("OA_Message");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_MessageAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_MessageAttr.MeaageType, null, "消息类型", true, false, 0,  50, 50);
                map.AddTBString(OA_MessageAttr.Author, null, "发布人", true, false, 0,  50, 50);
                map.AddTBString(OA_MessageAttr.MessageName, null, "", true, false, 0,  100, 100);
                map.AddTBString(OA_MessageAttr.CreateTime, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_MessageAttr.UpDT, null, "", true, false, 0,  50, 50);
                map.AddTBInt(OA_MessageAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Messages : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Message(); }
        }
    }
}