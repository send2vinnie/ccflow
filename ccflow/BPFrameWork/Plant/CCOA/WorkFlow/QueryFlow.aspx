<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryFlow.aspx.cs" Inherits="CCOA_WorkFlow_QueryFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc"-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <lizard:XGridView ID="gvData" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审批事项">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FLOWNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目编号">
                    <ItemTemplate>
                        <asp:Label ID="lbPrjNo" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:Label ID="lbPrjName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTitle" runat="server" OnClick="lbtnTitle_Click" CommandArgument='<%# Bind("OID") %>'
                            Text='<%# Bind("Title") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发起人">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("EMPS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发起时间">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("RDT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="流程编号" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lbNo" runat="server" Text='<%# Bind("OID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工作轨迹">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnWorkChart" runat="server" CommandArgument='<%# Bind("OID") %>'
                            OnClick="lbtnWorkChart_Click">查 看</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FK_Flow" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lbFK_Flow" runat="server" Text='<%# Bind("FK_Flow") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FID" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lbFID" runat="server" Text='<%# Bind("FID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </lizard:XGridView>
    </div>
    </form>
</body>
</html>
