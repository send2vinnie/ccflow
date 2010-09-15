<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SoftDtl.ascx.cs" Inherits="Comm_GE_Soft_SoftDtl" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<table width="100%" style="table-layout:fixed">
    <tr>
        <td valign="top" class="BigDoc" width='25%' style="text-align: justify">
            <uc1:Pub ID="Pub1" runat="server" />
            <table class="Table" style="border-collapse: collapse; margin-top: 5px;">
                <tr>
                    <uc1:Pub ID="Pub4" runat="server" />
                </tr>
                <asp:Panel ID="PanelPJ" runat="server" Visible="False">
                    <tr>
                        <td class="TD">
                            <cc1:GePJ ID="GePJ1" runat="server" IsShowPic="True" IsShowTitle="True" NewsGroup="1"
                                PJGroup="1"  DisplayMode="Vertical">
                            </cc1:GePJ>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </td>
        <td valign="top" class="BigDoc" width='75%'>
            <uc1:Pub ID="Pub2" runat="server" />
            <table class="Table" style="table-layout:fixed;border-collapse: collapse; margin-top: 5px;">
                <tr>
                    <uc1:Pub ID="Pub3" runat="server" />
                </tr>
                <asp:Panel ID="PanelComment" runat="server" Visible="False">
                    <tr>
                        <td class="TD">
                            <cc1:GeComment ID="GeComment1" runat="server" ShowType="ShowModaldialog">
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
                            </cc1:GeComment>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </td>
    </tr>
</table>
