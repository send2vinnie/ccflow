using System;
using System.Data;
using BP.DA;
using BP.En;


namespace BP.GE
{
    public class DKFileAdds
    {

        private static string fk_dkdir;
        private static string fk_dept;
        private static string fk_emp;
        private static string fileaddress;

        public static string FK_DKDir
        {
            get
            {
                return fk_dkdir;
            }
            set
            {
                fk_dkdir = value;
            }
        }

        public static string FK_Dept
        {
            get
            {
                return fk_dept;
            }
            set
            {
                fk_dept = value;
            }
        }
        public static string FK_Emp
        {
            get
            {
                return fk_emp;
            }
            set
            {
                fk_emp = value;
            }
        }

        public static string FileAddress
        {
            get
            {
                return fileaddress;
            }
            set
            {
                fileaddress = value;
            }
        } 
    
    }
    

}
 

