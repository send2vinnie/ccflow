<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="CCOA_Header" %>
<link href="Style/master.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        font-size: xx-large;
        font-family: 华文新魏;
        color: #0000FF;
    }
    .style2
    {
        font-size: medium;
    }
</style>
<div class="header">
    <div style="height: 60px; font-size: 40px; font-family: Arial Unicode MS; font-weight: bold;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <span class="style1">cc智能办公平台</span>
                </td>
                <td style="width: 260px;">
                    <span class="style2"><a href="../Home.aspx">CCOA</a></span> <span class="style2"><a
                        href="../Home.aspx">CCCRM</a></span> <span class="style2"><a href="../Home.aspx">CCHR</a></span>
                    <span class="style2"><a href="../Home.aspx">CCIM</a></span>
                </td>
            </tr>
        </table>
    </div>
    <div class="header_menu">
        <ul>
            <li><a href="../../CCOA/Home.aspx">桌面</a></li>
            <li><a href="../../CCOA/Attendance/Attendance.aspx">考勤</a></li>
            <li><a href="../../CCOA/News/NewsList.aspx">新闻公告</a></li>
            <%-- <li><a href="../../CCOA/Notice/NoticeList.aspx">公告通知</a></li>--%>
           <%-- <li><a href="../../CCOA/Forum/Forum.aspx">工作论坛</a></li>--%>
            <li><a href="../../CCOA/Document/Document.aspx">文档</a></li>
            <li><a href="../../CCOA/Memo/Memo.aspx">个人备忘</a></li>
            <%--<li><a href="../../CCOA/Calendar/Calendar.aspx">工作日历</a></li>--%>
            <li><a href="../../CCOA/SMS/SMS.aspx">短信平台</a></li>
        </ul>
    </div>
</div>
