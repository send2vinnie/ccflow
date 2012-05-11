<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="SMS.aspx.cs" Inherits="CCOA_SMS_SMS" %>

<%@ Register Src="~/CCOA/AddressBook/AddrBook.ascx" TagName="AddrBook" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <xuc:XToolBar ID="XToolbar1" runat="server" title="短信平台" />
    <table width="100%">
        <tr>
            <td>
                <uc:AddrBook ID="AddrBook" runat="server" />
            </td>
            <td style="vertical-align: top;">
                <table width="100%">
                    <tr>
                        <th>
                            手机号码
                        </th>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            短信内容
                        </th>
                        <td>
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSend" runat="server" Text="发送" OnClick="btnSend_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
