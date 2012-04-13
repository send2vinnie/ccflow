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
            <td>
                <div>
                <span id="liveclock" style"=width: 109px; height: 15px"></span>
   
                    <img src="Img/kq.png" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
