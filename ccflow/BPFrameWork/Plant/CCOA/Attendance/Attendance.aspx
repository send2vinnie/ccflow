<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs" Inherits="CCOA_Attendance_Attendance" %>

<%@ Register Src="~/CCOA/Attendance/Left.ascx" TagName="Left" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 200px; background: #F2F9FF; height: 500px; vertical-align: top;">
                <uc:Left ID="Left" runat="server" />
            </td>
            <td style="vertical-align: top;">
                <div style="background: url(Img/check.png) no-repeat; height: 100px; border: solid 1px #E5e5e5;
                    line-height: 100px; text-align: center;">
                    <span id="liveclock" style="width: 109px; height: 15px"></span>
                    <script type="text/javascript">
                        function www_codefans_net() {
                            var Digital = new Date()
                            var hours = Digital.getHours()
                            var minutes = Digital.getMinutes()
                            var seconds = Digital.getSeconds()
                            if (minutes <= 9)
                                minutes = "0" + minutes
                            if (seconds <= 9)
                                seconds = "0" + seconds
                            myclock = "<font size='6' face='Arial black'>" + hours + ":" + minutes + ":" + seconds + "</font>"
                            if (document.layers) {
                                document.layers.liveclock.document.write(myclock)
                                document.layers.liveclock.document.close()
                            } else if (document.all)
                                liveclock.innerHTML = myclock
                            setTimeout("www_codefans_net()", 1000)
                        }
                        www_codefans_net();
                    </script>
                    <asp:ImageButton ID="ibtnCheck" runat="server" ImageUrl="Img/kq.png" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
