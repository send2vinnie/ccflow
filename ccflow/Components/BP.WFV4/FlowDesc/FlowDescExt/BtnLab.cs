﻿using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;
//using BP.ZHZS.Base;

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
        /// 保存标签
        /// </summary>
        public const string SaveLab = "SaveLab";

        /// <summary>
        /// 退回是否启用
        /// </summary>
        public const string ReturnEnable = "ReturnEnable";
        /// <summary>
        /// 退回标签
        /// </summary>
        public const string ReturnLab = "ReturnLab";

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


        public const string TrackLab = "TrackLab";
        public const string TrackEnable = "TrackEnable";

        public const string OptLab = "OptLab";
        public const string OptEnable = "OptEnable";

        public const string CCLab = "CCLab";
        public const string CCEnable = "CCEnable";

        public const string DelLab = "DelLab";
        public const string DelEnable = "DelEnable";

        public const string AthLab = "AthLab";
        public const string AthEnable = "AthEnable";
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
                return "Send,Save,Return,CC,Shift,Del,Rpt,Ath,Track,Opt";
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
                return this.GetValBooleanByKey(BtnAttr.ReturnEnable);
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


        public string DelLab
        {
            get
            {
                return this.GetValStringByKey(BtnAttr.DelLab);
            }
        }
        public bool DelEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.DelEnable);
            }

        }

        #endregion

        #region 构造方法
        /// <summary>
        /// Btn
        /// </summary>
        public BtnLab() { }

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

                map.AddTBString(BtnAttr.SaveLab, "保存", "保存按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SaveEnable, true, "是否启用", true, true);


                map.AddTBString(BtnAttr.ReturnLab, "退回", "退回按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ReturnEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.CCLab, "抄送", "抄送按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.CCEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.ShiftLab, "移交", "移交按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ShiftEnable, true, "是否启用", true, true);


                map.AddTBString(BtnAttr.DelLab, "删除", "删除按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.DelEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.RptLab, "报告", "报告按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.RptEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.AthLab, "附件", "附件按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.AthEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.TrackLab, "轨迹", "轨迹按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.TrackEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.OptLab, "选项", "选项按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.OptEnable, true, "是否启用", true, true);
                 

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
