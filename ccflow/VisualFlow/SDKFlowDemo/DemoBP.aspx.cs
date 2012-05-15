using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Port;

public partial class SDKFlowDemo_DemoEntity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 全局的基本应用
    /// </summary>
    public void GloBaseApp()
    {
        // 当前登陆人员编号.
        string currLoginUserNo = BP.Web.WebUser.No;
        // 登陆人员名称
        string currLoginUserName = BP.Web.WebUser.Name;
        // 登陆人员部门编号.
        string currLoginUserDeptNo = BP.Web.WebUser.FK_Dept;
        // 登陆人员部门名称
        string currLoginUserDeptName = BP.Web.WebUser.FK_DeptName;

    }
    /// <summary>
    /// 数据库操作访问
    /// </summary>
    public void DataBaseAccess()
    {
        // 执行Insert ,delete, update 语句.
        BP.DA.DBAccess.RunSQL("DELETE FROM Port_Emp WHERE 1=2");

        //执行查询返回datatable.
        string sql = "SELECT * FROM Port_Emp";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        //执行查询返回 string 值.
        sql = "SELECT FK_Dept FROM Port_Emp WHERE No='" + BP.Web.WebUser.No + "'";
        string fk_dept = BP.DA.DBAccess.RunSQLReturnString(sql);

        //执行查询返回 int 值. 也可以返回float, string
        sql = "SELECT count(*) as Num FROM Port_Emp ";
        int empNum = BP.DA.DBAccess.RunSQLReturnValInt(sql);
    }
    /// <summary>
    /// Entity 的基本应用.
    /// </summary>
    public void EntityBaseApp()
    {
        // 插入一条数据.
        BP.Port.Emp emp = new BP.Port.Emp();
        emp.No = "zhangsan";
     //   emp.Retrieve();

        emp.Name = "张三";
        emp.FK_Dept = "01";
        emp.Pass = "pub";
        emp.Insert();

        BP.Port.Emp myEmp = new BP.Port.Emp();
        myEmp.No = "zhangsan";
        if (myEmp.RetrieveFromDBSources() == 0)
        {
            this.Response.Write("没有查询到编号等于zhangsan的人员记录.");
            return;
        }
        else
        {
            string msg = "";
            msg += "<BR>编号:"+myEmp.No;
            msg += "<BR>名称:"+myEmp.Name;
            msg += "<BR>密码:" + myEmp.Pass;
            msg += "<BR>部门编号:"+myEmp.FK_Dept;
            msg += "<BR>部门名称:" + myEmp.FK_DeptText;
            this.Response.Write(msg);
        }
    }
}

 