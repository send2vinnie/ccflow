using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;

namespace BP.EIP.DAL
{
    public partial class StaffDAL : IStaff
    {

        public System.Data.DataTable GetAddressDT(string departmentId, string searchValue)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAddressDT(string departmentId, bool containChildren)
        {
            throw new NotImplementedException();
        }

        public int UpdateAddress(Port_Staff staffEntity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchUpdateAddress(List<Port_Staff> staffEntites, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int SetStaffUser(string staffId, string userId)
        {
            throw new NotImplementedException();
        }

        public int DeleteStaffUser(string staffId)
        {
            throw new NotImplementedException();
        }

        public int MoveTo(string id, string departmentId)
        {
            throw new NotImplementedException();
        }

        public int BatchMoveTo(string[] ids, string departmentId)
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
