using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    public class GeInboxAttr : EntityOIDAttr
    {
        public const string Receiver = "Receiver";      //接收人编号
        public const string ReceiverT = "ReceiverT";    //接收人姓名
        public const string FK_MsgDoc = "FK_MsgDoc";    //邮件编号
        public const string ReadSta = "ReadSta";        //阅读状态  未读=1,已读=2, 已回复=3.
        public const string OPSta = "OPSta";            //操作状态  存草稿=1,删除到垃圾箱=2,彻底删除=3  
        public const string ReadDT = "ReadDT";          //读取日期 
    }

    public class GeInbox : EntityOID
    {
        #region 基本属性
        /// <summary>
        /// 消息内容
        /// </summary>
        public int FK_MsgDoc
        {
            get
            {
                return this.GetValIntByKey(GeInboxAttr.FK_MsgDoc);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.FK_MsgDoc, value);
            }
        }

        /// <summary>
        /// 接受人
        /// </summary>
        public string Receiver
        {
            get
            {
                return this.GetValStringByKey(GeInboxAttr.Receiver);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.Receiver, value);
            }
        }
        /// <summary>
        /// 接受人名称
        /// </summary>
        public string ReceiverT
        {
            get
            {
                return this.GetValStringByKey(GeInboxAttr.ReceiverT);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.ReceiverT, value);
            }
        }
        /// <summary>
        /// 读取日期
        /// </summary>
        public string ReadDT
        {
            get
            {
                return this.GetValStringByKey(GeInboxAttr.ReadDT);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.ReadDT, value);
            }
        }
        /// <summary>
        /// 阅读状态
        /// </summary>
        public int ReadSta
        {
            get
            {
                return this.GetValIntByKey(GeInboxAttr.ReadSta);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.ReadSta, value);
            }
        }
        /// <summary>
        /// 操作状态
        /// </summary>
        public int OPSta
        {
            get
            {
                return this.GetValIntByKey(GeInboxAttr.OPSta);
            }
            set
            {
                this.SetValByKey(GeInboxAttr.OPSta, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);    //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "GE_Inbox";                      // 要连接的物理表。
                map.DepositaryOfMap = Depositary.Application;       //实体map的存放位置.
                map.DepositaryOfEntity = Depositary.None;           //实体存放位置
                map.EnDesc = "收件箱";                              // 实体的描述.
                #endregion

                #region 字段
                map.AddTBIntPKOID();
                map.AddDDLEntities(GeInboxAttr.Receiver, null, "接收人", new Emps(), true);
                map.AddTBString(GeInboxAttr.ReceiverT, string.Empty, "接收人姓名", true, false, 0, 20, 20);
                map.AddTBInt(GeInboxAttr.ReadSta, 0, "阅读状态", true, false);  //已读,未读,已回复
                map.AddTBInt(GeInboxAttr.OPSta, 0, "操作状态", true, false);    //存草稿,删除到垃圾箱,彻底删除  
                map.AddTBDate(GeInboxAttr.ReadDT, "阅读日期", true, false);
                map.AddTBString(GeInboxAttr.FK_MsgDoc, string.Empty, "邮件编号", true, false, 0, 50, 25);
                this._enMap = map;
                return this._enMap;
                #endregion
            }
        }
        #endregion
    }
    /// <summary>
    /// 消息s
    /// </summary>
    public class GeInboxs : EntitiesOID
    {
        /// <summary>
        /// 消息
        /// </summary>
        public GeInboxs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GeInbox();
            }
        }
    }
}
