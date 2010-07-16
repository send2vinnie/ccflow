<%@ Page language="c#" CodeFile="ClearDatabase.aspx.cs" AutoEventWireup="false" Inherits="BP.Web.WF.WF.DeleteZF" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DeleteZF</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			
            <table>
                <tr>
                    <td style="width: 262px; height: 119px"><asp:Button id="Button1" style="Z-INDEX: 101; LEFT: 533px; POSITION: absolute; TOP: 61px" runat="server"
				Text="清除执法表数据"></asp:Button>
                        <asp:Button ID="Button2" runat="server" Text="清楚流程数据" OnClick="Button2_Click" /></td>
                    <td style="width: 299px; height: 119px">
                        <asp:Button ID="Button3" runat="server" Text="清除微机定税数据" OnClick="Button3_Click" /></td>
                    <td style="width: 422px; height: 119px">
                        </td>
                </tr>
                <tr>
                <td colspan=3  ><font color=red >在执行删除前一定要确认，这些运行中的数据是否需要。<BR>一但执行删除数据将无法恢复。</font>
                </td>
                </tr>
            </table>
		</form>
	</body>
</HTML>
