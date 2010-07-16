using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Port
{	 
	/// <summary>
	/// 授权属性
	/// </summary>
    public class AuthorAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 授权日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 接受人
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 是否有效
        /// </summary>
        public const string IsOK = "IsOK";
    }
	/// <summary>
	/// 授权
	/// </summary>
    public class Author : EntityNo
    {
        #region 实现基本的方方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 授权给
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStrByKey(AuthorAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(AuthorAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(AuthorAttr.RDT);
            }
            set
            {
                this.SetValByKey(AuthorAttr.RDT, value);
            }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsOK
        {
            get
            {
                return this.GetValBooleanByKey(AuthorAttr.IsOK);
            }
            set
            {
                this.SetValByKey(AuthorAttr.IsOK, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 授权
        /// </summary> 
        public Author()
        {
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_No"></param>
        public Author(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Author");
                map.EnDesc = this.ToE("Author", "授权"); // "授权";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, null, true, false, 2, 20, 4);

                map.AddTBString(AuthorAttr.FK_Emp, null, "授权给", true, false, 0, 50, 250);
                map.AddTBDate(AuthorAttr.RDT, null, "记录日期", true, true);
                map.AddBoolean(AuthorAttr.IsOK, true, "是否有效", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	 /// <summary>
	 /// 授权s
	 /// </summary>
    public class Authors : EntitiesNoName
    {
        /// <summary>
        /// 授权
        /// </summary>
        public Authors() { }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Author();
            }
        }
    }
}
