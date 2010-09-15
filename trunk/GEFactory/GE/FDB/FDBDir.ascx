<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FDBDir.ascx.cs" Inherits="GE_FDB_FDBDir" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register Src="PartFDBDir.ascx" TagName="PartFDBDir" TagPrefix="uc2" %>
<table width='100%'>
    <tr>
        <td valign="top" class='BigDoc' width='20%'>
            <uc2:PartFDBDir ID="PartFDBDir1" runat="server" />
        </td>
        <td valign="top" class='BigDoc'>
            <uc1:Pub ID="Pub2" runat="server" />
        </td>
    </tr>
</table>
