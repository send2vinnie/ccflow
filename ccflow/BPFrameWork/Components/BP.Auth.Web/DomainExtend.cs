using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.DomainServices.EntityFramework;

namespace BP.Auth.Web
{
    public partial class OrgDomainService : LinqToEntitiesDomainService<PlantEntities>
    {
        public string GetMaxEmpId()
        {
            var result = from index in ObjectContext.Port_Emp

                         select index.No;
            List<int> templist = new List<int>();
            foreach (var item in result)
            {
                templist.Add(Int32.Parse(item));
            }
            return templist.Max().ToString("00");
        }

        public string GetSpell(string Chinese)
        {
            return Hz2Py.Convert(Chinese);
        }

        public IQueryable<Port_Emp> GetEmpByStationNo(string StationNo, string DeptNo)
        {
            var result = from es in ObjectContext.Port_EmpStation
                         join ed in ObjectContext.Port_EmpDept
                         on es.FK_Emp equals ed.FK_Emp
                         join e in ObjectContext.Port_Emp
                         on es.FK_Emp equals e.No
                         where es.FK_Station == StationNo && ed.FK_Dept == DeptNo
                         select e;
            return result;
        }
    }
}