<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
 
<%@ Register Src="Categories.ascx" TagName="Categories" TagPrefix="uc1" %>
<%@ Register Src="Products.ascx" TagName="Products" TagPrefix="uc2" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ｐｏｒｔａｌ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:WebPartManager ID="WebPartManager1" runat="server">
        </asp:WebPartManager>
        <table style="width: 594px">
            <tr>
                <td colspan="2" style="font-size: large; color: navy; font-family: Tahoma; background-color: silver">
                    Welcome to my Portal!</td>
                <td style="font-size: large; color: navy; font-family: Tahoma; background-color: silver">
                    <asp:Menu ID="ModesMenu" runat="server" OnMenuItemClick="ModesMenu_MenuItemClick">
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td style="width: 97px" valign="top">
                    <asp:CatalogZone ID="TheCatalogZone" runat="server" BackColor="#EFF3FB" BorderColor="#CCCCCC"
                        BorderWidth="1px" Font-Names="Verdana" Padding="6">
                        <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                        <PartTitleStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        <PartChromeStyle BorderColor="#D1DDF1" BorderStyle="Solid" BorderWidth="1px" />
                        <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                        <PartLinkStyle Font-Size="0.8em" />
                        <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                        <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                        <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <PartStyle BorderColor="#EFF3FB" BorderWidth="5px" />
                        <SelectedPartLinkStyle Font-Size="0.8em" />
                        <FooterStyle BackColor="#D1DDF1" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                        <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <ZoneTemplate>
                            <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                        </ZoneTemplate>
                    </asp:CatalogZone>
                </td>
                <td style="width: 308px" valign="top">
                    <asp:WebPartZone ID="TheContentZone" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                        Padding="6">
                        <PartChromeStyle BackColor="#EFF3FB" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="#333333" />
                        <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                        <EmptyZoneTextStyle Font-Size="0.8em" />
                        <MenuLabelStyle ForeColor="White" />
                        <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                            BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                        <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                        <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                        <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                            Font-Size="0.6em" />
                        <PartTitleStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        <ZoneTemplate>
                            <uc1:Categories ID="Categories1" runat="server" />
                            <uc2:Products ID="Products1" runat="server" />
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td valign="top">
                    <asp:WebPartZone ID="TheInfoZone" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                        Padding="6">
                        <PartChromeStyle BackColor="#EFF3FB" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="#333333" />
                        <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                        <EmptyZoneTextStyle Font-Size="0.8em" />
                        <MenuLabelStyle ForeColor="White" />
                        <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                            BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                        <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                        <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                        <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                            Font-Size="0.6em" />
                        <PartTitleStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        <ZoneTemplate    >
                            <asp:DropDownList ID="LocationsDropDownList" runat="server">
                                <asp:ListItem>London-12345</asp:ListItem>
                                <asp:ListItem>NY-26402</asp:ListItem>
                                <asp:ListItem>HK-6930</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                            <asp:AdRotator ID="AdRotator1" runat="server" AdvertisementFile="~/Ads.xml" />
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

