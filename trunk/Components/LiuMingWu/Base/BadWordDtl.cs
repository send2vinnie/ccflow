using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 屏蔽的关键字
    /// </summary>
    public class BadWordDtlAttr : EntityNoNameAttr
    {
        public const string IPs = "IPs";
        public const string UseTime = "UseTime";
    }
    /// <summary>
    /// 屏蔽的关键字
    /// </summary>
    public class BadWordDtl : EntityNoName
    {
        #region 基本属性
        public string IPs
        {
            get
            {
                return this.GetValStringByKey(BadWordDtlAttr.IPs);
            }
            set
            {
                this.SetValByKey(BadWordDtlAttr.IPs, value);
            }
        }
        public int UseTime
        {
            get
            {
                return this.GetValIntByKey(BadWordDtlAttr.UseTime);
            }
            set
            {
                this.SetValByKey(BadWordDtlAttr.UseTime, value);
            }
        }
        #endregion

        /// <summary>
        /// 屏蔽的关键字
        /// </summary>
        public BadWordDtl(string no)
            : base(no)
        {
        }
        /// <summary>
        /// 屏蔽的关键字
        /// </summary>
        public BadWordDtl()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("GE_BadWordDtl");
                map.EnType = EnType.Sys;
                map.EnDesc = "屏蔽的关键字";
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = true;
                map.IsAllowRepeatName = false;
                map.CodeStruct = "3";

                map.AddTBStringPK(BadWordDtlAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(BadWordDtlAttr.Name, null, "字", true, false, 1, 100, 10);
                map.AddTBInt(BadWordDtlAttr.UseTime, 0, "屏蔽次数", true, false);
                map.AddTBStringDoc(BadWordDtlAttr.IPs, null, "来源IP", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    /// <summary>
    /// 屏蔽的关键字s
    /// </summary>
    public class BadWordDtls : EntitiesNoName
    {
        /// <summary>
        /// 屏蔽的关键字s
        /// </summary>
        public BadWordDtls()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BadWordDtl();
            }
        }
    }
}
