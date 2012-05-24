using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_CategoryAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string CategoryName = "CategoryName";
        public const string Type = "Type";
        public const string Description = "Description";
    }
    
    public partial class OA_Category : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_CategoryAttr.No);
            }
            set
            {
                this.SetValByKey(OA_CategoryAttr.No, value);
            }
        }
        
        /// <summary>
        /// 类别名称
        /// </summary>
        public String CategoryName
        {
            get
            {
                return this.GetValStringByKey(OA_CategoryAttr.CategoryName);
            }
            set
            {
                this.SetValByKey(OA_CategoryAttr.CategoryName, value);
            }
        }
        
        /// <summary>
        /// 类型：0-news;1-notice...
        /// </summary>
        public String Type
        {
            get
            {
                return this.GetValStringByKey(OA_CategoryAttr.Type);
            }
            set
            {
                this.SetValByKey(OA_CategoryAttr.Type, value);
            }
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(OA_CategoryAttr.Description);
            }
            set
            {
                this.SetValByKey(OA_CategoryAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Category()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Category(string No)
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
                Map map = new Map("OA_Category");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_CategoryAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(OA_CategoryAttr.CategoryName, null, "类别名称", true, false, 0,  50, 50);
                map.AddTBString(OA_CategoryAttr.Type, null, "类型：0-news;1-notice...", true, false, 0,  1, 1);
                map.AddTBString(OA_CategoryAttr.Description, null, "描述", true, false, 0,  200, 200);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Categorys : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Category(); }
        }
    }
}