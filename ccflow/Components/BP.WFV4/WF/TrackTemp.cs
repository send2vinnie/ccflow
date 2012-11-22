using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;

namespace BP.WF
{
    public enum ActionType
    {
        /// <summary>
        /// ����
        /// </summary>
        Start,
        /// <summary>
        /// ǰ��
        /// </summary>
        Forward,
        /// <summary>
        /// �˻�
        /// </summary>
        Return,
        /// <summary>
        /// �ƽ�
        /// </summary>
        Shift,
        /// <summary>
        /// �����ƽ�
        /// </summary>
        UnShift,
        /// <summary>
        /// ����
        /// </summary>
        Undo,
        /// <summary>
        /// ����ǰ��
        /// </summary>
        ForwardFL,
        /// <summary>
        /// ����ǰ��
        /// </summary>
        ForwardHL,
        /// <summary>
        /// ���̽���
        /// </summary>
        FlowOver,
        /// <summary>
        /// ������������
        /// </summary>
        CallSubFlow,
        /// <summary>
        /// ����������
        /// </summary>
        StartSubFlow,
        /// <summary>
        /// ���߳�ǰ��
        /// </summary>
        SubFlowForward,
        /// <summary>
        /// ȡ��
        /// </summary>
        Tackback,
        /// <summary>
        /// �ָ�����ɵ�����
        /// </summary>
        RebackOverFlow,
        /// <summary>
        /// ǿ����ֹ���� For lijian:2012-10-24
        /// </summary>
        FlowOverByCoercion,
        /// <summary>
        /// ����
        /// </summary>
        Hung,
        /// <summary>
        /// ȡ������
        /// </summary>
        UnHung
    }
    /// <summary>
    ///  ����
    /// </summary>
    public class TrackTempAttr
    {
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �������
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �����
        /// </summary>
        public const string ActionType = "ActionType";
        /// <summary>
        /// ʱ����
        /// </summary>
        public const string WorkTimeSpan = "WorkTimeSpan";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeData = "NodeData";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// �켣��ʱ���ֶ�
        /// </summary>
        public const string TrackTempFields = "TrackTempFields";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// �ӽڵ�
        /// </summary>
        public const string NDFrom = "NDFrom";
        /// <summary>
        /// ���ڵ�
        /// </summary>
        public const string NDTo = "NDTo";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string EmpFrom = "EmpFrom";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string EmpTo = "EmpTo";
        /// <summary>
        /// ���
        /// </summary>
        public const string Msg = "Msg";
        /// <summary>
        /// EmpFromT
        /// </summary>
        public const string EmpFromT = "EmpFromT";
        /// <summary>
        /// NDFromT
        /// </summary>
        public const string NDFromT = "NDFromT";
        /// <summary>
        /// NDToT
        /// </summary>
        public const string NDToT = "NDToT";
        /// <summary>
        /// EmpToT
        /// </summary>
        public const string EmpToT = "EmpToT";
        /// <summary>
        /// ʵ��ִ����Ա
        /// </summary>
        public const string Exer = "Exer";
    }
    /// <summary>
    /// �켣��ʱ��
    /// </summary>
    public class TrackTemp : BP.En.EntityMyPK
    {
        #region �켣��ʱ��
        /// <summary>
        /// �ڵ��
        /// </summary>
        public int NDFrom
        {
            get
            {
                return this.GetValIntByKey(TrackTempAttr.NDFrom);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDFrom, value);
            }
        }
        /// <summary>
        /// �ڵ㵽
        /// </summary>
        public int NDTo
        {
            get
            {
                return this.GetValIntByKey(TrackTempAttr.NDTo);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDTo, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string EmpFrom
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpFrom);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpFrom, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string EmpTo
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpTo);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpTo, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.RDT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.RDT, value);
            }
        }
        /// <summary>
        /// fid
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(TrackTempAttr.FID);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.FID, value);
            }
        }
        /// <summary>
        /// Workid
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TrackTempAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.WorkID, value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public ActionType HisActionType
        {
            get
            {
                return (ActionType)this.GetValIntByKey(TrackTempAttr.ActionType);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.ActionType, (int)value);
            }
        }
        public static string GetActionTypeT(ActionType at)
        {
            switch (at)
            {
                case ActionType.Forward:
                    return "ǰ��";
                case ActionType.Return:
                    return "�˻�";
                case ActionType.Shift:
                    return "�ƽ�";
                case ActionType.UnShift:
                    return "�����ƽ�";
                case ActionType.Start:
                    return "����";
                case ActionType.Undo:
                    return "��������";
                case ActionType.ForwardFL:
                    return " -ǰ��(������)";
                case ActionType.ForwardHL:
                    return " -������㷢��";
                case ActionType.FlowOver:
                    return "���̽���";
                case ActionType.CallSubFlow:
                    return "������������";
                case ActionType.StartSubFlow:
                    return "�����̷���";
                case ActionType.SubFlowForward:
                    return "������ǰ��";
                case ActionType.RebackOverFlow:
                    return "�ָ�����ɵ�����";
                case ActionType.FlowOverByCoercion:
                    return "ǿ�ƽ�������";
                default:
                    return "δ֪";
            }
        }
        public string HisActionTypeT
        {
            get
            {
                return TrackTemp.GetActionTypeT(this.HisActionType);
            }
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string NodeData
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NodeData);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NodeData, value);
            }
        }
        public string Exer
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.Exer);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.Exer, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Msg
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.Msg);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.Msg, value);
            }
        }
        public string MsgHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(TrackTempAttr.Msg);
            }
        }
        public string EmpToT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpToT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpToT, value);
            }
        }
        public string EmpFromT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpFromT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpFromT, value);
            }
        }

        public string NDFromT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NDFromT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDFromT, value);
            }
        }
        public string NDToT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NDToT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDToT, value);
            }
        }
        #endregion attrs

        #region ����
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "WF_TrackTemp"; // Ҫ�����
                map.EnDesc = "�켣��ʱ��";
                map.EnType = EnType.App;
                #endregion

                #region �ֶ�
                map.AddMyPK();
                map.AddTBString(TrackTempAttr.FK_Flow, null, "����", true, false, 0, 100, 100);
                map.AddTBInt(TrackTempAttr.ActionType, 0, "��������", true, false);

                map.AddTBInt(TrackTempAttr.FID, 0, "����ID", true, false);
                map.AddTBInt(TrackTempAttr.WorkID, 0, "����ID", true, false);

                map.AddTBInt(TrackTempAttr.NDFrom, 0, "�ӽڵ�", true, false);
                map.AddTBString(TrackTempAttr.NDFromT, null, "�ӽڵ�(����)", true, false, 0, 100, 100);

                map.AddTBInt(TrackTempAttr.NDTo, 0, "���ڵ�", true, false);
                map.AddTBString(TrackTempAttr.NDToT, null, "���ڵ�(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.EmpFrom, null, "����Ա", true, false, 0, 100, 100);
                map.AddTBString(TrackTempAttr.EmpFromT, null, "����Ա(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.EmpTo, null, "����Ա", true, false, 0, 4000, 100);
                map.AddTBString(TrackTempAttr.EmpToT, null, "����Ա(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.RDT, null, "����", true, false, 0, 30, 100);

                map.AddTBFloat(TrackTempAttr.WorkTimeSpan, 0, "ʱ����(��)", true, false);
                map.AddTBStringDoc(TrackTempAttr.Msg, null, "��Ϣ", true, false);
                map.AddTBStringDoc(TrackTempAttr.NodeData, null, "�ڵ�����(��־��Ϣ)", true, false);

                map.AddTBString(TrackTempAttr.Exer, null, "ִ����", true, false, 0, 30, 100);
                #endregion �ֶ�

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// �켣��ʱ��
        /// </summary>
        /// <param name="rptName"></param>
        public TrackTemp(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// �켣��ʱ��
        /// </summary>
        public TrackTemp()
        {
        }
        /// <summary>
        /// ������Ȩ��
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (BP.Web.WebUser.IsAuthorize)
                this.Exer = BP.Web.WebUser.AuthorizerEmpID + "," + BP.Web.WebUser.Auth;
            else
                this.Exer = BP.Web.WebUser.No + "," + BP.Web.WebUser.Name;
            return base.beforeInsert();
        }
        #endregion attrs
    }
    /// <summary>
    /// �켣��ʱ����
    /// </summary>
    public class TrackTemps : BP.En.Entities
    {
        /// <summary>
        /// �켣��ʱ����
        /// </summary>
        public TrackTemps()
        {
        }
        public override Entity GetNewEntity
        {
            get
            {
                return new TrackTemp();
            }
        }
    }
}
