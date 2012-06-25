using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_PersonalConfigAttr : EntityNoNameAttr
    {
        public const string FK_User = "FK_User";
        public const string Name = "Name";
        public const string SiteUrl = "SiteUrl";
        public const string Option = "Option";
        public const string Description = "Description";
    }

    public partial class OA_PersonalConfig : EntityNoName
    {
        #region 属性

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
        /// 类别名称
        /// </summary>
        public String Name
        {
            get
            {
                return this.GetValStringByKey(OA_FavoriteAttr.Name);
            }
            set
            {
                this.SetValByKey(OA_FavoriteAttr.Name, value);
            }
        }

        /// <summary>
        /// 选项
        /// </summary>
       
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
        public OA_PersonalConfig()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_PersonalConfig(string No)
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
                Map map = new Map("OA_PersonalConfig");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(OA_FavoriteAttr.No, null, "主键ID", true, true, 0, 50, 20);
                map.AddTBString(OA_FavoriteAttr.Seq, null, "序号", true, false, 0, 50, 20);
                map.AddTBString(OA_FavoriteAttr.Name, null, "名称", true, false, 0, 50, 20);
                map.AddTBString(OA_FavoriteAttr.SiteUrl, null, "网址", true, false, 0, 1000, 20);
                map.AddTBString(OA_FavoriteAttr.Target, null, "打开类型：_self;_blank", true, false, 0, 1, 1);
                map.AddTBString(OA_FavoriteAttr.Description, null, "描述", true, false, 0, 200, 200);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class OA_PersonalConfigs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_PersonalConfig(); }
        }
    }
}
