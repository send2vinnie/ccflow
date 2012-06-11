using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CCOA.Interface;

namespace BP.CCOA
{
    public class ClickHelper : IClick
    {
        public void ClickRecord(string objectId, string visitId)
        {
            throw new NotImplementedException();
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
