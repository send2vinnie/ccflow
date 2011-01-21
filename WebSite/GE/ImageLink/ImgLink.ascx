<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImgLink.ascx.cs" Inherits="GE_ImgLink_ImgLink" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="ImageSort.ascx" TagName="ImageSort" TagPrefix="uc1" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>

<table cellspacing="0" cellpadding="5" >
    <tr>
            <uc1:Pub ID="Pub1" runat="server" />
    </tr>
    <tr>
        <td class="TD">
            <cc1:GeImage ID="GeImage1" runat="server" ImageListStyle-ImgTitlePosition="Bottom"
                ImageListStyle-TableStyle="" GloDBType="DataTable" ImageListStyle-PicStyle=".img"
                ShowPage="False">
                <GloDBColumns>
                    <cc1:MyListItem ID="MyListItem4" runat="server" DataFormatString="&lt;a  href=&quot;@Url  &quot; target=&quot;@Target&quot;  &gt;&lt;img src='{0}' class='img' Width='@ImageW' Height='@ImageH'/&gt;&lt;/a&gt;" DataTextField="imgSrc">
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
        </td>
    </tr>
</table>

