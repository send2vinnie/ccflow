using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NoticeAttr : EntityNoNameAttr
    {
        public const string Author = "Author";
        public const string Clicks = "Clicks";
        public const string UpUser = "UpUser";
        public const string NoticeTitle = "NoticeTitle";
        public const string NoticeSubTitle = "NoticeSubTitle";
        public const string NoticeType = "NoticeType";
        public const string NoticeContent = "NoticeContent";
        public const string CreateTime = "CreateTime";
        public const string IsRead = "IsRead";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
        public const string AccessType = "AccessType";
    }

    public partial class OA_Notice : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 发布人
        /// </summary>
        public String Author
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.Author);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.Author, value);
            }
        }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            get
            {
                return this.GetValIntByKey(OA_NoticeAttr.Clicks);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.Clicks, value);
            }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.UpUser);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.UpUser, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NoticeTitle
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.NoticeTitle);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.NoticeTitle, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NoticeSubTitle
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.NoticeSubTitle);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.NoticeSubTitle, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NoticeType
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.NoticeType);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.NoticeType, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NoticeContent
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.NoticeContent);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.NoticeContent, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String CreateTime
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.CreateTime);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.CreateTime, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IsRead
        {
            get
            {
                return this.GetValIntByKey(OA_NoticeAttr.IsRead);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.IsRead, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.UpDT);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.UpDT, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(OA_NoticeAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.Status, value);
            }
        }

        public string AccessType
        {
            get
            {
                return this.GetValStringByKey(OA_NoticeAttr.AccessType);
            }
            set
            {
                this.SetValByKey(OA_NoticeAttr.AccessType, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Notice()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Notice(string No)
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
                Map map = new Map("OA_Notice");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(OA_NoticeAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_NoticeAttr.Author, null, "发布人", true, false, 0, 50, 50);
                map.AddTBInt(OA_NoticeAttr.Clicks, 0, "点击量", true, false);
                map.AddTBString(OA_NoticeAttr.UpUser, null, "更新人", true, false, 0, 50, 50);
                map.AddTBString(OA_NoticeAttr.NoticeTitle, null, "", true, false, 0, 200, 200);
                map.AddTBString(OA_NoticeAttr.NoticeSubTitle, null, "", true, false, 0, 200, 200);
                map.AddTBString(OA_NoticeAttr.NoticeType, null, "", true, false, 0, 1, 1);
                map.AddTBStringDoc(OA_NoticeAttr.NoticeContent, "", "内容", true, false);
                map.AddTBDateTime(OA_NoticeAttr.CreateTime, "创建时间", false, false);
                map.AddTBInt(OA_NoticeAttr.IsRead, 0, "", true, false);
                map.AddTBDateTime(OA_NoticeAttr.UpDT, "更新时间", false, false);
                map.AddTBString(OA_NoticeAttr.AccessType, null, "访问类型", true, false, 0, 4, 20);
                map.AddTBInt(OA_NoticeAttr.Status, 0, "", true, false);


                this._enMap = map;
                return this._enMap;
            }
        }
    }


    public partial class OA_Notices : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Notice(); }
        }
    }
}