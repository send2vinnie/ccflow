<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuTree.aspx.cs" Inherits="CCOA_MenuTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function killErrors() {
            return true;
        }
        window.onerror = killErrors;	
    </script>
    <script src="Js/moo.fx.js" type="text/javascript"></script>
    <script src="Js/moo.fx.pack.js" type="text/javascript"></script>
    <script src="Js/prototype.lite.js" type="text/javascript"></script>
    <link href="Style/menu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h1 class="title" align="left">
        <a href="javascript:void(0)">
            <img src="images/menu/netchat.gif" width="16px" height="16px" border="0">个人事务</a></h1>
    <asp:TreeView ID="TreeView1" runat="server" CollapseImageUrl="~/CCOA/Images/2.gif"
        ExpandImageUrl="~/CCOA/Images/1.gif" NodeIndent="10" ShowLines="True">
        <Nodes>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="部门通知" Value="1111b" ImageUrl="~/CCOA/Images/menu/notify.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/notify.gif" Text="部门通知浏览" Value="1111b1"
                    NavigateUrl="~/MyAffairs/MyUnitNotic.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/notify.gif" Text="部门通知管理" Value="1111b2"
                    NavigateUrl="~/MyAffairs/UnitNotic.aspx"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="公司通知" Value="1111c" ImageUrl="~/CCOA/Images/menu/news.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/news.gif" Text="公司通知浏览" Value="1111c1"
                    NavigateUrl="~/MyAffairs/MyCompanyNotic.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/news.gif" Text="公司通知管理" Value="1111c2"
                    NavigateUrl="~/MyAffairs/CompanyNotic.aspx"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="电子刊物" Value="1111d" ImageUrl="~/CCOA/Images/menu/file_folder.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/file_folder.gif" Text="电子刊物浏览" Value="1111d1"
                    NavigateUrl="~/MyAffairs/MyElecBook.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/file_folder.gif" Text="电子刊物管理" Value="1111d2"
                    NavigateUrl="~/MyAffairs/ElecBook.aspx"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode ImageUrl="~/CCOA/Images/menu/calendar.gif" Text="日程安排" Value="1111f"
                NavigateUrl="~/MyAffairs/MyCalendar.aspx"></asp:TreeNode>
            <asp:TreeNode ImageUrl="~/CCOA/Images/menu/diary.gif" Text="工作日志" Value="1111g" NavigateUrl="~/MyAffairs/MyDiary.aspx">
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="我的会议" Value="1111h" ImageUrl="~/CCOA/Images/menu/meeting.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/meeting.gif" Text="我的会议" Value="1111h1"
                    NavigateUrl="~/MyAffairs/MyMetting.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/vmeet.gif" Text="网络会议" Value="1111h3"
                    NavigateUrl="~/MyAffairs/MyNetMetting.aspx"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="通讯录管理" Value="1111i" Expanded="False" SelectAction="Expand" ImageUrl="~/CCOA/Images/menu/address.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/address.gif" Text="公司通讯录" Value="1111i1"
                    NavigateUrl="~/MyAffairs/CompanyGroup.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/address.gif" Text="个人通讯录" Value="1111i2"
                    NavigateUrl="~/MyAffairs/MyGroup.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/address.gif" Text="公共通讯录" Value="1111i3"
                    NavigateUrl="~/MyAffairs/PublicGroup.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/address.gif" Text="通讯录类别" Value="1111i4"
                    NavigateUrl="~/MyAffairs/GroupType.aspx"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode ImageUrl="~/CCOA/Images/menu/file_folder.gif" Text="个人文件柜" Value="1111k"
                NavigateUrl="~/MyAffairs/Folders.aspx"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="个人设置" Value="1111l" ImageUrl="~/CCOA/Images/menu/person_info.gif">
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="快捷菜单设置" Value="1111l1">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="用户提醒设置" Value="1" NavigateUrl="~/MyAffairs/MyReminded.aspx">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="密码修改" Value="1" NavigateUrl="~/MyAffairs/SystemPassword.aspx">
                </asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="邮件参数设置" Value="1111l4"
                    NavigateUrl="~/MyAffairs/Emailprv.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="常用审批备注" Value="1111l5"
                    NavigateUrl="~/MyAffairs/UseSpRemark.aspx"></asp:TreeNode>
                <asp:TreeNode ImageUrl="~/CCOA/Images/menu/winexe.gif" Text="常用模版设置" Value="1111l6"
                    NavigateUrl="~/MyAffairs/OftenModle.aspx"></asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
    </asp:TreeView>
    <script type="text/javascript">

        parent.closeAlert('UploadChoose');
	</script>
    </form>
</body>
</html>
