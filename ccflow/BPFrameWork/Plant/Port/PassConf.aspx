<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true" CodeFile="PassConf.aspx.cs" Inherits="Port_PassConf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="grid2" style="margin-top:10%;">
        <tr>
            <th colspan="2">修改密码</th>
        </tr>
        <tr>
            <td style="font-weight:bold;">输入当前口令:</td><td><asp:TextBox ID="txtCurPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="font-weight:bold;">输入新口令:</td><td><asp:TextBox ID="txtNewPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="font-weight:bold;">重新输入新口令:</td><td><asp:TextBox ID="txtReNewPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;"><asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="normalButton" 
                    onclick="btnSubmit_Click" /></td>
        </tr>
    </table>
    </asp:Content>
