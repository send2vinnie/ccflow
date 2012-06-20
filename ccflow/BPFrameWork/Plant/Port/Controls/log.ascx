<%@ Control Language="C#" AutoEventWireup="true" CodeFile="log.ascx.cs" Inherits="Port_Controls_log" %>
 <script src="../../CCOA/Js/usbkey.js" type="text/javascript" language="javascript"></script>
<Table class='Table' cellpadding='2' cellspacing='2'>
<TR><TD class=C align=left colspan=1><img src='Img/Login.gif' > <b>系统登陆</b></TD>
</TR>
<TR><TD align=left ><Table class='Table' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#C0C0C0' border=1px align=left  >
<TR>
<TD  nowrap >用户名：</TD>
<TD >
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox></TD>
</TR>
<TR>
<TD  nowrap >密码：</TD>
<TD >
    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox></TD>
</TR>
<TR class='TRSum' >
<TD  colspan=3 align=center nowrap >
    <lizard:XButton ID="btnLogin" runat="server" Text="登录" onclick="btnLogin_Click" /><lizard:XButton ID="btn1" runat="server" style="display:none" Text="登录" onclick="btn1_Click" />- <a href='Tools.aspx?RefNo=AutoLog' >授权方式登录</a> - <a href='Login.aspx?DoType=Logout' ><font color=green><b>安全退出</b></a>
</TD>
</TR></Table><BR><BR>
</TD>
</TR></Table>
