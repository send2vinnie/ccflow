<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoTabsDtl.ascx.cs"
    Inherits="GE_Video_VideoTabsDtl" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%--<%@ Register Src="UC_SC.ascx" TagName="UC_SC" TagPrefix="uc3" %>--%>
<div class='typeList'>
    <span>视频分类：</span>
    <uc1:Pub ID="PubSort" runat="server" />
</div>
<table style="width: 100%">
    <tr>
        <td valign="top" class="BigDoc" style="width: 70%">
            <table class="Table" style="border-collapse: collapse; margin-top: 5px;">
                <tr>
                    <td class="TD" style="margin-bottom: 5px" colspan="2">
                        <uc1:Pub ID="PubVideo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <uc1:Pub ID="Pub5" runat="server" />
                </tr>
                <uc1:Pub ID="Pub3" runat="server" />
                <tr>
                    <td class="TD" style="margin-bottom: 5px" colspan="2">
                        <input type="hidden" id="isLogin" runat="server" />
                        <div class="divDownSC" style="text-align: right; border: #c6c6c6 1px solid; background-color: #f6f6f6;
                            margin-bottom: 5px">
                            <asp:Panel ID="PanelSC" runat="server" Visible="False">
                                <span style="float: right; padding-right: 10px;">
                                    <cc1:GEFavorite ID="GEFavorite1" runat="server">
                                    </cc1:GEFavorite>
                                </span>
                            </asp:Panel>
                            <uc1:Pub ID="PubDownLoad" runat="server" />
                        </div>
                        <uc1:Pub ID="PubVideoDoc" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="Table" style="border-collapse: collapse; margin-top: 5px;">
                <!-- 评价 -->
                <asp:Panel ID="PanelPJ" runat="server" Visible="False">
                    <tr>
                        <uc1:Pub ID="Pub7" runat="server" />
                    </tr>
                    <tr>
                        <td class="TD" style="margin-bottom: 5px">
                            <cc1:GEPJ ID="GePJ1" runat="server" IsShowPic="True" IsShowTitle="False" NewsGroup="1"
                                PJGroup="1" DisplayMode="Horizontal">
                            </cc1:GEPJ>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="PanelComment" runat="server" Visible="False">
                    <!-- 评论 -->
                    <tr>
                        <uc1:Pub ID="Pub8" runat="server" />
                    </tr>
                    <tr>
                        <td>
                            <cc1:GEComment ID="GeComment1" runat="server" ShowType="ShowModaldialog">
                                <GloDBColumns>
                                    <cc1:MyListItem ID="MyListItem1" runat="server" DataTextField="FK_EmpT" ItemStyle="commentli">
                                    </cc1:MyListItem>
                                    <cc1:MyListItem ID="MyListItem7" runat="server" DataTextField="IP" ItemStyle="commentli">
                                    </cc1:MyListItem>
                                    <cc1:MyListItem ID="MyListItem2" runat="server" DataTextField="RDT" ItemStyle="commentli">
                                    </cc1:MyListItem>
                                    <cc1:MyListItem ID="MyListItem3" runat="server" DataTextField="Title" ItemStyle="commentli">
                                    </cc1:MyListItem>
                                </GloDBColumns>
                            </cc1:GEComment>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </td>
        <td valign="top" class="BigDoc" style="width: 30%">
            <!-- 最近浏览 -->
            <asp:Panel ID="PanelViewHistory" runat="server" Visible="false">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <cc1:GEMyView ID="GeMyView1" runat="server" MyHistoryNum="6" RecommendNum="0" VistorNum="0"
                                CssFile="StyleSheet3.css" WeeklySortNum="0">
                            </cc1:GEMyView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="margin-bottom: 5px; width: 100%; border-collapse: collapse;">
                <!-- 相关信息 -->
                <tr>
                    <uc1:Pub ID="Pub6" runat="server" />
                </tr>
                <tr>
                    <td class='TD'>
                        <uc1:Pub ID="Pub4" runat="server" />
                        <cc1:GEImage ID="GeImage1" runat="server" GloDBType="DataTable" PageSize="10" ShowPage="False">
                            <GloDBColumns>
                                <cc1:MyListItem ID="MyListItem4" runat="server" DataFormatString="&lt;a href=&quot;VideoTabsDtl.aspx?RefNo=@No&quot;&gt;&lt;img src=&quot;{0}&quot; onerror=&quot;this.src='@DefImgSrc'&quot; height=&quot;@ImgHeight&quot; width='@ImgWidth' style=&quot;border:none&quot; /&gt;&lt;/a&gt;"
                                    DataTextField="ImgUrl" EnableViewState="False">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgHeight" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="DefImgSrc" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                                <cc1:MyListItem ID="MyListItem5" runat="server" DataFormatString="&lt;a style=&quot;width:'@ImgWidth';overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;&quot; href=&quot;VideoTabsDtl.aspx?RefNo=@No&quot;&gt;{0}&lt;/a&gt;"
                                    DataTextField="Title" EnableViewState="True">
                                    <UrlListItems>
                                        <cc1:UrlList ParaName="No" ValueFrom="DataRow" />
                                        <cc1:UrlList ParaName="ImgWidth" ValueFrom="DataRow" />
                                    </UrlListItems>
                                </cc1:MyListItem>
                                <cc1:MyListItem ID="MyListItem6" runat="server" DataFormatString="浏览次数：{0}" DataTextField="ViewTimes"
                                    EnableViewState="True">
                                </cc1:MyListItem>
                            </GloDBColumns>
                        </cc1:GEImage>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
