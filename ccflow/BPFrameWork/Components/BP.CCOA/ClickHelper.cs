using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Interface;
using BP.CCOA.Enum;

namespace BP.CCOA
{
    public class ClickHelper : IClick
    {
        public void ClickRecord(ClickObjType objectType,string objectId, string visitId)
        {
            BP.CCOA.OA_ClickRecords click = new OA_ClickRecords();
            click.No = Guid.NewGuid().ToString();
            click.ObjectId = objectId;
            click.ObjectType = (int)objectType;
            click.VisitId = visitId;
            click.VisitDate = DateTime.Now.ToString();

            click.Insert();
        }

        public bool IsReaded(string objectId, string visitId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetReadedList(Enum.ClickObjType objType, string visitId)
        {
            throw new NotImplementedException();
        }

        public string GetVisitTime(string objectId, string visitId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetObjectRecord(string objectId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetVisitedRecord(string visitId)
        {
            throw new NotImplementedException();
        }
    }
}
