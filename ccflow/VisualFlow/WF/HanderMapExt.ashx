<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BP.Web;
using System.Configuration;
using System.Web.SessionState;

public class Handler : IHttpHandler, IRequiresSessionState  
{
    string no;
    string name;
    string fk_dept;
    public string DealSQL(string sql, string key)
    {
        sql = sql.Replace("@Key", key);
        sql = sql.Replace("@key", key);
        sql = sql.Replace("@Val", key);
        sql = sql.Replace("@val", key);

        //sql = sql.Replace("@WebUser.No", no);
        //sql = sql.Replace("@WebUser.Name", name);
        //sql = sql.Replace("@WebUser.FK_Dept", fk_dept);

        sql = sql.Replace("@WebUser.No", WebUser.No);
        sql = sql.Replace("@WebUser.Name", WebUser.Name);
        sql = sql.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
        return sql;
    }
    public void ProcessRequest(HttpContext context)
    {
        string fk_mapExt = context.Request.QueryString["FK_MapExt"].ToString();
        if (context.Request.QueryString["Key"] == null)
            return;
        no=context.Request.QueryString["WebUserNo"];
        name = context.Request.QueryString["WebUserName"];
        fk_dept = context.Request.QueryString["WebUserFK_Dept"];

        BP.Sys.MapExt me = new BP.Sys.MapExt(fk_mapExt);
       DataTable dt = null;
        string sql = "";
        string key=context.Request.QueryString["Key"];
        key = System.Web.HttpUtility.UrlDecode(key,            System.Text.Encoding.GetEncoding("GB2312"));
        key = key.Trim();
       // key = "周";        switch (me.ExtType)
        {
            case BP.Sys.MapExtXmlList.DDLFullCtrl: // 级连菜单。
                sql = this.DealSQL(me.DocOfSQLDeal, key);
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                context.Response.Write(JSONTODT(dt));
                return;
            case BP.Sys.MapExtXmlList.ActiveDDL: // 级连菜单。
                sql = this.DealSQL(me.DocOfSQLDeal, key);
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                context.Response.Write(JSONTODT(dt));
                return;
            case BP.Sys.MapExtXmlList.TBFullCtrl: // 自动完成。
                switch (context.Request.QueryString["DoType"])
                {
                    case "ReqCtrl":
                        // 获取填充 ctrl 值的信息.
                        sql = this.DealSQL(me.DocOfSQLDeal, key);
                        dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                        context.Response.Write(JSONTODT(dt));
                        break;
                    case "ReqDtlFullList":
                        /* 获取填充的明细表集合. */
                        DataTable dtDtl = new DataTable("Head");
                        dtDtl.Columns.Add("Dtl", typeof(string));
                        
                        string[] strsDtl = me.Tag1.Split('$');
                        foreach (string str in strsDtl)
                        {
                            if (str == "" || str == null)
                                continue;

                            string[] ss = str.Split(':');
                            DataRow dr = dtDtl.NewRow();
                            dr[0] = ss[0];
                            dtDtl.Rows.Add(dr);
                        }
                        context.Response.Write(JSONTODT(dtDtl));
                        break;
                    case "ReqDDLFullList":
                        /* 获取要个性化填充的下拉框. */
                        DataTable dt1 = new DataTable("Head");
                        dt1.Columns.Add("DDL", typeof(string));
                        //    dt1.Columns.Add("SQL", typeof(string));
                        string[] strs = me.Tag.Split('$');
                        foreach (string str in strs)
                        {
                            if (str == "" || str == null)
                                continue;

                            string[] ss = str.Split(':');
                            DataRow dr = dt1.NewRow();
                            dr[0] = ss[0];
                            // dr[1] = ss[1];
                            dt1.Rows.Add(dr);
                        }
                        context.Response.Write(JSONTODT(dt1));
                        break;
                    case "ReqDDLFullListDB":
                        /* 获取要个性化填充的下拉框的值. 根据已经传递过来的 ddl id. */
                        string myDDL = context.Request.QueryString["MyDDL"];
                        sql = "";
                        string[] strs1 = me.Tag.Split('$');
                        foreach (string str in strs1)
                        {
                            if (str == "" || str == null)
                                continue;

                            string[] ss = str.Split(':');
                            if (ss[0] == myDDL)
                            {
                                sql = ss[1];
                                sql = this.DealSQL(sql, key);
                                break;
                            }
                        }
                        dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                        context.Response.Write(JSONTODT(dt));
                        break;
                    default:
                        sql = this.DealSQL(me.DocOfSQLDeal, key);
                        dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                        context.Response.Write(JSONTODT(dt));
                        break;
                }
                return;
            default:
                break;
        }
        //string strKey = context.Request.QueryString["key"].ToString();
        //if (strKey == string.Empty)
        //{
        //    context.Response.Write(string.Empty);
        //    return;
        //}
        //string
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
    //public static string ToJson(DataSet dataSet)
    //{
    //    string jsonString = "{";
    //    foreach (DataTable table in dataSet.Tables)
    //    {
    //        jsonString += "\"" + ToJson(table.TableName) + "\":" + ToJson(table) + ",";
    //    }
    //    return jsonString = DeleteLast(jsonString) + "}";
    //}
    
    public string JSONTODT(DataTable dt)
    {
        
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName.ToLower() == "no")
                dc.ColumnName = "No";
            if (dc.ColumnName.ToLower() == "name")
                dc.ColumnName = "Name";
        }
        
        StringBuilder JsonString = new StringBuilder();
        if (dt != null && dt.Rows.Count > 0)
        {
            JsonString.Append("{ ");
            JsonString.Append("\"Head\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JsonString.Append("{ ");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                    }
                }
                /**/
                /*end Of String*/
                if (i == dt.Rows.Count - 1)
                {
                    JsonString.Append("} ");
                }
                else
                {
                    JsonString.Append("}, ");
                }
            }
            JsonString.Append("]}");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
}

