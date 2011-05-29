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
public class Designer : System.Web.Services.WebService {

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
    /// <summary>
    /// 编辑线
    /// </summary>
    /// <param name="oid"></param>
    /// <param name="fk_mapdata"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="borderWidth"></param>
    /// <param name="borderColor"></param>
    /// <param name="borderStyle"></param>
    [WebMethod(EnableSession = true)]
    public void EditLine(int oid, string fk_mapdata, int x1, int y1, int x2, int y2, int borderWidth, string borderColor, string borderStyle)
    {
        BP.Sys.FrmLine line = new BP.Sys.FrmLine();
        line.OID = oid;
        line.FK_MapData = fk_mapdata;
        line.X1 = x1;
        line.Y1 = y1;
        line.X2 = x2;
        line.Y2 = y2;

        line.BorderColor = borderColor;
        line.BorderStyle = borderStyle;
        line.BorderWidth = borderWidth;
        line.Save();
    }
    [WebMethod(EnableSession = true)]
    public void EditLab(int oid, string fk_mapdata, int x, int y, string frontColor,
        string frontName, string frontWeight)
    {
        BP.Sys.FrmLab en = new BP.Sys.FrmLab();
        en.OID = oid;
        en.FK_MapData = fk_mapdata;
        en.X = x;
        en.Y = y;

        en.FrontColor = frontColor;
        en.FrontName = frontName;
        en.FrontWeight = frontWeight;
        en.Save();
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
