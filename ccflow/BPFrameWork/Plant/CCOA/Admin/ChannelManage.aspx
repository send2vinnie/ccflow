<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="ChannelManage.aspx.cs" Inherits="CCOA_Admin_ChannelManage" %>

<%@ Register Src="~/CCOA/Admin/ChannelForm.ascx" TagName="ChannelForm" TagPrefix="uc" %>
<%@ Register Src="~/CCOA/Admin/ChannelTree.ascx" TagName="ChannelTree" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<xuc:XToolBar ID="XToolBar1" runat="server" title="栏目维护" />
    <table width="100%">
        <tr>
            <td style="width:240px;">
                <uc:ChannelTree ID="ChannelTree1" runat="server" />
            </td>
            <td style="vertical-align:top;">
                <uc:ChannelForm ID="ChannelForm1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
