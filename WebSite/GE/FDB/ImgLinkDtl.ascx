<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImgLinkDtl.ascx.cs" Inherits="GE_ImgLink_ImgLinkDtl" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<cc1:GeImage ID="GeImage1" runat="server" ImageListStyle-ImgTitlePosition="Bottom"
    ImageListStyle-TableStyle="" GloDBType="DataTable" ImageListStyle-PicStyle=".img">
    <GloDBColumns>
        <cc1:MyListItem ID="MyListItem4" runat="server" DataFormatString="&lt;a href=&quot;@Url &quot; target=&quot;@Target&quot;  &gt;&lt;img src='{0}' class='img' Width='@ImageW' Height='@ImageH'/&gt;&lt;/a&gt;"
            DataTextField="imgSrc">
            <UrlListItems>
                <cc1:UrlList ParaName="Url" ValueFrom="DataRow" />
                <cc1:UrlList ParaName="ImageH" ValueFrom="DataRow" />
                <cc1:UrlList ParaName="ImageW" ValueFrom="DataRow" />
                <cc1:UrlList ParaName="Target" ValueFrom="DataRow" />
            </UrlListItems>
        </cc1:MyListItem>
        <cc1:MyListItem ID="MyListItem5" runat="server" DataTextField="Name" HeaderText="Title:">
        </cc1:MyListItem>
    </GloDBColumns>
</cc1:GeImage>
