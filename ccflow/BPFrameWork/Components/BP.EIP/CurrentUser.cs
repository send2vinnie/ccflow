using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.EIP
{
    public class CurrentUser : BP.Web.WebUser
    {
        public CurrentUser()
        {

        }
        public CurrentUser(string userno)
        {
            BP.EIP.Port_Emp user = new Port_Emp(userno);
            this.No = userno;
            this.UserName = user.Name;
        }
        private string _userNo;
        public string No
        {
            get
            {
                return BP.Web.WebUser.No;
            }
            set
            {
                _userNo = value;
                BP.Web.WebUser.No = _userNo;
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return BP.Web.WebUser.Name;
            }
            set
            {
                _userName = value;
                BP.Web.WebUser.Name = _userName;
            }
        }
    }
}
