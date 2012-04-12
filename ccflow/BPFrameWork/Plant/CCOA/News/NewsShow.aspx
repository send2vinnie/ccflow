<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="NewsShow.aspx.cs" Inherits="CCOA_News_NewsShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="NewsShow">
        <div class="article mtop10">
            <div class="article_content">
                <h1>
                    <%=ThisArticle.Title%>
                </h1>
                <!--网站内容开始-->
                <div id="articleContnet">
                    <%=ThisArticle.Content%>
                </div>
                <!--网站内容结束-->
            </div>
        </div>
    </div>
</asp:Content>
