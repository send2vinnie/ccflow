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
    public class ExpDtlAttr 
    {
        #region 基本属性
        public const string MyPK = "MyPK";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 字段
        /// </summary>
        public const string Field = "Field";
        /// <summary>
        /// 字段名称
        /// </summary>
        public const string FieldName = "FieldName";
        /// <summary>
        /// 对方字段
        /// </summary>
        public const string RefField = "RefField";
        /// <summary>
        /// 序号
        /// </summary>
        public const string IDX = "IDX";
        public const string FK_Exp = "FK_Exp";
        #endregion
    }
    /// <summary>
    /// 这里存放每个外部程序设置的信息.	 
    /// </summary>
    public class ExpDtl : EntityMyPK
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
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(ExpDtlAttr.FK_Node);
            }
            set
            {
                SetValByKey(ExpDtlAttr.FK_Node, value);
            }
        }
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(ExpDtlAttr.IDX);
            }
            set
            {
                SetValByKey(ExpDtlAttr.IDX, value);
            }
        }
         public string RefField
        {
            get
            {
                return this.GetValStringByKey(ExpDtlAttr.RefField);
            }
            set
            {
                SetValByKey(ExpDtlAttr.RefField, value);
            }
        }
        public string FK_Exp
        {
            get
            {
                return this.GetValStringByKey(ExpDtlAttr.FK_Exp);
            }
            set
            {
                SetValByKey(ExpDtlAttr.FK_Exp, value);
            }
        }
     
        public string FieldName
        {
            get
            {
                string s = this.GetValStringByKey(ExpDtlAttr.FieldName);
                //if (s == "")
                //    return this.RefFieldName;
                return s;
            }
            set
            {
                SetValByKey(ExpDtlAttr.FieldName, value);
            }
        }
     
        
        public string Field
        {
            get
            {
                string s =  this.GetValStringByKey(ExpDtlAttr.Field);
                if (s == "")
                    return this.RefField;
                return s;
            }
            set
            {
                SetValByKey(ExpDtlAttr.Field, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(ExpDtlAttr.FK_Node);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 外部程序设置
        /// </summary>
        public ExpDtl() { }
        /// <summary>
        /// 外部程序设置
        /// </summary>
        /// <param name="_oid">外部程序设置ID</param>	
        public ExpDtl(string _oid)
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
                Map map = new Map("WF_ExpDtl");
                map.EnDesc = "流程视图属性";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();  /* FK_Node +"_"+ RefOID */

                //map.AddTBString(ExpDtlAttr.FK_Exp, null, "报表", false, true, 0, 100, 10);
                //map.AddDDLEntities(ExpDtlAttr.FK_Node, null, "节点", new NodeExts(), false);
                //map.AddTBInt(ExpDtlAttr.RefAttrOID, 0, "属性ID", false, false);
                //map.AddTBString(ExpDtlAttr.RefTable, null, "物理表", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.RefField, null, "字段", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.RefFieldName, null, "字段名称", true, false, 0, 200, 10);

                //map.AddTBString(ExpDtlAttr.Field, null, "字段(转换后的)", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.FieldName, null, "字段名称(转换后的)", true, false, 0, 200, 10);

              

                map.AddTBInt(ExpDtlAttr.IDX, 0, "序号", true, false);

                //map.AddTBInt(ExpDtlAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(ExpDtlAttr.ShowTime, 0, "发生时间", true, false, ExpDtlAttr.ShowTime, "@0=无(显示在表单底部)@1=当工作选择时@2=当保存时@3=当发送时");
                //map.AddTBString(ExpDtlAttr.FK_Node, null, "流程编号", false, true, 0, 100, 10);
                //map.AddTBString(ExpDtlAttr.DoWhat, null, "执行什么？", false, true, 0, 100, 10);
                //map.AddTBInt(ExpDtlAttr.H, 0, "窗口高度", false, false);
                //map.AddTBInt(ExpDtlAttr.W, 0, "窗口宽度", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override void afterDelete()
        {
            //BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            //attr.Delete(BP.Sys.MapAttrAttr.FK_MapData, this.FK_Exp,
            //    BP.Sys.MapAttrAttr.KeyOfEn, this.Field);

            base.afterDelete();
        }
 
        #region 顺序
        /// <summary>
        /// 先调整持续
        /// </summary>
        private void DoOrder()
        {
            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            int i = 0;
            foreach (ExpDtl attr in attrs)
            {
                i++;
                attr.IDX = i;
                attr.Update(ExpDtlAttr.IDX, attr.IDX);
            }
        }
        public string DoUp()
        {
            this.DoOrder();

            this.RetrieveFromDBSources();
            if (this.IDX == 1)
                return null;

            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            ExpDtl attrUp = (ExpDtl)attrs[this.IDX - 1 - 1];
            attrUp.Update(ExpDtlAttr.IDX, this.IDX);
            this.Update(ExpDtlAttr.IDX, this.IDX - 1);
            return null;
        }
        public string DoDown()
        {
            this.DoOrder();
            this.RetrieveFromDBSources();

            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            if (this.IDX == attrs.Count)
                return null;

            ExpDtl attrDown = (ExpDtl)attrs[this.IDX];
            attrDown.Update(ExpDtlAttr.IDX, this.IDX);
            this.Update(ExpDtlAttr.IDX, this.IDX + 1);
            return null;
        }
        #endregion
    }
    /// <summary>
    /// 外部程序设置集合
    /// </summary>
    public class ExpDtls : EntitiesMyPK
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ExpDtl();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 外部程序设置集合
        /// </summary>
        public ExpDtls()
        {
        }
        /// <summary>
        /// 外部程序设置集合.
        /// </summary>
        /// <param name="FlowNo"></param>
        public ExpDtls(string fk_rpt)
        {
          //  int i = this.Retrieve(ExpDtlAttr.FK_Exp, fk_rpt, ExpDtlAttr.IDX);
        }
        public ExpDtls(int nodeid)
        {
            this.Retrieve(ExpDtlAttr.FK_Node, nodeid, ExpDtlAttr.IDX);
        }
        #endregion
    }
}
