using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;

namespace BP.EIP.DAL
{
    public partial class DepartmentDAL : IDepartment
    {

        public System.Data.DataTable GetDTByParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTInner(string departmentId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetStaffs(string departmentId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetParentStaffs(string parentId)
        {
            throw new NotImplementedException();
        }

        public int MoveTo(string departmentId, string parentId)
        {
            throw new NotImplementedException();
        }

        public int BatchMoveTo(string[] departmentIds, string parentId)
        {
            throw new NotImplementedException();
        }

        public int BatchSetCode(string[] ids, string[] codes)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string No)
        {
            throw new NotImplementedException();
        }

        public void Add(BaseEntity entity, out string statusCode, out string statusMessage)
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

        public int Update(BaseEntity entity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
