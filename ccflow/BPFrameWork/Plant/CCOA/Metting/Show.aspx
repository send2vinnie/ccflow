<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Meeting.Show"
    Title="显示页" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMeetingId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            议题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblTopic" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            计划开始时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPlanStartTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            计划结束时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPlanEndTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            计划召开地址 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPlanAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            计划参加人员 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPlanMembers" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            实际开始时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRealStartTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            实际结束时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRealEndTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            实际召开地址 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRealAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            实际参加人员 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRealMembers" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            记录人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRecorder" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            会议纪要 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSummary" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            更新人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUpUser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            更新时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUpDT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            状态：0-未召开1-已召开 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
