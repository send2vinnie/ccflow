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
    /// 流程报表属性
    /// </summary>
    public class RptAttrAttr:BP.En.EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 类型
        /// </summary>
        public const string RefAttrOID = "RefAttrOID";
        /// <summary>
        /// RefFieldName
        /// </summary>
        public const string RefFieldName = "RefFieldName";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 执行
        /// </summary>
        public const string FK_Rpt = "FK_Rpt";
        public const string Field = "Field";
        public const string FieldName = "FieldName";
        public const string FieldNameRpt = "FieldNameRpt";
        public const string IsCanDel = "IsCanDel";
        public const string FK_Flow = "FK_Flow";
        public const string RefTable = "RefTable";
        public const string RefField = "RefField";
        public const string IsCanEdit = "IsCanEdit";
        public const string IDX = "IDX";
        #endregion
    }
    /// <summary>
    /// 这里存放每个外部程序设置的信息.	 
    /// </summary>
    public class RptAttr : EntityMyPK
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
        /// <summary>
        /// 外部程序设置的事务编号
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.FK_Node);
            }
            set
            {
                SetValByKey(RptAttrAttr.FK_Node, value);
            }
        }
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(RptAttrAttr.IDX);
            }
            set
            {
                SetValByKey(RptAttrAttr.IDX, value);
            }
        }
        public int RefAttrOID
        {
            get
            {
                return this.GetValIntByKey(RptAttrAttr.RefAttrOID);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefAttrOID, value);
            }
        }
        public string RefField
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefField);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefField, value);
            }
        }
        public string RefFieldName
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefFieldName);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefFieldName, value);
            }
        }
        public string FieldName
        {
            get
            {
                string s = this.GetValStringByKey(RptAttrAttr.FieldName);
                if (s == "")
                    return this.RefFieldName;
                return s;
            }
            set
            {
                SetValByKey(RptAttrAttr.FieldName, value);
            }
        }
        public string RefTable
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefTable);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefTable, value);
            }
        }
        /// <summary>
        /// 是否可以删除
        /// </summary>
        public bool IsCanDel
        {
            get
            {
                return this.GetValBooleanByKey(RptAttrAttr.IsCanDel);
            }
            set
            {
                this.SetValByKey(RptAttrAttr.IsCanDel, value);
            }
        }
        public bool IsCanEdit
        {
            get
            {
                return this.GetValBooleanByKey(RptAttrAttr.IsCanEdit);
            }
            set
            {
                this.SetValByKey(RptAttrAttr.IsCanEdit, value);
            }
        }
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptAttrAttr.FK_Rpt, value);
            }
        }
        
        public string Field
        {
            get
            {
                string s =  this.GetValStringByKey(RptAttrAttr.Field);
                if (s == "")
                    return this.RefField;
                return s;
            }
            set
            {
                SetValByKey(RptAttrAttr.Field, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(RptAttrAttr.FK_Node);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 外部程序设置
        /// </summary>
        public RptAttr() { }
        /// <summary>
        /// 外部程序设置
        /// </summary>
        /// <param name="_oid">外部程序设置ID</param>	
        public RptAttr(string _oid)
        {
            this.MyPK = _oid;
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
                Map map = new Map("WF_RptAttr");
                map.EnDesc = "流程视图属性";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();  

                map.AddTBString(RptAttrAttr.FK_Rpt, null, "报表", false, true, 0, 100, 10);
                map.AddDDLEntities(RptAttrAttr.FK_Node, null, "节点", new NodeExts(), false);

                map.AddTBInt(RptAttrAttr.RefAttrOID, 0, "属性ID", false, false);
                map.AddTBString(RptAttrAttr.RefTable, null, "物理表", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.RefField, null, "字段", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.RefFieldName, null, "字段名称", true, false, 0, 200, 10);

                map.AddTBString(RptAttrAttr.Field, null, "字段(转换后的)", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.FieldName, null, "字段名称(转换后的)", true, false, 0, 200, 10);

                map.AddTBInt(RptAttrAttr.IsCanDel, 0, "可否删除", false, false);
                map.AddTBInt(RptAttrAttr.IsCanEdit, 0, "可否编辑", false, false);

                map.AddTBInt(RptAttrAttr.IDX, 0, "序号", true, false);

                //map.AddTBInt(RptAttrAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(RptAttrAttr.ShowTime, 0, "发生时间", true, false, RptAttrAttr.ShowTime, "@0=无(显示在表单底部)@1=当工作选择时@2=当保存时@3=当发送时");
                //map.AddTBString(RptAttrAttr.FK_Node, null, "流程编号", false, true, 0, 100, 10);
                //map.AddTBString(RptAttrAttr.DoWhat, null, "执行什么？", false, true, 0, 100, 10);
                //map.AddTBInt(RptAttrAttr.H, 0, "窗口高度", false, false);
                //map.AddTBInt(RptAttrAttr.W, 0, "窗口宽度", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

       

        protected override void afterDelete()
        {
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            attr.Delete(BP.Sys.MapAttrAttr.FK_MapData, this.FK_Rpt,
                BP.Sys.MapAttrAttr.KeyOfEn, this.Field);

            base.afterDelete();
        }
 
        #region 顺序
        /// <summary>
        /// 先调整持续
        /// </summary>
        private void DoOrder()
        {
            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            int i = 0;
            foreach (RptAttr attr in attrs)
            {
                i++;
                attr.IDX = i;
                attr.Update(RptAttrAttr.IDX, attr.IDX);
            }
        }
        public string DoUp()
        {
            this.DoOrder();

            this.RetrieveFromDBSources();
            if (this.IDX == 1)
                return null;

            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            RptAttr attrUp = (RptAttr)attrs[this.IDX - 1 - 1];
            attrUp.Update(RptAttrAttr.IDX, this.IDX);
            this.Update(RptAttrAttr.IDX, this.IDX - 1);
            return null;
        }
        public string DoDown()
        {
            this.DoOrder();
            this.RetrieveFromDBSources();

            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            if (this.IDX == attrs.Count)
                return null;

            RptAttr attrDown = (RptAttr)attrs[this.IDX];
            attrDown.Update(RptAttrAttr.IDX, this.IDX);
            this.Update(RptAttrAttr.IDX, this.IDX + 1);
            return null;
        }
        #endregion
    }
    /// <summary>
    /// 外部程序设置集合
    /// </summary>
    public class RptAttrs : EntitiesMyPK
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new RptAttr();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 外部程序设置集合
        /// </summary>
        public RptAttrs()
        {
        }
        /// <summary>
        /// 外部程序设置集合.
        /// </summary>
        /// <param name="FlowNo"></param>
        public RptAttrs(string fk_rpt)
        {
            int i = this.Retrieve(RptAttrAttr.FK_Rpt, fk_rpt, RptAttrAttr.IDX);
        }
        public RptAttrs(int nodeid)
        {
            this.Retrieve(RptAttrAttr.FK_Node, nodeid, RptAttrAttr.IDX);
        }
        #endregion
    }
}
