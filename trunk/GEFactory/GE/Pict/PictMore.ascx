<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PictMore.ascx.cs" Inherits="GE_Pict_PictMore" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<table width="990px" cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top" style="width: 300px">
            <uc1:Pub ID="Pub1" runat="server" />
        </td>
        <td style="width: 800px;" valign="top" style="width: 685px;">
            <table class="Table" cellpadding="0" cellspacing="0">
                <tr class="TR">
                    <uc1:Pub ID="Pub2" runat="server" />
                </tr>
                <tr class="TR">
                    <td class="TD">
                        <uc1:Pub ID="Pub3" runat="server" />
                        <cc1:GeImage ID="GeImage1" runat="server" GloDBType="DataTable" PageSize="10" ShowPage="True">
                            <GloDBColumns>
                                <cc1:MyListItem ID="MyListItem1" runat="server" DataFormatString="&lt;a href=&quot;PictDtl.aspx?RefNo=@No&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth' style=&quot;border:none&quot; /&gt;&lt;/a&gt;"
                                    DataTextField="ImgUrl" EnableViewState="False">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                                <cc1:MyListItem ID="MyListItem2" runat="server" DataFormatString="&lt;a href=&quot;PictDtl.aspx?RefNo=@No&quot;&gt;{0}&lt;/a&gt;"
                                    DataTextField="Title" EnableViewState="True">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                            </GloDBColumns>
                        </cc1:GeImage>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
