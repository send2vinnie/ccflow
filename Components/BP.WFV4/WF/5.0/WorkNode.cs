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
                    GenerWorkerList wl = new GenerWorkerList();
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
            foreach (GenerWorkerList wl in this.HisWorkerLists)
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
                    _VirPath = System.Web.HttpContext.Current.Request.ApplicationPath;
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
                    {
                        bool b = System.Web.HttpContext.Current.Request.RawUrl.ToLower().Contains("oneflow");
                        if (b)
                            _AppType = "/WF/OneFlow";
                        else
                            _AppType = "/WF";
                    }
                }
                return _AppType;
            }
        }
        private string nextStationName = "";
        private WorkNode town = null;
        public GenerWorkerLists Func_GenerWorkerLists_WidthFID(WorkNode town)
        {
            this.town = town;
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            ps = new Paras();
            ps.SQL = "";
            ps.Add("WorkID", this.HisWork.FID);
            ps.Add("FK_Node", town.HisNode.NodeID);
            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL(ps);

            #region �����ж��Ƿ������˻�ȡ��һ��������Ա��sql.
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
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Exception("@û���ҵ��ɽ��ܵĹ�����Ա��@������Ϣ��ִ�е�sqlû�з�����Ա:" + sql);

                return WorkerListWayOfDept(town, dt);
            }
            #endregion


            // ���ڵ�󶨵���Ա����.
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByBindEmp)
            {
                ps = new Paras();
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.SQL = "SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + dbStr + "FK_Node ";
                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                    throw new Exception("@������ƴ���:��һ���ڵ�(" + town.HisNode.Name + ")û�а󶨹�����Ա . ");
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

            // ����ѡ�����Ա����
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                ps = new Paras();
                ps.Add("FK_Node", this.HisNode.NodeID);
                ps.Add("WorkID", this.WorkID);
                ps.SQL = "SELECT FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                dt = DBAccess.RunSQLReturnTable(ps);
                return WorkerListWayOfDept(town, dt);
            }

            // ���սڵ�ָ������Ա����
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeEmp)
            {
                ps = new Paras();
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.Add("WorkID", this.WorkID);
                ps.SQL = "SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node=" + dbStr + "FK_Node AND IsEnable=1 AND IsPass=1";
                dt = DBAccess.RunSQLReturnTable(ps);

                if (dt.Rows.Count == 0)
                    throw new Exception("@�����õĵ�ǰ�ڵ㰴�սڵ����Ա��������û��Ϊ(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")�ڵ����Ա��");
                return WorkerListWayOfDept(town, dt);
            }

            // ������һ���ڵ��ָ���ֶε���Ա���� 
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousNodeFormEmpsField)
            {
                // ��������Ա����,�Ƿ�������Ҫ��.
                string specEmpFields = town.HisNode.RecipientSQL;
                if (string.IsNullOrEmpty(specEmpFields))
                    specEmpFields = "SysSendEmps";

                if (this.HisWork.EnMap.Attrs.Contains(specEmpFields) == false)
                    throw new Exception("@�����õĵ�ǰ�ڵ㰴��ָ������Ա��������һ���Ľ�����Ա��������û���ڽڵ�������øñ�" + specEmpFields + "�ֶΡ�");

                //��ȡ�����˲���ʽ��������, 
                fk_emp = this.HisWork.GetValStringByKey(specEmpFields);
                fk_emp = fk_emp.Replace(";", ",");
                fk_emp = fk_emp.Replace("��", ",");
                fk_emp = fk_emp.Replace("��", ",");
                fk_emp = fk_emp.Replace("��", ",");
                fk_emp = fk_emp.Replace(" ", "");
                if (string.IsNullOrEmpty(fk_emp))
                    throw new Exception("@û�����ֶ�[" + this.HisWork.EnMap.Attrs.GetAttrByKey(specEmpFields).Desc + "]��ָ�������ˣ������޷����·��͡�");

                // �������������Ա�б���.
                string[] myemps = fk_emp.Split(',');
                foreach (string s in myemps)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;
                    DataRow dr = dt.NewRow();
                    dr[0] = s;
                    dt.Rows.Add(dr);
                }
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
                ps = new Paras();
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.SQL = "SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node)";
                dt = DBAccess.RunSQLReturnTable(ps);
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
                sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node1)";
                sql += ")";
                sql += "AND NO IN ";
                sql += "(";
                sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node2)";
                sql += ")";

                ps = new Paras();
                ps.Add("FK_Node1", town.HisNode.NodeID);
                ps.Add("FK_Node2", town.HisNode.NodeID);
                ps.SQL = sql;
                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count > 0)
                    return WorkerListWayOfDept(town, dt);
                else
                    throw new Exception("������ƴ������֯�ṹά��������:û���ҵ������������λ�Ľ������㡱�Ľڵ���Ա��");
            }

            #region ��ָ���Ľڵ���Ա����Ϊ��һ��������̽����ˡ�
            string empNo = WebUser.No;
            string empDept = WebUser.FK_Dept;
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeEmp)
            {
                /* ��ָ���ڵ��λ�ϵ���Ա���� */
                string fk_node = town.HisNode.RecipientSQL;
                if (DataType.IsNumStr(fk_node) == false)
                    throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ��λͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");
                ps = new Paras();
                ps.Add("OID", this.WorkID);
                ps.SQL = "SELECT Rec FROM ND" + fk_node + " WHERE OID=" + dbStr + "OID ";
                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 1)
                    return WorkerListWayOfDept(town, dt);

                throw new Exception("@������ƴ��󣬵���Ľڵ㣨" + town.HisNode.Name + "����ָ���Ľڵ���û�����ݣ��޷��ҵ���������Ա��");
            }
            #endregion ��ָ���Ľڵ���Ա����Ϊ��һ��������̽����ˡ�

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
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept =" + dbStr + "FK_Dept)";
                ps = new Paras();
                ps.SQL = sql;
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.Add("FK_Dept", empDept);
                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                {
                    //Stations nextStations = ;
                    if (town.HisNode.NodeStations.Count == 0)
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
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept + "%')"
               + " AND No!=" + dbStr + "EmpNo ";

            ps = new Paras();
            ps.SQL = sql;
            ps.Add("FK_Node", town.HisNode.NodeID);
            ps.Add("EmpNo", empNo);
            dt = DBAccess.RunSQLReturnTable(ps);
            if (dt.Rows.Count == 0)
            {
                NodeStations nextStations = town.HisNode.NodeStations;
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
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node) )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - lengthStep) + "%')"
                   + " AND No!='" + empNo + "'";
                ps = new Paras();
                ps.SQL = sql;
                ps.Add("FK_Node", town.HisNode.NodeID);
                ps.Add("EmpNo", empNo);
                dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count == 0)
                {
                    NodeStations nextStations = town.HisNode.NodeStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                    ps = new Paras();
                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (empDept.Length == 2)
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!=" + dbStr + "FK_Emp1 ) ";
                    else
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!=" + dbStr + "FK_Emp2 AND FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - 4) + "%')";

                    ps.SQL = sql;
                    ps.Add("FK_Node", town.HisNode.NodeID);
                    ps.Add("FK_Emp1", empNo);
                    ps.Add("FK_Emp2", empNo);
                    dt = DBAccess.RunSQLReturnTable(ps);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No!='" + empNo + "' AND No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            throw new Exception(this.ToE("WF3", "��λ(" + town.HisNode.HisStationsStr + ")��û����Ա����Ӧ�ڵ�:") + town.HisNode.Name);
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

        public GenerWorkerLists Func_GenerWorkerLists(WorkNode town)
       {
           this.town = town;

           DataTable dt = new DataTable();
           dt.Columns.Add("No", typeof(string));
           string sql;
           string fk_emp;

           // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ��,������Ϊ�˱������
           ps = new Paras();
           ps.Add("WorkID", this.HisWork.OID);
           ps.Add("FK_Node", town.HisNode.NodeID);
           ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node =" + dbStr + "FK_Node";
           DBAccess.RunSQL(ps);

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

               Attrs attrs = town.HisWork.EnMap.Attrs;
               sql = town.HisNode.RecipientSQL;
               Work wk=town.HisWork;
               foreach (Attr attr in attrs)
               {
                   if (attr.MyDataType == DataType.AppString)
                       sql = sql.Replace("@" + attr.Key, "'" + wk.GetValStrByKey(attr.Key) + "'");
                   else
                       sql = sql.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
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
               ps = new Paras();
               ps.Add("FK_Node", this.HisNode.NodeID);
               ps.Add("WorkID", this.HisWork.OID);
               ps.SQL = "SELECT FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 0)
                   throw new Exception("��ѡ����һ���蹤��(" + town.HisNode.Name + ")������Ա��");
               return WorkerListWayOfDept(town, dt);
           }
           // ���ڵ�󶨵���Ա����.
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByBindEmp)
           {
               ps = new Paras();
               ps.Add("FK_Node", town.HisNode.NodeID);
               ps.SQL = "SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + dbStr + "FK_Node ";
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 0)
                   throw new Exception("@������ƴ���:��һ���ڵ�(" + town.HisNode.Name + ")û�а󶨹�����Ա . ");
               return WorkerListWayOfDept(town, dt);
           }

           // ���սڵ�󶨵���Ա����
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeEmp)
           {
               /* ��ָ���ڵ��λ�ϵ���Ա���� */
               string fk_node = town.HisNode.RecipientSQL;
               if (DataType.IsNumStr(fk_node) == false)
                   throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ��λͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");
               ps = new Paras();
               ps.Add("OID", this.WorkID);
               ps.SQL = "SELECT Rec FROM ND" + fk_node + " WHERE OID=" + dbStr + "OID ";
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 1)
                   return WorkerListWayOfDept(town, dt);

               //ps = new Paras();
               //ps.Add("FK_Node", town.HisNode.NodeID);
               //ps.SQL = "SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + dbStr + "FK_Node ";
               //dt = DBAccess.RunSQLReturnTable(ps);
               //if (dt.Rows.Count == 0)
               //    throw new Exception("@�����õĵ�ǰ�ڵ㰴�սڵ����Ա��������û��Ϊ(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")�ڵ����Ա��");

               //fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
               //DataRow dr = dt.NewRow();
               //dr[0] = fk_emp;
               //dt.Rows.Add(dr);
               return WorkerListWayOfDept(town, dt);
           }

           // ������һ���ڵ��ָ���ֶε���Ա���� 
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousNodeFormEmpsField)
           {
               // ��������Ա����,�Ƿ�������Ҫ��.
               string specEmpFields = town.HisNode.RecipientSQL;
               if (string.IsNullOrEmpty(specEmpFields))
                   specEmpFields = "SysSendEmps";

               if (this.HisWork.EnMap.Attrs.Contains(specEmpFields) == false)
                   throw new Exception("@�����õĵ�ǰ�ڵ㰴��ָ������Ա��������һ���Ľ�����Ա��������û���ڽڵ�������øñ�" + specEmpFields + "�ֶΡ�");

               //��ȡ�����˲���ʽ��������, 
               fk_emp = this.HisWork.GetValStringByKey(specEmpFields);
               fk_emp = fk_emp.Replace(";", ",");
               fk_emp = fk_emp.Replace("��", ",");
               fk_emp = fk_emp.Replace("��", ",");
               fk_emp = fk_emp.Replace("��", ",");
               fk_emp = fk_emp.Replace(" ", "");
               if (string.IsNullOrEmpty(fk_emp))
                   throw new Exception("@û�����ֶ�[" + this.HisWork.EnMap.Attrs.GetAttrByKey(specEmpFields).Desc + "]��ָ�������ˣ������޷����·��͡�");

               // �������������Ա�б���.
               string[] myemps = fk_emp.Split(',');
               foreach (string s in myemps)
               {
                   if (string.IsNullOrEmpty(s))
                       continue;
                   DataRow dr = dt.NewRow();
                   dr[0] = s;
                   dt.Rows.Add(dr);
               }
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

           #region ���������λ�Ľ�������.
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDeptAndStation)
           {
               sql = "SELECT No FROM Port_Emp WHERE No IN ";
               sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
               sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node1)";
               sql += ")";
               sql += "AND No IN ";
               sql += "(";
               sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
               sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node1 )";
               sql += ")";

               ps = new Paras();
               ps.Add("FK_Node1", town.HisNode.NodeID);
               ps.Add("FK_Node2", town.HisNode.NodeID);
               ps.SQL = sql;
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count > 0)
                   return WorkerListWayOfDept(town, dt);
               else
                   throw new Exception("@�ڵ���ʹ������:�ڵ�(" + town.HisNode.NodeID + "," + town.HisNode.Name + "), ���ո�λ�벿�ŵĽ���ȷ�������˵ķ�Χ����û���ҵ���Ա:SQL=" + sql);
           }
           #endregion ���������λ�Ľ�������.

           #region �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByDept)
           {
               if (flowAppType == FlowAppType.Normal)
               {
                   ps = new Paras();
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.SQL = "SELECT DISTINCT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node )";
                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count > 0)
                       return WorkerListWayOfDept(town, dt);
               }

               if (flowAppType == FlowAppType.PRJ)
               {
                   sql = "SELECT No FROM Port_Emp WHERE No IN ";
                   sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                   sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node1)";
                   sql += ")";
                   sql += "AND NO IN ";
                   sql += "(";
                   sql += "SELECT FK_Emp FROM Prj_EmpPrjStation WHERE FK_Station IN ";
                   sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node2) AND FK_Prj=" + dbStr + "FK_Prj ";
                   sql += ")";

                   ps = new Paras();
                   ps.Add("FK_Node1", town.HisNode.NodeID);
                   ps.Add("FK_Node2", town.HisNode.NodeID);
                   ps.Add("FK_Prj", prjNo);
                   ps.SQL = sql;

                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                       sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                       sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + dbStr + "FK_Node1)";
                       sql += ")";
                       sql += "AND NO IN ";
                       sql += "(";
                       sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                       sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node2)";
                       sql += ")";

                       ps = new Paras();
                       ps.Add("FK_Node1", town.HisNode.NodeID);
                       ps.Add("FK_Node2", town.HisNode.NodeID);
                       ps.SQL = sql;
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }

                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count > 0)
                       return WorkerListWayOfDept(town, dt);
               }
           }
           #endregion �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���

           #region ��ָ���Ľڵ���Ա����Ϊ��һ��������̽����ˡ�
        
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeEmp)
           {
               /* ��ָ���ڵ��λ�ϵ���Ա���� */
               string fk_node = town.HisNode.RecipientSQL;
               if (DataType.IsNumStr(fk_node) == false)
                   throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ���ԱͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");

               ps = new Paras();
               ps.SQL = "SELECT Rec FROM ND" + fk_node + " WHERE OID=" + dbStr + "OID";
               ps.Add("OID", this.WorkID);
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 1)
                   return WorkerListWayOfDept(town, dt);

               throw new Exception("@������ƴ��󣬵���Ľڵ㣨" + town.HisNode.Name + "����ָ���Ľڵ���û�����ݣ��޷��ҵ���������Ա��");
           }
           #endregion ��ָ���Ľڵ���Ա����Ϊ��һ��������̽����ˡ�

           #region ���ڵ��λ����Ա���ż�������γ�ȼ���.
           if (town.HisNode.HisDeliveryWay == DeliveryWay.ByStationAndEmpDept)
           {
               sql = "SELECT No FROM Port_Emp WHERE NO IN "
                     + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node) )"
                     + " AND  FK_Dept IN "
                     + "(SELECT  FK_Dept  FROM Port_EmpDept WHERE FK_Emp =" + dbStr + "FK_Emp)";

               ps = new Paras();
               ps.Add("FK_Node", town.HisNode.NodeID);
               ps.Add("FK_Emp", WebUser.No);
               ps.SQL = sql;
               //2012.7.16��޸�
               //+" AND  NO IN "
               //+ "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Emp = '" + WebUser.No + "')";
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count > 0)
                   return WorkerListWayOfDept(town, dt);
               else
                   throw new Exception("@�ڵ���ʹ������:�ڵ�(" + town.HisNode.NodeID + "," + town.HisNode.Name + "), ���ڵ��λ����Ա���ż�������γ�ȼ��㣬û���ҵ���Ա:SQL=" + sql);
           }
           #endregion

           string empNo = WebUser.No;
           string empDept = WebUser.FK_Dept;

           #region ��ָ���Ľڵ����Ա��λ����Ϊ��һ��������̽����ˡ�
           if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpecNodeEmpStation)
           {
               /* ��ָ���Ľڵ����Ա��λ */
               string fk_node = town.HisNode.RecipientSQL;
               if (DataType.IsNumStr(fk_node) == false)
                   throw new Exception("������ƴ���:�����õĽڵ�(" + town.HisNode.Name + ")�Ľ��շ�ʽΪ��ָ���Ľڵ���Ա��λͶ�ݣ�������û���ڷ��ʹ������������ýڵ��š�");

               ps = new Paras();
               ps.SQL = "SELECT Rec,FK_Dept FROM ND" + fk_node + " WHERE OID=" + dbStr + "OID";
               ps.Add("OID", this.WorkID);
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count != 1)
                   throw new Exception("@������ƴ��󣬵���Ľڵ㣨" + town.HisNode.Name + "����ָ���Ľڵ���û�����ݣ��޷��ҵ���������Ա��");

               empNo = dt.Rows[0][0].ToString();
               empDept = dt.Rows[0][1].ToString();
           }
           #endregion ��ָ���Ľڵ���Ա����Ϊ��һ��������̽����ˡ�

           #region ����ж� - ���ո�λ��ִ�С�
           if (this.HisNode.IsStartNode == false)
           {
               ps = new Paras();
               if (flowAppType == FlowAppType.Normal)
               {
                   // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                   sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                      + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                      + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                   ps.SQL = sql;
                   ps.Add("WorkID", this.WorkID);
               }

               if (flowAppType == FlowAppType.PRJ)
               {
                   // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                   sql = "SELECT DISTINCT FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Station IN "
                      + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) AND FK_Prj=" + dbStr + "FK_Prj "
                      + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";

                   ps = new Paras();
                   ps.SQL = sql;
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.Add("FK_Prj", prjNo);
                   ps.Add("WorkID", this.WorkID);

                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                       sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                        + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) "
                        + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                       ps = new Paras();
                       ps.SQL = sql;
                       ps.Add("FK_Node", town.HisNode.NodeID);
                       ps.Add("WorkID", this.WorkID);
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }
               }

               dt = DBAccess.RunSQLReturnTable(ps);
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
                      + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node) )"
                      + " AND  NO IN "
                      + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept =" + dbStr + "FK_Dept)";
                   ps = new Paras();
                   ps.SQL = sql;
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.Add("FK_Dept", empDept);
               }
               if (flowAppType == FlowAppType.PRJ)
               {
                   sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj=" + dbStr + "FK_Prj1 AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node)"
                   + " AND  FK_Prj=" + dbStr + "FK_Prj2 ";
                   ps = new Paras();
                   ps.SQL = sql;
                   ps.Add("FK_Prj1", prjNo);
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.Add("FK_Prj2", prjNo);
                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count == 0)
                   {
                       /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ� */
                       sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                     + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node))"
                     + " AND  NO IN "
                     + "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept =" + dbStr + "FK_Dept)";

                       ps = new Paras();
                       ps.SQL = sql;
                       ps.Add("FK_Node", town.HisNode.NodeID);
                       ps.Add("FK_Dept", empDept);
                       dt = DBAccess.RunSQLReturnTable(ps);
                   }
                   else
                   {
                       return WorkerListWayOfDept(town, dt);
                   }
               }

               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 0)
               {
                   NodeStations nextStations = town.HisNode.NodeStations;
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
                  + " AND No!=" + dbStr + "FK_Emp";

               ps = new Paras();
               ps.SQL = sql;
               ps.Add("FK_Emp", empNo);

           }

           if (flowAppType == FlowAppType.PRJ)
           {
               sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj=" + dbStr + "FK_Prj1 AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node )"
                   + " AND  FK_Prj=" + dbStr + "FK_Prj2 ";
               ps = new Paras();
               ps.SQL = sql;
               ps.Add("FK_Prj1", prjNo);
               ps.Add("FK_Node", town.HisNode.NodeID);
               ps.Add("FK_Prj2", prjNo);
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 0)
               {
                   /* �����Ŀ����û�й�����Ա���ύ������������ȥ�ҡ�*/
                   sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node) )"
                  + " AND  NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept + "%')"
                  + " AND No!=" + dbStr + "FK_Emp";
                   ps = new Paras();
                   ps.SQL = sql;
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.Add("FK_Emp", empNo);
               }
               else
               {
                   return WorkerListWayOfDept(town, dt);
               }
           }

           dt = DBAccess.RunSQLReturnTable(ps);
           if (dt.Rows.Count == 0)
           {
               NodeStations nextStations = town.HisNode.NodeStations;
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
                  + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )"
                  + " AND  NO IN "
                  + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - lengthStep) + "%')"
                  + " AND No!=" + dbStr + "FK_Emp";

               ps = new Paras();
               ps.SQL = sql;
               ps.Add("FK_Node", town.HisNode.NodeID);
               ps.Add("FK_Emp", empNo);
               dt = DBAccess.RunSQLReturnTable(ps);
               if (dt.Rows.Count == 0)
               {
                   NodeStations nextStations = town.HisNode.NodeStations;
                   if (nextStations.Count == 0)
                       throw new Exception(this.ToE("WN19", "�ڵ�û�и�λ:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                   sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                   sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )";
                   sql += " AND  No IN ";
                   if (empDept.Length == 2)
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE   FK_Emp!=" + dbStr + "FK_Emp1 ) ";
                   else
                       sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!=" + dbStr + "FK_Emp2 AND FK_Dept LIKE '" + empDept.Substring(0, empDept.Length - 4) + "%')";

                   ps = new Paras();
                   ps.SQL = sql;
                   ps.Add("FK_Node", town.HisNode.NodeID);
                   ps.Add("FK_Emp1", empNo);
                   ps.Add("FK_Emp2", empNo);

                   dt = DBAccess.RunSQLReturnTable(ps);
                   if (dt.Rows.Count == 0)
                   {
                       sql = "SELECT No FROM Port_Emp WHERE No!=" + dbStr + "FK_Emp AND No IN ";
                       sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + dbStr + "FK_Node ) )";
                       ps = new Paras();
                       ps.SQL = sql;
                       ps.Add("FK_Emp", empNo);
                       ps.Add("FK_Node", town.HisNode.NodeID);
                       dt = DBAccess.RunSQLReturnTable(ps);
                       if (dt.Rows.Count == 0)
                       {
                           throw new Exception(this.ToE("WF3", "��λ(" + town.HisNode.HisStationsStr + ")��û����Ա����Ӧ�ڵ�:") + town.HisNode.Name);
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
        string dbStr = SystemConfig.AppCenterDBVarStr;
        public Paras ps = new Paras();
        private WorkNode DoReturnSubFlow(int backtoNodeID, string msg, bool isHiden)
        {
            Node nd = new Node(backtoNodeID);
            ps = new Paras();
            ps.SQL = "DELETE  FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID  AND FID=" + dbStr+"FID";
            ps.Add("FK_Node", backtoNodeID);
            ps.Add("WorkID", this.HisWork.OID);
            ps.Add("FID", this.HisWork.FID);
            BP.DA.DBAccess.RunSQL(ps);

            // �ҳ��ֺ����㴦�����Ա.
            ps = new Paras();
            ps.SQL="SELECT FK_Emp FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr+"FID";
            ps.Add("FID", this.HisWork.FID);
            ps.Add("FK_Node", backtoNodeID);
            DataTable dt = DBAccess.RunSQLReturnTable(ps);
            if (dt.Rows.Count != 1)
                throw new Exception("@ system error , this values must be =1");

            string fk_emp = dt.Rows[0][0].ToString();
            // ��ȡ��ǰ��������Ϣ.
            GenerWorkerList wl = new GenerWorkerList(this.HisWork.FID, this.HisNode.NodeID, fk_emp);
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

            ps = new Paras();
            ps.Add("FK_Node", backtoNodeID);
            ps.Add("WorkID", this.HisWork.OID);
            ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
            BP.DA.DBAccess.RunSQL(ps);

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
                //  WF.Port.WFEmp wfemp = new Port.WFEmp(wn.HisWork.Rec);
                string title = this.ToEP3("WN27", "�����˻أ�����:{0}.����:{1},�˻���:{2},��������",
                      wn.HisNode.FlowName, wn.HisNode.Name, WebUser.Name);

                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wn.HisWork.Rec + DateTime.Now.ToString("HHmmss"), wn.HisWork.Rec,
                                     title + msg, title, msg);
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

            string atPara = "@ToNode=" + backtoNodeID;
            this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.ReturnBefore, this.HisWork, atPara);

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
                    GenerWorkerLists wlsSubs = new GenerWorkerLists();
                    wlsSubs.Retrieve(GenerWorkerListAttr.FID, this.HisWork.OID);
                    foreach (GenerWorkerList sub in wlsSubs)
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
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow  SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE  WorkID=" + dbStr + "WorkID";
            ps.Add("FK_Node", backtoNodeID);
            ps.Add("NodeName", backToNode.Name);
            ps.Add("WorkID", this.WorkID);
            DBAccess.RunSQL(ps);

            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
            ps.Add("FK_Node", backtoNodeID);
            ps.Add("WorkID", this.WorkID);
            DBAccess.RunSQL(ps);

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
            catch (Exception ex)
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
            if (Glo.IsEnableSysMessage == true)
            {
                //   WF.Port.WFEmp wfemp = new Port.WFEmp(wnOfBackTo.HisWork.Rec);
                string title = this.ToEP3("WN27", "�����˻أ�����:{0}.����:{1},�˻���:{2},��������",
                    backToNode.FlowName, backToNode.Name, WebUser.Name);

                BP.TA.SMS.AddMsg(rw.MyPK, wnOfBackTo.HisWork.Rec, title + msg, title, msg);
            }

            //�˻غ��¼�
            this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.ReturnAfter, this.HisWork, atPara);
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
                    GenerWorkerLists wls = new GenerWorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(GenerWorkerListAttr.FID, this.WorkID);
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
                        qo.AddWhere(GenerWorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(GenerWorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (GenerWorkerList wl in wls)
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
                    ps = new Paras();
                    ps.SQL = "DELETE " + dtl.PTable + " WHERE RefPK="+dbStr+"WorkID";
                    ps.Add("WorkID", this.WorkID.ToString());
                    BP.DA.DBAccess.RunSQL(ps);
                }

                // ɾ����������Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE RefPKVal=" + dbStr + "WorkID AND FK_MapData=" + dbStr + "FK_MapData ", 
                    "WorkID", this.WorkID.ToString(), "FK_MapData", "ND" + nd.NodeID);
                // ɾ��ǩ����Ϣ��
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmEleDB WHERE RefPKVal=" + dbStr + "WorkID AND FK_MapData=" + dbStr + "FK_MapData ",
                    "WorkID",this.WorkID.ToString(),"FK_MapData","ND"+nd.NodeID);
                #endregion ɾ����ǰ�ڵ����ݡ�


                /*˵��:�Ѿ�ɾ���ýڵ����ݡ�*/
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE (WorkID=" + dbStr + "WorkID1 OR FID=" + dbStr + "WorkID2 ) AND FK_Node=" + dbStr+"FK_Node",
                    "WorkID1",this.WorkID,"WorkID2",this.WorkID,"FK_Node",nd.NodeID);
                if (nd.IsFL)
                {
                    /* ����Ƿ��� */
                    GenerWorkerLists wls = new GenerWorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(GenerWorkerListAttr.FID, this.WorkID);
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
                        qo.AddWhere(GenerWorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(GenerWorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (GenerWorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);
                        subWK.Delete();

                        //ɾ�������²���Ľڵ���Ϣ.
                        DeleteToNodesData(subNd.HisToNodes);
                    }

                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + dbStr + "WorkID", 
                        "WorkID", this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + dbStr + "WorkID",
                        "WorkID", this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + dbStr + "WorkID",
                        "WorkID", this.WorkID);
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
            GenerWorkerLists wkls = new GenerWorkerLists(this.HisWork.OID, this.HisNode.NodeID);
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
            GenerWorkerLists wls = new GenerWorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
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
            GenerWorkerLists wkls = new GenerWorkerLists(workid, this.HisNode.NodeID);
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

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + dbStr + "FK_Node and WorkID=" + workid,
                "FK_Node",wn.HisNode.NodeID,"WorkID",workid);

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // ��Ϣ֪ͨ��һ��������Ա����
            GenerWorkerLists wls = new GenerWorkerLists(workid, wn.HisNode.NodeID);
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

            if (this.HisNode.IsStartNode)
                this.HisWork.SetValByKey(StartWorkAttr.Title, this.HisGenerWorkFlow.Title);

            this.HisWork.DirectUpdate();

            // ��������Ĺ�����.
            string sql = "";
            sql = "DELETE FROM WF_GenerWorkerlist WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID AND FK_Emp <> " + dbStr + "FK_Emp";
            ps.SQL = sql;
            ps.Clear();
            ps.Add("FK_Node", this.HisNode.NodeID);
            ps.Add("WorkID", this.WorkID);
            ps.Add("FK_Emp", this.HisWork.Rec);
            DBAccess.RunSQL(ps);
            return "";
        }
        

        #region ���ݹ�����λ���ɹ�����
        private GenerWorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            return WorkerListWayOfDept(town,dt,0);
        }
        private GenerWorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt, Int64 fid)
        {
            if (dt.Rows.Count == 0)
            {
                //string msg = "������Ա�б�Ϊ��: ������ƻ�����֯�ṹά������,�ڵ�ķ��ʹ�����("+town.HisNode.RecipientSQL+")";
                throw new Exception("������Ա�б�Ϊ��"); // ������Ա�б�Ϊ��
            }

            Int64 workID = fid;
            if (workID == 0)
                workID = this.HisWork.OID;

            int toNodeId = town.HisNode.NodeID;
            this.HisWorkerLists = new GenerWorkerLists();
            this.HisWorkerLists.Clear();

#warning ����ʱ��  town.HisNode.DeductDays-1

            // 2008-01-22 ֮ǰ�Ķ�����
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , ����Ԥ�����ڵ����⡣

            DateTime dtOfShould;
            if (this.HisFlow.HisTimelineRole == TimelineRole.ByFlow)
            {
                /*������������ǰ��������ü��㡣*/
                dtOfShould = DataType.ParseSysDateTime2DateTime(this.HisGenerWorkFlow.SDTOfFlow);
            }
            else
            {
                int day = 0;
                int hh = 0;
                if (town.HisNode.DeductDays < 1)
                    day = 0;
                else
                    day = int.Parse(town.HisNode.DeductDays.ToString());

                dtOfShould = DataType.AddDays(DateTime.Now, day);
            }

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
               GenerWorkFlowAttr.SDTOfNode, dtOfShould.ToString("yyyy-MM-dd"));
                    break;
            }

            if (dt.Rows.Count == 1)
            {
                /* ���ֻ��һ����Ա */
                GenerWorkerList wl = new GenerWorkerList();
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
                    if (rm.Objs.IndexOf("@" + fk_emp + "@") != -1)
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
                        if (myemps.IndexOf("@" + dr[0].ToString() + ",") != -1)
                            continue;

                        myemps += "@" + dr[0].ToString() + ",";

                        GenerWorkerList wl = new GenerWorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_NodeText = town.HisNode.Name;
                        wl.FK_Emp = dr[0].ToString();

                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("@Ϊ��Ա���乤��ʱ���ִ���:" + wl.FK_Emp + ",û��ִ�гɹ�,�쳣��Ϣ." + ex.Message);
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

                        GenerWorkerList wl = new GenerWorkerList();
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
                foreach (GenerWorkerList wl in this.HisWorkerLists)
                {
                    objsmy += wl.FK_Emp + "@";
                }

                if (rm.Emps != emps || rm.Objs != objsmy)
                {
                    /* ������Ա�б����˱仯 */
                    rm.Emps = emps;
                    rm.Objs = objsmy;

                    string objExts = "";
                    foreach (GenerWorkerList wl in this.HisWorkerLists)
                    {
                        if (Glo.IsShowUserNoOnly)
                            objExts += wl.FK_Emp + "��";
                        else
                            objExts += wl.FK_Emp + "(" + wl.FK_EmpText + ")��";
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
                                empExts += emp.No + "(" + emp.Name + ")��";
                        }
                        else
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += "<strike><font color=red>" + emp.No + "</font></strike>��";
                            else
                                empExts += "<strike><font color=red>" + emp.No + "(" + emp.Name + ")</font></strike>��";
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
                GenerWorkerList wl = this.HisWorkerLists[0] as GenerWorkerList;
                this.AddToTrack(at, wl.FK_Emp, wl.FK_EmpText, wl.FK_Node, wl.FK_NodeText, null);
            }
            else
            {
                string info = "��(" + this.HisWorkerLists.Count+ ")�˽���\t\n";
                foreach (GenerWorkerList wl in this.HisWorkerLists)
                {
                    info += wl.FK_Emp + "," + wl.FK_EmpText + "\t\n";
                }
                this.AddToTrack(at, WebUser.No, WebUser.Name, town.HisNode.NodeID, town.HisNode.Name, info);
            }

            #region �����ݼ��������.
            string ids = "";
            string names = "";
            string idNames = "";
            if (this.HisWorkerLists.Count == 1)
            {
                GenerWorkerList gwl = (GenerWorkerList)this.HisWorkerLists[0];
                ids = gwl.FK_Emp;
                names = gwl.FK_EmpText;
                idNames = gwl.FK_Emp + "," + gwl.FK_EmpText;
            }
            else
            {
                foreach (GenerWorkerList gwl in this.HisWorkerLists)
                {
                    ids += gwl.FK_Emp + ",";
                    names += gwl.FK_EmpText + ",";
                    idNames += gwl.FK_Emp + " " + gwl.FK_EmpText + ",";
                }
            }

            this.addMsg(SendReturnMsgFlag.VarAcceptersID, ids, ids, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersName, names, names, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarAcceptersNID, idNames, idNames, SendReturnMsgType.SystemMsg);
            #endregion

            return this.HisWorkerLists;
        }
        #endregion

        #region ����

       

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
        private Node JumpToNode = null;
        private string JumpToEmp = null;
        
        #region NodeSend �ĸ�������.
        /// <summary>
        /// ��ȡ��һ����Ĺ����ڵ�
        /// </summary>
        /// <returns></returns>
        public Node NodeSend_GenerNextStepNode()
        {
            if (this.JumpToNode != null)
                return this.JumpToNode;

            Nodes nds = this.HisNode.HisToNodes;
            if (nds.Count == 1)
                return (Node)nds[0];

            if (nds.Count == 0)
                throw new Exception("û���ҵ��������˲��ڵ�.");

            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, this.HisNode.NodeID, CondAttr.PRI);
            if (dcsAll.Count == 0)
                throw new Exception("@û��Ϊ�ڵ�("+this.HisNode.NodeID+" , "+this.HisNode.Name+")���÷�������");

            #region ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.
            Nodes myNodes = new Nodes();
            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in nds)
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
                    throw new Exception("@������ƴ��󣺴ӽڵ�("+this.HisNode.Name+")���ڵ�("+nd.Name+")��û�����÷����������з�֧�Ľڵ�����з���������");
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
            
            // ���û���ҵ�.
            if (myNodes.Count == 0)
                throw new Exception("@����ڵ�ķ�����������:û�и���{"+this.HisNode.NodeID + this.HisNode.Name+"}�ڵ㵽�����ڵ�,����ת������.");

            //����ҵ�1��.
            if (myNodes.Count ==1)
                return myNodes[0] as Node;

            
            //����ҵ��˶��.
            foreach (Cond dc in dcsAll)
            {
                foreach (Node myND in myNodes)
                {
                    if (dc.ToNodeID == myND.NodeID)
                        return myND;
                }
            }

            throw new Exception("@��Ӧ�ó��ֵ��쳣,��Ӧ�����е�����.");
        }
        /// <summary>
        /// ��ȡ��һ����Ľڵ㼯��
        /// </summary>
        /// <returns></returns>
        public Nodes Func_GenerNextStepNodes()
        {
            Nodes toNodes = this.HisNode.HisToNodes;

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return toNodes;
            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, this.HisNode.NodeID, CondAttr.PRI);

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
                    myNodes.AddEntity(nd);
                    continue;
                }

                if (dcs.IsPass) // ������ת����������һ������.
                {
                    myNodes.AddEntity(nd);
                    continue;
                }
            }
            #endregion ��ȡ�ܹ�ͨ���Ľڵ㼯�ϣ����û�����÷���������Ĭ��ͨ��.

            if (myNodes.Count == 0)
                throw new Exception(string.Format("@����ڵ�ķ�����������:û�и���{0}�ڵ㵽�����ڵ�,����ת������.",
                    this.HisNode.NodeID + this.HisNode.Name));

            return myNodes;
        }
        /// <summary>
        /// ���һ�������������.
        /// </summary>
        /// <returns></returns>
        private void Func_CheckCompleteCondition()
        {
            if (this.HisNode.HisRunModel == RunModel.SubThread)
                throw new Exception("@������ƴ��󣺲����������߳��������������������");

            this.IsStopFlow = false;

            #region �жϽڵ��������
            try
            {
                // ���û������,��˵����,����Ϊ��ɽڵ����������.
                if (this.HisNode.IsCCNode == false)
                {

                    this.addMsg("CurrWorkOver",string.Format("��ǰ����[{0}]�Ѿ����", this.HisNode.Name));
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
                            this.addMsg(SendReturnMsgFlag.FlowOverByCond,"@��ǰ����[" + this.HisNode.Name + "]�����������[" + this.HisNodeCompleteConditions.ConditionDesc + "],�Ѿ����.");
                        else
                            this.addMsg(SendReturnMsgFlag.FlowOver, string.Format("��ǰ����{0}�Ѿ����", this.HisNode.Name));
                    }
                    else
                    {
                        // "@��ǰ����[" + this.HisNode.Name + "]û�����,��һ��������������."
                        throw new Exception(string.Format("@��ǰ����{0}û�����,��һ��������������.", this.HisNode.Name));
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
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������");
                    this.IsStopFlow = true;
                    this.addMsg("OneNodeFlowOver","@�����Ѿ��ɹ�����(һ�����̵Ĺ���)��");
                    //msg+="@�����Ѿ��ɹ�����(һ�����̵Ĺ���)�� @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                }

                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    string stopMsg = this.HisFlowCompleteConditions.ConditionDesc;
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������:" + stopMsg);
                    this.IsStopFlow = true;

                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    string mymsg = "@���Ϲ��������������" + stopMsg + "" + overMsg;
                    string mymsgHtml=mymsg+"@�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                    this.addMsg(SendReturnMsgFlag.FlowOver, mymsg, mymsgHtml, SendReturnMsgType.Info);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@�ж�����{0}����������ִ���.") + ex.Message, this.HisNode.Name));
            }
            #endregion
        }
        private string Func_DoSetThisWorkOver()
        {
            this.HisWork.AfterWorkNodeComplete();

            // ����״̬��
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
            this.HisWork.Rec = Web.WebUser.No;

            //�ж��ǲ���MD5���̣�
            if (this.HisFlow.IsMD5)
                this.HisWork.SetValByKey("MD5", Glo.GenerMD5(this.HisWork));

            if (this.HisNode.IsStartNode)
                this.HisWork.SetValByKey(StartWorkAttr.Title, this.HisGenerWorkFlow.Title);

            this.HisWork.DirectUpdate();

            // ��������Ĺ�����.
            string sql = "";
            sql = "DELETE FROM WF_GenerWorkerlist WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID AND FK_Emp <> " + dbStr + "FK_Emp";
            ps.SQL = sql;
            ps.Clear();
            ps.Add("FK_Node", this.HisNode.NodeID);
            ps.Add("WorkID", this.WorkID);
            ps.Add("FK_Emp", this.HisWork.Rec);
            DBAccess.RunSQL(ps);


            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
            ps.Add("FK_Node", this.HisNode.NodeID);
            ps.Add("WorkID", this.WorkID);
            DBAccess.RunSQL(ps);

            return "@�����Ѿ����.";
        }
        #endregion ��������
        /// <summary>
        /// ��ͨ�ڵ㵽��ͨ�ڵ�
        /// </summary>
        /// <param name="toND">Ҫ�������һ���ڵ�</param>
        /// <returns>ִ����Ϣ</returns>
        private void NodeSend_11(Node toND)
        {
            string sql = "";
            try
            {
                #region  ��ʼ������Ĺ����ڵ㡣

                #region ��������copy.
                Work toWK = toND.HisWork;
                toWK.SetValByKey("OID", this.HisWork.OID); //�趨����ID.
                if (this.HisNode.IsStartNode == false)
                    toWK.Copy(this.rptGe);

                //toWK.FID = 0;
                toWK.Copy(this.HisWork); // ִ�� copy ��һ���ڵ�����ݡ�
                toWK.NodeState = NodeState.Init; //�ڵ�״̬��
                toWK.Rec = BP.Web.WebUser.No;
                try
                {
                    //�ж��ǲ���MD5���̣�
                    if (this.HisFlow.IsMD5)
                        toWK.SetValByKey("MD5", Glo.GenerMD5(toWK));
                    toWK.Insert();
                }
                catch (Exception ex)
                {
                    toWK.CheckPhysicsTable();
                    try
                    {
                        toWK.Copy(this.HisWork); // ִ�� copy ��һ���ڵ�����ݡ�
                        toWK.NodeState = NodeState.Init; //�ڵ�״̬��
                        toWK.Rec = BP.Web.WebUser.No;
                        toWK.SaveAsOID(toWK.OID);
                    }
                    catch (Exception ex11)
                    {
                        if (toWK.Update() == 0)
                            throw new Exception(ex.Message + " == " + ex11.Message);
                        //  throw new Exception(ex.Message + " == " + ex11.Message);
                    }
                }
                #endregion ��������copy.

                #region ���Ƹ���
                if (this.HisNode.MapData.FrmAttachments.Count > 0)
                {
                    FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                          this.WorkID.ToString());
                    int idx = 0;
                    if (athDBs.Count > 0)
                    {
                        athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toND.NodeID,
                            FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                        /*˵����ǰ�ڵ��и�������*/
                        foreach (FrmAttachmentDB athDB in athDBs)
                        {
                            idx++;
                            FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                            athDB_N.Copy(athDB);
                            athDB_N.FK_MapData = "ND" + toND.NodeID;
                            athDB_N.RefPKVal = this.WorkID.ToString();
                            athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                            athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                               "ND" + toND.NodeID);
                            athDB_N.Save();
                        }
                    }
                }
                #endregion ���Ƹ�����

                #region ����Ele
                if (this.HisNode.MapData.FrmEles.Count > 0)
                {
                    FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                          this.WorkID.ToString());
                    if (eleDBs.Count > 0)
                    {
                        eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + toND.NodeID,
                            FrmEleDBAttr.RefPKVal, this.WorkID);

                        /*˵����ǰ�ڵ��и�������*/
                        foreach (FrmEleDB eleDB in eleDBs)
                        {
                            FrmEleDB eleDB_N = new FrmEleDB();
                            eleDB_N.Copy(eleDB);
                            eleDB_N.FK_MapData = "ND" + toND.NodeID;
                            eleDB_N.GenerPKVal();
                            eleDB_N.Save();
                        }
                    }
                }
                #endregion ����Ele

                #region ���ƶ�ѡ����
                if (this.HisNode.MapData.MapM2Ms.Count > 0)
                {
                    M2Ms m2ms = new M2Ms("ND" + this.HisNode.NodeID, this.WorkID);
                    if (m2ms.Count >= 1)
                    {
                        foreach (M2M item in m2ms)
                        {
                            M2M m2 = new M2M();
                            m2.Copy(item);
                            m2.EnOID = this.WorkID;
                            m2.FK_MapData = m2.FK_MapData.Replace("ND" + this.HisNode.NodeID, "ND" + toND.NodeID);
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
                }
                #endregion

                #region ������ϸ����
               // int deBugDtlCount=
                Sys.MapDtls dtls = this.HisNode.MapData.MapDtls;
                string recDtlLog = "@��¼������ϸ��Copy����,�ӽڵ�ID:"+this.HisNode.NodeID+" WorkID:"+this.WorkID+", ���ڵ�ID="+toND.NodeID;
                if (dtls.Count > 0)
                {
                    Sys.MapDtls toDtls = toND.MapData.MapDtls;
                    recDtlLog += "@���ڵ���ϸ��������:" + dtls.Count + "��";

                    Sys.MapDtls startDtls = null;
                    bool isEnablePass = false; /*�Ƿ�����ϸ�������.*/
                    foreach (MapDtl dtl in dtls)
                    {
                        if (dtl.IsEnablePass)
                            isEnablePass = true;
                    }

                    if (isEnablePass) /* ����оͽ�������ʼ�ڵ������ */
                        startDtls = new BP.Sys.MapDtls("ND" + int.Parse(toND.FK_Flow) + "01");

                    recDtlLog += "@����ѭ����ʼִ�������ϸ��copy.";
                    int i = -1;
                    foreach (Sys.MapDtl dtl in dtls)
                    {
                        recDtlLog += "@����ѭ����ʼִ����ϸ��("+dtl.No+")copy.";

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
                                break;
                            case DtlOpenType.ForWorkID:
                                qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                break;
                            case DtlOpenType.ForFID:
                                qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                break;
                        }
                        qo.DoQuery();

                        recDtlLog += "@��ѯ��������ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";

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

                        recDtlLog += "@ɾ��������ϸ��:" + dtl.No + ",����, ����ʼ������ϸ��,ִ��һ���е�copy.";
                        DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + dbStr + "RefPK", "RefPK", this.WorkID.ToString());

                        // copy����.
                        int deBugNumCopy = 0;
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
                            dtCopy.RefPKInt64 = this.WorkID;
                            deBugNumCopy++;

                            #region  ������ϸ���� - ������Ϣ
                            if (toDtl.IsEnableAthM)
                            {
                                /*��������˶฽��,�͸���������ϸ���ݵĸ�����Ϣ��*/
                                FrmAttachmentDBs athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
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
                                            "ND" + toND.NodeID);
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
                                        m2m.InitMyPK();
                                        m2m_N.DirectInsert();
                                    }
                                }
                            }
                            #endregion  ������ϸ���� - ������Ϣ

                        }
#warning ��¼��־.
                        if (gedtls.Count != deBugNumCopy)
                        {
                            recDtlLog += "@����ϸ��:" + dtl.No + ",��ϸ����:" + gedtls.Count + "��.";
                            //��¼��־.
                            Log.DefaultLogWriteLineInfo(recDtlLog);
                            throw new Exception("@ϵͳ���ִ����뽫������Ϣ����������Ա,лл��: ������Ϣ:" + recDtlLog);
                        }

                        #region �����������˻���
                        if (isEnablePass)
                        {
                            /* ������������ͨ�����ƣ��Ͱ�δ��˵�����copy����һ���ڵ���ȥ 
                             * 1, �ҵ���Ӧ����ϸ��.
                             * 2, ��δ���ͨ�������ݸ��Ƶ���ʼ��ϸ����.
                             */

                            string startTable = "ND" + int.Parse(toND.FK_Flow) + "01";
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
                                dtCopy.RefPKInt64 = this.WorkID;
                                dtCopy.SaveAsOID(gedtl.OID);
                            }
                            DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + WebUser.No + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + WebUser.No + "'");
                        }
                        #endregion �����������˻���
                    }
                }
                #endregion ������ϸ����

                #endregion ��ʼ������Ĺ����ڵ�.

                #region ����Ŀ��ڵ�����.
                try
                {
                    toWK.DirectSave();
                }
                catch
                {
                    try
                    {
                        toWK.CheckPhysicsTable();
                        toWK.DirectSave();
                    }
                    catch (Exception ex)
                    {
                        Log.DefaultLogWriteLineInfo("@���湤������" + ex.Message);
                        throw new Exception("@���湤������" + toWK.EnDesc + ex.Message);
                    }
                }
                #endregion ����Ŀ��ڵ�����.

                #region ִ�����ݳ�ʼ��
                // town.
                WorkNode town = new WorkNode(toWK, toND);

                // ���Ի����ǵĹ�����Ա��
                GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);

                //@������Ϣ�����
                this.AddIntoWacthDog(gwls);

                this.addMsg(SendReturnMsgFlag.ToEmps, this.ToEP3("TaskAutoSendTo", "@�����Զ��´��{0}����{1}λͬ��,{2}.", this.nextStationName,
                    this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt));

                if (this._RememberMe.NumOfEmps >= 2 && this.HisNode.IsTask)
                {
                    if (WebUser.IsWap)
                        this.addMsg(SendReturnMsgFlag.ToEmps, "<a href=\"" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + toND.NodeID + "&FK_Flow=" + toND.FK_Flow + "')\"><img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0/>ָ���ض���ͬ�´���</a>��");
                    else
                        this.addMsg(SendReturnMsgFlag.ToEmps, "<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + toND.NodeID + "&FK_Flow=" + toND.FK_Flow + "')\"><img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0/>ָ���ض���ͬ�´���</a>��");
                }

                if (WebUser.IsWap == false)
                    this.addMsg(SendReturnMsgFlag.ToEmps, "@<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + toND.NodeID + "');\" ><img src='" + this.VirPath + "/WF/Img/SMS.gif' border=0 />" + this.ToE("WN21", "���ֻ�����������(��)") + "</a>");

                if (this.HisNode.HisFormType != FormType.SDKForm)
                {
                    if (this.HisNode.IsStartNode)
                        this.addMsg(SendReturnMsgFlag.ToEmps, "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + toWK.OID + "&FK_Flow=" + toND.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>�� <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + toND.FK_Flow + "&FK_Node=" + toND.FK_Flow + "01'><img src=" + this.VirPath + "/WF/Img/New.gif border=0/>" + this.ToE("NewFlow", "�½�����") + "</a>��");
                    else
                        this.addMsg(SendReturnMsgFlag.ToEmps, "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + toWK.OID + "&FK_Flow=" + toND.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��");
                }

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", toND.NodeID);
                ps.Add("NodeName", toND.Name);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);

                if (this.HisNode.HisFormType == FormType.SDKForm || this.HisNode.HisFormType == FormType.SelfForm)
                {
                }
                else
                {
                    this.addMsg(SendReturnMsgFlag.WorkRpt, null, "@<img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + toWK.OID + "&FID=" + toWK.FID + "&FK_Flow=" + toND.FK_Flow + "'target='_blank' >��������</a>��");
                }
                #endregion

                //
                this.addMsg(SendReturnMsgFlag.WorkStartNode, "@" + string.Format("@��{0}��", toND.Step.ToString()) + "<font color=blue>" + toND.Name + "</font>�����ɹ�����" + ".");

                //����ϵͳ����.
                this.addMsg(SendReturnMsgFlag.VarToNodeID, this.HisNode.NodeID.ToString(), this.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                this.addMsg(SendReturnMsgFlag.VarToNodeName, this.HisNode.Name, this.HisNode.Name, SendReturnMsgType.SystemMsg);
            }
            catch (Exception ex)
            {
                toND.HisWorks.DoDBCheck(DBLevel.Middle);
                throw new Exception(string.Format("StartGEWorkErr", toND.Name) + "@" + ex.Message);
            }
        }
        private void NodeSend_2X_GenerFH()
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
        }
        /// <summary>
        /// ������������·��� to ���.
        /// </summary>
        /// <returns></returns>
        private void NodeSend_24_UnSameSheet(Nodes toNDs)
        {
            NodeSend_2X_GenerFH();
            /*�ֱ�����ÿ���ڵ����Ϣ.*/
            string msg = "";

            //��ѯ������һ���ڵ�ĸ�����Ϣ����
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());
            //��ѯ������һ��Ele��Ϣ����
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());

            foreach (Node nd in toNDs)
            {
                msg += "@" + nd.Name + "�����Ѿ��������������ߣ�";
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
                GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);
                foreach (GenerWorkerList wl in gwls)
                {
                    msg += wl.FK_Emp + "��" + wl.FK_EmpText + "��";
                    // ������������Ϣ��
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = wk.OID;
                    if (gwf.IsExits)
                        continue;

                    gwf.FID = this.WorkID;

#warning ��Ҫ�޸ĳɱ������ɹ���

#warning �������̵�Titlte�븸���̵�һ��.
                    gwf.Title = this.HisGenerWorkFlow.Title; // WorkNode.GenerTitle(this.rptGe);

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
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerlist SET WorkID=" + dbStr + "WorkID1,FID=" + dbStr + "FID WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr + "WorkID2 AND FK_Node=" + dbStr + "FK_Node ";
                    ps.Add("WorkID1", wk.OID);
                    ps.Add("FID", this.WorkID);
                    ps.Add("FK_Emp", wl.FK_Emp);
                    ps.Add("WorkID2", this.WorkID);
                    ps.Add("FK_Node", nd.NodeID);
                    DBAccess.RunSQL(ps);
                }
            }
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            this.addMsg("FenLiuUnSameSheet", msg);
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="toWN"></param>
        /// <returns></returns>
        private GenerWorkerLists NodeSend_24_SameSheet_GenerWorkerList(WorkNode toWN)
        {
            return null;
        }
        /// <summary>
        /// ������������·��� to ͬ��.
        /// </summary>
        /// <param name="toNode">����ķ����ڵ�</param>
        private void NodeSend_24_SameSheet(Node toNode)
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

            #region ������һ����Ĺ�����Ա
            // �����ݸ���һ�����̹߳�����ֵ����Ȼ�ڻ�ȡִ�л�ȡ������Ա�б���ִ�в���ʱ���ܵõ���Ӧ�Ĳ�����
            Work wk = toNode.HisWork;
            wk.Copy(this.rptGe);
            wk.Copy(this.HisWork);  //���ƹ�����Ϣ��
            if (wk.FID == 0)
                wk.FID = this.HisWork.OID;

            WorkNode town = new WorkNode(wk, toNode);

            // ������һ����Ҫִ�е���Ա.
            GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);

            //�����ǰ�����ݣ��������η��͡�
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            // �жϷ����Ĵ���.�ǲ�����ʷ��¼�����з�����
            bool IsHaveFH = false;
            ps = new Paras();
            ps.SQL = "SELECT COUNT(*) FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "OID";
            ps.Add("OID", this.HisWork.OID);
            if (DBAccess.RunSQLReturnValInt(ps) != 0)
                IsHaveFH = true;
            #endregion ������һ����Ĺ�����Ա

            #region ��������.
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
                    foreach (GenerWorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.rptGe);
                        mywk.Copy(this.HisWork);  //���ƹ�����Ϣ��
                     

                        bool isHaveEmp = false;
                        if (IsHaveFH)
                        {
                            /* ��������߹��������������ҵ�ͬһ����Աͬһ��FID�µ�OID �����⵱ǰ�̵߳�ID��*/
                            ps = new Paras();
                            ps.SQL = "SELECT WorkID,FK_Node FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "FID AND FK_Emp=" + dbStr + "FK_Emp ORDER BY RDT DESC";
                            ps.Add("FID", this.WorkID);
                            ps.Add("FK_Emp", wl.FK_Emp);
                            DataTable dt = DBAccess.RunSQLReturnTable(ps);
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
                        mywk.Rec = wl.FK_Emp;
                        mywk.Emps = wl.FK_Emp;

                        if (this.HisWork.FID == 0)
                            mywk.FID = this.HisWork.OID;

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

                        #region  ���ƴӱ���Ϣ.
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
                                DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + dbStr + "RefPK", "RefPK", mywk.OID);
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

                                    #region  ���ƴӱ��� - ������Ϣ - M2M- M2MM
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
                                    #endregion  ���ƴӱ��� - ������Ϣ

                                }
                            }
                        }
                        #endregion  ���Ƹ�����Ϣ

                        // ������������Ϣ��
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;
#warning �������̵�title �븸����һ��.
                        gwf.Title = this.HisGenerWorkFlow.Title; //WorkNode.GenerTitle(this.HisWork);

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
                        ps = new Paras();
                        ps.SQL = "UPDATE WF_GenerWorkerlist SET WorkID=" + dbStr + "WorkID1, FID=" + dbStr + "FID WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr + "WorkID2 AND FK_Node=" + dbStr + "FK_Node ";
                        ps.Add("WorkID1", mywk.OID);
                        ps.Add("FID", this.WorkID);
                        ps.Add("FK_Emp", wl.FK_Emp);
                        ps.Add("WorkID2", this.WorkID);
                        ps.Add("FK_Node", toNode.NodeID);

                        DBAccess.RunSQL(ps);
                    }
                    break;
                default:
                    throw new Exception("û�д�������ͣ�" + this.HisNode.HisFLRole.ToString());
            }
            #endregion ��������.

            #region ������Ϣ��ʾ
            string info = "@�����ڵ�:{0}�Ѿ�����@�����Զ��´��{1}����{2}λͬ��,{3}.";
            this.addMsg("FenLiuInfo", string.Format(info, toNode.Name,
                this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(),
                this._RememberMe.EmpsExt));

            // ����ǿ�ʼ�ڵ㣬�Ϳ�������ѡ������ˡ�
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2 && this.HisNode.IsTask)
                    this.addMsg("AllotTask", "@<img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0 /><a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "')\" >�޸Ľ��ܶ���</a>.");
            }

            if (this.HisNode.IsStartNode)
            {
                this.addMsg("UnDoNew", "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>�������η���</a>�� <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + toNode.FK_Flow + "&FK_Node=" + toNode.FK_Flow + "01' ><img src=./Img/New.gif border=0/>�½�����</a>��");
            }
            else
            {
                this.addMsg("UnDo", "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>�������η���</a>��");
            }

            //msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //���½ڵ�״̬��
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);
            this.addMsg("Rpt", "@<a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>");
            #endregion ������Ϣ��ʾ
        }
        /// <summary>
        /// �����㵽��ͨ�㷢��
        /// 1. ����Ҫ��������.
        /// 2, ����ͨ�ڵ�����ͨ�ڵ㷢��.
        /// </summary>
        /// <returns></returns>
        private void NodeSend_31(Node nd)
        {
            //��������.

            // ��1-1һ�����߼�����.
            this.NodeSend_11(nd);
        }
        /// <summary>
        /// ���߳����·���
        /// </summary>
        /// <returns></returns>
        private string NodeSend_4x()
        {
            return null;
        }
        /// <summary>
        /// ���߳��������
        /// </summary>
        /// <returns></returns>
        private void NodeSend_53_SameSheet_To_HeLiu(Node toNode)
        {
            string spanNodes = this.SpanSubTheadNodes(toNode);
            if (toNode.FromNodes.Count != 1)
            {
                NodeSend_53_UnSameSheet_To_HeLiu(toNode);
                  return;
            }

            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == toNode.NodeID)
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

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID AND FK_Node=" + dbStr + "FK_Node";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow  SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", toNode.NodeID);
                ps.Add("NodeName", toNode.Name);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region ���������

                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr + "FK_Node AND FID=" + dbStr + "FID AND IsPass=1";
                ps.Add("FK_Node", this.HisNode.NodeID);
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];
                decimal ok = (decimal)dt_worker.Rows.Count;

                ps = new Paras();
                ps.SQL = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE   IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
                ps.Add("FID", this.HisWork.FID);
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(ps);
                decimal passRate = ok / all * 100;
                numStr = "@���ǵ�(" + ok + ")����˽ڵ��ϵ�ͬ�£���������(" + all + ")�������̡�";
                if (toNode.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID",
                        "FK_Node", toNode.NodeID, "WorkID", this.HisWork.FID);
                    numStr += "@��һ������(" + toNode.Name + ")�Ѿ�������";
                }
                #endregion ���������


                // �����������ܴӱ�����.
                this.GenerHieLiuHuiZhongDtlData(toNode);

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, toNode.NodeID, toNode.Name, null);

                this.addMsg("ToHeLiuEmp",
                    "@�����Ѿ����е������ڵ�[" + toNode.Name + "].@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + toNode.NodeID + "')\" >����֪ͨ����</a>��" + this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID) + numStr);
            }
            else
            {

                /* �Ѿ���FID��˵������ǰ�Ѿ��з������ߺ����ڵ㡣*/
                /*
                 * ���´������û�����̵����λ��
                 * ˵���ǵ�һ�ε�����ڵ�������.
                 * ���磺һ������:
                 * A����-> B��ͨ-> C����
                 * ��B ��C ��, B����N ���̣߳���֮ǰ���ǵ�һ������C.
                 */

                // �����ҵ��˽ڵ�Ľ�����Ա�ļ��ϡ���Ϊ FID ����������FID��
                WorkNode town = new WorkNode(toNode.HisWork, toNode);

                // ���Ի����ǵĹ�����Ա��
                GenerWorkerLists gwls = this.Func_GenerWorkerLists_WidthFID(town);
                string fk_emp = "";
                string toEmpsStr = "";
                string emps = "";
                foreach (GenerWorkerList wl in gwls)
                {
                    fk_emp = wl.FK_Emp;
                    if (Glo.IsShowUserNoOnly)
                        toEmpsStr += wl.FK_Emp + "��";
                    else
                        toEmpsStr += wl.FK_Emp + "(" + wl.FK_EmpText + ")��";

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
                myfh.Update(GenerFHAttr.FK_Node, toNode.NodeID,
                    GenerFHAttr.ToEmpsMsg, toEmpsStr);

                Work mainWK = town.HisWork;
                mainWK.OID = this.HisWork.FID;
                if (mainWK.RetrieveFromDBSources() == 1)
                    mainWK.Delete();

                // ���Ʊ�����������ݵ���������ȥ��
                DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(toNode.FK_Flow) + "Rpt WHERE OID=" + dbStr + "OID",
                    "OID", this.HisWork.FID);
                foreach (DataColumn dc in dt.Columns)
                    mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

                mainWK.NodeState = NodeState.Init;
                mainWK.Rec = fk_emp;
                mainWK.Emps = emps;
                mainWK.OID = this.HisWork.FID;
                mainWK.Insert();

                // �����������ܴӱ�����.
                this.GenerHieLiuHuiZhongDtlData(toNode);

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
                        athDB_N.FK_MapData = "ND" + toNode.NodeID;
                        athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + toNode.NodeID) + "_" + idx;
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                           "ND" + toNode.NodeID);
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
                        eleDB_N.FK_MapData = "ND" + toNode.NodeID;
                        eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + toNode.NodeID);
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
                if (toNode.PassRate <= passRate1)
                {
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + dbStr + "WorkID1, FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID2";
                    ps.Add("WorkID1", this.HisWork.FID);
                    ps.Add("FK_Node", toNode.NodeID);
                    ps.Add("WorkID2", this.HisWork.OID);
                    DBAccess.RunSQL(ps);
                }
                else
                {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + dbStr + "WorkID1,FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID2";
                    ps.Add("WorkID1", this.HisWork.FID);
                    ps.Add("FK_Node", toNode.NodeID);
                    ps.Add("WorkID2", this.HisWork.OID);
                    DBAccess.RunSQL(ps);
                }
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", toNode.NodeID);
                ps.Add("NodeName", toNode.Name);
                ps.Add("WorkID", this.HisWork.FID);
                DBAccess.RunSQL(ps);

                //�����Ѿ�ͨ��.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                DBAccess.RunSQL(ps);
                #endregion ���ø�����״̬

                this.addMsg("InfoToHeLiu", "@�����Ѿ����е������ڵ�[" + toNode.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + toNode.NodeID + "')\" >����֪ͨ����</a>��" + "@���ǵ�һ������˽ڵ��ͬ��.");
            }
        }
        private string NodeSend_55(Node toNode)
        {
            return null;
        }
        /// <summary>
        /// �ڵ������˶�
        /// </summary>
        private void NodeSend_Send_5_5()
        {
            switch (this.HisNode.HisRunModel)
            {
                case RunModel.Ordinary: /* 1�� ��ͨ�ڵ����·��͵�*/
                    Node toND = this.NodeSend_GenerNextStepNode();
                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND.NodeID.ToString(), toND.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND.Name, toND.Name, SendReturnMsgType.SystemMsg);

                    switch (toND.HisRunModel)
                    {
                        case RunModel.Ordinary:   /*1-1 ��ͨ��to��ͨ�ڵ� */
                            this.NodeSend_11(toND);
                            break;
                        case RunModel.FL:  /* 1-2 ��ͨ��to������   */
                            this.NodeSend_11(toND);
                            break;
                        case RunModel.HL:  /*1-3 ��ͨ��to������   */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治�����Ӻ����ڵ�(" + toND.Name + ").");
                            break;
                        case RunModel.FHL: /*1-4 ��ͨ�ڵ�to�ֺ����� */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治�����ӷֺ����ڵ�(" + toND.Name + ").");
                        case RunModel.SubThread: /*1-5 ��ͨ��to���̵߳� */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ��ͨ�ڵ����治���������߳̽ڵ�(" + toND.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND.Name + ")");
                            break;
                    }
                    break;
                case RunModel.FL: /* 2: �����ڵ����·��͵�*/
                    Nodes toNDs = this.Func_GenerNextStepNodes();
                    if (toNDs.Count == 1)
                    {
                        Node toND2 = toNDs[0] as Node;
                        //����ϵͳ����.
                        this.addMsg(SendReturnMsgFlag.VarToNodeID, toND2.NodeID.ToString(), toND2.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                        this.addMsg(SendReturnMsgFlag.VarToNodeName, toND2.Name, toND2.Name, SendReturnMsgType.SystemMsg);

                        switch (toND2.HisRunModel)
                        {
                            case RunModel.Ordinary:    /*2.1 ������to��ͨ�ڵ� */
                                this.NodeSend_11(toND2); /* ����ͨ�ڵ㵽��ͨ�ڵ㴦��. */
                                break;
                            case RunModel.FL:  /*2.2 ������to������  */
                                throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND2.Name + ").");
                            case RunModel.HL:  /*2.3 ������to������,�ֺ�����   */
                            case RunModel.FHL:
                                throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND2.Name + ").");
                            case RunModel.SubThread: /* 2.4 ������to���̵߳�   */
                                if (toND2.HisSubThreadType == SubThreadType.SameSheet)
                                    NodeSend_24_SameSheet(toND2);
                                else
                                    NodeSend_24_UnSameSheet(toNDs); /*������ֻ����1�����*/
                                break;
                            default:
                                throw new Exception("@û���жϵĽڵ�����(" + toND2.Name + ")");
                                break;
                        }
                    }
                    else
                    {
                        /* ����ж���ڵ㣬���һ�����Ǳض������߳̽ڵ���򣬾�����ƴ���*/
                        bool isHaveSameSheet = false;
                        bool isHaveUnSameSheet = false;
                        foreach (Node nd in toNDs)
                        {
                            switch (nd.HisRunModel)
                            {
                                case RunModel.Ordinary:
                                    NodeSend_11(nd); /*����ͨ�ڵ㵽��ͨ�ڵ㴦��.*/
                                    break;
                                case RunModel.FL:
                                    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + nd.Name + ").");
                                case RunModel.FHL:
                                    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷֺ����ڵ�(" + nd.Name + ").");
                                case RunModel.HL:
                                    throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + nd.Name + ").");
                                default:
                                    break;
                            }
                            if (nd.HisSubThreadType == SubThreadType.SameSheet)
                                isHaveSameSheet = true;

                            if (nd.HisSubThreadType == SubThreadType.UnSameSheet)
                                isHaveUnSameSheet = true;
                        }

                        if (isHaveUnSameSheet && isHaveSameSheet)
                            throw new Exception("@��֧������ģʽ: �����ڵ�ͬʱ������ͬ�������߳�����������߳�.");

                        if (isHaveSameSheet == true)
                            throw new Exception("@��֧������ģʽ: �����ڵ�ͬʱ�����˶��ͬ�������߳�.");

                        //�������������߳̽ڵ�.
                        this.NodeSend_24_UnSameSheet(toNDs);
                    }
                    break;
                case RunModel.HL:  /* 3: �����ڵ����·��� */
                    Node toND3 = this.NodeSend_GenerNextStepNode();
                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND3.NodeID.ToString(), toND3.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND3.Name, toND3.Name, SendReturnMsgType.SystemMsg);

                    switch (toND3.HisRunModel)
                    {
                        case RunModel.Ordinary: /*3.1 ��ͨ�����ڵ� */
                            this.NodeSend_31(toND3); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        case RunModel.FL: /*3.2 ������ */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND3.Name + ").");
                        case RunModel.HL: /*3.3 ������ */
                        case RunModel.FHL:
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND3.Name + ").");
                        case RunModel.SubThread:/*3.4 ���߳�*/
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治���������߳̽ڵ�(" + toND3.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND3.Name + ")");
                    }
                    break;
                case RunModel.FHL:  /* 4: �����ڵ����·��͵� */
                    Node toND4 = this.NodeSend_GenerNextStepNode();
                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND4.NodeID.ToString(), toND4.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND4.Name, toND4.Name, SendReturnMsgType.SystemMsg);

                    switch (toND4.HisRunModel)
                    {
                        case RunModel.Ordinary: /*4.1 ��ͨ�����ڵ� */
                            this.NodeSend_11(toND4); /* ��������ͨ�����ͨ��һ�����߼�. */
                            break;
                        case RunModel.FL: /*4.2 ������ */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND4.Name + ").");
                        case RunModel.HL: /*4.3 ������ */
                        case RunModel.FHL:
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治�����Ӻ����ڵ�(" + toND4.Name + ").");
                        case RunModel.SubThread:/*4.5 ���߳�*/
                            if (toND4.HisSubThreadType == SubThreadType.SameSheet)
                                NodeSend_24_SameSheet(toND4);
                            //else
                            //    NodeSend_24_UnSameSheet(toNDs); /*������ֻ����1�����*/
                            break;
                        //throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ������(" + this.HisNode.Name + ")���治���������߳̽ڵ�(" + toND4.Name + ").");
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND4.Name + ")");
                    }
                    break;
                   // throw new Exception("@û���жϵ�����:" + this.HisNode.HisNodeWorkTypeT);
                case RunModel.SubThread:  /* 5: ���߳̽ڵ����·��͵� */
                    Node toND5 = this.NodeSend_GenerNextStepNode();
                    //����ϵͳ����.
                    this.addMsg(SendReturnMsgFlag.VarToNodeID, toND5.NodeID.ToString(), toND5.NodeID.ToString(), SendReturnMsgType.SystemMsg);
                    this.addMsg(SendReturnMsgFlag.VarToNodeName, toND5.Name, toND5.Name, SendReturnMsgType.SystemMsg);

                    switch (toND5.HisRunModel)
                    {
                        case RunModel.Ordinary: /*5.1 ��ͨ�����ڵ� */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ���̵߳�(" + this.HisNode.Name + ")���治��������ͨ�ڵ�(" + toND5.Name + ").");
                            break;
                        case RunModel.FL: /*5.2 ������ */
                            throw new Exception("@������ƴ���:�������̻�ȡ��ϸ��Ϣ, ���̵߳�(" + this.HisNode.Name + ")���治�����ӷ����ڵ�(" + toND5.Name + ").");
                        case RunModel.HL: /*5.3 ������ */
                        case RunModel.FHL: /*5.4 �ֺ����� */
                            if (this.HisNode.HisSubThreadType == SubThreadType.SameSheet)
                                this.NodeSend_53_SameSheet_To_HeLiu(toND5);
                            else
                                this.NodeSend_53_UnSameSheet_To_HeLiu(toND5);
                            break;
                        case RunModel.SubThread: /*5.5 ���߳�*/
                            if (toND5.HisSubThreadType == this.HisNode.HisSubThreadType)
                                this.NodeSend_11(toND5); /*����ͨ�ڵ�һ��.*/
                            else
                                throw new Exception("@�������ģʽ���������������̵߳����߳�ģʽ��һ�����ӽڵ�(" + this.HisNode.Name + ")���ڵ�(" + toND5.Name + ")");
                            break;
                        default:
                            throw new Exception("@û���жϵĽڵ�����(" + toND5.Name + ")");
                    }
                    break;
                default:
                    throw new Exception("@û���жϵ�����:" + this.HisNode.HisNodeWorkTypeT);
            }
        }
        #region ���ض�����.
        private SendReturnObjs HisMsgObjs = null;
        public void addMsg(string flag, string msg)
        {
            addMsg(flag, msg, null, SendReturnMsgType.Info);
        }
        public void addMsg(string flag, string msg, SendReturnMsgType msgType)
        {
            addMsg(flag, msg, null, msgType);
        }
        public void addMsg(string flag, string msg, string msgofHtml, SendReturnMsgType msgType)
        {
            if (HisMsgObjs == null)
                HisMsgObjs = new SendReturnObjs();
            this.HisMsgObjs.AddMsg(flag, msg, msgofHtml, msgType);
        }
        public void addMsg(string flag, string msg, string msgofHtml)
        {
            addMsg(flag, msg, msgofHtml, SendReturnMsgType.Info);
        }
        #endregion ���ض�����.
        /// <summary>
        /// ����������ҵ����
        /// </summary>
        public SendReturnObjs NodeSend()
        {
            return NodeSend(null, null);
        }
        /// <summary>
        /// ����������ҵ����.
        /// ��������:2012-11-11.
        /// ����ԭ��:�����߼��Բ�����,����©�Ĵ���ģʽ.
        /// �޸���:zhoupeng.
        /// �޸ĵص�:����.
        /// ----------------------------------- ˵�� -----------------------------
        /// 1���������Ϊ���󲿷�: ����ǰ���\5*5�㷨\���ͺ��ҵ����.
        /// 2, ��ϸ��ο��������ϵ�˵��.
        /// 3, ���ͺ����ֱ�ӻ�ȡ����
        /// </summary>
        /// <param name="jumpToNode">Ҫ��ת�Ľڵ�</param>
        /// <param name="jumpToEmp">Ҫ��ת����</param>
        /// <returns>ִ�нṹ</returns>
        public SendReturnObjs NodeSend(Node jumpToNode, string jumpToEmp)
        {
            //����ϵͳ����.
            //this.addMsg(SendReturnMsgFlag.VarCurrNodeID, this.HisNode.NodeID.ToString(), this.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
            //this.addMsg(SendReturnMsgFlag.VarCurrNodeName, this.HisNode.Name, this.HisNode.Name, SendReturnMsgType.SystemMsg);

            //����ϵͳ����.
            this.addMsg(SendReturnMsgFlag.VarCurrNodeID, this.HisNode.NodeID.ToString(), this.HisNode.NodeID.ToString(), SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarCurrNodeName, this.HisNode.Name, this.HisNode.Name, SendReturnMsgType.SystemMsg);
            this.addMsg(SendReturnMsgFlag.VarWorkID, this.WorkID.ToString(), this.WorkID.ToString(), SendReturnMsgType.SystemMsg);



            //������ת�ڵ㣬����п���Ϊnull.
            this.JumpToNode = jumpToNode;
            this.JumpToEmp = jumpToEmp;

            string sql = null;
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;

            #region ��һ��: ��鵱ǰ����Ա�Ƿ���Է���: �������� 3 ������.
            // ��1.1: ����Ƿ���Դ���ǰ�Ĺ���.
            if (this.HisNode.IsStartNode == false
                && BP.WF.Dev2Interface.Flow_CheckIsCanDoCurrentWork(this.WorkID, WebUser.No) == false)
                throw new Exception("@��ǰ�������Ѿ�������ɣ�������û�д���ǰ������Ȩ�ޡ�");

            // ��1.2: ���÷���ǰ���¼��ӿ�,�����û������ҵ���߼�.
            this.addMsg(SendReturnMsgFlag.SendWhen, this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.SendWhen, this.HisWork));

            // ��3: ������Ǻ����㣬�����߳�δ��ɵ����.
            if (this.HisNode.IsHL || this.HisNode.IsFLHL)
            {
                /*   ����Ǻ����� ��鵱ǰ�Ƿ��Ǻ���������ǣ���������ϵ����߳��Ƿ���ɡ�*/
                /*����Ƿ������߳�û�н���*/
                sql = "SELECT * FROM WF_GenerWorkerList WHERE FID=" + this.WorkID + " AND IsPass=0";
                DataTable dtWL = DBAccess.RunSQLReturnTable(sql);
                string infoErr = "";
                if (dtWL.Rows.Count != 0)
                {
                    infoErr += "@���������·��ͣ����������߳�û����ɡ�";
                    foreach (DataRow dr in dtWL.Rows)
                    {
                        infoErr += "@����Ա���:" + dr["FK_Emp"] + "," + dr["FK_EmpText"] + ",ͣ���ڵ�:" + dr["FK_NodeText"];
                    }
                    if (this.HisNode.IsForceKill)
                        infoErr += "@��֪ͨ���Ǵ������,����ǿ��ɾ�����������������·���.";
                    else
                        infoErr += "@��֪ͨ���Ǵ������,���������·���.";
                    throw new Exception(infoErr);
                }
            }
            #endregion ��һ��: ��鵱ǰ����Ա�Ƿ���Է���

            DBAccess.DoTransactionBegin();
            try
            {
                if (this.HisNode.IsStartNode)
                    InitStartWorkDataV2(); //��ʼ����ʼ�ڵ�����, �����ǰ�ڵ��ǿ�ʼ�ڵ�.

                this.CheckCompleteCondition();
                if (this.IsStopFlow == true)
                {
                    /*�ڼ����󣬷������ı�־�����Ѿ�ֹͣ�ˡ�*/
                    this.Func_DoSetThisWorkOver();
                }
                else
                {
                    #region �ڶ���: ������ĵ�������ת��������. 5*5 �ķ�ʽ����ͬ�ķ������.
                    // ִ�нڵ����·��͵�25��������ж�.
                    if (this.HisNode.IsEndNode == false)
                    {
                        this.NodeSend_Send_5_5();
                        // ����ǰ��������.
                        this.Func_DoSetThisWorkOver();
                    }

                    if (this.HisNode.IsEndNode == true)
                    {
                        this.DoSetThisWorkOver();
                    }
                    #endregion �ڶ���: 5*5 �ķ�ʽ����ͬ�ķ������.
                }

                #region ������: ������֮���ҵ���߼�.
                //�ѵ�ǰ�ڵ������copy���������ݱ���.
                this.DoCopyCurrentWorkDataToRpt();

                #endregion ������: ������֮���ҵ���߼�.
                 
                #region ��������
                if (Glo.IsEnableSysMessage)
                {
                    Listens lts = new Listens();
                    lts.RetrieveByLike(ListenAttr.Nodes, "%" + this.HisNode.NodeID + "%");

                    foreach (Listen lt in lts)
                    {
                        ps = new Paras();
                        ps.SQL = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE IsEnable=1 AND IsPass=1 AND FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                        ps.Add("FK_Node", lt.FK_Node);
                        ps.Add("WorkID", this.WorkID);

                        DataTable dtRem = BP.DA.DBAccess.RunSQLReturnTable(ps);
                        foreach (DataRow dr in dtRem.Rows)
                        {
                            string fk_emp = dr["FK_Emp"] as string;

                            //Port.WFEmp emp = new BP.WF.Port.WFEmp(fk_emp);
                            //if (emp.HisAlertWay == BP.WF.Port.AlertWay.None)
                            //{
                            //    // msg += "@<font color=red>����Ϣ�޷����͸���" + emp.Name + "����Ϊ���ر�����Ϣ���ѣ�������绰��"+emp.Tel+"</font>";
                            //    msg += "@<font color=red>" + this.ToEP2("WN25", "����Ϣ�޷����͸���{0}����Ϊ���ر�����Ϣ���ѣ�������绰��{1}��", emp.Name, emp.Tel) + "</font>";
                            //    continue;
                            //}
                            //else
                            //{
                            //    // msg += "@���Ĳ����Ѿ�ͨ����<font color=green><b>" + emp.HisAlertWayT + "</b></font>���ķ�ʽ���͸���" + emp.Name;
                            //    msg += this.ToEP2("WN26", "@���Ĳ����Ѿ�ͨ����<font color=green><b>{0}</b></font>���ķ�ʽ���͸���{1}", emp.HisAlertWayT, emp.Name);
                            //}

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

                            BP.TA.SMS.AddMsg(lt.OID + "_" + this.WorkID, fk_emp, title + doc, title, doc);
                        }
                    }
                }
                #endregion

                #region ���ɵ���
                if (this.HisNode.BillTemplates.Count > 0)
                {
                    BillTemplates reffunc = this.HisNode.BillTemplates;

                    #region ���ɵ�����Ϣ
                    Int64 workid = this.HisWork.OID;
                    int nodeId = this.HisNode.NodeID;
                    string flowNo = this.HisNode.FK_Flow;
                    #endregion

                    DateTime dtNow = DateTime.Now;
                    Flow fl = this.HisNode.HisFlow;
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

                            }
                            else
                            {
                                WorkNodes wns = new WorkNodes();
                                if (this.HisNode.HisRunModel == RunModel.FL
                                    || this.HisNode.HisRunModel == RunModel.FHL
                                    || this.HisNode.HisRunModel == RunModel.HL)
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
                                try
                                {
                                    Glo.Rtf2PDF(rtfPath, pdfPath);
                                }
                                catch (Exception ex)
                                {
                                    this.addMsg("RptError","�����������ݴ���:"+ex.Message);
                                }
                            }
                            #endregion

                            #region ���浥��
                            Bill bill = new Bill();
                            bill.MyPK = this.HisWork.FID + "_" + this.HisWork.OID + "_" + this.HisNode.NodeID + "_" + func.No;
                            bill.FID = this.HisWork.FID;
                            bill.WorkID = this.HisWork.OID;
                            bill.FK_Node = this.HisNode.NodeID;
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
                    this.addMsg(SendReturnMsgFlag.BillInfo,billInfo);
                }
                #endregion

                #region ִ�г���.
                if (this.HisNode.HisCCRole == CCRole.AutoCC || this.HisNode.HisCCRole == CCRole.HandAndAuto)
                {
                    try
                    {
                        /*������Զ�����*/
                        CC cc = this.HisNode.HisCC;

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
                            string ccMsg  = "@��Ϣ�Զ����͸�";
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
                                ccMsg += list.CCTo + "(" + dr[1].ToString() + ");";

                                BP.WF.Port.WFEmp wfemp = new Port.WFEmp(list.CCTo);


                                string sid = list.CCTo + "_" + list.RefWorkID + "_" + list.FK_Node + "_" + list.RDT;
                                string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                                string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";

                                string mytemp = mailTemp.Clone() as string;

                                mytemp = string.Format(mytemp, wfemp.Name, WebUser.Name, url, urlWap);

                                string title = this.ToEP3("WN27", "��������:{0}.����:{1},������:{2},��������",
                           this.HisNode.FlowName, this.HisNode.Name, WebUser.Name);

                                BP.TA.SMS.AddMsg(list.RefWorkID + "_" + list.FK_Node + "_" + wfemp.No, wfemp.No,
                                  title, title, mytemp);
                            }
                            this.addMsg(SendReturnMsgFlag.CCMsg, ccMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@�������ʱ���ִ���:" + ex.Message);
                    }
                }


                DBAccess.DoTransactionCommit(); //�ύ����.
                #endregion ������Ҫҵ���߼�.

                #region �����ͳɹ����¼�.
                try
                {
                    // �����ͳɹ��������
                    string SendSuccess=this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.SendSuccess, this.HisWork);
                    this.addMsg(SendReturnMsgFlag.SendSuccessMsg, SendSuccess);
                }
                catch (Exception ex)
                {
                    this.addMsg(SendReturnMsgFlag.SendSuccessMsgErr, ex.Message);
                }
                #endregion �����ͳɹ����¼�.

                #region �����ͳɹ������Ϣ��ʾ
                if (this.HisNode.HisTurnToDeal == TurnToDeal.SpecMsg)
                {
                    string msgOfSend = this.HisNode.TurnToDealDoc;
                    if (msgOfSend.Contains("@"))
                    {
                        Attrs attrs = this.HisWork.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            if (msgOfSend.Contains("@") == false)
                                continue;
                            msgOfSend = msgOfSend.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                        }
                    }

                    if (msgOfSend.Contains("@") == true)
                    {
                        /*˵����һЩ������ϵͳ��������.*/
                        string msgOfSendText = msgOfSend.Clone() as string;
                        foreach (SendReturnObj item in this.HisMsgObjs)
                        {
                            if (string.IsNullOrEmpty(item.MsgFlag))
                                continue;

                            if (msgOfSend.Contains("@") == false)
                                continue;

                            msgOfSendText = msgOfSendText.Replace("@" + item.MsgFlag, item.MsgOfText);

                            if (item.MsgOfHtml != null)
                                msgOfSend = msgOfSend.Replace("@" + item.MsgFlag, item.MsgOfHtml);
                            else
                                msgOfSend = msgOfSend.Replace("@" + item.MsgFlag, item.MsgOfText);
                        }

                        this.HisMsgObjs.OutMessageHtml = msgOfSend;
                        this.HisMsgObjs.OutMessageText = msgOfSendText;
                    }
                    else
                    {
                        this.HisMsgObjs.OutMessageHtml = msgOfSend;
                        this.HisMsgObjs.OutMessageText = msgOfSend;
                    }

                    //return msgOfSend;
                }
                #endregion �����ͳɹ����¼�.

                // �����Ҫ��ת.
                if (town != null)
                {
                    if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
                    {
                        town.NodeSend();
                        this.HisMsgObjs = town.HisMsgObjs;
                    }
                }

                //�����������.
                return this.HisMsgObjs;
            }
            catch (Exception ex)
            {
                this.WhenTranscactionRollbackError(ex);
                DBAccess.DoTransactionRollback();
                throw ex;
            }
        }
        /// <summary>
        /// �ֹ��Ļع��ύʧ����Ϣ.
        /// </summary>
        /// <param name="ex"></param>
        private void WhenTranscactionRollbackError(Exception ex)
        {
            /*���ύ���������£��ع����ݡ�*/
            try
            {
                // �ѹ�����״̬���û�����
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // �����̵�״̬���û�����
                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.WorkID = this.WorkID;
                if (gwf.RetrieveFromDBSources()==0)
                    return;

                if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                {
                    /* ���������������һ���б仯��*/
                    gwf.FK_Node = this.HisNode.NodeID;
                    gwf.NodeName = this.HisNode.Name;
                    gwf.WFState = 0;
                    gwf.Update();
                }

                //ִ������.
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr + "WorkID AND FK_Node=" + dbStr+"FK_Node ";
                ps.AddFK_Emp();
                ps.Add("WorkID", this.WorkID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

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
                    if (this.HisNode.HisRunModel == RunModel.SubThread && nd.HisRunModel == RunModel.SubThread )
                    {
                        /*�����������߳�, ɾ����ǰ���߳̽ڵ㡣*/
                        ps = new Paras();
                        ps.SQL = "DELETE WF_GenerWorkerlist  WHERE WorkID=" + dbStr + "WorkID AND FK_Node=" + dbStr + "FK_Node ";
                        ps.AddFK_Emp();
                        ps.Add("WorkID", this.WorkID);
                        ps.Add("FK_Node", nd.NodeID);
                        BP.DA.DBAccess.RunSQL(ps);
                        continue;
                    }

                    if ((this.HisNode.HisRunModel == RunModel.FL ||  this.HisNode.HisRunModel == RunModel.FHL) && nd.HisRunModel == RunModel.SubThread)
                    {
                        /*�����������߳�, ɾ����ǰ���߳̽ڵ㡣*/
                        ps = new Paras();
                        ps.SQL = "DELETE WF_GenerWorkerlist  WHERE FID=" + dbStr + "FID AND FK_Node=" + dbStr + "FK_Node ";
                        ps.AddFK_Emp();
                        ps.Add("FID", this.WorkID);
                        ps.Add("FK_Node", nd.NodeID);
                        BP.DA.DBAccess.RunSQL(ps);
                        continue;
                    }

                    mwk.OID = this.WorkID;
                    mwk.DirectDelete();

                }
                this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.SendError, this.HisWork);
            }
            catch (Exception ex1)
            {
                if (this.rptGe != null)
                    this.rptGe.CheckPhysicsTable();
                throw new Exception(ex.Message + "@�ع�����ʧ�����ݳ��ִ���" + ex1.Message + "@�п���ϵͳ�Ѿ��Զ��޸���������������ִ��һ�Ρ�");
            }
        }
         

        #region �û����ı���
        public GenerWorkerLists HisWorkerLists = null;
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
        /// <param name="toNode">ת��Ľڵ�</param>
        /// <returns>�����ķ��ص���Ϣ</returns>
        public string FeiLiuStartUp(Node toNode)
        {
            return null;
            // ����.
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // ������һ����Ҫִ�е���Ա.
            GenerWorkerLists gwls = this.Func_GenerWorkerLists(town);
            this.AddIntoWacthDog(gwls);  //@������Ϣ�����

            //�����ǰ�����ݣ��������η��͡�
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            // �жϷ����Ĵ���.�ǲ�����ʷ��¼�����з�����
            bool IsHaveFH = false;
            ps = new Paras();
            ps.SQL = "SELECT COUNT(*) FROM WF_GenerWorkerlist WHERE FID=" +dbStr+"OID";
            ps.Add("OID", this.HisWork.OID);
            if (DBAccess.RunSQLReturnValInt(ps) != 0)
                IsHaveFH = true;

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
                    foreach (GenerWorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.rptGe);
                        mywk.Copy(this.HisWork);  //���ƹ�����Ϣ��

                        bool isHaveEmp = false;
                        if (IsHaveFH)
                        {
                            /* ��������߹��������������ҵ�ͬһ����Աͬһ��FID�µ�OID �����⵱ǰ�̵߳�ID��*/
                            ps = new Paras();
                            ps.SQL = "SELECT WorkID,FK_Node FROM WF_GenerWorkerlist WHERE FID=" + dbStr + "FID AND FK_Emp=" + dbStr + "FK_Emp ORDER BY RDT DESC";
                            ps.Add("FID", this.WorkID);
                            ps.Add("FK_Emp", wl.FK_Emp);
                            DataTable dt = DBAccess.RunSQLReturnTable(ps);
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
                                DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" +dbStr+"RefPK", "RefPK",mywk.OID);
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
                        gwf.Title = WorkNode.GenerTitle(this.HisWork);

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
                        ps = new Paras();
                        ps.SQL = "UPDATE WF_GenerWorkerlist SET WorkID=" + dbStr + "WorkID1, FID=" + dbStr + "FID WHERE FK_Emp=" + dbStr + "FK_Emp AND WorkID=" + dbStr + "WorkID2 AND FK_Node=" + dbStr+"FK_Node ";
                        ps.Add("WorkID1", mywk.OID);
                        ps.Add("FID", this.WorkID );
                        ps.Add("FK_Emp", wl.FK_Emp);
                        ps.Add("WorkID2", this.WorkID);
                        ps.Add("FK_Node", toNode.NodeID);

                        DBAccess.RunSQL(ps);
                    }
                    break;
                default:
                    throw new Exception("û�д�������ͣ�" + this.HisNode.HisFLRole.ToString());
            }
            string info ="@�����ڵ�:{0}�Ѿ�����@�����Զ��´��{1}����{2}λͬ��,{3}.";
            string fenliuInfo = string.Format(info, toNode.Name,
                this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(),
                this._RememberMe.EmpsExt);

            this.addMsg(SendReturnMsgFlag.FenLiuInfo, fenliuInfo);

            // ����ǿ�ʼ�ڵ㣬�Ϳ�������ѡ������ˡ�
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2)
                {
                    this.addMsg(SendReturnMsgFlag.EditAccepter,null, "@<img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0 /><a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "')\" >" + this.ToE("W29", "�޸Ľ��ܶ���") + "</a>.", 
                        SendReturnMsgType.Info);
                }
            }

            if (this.HisNode.IsStartNode)
            {
                this.addMsg(SendReturnMsgFlag.NewFlowUnSend, null,
                    "@<a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>�� <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + toNode.FK_Flow + "&FK_Node=" + toNode.FK_Flow + "01' ><img src=" + this.VirPath + "/WF/Img/New.gif border=0/>" + this.ToE("NewFlow", "�½�����") + "</a>��", 
                    SendReturnMsgType.Info);
            }
            else
            {
                this.addMsg(SendReturnMsgFlag.UnSend,null,
                    "@<a href='" + this.VirPath  + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "�������η���") + "</a>��", 
                    SendReturnMsgType.Info);
            }

           // msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //���½ڵ�״̬��
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            this.addMsg(SendReturnMsgFlag.Rpt, null,"@<a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>", SendReturnMsgType.Info);
        }
        #endregion
        /// <summary>
        /// ���ɱ���
        /// </summary>
        /// <param name="wk"></param>
        /// <returns></returns>
        public static string GenerTitle(Work wk)
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
            titleRole = titleRole.Replace("@WebUser.FK_Dept", wk.RecOfEmp.FK_Dept);
            titleRole = titleRole.Replace("@RDT", wk.RDT);
            if (titleRole.Contains("@"))
            {
                Attrs attrs = wk.EnMap.Attrs;
                foreach (Attr attr in attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;

                    if (attr.IsFKorEnum)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            titleRole = titleRole.Replace('~', '-');
            titleRole = titleRole.Replace("'", "��");
            wk.SetValByKey("Title",titleRole);
            return titleRole;
        }
        public GEEntity rptGe = null;
        private void InitStartWorkData()
        {
            /* ������ʼ�������̼�¼. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;
            gwf.Title = WorkNode.GenerTitle(this.HisWork);
            this.HisWork.SetValByKey("Title", gwf.Title);
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
            GenerWorkerList wl = new GenerWorkerList();
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
            catch
            {
                wl.Update();
            }
            #endregion
        }
        private void InitStartWorkDataV2()
        {
            /*����ǿ�ʼ�����ж��ǲ��Ǳ���������̣�����Ǿ�Ҫ������д��־��*/
            if (SystemConfig.IsBSsystem)
            {
                string fk_nodeFrom = System.Web.HttpContext.Current.Request.QueryString["FromNode"];
                if (string.IsNullOrEmpty(fk_nodeFrom) == false)
                {
                    Node ndFrom = new Node(int.Parse(fk_nodeFrom));
                    string fromWorkID = System.Web.HttpContext.Current.Request.QueryString["FromWorkID"];
                    string pTitle = DBAccess.RunSQLReturnStringIsNull("SELECT Title FROM  ND" + int.Parse(ndFrom.FK_Flow) + "01 WHERE OID=" + fromWorkID, "");

                    //��¼��ǰ���̱�����
                    this.AddToTrack(ActionType.StartSubFlow, WebUser.No,
                        WebUser.Name, ndFrom.NodeID, ndFrom.FlowName + "\t\n" + ndFrom.FlowName, "��������(" + ndFrom.FlowName + ":" + pTitle + ")����.");

                    //��¼�����̱�����
                    TrackTemp tkParent = new TrackTemp();
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
                    tkParent.MyPK = tkParent.WorkID + "_" + tkParent.FID + "_" + (int)tkParent.HisActionType + "_" + tkParent.NDFrom + "_" + DateTime.Now.ToString("yyMMddHHmmss");
                    try
                    {
                        tkParent.Insert();
                    }
                    catch
                    {
                    }
                }
            }

            /* ������ʼ�������̼�¼. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;
            gwf.DirectDelete();

            gwf.Title = WorkNode.GenerTitle(this.HisWork);
            this.HisWork.SetValByKey("Title", gwf.Title);
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

            if (this.HisFlow.HisTimelineRole == TimelineRole.ByFlow)
            {
                try
                {
                    gwf.SDTOfFlow = this.HisWork.GetValStrByKey(WorkSysFieldAttr.SysSDTOfFlow);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError("������������ƴ���,��ȡ��ʼ�ڵ�{" + gwf.Title + "}����������Ӧ���ʱ���д���,�Ƿ����SysSDTOfFlow�ֶ�? �쳣��Ϣ:" + ex.Message);
                    /*��ȡ��ʼ�ڵ����������Ӧ���ʱ���д���,�Ƿ����SysSDTOfFlow�ֶ�? .*/
                    if (this.HisWork.EnMap.Attrs.Contains(WorkSysFieldAttr.SysSDTOfFlow) == false)
                        throw new Exception("������ƴ��������õ�����ʱЧ�����ǣ�����ʼ�ڵ��SysSDTOfFlow�ֶμ���������ǿ�ʼ�ڵ���������ֶ� SysSDTOfFlow , ϵͳ������Ϣ:" + ex.Message);

                    throw new Exception("��ʼ����ʼ�ڵ����ݴ���:" + ex.Message);
                }

                /*�ж�ʱ���������Ƿ�Ϸ�.*/
                try
                {
                    string rdt = gwf.SDTOfFlow;
                    DateTime dt = DataType.ParseSysDateTime2DateTime(rdt);
                }
                catch (Exception ex)
                {
                    throw new Exception("@����������������ʱ�����,{" + gwf.SDTOfFlow + "}��һ���Ƿ�������.");
                }
            }
            else
            {

            }

            gwf.DirectInsert();

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
            GenerWorkerList wl = new GenerWorkerList();
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
            catch
            {
                wl.Update();
            }
            #endregion
        }
        /// <summary>
        /// ִ�н���ǰ�����ڵ������copy��Rpt����ȥ.
        /// </summary>
        public void DoCopyCurrentWorkDataToRpt()
        {
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
                return;
            }

            /*����ǰ�����Ը��Ƶ�rpt������ȥ.*/
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

            // �ѵ�ǰ�Ĺ�����Ա��������ȥ.
            string str = rptGe.GetValStrByKey(GERptAttr.FlowEmps);
            if (str.Contains("@" + WebUser.No + "," + WebUser.Name) == false)
                rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + WebUser.No + "," + WebUser.Name);

            if (this.HisNode.IsEndNode)
            {
                rptGe.SetValByKey(GERptAttr.WFState, 1); // ����״̬��
                rptGe.SetValByKey(GERptAttr.FlowEnder, WebUser.No);
                rptGe.SetValByKey(GERptAttr.FlowEnderRDT, DataType.CurrentDataTime);
                rptGe.SetValByKey(GERptAttr.FlowEndNode, this.HisNode.NodeID);
                rptGe.SetValByKey(GERptAttr.FlowDaySpan, DataType.GetSpanDays(this.rptGe.GetValStringByKey(GERptAttr.FlowStartRDT), DataType.CurrentDataTime));
            }
            rptGe.DirectUpdate();
        }
        /// <summary>
        /// ִ������copy.
        /// </summary>
        /// <param name="fromWK"></param>
        public void DoCopyRptWork(Work fromWK)
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
                    case BP.WF.GERptAttr.FlowEndNode:
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
            TrackTemp t = new TrackTemp();
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
                    case ActionType.UnSend:
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
                t.MyPK = t.WorkID + "_" + t.FID + "_"  + t.NDFrom + "_" + t.NDTo +"_"+t.EmpFrom+"_"+t.EmpTo+"_"+ DateTime.Now.ToString("yyMMddHHmmss");
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
        public void AddIntoWacthDog(GenerWorkerLists gwls)
        {
            if (BP.SystemConfig.IsBSsystem == false)
                return;

            if (BP.WF.Glo.IsEnableSysMessage  == false)
                return;

            string basePath = "http://" + System.Web.HttpContext.Current.Request.Url.Host;
            basePath += "/" + System.Web.HttpContext.Current.Request.ApplicationPath;
            string mailTemp = BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfDataUser + "\\EmailTemplete\\" + WebUser.SysLang + ".txt");

            foreach (GenerWorkerList wl in gwls)
            {
                if (wl.IsEnable == false)
                    continue;

                string sid = wl.FK_Emp + "_" + wl.WorkID + "_" + wl.FK_Node + "_" + wl.RDT;
                string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";

                //string mytemp ="����" + wl.FK_EmpText + ":  <br><br>&nbsp;&nbsp; "+WebUser.Name+"�����Ĺ�����Ҫ������������<a href='" + url + "'>�򿪹���</a>�� \t\n <br>&nbsp;&nbsp;����򲻿��븴�Ƶ��������ַ���<br>&nbsp;&nbsp;" + url + " <br><br>&nbsp;&nbsp;���ʼ���iWF�������������Զ��������벻Ҫ�ظ���<br>*^_^*  лл ";
                string mytemp = mailTemp.Clone() as string;
                mytemp = string.Format(mytemp, wl.FK_EmpText, WebUser.Name, url, urlWap);

                // ִ����Ϣ���͡�
                // BP.WF.Port.WFEmp wfemp = new BP.WF.Port.WFEmp(wl.FK_Emp);
                // wfemp.No = wl.FK_Emp;

                string title = this.ToEP3("WN27", "����:{0}.����:{1},������:{2},��������",
                    this.HisNode.FlowName, wl.FK_NodeText, WebUser.Name);

                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wl.FK_Emp, wl.FK_Emp, title, title, mytemp);
            }

            /*
            string workers="";
            // ������
            foreach(GenerWorkerList wl in gwls)
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
        /// �����ڵ��Ƿ�ȫ����ɣ�
        /// </summary>
        private bool IsOverMGECheckStand = false;
        private bool IsStopFlow = false;
        /// <summary>
        /// ������̡��ڵ���������
        /// </summary>
        /// <returns></returns>
        private void CheckCompleteCondition()
        {
            this.IsStopFlow = false;

            if (this.HisNode.IsEndNode)
            {
                /* ���������� */
                this.addMsg(SendReturnMsgFlag.End,"@�����Ѿ��ߵ����һ���ڵ㣬���̳ɹ�������");
                this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "�����Ѿ��ߵ����һ���ڵ㣬���̳ɹ�������");
                this.IsStopFlow = true;
                return;
            }

            #region �жϽڵ��������
            try
            {
                // ���û������,��˵����,����Ϊ��ɽڵ����������.
                if (this.HisNode.IsCCNode == false)
                {
                    this.addMsg(SendReturnMsgFlag.OverCurr, string.Format("��ǰ����[{0}]�Ѿ����", this.HisNode.Name));
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
                        string CondInfo = "";
                        if (SystemConfig.IsDebug)
                            CondInfo = "@��ǰ����[" + this.HisNode.Name + "]�����������[" + this.HisNodeCompleteConditions.ConditionDesc + "],�Ѿ����.";
                        else
                            CondInfo = string.Format(this.ToE("WN6", "��ǰ����{0}�Ѿ����"), this.HisNode.Name);  //"@"; //��ǰ����[" + this.HisNode.Name + "],�Ѿ����.
                        this.addMsg(SendReturnMsgFlag.CondInfo, CondInfo);
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
                     this.HisWorkFlow.DoFlowOver(ActionType.FlowOver,"���������������");
                    this.IsStopFlow = true;
                    this.addMsg(SendReturnMsgFlag.OneNodeSendOver, "�����Ѿ��ɹ�����(һ�����̵Ĺ���)��", 
                        "�����Ѿ��ɹ�����(һ�����̵Ĺ���)�� @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>", SendReturnMsgType.Info);
                    return; 
                }

                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    string stopMsg = this.HisFlowCompleteConditions.ConditionDesc;
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver(ActionType.FlowOver, "���������������:"+stopMsg);
                    this.IsStopFlow = true;
                     
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                      this.addMsg(SendReturnMsgFlag.MacthFlowOver,"@���Ϲ��������������" + stopMsg + "" + overMsg ,
                          "@���Ϲ��������������" + stopMsg + "" + overMsg + " @�鿴<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>", SendReturnMsgType.Info);

                      return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@�ж�����{0}����������ִ���.") + ex.Message, this.HisNode.Name));
            }
            #endregion
        }
        #region ��������ڵ�

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
        /// <summary>
        /// ������߳���������˶�
        /// </summary>
        /// <param name="nd"></param>
        private void NodeSend_53_UnSameSheet_To_HeLiu(Node nd)
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

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr + "FID AND FK_Node=" + dbStr + "FK_Node";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow  SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("NodeName", nd.Name);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                // ��������������ϸ������.
                this.GenerHieLiuHuiZhongDtlData(nd);

                #region ���������
                Nodes fromNds = nd.FromNodes;
                string nearHLNodes = "";
                foreach (Node mynd in fromNds)
                {
                    if (mynd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                        nearHLNodes += "," + mynd.NodeID;
                }
                nearHLNodes = nearHLNodes.Substring(1);

                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node IN (" + nearHLNodes + ") AND FID=" + this.HisWork.FID + " AND IsPass=1";
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];
                decimal ok = (decimal)dt_worker.Rows.Count;

                ps = new Paras();
                ps.SQL = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
                ps.Add("FID", this.HisWork.FID);
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(ps);
                decimal passRate = ok / all * 100;
                numStr += "@���ǵ�(" + ok + ")����˽ڵ��ϵ�ͬ�£���������(" + all + ")�������̡�";
                if (nd.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    ps = new Paras();
                    ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID ";
                    ps.Add("FK_Node", nd.NodeID);
                    ps.Add("WorkID", this.HisWork.FID);
                    DBAccess.RunSQL(ps);
                    numStr += "@��һ������(" + nd.Name + ")�Ѿ�������";
                }
                #endregion ���������

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);
                this.addMsg("ToHeLiuInfo",
                    "@�����Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr);
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
            GenerWorkerLists gwls = this.Func_GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (GenerWorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "��";
                else
                    toEmpsStr += wl.FK_Emp + "(" + wl.FK_EmpText + ")��";

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
            ps = new Paras();
            ps.SQL = "SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID=" + dbStr + "OID";
            ps.Add("OID", this.HisWork.FID);
            DataTable dt = DBAccess.RunSQLReturnTable(ps);
            foreach (DataColumn dc in dt.Columns)
                mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.OID = this.HisWork.FID;
            mainWK.Insert();
            #endregion ���Ʊ�����������ݵ���������ȥ��

            #region ���Ƹ�����
            if (this.HisNode.MapData.FrmAttachments.Count != 0)
            {
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
            }
            #endregion ���Ƹ�����

            #region ����EleDB��
            if (this.HisNode.MapData.FrmEles.Count != 0)
            {
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
            }
            #endregion ����EleDB��

            // ��������������ϸ������.
            this.GenerHieLiuHuiZhongDtlData(nd);

            #endregion ��������ڵ������


            /* ��������Ҫ�ȴ�����������ȫ�����������ܿ�������*/
            string info = "";
            string sql1 = "";
#warning ���ڶ���ֺ�������ܻ������⡣
            ps = new Paras();
            ps.SQL = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + dbStr + "FID AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
            ps.Add("FID", this.HisWork.FID);
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(ps);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0,FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);
                info = "@��һ��������(" + nd.Name + ")�Ѿ�������";
            }
            else
            {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3,FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);
            }
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID;
            ps.Add("FK_Node", nd.NodeID);
            ps.Add("NodeName", nd.Name);
            ps.Add("WorkID", this.HisWork.FID);
            DBAccess.RunSQL(ps);

            if (myfh.FK_Node != nd.NodeID)
                this.addMsg("HeLiuInfo",
                    "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + "@���ǵ�һ������˽ڵ��ͬ��." + info);
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
            SpanSubTheadNodes_DG(toHLNode.FromNodes);
            if (_SpanSubTheadNodes == "")
                throw new Exception("��ȡ�ֺ���֮������߳̽ڵ㼯��Ϊ�գ�����������ƣ��ڷֺ���֮��Ľڵ��������Ϊ���߳̽ڵ㡣");
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

                SpanSubTheadNodes_DG(nd.FromNodes);
            }
        }
        /// <summary>
        /// ���߳�������㷢��
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithFID(Node nd)
        {
            //�����Ѿ�ͨ��.
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + dbStr + "WorkID AND FID=" + dbStr +"FID";
            ps.Add("WorkID",this.WorkID);
            ps.Add("FID", this.HisWork.FID);
            DBAccess.RunSQL(ps);

            string spanNodes = this.SpanSubTheadNodes(nd);
            if (nd.FromNodes.Count != 1)
            {
                NodeSend_53_UnSameSheet_To_HeLiu(nd);
                return null;
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

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" +dbStr + "WorkID AND FID=" +dbStr + "FID AND FK_Node=" + dbStr+"FK_Node";
                ps.Add("WorkID", this.WorkID);
                ps.Add("FID", this.HisWork.FID);
                ps.Add("FK_Node", this.HisNode.NodeID);
                DBAccess.RunSQL(ps);

                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkFlow  SET FK_Node=" + dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr + "WorkID";
                ps.Add("FK_Node", nd.NodeID);
                ps.Add("NodeName", nd.Name);
                ps.Add("WorkID", this.HisWork.OID);
                DBAccess.RunSQL(ps);

                /*
                 * ��θ��µ�ǰ�ڵ��״̬�����ʱ��.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region ���������

                ps = new Paras();
                ps.SQL = "SELECT FK_Emp,FK_EmpText FROM WF_GenerWorkerList WHERE FK_Node=" + dbStr+ "FK_Node AND FID=" + dbStr+ "FID AND IsPass=1";
                ps.Add("FK_Node", this.HisNode.NodeID);
                ps.Add("FID", this.HisWork.FID);
                DataTable dt_worker = BP.DA.DBAccess.RunSQLReturnTable(ps);
                string numStr = "@���·�����Ա��ִ�����:";
                foreach (DataRow dr in dt_worker.Rows)
                    numStr += "@" + dr[0] + "," + dr[1];
                decimal ok = (decimal)dt_worker.Rows.Count;

                ps = new Paras();
                ps.SQL="SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE   IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
                ps.Add("FID", this.HisWork.FID);
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(ps);
                decimal passRate = ok / all * 100;
                  numStr = "@���ǵ�(" + ok + ")����˽ڵ��ϵ�ͬ�£���������(" + all + ")�������̡�";
                if (nd.PassRate <= passRate)
                {
                    /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" +dbStr+"WorkID",
                        "FK_Node", nd.NodeID, "WorkID", this.HisWork.FID);
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
            GenerWorkerLists gwls = this.Func_GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (GenerWorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "��";
                else
                    toEmpsStr += wl.FK_Emp + "(" + wl.FK_EmpText + ")��";

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
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID="+dbStr+"OID",
                "OID",this.HisWork.FID );
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
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=0,  FID=0 WHERE FK_Node=" +dbStr+ "FK_Node AND WorkID=" +dbStr +"WorkID";
                ps.Add("FK_Node", nd.NodeID );
                ps.Add("WorkID",this.HisWork.OID);
                DBAccess.RunSQL(ps);
            }
            else
            {
#warning Ϊ�˲�������ʾ��;�Ĺ�����Ҫ�� =3 ���������Ĵ���ģʽ��
                ps = new Paras();
                ps.SQL = "UPDATE WF_GenerWorkerList SET IsPass=3, FID=0 WHERE FK_Node=" + dbStr + "FK_Node AND WorkID=" + dbStr+"WorkID";
                ps.Add("WorkID", this.HisWork.FID);
                ps.Add("FK_Node",nd.NodeID);
                DBAccess.RunSQL(ps);
            }
            ps = new Paras();
            ps.SQL = "UPDATE WF_GenerWorkFlow SET FK_Node=" +dbStr + "FK_Node,NodeName=" + dbStr + "NodeName WHERE WorkID=" + dbStr+"WorkID";
            ps.Add("FK_Node", nd.NodeID);
            ps.Add("NodeName",nd.Name);
            ps.Add("WorkID",this.HisWork.FID);
            DBAccess.RunSQL(ps);
            #endregion ���ø�����״̬

            return "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + "@���ǵ�һ������˽ڵ��ͬ��.";
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
            Nodes nds = this.HisNode.FromNodes;
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
            Nodes nds = this.HisNode.FromNodes;
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
            if (nd1.FromNodes.Contains(NodeAttr.NodeID, nd2.NodeID))
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
                GenerWorkerLists wls = new GenerWorkerLists(this.HisWork.OID, nd.NodeID);
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
                    ps = new Paras();
                    ps.SQL = "DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + dbStr + "WorkID AND FK_Node=" +dbStr+"FK_Node ";
                    ps.Add("WorkID", this.HisWork.OID);
                    ps.Add("FK_Node",nd.NodeID);
                    DBAccess.RunSQL(ps);
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
                if (nd.HisRunModel == RunModel.SubThread )
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
