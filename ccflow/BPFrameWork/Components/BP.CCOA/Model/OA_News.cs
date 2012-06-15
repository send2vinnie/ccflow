using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_NewsAttr : EntityNoNameAttr
    {
        public const string Author = "Author";
        public const string Clicks = "Clicks";
        public const string UpUser = "UpUser";
        public const string NewsTitle = "NewsTitle";
        public const string NewsSubTitle = "NewsSubTitle";
        public const string NewsType = "NewsType";
        public const string NewsContent = "NewsContent";
        public const string CreateTime = "CreateTime";
        public const string IsRead = "IsRead";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
        public const string AccessType = "AccessType";
    }

    public partial class OA_News : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 发布人
        /// </summary>
        public String Author
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.Author);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.Author, value);
            }
        }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            get
            {
                return this.GetValIntByKey(OA_NewsAttr.Clicks);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.Clicks, value);
            }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.UpUser);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.UpUser, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NewsTitle
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.NewsTitle);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.NewsTitle, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NewsSubTitle
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.NewsSubTitle);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.NewsSubTitle, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NewsType
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.NewsType);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.NewsType, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String NewsContent
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.NewsContent);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.NewsContent, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.CreateTime);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.CreateTime, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead
        {
            get
            {
                return this.GetValBooleanByKey(OA_NewsAttr.IsRead);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.IsRead, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpDT
        {
            get
            {
                return this.GetValStringByKey(OA_NewsAttr.UpDT);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.UpDT, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(OA_NewsAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_NewsAttr.Status, value);
            }
        }
        /// <summary>
        /// 访问类型
        /// </summary>
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
        public OA_News()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_News(string No)
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
                Map map = new Map("OA_News");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(OA_NewsAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_NewsAttr.Author, null, "发布人", true, false, 0, 10, 10);
                map.AddTBInt(OA_NewsAttr.Clicks, 0, "点击量", true, false);
                map.AddTBString(OA_NewsAttr.UpUser, null, "更新人", true, false, 0, 50, 50);
                map.AddTBString(OA_NewsAttr.NewsTitle, null, "", true, false, 0, 200, 200);
                map.AddTBString(OA_NewsAttr.NewsSubTitle, null, "", true, false, 0, 200, 200);
                map.AddTBString(OA_NewsAttr.NewsType, null, "", true, false, 0, 1, 1);
                map.AddTBStringDoc(OA_NewsAttr.NewsContent, "", "内容", true, false);
                map.AddTBString(OA_NewsAttr.CreateTime, null, "创建时间", true, false, 0, 50, 50);
                map.AddTBInt(OA_NewsAttr.IsRead, 0, "", true, false);
                map.AddTBString(OA_NewsAttr.UpDT, null, "更新时间", true, false, 0, 50, 50);
                map.AddTBString(OA_NoticeAttr.AccessType, null, "访问类型", true, false, 0, 4, 20);
                map.AddTBInt(OA_NewsAttr.Status, 0, "", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class OA_Newss : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_News(); }
        }
    }
}