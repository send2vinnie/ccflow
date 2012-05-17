<%@ Page Title="OA_Meeting" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="Lizard.OA.Web.OA_Meeting.List" %>

<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<%@ Register Src="../Controls/MiniPager.ascx" TagName="MiniPager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <!--Search end-->
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
    &nbsp;<lizard:XGridView ID="gridView" runat="server" AllowPaging="True" Width="100%"
        CellPadding="3" OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px"
        DataKeyNames="No" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false"
        PageSize="10" RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                    <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="Topic" HeaderText="议题" SortExpression="Topic" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanStartTime" HeaderText="计划开始时间" SortExpression="PlanStartTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanEndTime" HeaderText="计划结束时间" SortExpression="PlanEndTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanAddress" HeaderText="计划召开地址" SortExpression="PlanAddress"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanMembers" HeaderText="计划参加人员" SortExpression="PlanMembers"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="RealStartTime" HeaderText="实际开始时间" SortExpression="RealStartTime"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="RealEndTime" HeaderText="实际结束时间" SortExpression="RealEndTime"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="RealAddress" HeaderText="实际召开地址" SortExpression="RealAddress"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="RealMembers" HeaderText="实际参加人员" SortExpression="RealMembers"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Recorder" HeaderText="记录人" SortExpression="Recorder" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Summary" HeaderText="会议纪要" SortExpression="Summary" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="UpUser" HeaderText="更新人" SortExpression="UpUser" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="Status" HeaderText="状态：0-未召开1-已召开" SortExpression="Status"
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
    </lizard:XGridView>
    <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    </form>
</body>
</html>
