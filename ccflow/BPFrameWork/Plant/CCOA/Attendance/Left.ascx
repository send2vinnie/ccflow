<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="CCOA_Attendance_Left" %>
<table width="100%">
    <tr>
        <td style="width: 200px; background: #F2F9FF; height: 500px; vertical-align: top;">
            <div>
                <ul>
                    <%--  <li><a id="check" onclick="setframe(this)">考勤打卡</a></li>
                        <li><a id="list" onclick="setframe(this)">我的考勤</a></li>--%>
                    <li><a id="check" href="#" onclick="setframe('check')">考勤打卡</a></li>
                    <li><a id="list" href="#" onclick="setframe('list')">我的考勤</a></li>
                </ul>
            </div>
        </td>
    </tr>
</table>
