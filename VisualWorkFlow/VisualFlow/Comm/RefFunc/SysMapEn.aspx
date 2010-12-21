<%@ Page Language="C#" MasterPageFile="SysMapEn.master" AutoEventWireup="true" CodeFile="SysMapEn.aspx.cs" Inherits="Comm_RefFunc_SysMapEn" Title="Untitled Page" %>

<%@ Register src="SysMapEn.ascx" tagname="SysMapEn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Style/TableWF.css" rel="stylesheet" type="text/css" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="Style/JScript.js"></script>
    <script language="javascript">
     function ReinitIframe(dtlid) 
    {
    
     try {
     
        var iframe = document.getElementById("F" + dtlid);
        var tdF = document.getElementById("TD" + dtlid);
        iframe.height = iframe.contentWindow.document.body.scrollHeight;
        iframe.width = iframe.contentWindow.document.body.scrollWidth;

        if (tdF.width < iframe.width) {
            //alert(tdF.width +'  ' + iframe.width);
            tdF.width = iframe.width;
        } else {
            iframe.width = tdF.width;
        }

        tdF.height = iframe.height;
        return;
        
       }catch (ex) {
         return;
       }
    return;
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

