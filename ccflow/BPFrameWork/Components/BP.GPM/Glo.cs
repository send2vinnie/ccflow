using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.GPM
{
    public class Glo
    {
        /// <summary>
        /// 安装
        /// </summary>
        public static void Install()
        {

        }
        /// <summary>
        /// 是否可以执行判断
        /// </summary>
        /// <param name="obj">判断对象</param>
        /// <param name="cw">方式</param>
        /// <returns>是否可以执行</returns>
        public static bool IsCanDoIt(object obj, BP.GPM.CtrlWay cw)
        {
            int n = 0;
            string sql = "";
            switch (cw)
            {
                case CtrlWay.AnyOne:
                    return true;
                case CtrlWay.ByStation:
                    sql = "SELECT count(*) FROM GPM_ByStation WHERE RefObj='" + obj + "'  AND FK_Station IN (select fk_station from Port_EmpStation where FK_Emp='" + BP.Web.WebUser.No + "')";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                case CtrlWay.ByDept:
                    sql = "SELECT count(*) FROM GPM_ByDept WHERE RefObj='" + obj + "'  AND FK_Dept IN (select FK_Dept from Port_EmpDept WHERE FK_Emp='" + BP.Web.WebUser.No + "')";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                case CtrlWay.ByEmp:
                    sql = "SELECT count(*) FROM GPM_ByEmp WHERE RefObj='" + obj + "'  AND  FK_Emp='" + BP.Web.WebUser.No + "'";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                default:
                    break;
            }

            if (n == 0)
                return false;
            else
                return true;
        }
    }
}
