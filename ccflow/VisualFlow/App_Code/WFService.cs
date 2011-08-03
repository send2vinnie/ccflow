using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using Silverlight.DataSetConnector;
using System.Data;
using System.Data.SqlClient;
using System.IO;
/// <summary>
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WFService : System.Web.Services.WebService
{
    public WFService()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    /// <summary>
    /// 获取发起流程的列表
    /// </summary>
    /// <param name="userNo">用户编号</param>
    /// <returns>datatable</returns>
    [WebMethod]
    public DataTable DB_GenerCanStartFlowsOfDataTable(string userNo)
    {
        return Dev2Interface.DB_GenerCanStartFlowsOfDataTable(userNo);
    }
    /// <summary>
    /// 获取当前工作的列表
    /// </summary>
    /// <param name="userNo">用户编号</param>
    /// <returns>datatable</returns>
    [WebMethod]
    public DataTable DB_GenerEmpWorksOfDataTable(string userNo)
    {
        return Dev2Interface.DB_GenerEmpWorksOfDataTable(userNo);
    }
    /// <summary>
    /// 获取在途工作的列表
    /// </summary>
    /// <param name="userNo">用户编号</param>
    /// <returns>datatable</returns>
    [WebMethod]
    public DataTable DB_GenerRuningOfDataTable(string userNo)
    {
        return Dev2Interface.DB_GenerRuningOfDataTable(userNo);
    }
}
