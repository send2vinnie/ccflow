using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.DA;

public partial class Designer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "";
        try
        {
            #region 测试数据库是否连接成功。
            try
            {
                if (DBAccess.IsExitsObject("WF_FlowSort") == false)
                {
                    /*如果是没有安装.*/
                    this.Response.Redirect("../DBInstall.aspx", true);
                    return;
                }
            }
            catch
            {
                if (DBAccess.IsExitsObject("WF_FlowSort") == false)
                {
                    /*如果是没有安装.*/
                    this.Response.Redirect("../DBInstall.aspx", true);
                    return;
                }
            }
            #endregion 测试数据库是否连接成功。

            //保障升级后的数据完整性. 2011-11-26.
            sql = "UPDATE SYS_MAPEXT SET ExtType='TBFullCtrl' WHERE ExtType='FullCtrl'";
            DBAccess.RunSQL(sql);

            #region 手动升级. 2011-07-08 补充节点字段分组.
            //string sql = "DELETE Sys_EnCfg WHERE No='BP.WF.Ext.NodeO'";
            //BP.DA.DBAccess.RunSQL(sql);

            //sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Ext.NodeO','NodeID=基本配置@WarningDays=考核属性@SendLab=功能按钮标签与状态')";
            //BP.DA.DBAccess.RunSQL(sql);
            #endregion 手动升级. 2011-07-08 补充节点字段分组.

            #region 升级基础信息。 2011-11-02。 在过1个月去掉它。
            sql = "SELECT count(*) FROM CN_City ";
            if (BP.DA.DBAccess.RunSQLReturnValInt(sql) == 0  )
            {
                string scrpts = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfData + "\\Install\\SQLScript\\InitPublicData.sql");
                BP.DA.DBAccess.RunSQLs(scrpts);
            }
            // 升级退回规则。
            try
            {
                sql = "SELECT FK_Node FROM WF_ReturnWork";
                /*如果有这个列说明是未升级的 , 让它删除重建。*/
                BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
            catch
            {
                sql = "DROP TABLE WF_ReturnWork";
                BP.DA.DBAccess.RunSQL(sql);

                BP.WF.ReturnWork rw = new BP.WF.ReturnWork();
                rw.CheckPhysicsTable();
            }
            #endregion 升级基础信息。

            #region 更新 WF_EmpWorks. 2011-11-09
            try
            {
                sql = "DROP VIEW WF_EmpWorks";
                BP.DA.DBAccess.RunSQLs(sql);
            }
            catch
            {
            }
            sql = "CREATE VIEW  WF_EmpWorks AS SELECT A.WorkID, A.Rec AS Starter, A.RecName as StarterName, A.FK_Flow, A.FlowName,B.FK_Node, B.FK_NodeText AS NodeName, A.Title, A.RDT, B.RDT AS ADT, B.SDT, B.FK_Emp,B.FK_EmpText, B.FID ,A.FK_FlowSort FROM  WF_GenerWorkFlow A, WF_GenerWorkerList B WHERE     (B.IsEnable = 1) AND (B.IsPass = 0) AND A.WorkID = B.WorkID AND A.FK_Node = B.FK_Node ";
            BP.DA.DBAccess.RunSQLs(sql);

            // 更新老版本的字段长度。
            sql = "ALTER TABLE WF_Track ALTER COLUMN RDT varchar(20)";
            BP.DA.DBAccess.RunSQLs(sql);
            #endregion 更新 WF_EmpWorks. 2011-11-09

            #region 执行admin登陆.
            Emp emp = new Emp();
            emp.No = "admin";
            if (emp.RetrieveFromDBSources() == 1)
            {
                BP.Web.WebUser.SignInOfGener(emp, true);
            }
            else
            {
                throw new Exception("admin 用户丢失，请注意大小写。");
            }
            #endregion 执行admin登陆.

        }
        catch (Exception ex)
        {
            this.Response.Write(ex.Message + "<br>@<a href='../DBInstall.aspx' >点这里到系统升级界面。</a>");
            return;
        }
    }
}