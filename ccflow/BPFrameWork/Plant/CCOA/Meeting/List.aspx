<%@ Page Title="OA_Meeting" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="Lizard.OA.Web.OA_Meeting.List" %>

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
        OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="MeetingId"
        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
        RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MeetingId" HeaderText="主键Id" SortExpression="MeetingId"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Topic" HeaderText="议题" SortExpression="Topic" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanStartTime" HeaderText="计划开始时间" SortExpression="PlanStartTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanEndTime" HeaderText="计划结束时间" SortExpression="PlanEndTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanAddress" HeaderText="计划召开地址" SortExpression="PlanAddress"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PlanMembers" HeaderText="计划参加人员" SortExpression="PlanMembers"
                ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="RealStartTime" HeaderText="实际开始时间" SortExpression="RealStartTime"
                ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="RealEndTime" HeaderText="实际结束时间" SortExpression="RealEndTime"
                ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="RealAddress" HeaderText="实际召开地址" SortExpression="RealAddress"
                ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="RealMembers" HeaderText="实际参加人员" SortExpression="RealMembers"
                ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="Recorder" HeaderText="记录人" SortExpression="Recorder" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Summary" HeaderText="会议纪要" SortExpression="Summary" ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="UpUser" HeaderText="更新人" SortExpression="UpUser" ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="Status" HeaderText="状态：0-未召开1-已召开" SortExpression="Status"
                ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="MeetingId"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="MeetingId"
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
