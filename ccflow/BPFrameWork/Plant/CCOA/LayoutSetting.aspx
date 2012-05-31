<%@ Page Title="EIP_LayoutDetail" Language="C#" AutoEventWireup="true" CodeFile="LayoutSetting.aspx.cs"
    Inherits="Lizard.OA.Web.EIP_LayoutDetail.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="Style/control.css" rel="stylesheet" type="text/css" />
    <link href="Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
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
    <lizard:XGridView ID="gridView" runat="server"  Width="100%" CellPadding="3"
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="No"
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择" Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="ColumnNo" HeaderText="列号" SortExpression="ColumnNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PanelId" HeaderText="板块ID" SortExpression="PanelId" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PanelTitle" HeaderText="板块标题" SortExpression="PanelTitle"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ShowCollapseButton" HeaderText="是否显示收缩按钮" SortExpression="ShowCollapseButton"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Width" HeaderText="宽度" SortExpression="Width" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Height" HeaderText="高度" SortExpression="Height" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="IsShow" HeaderText="是否显示" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Url" HeaderText="链接地址" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </lizard:XGridView>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td align="left">
                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
