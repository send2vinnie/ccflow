using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_FavoriteAttr : EntityNoNameAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string Seq = "Seq";
        public const string SiteUrl = "SiteUrl";
        public const string Target = "Target";
        public const string Description = "Description";
    }
    
    public partial class OA_Favorite : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 对应用户
        /// </summary>
        public String FK_Emp
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.FK_Emp, value);
            }
        }
        
        /// <summary>
        /// 序号
        /// </summary>
        public String Seq
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.Seq);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.Seq, value);
            }
        }
        
        /// <summary>
        /// 选项
        /// </summary>
        public String Target
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.Target);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.Target, value);
            }
        }

         /// <summary>
        /// 网址
        /// </summary>
        public String SiteUrl
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.SiteUrl);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.SiteUrl, value);
            }
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.Description);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Favorite()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Favorite(string No)
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
                Map map = new Map("OA_Favorite");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_FavoriteAttr.No, null, "主键ID", true, true, 0, 50, 20);
                map.AddTBString(OA_FavoriteAttr.FK_Emp, "", "对应用户", true, false, 0, 50, 20);
                map.AddTBInt(OA_FavoriteAttr.Seq, 0, "序号", true, false);
                map.AddTBString(OA_FavoriteAttr.Name, "", "名称", true, false, 0,  50, 20);
                map.AddTBString(OA_FavoriteAttr.SiteUrl, "#", "网址", true, false, 0, 1000, 20);
                map.AddTBString(OA_FavoriteAttr.Target, "_blank", "打开类型：_self;_blank", true, false, 0, 10, 10);
                map.AddTBString(OA_FavoriteAttr.Description, null, "描述", true, false, 0,  200, 200);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Favorites : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Favorite(); }
        }
    }
}