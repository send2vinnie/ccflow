using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;
using BP.Port;
namespace BP.GE
{
    public class GeFriendAttr : EntityOIDAttr
    {
        public const string FK_Emp1 = "FK_Emp1";        //通讯本创建人
        public const string FK_Emp2 = "FK_Emp2";        //联系人编号
        public const string Email = "Email";            //邮箱
        public const string Mobile = "Mobile";          //手机
        public const string Phone = "Phone";            //电话
        public const string Name = "Name";              //姓名
        public const string Birthday = "Birthday";      //生日
        public const string Address = "Address";        //家庭地址
        public const string Company = "Company";        //公司地址    
        public const string Note = "Note";              //备注
    }
    public class GeFriend : EntityOID
    {
        /// <summary>
        /// 通讯本创建人
        /// </summary>
        public string FK_Emp1
        {
            get
            {
                return this.GetValStrByKey(GeFriendAttr.FK_Emp1);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.FK_Emp1, value);
            }
        }
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string FK_Emp2
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.FK_Emp2);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.FK_Emp2, value);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Email);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Email, value);
            }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Mobile);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Mobile, value);
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Phone);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Phone, value);
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Name);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Name, value);
            }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Birthday);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Birthday, value);
            }
        }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string Address
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Address);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Address, value);
            }
        }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Company
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Company);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Company, value);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GeFriendAttr.Note);
            }
            set
            {
                this.SetValByKey(GeFriendAttr.Note, value);
            }
        }

        public override Map EnMap
        {
            get
            {
                {
                    if (this._enMap != null)
                        return this._enMap;

                    Map map = new Map();

                    #region 基本属性
                    map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                    map.PhysicsTable = "GE_Friend"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "好友";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBIntPKOID();
                    map.AddDDLEntities(GeFriendAttr.FK_Emp1, null, "创建人", new Emps(), true);
                    map.AddDDLEntities(GeFriendAttr.FK_Emp2, null, "联系人编号", new Emps(), true);
                    map.AddTBString(GeFriendAttr.Email, string.Empty, "邮箱", true, false, 0, 50, 25);
                    map.AddTBString(GeFriendAttr.Mobile, string.Empty, "手机", true, false, 0, 15, 15);
                    map.AddTBString(GeFriendAttr.Phone, string.Empty, "电话", true, false, 0, 15, 15);
                    map.AddTBString(GeFriendAttr.Name, string.Empty, "姓名", true, false, 0, 10, 10);
                    map.AddTBDate(GeFriendAttr.Birthday, "生日", true, false);
                    map.AddTBString(GeFriendAttr.Address, string.Empty, "家庭住址", true, false, 0, 100, 100);
                    map.AddTBString(GeFriendAttr.Company, string.Empty, "公司地址", true, false, 0, 100, 100);
                    map.AddTBString(GeFriendAttr.Note, string.Empty, "备注", true, false, 0, 1000, 500);
                    #endregion 字段增加.

                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }
    /// <summary>
    /// 好友
    /// </summary>
    public class GeFriends : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GeFriend();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public GeFriends() { }
        #endregion
    }
}