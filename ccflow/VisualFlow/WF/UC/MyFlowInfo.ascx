<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowInfo.ascx.cs" Inherits="WF_UC_MyFlowInfo" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc2" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="MyFlowInfoWap.ascx" tagname="MyFlowInfoWap" tagprefix="uc3" %>

<table id="Table1" height="99%" width='100%'  align=center border=0 cellpadding=1 cellspacing=1 >
  <TR >
  <td width="25%" valign=top align=left >
      <uc2:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
  </td>
  <td valign=top align=left >
    <uc4:ToolBar ID="ToolBar1" runat="server"  />
      <uc3:MyFlowInfoWap ID="MyFlowInfoWap1" runat="server" />
  </td>
  </TR>
  </table>
