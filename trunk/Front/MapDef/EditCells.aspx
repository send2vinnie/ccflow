<%@ Page Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="EditCells.aspx.cs" Inherits="WF_MapDef_EditFromTable" Title="未命名頁面" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language=javascript>
  function Edit(mypk,refoid, ftype)
    {
        var url='EditF.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid +'&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>

