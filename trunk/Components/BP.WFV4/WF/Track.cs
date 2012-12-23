using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;

namespace BP.WF
{
 
    /// <summary>
    ///  ����
    /// </summary>
    public class TrackAttr:EntityMyPKAttr
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
        /// �켣�ֶ�
        /// </summary>
        public const string TrackFields = "TrackFields";
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
    /// �켣
    /// </summary>
    public class Track : BP.En.EntityMyPK
    {
        #region attrs
        /// <summary>
        /// �ڵ��
        /// </summary>
        public int NDFrom
        {
            get
            {
                return this.GetValIntByKey(TrackAttr.NDFrom);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDFrom, value);
            }
        }
        /// <summary>
        /// �ڵ㵽
        /// </summary>
        public int NDTo
        {
            get
            {
                return this.GetValIntByKey(TrackAttr.NDTo);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDTo, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TrackAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string EmpFrom
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpFrom);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpFrom, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string EmpTo
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpTo);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpTo, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.RDT);
            }
            set
            {
                this.SetValByKey(TrackAttr.RDT, value);
            }
        }
        /// <summary>
        /// fid
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.FID);
            }
            set
            {
                this.SetValByKey(TrackAttr.FID, value);
            }
        }
        /// <summary>
        /// Workid
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TrackAttr.WorkID, value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public ActionType HisActionType
        {
            get
            {
                return (ActionType)this.GetValIntByKey(TrackAttr.ActionType);
            }
            set
            {
                this.SetValByKey(TrackAttr.ActionType, (int)value);
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
                return this.GetValStringByKey(TrackAttr.NodeData);
            }
            set
            {
                this.SetValByKey(TrackAttr.NodeData, value);
            }
        }
        public string Exer
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.Exer);
            }
            set
            {
                this.SetValByKey(TrackAttr.Exer, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Msg
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.Msg);
            }
            set
            {
                this.SetValByKey(TrackAttr.Msg, value);
            }
        }
        public string MsgHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(TrackAttr.Msg);
            }
        }
        public string EmpToT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpToT);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpToT, value);
            }
        }
        public string EmpFromT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpFromT);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpFromT, value);
            }
        }

        public string NDFromT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NDFromT);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDFromT, value);
            }
        }
        public string NDToT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NDToT);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDToT, value);
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
                map.PhysicsTable = "WF_Track"; // Ҫ�����
                map.EnDesc = "�켣��";
                map.EnType = EnType.App;
                #endregion

                #region �ֶ�
                map.AddMyPK();
                map.AddTBString(TrackAttr.FK_Flow, null, "����", true, false, 0, 100, 100);
                map.AddTBInt(TrackAttr.ActionType, 0, "��������", true, false);

                map.AddTBInt(TrackAttr.FID, 0, "����ID", true, false);
                map.AddTBInt(TrackAttr.WorkID, 0, "����ID", true, false);

                map.AddTBInt(TrackAttr.NDFrom, 0, "�ӽڵ�", true, false);
                map.AddTBString(TrackAttr.NDFromT, null, "�ӽڵ�(����)", true, false, 0, 100, 100);

                map.AddTBInt(TrackAttr.NDTo, 0, "���ڵ�", true, false);
                map.AddTBString(TrackAttr.NDToT, null, "���ڵ�(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackAttr.EmpFrom, null, "����Ա", true, false, 0, 100, 100);
                map.AddTBString(TrackAttr.EmpFromT, null, "����Ա(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackAttr.EmpTo, null, "����Ա", true, false, 0, 4000, 100);
                map.AddTBString(TrackAttr.EmpToT, null, "����Ա(����)", true, false, 0, 100, 100);

                map.AddTBString(TrackAttr.RDT, null, "����", true, false, 0, 30, 100);

                map.AddTBFloat(TrackAttr.WorkTimeSpan, 0, "ʱ����(��)", true, false);
                map.AddTBStringDoc(TrackAttr.Msg, null, "��Ϣ", true, false);
                map.AddTBStringDoc(TrackAttr.NodeData, null, "�ڵ�����(��־��Ϣ)", true, false);

                map.AddTBString(TrackAttr.Exer, null, "ִ����", true, false, 0, 30, 100);
                #endregion �ֶ�

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// �켣
        /// </summary>
        /// <param name="rptName"></param>
        public Track(string mypk)
        {
            this.MyPK = mypk;
            if (this.RetrieveFromDBSources() == 0)
            {
                TrackTemp t = new TrackTemp(this.MyPK);
                this.Row = t.Row;
            }
        }
        /// <summary>
        /// �켣
        /// </summary>
        public Track()
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
    /// �켣����
    /// </summary>
    public class Tracks : BP.En.Entities
    {
        /// <summary>
        /// �켣����
        /// </summary>
        public Tracks()
        {
        }
        public override Entity GetNewEntity
        {
            get
            {
                return new Track();
            }
        }
    }
}
