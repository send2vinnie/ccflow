<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoTabsMore.ascx.cs" Inherits="Comm_GE_Video_VideoTabsMore" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<table width="100%" style="table-layout: fixed">
    <tr>
        <td valign="top" class="BigDoc" width='23%'>
            <uc1:Pub ID="PubSort" runat="server" />
        </td>
        <td valign="top" class="BigDoc" width='77%'>
            <uc1:Pub ID="PubContent" runat="server" />
            <uc1:Pub ID="PubPage" runat="server" />
        </td>
    </tr>
</table>