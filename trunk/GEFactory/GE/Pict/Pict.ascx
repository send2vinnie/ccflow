<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pict.ascx.cs" Inherits="GE_Pict_Pict" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<table cellspacing="0" style="margin-top: 5px; width: 767px;">
    <tr>
        <uc1:Pub ID="Pub1" runat="server" />
    </tr>
    <tr>
        <td class="cont_body">
            <uc1:Pub ID="Pub2" runat="server" />
            <cc1:GeImage ID="GeImage1" runat="server" GloDBType="DataTable" GloRepeatColumns="2"
                ShowPage="False">
                <GloDBColumns>
                    <cc1:MyListItem ID="MyListItem1" runat="server" DataFormatString="&lt;a href=&quot;PictDtl.aspx?RefNo=@No&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth'  /&gt;&lt;/a&gt;"
                        DataTextField="ImgUrl">
                        <UrlListItems>
                            <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                            <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                    <cc1:MyListItem ID="MyListItem2" runat="server" DataFormatString="&lt;a href=&quot;PictDtl.aspx?RefNo=@No&quot;&gt;{0}&lt;/a&gt;"
                        DataTextField="Name">
                        <UrlListItems>
                            <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                        </UrlListItems>
                    </cc1:MyListItem>
                </GloDBColumns>
            </cc1:GeImage>
        </td>
    </tr>
</table>
