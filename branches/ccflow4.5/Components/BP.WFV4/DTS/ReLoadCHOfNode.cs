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
    public class ReLoadCHOfNode : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ReLoadCHOfNode()
        {
            this.Title = "��������ÿ���ڵ㹤���Ŀ������ݷ���WF_CHOfNode����";
            this.Help = "����������ϴ��п�����web������ִ��ʧ�ܡ�";
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
        /// 
        /// </summary>
        public void DoFlowData()
        {
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string msg = "";
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            try
            {
                DBAccess.RunSQL("DROP TABLE WF_CHOfNode");
            }
            catch
            {
            }

            CHOfNode ndData = new CHOfNode();
            ndData.CheckPhysicsTable();

            DataTable dt = null;
            string sql = "";
            foreach (Flow fl in fls)
            {
                Nodes nds = fl.HisNodes;
                foreach (Node nd in nds)
                {
                    try
                    {
                        sql = "SELECT OID,FID,RDT,CDT,Rec,Emps FROM ND" + nd.NodeID + " WHERE NodeState=1 AND OID NOT IN(SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + ")";
                        dt = DBAccess.RunSQLReturnTable(sql);
                    }
                    catch (Exception ex)
                    {
                        msg += ex.Message;
                        continue;
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        ndData.MyPK = nd.NodeID + "_" + dr["OID"].ToString();
                        ndData.FK_Node = nd.NodeID;
                        ndData.FK_Flow = nd.FK_Flow;
                     

                        ndData.WorkID = Int64.Parse(dr["OID"].ToString());
                        ndData.FK_Emp = dr["Rec"].ToString();
                        try
                        {
                            ndData.FK_Dept = DBAccess.RunSQLReturnString("SELECT FK_Dept FROM Port_Emp WHERE No='" + ndData.FK_Emp + "'");
                        }
                        catch (Exception ex)
                        {
                           // msg += ex.Message;
                        }

                        ndData.RDT = dr["RDT"].ToString();
                        ndData.CDT = dr["CDT"].ToString();
                        ndData.FK_NY = dr["RDT"].ToString().Substring(0, 7);

                        // ��Ӧ�������.
                        DateTime dtOfShould;
                        int day = 0;
                        if (nd.DeductDays < 1)
                            day = 0;
                        else
                            day = (int)nd.DeductDays;
                        dtOfShould = DataType.AddDays(DataType.ParseSysDate2DateTime(ndData.RDT), day);
                        ndData.SDT = dtOfShould.ToString("yyyy-MM-dd");

                        ndData.SpanDays = DataType.GetSpanDays(ndData.RDT, ndData.CDT); /*�������*/
                        ndData.NodeDeductDays = (int)nd.DeductDays; /*��Ҫ�������*/
                        ndData.NodeDeductCent = nd.DeductCent; /*ÿ����һ��۷�*/
                        ndData.NodeMaxDeductCent = (int)nd.MaxDeductCent; /*������۷�*/
                        ndData.NodeSwinkCent = (int)nd.SwinkCent; /*�����ӷ�*/
                        ndData.CentOfAdd = (int)nd.SwinkCent;
                        if (ndData.SpanDays <= ndData.NodeDeductDays)
                        {
                            ndData.CentOfCut = 0;
                        }
                        else
                        {
                            /*����۷�*/
                            float cent = (ndData.SpanDays - ndData.NodeDeductDays) * nd.DeductCent;
                            ndData.CentOfCut = cent;
                        }
                        ndData.Cent = ndData.CentOfAdd - ndData.CentOfCut;
                        ndData.Insert();
                    }
                }
            }
            return msg+"\t\n -ִ�����.....";
        }
    }
}
