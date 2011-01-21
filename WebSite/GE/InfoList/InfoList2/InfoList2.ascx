<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoList2.ascx.cs" Inherits="GE_Info_InfoList2" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Panel ID="PanelList" runat="server" Visible="false">
    <table width='100%'>
        <tr>
            <td>
                <uc1:Pub ID="PubList" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:Pub ID="PubPage" runat="server" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PanelDtl" runat="server" Visible="false">
    <table width='100%'>
        <uc1:Pub ID="PubBack" runat="server" />
        <tr>
            <td class="BigDoc" valign="top">
                <table width="100%">
                    <uc1:Pub ID="PubTitle" runat="server" />
                    <uc1:Pub ID="PubContent" runat="server" />
                    <asp:Panel ID="PanelComment" runat="server" Visible="False">
                        <tr>
                            <uc1:Pub ID="PubComTitle" runat="server" />
                        </tr>
                        <tr>
                            <td>
                                <cc1:GeComment ID="GeComment1" runat="server" ShowType="ShowModaldialog">
                                    <GloDBColumns>
                                        <cc1:MyListItem ID="MyListItem1" runat="server" DataTextField="FK_EmpT" ItemStyle="commentli">
                                        </cc1:MyListItem>
                                        <cc1:MyListItem ID="MyListItem7" runat="server" DataTextField="IP" ItemStyle="commentli">
                                        </cc1:MyListItem>
                                        <cc1:MyListItem ID="MyListItem2" runat="server" DataTextField="RDT" ItemStyle="commentli">
                                        </cc1:MyListItem>
                                    </GloDBColumns>
                                </cc1:GeComment>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
            <asp:Panel ID="PanelRTD" runat="server" Visible="true">
                <td class="BigDoc" style="width: 28%" valign="top">
                    <table style="border-collapse: collapse;">
                        <asp:Panel ID="PanelPJ" runat="server" Visible="False">
                            <tr>
                                <uc1:Pub ID="PubPJTitle" runat="server" />
                            </tr>
                            <tr>
                                <td class="TD">
                                    <cc1:GePJ ID="GePJ1" runat="server" IsShowPic="True" IsShowTitle="False" NewsGroup="1"
                                        PJGroup="1" DisplayMode="Vertical">
                                    </cc1:GePJ>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="PanelHisView" runat="server" Visible="false">
                            <tr>
                                <td>
                                    <cc1:GeMyView ID="GeMyView1" runat="server" RecommendNum="0" VistorNum="0" WeeklySortNum="0">
                                    </cc1:GeMyView>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
            </asp:Panel>
        </tr>
    </table>
</asp:Panel>
