<%@ page language="C#" masterpagefile="SysMapEn.master" autoeventwireup="true" inherits="Comm_RefFunc_SysMapEn, App_Web_2uqcr4ki" title="Untitled Page" %>
<%@ Register src="SysMapEnUC.ascx" tagname="SysMapEnUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Style/TableWF.css" rel="stylesheet" type="text/css" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="Style/JScript.js"></script>
    <script language="javascript">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SysMapEnUC ID="SysMapEnUC1" runat="server" />
</asp:Content>

