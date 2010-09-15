<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveName.aspx.cs" Inherits="GE_NetDisk_SaveName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self">
    <title>编辑目录</title>

    <script type="text/javascript" language="javascript">
        function CheckName() {
            var strLen = document.getElementById("tbDirName").value.length;
            if (strLen > 10) {
                alert('请输入10字符以内的名字！');
                return false;
            }
            if (strLen == 0) {
                alert('目录名不允许为空！');
                return false;
            }
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    请输入目录名：
                </td>
                <td>
                    <asp:TextBox ID="tbDirName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return CheckName()"
                        OnClick="btnSave_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
