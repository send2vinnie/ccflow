<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlow.ascx.cs" Inherits="WF_UC_MyFlow" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc1" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>

    <script language="JavaScript" src="./../Style/JScript.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../Comm/JS/Calendar.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../Comm/ShortKey.js" type="text/javascript"></script>
    

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
<table id="Table1" border='0'>
<tr>
<td  valign='top' nowarp='true' width='25%' >
    <uc1:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
</td>
<td valign='top' id='Right' style="height:600px; width:75%" >
<!--  开始Table -->
<table width='100%' border=0  style="height:600px;width:100%" >
<tr>
<td style="height: 1px" class='ToolBar' >
                 <uc4:ToolBar ID="ToolBar1" runat="server" />
</td>
</tr>
<tr valign="top">
            <td valign="top" height="100%" width="100%">
                    <uc5:Pub ID="FlowMsg" runat="server" />
                    <uc6:UCEn ID="UCEn1" runat="server" />
                    <uc5:Pub ID="Pub1" runat="server" />
                    </td>
        </tr>
</table>
<!--  End Table -->
    
    </td>
    </tr>
 </table>
