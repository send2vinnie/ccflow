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
                <div class="article_info">
                    作者：<%=ThisArticle.Author %>
                    来源：<%=ThisArticle.Source %>
                    发布时间：
                    <%=ThisArticle.Updated%>
                    点击数：
                    <%=ThisArticle.Clicks %>
                </div>
                <!--网站内容开始-->
                <div class="Content" id="articleContnet">
                    <%=ThisArticle.Content%>
                </div>
                <!--网站内容结束-->
            </div>
        </div>
    </div>
</asp:Content>
