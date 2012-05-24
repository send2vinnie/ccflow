using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.EIP
{
    public class BaseEntity : BP.En.EntitiesNo
    {
        public override En.Entity GetNewEntity
        {
            get { throw new NotImplementedException(); }
        }
    }
}
