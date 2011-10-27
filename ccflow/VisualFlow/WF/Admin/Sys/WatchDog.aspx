<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="WatchDog.aspx.cs" Inherits="WF_Admin_Sys_WatchDogaspx" %><%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../../Comm/Style/Tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />

    <script language=javascript>
        function DelIt(workid, fk_flow) {
            if (window.confirm('您确定要删除吗？') == false)
                return;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td></td>
<td ><uc1:Pub ID="Top" runat="server" /></td>
</tr>
<tr>
<td colspan=1 width='30%'><uc1:Pub ID="Left" runat="server" /></td>
<td colspan=1 valign=top><uc1:Pub ID="Right" runat="server" /></td>
</tr>
</table>
</asp:Content>

