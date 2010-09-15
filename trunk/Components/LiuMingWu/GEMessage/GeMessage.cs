using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    public class GeMessageAttr : EntityOIDAttr
    {
        public const string Receiver = "Receiver";      //接收人编号
        public const string ReceiverT = "ReceiverT";    //接收人姓名

        public const string ReadSta = "ReadSta";        //阅读状态 未读=0,已读=1, 已回复=2. 
        public const string StaR = "StaR";              //接收人操作状态 删除到垃圾箱=2 彻底删除=3  
        public const string ReadDT = "ReadDT";          //读取日期 

        public const string Sender = "Sender";          //发件人编号
        public const string SenderT = "SenderT";        //发件人姓名
        public const string StaS = "StaS";              //发送人操作状态 存草稿=1 删除到垃圾箱=2 彻底删除=3  
        public const string SendDT = "SendDT";          //发送日期 

        public const string Title = "Title";            //标题    
        public const string Doc = "Doc";                //内容
    }

    public class GeMessage : EntityOID
    {
        /// <summary>
        /// 接受人
        /// </summary>
        public string Receiver
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.Receiver);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.Receiver, value);
            }
        }
        /// <summary>
        /// 接受人名称
        /// </summary>
        public string ReceiverT
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.ReceiverT);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.ReceiverT, value);
            }
        }
        /// <summary>
        /// 读取日期
        /// </summary>
        public string ReadDT
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.ReadDT);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.ReadDT, value);
            }
        }
        /// <summary>
        /// 阅读状态
        /// </summary>
        public int ReadSta
        {
            get
            {
                return this.GetValIntByKey(GeMessageAttr.ReadSta);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.ReadSta,value);
            }
        }
        /// <summary>
        /// 操作状态
        /// </summary>
        public int StaR
        {
            get
            {
                return this.GetValIntByKey(GeMessageAttr.StaR);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.StaR, value);
            }
        }

        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.Sender);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.Sender, value);
            }
        }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string SenderT
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.SenderT);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.SenderT, value);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string SendDT
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.SendDT);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.SendDT, value);
            }
        }
        /// <summary>
        /// 操作状态
        /// </summary>
        public int StaS
        {
            get
            {
                return this.GetValIntByKey(GeMessageAttr.StaS);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.StaS, value);
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.Title);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.Title, value);
            }
        }
        /// <summary>
        /// 文档
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(GeMessageAttr.Doc);
            }
            set
            {
                this.SetValByKey(GeMessageAttr.Doc, value);
            }
        }

        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);    //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "GE_Message";                    // 要连接的物理表。
                map.DepositaryOfMap = Depositary.Application;       //实体map的存放位置.
                map.DepositaryOfEntity = Depositary.None;           //实体存放位置
                map.EnDesc = "邮件";                                // 实体的描述.
                #endregion

                #region 字段

                map.AddTBIntPKOID();

                map.AddDDLEntities(GeMessageAttr.Receiver, null, "接收人编号", new Emps(), true);
                map.AddTBString(GeMessageAttr.ReceiverT, string.Empty, "接收人姓名", true, false, 0, 20, 20);
                map.AddTBInt(GeMessageAttr.ReadSta, 0, "阅读状态", true, false);      //已读,未读,已回复
                map.AddTBInt(GeMessageAttr.StaR, 0, "操作状态", true, false);         //删除到垃圾箱,彻底删除  
                map.AddTBDate(GeMessageAttr.ReadDT, "阅读日期", true, false);

                map.AddDDLEntities(GeMessageAttr.Sender, null, "发件人编号", new Emps(), true);
                map.AddTBString(GeMessageAttr.SenderT, string.Empty, "发件人", true, false, 0, 20, 20);
                map.AddTBInt(GeMessageAttr.StaS, 0, "操作状态", true, false);        //删除到垃圾箱,彻底删除  
                map.AddTBDate(GeMessageAttr.SendDT, "发送日期", true, false);

                map.AddTBString(GeMessageAttr.Title, null, "标题", true, false, 0, 100, 50);
                map.AddTBStringDoc(GeMessageAttr.Doc, null, "消息内容", true, false);

                this._enMap = map;
                return this._enMap;
                #endregion

            }
        }
    }

    /// <summary>
    /// 消息
    /// </summary>
    public class GeMessages : EntitiesOID
    {
        /// <summary>
        /// 消息
        /// </summary>
        public GeMessages()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GeMessage();
            }
        }
    }
}