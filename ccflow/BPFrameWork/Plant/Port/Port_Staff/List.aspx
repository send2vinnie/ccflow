﻿<%@ Page Title="PORT_STAFF" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="BP.EIP.Web.PORT_STAFF.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td style="width: 80px" align="right" class="tdbg">
                <b>关键字：</b>
            </td>
            <td class="tdbg">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <lizard:XButton ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click"></lizard:XButton>
            </td>
            <td class="tdbg">
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <asp:GridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames=""
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NO" HeaderText="NO" SortExpression="NO" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="EMPNO" HeaderText="EMPNO" SortExpression="EMPNO" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AGE" HeaderText="AGE" SortExpression="AGE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="IDCARD" HeaderText="IDCARD" SortExpression="IDCARD" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PHONE" HeaderText="PHONE" SortExpression="PHONE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" SortExpression="EMAIL" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="UPUSER" HeaderText="UPUSER" SortExpression="UPUSER" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FK_DEPT" HeaderText="FK_DEPT" SortExpression="FK_DEPT"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="EMPNAME" HeaderText="EMPNAME" SortExpression="EMPNAME"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SEX" HeaderText="SEX" SortExpression="SEX" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BIRTHDAY" HeaderText="BIRTHDAY" SortExpression="BIRTHDAY"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CREATEDATE" HeaderText="CREATEDATE" SortExpression="CREATEDATE"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="UPDT" HeaderText="UPDT" SortExpression="UPDT" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields=""
                DataNavigateUrlFormatString="Show.aspx?" Text="详细" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields=""
                DataNavigateUrlFormatString="Modify.aspx?" Text="编辑" />
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
                <lizard:XButton ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
