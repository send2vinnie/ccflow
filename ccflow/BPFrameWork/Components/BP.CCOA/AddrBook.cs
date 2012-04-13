using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.CCOA
{
    /// <summary>
    /// 通讯录
    /// </summary>
    public class AddrBookAttr : EntityNoNameAttr
    {
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// 宽度
        /// </summary>
        public const string FontSize = "FontSize";
        /// <summary>
        /// 颜色
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 风格
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 字体风格
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// 字体
        /// </summary>
        public const string FK_AddrBookDept = "FK_AddrBookDept";
        /// <summary>
        /// 是否粗体
        /// </summary>
        public const string Duty = "Duty";
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
    }
    /// <summary>
    /// 通讯录
    /// </summary>
    public class AddrBook : EntityNoName
    {
        #region 属性
        /// <summary>
        /// Tel
        /// </summary>
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(AddrBookAttr.Tel);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.Tel, value);
            }
        }
        public string EmailHtml
        {
            get
            {
                return PubClass.ToHtmlColor(this.Email);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(AddrBookAttr.Email);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.Email, value);
            }
        }
        public string FK_AddrBookDept
        {
            get
            {
                return this.GetValStringByKey(AddrBookAttr.FK_AddrBookDept);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.FK_AddrBookDept, value);
            }
        }
        public string Duty
        {
            get
            {
                return this.GetValStrByKey(AddrBookAttr.Duty);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.Duty, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(AddrBookAttr.Idx);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.Idx, value);
            }
        }
        /// <summary>
        /// Addr
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(AddrBookAttr.Addr);
            }
            set
            {
                this.SetValByKey(AddrBookAttr.Addr, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 通讯录
        /// </summary>
        public AddrBook()
        {
        }
        /// <summary>
        /// 通讯录
        /// </summary>
        /// <param name="mypk"></param>
        public AddrBook(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_AddrBook");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "通讯录";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(AddrBookAttr.No, null, "编号", true, true, 2, 50, 20);
                map.AddTBString(AddrBookAttr.Name, null, "名称", true, false, 0, 3900, 20);
                map.AddTBString(AddrBookAttr.Addr, null, "Addr", true, false, 0, 50, 20);
                map.AddTBString(AddrBookAttr.Tel, null, "Tel", true, false, 0, 50, 20);
                map.AddTBString(AddrBookAttr.Email, null, "Email", true, false, 0, 50, 20);
                map.AddDDLEntities(AddrBookAttr.FK_AddrBookDept, null, "部门", new AddrBookDepts(), true);
                map.AddTBString(AddrBookAttr.Duty, null, "职务", true, false, 0, 50, 20);
                //map.AddTBInt(AddrBookAttr.Duty, 0, "Duty", false, false);
                map.AddTBInt(AddrBookAttr.Idx, 0, "Idx", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 通讯录s
    /// </summary>
    public class AddrBooks : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 通讯录s
        /// </summary>
        public AddrBooks()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new AddrBook();
            }
        }
        #endregion
    }
}
