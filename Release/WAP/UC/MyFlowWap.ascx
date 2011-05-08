<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_MyFlowWap, App_Web_tjiemihi" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="../../WF/UC/FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc1" %>
<%@ Register src="../../WF/UC/UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
    <%@ Register src="../../WF/Pub.ascx" tagname="Pub" tagprefix="uc2" %>
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
<%--
<fieldset> 
<legend align=right><a href='Home.aspx' > <img src='./Img/Home.gif' border=0 >Home</a></legend>
--%>

<table width='100%'   >
<tr>
<td class='ToolBar' align=left >
<uc4:ToolBar ID="ToolBar1" runat="server" />
</td>
</tr>
<tr valign="top">
<td valign="top" align=left >
    <uc2:Pub ID="FlowMsg" runat="server" />
<uc6:UCEn ID="UCEn1" runat="server" />
    <uc2:Pub ID="Pub1" runat="server" />
</td>
</tr>
</table>
<%--
 </fieldset>--%>
 
