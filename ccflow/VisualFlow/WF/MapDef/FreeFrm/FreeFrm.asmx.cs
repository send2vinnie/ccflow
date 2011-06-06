using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using BP.DA;
using BP.Web;
using BP.En;
using BP.WF;
using Silverlight.DataSetConnector;

namespace FreeFrm.Web
{
    /// <summary>
    /// DA 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class FreeFrm : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 运行sqls
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public int RunSQLs(string sqls)
        {
            if (string.IsNullOrEmpty(sqls))
                return 0;

            int i = 0;
            string[] strs = sqls.Split('@');
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                i += BP.DA.DBAccess.RunSQL(str);
            }
            return i;
        }
        /// <summary>
        /// 运行sql返回table.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string RunSQLReturnTable(string sql)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
            return Connector.ToXml(ds);
            //Silverlight.DataSet ds = new DataSet();
            //ds.Tables.Add( BP.DA.DBAccess.RunSQLReturnTable(sql));
            //return Connector.ToXml(ds);
        }
        [WebMethod(EnableSession = true)]
        public string RequestSFTable(string ensName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (ensName.Contains("."))
            {
                Entities ens = BP.DA.ClassFactory.GetEns(ensName);
                ens.RetrieveAll();
                dt = ens.ToDataTableField();
                ds.Tables.Add(dt);
            }
            else
            {
                string sql = "SELECT No,Name FROM " + ensName;
                ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
            }
            return Connector.ToXml(ds);
        }
        /// <summary>
        /// 获取一个Frm
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string GetFrm(string fk_mapdata)
        {
            return GetFrm_P(fk_mapdata);
        }
        private string GetFrm_P(string fk_mapdata)
        {
            DataSet ds = new DataSet();
            // line.
            BP.Sys.FrmLines lins = new BP.Sys.FrmLines(fk_mapdata);
            DataTable dt = lins.ToDataTableField();
            dt.TableName = "Sys_FrmLine";
            ds.Tables.Add(dt);

            // link.
            BP.Sys.FrmLinks liks = new BP.Sys.FrmLinks(fk_mapdata);
            DataTable dtLink = liks.ToDataTableField();
            dtLink.TableName = "Sys_FrmLink";
            ds.Tables.Add(dtLink);

            // Img
            BP.Sys.FrmImgs imgs = new BP.Sys.FrmImgs(fk_mapdata);
            DataTable imgDt = imgs.ToDataTableField();
            imgDt.TableName = "Sys_FrmImg";
            ds.Tables.Add(imgDt);

            // Sys_FrmLab
            BP.Sys.FrmLabs labs = new BP.Sys.FrmLabs(fk_mapdata);
            DataTable dtlabs = labs.ToDataTableField();
            dtlabs.TableName = "Sys_FrmLab";
            ds.Tables.Add(dtlabs);

            // Sys_FrmRB
            BP.Sys.FrmRBs rbs = new BP.Sys.FrmRBs(fk_mapdata);
            DataTable dtRB = rbs.ToDataTableField();
            dtRB.TableName = "Sys_FrmRB";
            ds.Tables.Add(dtRB);

            // MapAttrs
            BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs();
            QueryObject qo = new QueryObject(attrs);
            qo.AddWhere(BP.Sys.MapAttrAttr.FK_MapData, fk_mapdata);
            qo.addAnd();
            qo.AddWhereNotIn(BP.Sys.MapAttrAttr.KeyOfEn,
                "'BillNo','CDT','Emps','FID','FK_Dept','FK_NY','MyNum','NodeState','OID','RDT','Rec','Title','WFLog','WFState'");
            qo.DoQuery();

            DataTable dtattrs = attrs.ToDataTableField();
            dtattrs.TableName = "Sys_MapAttr";
            ds.Tables.Add(dtattrs);

            // MapDtl
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls(fk_mapdata);
            DataTable dtDtl = dtls.ToDataTableField();
            dtDtl.TableName = "Sys_MapDtl";
            ds.Tables.Add(dtDtl);

            // Map2m
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms(fk_mapdata);
            DataTable dtM2m = m2ms.ToDataTableField();
            dtM2m.TableName = "Sys_MapM2M";
            ds.Tables.Add(dtM2m);
            return Connector.ToXml(ds);
        }
        /// <summary>
        /// 保存frm
        /// </summary>
        /// <param name="ds">frm 数据</param>
        /// <returns>保存的结果</returns>
        [WebMethod(EnableSession = true)]
        public string SaveFrm(string xml, string sqls)
        {
            StringReader sr = new StringReader(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(sr);

            string str = "";
            foreach (DataTable dt in ds.Tables)
            {
                try
                {
                    str += this.SaveDT(dt);
                }
                catch (Exception ex)
                {
                    str += ex.Message;
                }
            }
            this.RunSQLs(sqls);
            return str;
        }
        public string SaveDT(DataTable dt)
        {
            string igF = "@RowIndex@RowState@";
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            string tableName = dt.TableName.Replace("CopyOf", "");
            #region gener sql.
            //生成updataSQL.
            string updataSQL = "UPDATE " + tableName + " SET ";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;

                updataSQL += dc.ColumnName + "=@" + dc.ColumnName + ",";
            }
            updataSQL = updataSQL.Substring(0, updataSQL.Length - 1);
            string pk = "";
            if (dt.Columns.Contains("MyPK"))
                pk = "MyPK";
            if (dt.Columns.Contains("OID"))
                pk = "OID";
            if (dt.Columns.Contains("No"))
                pk = "No";
            updataSQL += " WHERE " + pk + "=@" + pk;

            //生成INSERT SQL.
            string insertSQL = "INSERT INTO " + tableName + " ( ";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;
                insertSQL += dc.ColumnName + ",";
            }
            insertSQL = insertSQL.Substring(0, insertSQL.Length - 1);
            insertSQL += ") VALUES (";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;
                insertSQL += "@" + dc.ColumnName + ",";
            }
            insertSQL = insertSQL.Substring(0, insertSQL.Length - 1);
            insertSQL += ")";
            #endregion gener sql.

            #region save to data.
            foreach (DataRow dr in dt.Rows)
            {
                BP.DA.Paras ps = new BP.DA.Paras();
                foreach (DataColumn dc in dt.Columns)
                {
                    ps.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                ps.SQL = updataSQL;
                try
                {
                    if (BP.DA.DBAccess.RunSQL(ps) == 0)
                    {
                        ps.SQL = insertSQL;
                        BP.DA.DBAccess.RunSQL(ps);
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    foreach (BP.DA.Para p in ps)
                    {
                        msg += "\r\n@" + p.ParaName + " = " + p.val;
                    }
                    return msg;
                }
            }
            #endregion save to data.
            return null;
        }
    }
}
