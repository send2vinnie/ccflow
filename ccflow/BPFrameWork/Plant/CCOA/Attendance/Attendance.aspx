<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs" Inherits="CCOA_Attendance_Attendance" %>

<%@ Register Src="~/CCOA/Attendance/Left.ascx" TagName="Left" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function time() {
            var date = new Date();
            var month = date.getMonth() + 1;
            var d = date.getDate();
            var day = date.getDay();
            var Hours = date.getHours();
            var Minutes = date.getMinutes();
            var Seconds = date.getSeconds()
            var y = date.getFullYear();
            if (Minutes <= 9) {
                Minutes = "0" + Minutes;
            }
            if (Seconds <= 9) {
                Seconds = "0" + Seconds;
            }
            var times = new Array("日", "一", "二", "三", "四", "五", "六");
//            var liveclock = document.getElementById('liveclock');
//            liveclock.innerHTML = "今天是" + y + "年" + month + "月" + d + "号 星期" + times[day] + Hours + ":" + Minutes + ":" + Seconds;
            setTimeout("time()", 1000);
        }
        time();

        function setframe(id) {
            var frame = document.getElementById('iframe1');
//            alert(id);
            if (frame!=null) {
                if (id == "check") {
                    frame.src = "Check.aspx";
                }
                else
                    frame.src = "List.aspx";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 200px; background: #F2F9FF; height: 500px; vertical-align: top;">
                <uc:Left ID="Left" runat="server" />
            </td>
            <td>
                <div>
                    <span id="liveclock" style="width: 109px; height: 15px"></span>
                    <iframe id="iframe1" style="none" src="check.aspx" width="100%" height="500px" scrolling="no" frameborder="0"></iframe>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
