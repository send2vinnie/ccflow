<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XPager.ascx.cs" Inherits="CCOA_Control_XPager" %>
<table style=" width:100%; font-family:'微软雅黑';">
	<tr>
		<td><font style="FONT-SIZE: 12px" size="2"><span>共有</span>&nbsp;&nbsp;
				<asp:label id="lblRecordCount"  CssClass="font12pxgreen" runat="server"></asp:label>&nbsp;&nbsp;<span>条记录&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				共&nbsp;&nbsp;</span>
				<asp:label id="lblPageCount" CssClass="font12pxgreen" runat="server"></asp:label>&nbsp;&nbsp;<span>页&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				当前是第&nbsp;&nbsp;</span>
				<asp:label id="lblCurrentPage" CssClass="font12pxgreen" runat="server"></asp:label>&nbsp;&nbsp;<span>页</span>
			</font>
		</td>
		<td align="right"><font style="FONT-SIZE: 12px">
				<asp:linkbutton id="lbnFirstPage" runat="server" CausesValidation="false" Text="首页" CommandName="first" OnClick="lbnFirstPage_Click"></asp:linkbutton>&nbsp;
				<asp:linkbutton id="lbnPrevPage" runat="server" CausesValidation="false" Text="上一页" CommandName="prev" OnClick="lbnPrevPage_Click"></asp:linkbutton>&nbsp;
				<asp:linkbutton id="lbnNextPage" runat="server" CausesValidation="false" Text="下一页" CommandName="next" OnClick="lbnNextPage_Click"></asp:linkbutton>&nbsp;
				<asp:linkbutton id="lbnLastPage" runat="server" CausesValidation="false" Text="尾页" CommandName="last" OnClick="lbnLastPage_Click"></asp:linkbutton></font></td>
		<td align="right"><font style="FONT-SIZE: 12px" size="2">

				第
				<asp:textbox onkeypress="javascript:if (event.keyCode == 13) {return false};" id="SearchIndex" cssclass="wenben"
					runat="server" Width="38px">1</asp:textbox>页 </font>&nbsp; <asp:LinkButton id="goPage" runat="server" style="FONT-SIZE: 12px" Text="转到" Height="20px" onclick="goPage_Click"></asp:LinkButton>
		</td>
	</tr>
</table>