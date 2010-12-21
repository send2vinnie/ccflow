<%@ Page language="c#" Inherits="BP.Web.Comm.UI.CalendarHelp1" CodeFile="Calendarhelp.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CalendarHelp</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body  class="Body<%=BP.Web.WebUser.Style%>">
		<form id="CalendarHelp" method="post" runat="server">
			<FONT face="宋体">
				<asp:Calendar id="Calendar1" style="Z-INDEX: 101; LEFT: 1px; POSITION: absolute; TOP: 0px" runat="server"
					BackColor="White" Width="330px" ForeColor="Black" Height="250px" Font-Size="9pt" Font-Names="Verdana"
					BorderColor="Black" BorderStyle="Solid" NextPrevFormat="ShortMonth" CellSpacing="1">
					<TodayDayStyle ForeColor="White" BackColor="#999999"></TodayDayStyle>
					<DayStyle BackColor="#CCCCCC"></DayStyle>
					<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="White"></NextPrevStyle>
					<DayHeaderStyle Font-Size="8pt" Font-Bold="True" Height="8pt" ForeColor="#333333"></DayHeaderStyle>
					<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
					<TitleStyle Font-Size="12pt" Font-Bold="True" Height="12pt" ForeColor="White" BackColor="#333399"></TitleStyle>
					<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
				</asp:Calendar>
				<cc1:Btn id="Btn_O" style="Z-INDEX: 102; LEFT: 55px; POSITION: absolute; TOP: 260px" accessKey="o"
					runat="server" Text="确定(O)" onclick="Btn_O_Click"></cc1:Btn>
				<cc1:Btn id="Btn_C" style="Z-INDEX: 103; LEFT: 174px; POSITION: absolute; TOP: 261px" runat="server"
					Text="取消(C)" onclick="Btn_C_Click"></cc1:Btn></FONT>
		</form>
	</body>
</HTML>
