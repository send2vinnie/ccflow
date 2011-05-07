using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

/// <summary>
///DA 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class DA : System.Web.Services.WebService {

    public DA () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(string text)
    {
        return "Hello World" + text;
    }

    [WebMethod]
    public int RunSQL(string sql)
    {
        return BP.DA.DBAccess.RunSQL(sql);
    }

    [WebMethod]
    public DataTable RunSQLReturnTable(string sql)
    {
        return BP.DA.DBAccess.RunSQLReturnTable(sql);
    }
}
