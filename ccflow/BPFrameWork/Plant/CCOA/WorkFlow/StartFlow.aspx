<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StartFlow.aspx.cs" Inherits="CCOA_WorkFlow_StartFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc"-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <lizard:XGridView ID="gvData" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FK_FlowSortText") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnName" runat="server" OnClick="lbtnName_Click" Text='<%# Bind("Name") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="描述">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </lizard:XGridView>
    </div>
    </form>
</body>
</html>
