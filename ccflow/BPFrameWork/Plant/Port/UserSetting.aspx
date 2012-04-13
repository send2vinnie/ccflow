<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true" CodeFile="UserSetting.aspx.cs" Inherits="Port_UserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table width="800" class="grid2" style="margin-top:20px; style="text-align:center"" >
            <tr>
                <th colspan="5"> 修改应用程序配置</th>
            </tr>
            <tr>
                <td style="font-weight:bold" style="text-align:center">
                    系统编码
                </td>
                <td style="font-weight:bold" style="text-align:center">
                    域名/应用程序
                </td>
                <td style="font-weight:bold" style="text-align:center">
                    用户名
                </td>
                <td style="font-weight:bold" style="text-align:center">
                    口令
                </td>
                <td style="font-weight:bold" style="text-align:center">
                    测试连接
                </td>
            </tr>
            <asp:Repeater ID="rptSysList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                           <%# Eval("SysCustomerNo")%>
                        </td>
                        <td style="text-align:center">
                            <asp:HiddenField ID="hideSysNo" runat="server" Value='<%# Eval("No") %>' />
                            <%# Eval("SysName")%>
                        </td>
                        <td>
                            <asp:TextBox CssClass="inputField" ID="txtUserName" Text='' runat="server"></asp:TextBox>
                            <%--<input class="inputField" id="username" type="text" value='<%# Eval("UserName") %>' size="32" runat="server">--%>
                        </td>
                        <td>
                            <asp:TextBox CssClass="inputField" ID="txtPassword" runat="server" TextMode="Password" ></asp:TextBox>
                            <%--<input class="inputField" id="password" type="password" name="password[]" value="1234rewq1234"
                                size="35">--%>
                        </td>
                        <td style="text-align:center">
                            <asp:HyperLink ID="nyText" runat="server" Text="点击"></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="5" style="text-align:center">
                    <asp:Button CssClass="normalButton" ID="btnSumit" runat="server" OnClick="btnSubmit_Click" Text="提交" />
                </td>
            </tr>
        </table>
    </asp:Content>
