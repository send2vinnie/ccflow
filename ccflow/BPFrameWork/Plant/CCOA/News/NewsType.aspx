<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="NewsType.aspx.cs" Inherits="CCOA_News_NewsType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    新闻类型：
    <div>
        <table width="96%;">
            <tr>
                <th>
                    名称
                </th>
                <th>
                    描述
                </th>
            </tr>
            <% foreach (BP.CCOA.ArticleType item in ArticleTypes)
               {%><tr>
               <td>
                   <%=item.Name %>
               </td>
               <td>
                   <%=item.Desc%>
               </td>
           </tr>
            <%} %>
        </table>
    </div>
</asp:Content>
