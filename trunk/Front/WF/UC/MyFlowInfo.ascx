<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowInfo.ascx.cs" Inherits="WF_UC_MyFlowInfo" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc2" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>

<table id="Table1" height="99%" align=center border=0 >
  <TR >
  <td width="25%" valign=top align=left >
      <uc2:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
  </td>
  <td valign=top align=center >
    <uc4:ToolBar ID="ToolBar1" runat="server"  />
      <uc1:Pub ID="Pub1" runat="server" />
  </td>
  </TR>
  </table>
