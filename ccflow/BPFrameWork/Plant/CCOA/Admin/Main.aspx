<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="CCOA_Admin_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 590px;
            height: 417px;
        }
        .style2
        {
            height: 417px;
            width: 245px;
        }
    </style>
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setiFrame(sender) {
            var url = sender.href;
            $("#frameConfig").url = url;
        }
    </script>
</head>
<body style="margin: 0">
    <table>
        <tr>
            <td class="style2">
                界面设置
                <ul>
                    <li><a href="#" onclick="setiFrame(this)">界面主题</a></li>
                    <li><a href="#" onclick="setiFrame(this)">门户设置</a></li>
                    <li><a href="#" onclick="setiFrame(this)">菜单快捷组</a></li>
                    <li><a href="#" onclick="setiFrame(this)">自定义桌面</a></li>
                    <li><a href="#" onclick="setiFrame(this)">个人网址</a></li>
                    <li><a href="#" onclick="setiFrame(this)">收藏夹</a></li>
                </ul>
                个人信息
                <ul>
                    <li><a href="#" onclick="setiFrame(this)">个人资料</a></li>
                    <li><a href="#" onclick="setiFrame(this)">昵称与头像</a></li>
                    <li><a href="#" onclick="setiFrame(this)">自定义用户组</a></li>
                    <li><a href="#" onclick="setiFrame(this)">人员关注</a></li>
                </ul>
                帐号与安全
                <ul>
                    <li><a href="#" onclick="setiFrame(this)">我的帐户</a></li>
                    <li><a href="#" onclick="setiFrame(this)">修改密码</a></li>
                    <li><a href="#" onclick="setiFrame(this)">安全日志</a></li>
                </ul>
            </td>
            <td class="style1">
                <iframe id="frameConfig" style="height: 400px; width: 500px;"></iframe>
            </td>
        </tr>
    </table>
</body>
</html>
