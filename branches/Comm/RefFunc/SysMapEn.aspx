<%@ Page Language="C#" MasterPageFile="SysMapEn.master" AutoEventWireup="true" CodeFile="SysMapEn.aspx.cs" Inherits="Comm_RefFunc_SysMapEn" Title="Untitled Page" %>

<%@ Register src="SysMapEn.ascx" tagname="SysMapEn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Style/TableWF.css" rel="stylesheet" type="text/css" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="Style/JScript.js"></script>
    <script language="javascript">
    function RSize() {
    if (document.body.scrollWidth > (window.screen.availWidth - 100)) {
        window.dialogWidth = (window.screen.availWidth - 50).toString() + "px"
    } else {
        window.dialogWidth = (document.body.scrollWidth + 100).toString() + "px"
    }

    if (document.body.scrollHeight > (window.screen.availHeight - 70)) {
        window.dialogHeight = (window.screen.availHeight ).toString() + "px"
    } else {
        window.dialogHeight = (document.body.scrollHeight + 165).toString() + "px"
    }

    window.dialogLeft = ((window.screen.availWidth - document.body.clientWidth) / 2).toString() + "px"
    window.dialogTop = ((window.screen.availHeight - document.body.clientHeight) / 2).toString() + "px"
   } 
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
    <uc1:SysMapEn ID="SysMapEn1" runat="server" />
</asp:Content>

