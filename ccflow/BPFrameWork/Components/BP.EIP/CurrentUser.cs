using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.EIP
{
    public class CurrentUser : BP.Web.WebUser
    {
        public string No
        {
            get
            {
                return BP.Web.WebUser.No;
            }
        }
    }
}
