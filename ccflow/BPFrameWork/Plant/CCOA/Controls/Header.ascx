<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="CCOA_Header" %>
<%@ Register Src="ImageButton.ascx" TagName="ImageButton" TagPrefix="uc1" %>
<%@ Register Src="JSClock.ascx" TagName="JSClock" TagPrefix="uc2" %>
<link href="../Style/control.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        var dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]

        var newDate = new Date();
        newDate.setDate(newDate.getDate());
        $('#Date').html(dayNames[newDate.getDay()] + " " + newDate.getDate() + ' ' + monthNames[newDate.getMonth()] + ' ' + newDate.getFullYear());

        setInterval(function () {
            var seconds = new Date().getSeconds();
            $("#sec").html((seconds < 10 ? "0" : "") + seconds);
        }, 1000);

        setInterval(function () {
            var minutes = new Date().getMinutes();
            $("#min").html((minutes < 10 ? "0" : "") + minutes);
        }, 1000);

        setInterval(function () {
            var hours = new Date().getHours();
            $("#hours").html((hours < 10 ? "0" : "") + hours);
        }, 1000);
    });


    function buttonClick(title, url) {
        addTab(title, url);
    }

</script>
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
                            <uc1:ImageButton ID="ImageButton1" runat="server" LinkUrl="Home.aspx" ImageUrl="../../CCOA/Images/png48/Home.png"
                                Title="工作台" OnClientClick="buttonClick" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton2" runat="server" LinkUrl="../../CCOA/News/List.aspx"
                                ImageUrl="../../CCOA/Images/png48/News.png" Title="我的资讯" OnClientClick="buttonClick" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton3" runat="server" ImageUrl="../../CCOA/Images/png48/Schedule.png"
                                Title="日程安排" OnClientClick="buttonClick" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton6" runat="server" LinkUrl="../../CCOA/Email/Inbox.aspx"
                                ImageUrl="../../CCOA/Images/png48/Email.png" Title="我的邮件" OnClientClick="buttonClick" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton4" runat="server" LinkUrl="../../CCOA/AddressBook/List.aspx"
                                ImageUrl="../../CCOA/Images/png48/Address-Book.png" Title="通讯录" OnClientClick="buttonClick" />
                        </li>
                        <li>
                            <uc1:ImageButton ID="ImageButton5" runat="server" LinkUrl="../../CCOA/Admin/Main.aspx"
                                ImageUrl="../../CCOA/Images/png48/Config.png" Title="配置管理" OnClientClick="buttonClick" />
                        </li>
                    </ul>
                </td>
                <td style="width: 260px; display: none;">
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
                <td>
                    <div class="clock">
                        <div id="Date">
                        </div>
                        <ul>
                            <li id="hours"></li>
                            <li id="point">:</li>
                            <li id="min"></li>
                            <li id="point">:</li>
                            <li id="sec"></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="HeaderSmallToolBar">
        <span class="userInfo">欢迎您，<%=BP.Web.WebUser.Name %>&nbsp;&nbsp;
            <asp:LinkButton ID="lbtExit" runat="server" OnClick="lbtExit_Click" ForeColor="Black">退出</asp:LinkButton></span>
    </div>
    <div style="clear: both;">
    </div>
</div>
