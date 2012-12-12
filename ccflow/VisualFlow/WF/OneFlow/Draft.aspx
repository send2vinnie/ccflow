<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="Draft.aspx.cs" Inherits="WF_OneFlow_Draft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <%
    /*获取数据.*/
    string fk_flow = this.Request.QueryString["FK_Flow"];
    string rptTable= "ND"+int.Parse(fk_flow)+"Rpt";
    string nodeID = "ND" + int.Parse(fk_flow)+"01";
    string fk_node =  int.Parse(fk_flow) + "01";
    string dbStr = BP.SystemConfig.AppCenterDBVarStr;
    string sql = "SELECT OID,Title,RDT FROM " + nodeID + " WHERE NodeState=0 AND Rec=" + dbStr + "Rec";
    BP.DA.Paras ps =new BP.DA.Paras();
    ps.SQL=sql;
    ps.Add("Rec",BP.Web.WebUser.No);
    System.Data.DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
    %>
<table  style="width:100%">
<Caption class='Caption' align=left style="background:url('../../Comm/Style/BG_Title.png') repeat-x ; height:30px ; line-height:30px" >
草稿</caption>
<tr>
<th class="Title">IDX </th>
<th class="Title">流水号</th>
<th class="Title">流程标题</th>
<th class="Title">时间</th>
<th class="Title">操作</th>
</tr>
<%
    int workid = 0;
    string title, rdt;
    int idx = 0;
    foreach (System.Data.DataRow dr in dt.Rows)
    {
        workid = int.Parse(dr["OID"].ToString());
        title = dr["Title"].ToString();
        rdt = dr["RDT"].ToString();
        idx++;
    %>
   <tr>
   <td class="Idx">
       <asp:CheckBox ID="CB"  Text=""  runat="server" /><%=idx %>
   </td>
   <td class="TD" width="5%"><%=workid%></td>
   <td class="TD" width="80%"><a href="MyFlow.aspx?WorkID=<%=workid %>&FK_Flow=<%=fk_flow %>&FK_Node=<%=fk_node %>" ><img src='../Img/WFState/Draft.png' class="Icon"  border=0/><%=title%></a></td>
   <td class="TD"><%=rdt.Substring(5)%></td>

   <td class="TD"><a href="javascript:DelDraft('<%=workid %>','<%=fk_node %>');" ><img src='../../Images/Btn/Delete.gif' class="Icon" >
   
       </a></td>
   </tr>
 <% } %>
</table>
<asp:Button  ID="Btn_Del" runat="server" Text="批量删除" onclick="Del_Click"  />
</asp:Content>

