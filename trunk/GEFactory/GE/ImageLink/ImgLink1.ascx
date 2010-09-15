<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImgLink1.ascx.cs" Inherits="GE_ImgLink1" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<table width="100%" cellspacing="0">
        <uc1:Pub ID="Pub1" runat="server" />
    <tr>
        <td class="TD">
            <uc1:Pub ID="Pub2" runat="server" />
            <cc1:GeImage ID="GeImage1" runat="server" GloDBType="DataTable" GloRepeatColumns="2"
                ShowPage="False">
                <GloDBColumns>
                    <cc1:MyListItem ID="MyListItem1" runat="server" DataFormatString="&lt;a target='@Target' href=&quot;@Url&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth'  /&gt;&lt;/a&gt;"
                        DataTextField="ImgUrl">
                        <UrlListItems>
                            <cc1:UrlList ParaName="Url" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="Target" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                    <cc1:MyListItem ID="MyListItem2" runat="server" DataFormatString="&lt;a style=&quot;width:@ImgWidth;overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;&quot; target='@Target' href=&quot;@Url&quot;&gt;{0}&lt;/a&gt;"
                        DataTextField="Name">
                        <UrlListItems>
                            <cc1:UrlList ParaName="Url" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="Target" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                </GloDBColumns>
            </cc1:GeImage>
        </td>
    </tr>
</table>
