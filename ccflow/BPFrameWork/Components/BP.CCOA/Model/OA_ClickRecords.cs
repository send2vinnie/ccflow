using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_ClickRecordsAttr : EntityNoNameAttr
    {
        public const string ObjectType = "ObjectType";
        public const string ObjectId = "ObjectId";
        public const string VisitDate = "VisitDate";
        public const string Clicks = "Clicks";
        public const string VisitId = "VisitId";
    }
    
    public partial class OA_ClickRecords : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 类型（0-新闻，1-公告）
        /// </summary>
        public int ObjectType
        {
            get
            {
                return this.GetValIntByKey(OA_ClickRecordsAttr.ObjectType);
            }
            set
            {
                this.SetValByKey(OA_ClickRecordsAttr.ObjectType, value);
            }
        }
        
        /// <summary>
        /// 被点击的主键Id
        /// </summary>
        public String ObjectId
        {
            get
            {
                return this.GetValStringByKey(OA_ClickRecordsAttr.ObjectId);
            }
            set
            {
                this.SetValByKey(OA_ClickRecordsAttr.ObjectId, value);
            }
        }
        
        /// <summary>
        /// 访问日期
        /// </summary>
        public String VisitDate
        {
            get
            {
                return this.GetValStringByKey(OA_ClickRecordsAttr.VisitDate);
            }
            set
            {
                this.SetValByKey(OA_ClickRecordsAttr.VisitDate, value);
            }
        }
        
        /// <summary>
        /// 点击次数
        /// </summary>
        public int Clicks
        {
            get
            {
                return this.GetValIntByKey(OA_ClickRecordsAttr.Clicks);
            }
            set
            {
                this.SetValByKey(OA_ClickRecordsAttr.Clicks, value);
            }
        }

        /// <summary>
        /// 访问日期
        /// </summary>
        public String VisitId
        {
            get
            {
                return this.GetValStringByKey(OA_ClickRecordsAttr.VisitId);
            }
            set
            {
                this.SetValByKey(OA_ClickRecordsAttr.VisitId, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_ClickRecords()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_ClickRecords(string No)
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
                Map map = new Map("OA_ClickRecords");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_ClickRecordsAttr.No, null, "主键No", true, true, 0, 50, 50);
                map.AddTBInt(OA_ClickRecordsAttr.ObjectType, 0, "类型（0-新闻，1-公告）", true, false);
                map.AddTBString(OA_ClickRecordsAttr.ObjectId, null, "被点击的主键Id", true, false, 0,  50, 50);
                map.AddTBString(OA_ClickRecordsAttr.VisitDate, null, "访问日期", true, false, 0,  20, 20);
                map.AddTBInt(OA_ClickRecordsAttr.Clicks, 0, "点击次数", true, false);
                map.AddTBString(OA_ClickRecordsAttr.VisitId, null, "访问人员", true, false, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_ClickRecordss : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_ClickRecords(); }
        }
    }
}