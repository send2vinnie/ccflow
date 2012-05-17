<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XPager.ascx.cs" Inherits="CCOA_Control_XPager" %>
<link href="../Style/control.css" rel="stylesheet" type="text/css" />
<table class="lizard-pager" style="width: 100%;">
    <tr>
        <td align="left">
            <font style="font-size: 12px">
                <asp:LinkButton ID="lbnFirstPage" runat="server" CausesValidation="false" Text="首页"
                    CommandName="first" OnClick="lbnFirstPage_Click"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbnPrevPage" runat="server" CausesValidation="false" Text="上一页"
                    CommandName="prev" OnClick="lbnPrevPage_Click"></asp:LinkButton>&nbsp;第
                <asp:TextBox onkeypress="javascript:if (event.keyCode == 13) {return false};" ID="SearchIndex"
                    CssClass="wenben" runat="server" Width="38px">1</asp:TextBox>页 &nbsp;
                <asp:LinkButton ID="goPage" runat="server" Style="font-size: 12px" Text="转到" Height="20px"
                    OnClick="goPage_Click"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbnNextPage" runat="server" CausesValidation="false" Text="下一页"
                    CommandName="next" OnClick="lbnNextPage_Click"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbnLastPage" runat="server" CausesValidation="false" Text="尾页"
                    CommandName="last" OnClick="lbnLastPage_Click"></asp:LinkButton></font>
        </td>
        <td align="right">
            <font style="font-size: 12px" size="2"><span>共有</span>&nbsp;&nbsp;
                <asp:Label ID="lblRecordCount" CssClass="font12pxgreen" runat="server"></asp:Label>&nbsp;&nbsp;<span>条记录&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    共&nbsp;&nbsp;</span>
                <asp:Label ID="lblPageCount" CssClass="font12pxgreen" runat="server"></asp:Label>&nbsp;&nbsp;<span>页&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    当前是第&nbsp;&nbsp;</span>
                <asp:Label ID="lblCurrentPage" CssClass="font12pxgreen" runat="server"></asp:Label>&nbsp;&nbsp;<span>页</span>
            </font>
        </td>
    </tr>
</table>
