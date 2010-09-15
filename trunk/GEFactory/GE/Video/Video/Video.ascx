<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Video.ascx.cs" Inherits="GE_Video_Video" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<table cellspacing="0">
    <tr>
        <uc1:Pub ID="PubUp" runat="server" />
    </tr>
    <tr>
        <td class="TD">
            <uc1:Pub ID="Pub1" runat="server" />
            <cc1:GeImage ID="GeImage1" runat="server" GloDBType="DataTable" GloRepeatColumns="2"
                ShowPage="False">
                <GloDBColumns>
                    <cc1:MyListItem ID="MyListItem1" runat="server" DataFormatString="&lt;a target='_blank' href=&quot;VideoDtl.aspx?RefNo=@No&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth'  /&gt;&lt;/a&gt;"
                        DataTextField="ImgUrl">
                        <UrlListItems>
                            <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                    <cc1:MyListItem ID="MyListItem2" runat="server" DataFormatString="&lt;a style=&quot;width:'@ImgWidth';overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;&quot; target='_blank' href=&quot;VideoDtl.aspx?RefNo=@No&quot;&gt;{0}&lt;/a&gt;"
                        DataTextField="Title">
                        <UrlListItems>
                            <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                </GloDBColumns>
            </cc1:GeImage>
        </td>
    </tr>
</table>
