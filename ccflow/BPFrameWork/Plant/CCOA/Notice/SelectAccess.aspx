<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAccess.aspx.cs" Inherits="CCOA_Notice_SelectAccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc" -->
    <script type="text/javascript">

        function GetData() {
            var tree = mini.get("tree1");
            var nodes = tree.getCheckedNodes();
            var selecedDept = "";
            for (i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                selecedDept += node.text + ",";
            }
            selecedDept = selecedDept.substr(0, selecedDept.length - 1);
            return selecedDept;
        }

        function GetSelectedIds() {
            var tree = mini.get("tree1");
            var nodes = tree.getCheckedNodes();
            var selecedDept = "";
            for (i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                selecedDept += node.id + ",";
            }
            selecedDept = selecedDept.substr(0, selecedDept.length - 1);
            return selecedDept;
        }

        function CollapseAll() {
            var tree = mini.get("tree1");
            tree.collapseAll();
        }

        function ExpandAll() {
            var tree = mini.get("tree1");
            tree.expandAll();
        }

        function CloseWindow(action) {
            if (window.CloseOwnerWindow) window.CloseOwnerWindow(action);
            else window.close();
        }

        function onCancel() {
            CloseWindow("cancel");
        }

        function onOK() {
            var tree = mini.get("tree1");
            var nodes = tree.getCheckedNodes();
            if (nodes.length == 0) {
                alert("请选择部门！");
                return;
            }
            CloseWindow("ok");
        }

        function QueryDept() {
            var deptName = mini.get('txtDepartment').value;
            var tree = mini.get('tree1');
            var url = "../Common/LoadDeptTree.aspx?DeptName=" + deptName;
            tree.setUrl(url);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    部门名称：
                    <input id="txtDepartment" class="mini-textbox" style="width: 160px;" />
                    <a href="#" class="mini-button" onclick="QueryDept()">查询</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a class="mini-button" onclick="CollapseAll()" style="width: 70px; margin-right: 5px;">
                        全部折叠</a> <a class="mini-button" onclick="ExpandAll()" style="width: 70px; margin-right: 5px;">
                            全部展开</a>
                </td>
            </tr>
            <tr>
                <td>
                    <ul id="tree1" class="mini-tree" style="width: 280px; padding: 5px;" textfield="text"
                        idfield="id" url="../Common/LoadDeptTree.aspx" showcheckbox="true" parentfield="pid">
                    </ul>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <a class="mini-button" onclick="onOK" style="width: 60px; margin-right: 5px;">选择</a>
                    <a class="mini-button" onclick="onCancel" style="width: 60px; margin-right: 5px;">取消</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
