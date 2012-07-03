using System;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Web;
using BP.Sys;
namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵����
    /// ������.
    /// �����������������
    /// ��������Ϣ��
    /// ���̵���Ϣ��
    /// </summary>
    public class WorkNode
    {
        #region ToE
        public string ToE(string s, string chName)
        {
            return BP.Sys.Language.GetValByUserLang(s, chName);
        }
        public string ToEP1(string s, string chName, string v)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v);
        }
        public string ToEP2(string s, string chName, string v, string v1)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1);
        }
        public string ToEP3(string s, string chName, string v, string v1, string v2)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1, v2);
        }
        #endregion

        #region Ȩ���ж�
        /// <summary>
        /// �ж�һ�����ܲ��ܶ���������ڵ���в�����
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        private bool IsCanOpenCurrentWorkNode(string empId)
        {
            NodeState stat = this.HisWork.NodeState;
            if (stat == NodeState.Init)
            {
                if (this.HisNode.IsStartNode)
                {
                    /*����ǿ�ʼ�����ڵ㣬�ӹ�����λ�ж�����û�й�����Ȩ�ޡ�*/
                    return WorkFlow.IsCanDoWorkCheckByEmpStation(this.HisNode.NodeID, empId);
                }
                else
                {
                    /* ����ǳ�ʼ���׶�,�ж����ĳ�ʼ���ڵ� */
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Emp = empId;

                    Emp myEmp = new  Emp(empId);
                    wl.FK_EmpText = myEmp.Name;

                    wl.FK_Node = this.HisNode.NodeID;
                    wl.FK_NodeText = this.HisNode.Name;
                    return wl.IsExits;
                }
            }
            else
            {
                /* ����ǳ�ʼ���׶� */
                return false;
            }
        }
        #endregion

        //��ѯ��ÿ���ڵ����Ľ����˼��ϣ�Emps����
        public string GenerEmps(Node nd)
        {
            string str = "";
            foreach (WorkerList wl in this.HisWorkerLists)
                str = wl.FK_Emp + ",";
            return str;
        }
        private string _VirPath = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string VirPath
        {
            get
            {
                if (_VirPath == null && BP.SystemConfig.IsBSsystem)
                    _VirPath = System.Web.HttpContext.Current.Request.ApplicationPath ;
                return _VirPath;
            }
        }
        private string _AppType = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string AppType
        {
            get
            {
                if (_AppType == null)
                {
                    if (BP.Web.WebUser.IsWap)
                        _AppType = "/WF/WAP";
                    else
                        _AppType = "/WF";
                }
                return _AppType;
            }
        }
        private string nextStationName = "";
        /// <summary>
        /// ������һ���Ĺ�����
        /// </summary>
        /// <param name="town">WorkNode</param>
        /// <returns></returns>
        public WorkerLists InitWorkerLists_QingHai(WorkNode town)
        {
            DataTable dt = new DataTable();
            // dt = DBAccess.RunSQLReturnTable("SELECT NO,NAME FROM PUB_EMP WHERE 1=2 ");
            dt.Columns.Add("No", typeof(string));

            string sql;
            string fk_emp;

            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            // �жϵ�ǰ�ڵ��Ƿ�ɼ���Ŀ����Ա.(����������)
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                //sql="SELECT No,Name FROM PUB_EMP WHERE NO='"+this.HisWork.GetValStringByKey("FK_Emp")+"'";
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                //dt=DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }
            //����־�(��)��
            if (this.HisWork.EnMap.Attrs.Contains("FK_FJZ"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);

                //sql = "SELECT No,Name FROM PUB_EMP WHERE NO='" + this.HisWork.GetValStringByKey("FK_FJZ") + "'";
                //dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            //�ӹ켣�����ѯ.
            sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
               + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
               + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + ")";
            dt = DBAccess.RunSQLReturnTable(sql);

            // ����ܹ��ҵ�.
            if (dt.Rows.Count >= 1)
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ��������¡�
            Stations nextStations = town.HisNode.HisStations;
            // �Ҳ���, ��Ҫ�ж��������⡣
            Station fromSt = this.HisStationOfUse;
            string DeptofUse = this.HisDeptOfUse.No;
            if (nextStations.Count == 0)
                throw new Exception(this.ToEP1("WF2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));  //����Աά��������û�и��ڵ�["+town.HisNode.Name+"]���ù�����λ��

            Station toSt = (Station)nextStations[0];
            if (fromSt.No == toSt.No)
            {
#warning edit 2008 - 05-28 .
                // sql = "SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
                sql = "SELECT No,Name FROM Port_Emp WHERE NO='" + WebUser.No + "'";  // IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                    DataRow dr = dt.NewRow();
                    dr[0] = fk_emp;
                    dt.Rows.Add(dr);
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }

            if (fromSt.Grade < toSt.Grade)
            {
            }
            else if (fromSt.Grade == toSt.Grade)
            {
            }
            else
            {
            }
            return WorkerListWayOfDept(town, dt);
        }
          
        private WorkNode town = null;
       public WorkerLists GenerWorkerLists_WidthFID(WorkNode town)
       {
            this.town = town;
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.FID + " AND FK_Node=" + town.HisNode.NodeID);

            #region �����ж��Ƿ������˻�ȡ��һ��������Ա��sql.
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySQL)
            {
                if (town.HisNode.RecipientSQL.Length < 4)
                    throw new Exception("@�����õĵ�ǰ�ڵ㰴��sql��������һ���Ľ�����Ա��������û������sql.");

                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = this.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }
                sql = sql.Replace("~", "'");
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Exception("@û���ҵ��ɽ��ܵĹ�����Ա��@������Ϣ��ִ�е�sqlû�з�����Ա:" + sql);

                return WorkerListWayOfDept(town, dt);
            }
            #endregion

            // ����һ�ڵ㷢���˴���
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOper
                || town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
            {
                DataRow dr = dt.NewRow();
                dr[0] = Web.WebUser.No;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // ����ѡ�����Ա����
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                sql = "SELECT FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // ���սڵ�ָ������Ա����
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpcEmp)
            {
                dt = DBAccess.RunSQLReturnTable("SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + town.HisNode.NodeID);
                if (dt.Rows.Count == 0)
                    throw new Exception("@�����õĵ�ǰ�ڵ㰴�սڵ����Ա��������û��Ϊ(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")�ڵ����Ա��");
                return WorkerListWayOfDept(town, dt);
            }

            // ���ձ��ֶ���Ա����
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByEmp)
            {
                if (this.HisWork.EnMap.Attrs.Contains("FK_Emp") == false)
                    throw new Exception("@�����õĵ�ǰ�ڵ㰴��ָ���ڵ���ֶ���Ա��������һ���Ľ�����Ա��������û���ڽڵ�(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")�������øñ�FK_Emp�ֶΡ�");

                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            //// �ж� �ڵ���Ա���Ƿ������ã�  ����оͲ����Ǹ�λ�����ˡ��ӽڵ���Ա���������ѯ��
            //if (town.HisNode.HisEmps.Length > 2)
            //{
            //    string[] emps = town.HisNode.HisEmps.Split('@');
            //    foreach (string emp in emps)
            //    {
            //        if (emp == null || emp == "")
            //            continue;
            //        DataRow dr = dt.NewRow();
            //        dr[0] = emp;
            //        dt.Rows.Add(dr);
            //    }
            //    return WorkerListWayOfDept(town, dt);
            //}

            // �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDept)
            {
                sql = "SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count > 0)
                    return WorkerListWayOfDept(town, dt);
                else
                    throw new Exception("������ƴ������֯�ṹά��������:û���ҵ������ż�����ʵĽڵ���Ա��");
            }

            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDeptAndStation)
            {
                /* ����������˸�λ�ļ��ϵĻ����Ͱ������Ľ������㡣 */
                sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                sql += ")";
                sql += "AND NO IN ";
                sql += "(";
                sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                sql += ")";
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count > 0)
                    return WorkerListWayOfDept(town, dt);
                else
                    throw new Exception("������ƴ������֯�ṹά��������:û���ҵ������������λ�Ľ������㡱�Ľڵ���Ա��");
            }

            string empNo = WebUser.No;
            string empDept = WebUser.FK_Dept;
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeStation)
            {
                /* ��ָ���ڵ��λ�ϵ���Ա���� */
                string fk_node = town.HisNode.RecipientSQL;
                if (DataType.IsNumStr(fk_node) == false)
                    throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ��λͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");

                DataTable dtR = DBAccess.RunSQLReturnTable("SELECT fk_dept,Rec FROM  ND" + fk_node + " where OID=" + this.WorkID);
                if (dtR.Rows.Count == 0)
                {
                    empDept = dtR.Rows[0][0] as string;
                    empNo = dtR.Rows[0][1] as string;
                }
            }
          

            #region  ���һ���ǰ��ո�λ��ִ�С�
            if (this.HisNode.IsStartNode == false)
            {
                // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.FID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // ����ܹ��ҵ�.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /* �����Աֻ��һ���������˵��������Ҫ */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            /* ���ִ�нڵ� �� ���ܽڵ��λ����һ�� */
            if (this.HisNode.GroupStaNDs == town.HisNode.GroupStaNDs)
            {
                /* ˵�����Ͱѵ�ǰ��Ա��Ϊ��һ���ڵ㴦���ˡ�*/
                DataRow dr = dt.NewRow();
                dr[0] = empNo;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }


            /* ���ִ�нڵ� �� ���ܽڵ��λ���ϲ�һ�� */
            if (this.HisNode.GroupStaNDs != town.HisNode.GroupStaNDs)
            {
                /* û�в�ѯ���������, �Ȱ��ձ����ż��㡣*/
                sql = "SELECT No FROM Port_Emp WHERE NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + empDept + "')";

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                }
                else
                {
                    bool isInit = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0].ToString() == empNo)
                        {
                            /* �����λ���鲻һ�������ҽ�������ﻹ�е�ǰ����Ա����˵���˳����˵�ǰ����Ա��ӵ�б��ڵ��ϵĸ�λҲӵ����һ���ڵ�Ĺ�����λ
                             ���£��ڵ�ķ��鲻ͬ�����ݵ�ͬһ�������ϡ� */
                            isInit = true;
                        }
                    }
                    if (isInit == false)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            // û�в�ѯ���������, ִ�в�ѯ���������ŵ��¼�������Ա��
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept + "%')"
               + " AND No!='" + empNo + "'";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ���������, �������ƥ���� ���һ������ ���㣬�ݹ��㷨δ��ɣ����������Ѿ�����󲿷���Ҫ��
            int lengthStep = 0; //�������衣
            while (true)
            {
                lengthStep += 2;
                int toLen = empDept.Length - lengthStep;
                if (toLen <= 0)
                    throw new Exception("@�Ѿ��оٵ���߲��ţ���Ȼû���ҵ�������Ա��");

                sql = "SELECT NO FROM Port_Emp WHERE No IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - lengthStep) + "%')"
                   + " AND No!='" + empNo+ "'";


                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (empDept.Length == 2)
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + empNo + "') ";
                    else
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + empNo + "' AND FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No!='" + empNo + "' AND No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "��λ(" + msg + ")��û����Ա����Ӧ�ڵ�:") + town.HisNode.Name);
                            //"ά����������[" + town.HisNode.Name + "]ά���ĸ�λ���Ƿ�����Ա��"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }
            #endregion  ���ո�λ��ִ�С�
        }
       public WorkerLists GenerWorkerLists(WorkNode town)
       {
           this.town = town;

           DataTable dt = new DataTable();
           dt.Columns.Add("No", typeof(string));
           string sql;
           string fk_emp;

           // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
           DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

           // ���ָ���ض�����Ա����
           if (JumpToEmp != null)
           {
               DataRow dr = dt.NewRow();
               dr[0] = JumpToEmp;
               dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }

           // ����һ�ڵ㷢���˴���
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOper
               || town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
           {
               DataRow dr = dt.NewRow();
               dr[0] = Web.WebUser.No;
               dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }
           //�����ж��Ƿ������˻�ȡ��һ��������Ա��sql.
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySQL)
           {
               if (town.HisNode.RecipientSQL.Length < 4)
                   throw new Exception("@�����õĵ�ǰ�ڵ㰴��sql��������һ���Ľ�����Ա��������û������sql.");

               Attrs attrs = this.HisWork.EnMap.Attrs;
               sql = town.HisNode.RecipientSQL;
               foreach (Attr attr in attrs)
               {
                   if (attr.MyDataType == DataType.AppString)
                       sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                   else
                       sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
               }

               sql = sql.Replace("~", "'");
               sql = sql.Replace("@WebUser.No", "'" + WebUser.No + "'");
               sql = sql.Replace("@WebUser.Name", "'" + WebUser.Name + "'");
               sql = sql.Replace("@WebUser.FK_Dept", "'" + WebUser.FK_Dept + "'");

               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count == 0)
                   throw new Exception("@û���ҵ��ɽ��ܵĹ�����Ա��@������Ϣ��ִ�е�sqlû�з�����Ա:" + sql);
               return WorkerListWayOfDept(town, dt);
           }

           // ����ѡ�����Ա����
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
           {
               sql = "SELECT FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count == 0)
                   throw new Exception("��ѡ����һ���蹤��(" + town.HisNode.Name + ")������Ա��");
               return WorkerListWayOfDept(town, dt);
           }

           // ���սڵ�ָ������Ա����
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpcEmp)
           {
               dt = DBAccess.RunSQLReturnTable("SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + town.HisNode.NodeID);
               if (dt.Rows.Count == 0)
                   throw new Exception("@�����õĵ�ǰ�ڵ㰴�սڵ����Ա��������û��Ϊ(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")�ڵ����Ա��");

               //fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
               //DataRow dr = dt.NewRow();
               //dr[0] = fk_emp;
               //dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }

           // ���սڵ�ָ������Ա����
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByEmp)
           {
               if (this.HisWork.EnMap.Attrs.Contains("FK_Emp") == false)
                   throw new Exception("@�����õĵ�ǰ�ڵ㰴��ָ������Ա��������һ���Ľ�����Ա��������û���ڽڵ�������øñ�FK_Emp�ֶΡ�");

               fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
               DataRow dr = dt.NewRow();
               dr[0] = fk_emp;
               dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }



           string prjNo = "";
           FlowAppType flowAppType = this.HisNode.HisFlow.HisFlowAppType;
           sql = "";
           if (this.HisNode.HisFlow.HisFlowAppType == FlowAppType.PRJ)
           {
               prjNo = "";
               try
               {
                   prjNo = this.HisWork.GetValStrByKey("PrjNo");
               }
               catch (Exception ex)
               {
                   throw new Exception("@��ǰ�����ǹ��������̣������ڽڵ����û��PrjNo�ֶ�(ע�����ִ�Сд)����ȷ�ϡ�@�쳣��Ϣ:" + ex.Message);
               }
           }
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDeptAndStation)
           {
               sql = "SELECT No FROM Port_Emp WHERE No IN ";
               sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
               sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
               sql += ")";
               sql += "AND No IN ";
               sql += "(";
               sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
               sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
               sql += ")";
               dt = DBAccess.RunSQLReturnTable(sql);

               if (dt.Rows.Count > 0)
                   return WorkerListWayOfDept(town, dt);
               else
                   throw new Exception("@�ڵ���ʹ������:�ڵ�(" + town.HisNode.NodeID + "," + town.HisNode.Name + "), ���ո�λ�벿�ŵĽ���ȷ�������˵ķ�Χ����û���ҵ���Ա:SQL=" + sql);
           }

           #region �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDept)
           {
               if (flowAppType == FlowAppType.Normal)
               {
                   sql = "SELECT DISTINCT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count > 0)
                       return WorkerListWayOfDept(town, dt);
               }

               if (flowAppType == FlowAppType.PRJ)
               {
                   sql = "SELECT No FROM Port_Emp WHERE No IN ";
                   sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                   sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                   sql += ")";
                   sql += "AND NO IN ";
                   sql += "(";
                   sql += "SELECT FK_Emp FROM Prj_EmpPrjStation WHERE FK_Station IN ";
                   sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") AND FK_Prj='" + prjNo + "'";
                   sql += ")";
                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                       sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                       sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                       sql += ")";
                       sql += "AND NO IN ";
                       sql += "(";
                       sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                       sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                       sql += ")";
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }


                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count > 0)
                       return WorkerListWayOfDept(town, dt);
               }
           }
           #endregion �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���

           #region ����ָ���ڵ�ĸ�λ��ִ�С�
           string empNo = WebUser.No;
           string empDept = WebUser.FK_Dept;
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeStation)
           {
               /* ��ָ���ڵ��λ�ϵ���Ա���� */
               string fk_node = town.HisNode.RecipientSQL;
               if (DataType.IsNumStr(fk_node) == false)
                   throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ��λͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");

               DataTable dtR = DBAccess.RunSQLReturnTable("SELECT fk_dept,Rec FROM  ND" + fk_node + " where OID=" + this.WorkID);
               if (dtR.Rows.Count == 0)
               {
                   empDept = dtR.Rows[0][0] as string;
                   empNo = dtR.Rows[0][1] as string;
               }
           }
           #endregion ����ָ���ڵ�ĸ�λ��ִ�С�

           #region ���ڵ��λ����Ա���ż�������γ�ȼ���.
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByStationAndEmpDept)
           {
               sql = "SELECT No FROM Port_Emp WHERE NO IN "
                     + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                     + " AND  NO IN "
                     + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Emp = '" + WebUser.No + "')";
               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count > 0)
                   return WorkerListWayOfDept(town, dt);
               else
                   throw new Exception("@�ڵ���ʹ������:�ڵ�(" + town.HisNode.NodeID + "," + town.HisNode.Name + "), ���ڵ��λ����Ա���ż�������γ�ȼ��㣬û���ҵ���Ա:SQL=" + sql);
           }
           #endregion 



           if (town.HisNode.HisDeliveryWay != DeliveryWay.ByStation)
               throw new Exception("@û���жϵ�ִ�й���:" + town.HisNode.HisDeliveryWay);

           #region ����ж� - ���ո�λ��ִ�С�
           if (this.HisNode.IsStartNode == false)
           {
               if (flowAppType == FlowAppType.Normal)
               {
                   // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                   sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                      + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                      + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
               }

               if (flowAppType == FlowAppType.PRJ)
               {
                   // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                   sql = "SELECT DISTINCT FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Station IN "
                      + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") AND FK_Prj='" + prjNo + "' "
                      + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                       sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                        + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                        + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }
               }

               dt = DBAccess.RunSQLReturnTable(sql);
               // ����ܹ��ҵ�.
               if (dt.Rows.Count >= 1)
               {
                   if (dt.Rows.Count == 1)
                   {
                       /*�����Աֻ��һ���������˵��������Ҫ */
                   }
                   return WorkerListWayOfDept(town, dt);
               }
           }

           /* ���ִ�нڵ� �� ���ܽڵ��λ����һ�� */
           if (this.HisNode.GroupStaNDs == town.HisNode.GroupStaNDs)
           {
               /* ˵�����Ͱѵ�ǰ��Ա��Ϊ��һ���ڵ㴦���ˡ�*/
               DataRow dr = dt.NewRow();
               dr[0] = WebUser.No;
               dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }

           /* ���ִ�нڵ� �� ���ܽڵ��λ���ϲ�һ�� */
           if (this.HisNode.GroupStaNDs != town.HisNode.GroupStaNDs)
           {
               /* û�в�ѯ���������, �Ȱ��ձ����ż��㡣*/
               if (flowAppType == FlowAppType.Normal)
               {
                   sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                      + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                      + " AND  NO IN "
                      + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + empDept + "')";
               }

               if (flowAppType == FlowAppType.PRJ)
               {
                   sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj='" + prjNo + "' AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")"
                   + " AND  FK_Prj='" + prjNo + "' ";

                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ� */
                       sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                     + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                     + " AND  NO IN "
                     + "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept = '" + empDept + "')";
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }
               }

               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count == 0)
               {
                   Stations nextStations = town.HisNode.HisStations;
                   if (nextStations.Count == 0)
                       throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
               }
               else
               {
                   bool isInit = false;
                   foreach (DataRow dr in dt.Rows)
                   {
                       if (dr[0].ToString() == Web.WebUser.No)
                       {
                           /* �����λ���鲻һ�������ҽ�������ﻹ�е�ǰ����Ա����˵���˳����˵�ǰ����Ա��ӵ�б��ڵ��ϵĸ�λҲӵ����һ���ڵ�Ĺ�����λ
                            ���£��ڵ�ķ��鲻ͬ�����ݵ�ͬһ�������ϡ� */
                           isInit = true;
                       }
                   }
#warning edit by peng, ����ȷ����ͬ��λ���ϵĴ��ݰ���ͬһ���˵Ĵ���ʽ��
                   if (isInit == false || isInit == true)
                       return WorkerListWayOfDept(town, dt);
               }
           }

           // û�в�ѯ���������, ִ�в�ѯ���������ŵ��¼�������Ա��
           if (flowAppType == FlowAppType.Normal)
           {
               sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                  + " AND  NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept + "%')"
                  + " AND No!='" + empNo + "'";
           }

           if (flowAppType == FlowAppType.PRJ)
           {
               sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj='" + prjNo + "' AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")"
                   + " AND  FK_Prj='" + prjNo + "' ";

               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count == 0)
               {
                   /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                   sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                  + " AND  NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept + "%')"
                  + " AND No!='" + empNo + "'";
               }
               else
               {
                   return WorkerListWayOfDept(town, dt);
               }
           }

           dt = DBAccess.RunSQLReturnTable(sql);
           if (dt.Rows.Count == 0)
           {
               Stations nextStations = town.HisNode.HisStations;
               if (nextStations.Count == 0)
                   throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
           }
           else
           {
               return WorkerListWayOfDept(town, dt);
           }


           /* û�в�ѯ���������, �������ƥ���� ���һ������ ���㣬�ݹ��㷨δ��ɣ����������Ѿ�����󲿷���Ҫ��
            * 
            * ��Ϊ:�����Ѿ����ĸ�λ���жϣ���û�б�Ҫ���ж��������͵����̴����ˡ�
            * 
            * */

           int lengthStep = 0; //�������衣
           while (true)
           {
               lengthStep += 2;
               sql = "SELECT NO FROM Port_Emp WHERE No IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                  + " AND  NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - lengthStep) + "%')"
                  + " AND No!='" + empNo + "'";

               dt = DBAccess.RunSQLReturnTable(sql);
               if (dt.Rows.Count == 0)
               {
                   Stations nextStations = town.HisNode.HisStations;
                   if (nextStations.Count == 0)
                       throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                   sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                   sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                   sql += " AND  No IN ";
                   if (empDept.Length == 2)
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE   FK_Emp!='" + empNo + "') ";
                   else
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + empNo + "' AND FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - 4) + "%')";

                   dt = DBAccess.RunSQLReturnTable(sql);
                   if (dt.Rows.Count == 0)
                   {
                       sql = "SELECT No FROM Port_Emp WHERE No!='" + empNo + "' AND No IN ";
                       sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                       dt = DBAccess.RunSQLReturnTable(sql);
                       if (dt.Rows.Count == 0)
                       {
                           string msg = town.HisNode.HisStationsStr;
                           throw new Exception(this.ToE("WF3", "��λ(" + msg + ")��û����Ա����Ӧ�ڵ�:") + town.HisNode.Name);
                           //"ά����������[" + town.HisNode.Name + "]ά���ĸ�λ���Ƿ�����Ա��"
                       }
                   }
                   return WorkerListWayOfDept(town, dt);
               }
               else
               {
                   return WorkerListWayOfDept(town, dt);
               }
           }
           #endregion  ���ո�λ��ִ�С�
       }
        public WorkerLists GenerWorkerListsV2_del(WorkNode town)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;
            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            //�����ж��Ƿ������˻�ȡ��һ��������Ա��sql.
            if (this.HisNode.RecipientSQL.Length > 4)
            {
                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = this.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    if (SystemConfig.IsDebug)
                        throw new Exception("@û���ҵ��ɽ��ܵĹ�����Ա��@������Ϣ��ִ�е�sqlû�з�����Ա:" + sql);
                    else
                        throw new Exception("@û���ҵ��ɽ��ܵĹ�����Ա��");
                }
                return WorkerListWayOfDept(town, dt);
            }

            if (this.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                sql = "SELECT  FK_Emp  FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // �жϵ�ǰ�ڵ��Ƿ�ɼ���Ŀ����Ա
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // �ж� �ڵ���Ա���Ƿ������ã�  ����оͲ����Ǹ�λ�����ˡ��ӽڵ���Ա���������ѯ��
            if (town.HisNode.HisEmps.Length > 2)
            {
                string[] emps = town.HisNode.HisEmps.Split('@');
                foreach (string emp in emps)
                {
                    if (emp == null || emp == "")
                        continue;
                    DataRow dr = dt.NewRow();
                    dr[0] = emp;
                    dt.Rows.Add(dr);
                }
                return WorkerListWayOfDept(town, dt);
            }

            // �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDept)
            {
                if (town.HisNode.HisStations.Count == 0)
                {
                    sql = "SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                    sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    sql += "AND NO IN ";
                    sql += "(";
                    sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                    sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            if (this.HisNode.IsStartNode == false)
            {
                // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // ����ܹ��ҵ�.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*�����Աֻ��һ���������˵��������Ҫ */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            // û�в�ѯ���������, �Ȱ��ձ����ż��㡣
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                // throw new Exception(this.ToEP1("WN2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ���������, �������ƥ�������㡣
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                //  throw new Exception(this.ToEP1("WN2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ���������, �������ƥ���� ���һ������ ���㡣
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                //throw new Exception(this.ToEP1("WF2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
                else
                {
                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (WebUser.FK_Dept.Length == 2)
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept )";
                    else
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "��λ(" + msg + ")��û����Ա����Ӧ�ڵ�:") + town.HisNode.Name);
                            //"ά����������[" + town.HisNode.Name + "]ά���ĸ�λ���Ƿ�����Ա��"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }
        }
        public bool IsSubFlowWorkNode
        {
            get
            {
                if (this.HisWork.FID == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// ����һ��word
        /// </summary>
        public void DoPrint()
        {
            string tempFile = SystemConfig.PathOfTemp + "\\" + this.WorkID + ".doc";
            Work wk = this.HisNode.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();
            Glo.GenerWord(tempFile, wk);
            PubClass.OpenWordDocV2(tempFile, this.HisNode.Name + ".doc");
        }
        public WorkNode DoReturnWork(int backtoNodeID, string msg)
        {
            return DoReturnWork(backtoNodeID, msg, false);
        }
        private WorkNode DoReturnSubFlow(int backtoNodeID, string msg, bool isHiden)
        {
            Node nd = new Node(backtoNodeID);

            // ɾ�����ܴ��ڵ�����.
            string sql = "DELETE  FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.OID + "  AND FID=" + this.HisWork.FID;
            BP.DA.DBAccess.RunSQL(sql);

            // �ҳ��ֺ����㴦�����Ա.
            sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.FID;
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count != 1)
                throw new Exception("@ system error , this values must be =1");

            string fk_emp = dt.Rows[0][0].ToString();

            // ��ȡ��ǰ��������Ϣ.
            WorkerList wl = new WorkerList(this.HisWork.FID, this.HisNode.NodeID, fk_emp);
            Emp emp = new Emp(fk_emp);

            // �ı䲿������������Ӧ�µ�����,����ʾһ���µĴ��칤�����û�������
            wl.IsPass = false;
            wl.WorkID = this.HisWork.OID;
            wl.FID = this.HisWork.FID;
            wl.RDT = DataType.CurrentDataTime;
            wl.FK_Emp = fk_emp;
            wl.FK_EmpText = emp.Name;
             
            wl.FK_Node = backtoNodeID;
            wl.FK_NodeText = nd.Name;
            wl.WarningDays = nd.WarningDays;
            wl.FK_Dept = emp.FK_Dept;

            DateTime dtNew = DateTime.Now;
            dtNew = dtNew.AddDays(nd.WarningDays);
            wl.SDT = dtNew.ToString(DataType.SysDataFormat); // DataType.CurrentDataTime;
            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.Insert();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = backtoNodeID;
            gwf.NodeName = nd.Name;
            gwf.DirectUpdate();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

            /* ����������˻ء�*/
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wl.WorkID;
            rw.ReturnToNode = wl.FK_Node;
            rw.ReturnNode = this.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;
            rw.ReturnToEmp = fk_emp;
            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            // ����track.
            this.AddToTrack(ActionType.Return, fk_emp, emp.Name, backtoNodeID, nd.Name, msg);

            WorkNode wn = new WorkNode(this.HisWork.FID, backtoNodeID);
            if (Glo.IsEnableSysMessage)
            {
                WF.Port.WFEmp wfemp = new Port.WFEmp(wn.HisWork.Rec);
                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wfemp.No, wfemp.No,
                    wfemp.HisAlertWay, wfemp.Tel,
                      this.ToEP3("WN27", "�����˻أ�����:{0}.����:{1},�˻���:{2},��������",
                      wn.HisNode.FlowName, wn.HisNode.Name, WebUser.Name),
                      wfemp.Email, null, msg);
            }
            return wn;

            throw new Exception("�������߳��˻ص���������߷ֺ����㹦�ܣ�û��ʵ�֡�");
        }
        /// <summary>
        /// �����˻�
        /// </summary>
        /// <param name="backtoNodeID">�˻ص��ڵ�</param>
        /// <param name="msg">�˻���Ϣ</param>
        /// <param name="IsBackTracking">�˻غ��Ƿ�ԭ·���أ�</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(int backtoNodeID, string msg, bool isBackTracking)
        {
            //�˻�ǰ�¼�
           this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.ReturnBefore, this.HisWork);
           if (this.HisNode.FocusField != "")
           {
               // �����ݸ�������
               this.HisWork.Update(this.HisNode.FocusField, "");
           }

            Node backToNode = new Node(backtoNodeID);
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL: /*�����ǰ�Ǻ����� */
                    /* ɱ������*/
                    WorkerLists wlsSubs = new WorkerLists();
                    wlsSubs.Retrieve(WorkerListAttr.FID, this.HisWork.OID);
                    foreach (WorkerList sub in wlsSubs)
                    {
                        GenerWorkFlow gw1f = new GenerWorkFlow();
                        gw1f.WorkID = sub.WorkID;
                        gw1f.DirectDelete();

                        Node subNode = new Node(sub.FK_Node);
                        Work wk = subNode.GetWork(sub.WorkID);
                        wk.Delete();
                        sub.Delete();
                    }
                    GenerFH fh = new GenerFH();
                    fh.FID = this.WorkID;
                    fh.Delete();
                    break;
                default:
                    switch (backToNode.HisNodeWorkType)
                    {
                        case NodeWorkType.StartWorkFL:
                        case NodeWorkType.WorkFL:
                        case NodeWorkType.WorkFHL:
                            if (this.IsSubFlowWorkNode)
                            {
                                /* �����֧����������������߷ֺ����ڵ����˻�. */
                                return DoReturnSubFlow(backtoNodeID, msg, isBackTracking);
                            }
                            // return DoReturnSubFlow(backtoNodeID, msg, isHiden);
                            break;
                        default:
                            break;
                    }
                    break;
            }

            // �ı䵱ǰ�Ĺ����ڵ㣮
            WorkNode wnOfBackTo = new WorkNode(this.WorkID, backtoNodeID);
            wnOfBackTo.HisWork.NodeState = NodeState.Back; // ���� return work ״̬��
            wnOfBackTo.HisWork.DirectUpdate();

            // �ı䵱ǰ���칤���ڵ㡣
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node='" + backtoNodeID + "',NodeName='" + backToNode.Name+ "' WHERE  WorkID=" + this.WorkID);
            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);

            // ��¼�˻ع켣��
            ReturnWork rw = new ReturnWork();
            rw.WorkID = wnOfBackTo.HisWork.OID;
            rw.ReturnToNode = wnOfBackTo.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;

            rw.ReturnNode = this.HisNode.NodeID; // ��ǰ�˻ؽڵ�.
            rw.ReturnToEmp = wnOfBackTo.HisWork.Rec; //�˻ظ���

            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Note = msg;
            rw.IsBackTracking = isBackTracking;
            rw.Insert();

            // ����track.
            this.AddToTrack(ActionType.Return, wnOfBackTo.HisWork.Rec, wnOfBackTo.HisWork.RecText, 
                backtoNodeID, wnOfBackTo.HisNode.Name, msg);

            try
            {
                // ��¼�˻���־.
                ReorderLog(backToNode, this.HisNode, rw);
            }
            catch(Exception ex)
            {
                Log.DebugWriteWarning(ex.Message);
            }

            // ���˻ص��Ľڵ���ǰ�����õݹ�ɾ������
            if (isBackTracking == false)
            {
                /*����˻ز���Ҫԭ·���أ���ɾ���м������ݡ�*/
                DeleteToNodesData(backToNode.HisToNodes);
            }

            //ɾ���������������ݡ�
            DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID NOT IN (SELECT WorkID FROM WF_GenerWorkerList )");
            DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID NOT IN (SELECT FID FROM WF_GenerWorkerList)");

            //����������Ϣ��
            WorkNode backWN = new WorkNode(this.WorkID, backtoNodeID);

            if (Glo.IsEnableSysMessage ==true)
            {
                WF.Port.WFEmp wfemp = new Port.WFEmp(wnOfBackTo.HisWork.Rec);
                BP.TA.SMS.AddMsg(rw.MyPK, wfemp.No,
                    wfemp.HisAlertWay, wfemp.Tel,
                      this.ToEP3("WN27", "�����˻أ�����:{0}.����:{1},�˻���:{2},��������",
                      backToNode.FlowName, backToNode.Name, WebUser.Name),
                      wfemp.Email, null, msg);
            }

            //�˻غ��¼�
            this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.ReturnAfter, this.HisWork);
            return wnOfBackTo;
        }
        private string infoLog = "";
        public void ReorderLog(Node fromND, Node toND, ReturnWork rw)
        {
            string filePath = BP.SystemConfig.PathOfDataUser + "\\ReturnLog\\" + this.HisNode.FK_Flow + "\\";
            if (System.IO.Directory.Exists(filePath) == false)
                System.IO.Directory.CreateDirectory(filePath);

            string file = filePath + "\\" + rw.MyPK;
            infoLog = "\r\n�˻���:" + WebUser.No+","+WebUser.Name + " \r\n�˻ؽڵ�:" + fromND.Name + " \r\n�˻ص�:" + toND.Name;
            infoLog += "\r\n�˻�ʱ��:" + DataType.CurrentDataTime;
            infoLog += "\r\nԭ��:" + rw.Note;

            ReorderLog(fromND, toND);
            DataType.WriteFile(file+".txt", infoLog);
            DataType.WriteFile(file + ".htm", infoLog.Replace("\r\n","<br>"));

            this.HisWork.Delete();
        }
        public void ReorderLog(Node fromND, Node toND)
        {
            /*��ʼ��������Ľڵ㼯��*/
            foreach (Node nd in fromND.HisToNodes)
            {
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                if (wk.RetrieveFromDBSources() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.Retrieve(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                if (nd.IsFL)
                {
                    /* ����Ƿ��� */
                    WorkerLists wls = new WorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(WorkerListAttr.FID, this.WorkID);
                    qo.addAnd();

                    string[] ndsStrs = nd.HisToNDs.Split('@');
                    string inStr = "";
                    foreach (string s in ndsStrs)
                    {
                        if (s == "" || s == null)
                            continue;
                        inStr += "'" + s + "',";
                    }
                    inStr = inStr.Substring(0, inStr.Length - 1);
                    if (inStr.Contains(",") == true)
                        qo.AddWhere(WorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(WorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (WorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);

                        infoLog += "\r\n*****************************************************************************************";
                        infoLog += "\r\n�ڵ�ID:" + subNd.NodeID + "  ��������:" + subWK.EnDesc;
                        infoLog += "\r\n������:" + subWK.Rec + " , " + wk.RecOfEmp.Name;
                        infoLog += "\r\n����ʱ��:" + subWK.RDT + " ����ʱ��:" + subWK.CDT;
                        infoLog += "\r\n ------------------------------------------------- ";


                        foreach (Attr attr in wk.EnMap.Attrs)
                        {
                            if (attr.UIVisible == false)
                                continue;
                            infoLog += "\r\n " + attr.Desc + ":" + subWK.GetValStrByKey(attr.Key);
                        }

                        //�ݹ���á�
                        ReorderLog(subNd, toND);
                    }
                }
                else
                {
                    infoLog += "\r\n*****************************************************************************************";
                    infoLog += "\r\n�ڵ�ID:" + wk.NodeID + "  ��������:" + wk.EnDesc;
                    infoLog += "\r\n������:" + wk.Rec +" , "+wk.RecOfEmp.Name;
                    infoLog += "\r\n����ʱ��:" + wk.RDT + " ����ʱ��:" + wk.CDT;
                    infoLog += "\r\n ------------------------------------------------- ";

                    foreach (Attr attr in wk.EnMap.Attrs)
                    {
                        if (attr.UIVisible == false)
                            continue;
                        infoLog += "\r\n" + attr.Desc + " : " + wk.GetValStrByKey(attr.Key);
                    }
                }

                /* ������˵�ǰ�Ľڵ� */
                if (nd.NodeID == toND.NodeID)
                    break;

                //�ݹ���á�
                ReorderLog(nd, toND);
            }
        }
        /// <summary>
        /// �ݹ�ɾ�������ڵ�֮�������
        /// </summary>
        /// <param name="nds">����Ľڵ㼯��</param>
        public void DeleteToNodesData(Nodes nds)
        {
            /*��ʼ��������Ľڵ㼯��*/
            foreach (Node nd in nds)
            {
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                if (wk.Delete() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.Delete(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                #region ɾ����ǰ�ڵ����ݣ�ɾ��������Ϣ��
                // ɾ����ϸ����Ϣ��
                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                foreach (MapDtl dtl in dtls)
                {
                    BP.DA.DBAccess.RunSQL("DELETE " + dtl.PTable + " WHERE RefPK='" + this.WorkID + "'");
                }

                // ɾ����������Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE RefPKVal='" + this.WorkID + "' AND FK_MapData='ND" + nd.NodeID + "'");
                // ɾ��ǩ����Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmEleDB WHERE RefPKVal='" + this.WorkID + "' AND FK_MapData='ND" + nd.NodeID + "'");
                #endregion ɾ����ǰ�ڵ����ݡ�


                /*˵��:�Ѿ�ɾ���ýڵ����ݡ�*/
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ") AND FK_Node=" + nd.NodeID);
                if (nd.IsFL)
                {
                    /* ����Ƿ��� */
                    WorkerLists wls = new WorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(WorkerListAttr.FID, this.WorkID);
                    qo.addAnd();

                    string[] ndsStrs = nd.HisToNDs.Split('@');
                    string inStr = "";
                    foreach (string s in ndsStrs)
                    {
                        if (s == "" || s == null)
                            continue;
                        inStr += "'" + s + "',";
                    }
                    inStr = inStr.Substring(0, inStr.Length - 1);
                    if (inStr.Contains(",") == true)
                        qo.AddWhere(WorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(WorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (WorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);
                        subWK.Delete();

                        //ɾ�������²���Ľڵ���Ϣ.
                        DeleteToNodesData(subNd.HisToNodes);
                    }

                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.WorkID);
                }
                DeleteToNodesData(nd.HisToNodes);
            }
        }
        /// <summary>
        /// ִ���˻�
        /// </summary>
        /// <param name="msg">�˻ع�����ԭ��</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(string msg)
        {
            // �ı䵱ǰ�Ĺ����ڵ㣮
            WorkNode wn = this.GetPreviousWorkNode();
            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.NodeName = wn.HisNode.Name;
            gwf.DirectUpdate();

            // ���� return work ״̬��
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // ɾ��������.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // д����־.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.ReturnToNode = wn.HisNode.NodeID;
            rw.ReturnToEmp = wn.HisWork.Rec;
            rw.ReturnNode = this.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;

            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // ��Ϣ֪ͨ��һ��������Ա����
            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "�˻صĹ���", wn.HisNode.Name, "�˻صĹ���");

            // ɾ���˻�ʱ��ǰ�ڵ�Ĺ�����Ϣ��
            this.HisWork.Delete();
            return wn;
        }
        /// <summary>
        /// ִ���˻�(�ں����ڵ���ִ���˻ص���һ������)
        /// </summary>
        /// <param name="workid">�߳�ID</param>
        /// <param name="msg">�˻ع�����ԭ��</param>
        /// <returns></returns>
        public WorkNode DoReturnWorkHL(Int64 workid, string msg)
        {
            // �ı䵱ǰ�Ĺ����ڵ㣮
            WorkNode wn = this.GetPreviousWorkNode_FHL(workid);

            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.NodeName = wn.HisNode.Name;
            gwf.DirectUpdate();

            // ���� return work ״̬��
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // ɾ��������.
            WorkerLists wkls = new WorkerLists(workid, this.HisNode.NodeID);
            wkls.Delete();

            // д����־.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = workid;
            rw.ReturnToNode = wn.HisNode.NodeID;
            rw.ReturnToEmp = wn.HisWork.Rec;
            rw.ReturnNode = this.HisNode.NodeID; 

            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node='" + wn.HisNode.NodeID + "' and workid=" + workid);

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // ��Ϣ֪ͨ��һ��������Ա����
            WorkerLists wls = new WorkerLists(workid, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "�˻صĹ���", wn.HisNode.Name, "�˻صĹ���");

            // ɾ���˻�ʱ��ǰ�ڵ�Ĺ�����Ϣ��
           // this.HisWork.Delete();
            return wn;
        }
        /// <summary>
        /// ִ�й������
        /// </summary>
        public string DoSetThisWorkOver()
        {
            this.HisWork.AfterWorkNodeComplete();

            // ����״̬��
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
            this.HisWork.Rec = Web.WebUser.No;

            //�ж��ǲ���MD5���̣�
            if (this.HisFlow.IsMD5)
                this.HisWork.SetValByKey("MD5", Glo.GenerMD5(this.HisWork));

            this.HisWork.DirectUpdate();

            // ��������Ĺ�����.
            string sql = "";
            sql = "DELETE FROM WF_GenerWorkerlist WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp <> '" + this.HisWork.Rec.ToString() + "'";
            DBAccess.RunSQL(sql);
            return "";
        }
        /// <summary>
        /// ���� "ְ��" �õ����ܹ�ִ��������ļ���
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDuty(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDuty  a, Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty ) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// ���� "����" �õ����ܹ�ִ��������ļ���
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDept(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDept  a, Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }

        #region ���ݹ�����λ���ɹ�����
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            return WorkerListWayOfDept(town,dt,0);
        }
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt, Int64 fid)
        {
            if (dt.Rows.Count == 0)
            {
                //string msg = "������Ա�б�Ϊ��: ������ƻ�����֯�ṹά������,�ڵ�ķ��ʹ�����("+town.HisNode.RecipientSQL+")";
                throw new Exception(this.ToE("WN4", "������Ա�б�Ϊ��.")); // ������Ա�б�Ϊ��
            }

            Int64 workID = fid;
            if (workID == 0)
                workID = this.HisWork.OID;

            int toNodeId = town.HisNode.NodeID;
            this.HisWorkerLists = new WorkerLists();
            this.HisWorkerLists.Clear();

#warning ����ʱ��  town.HisNode.DeductDays-1

            // 2008-01-22 ֮ǰ�Ķ�����
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , ����Ԥ�����ڵ����⡣

            DateTime dtOfShould;
            int day = 0;
            int hh = 0;
            if (town.HisNode.DeductDays < 1)
                day = 0;
            else
                day =int.Parse( town.HisNode.DeductDays.ToString());

             dtOfShould = DataType.AddDays(DateTime.Now, day);
            DateTime dtOfWarning = DateTime.Now;
            if (town.HisNode.WarningDays > 0)
                dtOfWarning = DataType.AddDays(dtOfShould, - int.Parse(town.HisNode.WarningDays.ToString())); // dtOfShould.AddDays(-town.HisNode.WarningDays);

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFHL:
                case NodeWorkType.WorkFL:
                case NodeWorkType.WorkHL:
                    break;
                default:
                    this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node,
                        town.HisNode.NodeID,
               "SDT", dtOfShould.ToString("yyyy-MM-dd"));
                    break;
            }

            if (dt.Rows.Count == 1)
            {
                /* ���ֻ��һ����Ա */
                WorkerList wl = new WorkerList();
                wl.WorkID = workID;
                wl.FK_Node = toNodeId;
                wl.FK_NodeText = town.HisNode.Name;

                wl.FK_Emp = dt.Rows[0][0].ToString();

                Emp emp = new Emp(wl.FK_Emp);
                wl.FK_EmpText = emp.Name;
                wl.FK_Dept = emp.FK_Dept;
                wl.WarningDays = town.HisNode.WarningDays;
                wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);

                wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                wl.FID = town.HisWork.FID;
                wl.Sender = WebUser.No;
                //if (wl.FID == 0)
                //    wl.FID = this.WorkID;

                wl.DirectInsert();
                this.HisWorkerLists.AddEntity(wl);
               
                    RememberMe rm = new RememberMe(); // this.GetHisRememberMe(town.HisNode);
                    rm.Objs = "@" + wl.FK_Emp + "@";
                    rm.ObjsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                    rm.Emps = "@" + wl.FK_Emp + "@";
                    rm.EmpsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                    this._RememberMe = rm;
                 
            }
            else
            {
                /* ����ж����Ա */
                RememberMe rm = this.GetHisRememberMe(town.HisNode);

                // �������ѡ�����Ա�������������ļ���Ϊ�ա�2011-11-06����糧���� .
                if (this.town.HisNode.HisDeliveryWay == DeliveryWay.BySelected
                    || town.HisNode.IsRememberMe == false)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                if (this.HisNode.IsFL)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                // �������Ƿ���ڵ�ǰ����Ա��
                bool isHaveIt = false;
                string emps = "@";
                foreach (DataRow dr in dt.Rows)
                {
                    string fk_emp = dr[0].ToString();
                    if (rm.Objs.IndexOf("@" + fk_emp) != -1)
                        isHaveIt = true;

                    emps += fk_emp + "@";
                }

                if (isHaveIt == false)
                {
                    /* ��������û�е�ǰ���ɵĲ�����Ա */
                    /* �Ѿ���֤��û���ظ�����Ա��*/
                    string myemps = "";
                    Emp emp = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (myemps.IndexOf(dr[0].ToString()) != -1)
                            continue;

                        myemps += "@" + dr[0].ToString();

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_NodeText = town.HisNode.Name;
                        wl.FK_Emp = dr[0].ToString();
                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;
                        wl.FK_Dept = emp.FK_Dept;
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;
                        wl.Sender = WebUser.No;

                        //if (wl.FID == 0)
                        //    wl.FID = this.WorkID;
                        try
                        {
                            wl.DirectInsert();
                            this.HisWorkerLists.AddEntity(wl);
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("��Ӧ�ó��ֵ��쳣��Ϣ��" + ex.Message);
                        }
                    }
                }
                else
                {
                    string[] strs = rm.Objs.Split('@');
                    string myemps = "";
                    foreach (string s in strs)
                    {
                        if (s.Length < 1)
                            continue;

                        if (myemps.IndexOf(s) != -1)
                            continue;

                        myemps += "@" + s;

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_NodeText = town.HisNode.Name;
                        wl.FK_Emp = s;
                        Emp emp = null;
                        try
                        {
                            emp = new Emp(s);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;
                        wl.FK_Dept = emp.FK_Dept;
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;
                        wl.Sender = WebUser.No;

                        try
                        {
                            if (town.HisNode.IsFLHL == false)
                            {
                                wl.DirectInsert();
                                this.HisWorkerLists.AddEntity(wl);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                string objsmy = "@";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    objsmy += wl.FK_Emp + "@";
                }

                if (rm.Emps != emps || rm.Objs != objsmy)
                {
                    /* ������Ա�б����˱仯 */
                    rm.Emps = emps;
                    rm.Objs = objsmy;

                    string objExts = "";
                    foreach (WorkerList wl in this.HisWorkerLists)
                    {
                        if (Glo.IsShowUserNoOnly)
                            objExts += wl.FK_Emp + "��";
                        else
                            objExts += wl.FK_Emp + "<" + wl.FK_EmpText + ">��";
                    }
                    rm.ObjsExt = objExts;

                    string empExts = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        if (rm.Objs.IndexOf(emp.No) != -1)
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += emp.No + "��";
                            else
                                empExts += emp.No + "<" + emp.Name + ">��";
                        }
                        else
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += "<strike><font color=red>" + emp.No + "</font></strike>��";
                            else
                                empExts += "<strike><font color=red>" + emp.No + "<" + emp.Name + "></font></strike>��";
                        }
                    }
                    rm.EmpsExt = empExts;
                    rm.Save();
                }
            }

            if (this.HisWorkerLists.Count == 0)
                throw new Exception("@���ݲ��Ų���������Ա���ִ�������[" + this.HisWorkFlow.HisFlow.Name + "],�нڵ�[" + town.HisNode.Name + "]�������,û���ҵ����ܴ˹����Ĺ�����Ա.");

            // �����־���͡�
            ActionType at = ActionType.Forward;
            switch (town.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWork:
                case NodeWorkType.StartWorkFL:
                    at = ActionType.Start;
                    break;
                case NodeWorkType.Work:
                    if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkFL
                        || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
                        at = ActionType.ForwardFL;
                    else
                        at = ActionType.Forward;
                    break;
                case NodeWorkType.WorkHL:
                    at = ActionType.ForwardHL;
                    break;
                case NodeWorkType.SubThreadWork:
                    at = ActionType.SubFlowForward;
                    break;
                default:
                    break;
            }
            if (this.HisWorkerLists.Count == 1)
            {
                WorkerList wl = this.HisWorkerLists[0] as WorkerList;
                this.AddToTrack(at, wl.FK_Emp, wl.FK_EmpText, wl.FK_Node, wl.FK_NodeText, null);
            }
            else
            {
                string info = "��(" + this.HisWorkerLists.Count+ ")�˽���\t\n";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    info += wl.FK_Emp + "," + wl.FK_EmpText + "\t\n";
                }
                this.AddToTrack(at, WebUser.No, WebUser.Name, town.HisNode.NodeID, town.HisNode.Name, info);
            }
            return this.HisWorkerLists;
        }
        #endregion


        #region ����

        #region ������λ
        private Station _HisStationOfUse = null;
        /// <summary>
        /// ȡ����ǰ�����õĸ�λ.
        /// �������������λ�Ľڵ���˵��.
        /// 
        /// </summary>
        /// <returns></returns>
        public Station HisStationOfUse
        {
            get
            {
                if (_HisStationOfUse == null)
                {
                    Stations sts = this.HisNode.HisStations;
                    if (sts.Count == 0)
                        throw new Exception(this.ToE("WN18", this.HisNode.Name));

                    _HisStationOfUse = (Station)this.HisNode.HisStations[0];
                    return _HisStationOfUse;

                    if (this.HisStations.Count == 1)
                    {
                        /* �����������ڵ�ֻ����һ��������λ���ʣ���ĩ��������ڵ������������ڵ��õĽڵ㡣*/
                        _HisStationOfUse = (Station)HisStations[0];
                        return _HisStationOfUse;
                    }

                    // �����ǰ�ڵ�������������λ���ʡ� �ҳ�������Ա�Ĺ�����λ����					
                    Stations mySts = this.HisWork.RecOfEmp.HisStations;
                    if (mySts.Count == 1)
                    {
                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                    }

                    // ȡ�����ڵ�Ĺ�����λ�빤����Ա������λ�Ľ�����
                    Stations gainSts = (Stations)mySts.GainIntersection(this.HisStations);
                    if (gainSts.Count == 0)
                    {
                        if (this.HisStations.Count == 0)
                            throw new Exception("@��û��Ϊ�ڵ�[" + this.HisNode.Name + "],���ù�����λ.");

                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                        ///// //throw new Exception("@��ȡʹ�ù�����λ���ִ���,������Ա["+this.HisWork.RecOfEmp.Name+"]�Ĺ���û�д������,���ı������Ĺ�����λ.���¹���������������ת��ȥ.");
                    }

                    /* �ж�������Ҫ������λ,��Ҫ������λ����. */
                    string mainStatNo = this.HisWork.HisRec.No;
                    foreach (Station myst in gainSts)
                    {
                        if (myst.No == mainStatNo)
                        {
                            _HisStationOfUse = myst;
                            return _HisStationOfUse;
                        }
                    }

                    /*ɨ�轻��,��������. */
                    foreach (Station myst in gainSts)
                    {

                    }

                    /* û��ɨ�赽��, ���ص�һ��station.*/
                    if (_HisStationOfUse == null)
                        _HisStationOfUse = (Station)gainSts[0];

                }
                return _HisStationOfUse;
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                return this.HisNode.HisStations;
            }
        }
        /// <summary>
        /// ���ص�һ�������ڵ�
        /// </summary>
        public Station HisStationOfFirst
        {
            get
            {
                return (Station)this.HisNode.HisStations[0];
            }
        }
        #endregion

        #region �ж�һ�˶ಿ�ŵ����
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        private Dept _HisDeptOfUse = null;
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        public Dept HisDeptOfUse
        {
            get
            {
                if (_HisDeptOfUse == null)
                {
                    //�ҵ�ȫ���Ĳ��š�
                    Depts depts;
                    if (this.HisWork.Rec == WebUser.No)
                        depts = WebUser.HisDepts;
                    else
                        depts = this.HisWork.RecOfEmp.HisDepts;

                    if (depts.Count == 0)
                    {
                        throw new Exception("��û�и�[" + this.HisWork.Rec + "]���ò��š�");
                    }

                    if (depts.Count == 1) /* ���ȫ���Ĳ���ֻ��һ�����ͷ�������*/
                    {
                        _HisDeptOfUse = (Dept)depts[0];
                        return _HisDeptOfUse;
                    }

                    if (_HisDeptOfUse == null)
                    {
                        /* �����û�ҵ����ͷ��ص�һ�����š� */
                        _HisDeptOfUse = depts[0] as Dept;
                    }
                }
                return _HisDeptOfUse;
            }
        }
        #endregion

        #endregion

        #region ����
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// �ڵ�������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,���������Ա������ڵ��ϵ�����������.
        /// </summary>
        public Conds HisNodeCompleteConditions
        {
            get
            {
                if (this._HisNodeCompleteConditions == null)
                {
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.HisNode.NodeID, this.WorkID);

                    return _HisNodeCompleteConditions;
                }
                return _HisNodeCompleteConditions;
            }
        }
        private Conds _HisFlowCompleteConditions = null;
        /// <summary>
        /// ����������������,�˽ڵ�������������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,�������������.
        /// </summary>
        public Conds HisFlowCompleteConditions
        {
            get
            {
                if (this._HisFlowCompleteConditions == null)
                {
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.HisNode.NodeID, this.WorkID);
                }
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region ������������
        ///// <summary>
        ///// �õ���ǰ���Ѿ���ɵĹ����ڵ�.
        ///// </summary>
        ///// <returns></returns>
        //public WorkNodes GetHadCompleteWorkNodes()
        //{
        //    WorkNodes mywns = new WorkNodes();
        //    WorkNodes wns = new WorkNodes(this.HisNode.HisFlow, this.HisWork.OID);
        //    foreach (WorkNode wn in wns)
        //    {
        //        if (wn.IsComplete)
        //            mywns.Add(wn);
        //    }
        //    return mywns;
        //}
        #endregion

        #region ���̹�������
        /// <summary>
        /// ���ȫ�ֵĹ��������ǲ����������
        /// �������ˣ��ͷ��ز����κδ���
        /// �������������ɵ��������ͷ�����Ӧ����Ϣ��
        /// ������������̵����������return null;  
        /// </summary>
        public string CheckGlobalCompleteCondition()
        {
            if (this.HisWorkFlow.IsComplete)  // ���ȫ�ֵĹ����Ѿ����.
                return this.ToE("FlowOver", "��ǰ�������Ѿ����"); //��ǰ�������Ѿ����

            #region ���ȫ�ֵ��������,���ڲ������õ�,���Ծ���ʱɾ��.
            /*
			GlobalCompleteConditions gcc = new GlobalCompleteConditions(this.HisNode.FK_Flow,this.HisWork.OID);
			if (gcc.IsOneOfConditionPassed)
			{
				this.HisWorkFlow.DoFlowOver();
				return "@��������["+this.HisNode.HisFlow.Name+"]ִ�й����У��ڽڵ�["+this.HisNode.Name+"]�����������������"+gcc.GetOneOfConditionPassed.ConditionDesc+"����������������";
			}
			*/
            #endregion

            return null;
        }
        private Flow _HisFlow = null;
        public Flow HisFlow
        {
            get
            {
                if (_HisFlow == null)
                    _HisFlow = this.HisNode.HisFlow;
                return _HisFlow;
            }
        }
        public Node JumpToNode = null;
        public string JumpToEmp = null;
        /// <summary>
        /// ������̻ع�������
        /// </summary>
        /// <param name="TransferPC"></param>
        /// <param name="ByTransfered"></param>
        /// <returns></returns>
        public string AfterNodeSave()
        {
            // ����Ƿ��ǵ�ǰ�ڵ㣬������ǾͿ��ܴ���sdk�û�ˢ�µĿ��ܣ����������ظ��ύ��
            //if (this.HisGenerWorkFlow.FK_Node != this.HisNode.NodeID)
            //    throw new Exception("@�������ظ��ύ������ˢ�½��档");

            DBAccess.DoTransactionBegin();
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;
            string msg = this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendWhen, this.HisWork);
            // ���÷���ǰ�Ľӿڡ�
            try
            {
                #region ���������־.
                if (this.HisNode.IsStartNode)
                {
                    /*����ǿ�ʼ�����ж��ǲ��Ǳ���������̣�����Ǿ�Ҫ������д��־��*/
                    if (SystemConfig.IsBSsystem)
                    {
                        string fk_nodeFrom = System.Web.HttpContext.Current.Request.QueryString["FromNode"];
                        if (string.IsNullOrEmpty(fk_nodeFrom) == false)
                        {
                            Node ndFrom = new Node(int.Parse(fk_nodeFrom));
                            string fromWorkID = System.Web.HttpContext.Current.Request.QueryString["FromWorkID"];

                            GenerWorkFlow gwfP = new GenerWorkFlow(Int64.Parse(fromWorkID));

                            //��¼��ǰ���̱�����
                            this.AddToTrack(ActionType.StartSubFlow, WebUser.No,
                                WebUser.Name, ndFrom.NodeID, ndFrom.FlowName + "\t\n" + ndFrom.Name, "��������(" + ndFrom.FlowName + ":" + gwfP.Title + ")����.");

                            //��¼�����̱�����
                            Track tkParent = new Track();
                            tkParent.WorkID = Int64.Parse(fromWorkID);
                            tkParent.RDT = DataType.CurrentDataTimess;
                            tkParent.HisActionType = ActionType.CallSubFlow;
                            tkParent.EmpFrom = WebUser.No;
                            tkParent.EmpFromT = WebUser.Name;

                            tkParent.NDTo = this.HisNode.NodeID;
                            tkParent.NDToT = this.HisNode.FlowName + " \t\n " + this.HisNode.Name;

                            tkParent.FK_Flow = ndFrom.FK_Flow;

                            tkParent.NDFrom = ndFrom.NodeID;
                            tkParent.NDFromT = ndFrom.Name;

                            tkParent.EmpTo = WebUser.No;
                            tkParent.EmpToT = WebUser.Name;
                            tkParent.Msg = "<a href='Track.aspx?FK_Flow=" + this.HisNode.FK_Flow + "&WorkID=" + this.HisWork.OID + "' target=_b >����������(" + this.HisNode.FlowName + ")</a>";
                            tkParent.MyPK = tkParent.WorkID + "_" + tkParent.FID + "_" + (int)tkParent.HisActionType + "_" + tkParent.NDFrom + "_" + DateTime.Now.ToString("yyMMddhhmmss");
                            tkParent.Insert();
                        }
                    }
                }
                #endregion ���������־.

                msg += AfterNodeSave_Do();
                DBAccess.DoTransactionCommit(); //�ύ����.
                try
                {
                    // �����ͳɹ��������
                    msg += this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendSuccess, this.HisWork);
                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }

                string msgOfSend = this.HisNode.TurnToDealDoc;
                if (msgOfSend.Length > 3)
                {
                    if (msgOfSend.Contains("@"))
                    {
                        Attrs attrs = this.HisWork.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            msgOfSend = msgOfSend.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                        }
                    }
                    return msgOfSend;
                }

                // �����Ҫ��ת.
                if (town != null)
                {
                    if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
                        return town.AfterNodeSave();
                }

                return msg;
            }
            catch (Exception ex)
            {
                this.WhenTranscactionRollbackError(ex);
                DBAccess.DoTransactionRollback();
                throw ex;
            }
        }
        private void WhenTranscactionRollbackError(Exception ex)
        {
            /*���ύ���������£��ع����ݡ�*/
            try
            {
                // �ѹ�����״̬���û�����
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // �����̵�״̬���û�����
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
                if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                {
                    /* ���������������һ���б仯��*/
                    gwf.FK_Node = this.HisNode.NodeID;
                    gwf.NodeName = this.HisNode.Name;
                    gwf.WFState = 0;
                    gwf.Update();
                }

                //ִ������.
                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE FK_Emp='"+WebUser.No+"' AND WorkID="+this.WorkID+" AND FK_Node="+this.HisNode.NodeID);

                Node startND = this.HisNode.HisFlow.HisStartNode;
                StartWork wk = startND.HisWork as StartWork;
                switch (startND.HisNodeWorkType)
                {
                    case NodeWorkType.StartWorkFL:
                    case NodeWorkType.WorkFL:
                        break;
                    default:
                        /*
                         Ҫ����ɾ��WFState �ڵ��ֶε����⡣
                         */
                        //// �ѿ�ʼ�ڵ��װ̬���û�����
                        //DBAccess.RunSQL("UPDATE " + wk.EnMap.PhysicsTable + " SET WFState=0 WHERE OID="+this.WorkID+" OR OID="+this);
                        //wk.OID = this.WorkID;
                        //int i =wk.RetrieveFromDBSources();
                        //if (wk.WFState == WFState.Complete)
                        //{
                        //    wk.Update("WFState", (int)WFState.Runing);
                        //}
                        break;
                }
                Nodes nds = this.HisNode.HisToNodes;
                foreach (Node nd in nds)
                {
                    Work mwk = nd.HisWork;
                    mwk.OID = this.WorkID;
                    mwk.DirectDelete();
                }
                this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendError, this.HisWork);
            }
            catch (Exception ex1)
            {
                if (this.rptGe != null)
                    this.rptGe.CheckPhysicsTable();
                throw new Exception(ex.Message + "@�ع�����ʧ�����ݳ��ִ���" + ex1.Message + "@�п���ϵͳ�Ѿ��Զ��޸���������������ִ��һ�Ρ�");
            }
        }
        #region �û����ı���
        public WorkerLists HisWorkerLists = null;
        public GenerWorkFlow _HisGenerWorkFlow;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID);
                return _HisGenerWorkFlow;
            }
            set
            {
                _HisGenerWorkFlow = value;
            }
        }
        private Int64 _WorkID = 0;
        public Int64 WorkID
        {
            get
            {
                return _WorkID;
            }
            set
            {
                _WorkID = value;
            }
        }
        #endregion
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public string FeiLiuStartUp(Node toNode)
        {
            // ����.
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // ������һ����Ҫִ�е���Ա.
            WorkerLists gwls = this.GenerWorkerLists(town);
            this.AddIntoWacthDog(gwls);  //@������Ϣ�����

            //�����ǰ�����ݣ��������η��͡�
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            // �жϷ����Ĵ���.�ǲ�����ʷ��¼�����з�����
            bool IsHaveFH = false;
            if (DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkerlist WHERE FID=" + this.HisWork.OID) != 0)
                IsHaveFH = true;

            string msg = "";
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                                            this.WorkID.ToString());

            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                                           this.WorkID.ToString());

            MapDtls dtlsFrom = new MapDtls("ND" + this.HisNode.NodeID);
            if (dtlsFrom.Count > 1)
            {
                foreach (MapDtl d in dtlsFrom)
                {
                    d.HisGEDtls_temp = null;
                }
            }
            MapDtls dtlsTo = null;
            if (dtlsFrom.Count >= 1)
                dtlsTo = new MapDtls("ND" + toNode.NodeID);

            // ���ղ��ŷ��飬�ֱ��������̡�
            switch (this.HisNode.HisFLRole)
            {
                case FLRole.ByStation:
                case FLRole.ByDept:
                case FLRole.ByEmp:
                    foreach (WorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.rptGe);
                        mywk.Copy(this.HisWork);  //���ƹ�����Ϣ��

                        bool isHaveEmp = false;
                        if (IsHaveFH)
                        {
                            /* ��������߹��������������ҵ�ͬһ����Աͬһ��FID�µ�OID �����⵱ǰ�̵߳�ID��*/
                            DataTable dt = DBAccess.RunSQLReturnTable("SELECT WorkID,FK_Node FROM WF_GenerWorkerlist WHERE FID=" + this.WorkID + " AND FK_Emp='" + wl.FK_Emp + "' ORDER BY RDT DESC");
                            if (dt.Rows.Count == 0)
                            {
                                /*û�з��֣���˵����ǰ�����ڵ���û������˵ķ�����Ϣ */
                                mywk.OID = DBAccess.GenerOID();
                            }
                            else
                            {
                                int workid_old = (int)dt.Rows[0][0];
                                int fk_Node_nearly = (int)dt.Rows[0][1];
                                Node nd_nearly = new Node(fk_Node_nearly);
                                Work nd_nearly_work = nd_nearly.HisWork;
                                nd_nearly_work.OID = workid_old;
                                if (nd_nearly_work.RetrieveFromDBSources() != 0)
                                {
                                    mywk.Copy(nd_nearly_work);
                                    mywk.OID = workid_old;
                                }
                                else
                                {
                                    mywk.OID = DBAccess.GenerOID();
                                }
                                isHaveEmp = true;
                            }
                        }
                        else
                        {
                            mywk.OID = DBAccess.GenerOID();  //BP.DA.DBAccess.GenerOID();
                        }
                        mywk.FID = this.HisWork.FID;
                        mywk.Rec = wl.FK_Emp;
                        mywk.Emps = wl.FK_Emp;
                        mywk.BeforeSave();

                        //�ж��ǲ���MD5���̣�
                        if (this.HisFlow.IsMD5)
                            mywk.SetValByKey("MD5", Glo.GenerMD5(mywk));

                        mywk.InsertAsOID(mywk.OID);

                        #region  ���Ƹ�����Ϣ
                        if (athDBs.Count > 0)
                        {
                            /* ˵����ǰ�ڵ��и������� */
                            athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toNode.NodeID,
                                FrmAttachmentDBAttr.RefPKVal, mywk.OID);
                            int i = 0;
                            foreach (FrmAttachmentDB athDB in athDBs)
                            {
                                i++;
                                FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                athDB_N.Copy(athDB);
                                athDB_N.FK_MapData = "ND" + toNode.NodeID;
                                athDB_N.MyPK = mywk.OID + "_" + mywk.FID + "_" + toNode.NodeID + "_" + i.ToString();
                                athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                    "ND" + toNode.NodeID);
                                athDB_N.RefPKVal = mywk.OID.ToString();
                                athDB_N.DirectInsert();
                            }
                        }
                        #endregion  ���Ƹ�����Ϣ

                        #region  ����ǩ����Ϣ
                        if (eleDBs.Count > 0)
                        {
                            /* ˵����ǰ�ڵ��и������� */
                            eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + toNode.NodeID,
                                FrmEleDBAttr.RefPKVal, mywk.OID);
                            int i = 0;
                            foreach (FrmEleDB eleDB in eleDBs)
                            {
                                i++;
                                FrmEleDB athDB_N = new FrmEleDB();
                                athDB_N.Copy(eleDB);
                                athDB_N.FK_MapData = "ND" + toNode.NodeID;
                                athDB_N.RefPKVal = mywk.OID.ToString();
                                athDB_N.GenerPKVal();
                                athDB_N.DirectInsert();
                            }
                        }
                        #endregion  ���Ƹ�����Ϣ


                        #region  ������ϸ����Ϣ.
                        if (dtlsFrom.Count > 0)
                        {
                            int i = -1;
                            foreach (Sys.MapDtl dtl in dtlsFrom)
                            {
                                i++;
                                if (dtlsTo.Count <= i)
                                    continue;
                                Sys.MapDtl toDtl = (Sys.MapDtl)dtlsTo[i];
                                if (toDtl.IsCopyNDData == false)
                                    continue;

                                //��ȡ��ϸ���ݡ�
                                GEDtls gedtls = null;
                                if (dtl.HisGEDtls_temp == null)
                                {
                                    gedtls = new GEDtls(dtl.No);
                                    QueryObject qo = null;
                                    qo = new QueryObject(gedtls);
                                    switch (dtl.DtlOpenType)
                                    {
                                        case DtlOpenType.ForEmp:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForWorkID:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForFID:
                                            qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                            break;
                                    }
                                    qo.DoQuery();
                                    dtl.HisGEDtls_temp = gedtls;
                                }
                                gedtls = dtl.HisGEDtls_temp;

                                int unPass = 0;
                                DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + mywk.OID);
                                foreach (GEDtl gedtl in gedtls)
                                {
                                    BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                                    dtCopy.Copy(gedtl);
                                    dtCopy.FK_MapDtl = toDtl.No;
                                    dtCopy.RefPK = mywk.OID.ToString();
                                    dtCopy.OID = 0;
                                    dtCopy.Insert();

                                    //dtCopy.InsertAsOID(gedtl.OID);
                                    //dtCopy.InsertAsOID(gedtl.OID);

                                    #region  ������ϸ���� - ������Ϣ - M2M- M2MM
                                    if (toDtl.IsEnableAthM)
                                    {
                                        /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                                        athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                        if (athDBs.Count > 0)
                                        {
                                            i = 0;
                                            foreach (FrmAttachmentDB athDB in athDBs)
                                            {
                                                i++;
                                                FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                                athDB_N.Copy(athDB);
                                                athDB_N.FK_MapData = toDtl.No;
                                                athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                                athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                                    "ND" + toNode.NodeID);
                                                athDB_N.RefPKVal = dtCopy.OID.ToString();
                                                athDB_N.DirectInsert();
                                            }
                                        }
                                    }
                                    if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                                    {
                                        /*���������m2m */
                                        M2Ms m2ms = new M2Ms(dtl.No, gedtl.OID);
                                        if (m2ms.Count > 0)
                                        {
                                            i = 0;
                                            foreach (M2M m2m in m2ms)
                                            {
                                                i++;
                                                M2M m2m_N = new M2M();
                                                m2m_N.Copy(m2m);
                                                m2m_N.FK_MapData = toDtl.No;
                                                m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                                m2m_N.EnOID = gedtl.OID;
                                                m2m_N.InitMyPK();
                                                m2m_N.DirectInsert();
                                            }
                                        }
                                    }
                                    #endregion  ������ϸ���� - ������Ϣ

                                }
                            }
                        }
                        #endregion  ���Ƹ�����Ϣ

                        // ������������Ϣ��
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;
                        gwf.Title = this.GenerTitle(this.HisWork);

                        //if (BP.WF.Glo.IsShowUserNoOnly == false)
                        //    gwf.Title = WebUser.No + "," + WebUser.Name + " ����" + toNode.Name + "(�����ڵ�)";
                        //else
                        //    gwf.Title = WebUser.No + " ����" + toNode.Name + "(�����ڵ�)";

                        gwf.WFState = 0;
                        gwf.RDT = DataType.CurrentDataTime;
                        gwf.Rec = Web.WebUser.No;
                        gwf.RecName = Web.WebUser.Name;
                        gwf.FK_Flow = toNode.FK_Flow;
                        gwf.FlowName = toNode.FlowName;
                        gwf.FID = this.WorkID;
                        gwf.FK_FlowSort = toNode.HisFlow.FK_FlowSort;
                        gwf.FK_Node = toNode.NodeID;
                        gwf.NodeName = toNode.Name;
                        gwf.FK_Dept = wl.FK_Dept;
                        gwf.DeptName = wl.FK_DeptT;
                        try
                        {
                            gwf.DirectInsert();
                        }
                        catch
                        {
                            gwf.DirectUpdate();
                        }
                        DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WorkID=" + mywk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + toNode.NodeID);
                    }
                    break;
                default:
                    throw new Exception("û�д�������ͣ�" + this.HisNode.HisFLRole.ToString());
            }

            // return ��ϵͳ�Զ��´�����¼�λͬ�¡�";

            string info = this.ToE("WN28", "@�����ڵ�:{0}�Ѿ�����@�����Զ��´��{1}����{2}λͬ��,{3}.");

            msg += string.Format(info, toNode.Name,
                this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(),
                this._RememberMe.EmpsExt);

            // ����ǿ�ʼ�ڵ㣬�Ϳ�������ѡ������ˡ�
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2)
                    msg += "@<img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0 /><a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "')\" >" + this.ToE("W29", "�޸Ľ��ܶ���") + "</a>.";
            }

            if (this.HisNode.IsStartNode)
            {
                msg += "@<a href='" + this.VirPath  + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>�� <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + toNode.FK_Flow + "&FK_Node="+toNode.FK_Flow+"01' ><img src=./Img/New.gif border=0/>" + this.ToE("NewFlow", "�½�����") + "</a>��";
            }
            else
            {
                msg += "@<a href='" + this.VirPath  + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��";
                //  msg += "@<a href=\"javascript:WinOpen('" + this.VirPath + "/" + this.AppType + "/" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��";
            }

            msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //���½ڵ�״̬��
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            msg += "@<a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
            return msg;
            // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
            // return this.ToEP3("TaskAutoSendTo", "@�����Զ��´��{0}����{1}λͬ��,{2}.", this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string FeiLiuStartUp()
        {
            #region GenerFH
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                try
                {
                    fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                }
                catch (Exception ex)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + this.HisNode.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "Title";
                    int i = attr.Retrieve(MapAttrAttr.FK_MapData, attr.FK_MapData, MapAttrAttr.KeyOfEn, attr.KeyOfEn);
                    if (i == 0)
                    {
                        attr.KeyOfEn = "Title";
                        attr.Name = BP.Sys.Language.GetValByUserLang("Title", "����"); // "���̱���";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.IDX = -100;
                        attr.Insert();
                    }
                    fh.Title = WebUser.No + "-" + WebUser.Name + " @ " + DataType.CurrentDataTime + " ";
                }
                fh.RDT = DataType.CurrentData;
                fh.FID = this.WorkID;
                fh.FK_Flow = this.HisNode.FK_Flow;
                fh.FK_Node = this.HisNode.NodeID;
                fh.GroupKey = WebUser.No;
                fh.WFState = 0;
                try
                {
                    fh.DirectInsert();
                }
                catch
                {
                    fh.DirectUpdate();
                }
            }
            #endregion GenerFH

            string msg = "";
            Nodes toNodes = this.HisNode.HisToNodes;

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return FeiLiuStartUp((Node)toNodes[0]);

            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, this.HisNode.NodeID, CondAttr.PRI);
            if (dcsAll.Count == 0)
            {
                /*���û�����÷���������ȫ��ͨ��*/
                return msg + StartNextNode(toNodes);
            }

            #region ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.
            Nodes myNodes = new Nodes();
            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                foreach (Cond dc in dcsAll)
                {
                    if (dc.ToNodeID != nd.NodeID)
                        continue;

                    dc.WorkID = this.HisWork.OID;
                    dcs.AddEntity(dc);
                }

                if (dcs.Count == 0)
                {
                    // ���û�����÷�����������Ĭ��ͨ����.
                    myNodes.AddEntity(nd);
                    continue;
                    // throw new Exception(string.Format(this.ToE("WN10", "@����ڵ�ķ�����������:û�и���{0}�ڵ㵽{1},����ת������."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                }

                if (dcs.IsPass) // ������ת����������һ������.
                {
                    myNodes.AddEntity(nd);
                    continue;
                    //numOfWay++;
                    //toNodeId = nd.NodeID;
                    //msg = FeiLiuStartUp(nd);
                }
            }
            #endregion ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.

            if (myNodes.Count == 0)
            {
                throw new Exception(string.Format(this.ToE("WN10_1",
                    "@����ڵ�ķ�����������:û�и���{0}�ڵ㵽�����ڵ�,����ת������."), this.HisNode.NodeID + this.HisNode.Name));
            }
            else
            {
                return msg + StartNextNode(myNodes);
            }
        }
        #endregion
        public string GenerTitle(Work wk)
        {
            // ���ɱ���.
            Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
            if (myattr == null)
                myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

            string titleRole = "";
            if (myattr != null)
                titleRole = myattr.DefaultVal.ToString();

            if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name��@RDT����.";

            titleRole = titleRole.Replace("@WebUser.No", wk.Rec);
            titleRole = titleRole.Replace("@WebUser.Name", wk.RecText);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", wk.RecOfEmp.FK_DeptText);
            titleRole = titleRole.Replace("@RDT", wk.RDT);
            if (titleRole.Contains("@"))
            {
                foreach (Attr attr in wk.EnMap.Attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsFKorEnum)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        public GEEntity rptGe = null;
        private void InitStartWorkData()
        {
            /* ������ʼ�������̼�¼. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;
            gwf.Title = this.GenerTitle(this.HisWork);
            gwf.WFState = 0;
            gwf.RDT = this.HisWork.RDT;
            gwf.Rec = Web.WebUser.No;
            gwf.RecName = Web.WebUser.Name;
            gwf.FK_Flow = this.HisNode.FK_Flow;
            gwf.FlowName = this.HisNode.FlowName;
            gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;
            gwf.FK_Node = this.HisNode.NodeID;
            gwf.NodeName = this.HisNode.Name;
            gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
            gwf.DeptName = this.HisWork.RecOfEmp.FK_DeptText;
            if (Glo.IsEnablePRI)
            {
                try
                {
                    gwf.PRI = this.HisWork.GetValIntByKey("PRI");
                }
                catch (Exception ex)
                {
                    this.HisNode.RepareMap();
                }
            }

            try
            {
                gwf.DirectInsert();
            }
            catch
            {
                gwf.DirectUpdate();
            }

            StartWork sw = (StartWork)this.HisWork;

            #region ����  HisGenerWorkFlow

            this.HisGenerWorkFlow = gwf;

            //#warning ȥ���������û���뵽���ȥд��
            // ��¼������ͳ�Ʒ�����ȥ��
            //this.HisCHOfFlow = new CHOfFlow();
            //this.HisCHOfFlow.Copy(gwf);
            //this.HisCHOfFlow.WorkID = this.HisWork.OID;
            //this.HisCHOfFlow.WFState = (int)WFState.Runing;
            ///* ˵��û�������¼ */
            //this.HisCHOfFlow.FK_Flow = this.HisNode.FK_Flow;
            //this.HisCHOfFlow.WFState = 0;
            //this.HisCHOfFlow.Title = gwf.Title;
            //this.HisCHOfFlow.FK_Emp = this.HisWork.Rec.ToString();
            //this.HisCHOfFlow.RDT = this.HisWork.RDT;
            //this.HisCHOfFlow.CDT = DataType.CurrentDataTime;
            //this.HisCHOfFlow.SpanDays = 0;
            //this.HisCHOfFlow.FK_Dept = this.HisDeptOfUse.No;
            //this.HisCHOfFlow.FK_NY = DataType.CurrentYearMonth;
            //try
            //{
            //    this.HisCHOfFlow.Insert();
            //}
            //catch
            //{
            //    this.HisCHOfFlow.Update();
            //}
            #endregion HisCHOfFlow

            #region  ������ʼ������,�ܹ�ִ�����ǵ���Ա.
            WorkerList wl = new WorkerList();
            wl.WorkID = this.HisWork.OID;
            wl.FK_Node = this.HisNode.NodeID;
            wl.FK_NodeText = this.HisNode.Name;
            wl.FK_Emp = WebUser.No;
            wl.FK_EmpText = WebUser.Name;
            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.FK_Dept = WebUser.FK_Dept;
            wl.WarningDays = this.HisNode.WarningDays;
            wl.SDT = DataType.CurrentData;
            wl.DTOfWarning = DataType.CurrentData;
            wl.RDT = DataType.CurrentDataTime;
            try
            {
                wl.Insert(); // �Ȳ��룬����¡�
            }
            catch (Exception ex)
            {
                wl.Update();
            }
            #endregion
        }
        /// <summary>
        /// ִ������copy.
        /// </summary>
        /// <param name="fromWK"></param>
        private void DoCopyRptWork(Work fromWK)
        {
            foreach (Attr attr in fromWK.EnMap.Attrs)
            {
                switch (attr.Key)
                {
                    case BP.WF.GERptAttr.FK_NY:
                    case BP.WF.GERptAttr.FK_Dept:
                    case BP.WF.GERptAttr.FlowDaySpan:
                    case BP.WF.GERptAttr.FlowEmps:
                    case BP.WF.GERptAttr.FlowEnder:
                    case BP.WF.GERptAttr.FlowEnderRDT:
                    case BP.WF.GERptAttr.FlowStarter:
                    case BP.WF.GERptAttr.Title:
                        continue;
                    default:
                        break;
                }

                object obj = fromWK.GetValByKey(attr.Key);
                if (obj == null)
                    continue;
                this.rptGe.SetValByKey(attr.Key, obj);
            }
            if (this.HisNode.IsStartNode)
                this.rptGe.SetValByKey("Title", fromWK.GetValByKey("Title"));
        }
        /// <summary>
        /// �����̽ڵ㱣���Ĳ���.
        /// 1, �жϽڵ�������ǲ������,������,�����ýڵ�����״̬.
        /// 2, �ж��ǲ��Ƿ��Ϲ���������������������, ������,������,���������������. return ;
        /// 3, �жϽڵ�ķ���.
        /// 1, һ��һ�����жϴ˽ڵ�ķ���, ����������ڵ㹤��.
        /// 2, ���û���κ�һ���ڵ�Ĺ���.��ĩ���׳��쳣, ���̵Ľڵ㷽�������������. .
        /// </summary>
        /// <param name="TransferPC">�Ƿ�Ҫִ�л�ȡ�ⲿ��Ϣ����</param>
        /// <param name="ByTransfered">�ǲ��Ǳ����õ�,ֻ���ڿ�ʼ�ڵ�,�������ⲿ���õ��Զ�����������,�������Ϊtrue, �����ڵ�ǰ�Ľڵ����֮��Ͳ�����һ����Ĺ���,�������Ϊfalse, ������һ������.</param>
        /// <returns>ִ�к������</returns>
        private string AfterNodeSave_Do()
        {
            #region �����ݷŵ����������.
            if (this.HisNode.IsStartNode)
                this.InitStartWorkData();

            this.rptGe = this.HisNode.HisFlow.HisFlowData;
            this.rptGe.SetValByKey("OID", this.WorkID);
            if (rptGe.RetrieveFromDBSources() == 0)
            {
                rptGe.SetValByKey("OID", this.WorkID);
                this.DoCopyRptWork(this.HisWork);

                rptGe.SetValByKey(GERptAttr.FlowEmps, "@" + WebUser.No + "," + WebUser.Name);
                rptGe.SetValByKey(GERptAttr.FlowStarter, WebUser.No);
                rptGe.SetValByKey(GERptAttr.FlowStartRDT, DataType.CurrentDataTime);
                rptGe.SetValByKey(GERptAttr.WFState, 0);
                rptGe.SetValByKey(GERptAttr.FK_NY, DataType.CurrentYearMonth);
                rptGe.SetValByKey(GERptAttr.FK_Dept, WebUser.FK_Dept);
                rptGe.Insert();
            }
            else
            {
                foreach (Attr attr in this.HisWork.EnMap.Attrs)
                {
                    switch (attr.Key)
                    {
                        case StartWorkAttr.FK_Dept:
                        case StartWorkAttr.FID:
                        case StartWorkAttr.CDT:
                        case StartWorkAttr.RDT:
                        case StartWorkAttr.Rec:
                        case StartWorkAttr.Sender:
                        case StartWorkAttr.NodeState:
                        case StartWorkAttr.OID:
                        case StartWorkAttr.Title:
                            continue;
                        default:
                            break;
                    }
                    object obj = this.HisWork.GetValByKey(attr.Key);
                    if (obj == null)
                        continue;
                    rptGe.SetValByKey(attr.Key, obj);
                }

                try
                {
                    string str = rptGe.GetValStrByKey(GERptAttr.FlowEmps);
                    if (str.Contains("@" + WebUser.No + "," + WebUser.Name) == false)
                        rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + WebUser.No + "," + WebUser.Name);
                }
                catch
                {
                    this.HisNode.HisFlow.DoCheck();
                }

                if (this.HisNode.IsEndNode)
                {
                    rptGe.SetValByKey(GERptAttr.WFState, 1); // ����״̬��
                    rptGe.SetValByKey(GERptAttr.FlowEnder, WebUser.No);
                    rptGe.SetValByKey(GERptAttr.FlowEnderRDT, DataType.CurrentDataTime);
                    rptGe.SetValByKey(GERptAttr.FlowDaySpan, DataType.GetSpanDays(this.rptGe.GetValStringByKey(GERptAttr.FlowStartRDT), DataType.CurrentDataTime));
                }
                rptGe.DirectUpdate();

                /* �鿴һ�±�·���Ƿ���ԭ·�˻صĿ��ܣ�*/
                //string retoNode = DBAccess.RunSQLReturnString("SELECT ReturnNode FROM WF_ReturnWork WHERE ReturnToNode=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID + " AND IsBackTracking=1", null);
                //if (retoNode != null)
                //{
                //    /*˵����ԭ·�˻ص����⡣*/
                //    this.JumpToEmp = DBAccess.RunSQLReturnString("SELECT Returner FROM WF_ReturnWork WHERE ReturnToNode=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID + " AND IsBackTracking=1", null);
                //    this.JumpToNode = new Node(int.Parse(retoNode));
                //}
            }
            #endregion

            #region ���ݵ�ǰ�ڵ����Ͳ�ͬ����ͬ��ģʽ��
            string msg = "";
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFL:  /* �������� */
                    this.HisWork.FID = this.HisWork.OID;
                    msg = this.FeiLiuStartUp();
                    break;
                case NodeWorkType.WorkFHL:   /* �������� */
                    this.HisWork.FID = this.HisWork.OID;
                    msg = this.FeiLiuStartUp();
                    break;
                case NodeWorkType.WorkHL:   /* ��ǰ�����ڵ��Ǻ��� */
                    msg = this.StartupNewNodeWork();
                    msg += this.DoSetThisWorkOver(); // ִ�д˹���������
                    break;
                default: /* �����ĵ���߼� */
                    msg = this.StartupNewNodeWork();
                    msg += this.DoSetThisWorkOver();
                    break;
            }
            #endregion ���ݵ�ǰ�ڵ����Ͳ�ͬ����ͬ��ģʽ��

            #region ��������
            if (Glo.IsEnableSysMessage)
            {
                Listens lts = new Listens();
                lts.RetrieveByLike(ListenAttr.Nodes, "%" + this.HisNode.NodeID + "%");

                foreach (Listen lt in lts)
                {
                    string sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE IsEnable=1 AND IsPass=1 AND FK_Node=" + lt.FK_Node + " AND WorkID=" + this.WorkID;
                    DataTable dtRem = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    foreach (DataRow dr in dtRem.Rows)
                    {
                        string fk_emp = dr["FK_Emp"] as string;
                        Port.WFEmp emp = new BP.WF.Port.WFEmp(fk_emp);
                        if (emp.HisAlertWay == BP.WF.Port.AlertWay.None)
                        {
                            // msg += "@<font color=red>����Ϣ�޷����͸���" + emp.Name + "����Ϊ���ر�����Ϣ���ѣ�������绰��"+emp.Tel+"</font>";
                            msg += "@<font color=red>" + this.ToEP2("WN25", "����Ϣ�޷����͸���{0}����Ϊ���ر�����Ϣ���ѣ�������绰��{1}��", emp.Name, emp.Tel) + "</font>";
                            continue;
                        }
                        else
                        {
                            // msg += "@���Ĳ����Ѿ�ͨ����<font color=green><b>" + emp.HisAlertWayT + "</b></font>���ķ�ʽ���͸���" + emp.Name;
                            msg += this.ToEP2("WN26", "@���Ĳ����Ѿ�ͨ����<font color=green><b>{0}</b></font>���ķ�ʽ���͸���{1}", emp.HisAlertWayT, emp.Name);
                        }

                        string title = lt.Title.Clone() as string;

                        title = title.Replace("@WebUser.No", WebUser.No);
                        title = title.Replace("@WebUser.Name", WebUser.Name);
                        title = title.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                        title = title.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

                        string doc = lt.Doc.Clone() as string;
                        doc = doc.Replace("@WebUser.No", WebUser.No);
                        doc = doc.Replace("@WebUser.Name", WebUser.Name);
                        doc = doc.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                        doc = doc.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

                        Attrs attrs = this.rptGe.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            title = title.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                            doc = doc.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                        }

                        BP.TA.SMS.AddMsg(lt.OID + "_" + this.WorkID, fk_emp, emp.HisAlertWay, emp.Tel, title, emp.Email, title, doc);
                    }
                }
            }
            #endregion

            #region ���ɵ���
            BillTemplates reffunc = this.HisNode.HisBillTemplates;
            if (reffunc.Count > 0)
            {
                #region ���ɵ�����Ϣ
                Int64 workid = this.HisWork.OID;
                int nodeId = this.HisNode.NodeID;
                string flowNo = this.HisNode.FK_Flow;
                #endregion

                DateTime dt = DateTime.Now;
                Flow fl = new Flow(this.HisNode.FK_Flow);
                string year = dt.Year.ToString();
                string billInfo = "";
                foreach (BillTemplate func in reffunc)
                {
                    string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                    BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();

                    Works works;
                    string[] paths;
                    string path;
                    try
                    {
                        #region ���ɵ���
                        rtf.HisEns.Clear();
                        rtf.EnsDataDtls.Clear();
                        if (func.NodeID == 0)
                        {
                            // �ж��Ƿ��������ִ
                            //if (fl.DateLit == 0)
                            //    continue;
                            //HisCHOfFlow.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
                            //HisCHOfFlow.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
                            //HisCHOfFlow.Update();
                            //   rtf.AddEn(HisCHOfFlow);
                        }
                        else
                        {
                            WorkNodes wns = new WorkNodes();
                            if (this.HisNode.HisFNType == FNType.River)
                                wns.GenerByFID(this.HisNode.HisFlow, this.WorkID);
                            else
                                wns.GenerByWorkID(this.HisNode.HisFlow, this.WorkID);

                            rtf.HisGEEntity = rptGe;
                            works = wns.GetWorks;
                            foreach (Work wk in works)
                            {
                                if (wk.OID == 0)
                                    continue;

                                rtf.AddEn(wk);
                                rtf.ensStrs += ".ND" + wk.NodeID;
                                ArrayList al = wk.GetDtlsDatasOfArrayList();
                                foreach (Entities ens in al)
                                    rtf.AddDtlEns(ens);
                            }
                            //    w = new BP.Port.WordNo(WebUser.FK_DeptOfXJ);
                            // rtf.AddEn(w);
                        }

                        paths = file.Split('_');
                        path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                        string billUrl = this.VirPath + "/DataUser/Bill/" + path + file;

                        if (func.HisBillFileType == BillFileType.PDF)
                        {
                            billUrl = billUrl.Replace(".doc", ".pdf");
                            billInfo += "<img src='" + this.VirPath + "/Images/FileType/PDF.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                        }
                        else
                        {
                            billInfo += "<img src='" + this.VirPath + "/Images/FileType/doc.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                        }

                        //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                        path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";

                        if (System.IO.Directory.Exists(path) == false)
                            System.IO.Directory.CreateDirectory(path);

                        rtf.MakeDoc(func.Url + ".rtf",
                            path, file, func.ReplaceVal, false);
                        #endregion

                        #region ת����pdf.
                        if (func.HisBillFileType == BillFileType.PDF)
                        {
                            string rtfPath = path + file;
                            string pdfPath = rtfPath.Replace(".doc", ".pdf");
                            //  string pdfPath = path + func.Url + ".pdf";
                            try
                            {
                                Glo.Rtf2PDF(rtfPath, pdfPath);
                            }
                            catch (Exception ex)
                            {
                                msg += ex.Message;
                            }
                        }
                        #endregion

                        #region ���浥��
                        Bill bill = new Bill();
                        bill.MyPK = this.HisWork.FID + "_" + this.HisWork.OID + "_" + this.HisNode.NodeID + "_" + func.No;
                        bill.FID = this.HisWork.FID;
                        bill.WorkID = this.HisWork.OID;
                        bill.FK_Node = this.HisNode.NodeID;
                        //  bill.FK_Bill = func.No;
                        bill.FK_Dept = WebUser.FK_Dept;
                        bill.FK_Emp = WebUser.No;
                        bill.Url = billUrl;
                        bill.RDT = DataType.CurrentDataTime;
                        bill.FullPath = path + file;
                        bill.FK_NY = DataType.CurrentYearMonth;
                        bill.FK_Flow = this.HisNode.FK_Flow;
                        bill.FK_BillType = func.FK_BillType;
                        bill.FK_Flow = this.HisNode.FK_Flow;
                        bill.Emps = this.rptGe.GetValStrByKey("Emps");
                        bill.FK_Starter = this.rptGe.GetValStrByKey("Rec");
                        bill.StartDT = this.rptGe.GetValStrByKey("RDT");
                        bill.Title = this.rptGe.GetValStrByKey("Title");
                        bill.FK_Dept = this.rptGe.GetValStrByKey("FK_Dept");
                        try
                        {
                            bill.Insert();
                        }
                        catch
                        {
                            bill.Update();
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        BP.WF.DTS.InitBillDir dir = new BP.WF.DTS.InitBillDir();
                        dir.Do();

                        path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                        string msgErr = "@" + this.ToE("WN5", "���ɵ���ʧ�ܣ����ù���Ա���Ŀ¼����") + "[" + BP.WF.Glo.FlowFileBill + "]��@Err��" + ex.Message + " @File=" + file + " @Path:" + path;
                        billInfo += "@<font color=red>" + msgErr + "</font>";
                        throw new Exception(msgErr + "@������Ϣ:" + ex.Message);
                    }

                } // end ����ѭ�����ݡ�

                if (billInfo != "")
                    billInfo = "@" + billInfo;
                msg += billInfo;
            }
            #endregion

            // this.HisWork.DoCopy(); // copy ���ص����ݵ�ָ����ϵͳ.
            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

            #region ִ�г���.
            if (this.HisNode.HisCCRole == CCRole.AutoCC || this.HisNode.HisCCRole == CCRole.HandAndAuto)
            {
                /*������Զ�����*/
                CC cc = new CC();
                cc.NodeID = this.HisNode.NodeID;
                cc.Retrieve();
                string ccTitle = cc.CCTitle.Clone() as string;
                ccTitle = ccTitle.Replace("@WebUser.No", WebUser.No);
                ccTitle = ccTitle.Replace("@WebUser.Name", WebUser.Name);
                ccTitle = ccTitle.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                ccTitle = ccTitle.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);
                ccTitle = ccTitle.Replace("@RDT", DataType.CurrentData);


                string ccDoc = cc.CCDoc.Clone() as string;
                ccDoc = ccDoc.Replace("@WebUser.No", WebUser.No);
                ccDoc = ccDoc.Replace("@WebUser.Name", WebUser.Name);
                ccDoc = ccDoc.Replace("@RDT", DataType.CurrentData);
                ccDoc = ccDoc.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                ccDoc = ccDoc.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

                foreach (Attr item in this.rptGe.EnMap.Attrs)
                {
                    if (ccDoc.Contains("@" + item.Key) == true)
                        ccDoc = ccDoc.Replace("@" + item.Key, this.rptGe.GetValStrByKey(item.Key));

                    if (ccTitle.Contains("@" + item.Key) == true)
                        ccTitle = ccTitle.Replace("@" + item.Key, this.rptGe.GetValStrByKey(item.Key));
                }

              //  ccDoc += "\t\n ------------------- ";
              //  string msgPK = "SELECT MyPK FROM WF_Track WHERE WorkID="+this.WorkID+" AND NDFrom="+this.HisNode.NodeID+" ORDER BY RDT";

                DataTable ccers = cc.GenerCCers(this.rptGe);
                if (ccers.Rows.Count > 0)
                {
                    msg += "@��Ϣ�Զ����͸�";
                    string basePath = "http://" + System.Web.HttpContext.Current.Request.Url.Host;
                    basePath += "/" + System.Web.HttpContext.Current.Request.ApplicationPath;
                    string mailTemp = BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfDataUser + "\\EmailTemplete\\CC_" + WebUser.SysLang + ".txt");

                    foreach (DataRow dr in ccers.Rows)
                    {
                        ccDoc = ccDoc.Replace("@Accepter", dr[1].ToString());
                        ccTitle = ccTitle.Replace("@Accepter", dr[1].ToString());

                        CCList list = new CCList();
                        list.MyPK = this.WorkID + "_" + this.HisNode.NodeID + "_" + dr[0].ToString();
                        list.FK_Flow = this.HisNode.FK_Flow;
                        list.FlowName = this.HisNode.FlowName;
                        list.FK_Node = this.HisNode.NodeID;
                        list.NodeName = this.HisNode.Name;
                        list.Title = ccTitle;
                        list.Doc = ccDoc;
                        list.CCTo = dr[0].ToString();
                        list.RDT = DataType.CurrentDataTime;
                        list.Rec = WebUser.No;
                        list.RefWorkID = this.WorkID;
                        list.FID = this.HisWork.FID;
                        try
                        {
                            list.Insert();
                        }
                        catch
                        {
                            list.CheckPhysicsTable();
                            list.Update();
                        }
                        msg += list.CCTo + "(" + dr[1].ToString() + ");";
                        BP.WF.Port.WFEmp wfemp = new Port.WFEmp(list.CCTo);


                        string sid = list.CCTo + "_" + list.RefWorkID + "_" + list.FK_Node + "_" + list.RDT;
                        string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                        string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";

                        string mytemp = mailTemp.Clone() as string;
                        mytemp = string.Format(mytemp, wfemp.Name, WebUser.Name, url, urlWap);

                        BP.TA.SMS.AddMsg(list.RefWorkID + "_" + list.FK_Node + "_" + wfemp.No, wfemp.No, wfemp.HisAlertWay, wfemp.Tel,
                   this.ToEP3("WN27", "��������:{0}.����:{1},������:{2},��������",
                   this.HisNode.FlowName, this.HisNode.Name, WebUser.Name),  wfemp.Email, null, mytemp);
                    }
                }
                //    BP.TA.SMS.AddMsg(
                //   this.WorkerListWayOfDept
            }
            #endregion ִ�г���.
            return msg;
        }
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="at">����</param>
        /// <param name="toEmp">����Ա</param>
        /// <param name="toEmpName">����Ա����</param>
        /// <param name="toNDid">���ڵ�</param>
        /// <param name="toNDName">���ڵ�����</param>
        /// <param name="msg">��Ϣ</param>
        public void AddToTrack(ActionType at, string toEmp, string toEmpName, int toNDid, string toNDName, string msg)
        {
            Track t = new Track();
            t.WorkID = this.HisWork.OID;
            t.FID = this.HisWork.FID;
            t.RDT = DataType.CurrentDataTimess;
            t.HisActionType = at;

            t.NDFrom = this.HisNode.NodeID;
            t.NDFromT = this.HisNode.Name;

            t.EmpFrom = WebUser.No;
            t.EmpFromT = WebUser.Name;
            t.FK_Flow = this.HisNode.FK_Flow;

            t.NDTo = toNDid;
            t.NDToT = toNDName;

            t.EmpTo = toEmp;
            t.EmpToT = toEmpName;
            t.Msg = msg;
            if (string.IsNullOrEmpty(msg))
            {
                switch (at)
                {
                    case ActionType.Forward:
                    case ActionType.Start:
                    case ActionType.Undo:
                    case ActionType.ForwardFL:
                    case ActionType.ForwardHL:
                        //�ж��Ƿ��н����ֶΣ�����оͰ�����¼����־�
                        if (this.HisNode.FocusField.Length > 1)
                        {
                            try
                            {
                                t.Msg = this.HisWork.GetValStrByKey(this.HisNode.FocusField);
                            }
                            catch (Exception ex)
                            {
                                Log.DebugWriteError("@�����ֶα�ɾ����" + ex.Message + "@" + this.HisNode.FocusField);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (at == ActionType.Forward)
            {
                if (this.HisNode.IsFL)
                    at = ActionType.ForwardFL;
            }

            try
            {
                t.MyPK = t.WorkID + "_" + t.FID + "_"  + t.NDFrom + "_" + t.NDTo +"_"+t.EmpFrom+"_"+t.EmpTo+"_"+ DateTime.Now.ToString("yyMMddhhmmss");
                t.Insert();
            }
            catch
            {
                t.CheckPhysicsTable();
            }
        }
        /// <summary>
        /// ���빤����¼
        /// </summary>
        /// <param name="gwls"></param>
        public void AddIntoWacthDog(WorkerLists gwls)
        {
            if (BP.SystemConfig.IsBSsystem == false)
                return;

            if (BP.WF.Glo.IsEnableSysMessage  == false)
                return;

            string basePath = "http://" + System.Web.HttpContext.Current.Request.Url.Host;
            basePath += "/" + System.Web.HttpContext.Current.Request.ApplicationPath;
            string mailTemp = BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfDataUser + "\\EmailTemplete\\" + WebUser.SysLang + ".txt");

            foreach (WorkerList wl in gwls)
            {
                if (wl.IsEnable == false)
                    continue;

                string sid = wl.FK_Emp + "_" + wl.WorkID + "_" + wl.FK_Node + "_" + wl.RDT;
                string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";

                //string mytemp ="����" + wl.FK_EmpText + ":  <br><br>&nbsp;&nbsp; "+WebUser.Name+"�����Ĺ�����Ҫ������������<a href='" + url + "'>�򿪹���</a>�� \t\n <br>&nbsp;&nbsp;����򲻿��븴�Ƶ��������ַ���<br>&nbsp;&nbsp;" + url + " <br><br>&nbsp;&nbsp;���ʼ��ɳ۳ҹ������������Զ��������벻Ҫ�ظ���<br>*^_^*  лл ";
                string mytemp = mailTemp.Clone() as string;
                mytemp = string.Format(mytemp, wl.FK_EmpText, WebUser.Name, url, urlWap);

                //ִ����Ϣ���͡�
                BP.WF.Port.WFEmp wfemp = new BP.WF.Port.WFEmp(wl.FK_Emp);
                // wfemp.No = wl.FK_Emp;
                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wfemp.No, wfemp.No, wfemp.HisAlertWay, wfemp.Tel,
                    this.ToEP3("WN27", "����:{0}.����:{1},������:{2},��������",
                    this.HisNode.FlowName, wl.FK_NodeText, WebUser.Name),
                    wfemp.Email, null, mytemp);
            }

            /*
            string workers="";
            // ������
            foreach(WorkerList wl in gwls)
            {
                workers+=","+wl.FK_Emp;
            }
            Watchdog wd =new Watchdog();
            wd.InitDateTime=DataType.CurrentDataTime ;
            wd.WorkID=this.HisWork.OID;
            wd.NodeId =this.HisNode.OID;
            wd.Workers = workers+",";
            wd.FK_Dept =this.HisDeptOfUse.No ;
            wd.FK_Station=this.HisStationOfUse.No;
            wd.Save();
            */
        }
        /// <summary>
        /// ��ǩ�ڵ��Ƿ�ȫ����ɣ�
        /// </summary>
        private bool IsOverMGECheckStand = false;
        private bool IsStopFlow = false;
        /// <summary>
        /// ������̡��ڵ���������
        /// </summary>
        /// <returns></returns>
        private string CheckCompleteCondition()
        {
            this.IsStopFlow = false;
            // ���ü��ȫ�ֵĹ���.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            #region �жϽڵ��������
            try
            {
                // ���û������,��˵����,����Ϊ��ɽڵ����������.
                if (this.HisNode.IsCCNode == false)
                {
                    msg = string.Format(this.ToE("WN6", "��ǰ����[{0}]�Ѿ����"), this.HisNode.Name);
                }
                else
                {
                    int i = this.HisNodeCompleteConditions.Count;
                    if (i == 0)
                    {
                        this.HisNode.IsCCNode = false;
                        this.HisNode.Update();
                    }

                    if (this.HisNodeCompleteConditions.IsPass)
                    {
                        if (SystemConfig.IsDebug)
                            msg = "@��ǰ����[" + this.HisNode.Name + "]�����������[" + this.HisNodeCompleteConditions.ConditionDesc + "],�Ѿ����.";
                        else
                            msg = string.Format(this.ToE("WN6", "��ǰ����{0}�Ѿ����"), this.HisNode.Name);  //"@"; //��ǰ����[" + this.HisNode.Name + "],�Ѿ����.
                    }
                    else
                    {
                        // "@��ǰ����[" + this.HisNode.Name + "]û�����,��һ��������������."
                        throw new Exception(string.Format(this.ToE("WN7", "@��ǰ����{0}û�����,��һ��������������."), this.HisNode.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN8", "@�жϽڵ�{0}����������ִ���.") + ex.Message, this.HisNode.Name)); //"@�жϽڵ�[" + this.HisNode.Name + "]����������ִ���:" + ex.Message;
            }
            #endregion

            #region �ж���������.
            try
            {
                if (this.HisNode.HisToNodes.Count == 0 && this.HisNode.IsStartNode)
                {
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver("");
                    this.IsStopFlow = true;
                    this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                        this.HisNode.NodeID, this.HisNode.Name, "���̽���");
                    return "�����Ѿ��ɹ�����(һ�����̵Ĺ���)�� @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    // return msg + "@���Ϲ��������������" + this.HisFlowCompleteConditions.ConditionDesc + "" + overMsg + " @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                }

                if ((this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass))
                {
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver("");
                    this.IsStopFlow = true;

                    this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                        this.HisNode.NodeID, this.HisNode.Name, "���̽���");
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    return msg + "@���Ϲ��������������" + this.HisFlowCompleteConditions.ConditionDesc + "" + overMsg + " @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@�ж�����{0}����������ִ���.") + ex.Message, this.HisNode.Name));
            }
            #endregion
            return msg;
        }
        /// <summary>
        /// �����µĽڵ�
        /// </summary>
        /// <returns></returns>
        public string StartupNewNodeWork()
        {
            #region ������һ���ڵ�֮ǰ���б�Ҫ���жϡ�
            string msg = this.CheckCompleteCondition();
            if (this.IsStopFlow == true)
            {
                /*�ڼ����������Ѿ�ֹͣ�ˡ�*/
                return msg;
            }

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL:
                case NodeWorkType.WorkFHL:
                    if (this.HisNode.IsForceKill)
                    {
                        // ����Ƿ���û����ɵĽ��̡�
                        WorkerLists wls = new WorkerLists();
                        wls.Retrieve(WorkerListAttr.FID, this.HisWork.OID,
                            WorkerListAttr.IsPass, 0);
                        if (wls.Count > 0)
                        {
                            msg += "@��ʼ���ж�û����ɷ���������ͬ��ǿ��ɾ����";
                            foreach (WorkerList wl in wls)
                            {
                                WorkFlow wf = new WorkFlow(wl.FK_Flow, wl.WorkID, wl.FID);
                                wf.DoDeleteWorkFlowByReal();
                                msg += "@�Ѿ��ѣ�" + wl.FK_Emp + " , " + wl.FK_EmpText + "���Ĺ���ɾ����";
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = DBAccess.RunSQLReturnTable("SELECT b.No,b.Name FROM WF_GenerWorkerList a,Port_Emp b WHERE a.FK_Emp=b.No AND IsPass=0 AND FID=" + this.HisWork.OID);
                        if (dt.Rows.Count != 0)
                        {
                            int i = BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET FID=0 WHERE FID=WorkID AND WorkID=" + this.WorkID);
                            if (i == 1)
                            {
                                //  this.HisWorkFlow.FID = 0;
                            }

                            dt = DBAccess.RunSQLReturnTable("SELECT b.No,b.Name FROM WF_GenerWorkerList a,Port_Emp b WHERE a.FK_Emp=b.No AND a.IsPass=0 AND a.IsEnable=1 AND FID=" + this.HisWork.OID);
                            if (dt.Rows.Count != 0)
                            {
                                msg = "@ִ����ɴ�����������Աû����ɹ�����";
                                foreach (DataRow dr in dt.Rows)
                                {
                                    msg += "@" + dr[0].ToString() + " - " + dr[1].ToString();
                                }
                                msg += "@����ÿ����Ա����ɺ󣬲ſ���ִ�д˲�����";
                                throw new Exception(msg);
                            }
                        }
                    }
                    break;
                case NodeWorkType.Work:
                case NodeWorkType.StartWork:
                    // ����Ľ�����
                    break;
                case NodeWorkType.SubThreadWork:
                    break;
                default:
                    break;
                //throw new Exception("@������ƴ��󣬽����ڵ�����Ͳ���Ϊ:(" + this.HisNode.HisNodeWorkTypeT + ") ����.");
            }
            #endregion

            // ȡ��ǰ�ڵ�Nodes.
            Nodes toNodes = this.HisNode.HisToNodes;
            if (toNodes.Count == 0)
            {
                /* ��������һ���ڵ㣬���������̽�����*/
                string ovrMsg = this.HisWorkFlow.DoFlowOver("��");
                
                this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                    this.HisNode.NodeID, this.HisNode.Name, "���̽���");

                if (this.HisNode.HisFormType == FormType.SDKForm)
                    return ovrMsg + this.ToE("WN0", "@�˹����������е����һ�����ڣ������ɹ�������");
                else
                    return ovrMsg + this.ToE("WN0", "@�˹����������е����һ�����ڣ������ɹ�������") + "<img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "&FK_Node=" + this.HisNode.NodeID + "'target='_blank' >" + this.ToE("WorkRpt", "��������") + "</a>";
            }

            // �������ת�Ľڵ�.
            if (this.JumpToNode != null)
            {
                return msg + StartNextNode(this.JumpToNode);
            }

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return msg + StartNextNode((Node)toNodes[0]);

            Node toNode = null;
            int numOfWay = 0;
            string condMsg = "";
            Conds dcs = new Conds();
            QueryObject qo = new QueryObject(dcs);
            qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
            qo.addOrderBy(CondAttr.PRI);
            qo.DoQuery();
            foreach (Cond cd in dcs)
            {
                cd.WorkID = this.WorkID;
                foreach (Node nd in toNodes)
                {
                    if (cd.ToNodeID != nd.NodeID)
                        continue;
                    if (cd.IsPassed) // ������ת����������һ������.
                    {
                        numOfWay++;
                        toNode = nd;
                        break;
                    }
                    condMsg += "<b>@��鷽�����������ڵ㣺" + nd.Name + "</b>";
                    condMsg += dcs.MsgOfDesc;
                }

                if (toNode != null)
                    break;
            }

            if (toNode == null)
                throw new Exception(string.Format(this.ToE("WN11", "@ת���������ô���,�ڵ�����:{0}, ϵͳ�޷�Ͷ�ݡ�"),
                    this.HisNode.Name));

            /* ɾ����������������ϵ������������ݡ�
             * ����˵�����������������˱仯����ܲ������������ϵ����ݡ���Ϊ�˹������������������������衣 */
            foreach (Node nd in toNodes)
            {
                if (nd.NodeID == toNode.NodeID)
                    continue;

                // ɾ�������������Ϊ������������ݲ��������ˡ�
                Work wk = nd.HisWork;
                wk.OID = this.HisWork.OID;
                if (wk.Delete() != 0)
                {
                    /* ɾ������������Ϣ����ϸ����Ϣ�� */
                    #warning û����
                }
            }
            msg += StartNextNode(toNode);
            return msg;
        }

        #region ������˽ڵ�
        /// <summary>
        /// ��������ڵ�.
        /// </summary>
        /// <param name="nds"></param>
        /// <returns></returns>
        public string StartNextNode(Nodes nds)
        {
            /* ������ */
            foreach (Node nd in nds)
            {
                if (nd.IsHL == true)
                {
                    /* ������е�һ���㵽���˺����㣬��������������㡣*/
                    if (nds.Count != 1)
                    {
                        throw new Exception("@������������̽ڵ㷢��ʱ�����㲻��ͬʱ���������������");
                    }
                    else
                    {
                        return StartNextNode(nd);
                    }
                }
            }

            /*�ֱ�����ÿ���ڵ����Ϣ.*/
            string msg = "";

            //��ѯ������һ���ڵ�ĸ�����Ϣ����
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());

            //��ѯ������һ��Ele��Ϣ����
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());

            foreach (Node nd in nds)
            {
                msg += "@"+nd.Name+"�����Ѿ��������������ߣ�";
                //����һ��������Ϣ��
                Work wk = nd.HisWork;
                wk.Copy(this.HisWork);
                wk.FID = this.HisWork.OID;
                wk.OID = BP.DA.DBAccess.GenerOID();
                wk.NodeState = NodeState.Init;
                wk.BeforeSave();
                wk.DirectInsert();

                if (athDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                            "ND" + nd.NodeID) + "_" + idx;
                        athDB_N.RefPKVal = wk.OID.ToString();
                        athDB_N.Insert();
                    }
                }

                if (eleDBs.Count > 0)
                {
                    /*˵����ǰ�ڵ��и�������*/
                    int idx = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.Insert();
                    }
                }

                //������Ĺ����ߡ�
                WorkNode town = new WorkNode(wk, nd);
                WorkerLists gwls = this.GenerWorkerLists(town);
                foreach (WorkerList wl in gwls)
                {
                    msg += wl.FK_Emp + "��" + wl.FK_EmpText + "��";

                    // ������������Ϣ��
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = wk.OID;
                    if (gwf.IsExits)
                        continue;
                    gwf.FID = this.WorkID;

#warning ��Ҫ�޸ĳɱ������ɹ���

                    gwf.Title = this.GenerTitle(this.HisWork);

                    gwf.WFState = 0;
                    gwf.RDT = DataType.CurrentDataTime;
                    gwf.Rec = Web.WebUser.No;
                    gwf.RecName = Web.WebUser.Name;
                    gwf.FK_Flow = nd.FK_Flow;
                    gwf.FlowName = nd.FlowName;

                    gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;

                    gwf.FK_Node = nd.NodeID;
                    gwf.NodeName = nd.Name;
                    gwf.FK_Dept = wl.FK_Dept;
                    gwf.DeptName = wl.FK_DeptT;
                    gwf.DirectInsert();
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WorkID=" + wk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);
                }
            }
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);
            return msg;
        }
        /// <summary>
        /// ����ָ������һ���ڵ� .
        /// </summary>
        /// <param name="nd">Ҫ�����Ľڵ�</param>
        public string StartNextNode(Node nd)
        {
            /*�����ǰ�ڵ��Ƿ�������һ���ڵ��Ǻ������Ͱ���ͨ�ڵ���������*/
            if (this.HisNode.IsFL && nd.IsHL)
                return StartNextWorkNodeOrdinary(nd);  /* ��ͨ�ڵ� */
            string msg = "";
            try
            {
                switch (nd.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFHL: // �����һ���ڵ��Ǻ����ڵ㣬�����Ƿֺ����ڵ㡣
                        // �жϵ�ǰ�ڵ��������ʲô.
                        switch (this.HisNode.HisNodeWorkType)
                        {
                            case NodeWorkType.Work:
                            case NodeWorkType.SubThreadWork:
                            case NodeWorkType.StartWork: /* �����ǰ������������ */
                                if (this.HisWork.FID == 0)
                                    msg = StartNextWorkNodeHeLiu_WithOutFID(nd);  /* û������ID,����: */
                                else
                                    msg = StartNextWorkNodeHeLiu_WithFID(nd);  /* �����ڵ�, ��ʼ�ڵ��Ƿ����ڵ� */
                                break;
                            default:
                                throw new Exception("@û���жϵ������");
                        }
                        msg += "@<a href='" + this.VirPath + this.AppType+"/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&FID=" + this.HisWork.FID + "&WorkID=" + this.WorkID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��";
                        return msg;
                    case NodeWorkType.StartWork:
                    case NodeWorkType.StartWorkFL:
                        throw new Exception("System Error ");
                        break;
                    case NodeWorkType.SubThreadWork: // �������һ���ڵ������߳�.
                    default:
                        return StartNextWorkNodeOrdinary(nd);  /* ��ͨ�ڵ� */
                }
                // this.InitEmps(nd);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + this.ToE("StartNextNodeErr", "@������һ���ڵ���ִ���") + ":" + ex.Message); //������һ���ڵ���ִ���
            }
        }
        /// <summary>
        /// �������¸�����Ҫ���Ĺ�����
        /// </summary>
        /// <param name="wk">Ҫ�����Ĺ���</param>
        /// <param name="nd">Ҫ�����Ľڵ㡣</param>
        /// <returns>��������Ϣ</returns>
        private string beforeStartNode(Work wk, Node nd)
        {
            // town.
            WorkNode town = new WorkNode(wk, nd);

            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = this.GenerWorkerLists(town);

            //@������Ϣ�����
            this.AddIntoWacthDog(gwls);

            string msg = "";
            msg = this.ToEP3("TaskAutoSendTo", "@�����Զ��´��{0}����{1}λͬ��,{2}.", this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
            if (this._RememberMe.NumOfEmps >= 2)
            {
                if (WebUser.IsWap)
                    msg += "<a href=\"" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "ָ���ض���ͬ�´���") + "</a>��";
                else
                    msg += "<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "ָ���ض���ͬ�´���") + "</a>��";
            }

            msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);

            if (WebUser.IsWap == false)
                msg += "@<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "');\" ><img src='" + this.VirPath + "/WF/Img/SMS.gif' border=0 />" + this.ToE("WN21", "���ֻ�����������(��)") + "</a>";

            if (this.HisNode.HisFormType != FormType.SDKForm)
            {
                if (this.HisNode.IsStartNode)
                    msg += "@<a href='" + this.VirPath   + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>�� <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + nd.FK_Flow + "&FK_Node="+nd.FK_Flow+"01'><img src=" + this.VirPath + "/WF/Img/New.gif border=0/>" + this.ToE("NewFlow", "�½�����") + "</a>��";
                else
                    msg += "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��";
            }

            string str = "";
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ", NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

            if (this.HisNode.HisFormType == FormType.SDKForm)
                return msg;
            else
                return msg + "@" + str + " <img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FID=" + wk.FID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >" + this.ToE("WorkRpt", "��������") + "</a>��";
        }
        /// <summary>
        /// ����Ϊʲô���͸�����
        /// </summary>
        /// <param name="fNodeID"></param>
        /// <param name="toNodeID"></param>
        /// <returns></returns>
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "";
            //return "@<a href='WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >" + this.ToE("WN20", "ΪʲôҪ���͸����ǣ�") + "</a>";
        }
        /// <summary>
        /// ��������ID
        /// </summary>
        public static Int64 FID = 0;
        /// <summary>
        /// û��FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithOutFID(Node nd)
        {
            throw new Exception("δ���:StartNextWorkNodeHeLiu_WithOutFID");
        }
        private string StartNextWorkNodeHeLiu_WithFID_YiBu(Node nd)
        {
            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == nd.NodeID)
            {
                /* ˵�����ǵ�һ�ε�����ڵ�������, 
                 * ���磺һ�����̣�
                 * A����-> B��ͨ-> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ�Ѿ���һ���̵߳����C.
                 */

                /* 
                 * ����:�������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
                 * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
                 */
                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                // ��������������ϸ������.
                this.GenerHieLiuHuiZhongDtlData(nd);

                #region ���������
                Nodes fromNds = nd.HisFromNodes;
                string nearHLNodes = "";
                foreach (Node mynd in fromNds)
                {
                    if (mynd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                        nearHLNodes += "," + mynd.NodeID;
                }
                nearHLNodes = nearHLNodes.Substring(1);

                string sqlOK = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node IN (" + nearHLNodes + ") AND FID=" + this.HisWork.FID + " AND IsPass=1";
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(sqlOK);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];
                decimal ok = (decimal)dt_worker.Rows.Count;

                string sqlAll = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(sqlAll);
                decimal passRate = ok / all * 100;
                 numStr += "@���ǵ�(" + ok + ")����˽ڵ��ϵ�ͬ�£���������(" + all + ")�������̡�";
                if (nd.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.FID);
                    numStr += "@��һ������(" + nd.Name + ")�Ѿ�������";
                }


                #endregion ���������

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);
                return "@�����Ѿ����е������ڵ�[" + nd.Name + "]����ǰ�����Ѿ����.@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr;
            }

            /* �Ѿ���FID��˵������ǰ�Ѿ��з������ߺ����ڵ㡣*/
            /*
             * ���´������û�����̵����λ��
             * ˵���ǵ�һ�ε�����ڵ�������.
             * ���磺һ������:
             * A����-> B��ͨ-> C����
             * ��B ��C ��, B����N ���̣߳���֮ǰ���ǵ�һ������C.
             */

            // �����ҵ��˽ڵ�Ľ�����Ա�ļ��ϡ���Ϊ FID ����������FID��
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = this.GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (WorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "��";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">��";

                if (gwls.Count == 1)
                    emps = fk_emp;
                else
                    emps += "@" + fk_emp;
            }

            /* 
            * �������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
            * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
            */

            // ���ø�����״̬ ���õ�ǰ�Ľڵ�Ϊ:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);

            #region ��������ڵ�����ݡ�
            Work mainWK = town.HisWork;
            mainWK.OID = this.HisWork.FID;
            if (mainWK.RetrieveFromDBSources() == 1)
                mainWK.Delete();

            #region ���Ʊ�����������ݵ���������ȥ��
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID=" + this.HisWork.FID);
            foreach (DataColumn dc in dt.Columns)
                mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.OID = this.HisWork.FID;
            mainWK.Insert();
            #endregion ���Ʊ�����������ݵ���������ȥ��

            #region ���Ƹ�����
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (athDBs.Count > 0)
            {
                /*˵����ǰ�ڵ��и�������*/
                int idx = 0;
                foreach (FrmAttachmentDB athDB in athDBs)
                {
                    idx++;
                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                    athDB_N.Copy(athDB);
                    athDB_N.FK_MapData = "ND" + nd.NodeID;
                    athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + idx;
                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                       "ND" + nd.NodeID);
                    athDB_N.RefPKVal = this.HisWork.FID.ToString();
                    athDB_N.Save();
                }
            }
            #endregion ���Ƹ�����

            #region ����EleDB��
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (eleDBs.Count > 0)
            {
                /*˵����ǰ�ڵ��и�������*/
                int idx = 0;
                foreach (FrmEleDB eleDB in eleDBs)
                {
                    idx++;
                    FrmEleDB eleDB_N = new FrmEleDB();
                    eleDB_N.Copy(eleDB);
                    eleDB_N.FK_MapData = "ND" + nd.NodeID;
                    eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);

                    eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                    eleDB_N.Save();
                }
            }
            #endregion ����EleDB��

            // ��������������ϸ������.
            this.GenerHieLiuHuiZhongDtlData(nd);

            #endregion ��������ڵ������


            /* ��������Ҫ�ȴ�����������ȫ�����������ܿ�������*/
            string info = "";
            string sql1 = "";
#warning ���ڶ���ֺ�������ܻ������⡣
            sql1 = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + this.HisWork.FID + " AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
                info = "@��һ��������("+nd.Name+")�Ѿ�������";
            }
            else
            {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID);
            return "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + "@���ǵ�һ������˽ڵ��ͬ��."+info;
        }
        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="nd"></param>
        private void GenerHieLiuHuiZhongDtlData(Node ndOfHeLiu)
        {
            MapDtls mydtls = ndOfHeLiu.HisWork.HisMapDtls;
            foreach (MapDtl dtl in mydtls)
            {
                if (dtl.IsHLDtl == false)
                    continue;
                GEDtl geDtl = dtl.HisGEDtl;
                geDtl.Copy(this.HisWork);
                geDtl.RefPK = this.HisWork.FID.ToString();
                geDtl.Rec = WebUser.No;
                geDtl.RDT = DataType.CurrentDataTime;
                try
                {
                    geDtl.InsertAsOID(geDtl.OID);
                }
                catch
                {
                    geDtl.Update();
                }
                break;
            }
        }
        /// <summary>
        /// ���߳̽ڵ�
        /// </summary>
        private string _SpanSubTheadNodes = null;
        /// <summary>
        /// ��ȡ���������֮������߳̽ڵ㼯��.
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        private string SpanSubTheadNodes(Node toHLNode)
        {
            _SpanSubTheadNodes = "";
            SpanSubTheadNodes_DG(toHLNode.HisFromNodes);
            if (_SpanSubTheadNodes == "")
                throw new Exception("��ȡ�ֺ���֮������߳̽ڵ㼯�ϳ��ִ�");
            _SpanSubTheadNodes = _SpanSubTheadNodes.Substring(1);
            return _SpanSubTheadNodes;

            //DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_GenerWorkerlist WHERE FID=" + this.HisWork.FID + " AND FK_Node IN (" + _SpanSubTheadNodes + ") ");
            //_SpanSubTheadNodes = "";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    _SpanSubTheadNodes += "," + dr["FK_Node"].ToString();
            //}
            //_SpanSubTheadNodes = _SpanSubTheadNodes.Substring(1);
            //return _SpanSubTheadNodes;
        }
        private void SpanSubTheadNodes_DG(Nodes subNDs)
        {
            foreach (Node nd in subNDs)
            {
                if (nd.IsFL == true)
                    continue;

                if (nd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                    _SpanSubTheadNodes += "," + nd.NodeID;

                SpanSubTheadNodes_DG(nd.HisFromNodes);
            }
        }
        /// <summary>
        /// ��FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithFID(Node nd)
        {
            //�����Ѿ�ͨ��.
            DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID );

            string spanNodes = this.SpanSubTheadNodes(nd);
            if (nd.HisFromNodes.Count != 1)
            {
                return StartNextWorkNodeHeLiu_WithFID_YiBu(nd);
            }

            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == nd.NodeID)
            {
                /* ˵�����ǵ�һ�ε�����ڵ�������, 
                 * ���磺һ�����̣�
                 * A����-> B��ͨ-> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ�Ѿ���һ���̵߳����C.
                 */

                /* 
                 * ����:�������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
                 * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
                 */

                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region ���������

                string sqlOK = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID + " AND IsPass=1";
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(sqlOK);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];
                decimal ok = (decimal)dt_worker.Rows.Count;

                string sql = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE   IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                decimal passRate = ok / all * 100;
                  numStr = "@���ǵ�(" + ok + ")����˽ڵ��ϵ�ͬ�£���������(" + all + ")�������̡�";
                if (nd.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.FID);
                    numStr += "@��һ������(" + nd.Name + ")�Ѿ�������";
                }
                #endregion ���������


                // ��������������ϸ������.
                this.GenerHieLiuHuiZhongDtlData(nd);

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);

                return "@�����Ѿ����е������ڵ�[" + nd.Name + "]����ǰ�����Ѿ����.@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr;
            }

            /* �Ѿ���FID��˵������ǰ�Ѿ��з������ߺ����ڵ㡣*/
            /*
             * ���´������û�����̵����λ��
             * ˵���ǵ�һ�ε�����ڵ�������.
             * ���磺һ������:
             * A����-> B��ͨ-> C����
             * ��B ��C ��, B����N ���̣߳���֮ǰ���ǵ�һ������C.
             */

            // �����ҵ��˽ڵ�Ľ�����Ա�ļ��ϡ���Ϊ FID ����������FID��
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = this.GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (WorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "��";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">��";

                if (gwls.Count == 1)
                    emps = fk_emp;
                else
                    emps += "@" + fk_emp;
            }

            /* 
            * �������Ľڵ� worklist ��Ϣ, ˵����ǰ�ڵ��Ѿ������.
            * ���õ�ǰ�Ĳ���Ա�ܿ����Լ��Ĺ�����
            */

            #region ���ø�����״̬ ���õ�ǰ�Ľڵ�Ϊ:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);

            Work mainWK = town.HisWork;
            mainWK.OID = this.HisWork.FID;
            if (mainWK.RetrieveFromDBSources() == 1)
                mainWK.Delete();

            // ���Ʊ�����������ݵ���������ȥ��
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID=" + this.HisWork.FID);
            foreach (DataColumn dc in dt.Columns)
                mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.OID = this.HisWork.FID;
            mainWK.Insert();

            // ��������������ϸ������.
            this.GenerHieLiuHuiZhongDtlData(nd);

            /*��������ݵĸ��ơ�*/
            #region ���Ƹ�����
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (athDBs.Count > 0)
            {
                /*˵����ǰ�ڵ��и�������*/
                int idx = 0;
                foreach (FrmAttachmentDB athDB in athDBs)
                {
                    idx++;
                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                    athDB_N.Copy(athDB);
                    athDB_N.FK_MapData = "ND" + nd.NodeID;
                    athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + idx;
                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                       "ND" + nd.NodeID);
                    athDB_N.RefPKVal = this.HisWork.FID.ToString();
                    athDB_N.Save();
                }
            }
            #endregion ���Ƹ�����

            #region ����Ele��
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (eleDBs.Count > 0)
            {
                /*˵����ǰ�ڵ��и�������*/
                int idx = 0;
                foreach (FrmEleDB eleDB in eleDBs)
                {
                    idx++;
                    FrmEleDB eleDB_N = new FrmEleDB();
                    eleDB_N.Copy(eleDB);
                    eleDB_N.FK_MapData = "ND" + nd.NodeID;
                    eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                    eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                    eleDB_N.Save();
                }
            }
            #endregion ���Ƹ�����

            /* ��������Ҫ�ȴ�����������ȫ�����������ܿ�������*/

            string sql1 = "";
            // "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
            // string sql1 = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE  IsPass=0 AND FID=" + this.HisWork.FID;

#warning ���ڶ���ֺ�������ܻ������⡣
            sql1 = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }
            else
            {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }

            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID);
            #endregion ���ø�����״̬

            return "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + "@���ǵ�һ������˽ڵ��ͬ��.";
        }
        /// <summary>
        /// ������һ�������ڵ�
        /// </summary>
        /// <param name="nd">�ڵ�</param>		 
        /// <returns></returns>
        private string StartNextWorkNodeOrdinary(Node nd)
        {
            string sql = "";
            try
            {
                #region  ��ʼ������Ĺ����ڵ㡣
                Work wk = nd.HisWork;
                wk.SetValByKey("OID", this.HisWork.OID); //�趨����ID.
                if (this.HisNode.IsStartNode == false)
                    wk.Copy(this.rptGe);

                wk.FID = 0;
                wk.Copy(this.HisWork); // ִ�� copy ��һ���ڵ�����ݡ�
                wk.NodeState = NodeState.Init; //�ڵ�״̬��
                wk.Rec = BP.Web.WebUser.No;
                try
                {
                    //�ж��ǲ���MD5���̣�
                    if (this.HisFlow.IsMD5)
                        wk.SetValByKey("MD5", Glo.GenerMD5(wk));

                    wk.Insert();
                }
                catch (Exception ex)
                {
                    wk.CheckPhysicsTable();
                    try
                    {
                        wk.Update();
                    }
                    catch (Exception ex11)
                    {
                        throw new Exception(ex.Message + " == " + ex11.Message);
                    }
                }

                #region ���Ƹ�����
                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                int idx = 0;
                if (athDBs.Count > 0)
                {
                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + nd.NodeID,
                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.RefPKVal = this.WorkID.ToString();
                        athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                           "ND" + nd.NodeID);
                        athDB_N.Save();
                    }
                }
                #endregion ���Ƹ�����


                #region ����Ele��
                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (eleDBs.Count > 0)
                {
                    eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + nd.NodeID,
                        FrmEleDBAttr.RefPKVal, this.WorkID);

                    /*˵����ǰ�ڵ��и�������*/
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.GenerPKVal();
                        eleDB_N.Save();
                    }
                }
                #endregion ����Ele��

                #region ���ƶ�ѡ����
                M2Ms m2ms = new M2Ms("ND"+this.HisNode.NodeID, this.WorkID);
                if (m2ms.Count >= 1)
                {
                    foreach (M2M item in m2ms)
                    {
                        M2M m2 = new M2M();
                        m2.Copy(item);
                        m2.EnOID = this.WorkID;
                        m2.FK_MapData = m2.FK_MapData.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        m2.InitMyPK();
                        try
                        {
                            m2.DirectInsert();
                        }
                        catch
                        {
                            m2.DirectUpdate();
                        }
                    }
                }
                #endregion

                #region ������ϸ���ݡ�
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + this.HisNode.NodeID);
                if (dtls.Count >= 1)
                {
                    Sys.MapDtls toDtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                    Sys.MapDtls startDtls = null;
                    bool isEnablePass = false;
                    foreach (MapDtl dtl in dtls)
                    {
                        if (dtl.IsEnablePass)
                            isEnablePass = true;
                    }

                    if (isEnablePass)
                        startDtls = new BP.Sys.MapDtls("ND" + int.Parse(nd.FK_Flow) + "01");

                    int i = -1;
                    foreach (Sys.MapDtl dtl in dtls)
                    {
                        i++;
                        if (toDtls.Count <= i)
                            continue;

                        Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];
                        if (dtl.IsEnablePass == true)
                        {
                            /*����������Ƿ���ϸ������ͨ������,������copy�ڵ����ݡ�*/
                            toDtl.IsCopyNDData = true;
                        }

                        if (toDtl.IsCopyNDData == false)
                            continue;

                        //��ȡ��ϸ���ݡ�
                        GEDtls gedtls = new GEDtls(dtl.No);
                        QueryObject qo = null;
                        qo = new QueryObject(gedtls);
                        switch (dtl.DtlOpenType)
                        {
                            case DtlOpenType.ForEmp:
                                qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                //qo.addAnd();
                                //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                                break;
                            case DtlOpenType.ForWorkID:
                                qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                break;
                            case DtlOpenType.ForFID:
                                qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                break;
                        }
                        qo.DoQuery();
                        int unPass = 0;
                        // �Ƿ�������˻��ơ�
                        isEnablePass = dtl.IsEnablePass;
                        if (isEnablePass && this.HisNode.IsStartNode == false)
                            isEnablePass = true;
                        else
                            isEnablePass = false;

                        if (isEnablePass == true)
                        {
                            /*�жϵ�ǰ�ڵ����ϸ�����Ƿ��У�isPass ����ֶΣ����û���׳��쳣��Ϣ��*/
                            if (gedtls.Count != 0)
                            {
                                GEDtl dtl1 = gedtls[0] as GEDtl;
                                if (dtl1.EnMap.Attrs.Contains("IsPass") == false)
                                    isEnablePass = false;
                            }
                        }

                        DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + this.WorkID);
                        foreach (GEDtl gedtl in gedtls)
                        {
                            if (isEnablePass)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass") == false)
                                {
                                    /*û�����ͨ���ľ� continue ���ǣ��������Ѿ�����ͨ����.*/
                                    continue;
                                }
                            }

                            BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                            dtCopy.Copy(gedtl);
                            dtCopy.FK_MapDtl = toDtl.No;
                            dtCopy.RefPK = this.WorkID.ToString();
                            dtCopy.InsertAsOID(dtCopy.OID);

                            #region  ������ϸ���� - ������Ϣ
                            if (toDtl.IsEnableAthM)
                            {
                                /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                                athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                if (athDBs.Count > 0)
                                {
                                    i = 0;
                                    foreach (FrmAttachmentDB athDB in athDBs)
                                    {
                                        i++;
                                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                        athDB_N.Copy(athDB);
                                        athDB_N.FK_MapData = toDtl.No;
                                        athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                            "ND" + nd.NodeID);
                                        athDB_N.RefPKVal = dtCopy.OID.ToString();
                                        athDB_N.DirectInsert();
                                    }
                                }
                            }
                            if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                            {
                                /*���������m2m */
                                m2ms = new M2Ms(dtl.No, gedtl.OID);
                                if (m2ms.Count > 0)
                                {
                                    i = 0;
                                    foreach (M2M m2m in m2ms)
                                    {
                                        i++;
                                        M2M m2m_N = new M2M();
                                        m2m_N.Copy(m2m);
                                        m2m_N.FK_MapData = toDtl.No;
                                        m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                        m2m_N.EnOID = gedtl.OID;
                                        m2m.InitMyPK();
                                        m2m_N.DirectInsert();
                                    }
                                }
                            }
                            #endregion  ������ϸ���� - ������Ϣ

                        }
                        if (isEnablePass)
                        {
                            /* ������������ͨ�����ƣ��Ͱ�δ��˵�����copy����һ���ڵ���ȥ 
                             * 1, �ҵ���Ӧ����ϸ��.
                             * 2, ��δ���ͨ�������ݸ��Ƶ���ʼ��ϸ����.
                             */

                            string startTable = "ND" + int.Parse(nd.FK_Flow) + "01";
                            string startUser = "SELECT Rec FROM " + startTable + " WHERE OID=" + this.WorkID;
                            startUser = DBAccess.RunSQLReturnString(startUser);

                            MapDtl startDtl = (MapDtl)startDtls[i];
                            foreach (GEDtl gedtl in gedtls)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass"))
                                    continue; /* �ų����ͨ���� */

                                BP.Sys.GEDtl dtCopy = new GEDtl(startDtl.No);
                                dtCopy.Copy(gedtl);
                                dtCopy.OID = 0;
                                dtCopy.FK_MapDtl = startDtl.No;
                                dtCopy.RefPK = gedtl.OID.ToString(); //this.WorkID.ToString();
                                dtCopy.SetValByKey("BatchID", this.WorkID);
                                dtCopy.SetValByKey("IsPass", 0);
                                dtCopy.SetValByKey("Rec", startUser);
                                dtCopy.SetValByKey("Checker", BP.Web.WebUser.Name);
                                dtCopy.SaveAsOID(gedtl.OID);
                            }
                            DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + WebUser.No + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + WebUser.No + "'");
                        }
                    }
                }
                #endregion ������ϸ����

                //  wk.CopyCellsData("ND" + this.HisNode.NodeID + "_" + this.HisWork.OID);
                #endregion

                try
                {
                    wk.BeforeSave();
                }
                catch
                {
                }

                try
                {
                    wk.DirectSave();
                }
                catch
                {
                    #region ��������ڵ��ֶ��޸�����
                    try
                    {
                        wk.CheckPhysicsTable();
                        wk.DirectSave();
                    }
                    catch (Exception ex)
                    {
                        Log.DebugWriteInfo(this.ToE("SaveWorkErr", "���湤������") + "��" + ex.Message);
                        throw new Exception(this.ToE("SaveWorkErr", "���湤������  ") + wk.EnDesc + ex.Message);
                    }
                    #endregion ����ֶ��޸�����
                }
                // ����һ�������ڵ�.
                string msg = this.beforeStartNode(wk, nd);
                return "@" + string.Format(this.ToE("NStep", "@��{0}��"), nd.Step.ToString()) + "<font color=blue>" + nd.Name + "</font>" + this.ToE("WorkStartOK", "�����ɹ�����") + "." + msg;
            }
            catch (Exception ex)
            {
                nd.HisWorks.DoDBCheck(DBLevel.Middle);
                throw new Exception(string.Format("StartGEWorkErr", nd.Name) + "@" + ex.Message + sql);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        private Work _HisWork = null;
        /// <summary>
        /// ����
        /// </summary>
        public Work HisWork
        {
            get
            {
                return this._HisWork;
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        private Node _HisNode = null;
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node HisNode
        {
            get
            {
                return this._HisNode;
            }
        }
        private RememberMe _RememberMe = null;
        public RememberMe GetHisRememberMe(Node nd)
        {

            if (_RememberMe == null || _RememberMe.FK_Node != nd.NodeID)
            {
                _RememberMe = new RememberMe();
                _RememberMe.FK_Emp = Web.WebUser.No;
                _RememberMe.FK_Node = nd.NodeID;
                _RememberMe.RetrieveFromDBSources();
            }
            return this._RememberMe;
        }
        private WorkFlow _HisWorkFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public WorkFlow HisWorkFlow
        {
            get
            {
                if (_HisWorkFlow == null)
                    _HisWorkFlow = new WorkFlow(this.HisNode.HisFlow, this.HisWork.OID, this.HisWork.FID);
                return _HisWorkFlow;
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�Ĺ����ǲ�����ɡ�
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (this.HisWork.NodeState == NodeState.Complete)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����һ�������ڵ�����.
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <param name="nodeId">�ڵ�ID</param>
        public WorkNode(Int64 workId, int nodeId)
        {
            this.WorkID = workId;
            Node nd = new Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workId;
            wk.Retrieve();
            this._HisWork = wk;
            this._HisNode = nd;
        }
        /// <summary>
        /// ����һ�������ڵ�����
        /// </summary>
        /// <param name="nd">�ڵ�ID</param>
        /// <param name="wk">����</param>
        public WorkNode(Work wk, Node nd)
        {
            this.WorkID = wk.OID;

            //Node nd  = new Node(ndId);
            // if (nd.HisWorks.GetNewEntity.ToString() != wk.ToString())
            //   throw new Exception("@���������ӵ�ʧ��:����ڵ��" + nd.Name + "�ݲɼ���Ϣ��ŵ�ʵ��[" + nd.WorksEnsName + "]��������ʵ��[" + wk.ToString() + "]��һ�£�");
            this._HisWork = wk;
            this._HisNode = nd;
        }
        #endregion

        #region ��������
        private void Repair()
        {
        }
        public WorkNode GetPreviousWorkNode_FHL(Int64 workid)
        {
            WorkNodes wns = this.GetPreviousWorkNodes_FHL();
            foreach (WorkNode wn in wns)
            {
                if (wn.HisWork.OID == workid)
                    return wn;
            }
            return null;
        }
        public WorkNodes GetPreviousWorkNodes_FHL()
        {
            // ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "�˽ڵ��ǿ�ʼ�ڵ�,û����һ������")); //�˽ڵ��ǿ�ʼ�ڵ�,û����һ������.

            if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkHL
               || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
            {
            }
            else
            {
                throw new Exception("@��ǰ������ - ���Ƿֺ����ڵ㡣");
            }

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                Works wks = (Works)nd.HisWorks;
                wks.Retrieve(WorkAttr.FID, this.HisWork.OID);

                if (wks.Count == 0)
                    continue;

                foreach (Work wk in wks)
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    wns.Add(wn);
                }
            }
            return wns;
        }
        /// <summary>
        /// �õ�������һ������
        /// 1, �ӵ�ǰ���ҵ�������һ�������Ľڵ㼯��.		 
        /// ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
        /// 
        /// </summary>
        /// <returns>�õ�������һ������</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "�˽ڵ��ǿ�ʼ�ڵ�,û����һ������")); //�˽ڵ��ǿ�ʼ�ڵ�,û����һ������.

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL: /* ����Ǻ��� */
                        if (this.IsSubFlowWorkNode == false)
                        {
                            /* ��������߳� */
                            Node pnd = nd.HisPriFLNode;
                            if (pnd == null)
                                throw new Exception("@û��ȡ��������һ����ķ����ڵ㣬��ȷ������Ƿ����");

                            Work wk1 = (Work)pnd.HisWorks.GetNewEntity;
                            wk1.OID = this.HisWork.OID;
                            if (wk1.RetrieveFromDBSources() == 0)
                                continue;
                            WorkNode wn11 = new WorkNode(wk1, pnd);
                            return wn11;
                            break;
                        }
                        break;
                    default:
                        break;
                }

                Work wk = (Work)nd.HisWorks.GetNewEntity;
                wk.OID = this.HisWork.OID;
                if (wk.RetrieveFromDBSources() == 0)
                    continue;

                WorkNode wn = new WorkNode(wk, nd);
                wns.Add(wn);
            }
            switch (wns.Count)
            {
                case 0:
                    throw new Exception(this.ToE("WN15", "û���ҵ�������һ������,ϵͳ������֪ͨ����Ա��������������һ��ͬ�³������͡������ñ����ع���Ա�û���½=�����칤��=�����̲�ѯ=���ڹؼ���������Workid��������ѡ��ȫ������ѯ��������ɾ������") + "@WorkID=" + this.WorkID);
                case 1:
                    return (WorkNode)wns[0];
                default:
                    break;
            }
            Node nd1 = wns[0].HisNode;
            Node nd2 = wns[1].HisNode;
            if (nd1.HisFromNodes.Contains(NodeAttr.NodeID, nd2.NodeID))
            {
                return wns[0];
            }
            else
            {
                return wns[1];
            }
        }
        /// <summary>
        /// �õ�������һ������.
        /// �����ǰ������û�д������״̬,�ͷ��ص�ǰ�Ĺ���.
        /// </summary>
        /// <returns>�õ�������һ������</returns>
        private WorkNode GetNextWorkNode()
        {
            // �����ǰ������û�д������״̬,�ͷ��ص�ǰ�Ĺ���.
            if (this.HisWork.NodeState != NodeState.Complete)
                throw new Exception(this.ToE("WN16", "@�˽ڵ�Ĺ�������û�����,û����һ������.")); //"@�˽ڵ�Ĺ�������û�����,û����һ������."

            // �������һ�������ڵ�
            if (this.HisNode.IsEndNode)
                throw new Exception(this.ToE("ND17", "@�˽ڵ��ǽ����ڵ�,û����һ������.")); // "@�˽ڵ��ǽ����ڵ�,û����һ������."

            // throw new Exception("@��ǰ�Ĺ���û���������,������");
            Nodes nds = this.HisNode.HisToNodes;
            if (nds.Count == 0)
                throw new Exception("@û���ҵ��ӵ�ǰ�ڵ㡾" + this.HisNode.Name + "����ת��ڵ㣬��������һ�����ڵ㣬����ȡ����һ�������ڵ㡣");

            foreach (Node nd in nds)
            {
                Work wk = (Work)nd.HisWorks.GetNewEntity;
                
                wk.OID = this.HisWork.OID;

                if (wk.IsExits == false)
                    continue;

                wk.Retrieve();
                WorkNode wn = new WorkNode(wk, nd);
                return wn;
            }

            if (nds.Count == 1)
            {
                /* ��һ�������,������ļ�¼���Ƿ�ɾ��.
                 * ���Ȱ취��:
                 * 1,�ѵ�ǰ�����̵ĵ�ǰ�����ڵ�����Ϊ��ǰ�Ľڵ�.,
                 * 2,������Ϊ������Ա.
                 * */
                // �ж��ǲ�������һ���Ĺ�����Ա�б�.
                Node nd = (Node)nds[0];
                WorkerLists wls = new WorkerLists(this.HisWork.OID, nd.NodeID);
                if (wls.Count == 0)
                {
                    /*˵��û�в����������б�.*/
                    this.HisWork.NodeState = NodeState.Init;
                    this.HisWork.DirectUpdate();
                    GenerWorkFlow wgf = new GenerWorkFlow(this.HisWork.OID);
                    throw new Exception("@��ǰ�Ĺ���û����ȷ�Ĵ���, û����һ���蹤���ڵ�.Ҳ��������Ƿ�ɾ���˹������еļ�¼,��ɵ�û���ҵ�,��һ����������,����ϵͳ�Ѿ��ָ���ǰ�ڵ�ΪΪ���״̬,�����̿���������������ȥ.");
                }
                else
                {
                    /*˵���Ѿ������������б�.*/
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID);
                }
            }
            throw new Exception("@û���ҵ���һ���蹤���ڵ�[" + this.HisNode.Name + "]����һ������,���̴���.��Ѵ����ⷢ�͸�������Ա��");
        }
        #endregion
    }
    /// <summary>
    /// �����ڵ㼯��.
    /// </summary>
    public class WorkNodes : CollectionBase
    {
        #region ����
        /// <summary>
        /// ���Ĺ���s
        /// </summary> 
        public Works GetWorks
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@��ʼ��ʧ�ܣ�û���ҵ��κνڵ㡣");

                Works ens = this[0].HisNode.HisWorks;
                ens.Clear();

                foreach (WorkNode wn in this)
                {
                    ens.AddEntity(wn.HisWork);
                }
                return ens;
            }
        }
        /// <summary>
        /// �����ڵ㼯��
        /// </summary>
        public WorkNodes()
        {
        }

        public int GenerByFID(Flow flow, Int64 fid)
        {
            this.Clear();

            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisFNType != FNType.River)
                    continue;

                Work wk = nd.GetWork(fid);
                if (wk == null)
                    continue;


                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }

        public int GenerByWorkID(Flow flow, Int64 oid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                //if (nd.HisFNType == FNType.River)
                //    continue;

                Work wk = nd.GetWork(oid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// ɾ����������
        /// </summary>
        public void DeleteWorks()
        {
            foreach (WorkNode wn in this)
            {
                wn.HisWork.Delete();
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ��WorkNode
        /// </summary>
        /// <param name="wn">���� �ڵ�</param>
        public void Add(WorkNode wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkNode this[int index]
        {
            get
            {
                return (WorkNode)this.InnerList[index];
            }
        }
        #endregion
    }
}
