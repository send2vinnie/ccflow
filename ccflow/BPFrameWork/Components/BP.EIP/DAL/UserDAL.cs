/***************************************************
 * Author:王晓伟
 * Date：2012-5-29
 * Description：User（用户）数据访问类
 * *************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;
using System.Data;
using BP.EIP.Enum;
using BP.EIP.Enum;

namespace BP.EIP.DAL
{
    internal class UserDAL : IUser
    {
        public bool ExistsName(string userName)
        {
            BP.EIP.Port_Emp model = new Port_Emp();
            model.RetrieveByAttr("Name", userName);

            if (model.Row.Count == 0)
            {
                return false;
            }
            return true;
        }

        public System.Data.DataTable GetDTByDept(string departmentNo, bool containChildren)
        {
            DataTable dt = new DataTable();
            if (containChildren)
            {

            }
            else
            {
                string strSQL = @"SELECT * FROM PORT_EMPDEPT A 
                                  LEFT JOIN PORT_EMP B ON A.FK_EMP = B.NO
                                  WHERE A.FK_DEPT = '{0}'";
                strSQL = string.Format(strSQL, departmentNo);

                return DBAccess.RunSQLReturnTable(strSQL);
            }

            return dt;
        }

        public System.Data.DataTable GetDTByRole(string roleId)
        {
            string strSQL = @"SELECT *  FROM PORT_EMPSTATION A
                              LEFT JOIN PORT_EMP B ON A.FK_EMP = B.NO
                              WHERE A.FK_STATION = '{0}'";

            strSQL = string.Format(strSQL, roleId);

            return DBAccess.RunSQLReturnTable(strSQL);
        }

        public System.Data.DataTable GetDTByStatus(Enum.AuditStatus status)
        {
            int intStatus = (int)status;
            string strSQL = @"SELECT * FROM EMP_EMP WHERE STATUS ='" + intStatus + "'";

            return DBAccess.RunSQLReturnTable(strSQL);
        }

        public int SetDefaultRole(string userId, string roleId)
        {
            string strSQL = "UPDATE PORT_EMPSTATION SET FK_STATION='{0}' WHERE FK_EMP='{1}'";
            strSQL = string.Format(strSQL, roleId, userId);

            return DBAccess.RunSQL(strSQL);
        }

        public int BatchSetDefaultRole(string[] userIds, string roleId)
        {
            string strSQL = "UPDATE PORT_EMPSTATION SET FK_STATION='{0}' WHERE FK_EMP IN ({1})";
            string arrUserIds = Tool.ArrayToString(userIds);

            strSQL = string.Format(strSQL, roleId, arrUserIds);

            return DBAccess.RunSQL(strSQL);
        }

        public string[] GetUserRoleIds(string userId)
        {
            string strSQL = @"SELECT FK_STATION FROM PORT_EMPSTATION WHERE FK_EMP='{0}'";
            strSQL = string.Format(strSQL, userId);

            DataTable dt = DBAccess.RunSQLReturnTable(strSQL);
            if (dt == null || dt.Rows.Count == 0)
            {
                return new string[] { };
            }
            string[] roleIds = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleIds[i] = dt.Rows[i]["FK_STATION"].ToString();
            }

            return roleIds;
        }

        public int AddUserToRole(string userId, string[] addToRoleIds)
        {
            StringBuilder sbrSQL = new StringBuilder();
            foreach (var roleId in addToRoleIds)
            {
                string guid = Guid.NewGuid().ToString();
                sbrSQL.AppendFormat("INSERT INTO PORT_EMPSTATION(NO,FK_EMP,FK_STATION) VALUES({0},{1},{2});",
                    guid, userId, roleId);
            }

            return DBAccess.RunSQL(sbrSQL.ToString());
        }

        public int RemoveUserFromRole(string userId, string[] removeRoleIds)
        {
            string roleIds = Tool.ArrayToString(removeRoleIds);

            string strSQL = @"DELETE PORT_EMPSTATION WHERE FK_EMP ='{0}' AND FK_STATSION IN {1}";
            strSQL = string.Format(strSQL, userId, roleIds);

            return DBAccess.RunSQL(strSQL);
        }

        public System.Data.DataTable GetLoginUserDT()
        {
            string strSQL = @"SELECT * FROM PORT_EMP WHERE ISLOGIN = '1'";

            return DBAccess.RunSQLReturnTable(strSQL);
        }

        public CurrentUser AccountActivation(string uid, out StatusCode statusCode, out string statusMessage)
        {
            string strSQL = @"UPDATE PORT_EMP SET Status='1' WHERE NO = '{0}'";
            strSQL = string.Format(strSQL, uid);

            ///TODO:没有实现

            CurrentUser curUser = new CurrentUser();

            statusCode = Enum.StatusCode.Success;
            statusMessage = "";

            return curUser;
        }

        public CurrentUser LoginByUid(string uid, out StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public CurrentUser LoginByUserName(string userName, out StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public CurrentUser UserLogOn(string userName, string password, out StatusCode statusCode, out string statusMessage)
        {
            CurrentUser curUser = new CurrentUser();
            Port.Emp user = new Port.Emp();
            user.RetrieveByAttr("Name", userName);
            if (user.Name != userName)
            {
                statusCode = Enum.StatusCode.Error;
                statusMessage = "用户不存在！";
                return null;
            }
            if (user.Pass != password)
            {
                statusCode = Enum.StatusCode.Error;
                statusMessage = "用户名或密码不正确！";
                return null;
            }

            try
            {
                BP.Web.WebUser.SignInOfGener(user);
                statusCode = Enum.StatusCode.Success;
                statusMessage = "";
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
                statusCode = Enum.StatusCode.Exception;
            }

            return curUser;
        }

        public CurrentUser UserLogOn(string userName, string password, string ipAddress, out StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            BP.Web.WebUser.Exit();
        }

        public int ServerCheckOnLine()
        {
            string strSQL = @"SELECT * FROM PORT_EMP WHERE ISLOGIN = '1'";

            return DBAccess.RunSQLReturnCOUNT(strSQL);
        }

        public int SetPassword(string[] userIds, string password, out StatusCode statusCode, out string statusMessage)
        {
            string strSQL = "UPDATE PORT_EMP SET PASS = '{0}' WHERE NO IN {1}";
            string arrUserIds = Tool.ArrayToString(userIds);
            strSQL = string.Format(strSQL, password, arrUserIds);

            try
            {
                statusCode = StatusCode.Success;
                statusMessage = "";
                return DBAccess.RunSQL(strSQL);
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.Message;
            }

            return 0;
        }

        public int ChangePassword(string userId, string oldPassword, string newPassword, out StatusCode statusCode, out string statusMessage)
        {
            Port_Emp emp = new Port_Emp(userId);
            if (emp.Pass != oldPassword)
            {
                statusCode = Enum.StatusCode.Error;
                statusMessage = "原始密码输入不正确，请重新输入！";
                return 0;
            }
            string strSQL = "UPDATE PORT_EMP SET PASS = '{0}' WHERE NO='{1}'";
            strSQL = string.Format(strSQL, newPassword, userId);

            try
            {
                statusCode = StatusCode.Success;
                statusMessage = "";
                return DBAccess.RunSQL(strSQL);
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.Message;
            }

            return 0;
        }

        public bool Exists(string No)
        {
            throw new NotImplementedException();
        }

        public void Add(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public BaseEntity GetEntity(string No)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            throw new NotImplementedException();
        }

        public int Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public int BatchDelete(string[] ids)
        {
            throw new NotImplementedException();
        }

        public int SetDeleted(string[] ids)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Search(string searchValue)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
