using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;

namespace BP.WF
{
    /// <summary>
    /// Btn属性
    /// </summary>
    public class BtnAttr : EntityNoNameAttr
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
        public const string CCEnable = "CCEnable";

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
    }
    /// <summary>
    /// Btn
    /// </summary>
    public class BtnLab : Entity
    {
        public static string Btns
        {
            get
            {
                return "Send,Save,Return,CC,Shift,Del,Rpt,Ath,Track,Opt,EndFLow";
            }
        }
        #region 基本属性
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(BtnAttr.NodeID);
            }
            set
            {
                this.SetValByKey(BtnAttr.NodeID, value);
            }
        }
        public string SearchLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.SearchLab);
            }
            set
            {
                this.SetValByKey(BtnAttr.SearchLab, value);
            }
        }
        public bool SearchEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.SearchEnable);
            }
            set
            {
                this.SetValByKey(BtnAttr.SearchEnable, value);
            }
        }

        public string ShiftLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.ShiftLab);
            }
            set
            {
                this.SetValByKey(BtnAttr.ShiftLab, value);
            }
        }
        public bool ShiftEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.ShiftEnable);
            }
            set
            {
                this.SetValByKey(BtnAttr.ShiftEnable, value);
            }
        }
        public string SelectAccepterLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.SelectAccepterLab);
            }
        }
        public bool SelectAccepterEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.SelectAccepterEnable);
            }
        }
        public string RptLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.RptLab);
            }
        }
        public bool RptEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.RptEnable);
            }
        }
        public string SaveLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.SaveLab);
            }
        }
        public bool SaveEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.SaveEnable);
            }
        }
        public string JumpWayLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.JumpWayLab);
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
                return this.GetValStringByKey(BtnAttr.ReturnLab);
            }
        }
        public bool ReturnEnable
        {
            get
            {
                return  this.GetValBooleanByKey(BtnAttr.ReturnRole);
            }
        }

        public string AthLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.AthLab);
            }
        }
        public bool AthEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.FJOpen);
            }
        }

        public string PrintDocLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.PrintDocLab);
            }
        }
        public bool PrintDocEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.PrintDocEnable);
            }
        }



        public string SendLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.SendLab);
            }
        }
        public bool SendEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.SendEnable);
            }
        }
        public string TrackLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.TrackLab);
            }
        }
        public bool TrackEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.TrackEnable);
            }
        }
        public string OptLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.OptLab);
            }
        }
        public bool OptEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.OptEnable);
            }
        }

        public string CCLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.CCLab);
            }
        }
        public bool CCEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.CCEnable);
            }
        }
        public string DeleteLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.DelLab);
            }
        }
        public bool DeleteEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.DelEnable);
            }
        }

        /// <summary>
        /// 结束流程
        /// </summary>
        public string EndFlowLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.EndFlowLab);
            }
        }
        /// <summary>
        /// 是否启用结束流程
        /// </summary>
        public bool EndFlowEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.EndFlowEnable);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// Btn
        /// </summary>
        public BtnLab() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeid"></param>
        public BtnLab(int nodeid)
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

                map.AddTBIntPK(BtnAttr.NodeID, 0, "NodeID", true, false);

                map.AddTBString(BtnAttr.SendLab, "发送", "发送按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SendEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.JumpWayLab, "跳转", "跳转按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(NodeAttr.JumpWay, false, "是否启用", true, true);


                map.AddTBString(BtnAttr.SaveLab, "保存", "保存按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SaveEnable, true, "是否启用", true, true);


                map.AddTBString(BtnAttr.ReturnLab, "退回", "退回按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ReturnRole, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.CCLab, "抄送", "抄送按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.CCEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.ShiftLab, "移交", "移交按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ShiftEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.DelLab, "删除流程", "删除流程按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.DelEnable, false, "是否启用", true, true);

                map.AddTBString(BtnAttr.EndFlowLab, "结束流程", "结束流程按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.EndFlowEnable, false, "是否启用", true, true);

                map.AddTBString(BtnAttr.RptLab, "报告", "报告按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.RptEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.PrintDocLab, "打印单据", "打印单据按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.PrintDocEnable, false, "是否启用", true, true);

                map.AddTBString(BtnAttr.AthLab, "附件", "附件按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.FJOpen, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.TrackLab, "轨迹", "轨迹按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.TrackEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.SelectAccepterLab, "接受人", "接受人按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SelectAccepterEnable, false, "是否启用", true, true);

                map.AddTBString(BtnAttr.OptLab, "选项", "选项按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.OptEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.SearchLab, "查询", "查询按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SearchEnable, true, "是否启用", true, true);

                //map.AddTBString(BtnAttr.URL, null, "URL", true, false, 0, 50, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// Btn
    /// </summary>
    public class BtnLabs : Entities
    {
        /// <summary>
        /// Btn
        /// </summary>
        public BtnLabs()
        {
        }
        /// <summary>
        /// Btn
        /// </summary>
        /// <param name="NodeID"></param>
        public BtnLabs(string NodeID)
        {
            this.Retrieve(BtnAttr.NodeID, NodeID);
        }
        public BtnLabs(int fk_node)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(BtnAttr.No, "SELECT FK_Btn FROM WF_BtnNode WHERE FK_Node=" + fk_node);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BtnLab();
            }
        }
    }
}
