<%@ Page Title="OA_News" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="Lizard.OA.Web.OA_News.List" %>

<%@ Register Src="~/CCOA/Controls/MiniPager.ascx" TagPrefix="xuc" TagName="MiniPager" %>
<%@ Register Src="~/Comm/Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../Style/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CheckAll() {
            var frm = document.forms[0];
            for (var i = 0; i < frm.elements.length; i++) {
                var e = frm.elements[i];
                if ((e.name != 'allbox') && (e.type == 'checkbox')) {
                    e.checked = frm.allbox.checked;
                    alert('ddd');
                    //                    if (frm.allbox.checked) {
                    //                        hL(e);
                    //                    } 
                    //                    else {
                    //                        dL(e);
                    //                    } 
                }
            }
        }
        //CheckBox选择项
        function CCA(CB) {
            var frm = document.forms[0];
            if (CB.checked)
                hL(CB);
            else
                dL(CB);

            var TB = TO = 0;
            for (var i = 0; i < frm.elements.length; i++) {
                var e = frm.elements[i];
                if ((e.name != 'allbox') && (e.type == 'checkbox')) {
                    TB++;
                    if (e.checked)
                        TO++;
                }
            }
            //frm.allbox.checked = (TO == TB) ? true : false;
            //alert(TO);
        }


        function hL(E) {
            while (E.tagName != "TR")
            { E = E.parentElement; }
            E.className = "H";
        }

        function dL(E) {
            while (E.tagName != "TR")
            { E = E.parentElement; }
            E.className = "";
        }
    </script>
</head>
<body>
    <form runat="server">
    <div>
        <uc1:MiniToolBar ID="MiniToolBar1" runat="server" PopAddUrl="News/Add.aspx" />
        <!--Search end-->
        <div class="subtoolbar">
            <lizard:XDropDownList ID="ddlCategory" runat="server" Width="100" CssClass="mini-combobox">
                <asp:ListItem Text="未读新闻" Value="1" />
                <asp:ListItem Text="已读新闻" Value="2" />
                <asp:ListItem Text="全部新闻" Value="3" />
            </lizard:XDropDownList>
            &nbsp; 发布日期
            <lizard:XDatePicker ID="xdpCreateDate" runat="server" />
            &nbsp;<lizard:XButton ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" />
            &nbsp;<asp:LinkButton ID="lbtMarkReaded" runat="server" OnClick="lbtMarkReaded_Click">标记所有为已读</asp:LinkButton></div>
        <lizard:XGridView ID="gridView" runat="server" Width="100%" CellPadding="3" OnPageIndexChanging="gridView_PageIndexChanging"
            BorderWidth="1px" DataKeyNames="No" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false"
            PageSize="10" RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated"
            CssClass="lizard-grid">
            <Columns>
                <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                        <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:TemplateField HeaderText="新闻标题" SortExpression="NewsTitle" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href="Show.aspx?id=<%#Eval("No") %>" target="_blank">
                            <%# Eval("NewsTitle") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NewsSubTitle" HeaderText="副标题" SortExpression="NewsSubTitle"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="新闻类型" SortExpression="NewsType" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblNoticeType" runat="server" Text='<%# XTool.GetCatelogyByCode(Eval("NewsType")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NewsContent" HeaderText="新闻内容" SortExpression="NewsContent"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Author" HeaderText="发布人" SortExpression="Author" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Clicks" HeaderText="点击量" SortExpression="Clicks" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="是否阅读" SortExpression="IsRead" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIsRead" runat="server" Text='<%# IsRead(Eval("No").ToString()) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="UpUser" HeaderText="更新人" SortExpression="UpUser" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
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
    </div>
    </form>
</body>
</html>
