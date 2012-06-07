<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="CCOA_Header" %>
<%@ Register Src="ImageButton.ascx" TagName="ImageButton" TagPrefix="uc1" %>
<%@ Register Src="JSClock.ascx" TagName="JSClock" TagPrefix="uc2" %>
<link href="../Style/control.css" rel="stylesheet" type="text/css" />
<div class="header">
    <div style="height: 80px; font-size: 40px; font-family: Arial Unicode MS; font-weight: bold;
        background: url(../../CCOA/Images/top_bg.jpg) repeat-x;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;">
                    <div style="margin-left: 30px;">
                        <img alt="" src="../../CCOA/Images/oa_logo.png" height="40px" />
                    </div>
                </td>
                <td id="imageTool">
                    <ul>
                        <li>
                            <uc1:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../CCOA/Images/png48/Home.png"
                                Text="工作台" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton2" runat="server" ImageUrl="../../CCOA/Images/png48/News.png"
                                Text="我的资讯" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton3" runat="server" ImageUrl="../../CCOA/Images/png48/Schedule.png"
                                Text="日程安排" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton6" runat="server" ImageUrl="../../CCOA/Images/png48/Email.png"
                                Text="我的邮件" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton4" runat="server" ImageUrl="../../CCOA/Images/png48/Address-Book.png"
                                Text="通讯录" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton5" runat="server" ImageUrl="../../CCOA/Images/png48/Config.png"
                                Text="配置管理" />
                        </li>
                    </ul>
                </td>
                <td style="width: 260px;">
                    <span class="style2"><a href="../Home.aspx">CCOA</a></span> <span class="style2"><a
                        href="../Home.aspx">CCCRM</a></span> <span class="style2"><a href="../Home.aspx">CCIM</a></span>
                    <select id="selectSkin" onchange="onSkinChange(this.value)" style="width: 100px;">
                        <option value="default">Default</option>
                        <option value="blue">Blue</option>
                        <option value="gray">Gray</option>
                        <option value="olive2003">Olive2003</option>
                        <option value="blue2003">Blue2003</option>
                        <option value="blue2010">Blue2010</option>
                    </select>
                    <div id="showClock">
                        <%--<uc2:JSClock ID="JSClock1" runat="server" />--%>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="HeaderSmallToolBar">
        <span class="userInfo">欢迎您，<%=BP.Web.WebUser.Name %></span>
    </div>
    <div style="clear: both;">
    </div>
</div>
