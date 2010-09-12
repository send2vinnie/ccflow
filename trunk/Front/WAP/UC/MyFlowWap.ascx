<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowWap.ascx.cs" Inherits="WF_UC_MyFlowWap" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="../../WF/UC/Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="../../WF/UC/FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc1" %>
<%@ Register src="../../WF/UC/UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
    <style type="text/css">
        .style1
        {
            width: 225px;
        }
    </style>
    <script language="JavaScript" src="./../Style/JScript.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../Comm/JS/Calendar.js" type="text/javascript"></script>
<script language=javascript>
    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 40; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            document.getElementById(rowIdx + '_' + i).style.display = sta;
        }
    }
</script>
<table width='100%'   >
<tr>
<td class='ToolBar' align=left >
<uc4:ToolBar ID="ToolBar1" runat="server" />
</td>
</tr>
<tr valign="top">
            <td valign="top" align=left >
                    <uc5:Pub ID="FlowMsg" runat="server" />
                    <uc6:UCEn ID="UCEn1" runat="server" />
                    <uc5:Pub ID="Pub1" runat="server" />
                    </td>
        </tr>
</table>
 
