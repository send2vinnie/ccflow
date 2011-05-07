<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowUC.ascx.cs" Inherits="WF_UC_MyFlowUC" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
    <%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc1" %>
    <%@ Register src="MyFlow.ascx" tagname="MyFlow" tagprefix="uc2" %>
    <style type="text/css">
        .style1
        {
            width: 225px;
        }
    </style>
	<script language="JavaScript" src="./../../Comm/JScript.js"></script>

<table id="Table1" border='0' cellspacing='1' cellpadding='1'  width='100%' >
<tr>
<td  valign='top'  align="Left" class="style1"  width='0%'  >
    <uc1:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
</td>
<td valign='top' id='Right' style="height:600px;" align=left width='80%' >
    <uc2:MyFlow ID="MyFlow1" runat="server" />
    </td>
    </tr>
 </table>
