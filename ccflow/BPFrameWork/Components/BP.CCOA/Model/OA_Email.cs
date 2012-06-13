using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_EmailAttr : EntityNoNameAttr
    {
        public const string Addresser = "Addresser";
        public const string Addressee = "Addressee";
        public const string Subject = "Subject";
        public const string Content = "Content";
        public const string PriorityLevel = "PriorityLevel";
        public const string Category = "Category";
        public const string CreateTime = "CreateTime";
        public const string SendTime = "SendTime";
        public const string IsDel = "IsDel";
        public const string UpDT = "UpDT";
        public const string IsRead = "IsRead";
    }

    public partial class OA_Email : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 发件人
        /// </summary>
        public String Addresser
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.Addresser);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.Addresser, value);
            }
        }

        /// <summary>
        /// 收件人
        /// </summary>
        public String Addressee
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.Addressee);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.Addressee, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Subject
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.Subject);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.Subject, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Content
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.Content);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.Content, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PriorityLevel
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.PriorityLevel);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.PriorityLevel, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Category
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.Category);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.Category, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String CreateTime
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.CreateTime);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.CreateTime, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String SendTime
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.SendTime);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.SendTime, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IsDel
        {
            get
            {
                return this.GetValIntByKey(OA_EmailAttr.IsDel);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.IsDel, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAttr.UpDT);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.UpDT, value);
            }
        }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int IsRead
        {
            get
            {
                return this.GetValIntByKey(OA_EmailAttr.IsRead);
            }
            set
            {
                this.SetValByKey(OA_EmailAttr.IsRead, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Email()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Email(string No)
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
                Map map = new Map("OA_Email");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(OA_EmailAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_EmailAttr.Addresser, null, "发件人", true, false, 0, 10, 10);
                map.AddTBString(OA_EmailAttr.Addressee, null, "收件人", true, false, 0, 1000, 1000);
                map.AddTBString(OA_EmailAttr.Subject, null, "", true, false, 0, 200, 200);
                map.AddTBString(OA_EmailAttr.Content, null, "", true, false, 0, 16, 16);
                map.AddTBString(OA_EmailAttr.PriorityLevel, null, "", true, false, 0, 1, 1);
                map.AddTBString(OA_EmailAttr.Category, null, "", true, false, 0, 1, 1);
                map.AddTBString(OA_EmailAttr.CreateTime, null, "", true, false, 0, 50, 50);
                map.AddTBString(OA_EmailAttr.SendTime, null, "", true, false, 0, 50, 50);
                map.AddTBInt(OA_EmailAttr.IsDel, 0, "", true, false);
                map.AddTBString(OA_EmailAttr.UpDT, null, "", true, false, 0, 50, 50);
                map.AddTBInt(OA_EmailAttr.IsRead, 0, "", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class OA_Emails : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Email(); }
        }
    }
}