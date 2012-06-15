<%@ Page Title="人员岗位" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="BP.EIP.Web.Port_Menu.List" %>

<%@ Register Src="../../CCOA/Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <!--#include file="../inc/html_head.inc" -->
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function getSelectedNode() {
            var tree = mini.get("tree1");
            var node = tree.getSelectedNode();
            if (node) {
                alert(node.text);
            } else {
                alert("请选中节点");
            }
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
    <br />
    <table width="100%">
        <tr>
            <td style="width: 200px; vertical-align: top;">
                <div>
                    <h3>
                        选择系统</h3>
                    <%--<ul id="tree1" class="mini-tree" url="../../DataUser/tree.txt" style="width: 200px;
                        padding: 5px;" showtreeicon="true" textfield="text" idfield="id" onnodeclick="getSelectedNode">
                    </ul>--%>
                    <ul>
                        <%foreach (BP.EIP.Port_App item in AppList)
                          {%>
                        <li><a href="List.aspx?app=<%=item.No %>">
                            <%=item.AppName %></a> </li>
                        <% } %>
                    </ul>
                </div>
            </td>
            <td style="vertical-align: top;">
                <lizard:XGridView ID="gridView" runat="server" Width="100%" CellPadding="3" OnPageIndexChanging="gridView_PageIndexChanging"
                    BorderWidth="1px" DataKeyNames="No" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false"
                    PageSize="10" RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                                <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="MenuNo" HeaderText="菜单号" SortExpression="MenuNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Pid" HeaderText="父级编号" SortExpression="Pid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FK_Function" HeaderText="FK_Function" SortExpression="FK_Function"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <asp:BoundField DataField="MenuName" HeaderText="MenuName" SortExpression="MenuName"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <asp:BoundField DataField="Title" HeaderText="标题" SortExpression="Title" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Img" HeaderText="图标" SortExpression="Img" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Url" HeaderText="Url地址" SortExpression="Url" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Path" HeaderText="路径" SortExpression="Path" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                        <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="No"
                            DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" Visible="false" />
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
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
