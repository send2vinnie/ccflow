<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FtpMain.ascx.cs" Inherits="GE_Comm_FtpMain" %>
<%@ Register Src="FtpDirReader.ascx" TagName="FtpDirReader" TagPrefix="uc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>

<table width="100%">
    <tr>
        <td valign="top" class="BigDoc" style="width:23%;">
            <uc1:FtpDirReader ID="FtpDirReader1" runat="server" />
        </td>
        <td valign="top" class="BigDoc" style="width:77%;">
            <uc1:Pub ID="Pub2" runat="server" />
        </td>
    </tr>
</table>
