using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
    /// <summary>
    /// 属性
    /// </summary>
    public class M2MAttr
    {
        public const string FK_Node = "FK_Node";
        public const string WorkID = "WorkID";
        public const string Doc = "Doc";
        public const string ValNames = "ValNames";
        public const string MapM2M = "MapM2M";

    }
	/// <summary>
    ///  M2M类别
	/// </summary>
    public class M2M : EntityMyPK
    {
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(M2MAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(M2MAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(M2MAttr.WorkID);
            }
            set
            {
                this.SetValByKey(M2MAttr.WorkID, value);
            }
        }
        public string MapM2M
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.MapM2M);
            }
            set
            {
                this.SetValByKey(M2MAttr.MapM2M, value);
            }
        }
        public string Vals
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.Doc);
            }
            set
            {
                this.SetValByKey(M2MAttr.Doc, value);
            }
        }
        public string ValNames
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.ValNames);
            }
            set
            {
                this.SetValByKey(M2MAttr.ValNames, value);
            }
        }
        #region 构造方法
        /// <summary>
        /// M2M数据存储
        /// </summary>
        public M2M()
        {
        }
        /// <summary>
        /// M2M数据存储
        /// </summary>
        /// <param name="_No"></param>
        public M2M(string _No) : base(_No) { }

        public M2M(int nodeid, int workid) 
        {
            this.FK_Node = nodeid;
            this.WorkID = workid;
            this.MyPK = this.FK_Node + "_" + this.WorkID;
            this.RetrieveFromDBSources();
        }
        #endregion

        /// <summary>
        /// M2M数据存储
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_M2M");
                map.EnDesc = "M2M数据存储";
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBInt(M2MAttr.FK_Node, 0, "FK_Node", true, true);
                map.AddTBInt(M2MAttr.WorkID, 0, "WorkID", true, false);

                map.AddTBString(M2MAttr.MapM2M, null, "MapM2M", true, true,0,20,20);

                map.AddTBStringDoc();
                map.AddTBStringDoc(M2MAttr.ValNames, null, "ValNames", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Node + "_" + this.WorkID+"_"+this.MapM2M;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
    /// M2M数据存储
	/// </summary>
    public class M2Ms : SimpleNoNames
    {
        /// <summary>
        /// M2M数据存储s
        /// </summary>
        public M2Ms() { }
        /// <summary>
        /// M2M数据存储s
        /// </summary>
        /// <param name="fk_node"></param>
        /// <param name="workid"></param>
        public M2Ms(int fk_node, Int64 workid)
        {
            this.Retrieve(M2MAttr.FK_Node, fk_node, M2MAttr.WorkID, workid);
        }
        /// <summary>
        /// M2M数据存储 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new M2M();
            }
        }
    }
}
