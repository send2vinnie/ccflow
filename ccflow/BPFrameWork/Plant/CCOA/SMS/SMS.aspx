<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="SMS.aspx.cs" Inherits="CCOA_SMS_SMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <th>
                手机号码
            </th>
            <td>
                <base:XTextBox ID="txtPhoneNumber" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                短信内容
            </th>
            <td>
                <base:XTextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" />
            </td>
            
        </tr>
         <tr>
            <th>
               
            </th>
            <td>
               <base:XButton ID="btnSend" runat="server" Text="发送" onclick="btnSend_Click" />
            </td>
            
        </tr>
    </table>
</asp:Content>
