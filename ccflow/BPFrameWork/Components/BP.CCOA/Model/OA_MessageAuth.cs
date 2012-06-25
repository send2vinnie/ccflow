using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public partial class OA_MessageAuthAttr : EntityNoNameAttr
    {
        public const string FK_Message = "FK_Message";
        public const string FK_Id = "FK_Id";
        public const string IsDelete = "IsDelete";
        public const string Status = "Status";
    }

    public partial class OA_MessageAuth : EntityNoName
    {
        #region 属性

        /// <summary>
        /// Email公告外键
        /// </summary>
        public String FK_Message
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAuthAttr.FK_Message);
            }
            set
            {
                this.SetValByKey(OA_MessageAuthAttr.FK_Message, value);
            }
        }

        /// <summary>
        /// 可以访问的主键
        /// </summary>
        public String FK_Id
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAuthAttr.FK_Id);
            }
            set
            {
                this.SetValByKey(OA_MessageAuthAttr.FK_Id, value);
            }
        }

        /// <summary>
        /// 是否已从收件箱删除
        /// </summary>
        public String IsDelete
        {
            get
            {
                return this.GetValStringByKey(OA_MessageAuthAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(OA_MessageAuthAttr.IsDelete, value);
            }
        }

        /// <summary>
        /// 垃圾箱删除标记
        /// </summary>
        public String Status
        {
            get
            {
                return this.GetValStringByKey(OA_EmailAuthAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_EmailAuthAttr.Status, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_MessageAuth()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_MessageAuth(string No)
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
                Map map = new Map("OA_MessageAuth");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(OA_MessageAuthAttr.No, null, "主键No", true, true, 0, 50, 50);
                map.AddTBString(OA_MessageAuthAttr.FK_Message, null, "Message外键", true, false, 0, 50, 50);
                map.AddTBString(OA_EmailAuthAttr.FK_Id, null, "可以访问的主键", true, false, 0, 50, 50);
                map.AddTBString(OA_MessageAuthAttr.IsDelete, "0", "是否已删除", true, false, 0, 1, 1);
                map.AddTBString(OA_MessageAuthAttr.Status, "1", "状态标记", true, false, 0, 1, 1);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class OA_MessageAuths : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_MessageAuth(); }
        }
    }
}
