using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using BP.DA;
using BP.Sys;
using BP.Web;
using BP.En;
using BP.WF;
using BP.Port;
using Silverlight.DataSetConnector;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;

/// <summary>
///ccflowAPI 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class CCFlowAPI : CCForm {

    public CCFlowAPI()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    /// <summary>
    /// 待办提示
    /// </summary>
    /// <param name="userNo"></param>
    /// <returns></returns>
    [WebMethod]
    public string AlertString(string userNo)
    {
        return "@EmpWorks=12@CC=34";
    }
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="userNo"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    [WebMethod]
    public void Port_Login(string userNo, string pass)
    {
        Emp emp = new Emp(userNo);
        if (emp.Equals(pass) == false)
            throw new Exception("@用户名或者密码错误");
        BP.WF.Dev2Interface.Port_Login(userNo, "");
    }
    /// <summary>
    /// 获取一条待办工作
    /// </summary>
    /// <param name="fk_flow">工作编号</param>
    /// <param name="fk_node">节点编号</param>
    /// <param name="workID">工作ID</param>
    /// <param name="userNo">操作员编号</param>
    /// <returns></returns>
    [WebMethod]
    public string GenerWorkNode(string fk_flow, int fk_node, Int64 workID, string userNo)
    {
        //if (Dev2Interface.Flow_CheckIsCanDoCurrentWork(workID, userNo) == false)
        //    throw new Exception("您没有处理当前工作的权限。");

        try
        {
            Emp emp = new Emp(userNo);
            BP.Web.WebUser.SignInOfGener(emp);
        }
        catch
        {
        }


        MapData md = new MapData();
        md.No = "ND" + fk_node;
        if (md.RetrieveFromDBSources() == 0)
            throw new Exception("装载错误，该表单ID=" + md.No + "丢失，请修复一次流程重新加载一次.");
        DataSet myds = md.GenerHisDataSet();

        // 节点数据.
        Node nd = new Node(fk_node);
        myds.Tables.Add(nd.ToDataTableField("WF_Node"));

        //节点标签数据.
        BtnLab btnLab = new BtnLab(fk_node);
        myds.Tables.Add(nd.ToDataTableField("WF_BtnLab"));

        // 流程数据.
        Flow fl = new Flow(fk_flow);
        myds.Tables.Add(nd.ToDataTableField("WF_Flow"));

        //// 工作数据放里面去.
        BP.WF.Work wk = nd.HisWork;
        wk.OID = workID;
        wk.RetrieveFromDBSources();
        myds.Tables.Add(wk.ToDataTableField("NDData"));

        return Connector.ToXml(myds);
    }
}
