using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// 外部程序设置属性
    /// </summary>
    public class FAppSetAttr:BP.En.EntityOIDNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 类型
        /// </summary>
        public const string AppType = "AppType";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 节点编号
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// x
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// y
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// ShowTime
        /// </summary>
        public const string ShowTime = "ShowTime";
        public const string DoWhat = "DoWhat";
        #endregion
    }
    /// <summary>
    /// 这里存放每个外部程序设置的信息.	 
    /// </summary>
    public class FAppSet : EntityOIDName
    {
        #region 基本属性
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                return uac;
            }
        }
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.NodeID);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.NodeID, value);
            }
        }
        /// <summary>
        /// x
        /// </summary>
        public int H
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.H);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.H, value);
            }
        }
        /// <summary>
        /// y
        /// </summary>
        public int W
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.W);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.W, value);
            }
        }
        /// <summary>
        /// 显示时间
        /// </summary>
        public int ShowTime_
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.ShowTime );
            }
            set
            {
                this.SetValByKey(FAppSetAttr.ShowTime, value);
            }
        }
        public string ShowTimeT_del
        {
            get
            {
                return this.GetValRefTextByKey(FAppSetAttr.ShowTime);
            }
        }
        /// <summary>
        /// 外部程序设置的事务编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string DoWhat
        {
            get
            {
                return this.GetValStringByKey(FAppSetAttr.DoWhat);
            }
            set
            {
                SetValByKey(FAppSetAttr.DoWhat, value);
            }
        }
        
        public string AppType
        {
            get
            {
                return this.GetValStringByKey(FAppSetAttr.AppType);
            }
            set
            {
                SetValByKey(FAppSetAttr.AppType, value);
            }
        }
        public string AppTypeT
        {
            get
            {
                return this.GetValRefTextByKey(FAppSetAttr.AppType);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 外部程序设置
        /// </summary>
        public FAppSet() { }
        /// <summary>
        /// 外部程序设置
        /// </summary>
        /// <param name="_oid">外部程序设置ID</param>	
        public FAppSet(int _oid)
        {
            this.OID = _oid;
            this.Retrieve();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_FAppSet");
                map.EnDesc = "外部程序设置";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(NodeAttr.Name, null, "显示标签", true, false, 0, 400, 10);
                map.AddTBInt(FAppSetAttr.NodeID, 0, "NodeID", false, false);

                map.AddDDLSysEnum(FAppSetAttr.AppType, 0, "应用类型", true, false, FAppSetAttr.AppType,"@0=外部Url连接@1=本地可执行文件");

                //map.AddDDLSysEnum(FAppSetAttr.ShowTime, 0, "发生时间", true, false, FAppSetAttr.ShowTime, 
                //    "@0=无(显示在表单底部)@1=当工作选择时@2=当保存时@3=当发送时");

                map.AddTBString(FAppSetAttr.FK_Flow, null, "流程编号", false, true, 0, 100, 10);
                map.AddTBString(FAppSetAttr.DoWhat, null, "执行什么？", false, true, 0, 100, 10);

                map.AddTBInt(FAppSetAttr.H, 0, "窗口高度", false, false);
                map.AddTBInt(FAppSetAttr.W, 0, "窗口宽度", false, false);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 外部程序设置集合
    /// </summary>
    public class FAppSets : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FAppSet();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 外部程序设置集合
        /// </summary>
        public FAppSets()
        {
        }
        /// <summary>
        /// 外部程序设置集合.
        /// </summary>
        /// <param name="FlowNo"></param>
        public FAppSets(string fk_flow)
        {
            this.Retrieve(NodeAttr.FK_Flow, fk_flow);
        }
        public FAppSets(int nodeid)
        {
            this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}
