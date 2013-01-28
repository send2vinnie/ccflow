using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF.DTS
{
    /// <summary>
    /// �޸���������ֶγ��� ��ժҪ˵��
    /// </summary>
    public class SearchMaxWorkID : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public SearchMaxWorkID()
        {
            this.Title = "��ѯ����WorkID";
            this.Help = "����Ƿ�workid�ظ���";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                if (Web.WebUser.No == "admin")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {

            string msg="";

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            Int64 workid = 0;
            foreach (Flow fl in fls)
            {
                DataTable dt = DBAccess.RunSQLReturnTable("SELECT MAX(OID) FROM ND" + int.Parse(fl.No) + "Rpt");
                if (dt.Rows.Count == 0)
                    continue;
                try
                {
                    Int64 workidN = Int64.Parse(dt.Rows[0][0].ToString());
                    if (workidN > workid)
                        workid = workidN;
                }
                catch(Exception ex)
                {
                    continue;
                    //msg += "@" + ex.Message + " val=" + dt.Rows[0][0].ToString()+".";
                }
            }

            DataTable d1t = DBAccess.RunSQLReturnTable("SELECT IntVal FROM Sys_Serial where CfgKey='OID'");
            Int64 workidOld = int.Parse(d1t.Rows[0][0].ToString());
            return "ϵͳSys_Serial OID ��" + workidOld + " ,������ʹ�õ����OID�� " + workid +" <hr>"+msg;
        }
    }
}
