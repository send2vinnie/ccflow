<%@ Page Title="OA_Message" Language="C#" AutoEventWireup="true" CodeFile="Manage.aspx.cs"
    Inherits="Lizard.OA.Web.OA_Message.Manage" %>

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
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" RefreshUrl="Manage.aspx" DeleteUrl="Delete.aspx?EmailType=0"
        IsDeleteUrlHaveParamerter="true" />
    &nbsp; 发送日期：
    <lizard:xdatepicker id="xdpCreateDate" runat="server" />
    &nbsp;<lizard:xbutton id="btnOk" runat="server" text="确定" onclick="btnOk_Click" />
    &nbsp;
    <br />
    <lizard:xgridview id="gridView" runat="server" width="100%" cellpadding="3" onpageindexchanging="gridView_PageIndexChanging"
        borderwidth="1px" datakeynames="No" onrowdatabound="gridView_RowDataBound" autogeneratecolumns="False"
        rowstyle-horizontalalign="Center" onrowcreated="gridView_OnRowCreated" cssclass="lizard-grid">
        <columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                     <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:TemplateField HeaderText="消息标题" SortExpression="MessageName" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="Show.aspx?id=<%#Eval("No") %>" target="_blank">
                        <%# Eval("MessageName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
          <%--  <asp:BoundField DataField="MessageName" HeaderText="消息标题" SortExpression="MessageName"
                ItemStyle-HorizontalAlign="Center" />--%>
               <asp:TemplateField HeaderText="消息类型" SortExpression="MeaageType" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblMessageType" runat="server" Text='<%# XTool.GetCatelogyByCode(Eval("MeaageType")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Author" HeaderText="发布人" SortExpression="Author" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="AccessType" HeaderText="发布类别" SortExpression="AccessType"
                ItemStyle-HorizontalAlign="Center" />
                <%--asp:TemplateField HeaderText="是否阅读" SortExpression="IsRead" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblClicks" runat="server" Text='<%# XTool.ConvertBooleanText(Eval("ReadFlag")) %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="UpDT" HeaderText="最后更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:BoundField DataField="Status" HeaderText="状态：1-有效0-无效" SortExpression="Status"
                ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" Visible="false"/>
            <%--<asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />--%>
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </columns>
    </lizard:xgridview>
    <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    </form>
</body>
</html>
