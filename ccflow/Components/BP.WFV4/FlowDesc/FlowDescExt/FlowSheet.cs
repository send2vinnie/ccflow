using System;
using System.Collections;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF.Ext
{
    /// <summary>
    /// 流程
    /// </summary>
    public class FlowSheet : EntityNoName
    {
        #region 构造方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 流程
        /// </summary>
        public FlowSheet()
        {
        }
        /// <summary>
        /// 流程
        /// </summary>
        /// <param name="_No">编号</param>
        public FlowSheet(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("流程编号不存在");
            }
            else
            {
                this.Retrieve();
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

                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "流程");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, this.ToE("No", "编号"), true, true, 1, 10, 3);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "流程类别"), new FlowSorts(), true);
                map.AddTBString(FlowAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 50, 10, true);
                map.AddBoolean(FlowAttr.IsOK, true, this.ToE("IsEnable", "是否起用"), true, true);

                // map.AddDDLSysEnum(BP.WF.FlowAttr.CCType, (int)CCType.None, "抄送类型", true, true, BP.WF.FlowAttr.CCType,
                // "@0=不抄送@1=按人员@2=按岗位@3=按节点@4=按部门@5=按照部门与岗位");
                // map.AddDDLSysEnum(BP.WF.FlowAttr.CCWay, (int)CCWay.ByMsg, "抄送方式", true, true, BP.WF.FlowAttr.CCWay,
                //"@0=调用本系统及时信息@1=通过Email(在web.config中配置)@2=调用手机接口@3=调用数据库函数");
                // map.AddBoolean(FlowAttr.IsCCAll, false, "是否抄送参与人", true, true);

                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, this.ToE("RunWay", "运行方式"), true, true, FlowAttr.FlowRunWay,
                    "@0=手工启动@1=按月启动@2=按周启动@3=按天启动@4=按小时启动");

                map.AddTBString(FlowAttr.FlowRunObj, null, this.ToE("RunDoc", "运行内容"), true, false, 0, 100, 10,true);
                map.AddBoolean(FlowAttr.IsCanStart, true, this.ToE("IsCanRunBySelf", "可以独立启动否？(独立启动的流程可以显示在发起流程列表里)"), true, true, true);

                map.AddTBStringDoc(BP.WF.FlowAttr.RunSQL, null, this.ToE("EndFlowRunSQL", "流程结束执行后执行的SQL"), true, false, true);


               // map.AddTBString(BP.WF.FlowAttr.RunSQL, null, this.ToE("Note", "备注"), true, false, 0, 100, 10, true);

                map.AddTBString(FlowAttr.Note, null, this.ToE("Note", "备注"), true, false, 0, 100, 10, true);
                map.AddTBString(FlowAttr.StartListUrl, null, this.ToE("StartListUrl", "导航Url"), true, false, 0, 500, 10, true);

                //  map.AddTBInt("Age", 20, "年龄", true, false);
                //  map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow,
                //  FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "抄送岗位");

                //map.AttrsOfOneVSM.Add(new FlowEmps(), new Emps(), FlowEmpAttr.FK_Flow,
                //FlowEmpAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, "干预人员");

                map.AddSearchAttr(BP.WF.FlowAttr.FK_FlowSort);
                map.AddSearchAttr(BP.WF.FlowAttr.FlowRunWay);


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("CCNode", "抄送节点"); // "抄送节点";
                rm.ToolTip = "当抄送方式设置为抄送节点时，此设置才有效。";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCCNode";
                //map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "设计检查报告"); // "设计检查报告";
                //rm.ToolTip = "检查流程设计的问题。";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("ViewDef", "视图定义"); //"视图定义";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoDRpt";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("Rpt", "报表"); // "报表运行";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Icon = "/Images/Btn/Delete.gif";
                rm.Title = this.ToE("DelFlowData", "删除数据"); // "删除数据";
                rm.Warning = this.ToE("AYS", "您确定要执行删除流程数据吗?");// "您确定要执行删除流程数据吗？";

                //  rm.Warning = "您确定要执行删除流程数据吗？";
                //rm.ToolTip = "清除历史流程数据。";

                rm.ClassMethodName = this.ToString() + ".DoDelData";
                map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = this.ToE("Event", "事件"); // "报表运行";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                ////rm.Icon = "/Images/Btn/Table.gif";
                //map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSheetDataOut", "数据转出定义");  //"数据转出定义";
                ////  rm.Icon = "/Images/Btn/Table.gif";
                //rm.ToolTip = "在流程完成时间，流程数据转储存到其它系统中应用。";
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  公共方法
        public string DoCCNode()
        {
            PubClass.WinOpen("./../WF/Admin/CCNode.aspx?FK_Flow=" + this.No, 400, 500);
            return null;
        }
        /// <summary>
        /// 执行检查
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoCheck();
        }

        public string DoDelData()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDelData();
        }

        /// <summary>
        /// 设计数据转出
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoExp();
        }
        /// <summary>
        /// 定义报表
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDRpt();
        }
        /// <summary>
        /// 运行报表
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoOpenRpt();
        }
        /// <summary>
        /// 更新之后的事情，也要更新缓存。
        /// </summary>
        protected override void afterUpdate()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            fl.Update();
            base.afterUpdate();
        }
        #endregion
    }
    /// <summary>
    /// 流程集合
    /// </summary>
    public class FlowSheets : EntitiesNoName
    {
        #region 查询
        /// <summary>
        /// 查询出来全部的在生存期间内的流程
        /// </summary>
        /// <param name="FlowSort">流程类别</param>
        /// <param name="IsCountInLifeCycle">是不是计算在生存期间内 true 查询出来全部的 </param>
        public void Retrieve(string FlowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BP.WF.FlowAttr.FK_FlowSort, FlowSort);
            qo.addOrderBy(BP.WF.FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 工作流程
        /// </summary>
        public FlowSheets() { }
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="fk_sort"></param>
        public FlowSheets(string fk_sort)
        {
            this.Retrieve(BP.WF.FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region 得到实体
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowSheet();
            }
        }
        #endregion
    }
}

