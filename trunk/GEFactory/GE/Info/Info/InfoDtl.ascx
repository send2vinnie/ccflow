<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoDtl.ascx.cs" Inherits="Comm_GE_Info_InfoDtl" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<table width="100%" style="table-layout: fixed">
    <tr>
        <td valign="top" class="BigDoc" width='75%'>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background: #f5f8fd; padding: 10px; border: #89c6fd 1px solid; border-bottom: none;">
                        <uc1:Pub ID="PubH" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #f5f8fd; padding: 10px; height: 400px; border: #89c6fd 1px solid;
                        border-top: none;" valign="top">
                        <table style="border-collapse: collapse; margin-top: 10px; width: 100%">
                            <uc1:Pub ID="PubContent" runat="server" />
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height='5px'>
                    </td>
                </tr>
                <asp:Panel ID="PanelComment" runat="server" Visible="False">
                    <tr>
                        <uc1:Pub ID="Pub5" runat="server" />
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
                                </GloDBColumns>
                            </cc1:GEComment>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td height='5px'>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:Pub ID="Pub2" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top" class="BigDoc" width='25%'>
            <asp:Panel ID="PanelViewHistory" runat="server" Visible="False">
                <table style="border-collapse: collapse; table-layout: fixed; width: 100%">
                    <tr>
                        <td>
                            <cc1:GEMyView ID="GeMyView1" runat="server" MyHistoryNum="6" RecommendNum="0" VistorNum="0"
                                CssFile="StyleSheet.css" WeeklySortNum="0">
                            </cc1:GEMyView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <uc1:Pub ID="PubXG" runat="server" />
            <table class="Table" style="border-collapse: collapse; table-layout: fixed; margin-top: 5px">
                <tr>
                    <uc1:Pub ID="Pub4" runat="server" />
                </tr>
                <asp:Panel ID="PanelPJ" runat="server" Visible="False">
                    <tr>
                        <td class="TD">
                            <cc1:GEPJ ID="GePJ1" runat="server" IsShowPic="True" IsShowTitle="True" NewsGroup="1"
                                PJGroup="1" DisplayMode="Vertical">
                            </cc1:GEPJ>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </td>
    </tr>
</table>
