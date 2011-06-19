using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.DA;
using BP.Sys;

public partial class Reback : System.Web.UI.Page
{
    public void ClearDB()
    {
        string sql = "SELECT * from WF_GenerWorkFlow  order  by Rec";
        DataTable dt = DBAccess.RunSQLReturnTable(sql);

        string delSql = "<br>/******************* 以下流程因为数据中断被清除需要手工清除它们. 清除前请打印出来让同事重新发起.*/ <br>";
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string fk_flow = dr["FK_Flow"].ToString();
            string workid = dr["WorkID"].ToString();
            string title = dr["Title"].ToString();
            string RDT = dr["RDT"].ToString();
            string Rec = dr["Rec"].ToString();


            sql = "SELECT * from WF_GenerWorkerList where workid=" + workid;
            DataTable mydt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow mydr in mydt.Rows)
            {
                string fk_mapdata = "ND" + mydr["FK_Node"].ToString();

                sql = "SELECT count(*) from " + fk_mapdata + " where OID=" + workid;

                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    idx++;
                    Flow fl = new Flow(fk_flow);
                    delSql += "<BR> /* ERROR "+idx+": 流程:"+fl.Name+", 发起人:"+Rec+" 发起日期:"+RDT+", 标题:"+title+" */ ";
                    delSql += "<BR>DELETE  WF_GenerWorkFlow WHERE WorkID=" + workid;
                    delSql += "<BR>DELETE  WF_GenerWorkerList WHERE WorkID=" + workid;
                    Nodes nds = new Nodes(fk_flow); // fl.HisNodes;
                    foreach (Node nd in nds)
                    {
                        delSql += "<BR>DELETE  ND"+nd.NodeID+" WHERE OID=" + workid;
                    }
                    break;
                }
            }
        }

        this.Response.Write(delSql);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       // string sql = "<br> begin transaction ";
        string sql = "<br>/* 执行数据导入 */ ";
        // BP.DA.DBAccess.DoTransactionBegin
        Flows fls = new Flows();
        fls.RetrieveAll();

        string errNode = "";
        string myattrs = "";
        string fk_mapdata = "";
        string isExitSQL = "";
        MapAttrs attrs = null;
        foreach (Flow fl in fls)
        {

            Nodes nds = fl.HisNodes;
            bool ishavedtl = false;
            foreach (Node nd in nds)
            {
                isExitSQL = "SELECT count(*) from wf0422.dbo.wf_node where nodeid=" + nd.NodeID;
                if (DBAccess.RunSQLReturnValInt(isExitSQL) == 0)
                    continue;

                fk_mapdata = "ND" + nd.NodeID;
                try
                {
                    isExitSQL = "SELECT count(*) from wf0422.dbo." + fk_mapdata;
                    DBAccess.RunSQLReturnValInt(isExitSQL);
                }
                catch
                {
                    continue;
                }
                MapDtls dtls = new MapDtls(fk_mapdata);
                if (dtls.Count == 0)
                    continue;

                foreach (MapDtl dtl in dtls)
                {
                    attrs = new MapAttrs(dtl.No);
                    myattrs = "";
                    foreach (MapAttr attr in attrs)
                    {
                        isExitSQL = "SELECT count(*) from wf0422.dbo.sys_mapAttr where mypk='" + dtl.No + "_" + attr.KeyOfEn + "'";
                        if (DBAccess.RunSQLReturnValInt(isExitSQL) == 0)
                            continue;
                        myattrs += "," + attr.KeyOfEn;
                    }

                    if (myattrs == "")
                    {
                        errNode += "@" + nd.NodeID;
                        continue;
                    }

                    myattrs = myattrs.Substring(1);
                    sql += "<br>INSERT INTO " + dtl.No + " (" + myattrs + ") SELECT " + myattrs + " FROM wf0422.dbo." + dtl.No + ";";

                    ishavedtl = true;
                }

                //attrs = new MapAttrs(fk_mapdata);
                //myattrs = "";
                //foreach (MapAttr attr in attrs)
                //{
                //    isExitSQL = "SELECT count(*) from wf0422.dbo.sys_mapAttr where mypk='" + fk_mapdata + "_" + attr.KeyOfEn + "'";
                //    if (DBAccess.RunSQLReturnValInt(isExitSQL) == 0)
                //        continue;
                //    myattrs += "," + attr.KeyOfEn;
                //}
                //if (myattrs == "")
                //{
                //    errNode += "@" + nd.NodeID;
                //    continue;
                //}
                //myattrs = myattrs.Substring(1);
                //sql += "<br>INSERT INTO " + fk_mapdata + " (" + myattrs + ") SELECT " + myattrs + " FROM wf0422.dbo." + fk_mapdata + ";";

            }

            if (ishavedtl == false)
                continue;

            fk_mapdata = "ND" + int.Parse(fl.No) + "RptDtl1";
            attrs = new MapAttrs(fk_mapdata);
            myattrs = "";
            foreach (MapAttr attr in attrs)
            {

                isExitSQL = "SELECT count(*) from wf0422.dbo.sys_mapAttr where mypk='" + fk_mapdata + "_" + attr.KeyOfEn + "'";
                if (DBAccess.RunSQLReturnValInt(isExitSQL) == 0)
                    continue;

                myattrs += "," + attr.KeyOfEn;
            }

            if (myattrs == "")
            {
                errNode += "@" + fl.No;
                continue;
            }
            myattrs = myattrs.Substring(1);
            sql += "<br>INSERT INTO " + fk_mapdata + " (" + myattrs + ") SELECT " + myattrs + " FROM wf0422.dbo." + fk_mapdata + " ; ";
        }

     //  sql += " <br>rollback transaction";

        this.Response.Write(sql);
      //  this.Response.Write("<hr><hr><hr>" + errNode);

        this.ClearDB();
        return;

    }
}