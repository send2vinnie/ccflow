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
    public class FlowDoc : EntityNoName
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
        public FlowDoc()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public FlowDoc(string _No)
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

                map.AddTBStringPK(BP.WF.FlowAttr.No, null, this.ToE("No", "���"), true, true, 1, 10, 3);
                map.AddTBString(BP.WF.FlowAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 50, 10);

                //map.AddDDLEntities(BP.WF.FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������"),
                //new FlowSorts(), true);
                //map.AddTBInt(BP.WF.FlowAttr.FlowSort, (int)BP.WF.FlowType.Panel, "��������", false, false);
                // @0=ҵ������@1=��������.
                // map.AddTBInt(BP.WF.FlowAttr.FlowSheetType, (int)FlowSheetType.SheetFlow, "������", false, false);


                map.AddDDLSysEnum(BP.WF.FlowAttr.DocType, (int)DocType.OfficialDoc, "��������", true, true,
                    BP.WF.FlowAttr.DocType, "@0=��ʽ����@1=�㺯");

                map.AddDDLSysEnum(BP.WF.FlowAttr.XWType, (int)XWType.Down, "��������", true, true, BP.WF.FlowAttr.XWType,
                    "@0=������@1=ƽ����@2=������");

                map.AddBoolean(BP.WF.FlowAttr.IsOK, true, this.ToE("IsEnable", "�Ƿ�����"), true, true);


               // map.AddBoolean(BP.WF.FlowAttr.CCType, false, "�Ƿ��Ͳ�����", true, true);

      //          map.AddDDLSysEnum(BP.WF.FlowAttr.CCType, (int)CCType.None, "��������", true, true, BP.WF.FlowAttr.CCType, 
      //              "@0=������@1=����Ա@2=����λ@3=���ڵ�@4=������@5=���ղ������λ");
      //          map.AddDDLSysEnum(BP.WF.FlowAttr.CCWay, (int)CCWay.ByMsg, "���ͷ�ʽ", true, true, BP.WF.FlowAttr.CCWay,
      //"@0=���ñ�ϵͳ��ʱ��Ϣ@1=ͨ��Email(��web.config������)@2=�����ֻ��ӿ�@3=�������ݿ⺯��");

                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "���з�ʽ", true, true, FlowAttr.FlowRunWay,
                    "@0=�ֹ�����@1=ָ����Ա��ʱ����@2=���ݼ���ʱ����@3=����ʽ����");
                map.AddTBString(FlowAttr.RunObj, null, "��������", true, false, 0, 100, 10);
               
                map.AddTBString(BP.WF.FlowAttr.Note, null, this.ToE("Note", "��ע"), true, false, 0, 100, 10,true);
                
                map.AddTBString(FlowAttr.StartListUrl, null, this.ToE("StartListUrl", "����Url"), true, false, 0, 500, 10, true);

                // map.AddBoolean(BP.WF.FlowAttr.CCType, false, "������ɺ��Ͳ�����Ա", true, true);
                // map.AddTBString(BP.WF.FlowAttr.CCStas, null, "Ҫ���͵ĸ�λ", false, false, 0, 2000, 10);
                // map.AddTBDecimal(BP.WF.FlowAttr.AvgDay, 0, "ƽ����������", false, false);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "��Ƽ�鱨��"); // "��Ƽ�鱨��";
                rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "��ͼ����"); //"��ͼ����";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoDRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowDocDataOut", "����ת������");  //"����ת������";
                //  rm.Icon = "/Images/Btn/Table.gif";
                rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";

                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);

                //map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow,
                //    FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "���͸�λ");


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  ��������
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
        /// ����֮�������
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
    public class FlowDocs : EntitiesNoName
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
        public FlowDocs() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public FlowDocs(string fk_sort)
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
                return new FlowDoc();
            }
        }
        #endregion
    }
}

