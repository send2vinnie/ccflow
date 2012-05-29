using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.EIP.Enum;

namespace BP.EIP.DAL
{
    public partial class RoleDAL:IRole
    {

        public System.Data.DataTable GetDTByDept(string departmentId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTUserByRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public int ClearRoleUser(string roleId)
        {
            throw new NotImplementedException();
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
