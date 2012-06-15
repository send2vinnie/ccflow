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
    <link href="../Style/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" RefreshUrl="Inbox.aspx" DeleteUrl="Delete.aspx?EmailType=0"
        IsDeleteUrlHaveParamerter="true" />
   <div class="subtoolbar">  <lizard:xdropdownlist id="ddlCategory" runat="server" width="100">
        <asp:ListItem Text="未读邮件" Value="1" />
        <asp:ListItem Text="已读邮件" Value="2" />
        <asp:ListItem Text="全部邮件" Value="3" />
    </lizard:xdropdownlist>
    &nbsp; 发送日期：
    <lizard:xdatepicker id="xdpCreateDate" runat="server" />
    &nbsp;<lizard:xbutton id="btnOk" runat="server" text="确定" onclick="btnOk_Click" />
    &nbsp;
    <asp:LinkButton ID="lbtReaded" CssClass="mini-button" runat="server" OnClick="lbtReaded_Click">标记所有为已读</asp:LinkButton>
   </div>
    <lizard:xgridview id="gridView" runat="server" width="100%" cellpadding="3" onpageindexchanging="gridView_PageIndexChanging"
        borderwidth="1px" datakeynames="No" onrowdatabound="gridView_RowDataBound" autogeneratecolumns="false"
        pagesize="10" rowstyle-horizontalalign="Center" onrowcreated="gridView_OnRowCreated"
        cssclass="lizard-grid">
        <columns>
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
                ItemStyle-HorizontalAlign="Center" Visible="false"/>
              <asp:TemplateField   HeaderText="发件人" SortExpression="Addresser" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                  <asp:Label id="lblSendPeople" runat="server" text='<%# XEmailTool.GetSendPeople(Eval("Addresser")) %>' />
                </ItemTemplate>
                  </asp:TemplateField>
            <asp:BoundField DataField="Addressee" HeaderText="收件人" SortExpression="Addressee"
                ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:BoundField DataField="PriorityLevel" HeaderText="类型：0-普通1-重要2-紧急" SortExpression="PriorityLevel"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Category" HeaderText="分类：0-收件箱1-草稿箱2-" SortExpression="Category"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
             <asp:BoundField DataField="SendTime" HeaderText="发送时间" SortExpression="SendTime" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField   HeaderText="是否阅读" SortExpression="ReadFlag" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                  <asp:Label id="lblClicks" runat="server" text='<%# XTool.ConvertBooleanText(Eval("ReadFlag")) %>' />
                </ItemTemplate>
                  </asp:TemplateField>
            <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" Visible="false" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" Visible="false"/>
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </columns>
    </lizard:xgridview>
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
