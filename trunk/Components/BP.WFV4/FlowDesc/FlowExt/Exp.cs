using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    public enum DLink
    {
        AppCenterDSN,
        Oracle,
        SQLServer,
        Ole,
        ODBC
    }
    /// <summary>
    /// 数据转出
    /// </summary>
    public class ExpAttr:BP.En.EntityOIDNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 类型
        /// </summary>
        public const string DLink = "DLink";
        /// <summary>
        /// 流程
        /// </summary>
        public const string No = "No";
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
        /// DTSWhen
        /// </summary>
        public const string DTSWhen = "DTSWhen";
        public const string ExpDesc = "ExpDesc";
        public const string RefTable = "RefTable";
        public const string IsEnable = "IsEnable";
        #endregion
    }
    /// <summary>
    /// 这里存放每个外部程序设置的信息.	 
    /// </summary>
    public class Exp : EntityNoName
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
                return this.GetValIntByKey(ExpAttr.NodeID);
            }
            set
            {
                this.SetValByKey(ExpAttr.NodeID, value);
            }
        }
      
        /// <summary>
        /// 显示时间
        /// </summary>
        public int DTSWhen
        {
            get
            {
                return this.GetValIntByKey(ExpAttr.DTSWhen);
            }
            set
            {
                this.SetValByKey(ExpAttr.DTSWhen, value);
            }
        }
        public string DTSWhenT
        {
            get
            {
                return this.GetValRefTextByKey(ExpAttr.DTSWhen);
            }
        }
        public string ExpDesc
        {
            get
            {
                return this.GetValStringByKey(ExpAttr.ExpDesc);
            }
            set
            {
                SetValByKey(ExpAttr.ExpDesc, value);
            }
        }
        public string RefTable
        {
            get
            {
                return this.GetValStringByKey(ExpAttr.RefTable);
            }
            set
            {
                SetValByKey(ExpAttr.RefTable, value);
            }
        }

        public DLink DLink
        {
            get
            {
                return (DLink)this.GetValIntByKey(ExpAttr.DLink);
            }
            set
            {
                SetValByKey(ExpAttr.DLink, (int)value);
            }
        }
        public string DLinkT
        {
            get
            {
                return this.GetValRefTextByKey(ExpAttr.DLink);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 外部程序设置
        /// </summary>
        public Exp() { }
        /// <summary>
        /// 外部程序设置
        /// </summary>
        /// <param name="_oid">外部程序设置ID</param>	
        public Exp(string id)
        {
            this.No = id;
            int i =this.RetrieveFromDBSources();
            if (i == 0)
            {
                Flow f = new Flow(id);
                this.Name = f.Name;
                this.Insert();
            }

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
                Map map = new Map("WF_Exp");
                map.EnDesc = "数据转出";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                //      map.AddTBIntPKOID();
                map.AddTBStringPK(ExpAttr.No, null, "流程编号", false, true, 0, 100, 10);
                map.AddTBString(ExpAttr.Name, null, "流程名称", true, false, 0, 400, 10);
                map.AddTBInt(ExpAttr.NodeID, 0, "NodeID", false, false);
                map.AddTBInt(ExpAttr.IsEnable, 0, "是否起用", false, false);

                map.AddDDLSysEnum(ExpAttr.DTSWhen, 0, "发生时间", true, false, ExpAttr.DTSWhen, "@0=在流程结束时@1=在相关节点成功发送后");
                map.AddDDLSysEnum(ExpAttr.DLink, 0, "数据源", true, false, ExpAttr.DLink, "@0=AppCenterDSN(本地库)@1=Oracle连接@2=SQLServer连接@3=Ole连接@4=ODBC连接");
                map.AddTBString(ExpAttr.RefTable, null, "物理表", true, false, 0, 20, 10);
                map.AddTBString(ExpAttr.ExpDesc, null, "说明", false, true, 0, 100, 10);

                //map.AddTBInt(ExpAttr.H, 0, "窗口高度", false, false);
                //map.AddTBInt(ExpAttr.W, 0, "窗口宽度", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 数据转出
    /// </summary>
    public class Exps : EntitiesNoName
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Exp();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 数据转出
        /// </summary>
        public Exps()
        {
        }
        /// <summary>
        /// 数据转出.
        /// </summary>
        /// <param name="FlowNo"></param>
        public Exps(string fk_flow)
        {
            //this.Retrieve(NodeAttr.No, fk_flow);
        }
        public Exps(int nodeid )
        {
            ///this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}
