using System;
using System.Collections;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF.Ext
{
    /// <summary>
    /// ����
    /// </summary>
    public class FlowSheet : EntityNoName
    {
        #region ���췽��
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
        /// ����
        /// </summary>
        public FlowSheet()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public FlowSheet(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("���̱�Ų�����");
            }
            else
            {
                this.Retrieve();
            }
        }
        /// <summary>
        /// ��д���෽��
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
                map.EnDesc = this.ToE("Flow", "����");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, this.ToE("No", "���"), true, true, 1, 10, 3);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������"),
                    new FlowSorts(), true);
                map.AddTBString(FlowAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 50, 10, true);
                map.AddBoolean(FlowAttr.IsOK, true, this.ToE("IsEnable", "�Ƿ�����"), true, true);

                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, this.ToE("RunWay", "���з�ʽ"),
                    true, true, FlowAttr.FlowRunWay, "@0=�ֹ�����@1=ָ����Ա��ʱ����@2=���ݼ���ʱ����@3=����ʽ����");

                map.AddTBString(FlowAttr.RunObj, null, "��������", true, false, 0, 100, 10, true);
                map.AddBoolean(FlowAttr.IsCanStart, true, this.ToE("IsCanRunBySelf", "���Զ���������(�������������̿�����ʾ�ڷ��������б���)"), true, true, true);

                map.AddBoolean(FlowAttr.IsMD5, false, "�Ƿ������ݼ�������(MD5���ݼ��ܷ��۸�)", true, true,true);

                map.AddTBStringDoc(FlowAttr.Note, null, this.ToE("Note", "��ע"), 
                    true, false, true);
                map.AddTBString(FlowAttr.StartListUrl, null, this.ToE("StartListUrl", "����Url"), true, false, 0, 500, 10, true);

                map.AddDDLSysEnum(FlowAttr.AppType, (int)FlowAppType.Normal,"����Ӧ������",
                  true, true, "FlowAppType", "@0=������@1=������(������Ŀ�����)");


                map.AddSearchAttr(BP.WF.FlowAttr.FK_FlowSort);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("CCNode", "���ͽڵ�"); // "���ͽڵ�";
                rm.ToolTip = "�����ͷ�ʽ����Ϊ���ͽڵ�ʱ�������ò���Ч��";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCCNode";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "��鱨��"); // "��Ƽ�鱨��";
                //rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("Rpt", "��Ʊ���"); // "��������";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif"; 
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/Delete.gif";
                rm.Title = "ɾ������"; // this.ToE("DelFlowData", "ɾ������"); // "ɾ������";
                rm.Warning = this.ToE("AYS", "��ȷ��Ҫִ��ɾ������������?");// "��ȷ��Ҫִ��ɾ������������";
                rm.ClassMethodName = this.ToString() + ".DoDelData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/Delete.gif";
                rm.Title = "ɾ����������"; // this.ToE("DelFlowData", "ɾ������"); // "ɾ������";
                rm.ClassMethodName = this.ToString() + ".DoDelDataOne";
                rm.HisAttrs.AddTBInt("WorkID",0, "���빤��ID",true,false);
                rm.HisAttrs.AddTBString("sd", null, "ɾ����ע", true, false,0,100,100);
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.Title = "�������ɱ�������"; // "ɾ������";
                rm.Warning = this.ToE("AYS", "��ȷ��Ҫִ����? ע��:�˷����ķ���Դ��");// "��ȷ��Ҫִ��ɾ������������";
                rm.ClassMethodName = this.ToString() + ".DoReloadRptData";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "�����Զ���������Դ";
                rm.Icon = "/Images/Btn/DTS.gif";

                rm.ClassMethodName = this.ToString() + ".DoSetStartFlowDataSources()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "�ֹ�������ʱ����";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.Warning = this.ToE("AYS", "��ȷ��Ҫִ����? ע��:���������������������Ϊweb��ִ��ʱ�����ʱ���⣬�����ִ��ʧ�ܡ�");// "��ȷ��Ҫִ��ɾ������������";
                rm.ClassMethodName = this.ToString() + ".DoAutoStartIt()";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "�������ݹ���";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.ClassMethodName = this.ToString() + ".DoDataManger()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "�����������̱���";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.ClassMethodName = this.ToString() + ".DoGenerTitle()";
                rm.Warning = "��ȷ��Ҫ�����µĹ������²���������";
                map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = "�����Զ�����"; // "��������";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                ////rm.Icon = "/Images/Btn/Table.gif"; 
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("Event", "�¼�"); // "��������";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                ////rm.Icon = "/Images/Btn/Table.gif";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSheetDataOut", "����ת������");  //"����ת������";
                ////  rm.Icon = "/Images/Btn/Table.gif";
                //rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  ��������
        /// <summary>
        /// ���²������⣬�����µĹ���.
        /// </summary>
        public string DoGenerTitle()
        {
            if (WebUser.No != "admin")
                return "��admin�û�����ִ�С�";
            Flow fl = new Flow(this.No);
            Node nd = fl.HisStartNode;
            Works wks = nd.HisWorks;
            wks.RetrieveAllFromDBSource(WorkAttr.Rec);
            string table = nd.HisWork.EnMap.PhysicsTable;
            string tableRpt = "ND" + int.Parse(this.No) + "Rpt";
            foreach (Work wk in wks)
            {
                if (wk.NodeState == NodeState.Init)
                    continue;
                if (wk.Rec != WebUser.No)
                {
                    BP.Web.WebUser.Exit();
                    try
                    {
                        Emp emp = new Emp(wk.Rec);
                        BP.Web.WebUser.SignInOfGener(emp);
                    }
                    catch
                    {
                        continue;
                    }
                }
                string sql = "";
                string title = WorkNode.GenerTitle(wk);
                Paras ps = new Paras();
                ps.Add("Title", title);
                ps.Add("OID", wk.OID);
                ps.SQL = "UPDATE " + table + " SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE OID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE " + tableRpt + " SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE OID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE WF_GenerWorkFlow SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE WorkID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE WF_GenerFH SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE FID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQLs(sql);
            }
            Emp emp1 = new Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp1);
            return "ȫ�����ɳɹ�,Ӱ������(" + wks.Count + ")��";
        }
        /// <summary>
        /// �������ݹ���
        /// </summary>
        /// <returns></returns>
        public string DoDataManger()
        {
            PubClass.WinOpen("./../WF/Admin/FlowDB.aspx?s=d34&FK_Flow=" + this.No + "&ExtType=StartFlow&RefNo==", 700, 500);
            return null;
        }
        /// <summary>
        /// ���屨��
        /// </summary>
        /// <returns></returns>
        public string DoAutoStartIt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoAutoStartIt();
        }

        public string DoDelDataOne(int workid, string sd)
        {
            return "ɾ���ɹ� workid="+workid+"  ����:"+sd;
        }

        

        public string DoSetStartFlowDataSources()
        {
            string flowID=int.Parse(this.No).ToString()+"01";
            PubClass.WinOpen("./../WF/MapDef/MapExt.aspx?s=d34&FK_MapData=ND" + flowID + "&ExtType=StartFlow&RefNo==", 700, 500);
            return null;
        }
        public string DoCCNode()
        {
            PubClass.WinOpen("./../WF/Admin/CCNode.aspx?FK_Flow=" + this.No, 400, 500);
            return null;
        }
        /// <summary>
        /// ִ�м��
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoCheck();
        }

        public string DoReloadRptData()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoReloadRptData();
        }

        public string DoDelData()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDelData();
        }

        /// <summary>
        /// �������ת��
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
        /// ���屨��
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
        /// ���б���
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
        /// ����֮������飬ҲҪ���»��档
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
    /// ���̼���
    /// </summary>
    public class FlowSheets : EntitiesNoName
    {
        #region ��ѯ
        /// <summary>
        /// ��ѯ����ȫ�����������ڼ��ڵ�����
        /// </summary>
        /// <param name="FlowSort">�������</param>
        /// <param name="IsCountInLifeCycle">�ǲ��Ǽ����������ڼ��� true ��ѯ����ȫ���� </param>
        public void Retrieve(string FlowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BP.WF.FlowAttr.FK_FlowSort, FlowSort);
            qo.addOrderBy(BP.WF.FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public FlowSheets() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public FlowSheets(string fk_sort)
        {
            this.Retrieve(BP.WF.FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region �õ�ʵ��
        /// <summary>
        /// �õ����� Entity 
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

