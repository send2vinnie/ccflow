using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_EmpPostAttr : EntityNoNameAttr
    {
        public const string EmpId = "EmpId";
        public const string PostId = "PostId";
        public const string IsSub = "IsSub";
        public const string No = "No";
    }

    public partial class EIP_EmpPost : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public String EmpId
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpPostAttr.EmpId);
            }
            set
            {
                this.SetValByKey(EIP_EmpPostAttr.EmpId, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PostId
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpPostAttr.PostId);
            }
            set
            {
                this.SetValByKey(EIP_EmpPostAttr.PostId, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Object IsSub
        {
            get
            {
                return this.GetValBooleanByKey(EIP_EmpPostAttr.IsSub);
            }
            set
            {
                this.SetValByKey(EIP_EmpPostAttr.IsSub, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpPostAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_EmpPostAttr.No, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_EmpPost()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_EmpPost(string No)
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
                Map map = new Map("EIP_EmpPost");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBString(EIP_EmpPostAttr.EmpId, null, "", true, false, 0, 50, 50);
                map.AddTBString(EIP_EmpPostAttr.PostId, null, "", true, false, 0, 50, 50);
                map.AddTBInt(EIP_EmpPostAttr.IsSub, 1, "是否次要职位", true, false);
                map.AddTBStringPK(EIP_EmpPostAttr.No, null, "", true, true, 0, 50, 50);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class EIP_EmpPosts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_EmpPost(); }
        }
    }
}