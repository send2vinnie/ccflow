<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSetting.aspx.cs" Inherits="Port_UserSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Style/Main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        * { font-family: "微软雅黑" }
    </style>
</head>
<body style="background-color:#F7F7F3">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" align="center" valign="middle">
                    <tr>
                        <td colspan="2" height="4" bgcolor="#ECC182">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="appTitle">
                            修改应用程序配置:&nbsp;&nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="2">
                            <img src="/unauth/images/seperator.gif" width="100%" height="2" border="0" align="absmiddle">
                        </td>
                        <td width="2" height="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="10">
                        </td>
                    </tr>
                    <tr bgcolor="#EFEFEF" valign="middle">
                        <td class="appData">
                            <table border="1" cellspacing="1" cellpadding="1" width="800" class="ssoTable">
                                <tr>
                                    <td width="25%">
                                        <font color="#000000">域名/应用程序</font>
                                    </td>
                                    <td width="25%">
                                        <font color="#000000">描述</font>
                                    </td>
                                    <td width="25%">
                                        <font color="#000000">用户名</font>
                                    </td>
                                    <td width="25%">
                                        <font color="#000000">口令</font>
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptSysList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td width="25%">
                                                <asp:HiddenField ID="hideSysNo" runat="server" Value='<%# Eval("No") %>' />
                                                <%# Eval("SysName")%>
                                            </td>
                                            <td width="25%">
                                                <%# Eval("SysDescription")%>
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
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                            </table>
                        </td>
                        <td width="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="appData" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="butt" 
                                onclick="btnSubmit_Click"/>
                        </td>
                        <td width="2">
                        </td>
    </tr>
    <tr>
        <td colspan="2" height="20">
        </td>
    </tr>
    <tr>
        <td colspan="2" height="4" bgcolor="#ECC182">
        </td>
    </tr>
    </table> 
            </td>
        </tr>
        <tr height="20%">
            <td>
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
