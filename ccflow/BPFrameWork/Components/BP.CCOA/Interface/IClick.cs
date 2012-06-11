﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.CCOA.Enum;

namespace BP.CCOA.Interface
{
    public interface IClick
    {
        void ClickRecord(ClickObjType objectType, string objectId, string visitId);

        bool IsReaded(string objectId, string visitId);

        string GetVisitTime(string objectId, string visitId);

        DataTable GetObjectRecord(string objectId);

        DataTable GetVisitedRecord(string visitId);

        bool SetAllRead(string visitId, ClickObjType objectType);
    }
}
