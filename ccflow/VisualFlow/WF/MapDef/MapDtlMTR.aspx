<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" ValidateRequest="true"  AutoEventWireup="true" CodeFile="MapDtlMTR.aspx.cs" Inherits="WF_MapDef_MapDtlMTR" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
     <link href="../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
	<script language="JavaScript" src="../../Comm/JScript.js" ></script>
    
    <script type="text/javascript">
        function Rep(tbid) {
        alert(tbid);

         var mytb=document.getElementById(tbid);
            var s = mytb.value;
            alert(s);
            s = s.toString().replace('<', '《');
            s = s.toString().replace('>', '》');
            s = s.toString().replace(''', '‘');
            alert(s);
            mytb.value=s;
            alert(mytb.value);

            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>