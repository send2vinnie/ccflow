
namespace BP.Auth.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the PlantEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class OrgDomainService : LinqToEntitiesDomainService<PlantEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Port_Dept' query.
        public IQueryable<Port_Dept> GetPort_Dept()
        {
            return this.ObjectContext.Port_Dept;
        }

        public void InsertPort_Dept(Port_Dept port_Dept)
        {
            if ((port_Dept.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(port_Dept, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Port_Dept.AddObject(port_Dept);
            }
        }

        public void UpdatePort_Dept(Port_Dept currentPort_Dept)
        {
            this.ObjectContext.Port_Dept.AttachAsModified(currentPort_Dept, this.ChangeSet.GetOriginal(currentPort_Dept));
        }

        public void DeletePort_Dept(Port_Dept port_Dept)
        {
            if ((port_Dept.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Port_Dept.Attach(port_Dept);
            }
            this.ObjectContext.Port_Dept.DeleteObject(port_Dept);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Port_Emp' query.
        public IQueryable<Port_Emp> GetPort_Emp()
        {
            return this.ObjectContext.Port_Emp;
        }

        public void InsertPort_Emp(Port_Emp port_Emp)
        {
            if ((port_Emp.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(port_Emp, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Port_Emp.AddObject(port_Emp);
            }
        }

        public void UpdatePort_Emp(Port_Emp currentPort_Emp)
        {
            this.ObjectContext.Port_Emp.AttachAsModified(currentPort_Emp, this.ChangeSet.GetOriginal(currentPort_Emp));
        }

        public void DeletePort_Emp(Port_Emp port_Emp)
        {
            if ((port_Emp.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Port_Emp.Attach(port_Emp);
            }
            this.ObjectContext.Port_Emp.DeleteObject(port_Emp);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Port_EmpDept' query.
        public IQueryable<Port_EmpDept> GetPort_EmpDept()
        {
            return this.ObjectContext.Port_EmpDept;
        }

        public void InsertPort_EmpDept(Port_EmpDept port_EmpDept)
        {
            if ((port_EmpDept.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(port_EmpDept, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Port_EmpDept.AddObject(port_EmpDept);
            }
        }

        public void UpdatePort_EmpDept(Port_EmpDept currentPort_EmpDept)
        {
            this.ObjectContext.Port_EmpDept.AttachAsModified(currentPort_EmpDept, this.ChangeSet.GetOriginal(currentPort_EmpDept));
        }

        public void DeletePort_EmpDept(Port_EmpDept port_EmpDept)
        {
            if ((port_EmpDept.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Port_EmpDept.Attach(port_EmpDept);
            }
            this.ObjectContext.Port_EmpDept.DeleteObject(port_EmpDept);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Port_EmpStation' query.
        public IQueryable<Port_EmpStation> GetPort_EmpStation()
        {
            return this.ObjectContext.Port_EmpStation;
        }

        public void InsertPort_EmpStation(Port_EmpStation port_EmpStation)
        {
            if ((port_EmpStation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(port_EmpStation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Port_EmpStation.AddObject(port_EmpStation);
            }
        }

        public void UpdatePort_EmpStation(Port_EmpStation currentPort_EmpStation)
        {
            this.ObjectContext.Port_EmpStation.AttachAsModified(currentPort_EmpStation, this.ChangeSet.GetOriginal(currentPort_EmpStation));
        }

        public void DeletePort_EmpStation(Port_EmpStation port_EmpStation)
        {
            if ((port_EmpStation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Port_EmpStation.Attach(port_EmpStation);
            }
            this.ObjectContext.Port_EmpStation.DeleteObject(port_EmpStation);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Port_Station' query.
        public IQueryable<Port_Station> GetPort_Station()
        {
            return this.ObjectContext.Port_Station;
        }

        public void InsertPort_Station(Port_Station port_Station)
        {
            if ((port_Station.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(port_Station, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Port_Station.AddObject(port_Station);
            }
        }

        public void UpdatePort_Station(Port_Station currentPort_Station)
        {
            this.ObjectContext.Port_Station.AttachAsModified(currentPort_Station, this.ChangeSet.GetOriginal(currentPort_Station));
        }

        public void DeletePort_Station(Port_Station port_Station)
        {
            if ((port_Station.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Port_Station.Attach(port_Station);
            }
            this.ObjectContext.Port_Station.DeleteObject(port_Station);
        }
    }
}


