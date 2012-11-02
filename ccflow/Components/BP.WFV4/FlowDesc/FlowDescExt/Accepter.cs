using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;

namespace BP.WF
{
    /// <summary>
    /// Accpter属性
    /// </summary>
    public class AccpterAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 流程
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 发送是否启用
        /// </summary>
        public const string SendEnable = "SendEnable";
        /// <summary>
        /// 发送标签
        /// </summary>
        public const string SendLab = "SendLab";
        /// <summary>
        /// 保存是否启用
        /// </summary>
        public const string SaveEnable = "SaveEnable";
        /// <summary>
        /// 跳转规则
        /// </summary>
        public const string JumpWayLab = "JumpWayLab";
        /// <summary>
        /// 保存标签
        /// </summary>
        public const string SaveLab = "SaveLab";
        /// <summary>
        /// 退回是否启用
        /// </summary>
        public const string ReturnRole = "ReturnRole";
        /// <summary>
        /// 退回标签
        /// </summary>
        public const string ReturnLab = "ReturnLab";
        /// <summary>
        /// 打印单据标签
        /// </summary>
        public const string PrintDocLab = "PrintDocLab";
        /// <summary>
        /// 打印单据是否起用
        /// </summary>
        public const string PrintDocEnable = "PrintDocEnable";
        /// <summary>
        /// 移交是否启用
        /// </summary>
        public const string ShiftEnable = "ShiftEnable";
        /// <summary>
        /// 移交标签
        /// </summary>
        public const string ShiftLab = "ShiftLab";

        public const string RptLab = "RptLab";
        public const string RptEnable = "RptEnable";
        /// <summary>
        /// 查询标签
        /// </summary>
        public const string SearchLab = "SearchLab";
        /// <summary>
        /// 查询是否可用
        /// </summary>
        public const string SearchEnable = "SearchEnable";
        public const string TrackLab = "TrackLab";
        public const string TrackEnable = "TrackEnable";

        public const string OptLab = "OptLab";
        public const string OptEnable = "OptEnable";

        public const string CCLab = "CCLab";
        public const string CCRole = "CCRole";

        public const string DelLab = "DelLab";
        public const string DelEnable = "DelEnable";

        /// <summary>
        /// 结束流程
        /// </summary>
        public const string EndFlowLab = "EndFlowLab";
        /// <summary>
        /// 结束流程
        /// </summary>
        public const string EndFlowEnable = "EndFlowEnable";
        /// <summary>
        /// AthLab
        /// </summary>
        public const string AthLab = "AthLab";
        /// <summary>
        /// FJOpen
        /// </summary>
        public const string FJOpen = "FJOpen";
        /// <summary>
        /// 选择接受人
        /// </summary>
        public const string SelectAccepterLab = "SelectAccepterLab";
        /// <summary>
        /// SelectAccepterEnable
        /// </summary>
        public const string SelectAccepterEnable = "SelectAccepterEnable";
        /// <summary>
        /// 发送按钮
        /// </summary>
        public const string SendJS = "SendJS";
        /// <summary>
        /// 抄送标题
        /// </summary>
        public const string CCTitle = "CCTitle";
        /// <summary>
        /// 抄送内容
        /// </summary>
        public const string CCDoc = "CCDoc";
        /// <summary>
        /// 抄送对象
        /// </summary>
        public const string CCEmps = "CCEmps";
    }
    /// <summary>
    /// Accpter
    /// </summary>
    public class Accepter : Entity
    {

        #region 基本属性
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(AccpterAttr.NodeID);
            }
            set
            {
                this.SetValByKey(AccpterAttr.NodeID, value);
            }
        }
        public string SearchLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.SearchLab);
            }
            set
            {
                this.SetValByKey(AccpterAttr.SearchLab, value);
            }
        }
        public bool SearchEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.SearchEnable);
            }
            set
            {
                this.SetValByKey(AccpterAttr.SearchEnable, value);
            }
        }

        public string ShiftLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.ShiftLab);
            }
            set
            {
                this.SetValByKey(AccpterAttr.ShiftLab, value);
            }
        }
        public bool ShiftEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.ShiftEnable);
            }
            set
            {
                this.SetValByKey(AccpterAttr.ShiftEnable, value);
            }
        }
        public string SelectAccepterLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.SelectAccepterLab);
            }
        }
        public int SelectAccepterEnable
        {
            get
            {
                return this.GetValIntByKey(AccpterAttr.SelectAccepterEnable);
            }
        }
        public string RptLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.RptLab);
            }
        }
        public bool RptEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.RptEnable);
            }
        }
        public string SaveLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.SaveLab);
            }
        }
        public bool SaveEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.SaveEnable);
            }
        }
        public string JumpWayLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.JumpWayLab);
            }
        }
        public JumpWay JumpWayEnum
        {
            get
            {
                return (JumpWay)this.GetValIntByKey(NodeAttr.JumpWay);
            }
        }
        public bool JumpWayEnable
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.JumpWay);
            }
        }
        public string ReturnLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.ReturnLab);
            }
        }
        public bool ReturnEnable
        {
            get
            {
                return  this.GetValBooleanByKey(AccpterAttr.ReturnRole);
            }
        }

        public string AthLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.AthLab);
            }
        }
        public bool AthEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.FJOpen);
            }
        }

        public string PrintDocLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.PrintDocLab);
            }
        }
        public bool PrintDocEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.PrintDocEnable);
            }
        }
        public string SendLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.SendLab);
            }
        }
        public bool SendEnable
        {
            get
            {
                return true;
            }
        }
        public string SendJS
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.SendJS).Replace("~","'");
            }
        }
        public string TrackLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.TrackLab);
            }
        }
        public bool TrackEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.TrackEnable);
            }
        }
        public string OptLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.OptLab);
            }
        }
        public bool OptEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.OptEnable);
            }
        }
        public string CCLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.CCLab);
            }
        }
        public CCRole CCRole
        {
            get
            {
                return (CCRole)this.GetValIntByKey(AccpterAttr.CCRole);
            }
        }
        public string DeleteLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.DelLab);
            }
        }
        public bool DeleteEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.DelEnable);
            }
        }

        /// <summary>
        /// 结束流程
        /// </summary>
        public string EndFlowLab
        {
            get
            {
                return this.GetValStringByKey(AccpterAttr.EndFlowLab);
            }
        }
        /// <summary>
        /// 是否启用结束流程
        /// </summary>
        public bool EndFlowEnable
        {
            get
            {
                return this.GetValBooleanByKey(AccpterAttr.EndFlowEnable);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// Accpter
        /// </summary>
        public Accepter() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeid"></param>
        public Accepter(int nodeid)
        {
            this.NodeID = nodeid;
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

                Map map = new Map("WF_Node");
                map.EnDesc = "节点标签";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(AccpterAttr.NodeID, 0, "NodeID", true, false);

             //   map.AddDDLSysEnum(AccpterAttr.SendEnable,

                map.AddTBString(AccpterAttr.SendLab, "发送", "发送按钮标签", true, false, 0, 50, 10);
                map.AddTBString(AccpterAttr.SendJS, "", "按钮JS函数", true, false, 0, 50, 10);
                
             //   map.AddBoolean(AccpterAttr.SendEnable, true, "是否启用", true, true);


                map.AddTBString(AccpterAttr.JumpWayLab, "跳转", "跳转按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(NodeAttr.JumpWay, false, "是否启用", true, true);


                map.AddTBString(AccpterAttr.SaveLab, "保存", "保存按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.SaveEnable, true, "是否启用", true, true);


                map.AddTBString(AccpterAttr.ReturnLab, "退回", "退回按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.ReturnRole, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.CCLab, "抄送", "抄送按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(AccpterAttr.CCRole, 0, "抄送规则",true, true, AccpterAttr.CCRole);

            //  map.AddBoolean(AccpterAttr, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.ShiftLab, "移交", "移交按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.ShiftEnable, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.DelLab, "删除流程", "删除流程按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.DelEnable, false, "是否启用", true, true);

                map.AddTBString(AccpterAttr.EndFlowLab, "结束流程", "结束流程按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.EndFlowEnable, false, "是否启用", true, true);

                map.AddTBString(AccpterAttr.RptLab, "报告", "报告按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.RptEnable, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.PrintDocLab, "打印单据", "打印单据按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.PrintDocEnable, false, "是否启用", true, true);

                map.AddTBString(AccpterAttr.AthLab, "附件", "附件按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.FJOpen, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.TrackLab, "轨迹", "轨迹按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.TrackEnable, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.SelectAccepterLab, "接受人", "接受人按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(AccpterAttr.SelectAccepterEnable, 0, "方式",
          true, true, AccpterAttr.SelectAccepterEnable);

               // map.AddBoolean(AccpterAttr.SelectAccepterEnable, false, "是否启用", true, true);

                map.AddTBString(AccpterAttr.OptLab, "选项", "选项按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.OptEnable, true, "是否启用", true, true);

                map.AddTBString(AccpterAttr.SearchLab, "查询", "查询按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(AccpterAttr.SearchEnable, true, "是否启用", true, true);

                //map.AddTBString(AccpterAttr.URL, null, "URL", true, false, 0, 50, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// Accpter
    /// </summary>
    public class Accepters : Entities
    {
        /// <summary>
        /// Accpter
        /// </summary>
        public Accepters()
        {
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Accepter();
            }
        }
    }
}
