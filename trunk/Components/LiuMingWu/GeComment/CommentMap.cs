using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class CommentAttr : EntityOIDAttr
    {
        #region 基本属性
        //组别
        public const string GroupKey = "GroupKey";
        //主题
        public const string RefOID = "RefOID";
        //评论人
        public const string FK_Emp = "FK_Emp";
        public const string FK_EmpT = "FK_EmpT";
        public const string FK_Dept = "FK_Dept";
        //评论时间
        public const string RDT = "RDT";
        //标题
        public const string Title = "Title";
        //内容
        public const string Doc = "Doc";
        //评论人的IP
        public const string IP = "IP";
        //是否是匿名用户
        public const string ISAnony = "ISAnony";
        //Email
        public const string Email = "Email";
        #endregion
    }
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : EntityOID
    {
        #region 基本属性
        /// <summary>
        /// 评论人
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CommentAttr.FK_Emp, value);
            }
        }
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(CommentAttr.FK_EmpT);
            }
            set
            {
                this.SetValByKey(CommentAttr.FK_EmpT, value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValRefTextByKey(CommentAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CommentAttr.FK_Dept, value);
            }
        }
        //组别
        public string GroupKey
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.GroupKey);
            }
            set
            {
                this.SetValByKey(CommentAttr.GroupKey, value);
            }
        }
        //主题
        public string RefOID
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.RefOID);
            }
            set
            {
                this.SetValByKey(CommentAttr.RefOID, value);
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStrByKey(CommentAttr.Title);
            }
            set
            {
                this.SetValByKey(CommentAttr.Title, value);
            }
        }
        /// <summary>
        /// 评论的时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.RDT);
            }
            set
            {
                this.SetValByKey(CommentAttr.RDT, value);
            }
        }
        /// <summary>
        /// 评论人的IP
        /// </summary>
        public string IP
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.IP);
            }
            set
            {
                this.SetValByKey(CommentAttr.IP, value);
            }
        }
        /// <summary>
        /// 是否是匿名用户
        /// </summary>
        public string ISAnony
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.ISAnony);
            }
            set
            {
                this.SetValByKey(CommentAttr.ISAnony, value);
            }
        }
        /// <summary>
        /// 评论人的Email
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(CommentAttr.Email);
            }
            set
            {
                this.SetValByKey(CommentAttr.Email, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStrByKey(CommentAttr.Doc);
            }
            set
            {
                this.SetValByKey(CommentAttr.Doc, value);
            }
        }


        #endregion
       
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
                    map.PhysicsTable = "GE_Comment"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "评论";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBIntPKOID();
                    map.AddTBString(CommentAttr.GroupKey, string.Empty, "组别", true, false, 1, 20, 10);
                    map.AddTBString(CommentAttr.RefOID, string.Empty, "评论的主题", true, false, 0, 200, 10);
                    map.AddTBString(CommentAttr.FK_Dept, string.Empty, "部门", true, false, 0, 200, 10);
                    map.AddTBString(CommentAttr.FK_Emp, string.Empty, "评论人", true, false, 0, 200, 10);
                    map.AddTBString(CommentAttr.FK_EmpT, string.Empty, "评论人名称", true, false, 0, 200, 10);

                    map.AddTBDate(CommentAttr.RDT, string.Empty, "评论时间", true, false);
                    map.AddTBString(CommentAttr.IP, string.Empty, "IP", true, false, 0, 20, 20);
                    map.AddTBString(CommentAttr.Email, string.Empty, "Email", true, false, 0, 50, 50);
                    map.AddTBString(CommentAttr.Title, string.Empty, "标题", true, false, 0, 50, 25);
                    //map.AddTBString(CommentAttr.Doc, string.Empty, "内容", true, false, 0, 1024, 50);
                    map.AddTBStringDoc();
                    #endregion 字段增加.

                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }
    /// <summary>
    /// 评论
    /// </summary>
    public class Comments : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Comment();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public Comments() { }
        #endregion
    }
}
