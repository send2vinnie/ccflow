<%@ Page Title="OA_Email" Language="C#" AutoEventWireup="true" CodeFile="Inbox.aspx.cs"
    Inherits="Lizard.OA.Web.OA_Email.Inbox" %>

<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<%@ Register Src="../Controls/MiniPager.ascx" TagName="MiniPager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Comm/Scripts/CheckBox.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" RefreshUrl="Inbox.aspx" />
    <table style="width: 100%; display: none;" cellpadding="2" cellspacing="1" class="border">
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
    <lizard:XGridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="No"
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated" CssClass="lizard-grid">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                    <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:TemplateField HeaderText="主题" SortExpression="Subject" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="Show.aspx?id=<%#Eval("No") %>">
                        <%# Eval("Subject")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Addresser" HeaderText="发件人" SortExpression="Addresser"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Addressee" HeaderText="收件人" SortExpression="Addressee"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PriorityLevel" HeaderText="类型：0-普通1-重要2-紧急" SortExpression="PriorityLevel"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Category" HeaderText="分类：0-收件箱1-草稿箱2-" SortExpression="Category"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center" />
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
    <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    &nbsp;&nbsp;<table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td align="left">
                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" Visible="false" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
