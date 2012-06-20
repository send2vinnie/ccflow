<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysManager.aspx.cs" Inherits="Port_SysManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        * { font-family: "微软雅黑"; font-size:12px; }
        .bg { width:80%; margin:auto; }
        .grid { text-align:center; width:98%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bg">
    <table>
        <tr>
            <td>所属分组：</td>
            <td>
            <asp:DropDownList ID="ddlGroup" runat="server">
                <asp:ListItem Text="企业办公" Value="1"></asp:ListItem>
                <asp:ListItem Text="协同办公" Value="2"></asp:ListItem>
                <asp:ListItem Text="管理计划" Value="3"></asp:ListItem>
                <asp:ListItem Text="业务管理" Value="4"></asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>系统名称：</td><td><asp:TextBox ID="txtSysName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>系统地址：</td><td><asp:TextBox ID="txtSysUrl" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>系统描述：</td><td><asp:TextBox ID="txtSysDescription" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>显示排序：</td><td><asp:TextBox ID="txtSysOrder" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><lizard:XButton ID="btnAdd" runat="server" Text="添加" 
                    onclick="btnAdd_Click" /></td>
        </tr>
    </table>
    <lizard:XGridView ID="grid" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="grid" 
        onrowcancelingedit="grid_RowCancelingEdit" onrowdeleting="grid_RowDeleting" 
        onrowediting="grid_RowEditing" onrowupdating="grid_RowUpdating">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="系统名称">
                <ItemTemplate><%# Eval("SysName") %></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtSysName" runat="server" Text='<%# Eval("SysName") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="系统地址">
                <ItemTemplate><%# Eval("SysUrl") %></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtSysUrl" runat="server" Text='<%# Eval("SysUrl") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="系统描述">
                <ItemTemplate><%# Eval("SysDescription")%></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtSysDescription" runat="server" Text='<%# Eval("SysDescription") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="显示排序">
                <ItemTemplate><%# Eval("SysOrder")%></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtSysOrder" runat="server" Text='<%# Eval("SysOrder") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属组别">
                <ItemTemplate><%# Eval("SysGroup")%></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtSysGroup" runat="server" Text='<%# Eval("SysGroup") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </lizard:XGridView>
    </div>
    </form>
</body>
</html>
