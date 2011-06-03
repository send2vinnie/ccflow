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
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    

    /// <summary>
    /// 根据workID获取工作列表
    /// FK_Node 节点ID
    /// rdt 记录日期，也是工作接受日期。
    /// sdt 应完成日期.
    /// FK_emp 操作员编号。
    /// EmpName 操作员名称.
    /// </summary>
    /// <param name="workid">workid</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetDTOfWorkList(string fk_flow, string workid)
    {

        string sql = " SELECT A.FK_Node, A.RDT,A.SDT,A.FK_Emp,b.Name as EmpName";
        sql += " FROM WF_GenerWorkerList A, WF_Emp B WHERE A.FK_Emp=b.No AND A.IsEnable=1 AND A.WorkID=" + workid;

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
        {
            sql = "SELECT A.FK_Node, A.RDT,A.CDT, A.CDT as SDT, A.Rec AS FK_Emp ,b.Name as EmpName";
            sql += " FROM V" + fk_flow + " A, WF_Emp B WHERE A.Rec=b.No AND A.OID=" + workid;
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 让admin 登录
    /// </summary>
    /// <param name="lang">当前的语言</param>
    [WebMethod(EnableSession = true)]
    public void LetAdminLogin(string lang, bool islogin)
    {
        if (islogin)
        {
            Emp emp = new Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp, lang, "admin", true);
        }
    }

    #region 执行功能的方法
    [WebMethod(EnableSession = true)]
    public string WinOpenEns(string lang, string dotype, string fk_dept, string fk_emp, string enName,bool isLogin)
    {
        LetAdminLogin("CH", isLogin);
       string url = "";
       switch (dotype)
       {
           case "EnsEdit":
               url = "/Comm/UIEn.aspx?EnName=" + enName + "&No=" + fk_emp;
               break;
           case "EnsAdd":
               url = "/Comm/UIEn.aspx?EnName=BP.Port.Emp&FK_Dept=" + fk_dept;
               break;
           default:
               break;
       }
       return url;

    }
    [WebMethod(EnableSession = true)]
    public string WinOpen(string lang, string dotype, string fk_flow, string node1, string node2, bool isLogin)
    {
        LetAdminLogin("CH", isLogin);
        string url = "";
        switch (dotype)
        {
            
            case "NodeP":
                url = "/Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + node1;
                break;
            case "FlowP":
                Flow fl = new Flow(fk_flow);
                if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
                    url = "/Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowDoc&PK=" + fk_flow;
                else
                    url = "/Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fk_flow;
                break;
            //case "Ens": // 实体编辑.
            //    this.Response.Redirect("./Comm/Batch.aspx?EnsName=" + this.EnsName, true);
            //    break;
            //case "En": // 单个实体编辑.
            //    //switch (this.EnName)
            //    //{
            //    //    case "BP.WF.Flow":
            //    //        Flow fl = new Flow(this.PK);
            //    //        if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
            //    //            this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowDoc&PK=" + this.PK, true);
            //    //        else
            //    //            this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + this.PK, true);
            //    //        break;
            //    //    case "BP.WF.Node":
            //    //        Node nd = new Node(this.PK);
            //    //        this.Response.Redirect("./Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + this.PK, true);
            //    //        break;
            //    //    default:
            //    //        this.Response.Redirect("./Comm/UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PK, true);
            //    //        break;
            //    //}
            //    //break;
            case "StaDef": // 节点岗位.
                url = "/Comm/UIEn1ToM.aspx?EnName=BP.WF.Ext.NodeO&AttrKey=BP.WF.NodeStations&PK=" + node1 + "&NodeID=" + node1 + "&RunModel=0&FLRole=0&FJOpen=0&r=" + node1;
                break;
            case "WFRpt": // 报表设计.
                url = "/WF/MapDef/WFRpt.aspx?PK=ND" + int.Parse(fk_flow) + "Rpt";
                break;
            case "MapDef": //定义表单.
                url = "/WF/MapDef/MapDef.aspx?PK=ND" + node1;
                break;
            case "Dir": // 方向。
                url = "/WF/Admin/Cond.aspx?FK_Flow=" + fk_flow + "&FK_MainNode=" + node1 + "&FK_Node=" + node1 + "&ToNodeID=" + node2 + "&CondType=2";
                break;
            case "RunFlow": //运行流程。
                url = "/WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow + "&Lang=";
                break;
            case "FlowCheck": // 流程设计。
                url = "/WF/Admin/DoType.aspx?RefNo=" + fk_flow + "&DoType=" + dotype;
                break;
            case "NewFlow":
                url = "/Comm/RefFunc/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=048";
                break;
            case "EditFlowSort":
                url = "/WF/Admin/SetFlowSort.aspx?Fk_FlowSort="+fk_flow;
                break;
            default:
                break;
        }
        return url;
    }
    #endregion 执行功能的方法


    [WebMethod(EnableSession = true)]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public DataTable RunSQLReturnTablePeng(string sql)
    {
        return BP.DA.DBAccess.RunSQLReturnTable(sql);
        //DataSet ds = new DataSet();
        //ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
        //return Connector.ToXml(ds);
        // return BP.DA.DBAccess.RunSQLReturnTable(sql);
    }
    [WebMethod(EnableSession = true)]
    public string  GetName(string sql)
    {
        return "zhou";
    }

    [WebMethod(EnableSession = true)]
    public string RunSQLReturnTable(string sql,bool isLogin)
    {
        LetAdminLogin("CH", isLogin);
        DataSet ds = new DataSet();
        ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
        return Connector.ToXml(ds);
    }

    #region 工作流
    //select nodeID,x,y,NodeWorkType,HisToNDs from wf_node
    [WebMethod(EnableSession = true)]
    public string GetFlowSort()
    {
        //FlowSorts fs = new FlowSorts();
        //fs.RetrieveAll();
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet("select * from WF_FlowSort");
        return Connector.ToXml(ds);
    }
    [WebMethod(EnableSession = true)]
    public string GetFlowBySort(string sort)
    {
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet("select No,Name,FK_FlowSort from WF_Flow");
        return Connector.ToXml(ds);
    }
    [WebMethod(EnableSession = true)]
    public string GetFlows()
    {
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet("select No,Name,FK_FlowSort from WF_Flow ");
        return Connector.ToXml(ds);
    }
#endregion

    #region 部门
    ///// <summary>
    ///// 获取部门
    ///// </summary>
    ///// <returns></returns>
    //[WebMethod(EnableSession = true)]
    //public string GetDepts()
    //{
    //    DataSet ds = new DataSet();
    //    ds = BP.DA.DBAccess.RunSQLReturnDataSet("select No,Name from Port_Dept");
    //    return Connector.ToXml(ds);
    //}
    ///// <summary>
    /////岗位
    ///// </summary>
    ///// <returns></returns>
    //[WebMethod(EnableSession = true)]
    //public string GetStations() 
    //{
      
    //    DataSet ds = new DataSet();
    //    ds = BP.DA.DBAccess.RunSQLReturnDataSet(@"select  No,Name  from Port_Station");
    //    return Connector.ToXml(ds);
    //}
    ///// <summary>
    /////人员
    ///// </summary>
    ///// <returns></returns>
    //[WebMethod(EnableSession = true)]
    //public string GetEmps()
    //{
       
    //    DataSet ds = new DataSet();
    //    ds = BP.DA.DBAccess.RunSQLReturnDataSet("select * from Port_Emp");
    //    return Connector.ToXml(ds);
    //}
    ///// <summary>
    ///// 部门人员
    ///// </summary>
    ///// <returns></returns>
    //[WebMethod(EnableSession = true)]
    //public string GetDeptEmps() 
    //{
      
    //    DataSet ds = new DataSet();
    //    ds = BP.DA.DBAccess.RunSQLReturnDataSet("select * from Port_Emp");
    //    return Connector.ToXml(ds);
    //}
 
    /// <summary>
    /// 岗位人员
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetStationEmps()
    {
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet(@"select e.No as EmpNo, e.Name as EmpName,s.No,s.Name  from Port_Station s,Port_EmpStation es ,Port_Emp e 
where s.No=es.FK_Station and e.No=es.FK_Emp");
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 岗位维护
    /// </summary>
    [WebMethod(EnableSession = true)]
    public string MaintainStation( string pk    )
    {
        string url = "http://localhost/Flow/Comm/RefFunc/UIEn.aspx?EnsName=BP.WF.Port.Stations&PK=" + pk + "&rowUrl=1";
        return url;
    }
    /// <summary>
    /// 部门维护
    /// </summary>
    [WebMethod(EnableSession = true)]
    public string MaintainDept(string pk)
    {
        string url = "http://localhost/Flow/Comm/RefFunc/UIEn.aspx?EnsName=BP.WF.Port.Depts&PK=" + pk + "&rowUrl=1";
        return url;
    }
    /// <summary>
    /// 人员维护
    /// </summary>
    /// <param name="pk"></param>
    /// <returns></returns>
   [WebMethod(EnableSession = true)]
    public string MaintainEmp(string pk)
    {
        string url = "http://localhost/Flow/Comm/RefFunc/UIEn.aspx?EnsName=BP.WF.Port.Emps&PK="+pk;
        return url;
    }
    #endregion

    [WebMethod(EnableSession = true)]
    public int RunSQL(string sql)
    {
        return BP.DA.DBAccess.RunSQL(sql);
    }
    [WebMethod(EnableSession = true)]
    public string Do(string doWhat, string para1, bool isLogin)
    {
        LetAdminLogin("CH", isLogin);

        switch (doWhat)
        {
            case "GenerFlowTemplete":
                Flow temp = new BP.WF.Flow(para1);
                return null;
            case "NewFlowSort":
                BP.WF.FlowSort fs = new FlowSort();
                try
                {
                    fs.Name = para1;
                    fs.No = fs.GenerNewNo;
                    fs.Insert();
                    return fs.No;
                }
                catch { try { return fs.No; } catch { return null; } }
            case "NewFlow":
                Flow fl = new Flow();

                try
                {
                    if (para1 != null)
                        fl.FK_FlowSort = para1;
                    fl.Name = "新建流程";
                    fl.DoNewFlow();
                    return fl.No + ";" + fl.Name;
                }
                catch { try { return fl.No; } catch { return null; } }
            case "DelFlow":
                BP.WF.Flow fl1 = new BP.WF.Flow(para1);
                try
                {
                    fl1.DoDelete();
                }
                catch { }
                return null;
            case "DelLable":
                BP.WF.LabNote ln = new BP.WF.LabNote(para1);
                ln.Delete();
                return null;
            case "DelFlowSort":
                FlowSort delfs = new FlowSort(para1);
                delfs.Delete();
                return null;
            case "NewNode":
                BP.WF.Flow fl11 = new BP.WF.Flow(para1);
                BP.WF.Node node = new BP.WF.Node();
                node.FK_Flow = "";
                node.X = 0;
                node.Y = 0;
                node.Insert();
                return null;
            case "DelNode":
                if (!string.IsNullOrEmpty(para1))
                {
                    BP.WF.Node delNode = new BP.WF.Node(int.Parse(para1));
                    delNode.Delete();
                }
                return null;
            case "NewLab":
                BP.WF.LabNote lab = new BP.WF.LabNote();
                lab.FK_Flow = para1;
                lab.MyPK = BP.DA.DBAccess.GenerOID().ToString();
                lab.Insert();
                return lab.MyPK;
            case "DelLab":
                BP.WF.LabNote dellab = new BP.WF.LabNote();
                dellab.MyPK = para1;
                dellab.Delete();
                return null;
            default:
                throw null;
        }
    }
    /// <summary>
    /// 创建一个节点
    /// </summary>
    /// <param name="fk_flow">流程编号</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>返回节点编号</returns>
    [WebMethod(EnableSession = true)]
    public int DoNewNode(string fk_flow, int x, int y, string nodeName, bool isLogin)
    {
        LetAdminLogin("CH", isLogin);
        if (string.IsNullOrEmpty(fk_flow))
            return 0;

        Flow fl = new Flow(fk_flow);
        BP.WF.Node nf = new BP.WF.Node(fl.DoNewNode(x, y).NodeID);
        nf.Name = nodeName;
        nf.Save();
        return nf.NodeID;
    }

    /// <summary>
    /// 创建一个连接线
    /// </summary>
    /// <param name="from">从节点</param>
    /// <param name="to">到节点</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public bool DoDrewLine(int from, int to)
    {
        Direction dir = new Direction();
        dir.Node = from;
        dir.ToNode = to;
        try
        {
            dir.Insert();
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 删除一个连接线
    /// </summary>
    /// <param name="from">从节点</param>
    /// <param name="to">到节点</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public bool DoDropLine(int from, int to)
    {
        Direction dir = new Direction();
        dir.Node = from;
        dir.ToNode = to;
        dir.Delete();
        return true;
    }

    /// <summary>
    /// 创建一个标签
    /// </summary>
    /// <param name="fk_flow">流程编号</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>返回标签编号</returns>
    [WebMethod(EnableSession = true)]
    public string DoNewLabel(string fk_flow, int x, int y, string name, string lableId)
    {

        LabNote lab = new LabNote();

        lab.FK_Flow = fk_flow;
        lab.X = x;
        lab.Y = y;
        if (string.IsNullOrEmpty(lableId))
        { lab.MyPK = BP.DA.DBAccess.GenerOID().ToString(); }
        else
        {
            lab.MyPK = lableId;
        }
        lab.Name = name;
        lab.Save();
        return lab.MyPK;
    }
    /// <summary>
    /// 产生流程模板
    /// </summary>
    /// <param name="fk_flow">流程编号</param>
    /// <returns>流程模板路径</returns>
    [WebMethod(EnableSession = true)]
    public string FlowTemplete_Gener(string fk_flow, bool islogin)
    {
        LetAdminLogin("CH",islogin);
        Flow fl = new Flow(fk_flow);
        return fl.GenerFlowXmlTemplete();
    }
    /// <summary>
    /// load flow templete.
    /// </summary>
    /// <param name="fk_flowSort">流程类别编号</param>
    /// <param name="path">模板文件路径</param>
    [WebMethod(EnableSession = true)]
    public void FlowTemplete_Load(string fk_flowSort, string path, bool islogin)
    {
        LetAdminLogin("CH", islogin);
        Flow.DoLoadFlowTemplate(fk_flowSort,path); 
    }
    /// <summary>
    /// 根据工作流取连线
    /// </summary>
    /// <param name="flowid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetDirection(string flowid)
    {
       
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(@"SELECT Node ,ToNode FROM WF_Direction WHERE Node IN
(SELECT NodeID FROM WF_Node WHERE FK_Flow='" + flowid + "') ");
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 根据工作流取标签
    /// </summary>
    /// <param name="flowid"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetLables(string flowid)
    {
        LabNotes lns = new LabNotes(flowid);
        lns.Retrieve("FK_Flow", flowid);
        DataSet ds = lns.ToDataSet();
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 保存流程
    /// </summary>
    /// <param name="paras">参数@1001,90,23@1002,80,20</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public void DoSaveFlow(string paras)
    {
        string[] strs = paras.Split('@');
        foreach (string str in strs)
        {
            string[] ps = str.Split(',');
            string sql = "update wf_node set X=" + ps[1] + ", y=" + ps[2] + " where nodeid=" + ps[0];
            BP.DA.DBAccess.RunSQL(sql);
        }
    }
    /// <summary>
    /// 保存结点
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="nodeName"></param>
    [WebMethod(EnableSession = true)]
    public void DoSaveFlowNode(int nodeID, int x, int y, string nodeName, bool islogin)
    {
        LetAdminLogin("CH", islogin);
        BP.WF.Node n;
        if (!string.IsNullOrEmpty(nodeID.ToString()))
        {
            n = new BP.WF.Node(nodeID);

            n.Name = nodeName;
            n.X = x;
            n.Y = y;
            try
            {
                n.Save();
            }
            catch
            {
            }
        }
    }
    [WebMethod]
    public string Uploadfile(byte[] FileByte, string fileName)
    {
        //文件存放路径
        string filepath =@"D:\ccflow\VisualFlow\Temp\"+ fileName ;
        //如果文件已经存在则删除
        if (File.Exists(filepath))
            File.Delete(filepath);
        //创建文件流实例，用于写入文件
        FileStream stream = new FileStream(filepath, FileMode.CreateNew);
        //写入文件
        stream.Write(FileByte, 0, FileByte.Length);
        stream.Close();
        return filepath;
    }

}
