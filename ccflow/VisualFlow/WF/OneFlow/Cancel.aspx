<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="Cancel.aspx.cs" Inherits="WF_OneFlow_Cancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <%
    /*获取数据.*/
    string fk_flow = this.Request.QueryString["FK_Flow"];
    string rptTable= "ND"+int.Parse(fk_flow)+"Rpt";
    string dbStr = BP.SystemConfig.AppCenterDBVarStr;
    string sql = "SELECT OID,Title,RDT,FID FROM " + rptTable + " WHERE WFState=1 AND Rec=" + dbStr + "Rec";
    BP.DA.Paras ps =new BP.DA.Paras();
    ps.SQL=sql;
    ps.Add("Rec",BP.Web.WebUser.No);
    System.Data.DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
    %>
<table  style="width:100%">
<Caption class='Caption' align="left" style="background:url('../../Comm/Style/BG_Title.png') repeat-x ; height:30px ; line-height:30px" >
撤销</Caption>
<tr>
<th class="Title">IDX </th>
<th class="Title">流水号</th>
<th class="Title">流程标题</th>
<th class="Title">时间</th>
<th class="Title">操作</th>
</tr>
<%
    int workid = 0;
    string title, rdt, fid;
    int idx = 0;
    string appPath = this.Request.ApplicationPath;
    foreach (System.Data.DataRow dr in dt.Rows)
    {
        workid = int.Parse(dr["OID"].ToString());
        title = dr["Title"].ToString();
        rdt = dr["RDT"].ToString();
        fid = dr["FID"].ToString();
        idx++;
    %>
   <tr>
   <td class="Idx">
       <asp:CheckBox ID="CB"  Text=""  runat="server" /><%=idx %>
   </td>
   <td class="TD" width="5%"><%=workid%></td>
   <td class="TD" width="75%"><a href="javascript:WinOpen('<%=appPath %>/WF/WFRpt.aspx?WorkID=<%=workid%>&FK_Flow=<%=fk_flow%>&FID=<%=fid%>')" ><img src='<%=appPath %>/WF/Img/Action/FlowOverByCoercion.png' class="Icon"  /><%=title %></a></td>
   <td class="TD  width="8%" "><%=rdt.Substring(5)%></td>
   <td class="TD" width="8%" ><img src='<%=appPath %>/WF/Img/Action/RebackFlow.png' class="Icon" />恢复启用</td>
   </tr>
 <% } %>
</table>
</asp:Content>

