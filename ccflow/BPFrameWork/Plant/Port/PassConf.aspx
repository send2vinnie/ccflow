<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassConf.aspx.cs" Inherits="Port_PassConf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <link href="Style/Main.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/general.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
		<tr><td>
		<table cellpadding="0" cellspacing="0" border="0" align="center" valign="middle" width="380">
			<TR><TD colspan="3" height="4" bgcolor="#ECC182"></TD></TR>
			<TR><TD width="10" height="27" valign="top" bgcolor="#1A286E">
			</TD><td width="100%">&nbsp;</td>
			<TD width="10" valign="top" bgcolor="#1A286E">
			</TD></TR>
			<TR><TD></TD>
			<td class="pageTitle">修改密码:&nbsp;&nbsp;</td><TD></TD></TR>
			<TR><TD width="10" height="2"></TD>
			<TD height="2">
				<img src="/unauth/images/seperator.gif" width="100%" height="2" border="0" align="absmiddle"></TD>
			<TD width="10" height="2"></TD></TR>
			<tr><td colspan="3" height="10"></td></tr>
			<TR bgcolor="#EFEFEF" valign="middle">
      <TD width="10"></TD>
			<TD class="mainData">
				<table cellpadding="0" cellspacing="0" border="0">
					<tr>
					<td class="Label" align="left">输入当前口令:&nbsp;</td>
					<td align="left">
                        <asp:TextBox ID="txtCurPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox>
                    </td>
					</tr>
					<tr><td colspan="2" height="10"></td></tr>
					<tr>
					<td class="Label" align="left">输入新口令:&nbsp;</td>
					<td align="left">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox>
                    </td>
					</tr>
					<tr><td colspan="2" height="10"></td></tr>
					<tr>
					<td class="Label" align="left">重新输入新口令:&nbsp;</td>
					<td align="left">
						<asp:TextBox ID="txtReNewPassword" runat="server" CssClass="inputField" TextMode="Password" size="30"></asp:TextBox>
                    </td>
					</tr>
					</table>
			</TD><TD width="10"></TD></tr>
			<tr><td height="20"></td></tr>
			<tr><TD width="10"></TD>
			<td class="mainData" align="center">
                <lizard:XButton ID="btnSubmit" runat="server" Text="提交" CssClass="butt" 
                    onclick="btnSubmit_Click" />
			</td>
			<TD width="10"></TD>
			</tr>
			<TR><TD colspan="3" height="20"></TD></TR>
		  <TR><TD colspan="3" height="4" bgcolor="#ECC182"></TD></TR>
		</table>		
	</td></tr>
	<tr height="20%"><td></td></tr>
	</table>

    </form>
</body>
</html>
