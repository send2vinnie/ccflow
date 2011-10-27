using System;
using System.Collections.Generic;
using System.Data;
using BP.DA;
using System.Web;
using System.Web.Services;

/// <summary>
///DocFlow 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class DocFlow : System.Web.Services.WebService {

    public DocFlow () {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 获取webconfig 中的值.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetSettingByKey(string key)
    {
        return BP.SystemConfig.AppSettings[key];
    }

    #region 功能执行
    [WebMethod]
    public string DoType(string type, object p1, object p2, object p3, object p4, object p5, object p6,object p7)
    {
        switch (type)
        {
            case "DeleteFlow":
                break;
            default:
                break;
        }
        return null;
    }
    #endregion

    #region 数据库交互
    /// <summary>
    /// 运行sql 返回一个string.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [WebMethod]
    public string RunSQLReturnString(string sql)
    {
        return BP.DA.DBAccess.RunSQLReturnString(sql);
    }

    /// <summary>
    /// 运行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    [WebMethod]
    public int RunSQL(string sql)
    {
        return BP.DA.DBAccess.RunSQL(sql);
    }
    /// <summary>
    /// 运行sql 返回datatable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable RunSQLReturnTable(string sql)
    {
        return BP.DA.DBAccess.RunSQLReturnTable(sql);
    }
    /// <summary>
    /// 产生OID
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public int GenerOID()
    {
        return BP.DA.DBAccess.GenerOID();
    }
    #endregion 数据库交互

}
