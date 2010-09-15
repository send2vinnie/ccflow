using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    public class GeOutboxAttr : EntityOIDAttr
    {
        public const string Sender = "Sender";          //发件人编号
        public const string SenderT = "SenderT";        //发件人姓名
        public const string OPSta = "OPSta";            //操作状态  存草稿,删除到垃圾箱,彻底删除  
        public const string SendDT = "SendDT";          //发送日期 
        public const string FK_MsgDoc = "FK_MsgDoc";    //邮件编号
    }

    public class GeOutbox : EntityOID
    {
        #region 基本属性
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(GeOutboxAttr.Sender);
            }
            set
            {
                this.SetValByKey(GeOutboxAttr.Sender, value);
            }
        }
        /// <summary>
        /// 接受人名称
        /// </summary>
        public string SenderT
        {
            get
            {
                return this.GetValStringByKey(GeOutboxAttr.SenderT);
            }
            set
            {
                this.SetValByKey(GeOutboxAttr.SenderT,value);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string SendDT
        {
            get
            {
                return this.GetValStringByKey(GeOutboxAttr.SendDT);
            }
            set
            {
                this.SetValByKey(GeOutboxAttr.SendDT, value);
            }
        }
        /// <summary>
        /// 操作状态
        /// </summary>
        public int OPSta
        {
            get
            {
                return this.GetValIntByKey(GeOutboxAttr.OPSta);
            }
            set
            {
                this.SetValByKey(GeOutboxAttr.OPSta, value);
            }
        }
        /// <summary>
        /// 邮件编号
        /// </summary>
        public int FK_MsgDoc
        {
            get
            {
                return this.GetValIntByKey(GeOutboxAttr.FK_MsgDoc);
            }
            set
            {
                this.SetValByKey(GeOutboxAttr.FK_MsgDoc, value);
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
                map.PhysicsTable = "GE_Outbox";                     //要连接的物理表。
                map.DepositaryOfMap = Depositary.Application;       //实体map的存放位置.
                map.DepositaryOfEntity = Depositary.None;           //实体存放位置
                map.EnDesc = "发件箱";                              //实体的描述.
                #endregion

                #region 字段
                map.AddTBIntPKOID();
                map.AddDDLEntities(GeOutboxAttr.Sender, null, "发件人编号", new Emps(), true);
                map.AddTBString(GeOutboxAttr.SenderT, string.Empty, "发件人", true, false, 0, 20, 20);
                map.AddTBInt(GeOutboxAttr.OPSta, 0, "操作状态", true, false); //存草稿,删除到垃圾箱,彻底删除  
                map.AddTBDate(GeOutboxAttr.SendDT, "发送日期", true, false);
                map.AddTBString(GeOutboxAttr.FK_MsgDoc, string.Empty, "邮件编号", true, false, 0, 50, 25);
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
    public class GeOutboxs : EntitiesOID
    {
        /// <summary>
        /// 消息
        /// </summary>
        public GeOutboxs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GeOutbox();
            }
        }
    }
}
