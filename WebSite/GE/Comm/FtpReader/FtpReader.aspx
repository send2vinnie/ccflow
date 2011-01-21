<%@ Page Language="C#" MasterPageFile="~/GE/Template/WinOpen.master" AutoEventWireup="true"
    CodeFile="FtpReader.aspx.cs" Inherits="GE_Comm_FtpReader" Title="无标题页" %>

<%@ Register Src="FtpReader.ascx" TagName="FtpReader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 99%">
        <uc1:FtpReader ID="FtpReader1" runat="server" />
    </div>
</asp:Content>
