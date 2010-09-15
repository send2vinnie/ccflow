<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenLink1.ascx.cs" Inherits="GE_ImageLink_OpenLink1" %>

<%@ Register src="../Comm/FtpReader/FtpMain.ascx" tagname="FtpMain" tagprefix="uc1" %>

<asp:Panel ID="PanelFTP" runat="server" Visible="false">
    <uc1:FtpMain ID="FtpMain1" runat="server" />
</asp:Panel>
