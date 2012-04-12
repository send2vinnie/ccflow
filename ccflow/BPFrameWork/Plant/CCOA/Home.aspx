<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="CCOA_Home" %>

<%@ Register Src="~/CCOA/Controls/Article_Newest.ascx" TagName="Article_Newest" TagPrefix="uc" %>
<%@ Register Src="~/CCOA/Controls/Email.ascx" TagName="Email" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <uc:Article_Newest ID="Article_Newest1" runat="server" Title="新闻" ShowUrl="../CCOA/News/NewsShow.aspx" />
            </td>
            <td>
                <uc:Article_Newest ID="Article_Newest2" runat="server" Title="公告通知" />
            </td>
        </tr>
        <tr>
            <td>
                <uc:Email ID="Email1" runat="server" Title="电子邮件" />
            </td>
            <td>
                <uc:Article_Newest ID="Article_Newest4" runat="server" Title="待办事宜" />
            </td>
        </tr>
    </table>
</asp:Content>
