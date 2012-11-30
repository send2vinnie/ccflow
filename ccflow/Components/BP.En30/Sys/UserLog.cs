using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// �û���־
	/// </summary>
    public class UserLogAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ��־���
        /// </summary>
        public const string LogFlag = "LogFlag";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Docs = "Docs";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        public const string IP = "IP";

    }
	/// <summary>
	/// �û���־
	/// </summary>
	public class UserLog: EntityOID
	{
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="empNo"></param>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        public static void AddLog(string logType, string empNo, string msg, string ip)
        {
            UserLog ul = new UserLog();
            ul.OID = (int)DBAccess.GenerOID("UL");
            ul.FK_Emp = empNo;
            ul.LogFlag = logType;
            ul.Docs = msg;
            ul.IP = ip;
            ul.RDT = DataType.CurrentDataTime;
            try
            {
                ul.InsertAsOID(ul.OID);
            }
            catch
            {
            }
        }
        public static void AddLog(string logType, string empNo, string msg)
        {
            UserLog ul = new UserLog();
            ul.OID = (int)DBAccess.GenerOID("UL");
            ul.FK_Emp = empNo;
            ul.LogFlag = logType;
            ul.Docs = msg;
            ul.RDT = DataType.CurrentDataTime;
            try
            {
                if (BP.SystemConfig.IsBSsystem)
                    ul.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                ul.InsertAsOID(ul.OID);
            }
            catch
            {
            }
        }

		#region �û���־��Ϣ��ֵ�б�
		#endregion

		#region ��������
        public string IP
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.IP);
            }
            set
            {
                this.SetValByKey(UserLogAttr.IP, value);
            }
        }
        /// <summary>
        /// ��־��Ǽ�
        /// </summary>
        public string LogFlag
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.LogFlag);
            }
            set
            {
                this.SetValByKey(UserLogAttr.LogFlag, value);
            }
        }
		/// <summary>
		/// FK_Emp
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(UserLogAttr.FK_Emp) ; 
			}
			set
			{
				this.SetValByKey(UserLogAttr.FK_Emp,value) ; 
			}
		}
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(UserLogAttr.RDT, value);
            }
        }
      
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(UserLogAttr.Docs, value);
            }
        }
      
		#endregion

		#region ���췽��
		/// <summary>
		/// �û���־
		/// </summary>
		public UserLog()
		{
		}
	 
		/// <summary>
		/// EnMap
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_UserLog");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "�û���־";
                map.EnType = EnType.Sys;
                map.AddTBIntPKOID();
                map.AddTBString(UserLogAttr.FK_Emp, null, "�û�", false, false, 1, 30, 20);
                map.AddTBString(UserLogAttr.IP, null, "IP", true, false, 0, 200, 20);
                map.AddTBString(UserLogAttr.LogFlag, null, "Flag", true, false, 0, 300, 20);
                map.AddTBString(UserLogAttr.Docs, null, "Docs", true, false, 0, 300, 20);
                map.AddTBString(UserLogAttr.RDT, null, "��¼����", true, false, 0, 20, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        #region ��д
        public override Entities GetNewEntities
        {
            get { return new UserLogs(); }
        }
        protected override bool beforeUpdateInsertAction()
        {
          //  this.MyPK = this.FK_Emp + this.CfgKey;
            return base.beforeUpdateInsertAction();
        }
        #endregion ��д
    }
	/// <summary>
	/// �û���־s
	/// </summary>
    public class UserLogs : EntitiesOID
    {
        #region ����
        public UserLogs()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emp"></param>
        public UserLogs(string emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(UserLogAttr.FK_Emp, emp);
            qo.DoQuery();
        }
        #endregion

        #region ��д
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new UserLog();
            }
        }
        #endregion

    }
}
