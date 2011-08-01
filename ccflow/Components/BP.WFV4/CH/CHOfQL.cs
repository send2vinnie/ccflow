using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// 质量考核
    /// </summary>
    public class CHOfQLAttr : EntityNoNameAttr
    {
        public const string FK_Node = "FK_Node";
        public const string Desc = "Desc";
        public const string FK_Flow = "FK_Flow";
        public const string Cent = "Cent";
        public const string Ext = "Ext";
        public const string FK_Emp = "FK_Emp";
        public const string RDT = "RDT";
        public const string WorkID = "WorkID";
        /// <summary>
        /// MyPK
        /// </summary>
        public const string MyPK = "MyPK";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        public const string FK_NY = "FK_NY";
        public const string FK_Dept = "FK_Dept";
        public const string Note1 = "Note1";
        public const string Note2 = "Note2";
    }
	/// <summary>
	/// 质量考核
	/// </summary>
	public class CHOfQL :Entity
	{
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.Readonly();
                return uac;
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfQLAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.WorkID, value);
            }
        }
        public string Note1
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Note1);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Note1, value);
            }
        }
        public string Note2
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Note2);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Note2, value);
            }
        }

        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Node, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Flow, value);
            }
        }
        public string FK_NodeText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Node);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Emp);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.RDT);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.RDT, value);
            }
        }
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Ext);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Ext, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Emp, value);
            }
        }
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Desc);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Desc, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_NY, value);
            }
        }
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.MyPK);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.MyPK, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Rec);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Rec, value);
            }
        }
        public string FK_FlowText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Flow);
            }
        }
        public int Cent
        {
            get
            {
                return this.GetValIntByKey(CHOfQLAttr.Cent);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Cent, value);
            }
        }
        
		#region 实现基本的方方法	
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_CHOfQL");
                map.EnDesc = "质量考核";
                map.EnType = EnType.App;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddMyPK();

                map.AddDDLEntities(CHOfQLAttr.FK_Flow, null, "流程", new Flows(), true);
                map.AddDDLEntities(CHOfQLAttr.FK_Node, null, "节点", new NodeExts(), true);

                map.AddDDLEntities(CHOfQLAttr.Rec, null, "考核对象", new Port.Emps(), true);
                map.AddTBInt(CHOfQLAttr.Cent, 0, "分值", true, false);

                map.AddDDLEntities(CHOfQLAttr.FK_Emp, null, "考核人", new Port.Emps(), true);
                map.AddTBDate(CHOfQLAttr.RDT, "考核时间", true, true);


                map.AddTBStringDoc(CHOfQLAttr.Note1, "", "奖惩原因", true, true);
                map.AddTBStringDoc(CHOfQLAttr.Note2, "", "备注", true, true);


                map.AddDDLEntities(CHOfQLAttr.FK_NY, null, "年月", new BP.Pub.NYs(), true);
                map.AddDDLEntities(CHOfQLAttr.FK_Dept, null, "部门", new BP.Port.Depts(), true);
                map.AddTBInt(CHOfQLAttr.WorkID, 0, "WorkID", false, false);


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告"); // "工作报告";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCheck", "执行考核"); // "执行考核";
                rm.ClassMethodName = this.ToString() + ".DoCheck";

                Attrs attrs = new Attrs();
                attrs.AddTBString(CHOfQLAttr.Rec, "当前操作员", "被考核对象", true, true, 0, 100, 10);
                attrs.AddTBInt(CHOfQLAttr.Cent, 0, "奖/扣分(用正负表示)", true, false );
                attrs.AddTBStringDoc(CHOfQLAttr.Note1, "", "奖惩原因", true, false);
                attrs.AddTBStringDoc(CHOfQLAttr.Note2, "", "备注", true, false);
                rm.HisAttrs = attrs;

                map.AddRefMethod(rm);

                map.AddSearchAttr(CHOfQLAttr.FK_Dept);
                map.AddSearchAttr(CHOfQLAttr.FK_Emp);
                map.AddSearchAttr(CHOfQLAttr.FK_NY);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        public string DoRpt()
        {
            PubClass.WinOpen("../WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node);
            return null;
        }
        public string DoCheck(string tmp, int cent, string note1, string note2)
        {
            if (this.FK_Emp != Web.WebUser.No)
                return "您不是考核人，所以您不能执行。";
 

            if (cent == 0)
                return "考核分值为0，系统拒绝执行。";

           

            this.Cent = cent;
            this.Note1 = note1;
            this.Note2 = note2;
            this.Update();

            return "考核执行成功。";
        }

		#region 构造方法
		/// <summary>
		/// 质量考核
		/// </summary> 
        public CHOfQL(Int64 workid, string FK_Node)
        {
            BP.WF.CHOfFlow ch = new CHOfFlow(workid);
            this.Copy(ch);

            this.WorkID = workid;
            this.FK_Node = FK_Node;

            Node nd = new Node(int.Parse(this.FK_Node));


            BP.WF.Work wk = nd.HisWork;
            wk.OID = workid;
            wk.RetrieveFromDBSources();

            this.Rec = wk.Rec;
            this.RDT = wk.RDT;
            this.FK_Emp = Web.WebUser.No;


            this.MyPK = this.FK_Node + "@" + Web.WebUser.No + "@" + this.WorkID;
            if (ch.RetrieveFromDBSources() == 0)
                this.Insert();

        }
        /// <summary>
        /// 质量考核
        /// </summary>
        /// <param name="mypk"></param>
        public CHOfQL(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        public CHOfQL()
        {
        }
		#endregion 
	}

    public class CHOfQLs : EntitiesNoName
	{
		/// <summary>
		/// 质量考核
		/// </summary>
		public CHOfQLs(){}
		/// <summary>
		/// 得到它的 质量考核
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new CHOfQL();
            }
        }
      
	}
}
