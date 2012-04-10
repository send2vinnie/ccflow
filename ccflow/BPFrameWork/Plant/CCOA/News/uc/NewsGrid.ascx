<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsGrid.ascx.cs" Inherits="CCOA_News_uc_NewsGrid" %>
<%@ Register Src="~/CCOA/Controls/XSearch.ascx" TagName="XSearch" TagPrefix="uc" %>
<div style="text-align: right; width: 96.5%; height: 40px;">
    <table cellpadding="0" cellspacing="0" style="float: right;">
        <tr>
            <td>
                <uc:XSearch ID="xSearch" runat="server" ShowDateRange="true" />
            </td>
            <td>
                <base:XButton ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:GridView ID="grid" CssClass="grid" runat="server" OnRowDataBound="grid_RowDataBound"
    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" BorderWidth="0" EmptyDataText="没有数据">
    <Columns>
        <asp:TemplateField HeaderText="编号" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblNo" runat="server" Text='<%# Eval("NO")%>'></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="F_ID" HeaderText="合同编号" />
        <asp:TemplateField HeaderText="合同名称">
            <ItemTemplate>
                <base:xlabel id="lblName" runat="server" text='<%# Eval("F_NAME") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="F_ENGINEERING_NAME" HeaderText="所属项目名称" Visible="false" />
        <asp:BoundField DataField="F_CATEGORY" HeaderText="合同类别" Visible="false" />
        <asp:BoundField DataField="F_CONSTRUCTION_ORGANIZATION" HeaderText="对方单位" />
        <asp:TemplateField HeaderText="合同金额" HeaderStyle-Width="50px">
            <ItemTemplate>
                <base:xlabel id="lblF_CONTRACT_AMOUNT" runat="server" text='<%# Eval("F_CONTRACT_AMOUNT")%>'
                    dataformat="Currency" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="决算金额" HeaderStyle-Width="50px">
            <ItemTemplate>
                <base:xlabel id="lblF_BIDDING_AMOUNT" runat="server" text='<%# Eval("F_BIDDING_AMOUNT")%>'
                    dataformat="Currency" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="F_PROPERTY" HeaderText="合同性质" Visible="false" />
        <asp:BoundField DataField="F_SDATE" HeaderText="计划开工日期" DataFormatString="{0:yyyy-MM-dd}"
            Visible="false" />
        <asp:BoundField DataField="F_EDATE" HeaderText="计划竣工日期" DataFormatString="{0:yyyy-MM-dd}"
            Visible="false" />
        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" Visible="false">
            <ItemTemplate>
                <base:xlinkbutton id="lnkEdit" runat="server" text="修改" onclick="lnkEdit_Click" commandargument='<%# Eval("NO") %>' />
                <base:xlinkbutton id="lnkDelete" runat="server" text="删除" commandargument='<%# Eval("NO") %>'
                    onclick="lnkDelete_Click" onclientclick="return confirm('不可恢复，你确定要删除吗？')" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
