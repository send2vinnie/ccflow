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
///Designer 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class Designer : System.Web.Services.WebService
{
    #region 数据库访问
    /// <summary>
    /// 运行sql返回table.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="isLogin"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string RunSQLReturnTable(string sql)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
        return Connector.ToXml(ds);
    }
    #endregion 数据库访问


    public Designer()
    {
    }
    #region 设计器的方法
    /// <summary>
    /// 运行sql返回 dataset .
    /// </summary>
    /// <param name="sql">要执行的sql</param>
    /// <returns>ds</returns>
    public string RunSQLReturnDS(string sql)
    {
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        return null;
        //   return Connector.ToXml(BP.DA.DBAccess.RunSQLReturnDataSet(sql));
    }
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="fk_mapdata"></param>
    /// <returns></returns>
    public string Lines(string fk_mapdata)
    {
        string sql = "SELECT * FROM Sys_FrmLine WHERE FK_MapData='" + fk_mapdata + "'";
        return RunSQLReturnDS(sql);
    }
    public string Labs(string fk_mapdata)
    {
        string sql = "SELECT * FROM Sys_FrmLab WHERE FK_MapData='" + fk_mapdata + "'";
        return RunSQLReturnDS(sql);
    }
    public string MapAttrs(string fk_mapdata)
    {
        string sql = "SELECT * FROM Sys_FrmAttr WHERE FK_MapData='" + fk_mapdata + "'";
        return RunSQLReturnDS(sql);
    }
    [WebMethod(EnableSession = true)]
    public void EditLab(int oid, string fk_mapdata, int x, int y, string frontColor,
        string frontName, string frontWeight)
    {
        //BP.Sys.FrmLab en = new BP.Sys.FrmLab();
        //en.OID = oid;
        //en.FK_MapData = fk_mapdata;
        //en.X = x;
        //en.Y = y;

        //en.FrontColor = frontColor;
        //en.FrontName = frontName;
        //en.FrontWeight = frontWeight;
        //en.Save();
    }
    /// <summary>
    /// 更新字段.
    /// </summary>
    /// <param name="mypk"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [WebMethod(EnableSession = true)]
    public void EditMapAttr(string mypk, int x, int y)
    {
        BP.DA.DBAccess.RunSQL("UPDATE Sys_MapAttr Set x=" + x + " ,Y=" + y + " where mypk='" + mypk + "'");
    }

    [WebMethod(EnableSession = true)]
    public void DelLine(int oid)
    {
        BP.DA.DBAccess.RunSQL("DELETE Sys_FrmLine WHERE OID='" + oid + "'");
    }
    [WebMethod(EnableSession = true)]
    public void DelLab(int oid)
    {
        BP.DA.DBAccess.RunSQL("DELETE Sys_FrmLabel WHERE OID='" + oid + "'");
    }
    [WebMethod(EnableSession = true)]
    public void DelMapAttr(string mypk)
    {
        BP.DA.DBAccess.RunSQL("DELETE Sys_MapAttr WHERE mypk='" + mypk + "'");
    }
    #endregion 设计器的方法
    
}
