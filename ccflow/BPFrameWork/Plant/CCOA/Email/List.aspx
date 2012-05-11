﻿<%@ Page Title="OA_Email" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="Lizard.OA.Web.OA_Email.List" %>

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
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="EmailId"
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EmailId" HeaderText="主键Id" SortExpression="EmailId" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="Subject" HeaderText="主题" SortExpression="Subject" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Addresser" HeaderText="发件人" SortExpression="Addresser"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Addressee" HeaderText="收件人" SortExpression="Addressee"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Content" HeaderText="邮件内容" SortExpression="Content" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PriorityLevel" HeaderText="类型：0-普通1-重要2-紧急" SortExpression="PriorityLevel"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Category" HeaderText="分类：0-收件箱1-草稿箱2-" SortExpression="Category"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="SendTime" HeaderText="发送时间" SortExpression="SendTime"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="IsDel" HeaderText="是否已删除" SortExpression="IsDel" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="EmailId"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="EmailId"
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
