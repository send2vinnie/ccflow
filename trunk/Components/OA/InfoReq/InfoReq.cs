using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum ReqSta
    {
        /// <summary>
        /// 未处理
        /// </summary>
        UnDeal=0,
        /// <summary>
        /// 处理成功
        /// </summary>
        OK,
        /// <summary>
        /// 处理中
        /// </summary>
        Dealing,
        /// <summary>
        /// 备案数据
        /// </summary>
        Dust
    }
	/// <summary>
    /// 信息反馈连接
	/// </summary>
    public class InfoReqAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 类别
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        /// <summary>
        /// 记录日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 联系人
        /// </summary>
        public const string LinkMan = "LinkMan";
        /// <summary>
        /// LinkMan
        /// </summary>
        public const string LinkAddress = "LinkAddress";
        /// <summary>
        /// 联系电话
        /// </summary>
        public const string LinkTel="LinkTel";
        /// <summary>
        /// Email
        /// </summary>
        public const string LinkEmail="LinkEmail"; 
        /// <summary>
        /// 状态
        /// </summary>
        public const string ReqSta = "ReqSta";
        /// <summary>
        /// 是否公开
        /// </summary>
        public const string IsOpen = "IsOpen";
        /// <summary>
        /// 采集信息
        /// </summary>
        public const string SubDoc = "SubDoc";
        /// <summary>
        /// IP
        /// </summary>
        public const string IP = "IP";
        /// <summary>
        /// 提交人
        /// </summary>
        public const string SubMan = "SubMan";
        /// <summary>
        /// 处理人
        /// </summary>
        public const string DealMan = "DealMan";
        /// <summary>
        /// 处理内容
        /// </summary>
        public const string DealDoc = "DealDoc";
        /// <summary>
        /// 处理日期
        /// </summary>
        public const string DealRDT = "DealRDT";

        public const string IsAnonymous = "IsAnonymous";
    }
	/// <summary>
    /// 信息反馈
	/// </summary>
    public class InfoReq : EntityNoName
    {
        #region 属性
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = true;
                uac.IsUpdate = true;
                uac.IsInsert = false;
                return uac;
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.RDT, value);
            }
        }

        // 是否设为焦点
        public string IsOpen
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.IsOpen);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.IsOpen, value);
            }
        }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnonymous
        {
            get
            {
                return this.GetValBooleanByKey(InfoReqAttr.IsAnonymous);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.IsAnonymous, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string SubDoc
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.SubDoc);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.SubDoc, value);
            }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkMan);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkMan, value);
            }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string LinkAddress
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkAddress);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkAddress, value);
            }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkTel
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkTel);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkTel, value);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string LinkEmail 
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkEmail);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkEmail, value);
            }
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string DealRDT
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealRDT);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealRDT, value);
            }
        }
        /// <summary>
        /// 处理人
        /// </summary>
        public string DealMan
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealMan);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealMan, value);
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.FK_Sort);
            }
        }
        /// <summary>
        /// 处理状态
        /// </summary>
        public ReqSta ReqSta
        {
            get
            {
                return (ReqSta)this.GetValIntByKey(InfoReqAttr.ReqSta);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.ReqSta, (int)value);
            }
        }
        /// <summary>
        /// 回复
        /// </summary>
        public string DealDoc
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealDoc);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealDoc, value);
            }
        }
        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 信息反馈
        /// </summary>
        public InfoReq(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息反馈
        /// </summary>
        public InfoReq()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_InfoReq");

                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.InfoReqSorts' >类别</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.InfoReqs')\">属性设置</a>";

                map.IsAutoGenerNo = true;
                map.EnType = EnType.Sys;
                map.EnDesc = "信息反馈";
                map.DepositaryOfEntity = Depositary.Application;
               
                map.CodeStruct = "5";
                map.MoveTo = InfoReqAttr.ReqSta;

                map.AddTBStringPK(InfoReqAttr.No, null, "编号", false, true, 5, 5, 5);
                map.AddDDLEntities(InfoReqAttr.FK_Sort, null, "类别", new InfoReqSorts(), false);
                map.AddDDLSysEnum(InfoReqAttr.ReqSta, 0, "状态", true, true, InfoReqAttr.ReqSta, "@0=未处理@1=已处理@2=处理中@3=备案数据");
                map.AddTBString(InfoReqAttr.Name, null, "标题", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(InfoReqAttr.SubDoc, null, "提交信息", true, true, true);
                map.AddTBString(InfoReqAttr.LinkMan, null, "联系人", true, true, 0, 100, 10, true);
                map.AddTBString(InfoReqAttr.LinkAddress, null, "联系地址", true, true, 0, 500, 10, true);
                map.AddTBString(InfoReqAttr.LinkTel, null, "联系电话", true, true, 0, 50, 10, true);
                map.AddTBString(InfoReqAttr.LinkEmail, null, "电子邮箱", true, true, 0, 100, 10, true);

                map.AddTBDate(InfoReqAttr.RDT, null, "提交日期", true, true);


                map.AddTBDate(InfoReqAttr.DealMan, null, "处理人", true, false);
                map.AddTBStringDoc(InfoReqAttr.DealDoc, null, "处理内容", true, false, true);
                map.AddTBDate(InfoReqAttr.DealRDT, null, "处理日期", true, false);
                map.AddBoolean(InfoReqAttr.IsOpen, false, "是否公开", true, true);
                map.AddBoolean(InfoReqAttr.IsAnonymous, true, "匿名发布", true, true);
                //map.AddMyFileS();
                map.AddSearchAttr(InfoReqAttr.FK_Sort);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            this.RDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.DealRDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            if (this.DealMan == "")
                this.DealMan = Web.WebUser.Name;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
    /// 信息反馈s
	/// </summary>
    public class InfoReqs : EntitiesNoName
    {
        /// <summary>
        /// 信息反馈s
        /// </summary>
        public InfoReqs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoReq();
            }
        }
    }
}
