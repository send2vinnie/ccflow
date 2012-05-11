<%@ Page Title="OA_Message" Language="C#" MasterPageFile="~/CCOA/WinOpen.master"
    AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Lizard.OA.Web.OA_Message.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--Title -->
    <!--Title end -->
    <!--Add  -->
    <!--Add end -->
    <!--Search -->
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td style="width: 80px" align="right" class="tdbg">
                <b>关键字：</b>
            </td>
            <td class="tdbg">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click"></asp:Button>
            </td>
            <td class="tdbg">
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <asp:GridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="No"
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MessageName" HeaderText="消息名称（标题）" SortExpression="MessageName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MeaageType" HeaderText="消息类型" SortExpression="MeaageType"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Author" HeaderText="发布人" SortExpression="Author" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="UpDT" HeaderText="最后更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="状态：1-有效0-无效" SortExpression="Status"
                ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td align="left">
                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
