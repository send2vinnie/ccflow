<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeList.aspx.cs" Inherits="CCOA_Common_TreeList" %>

<%@ Register Src="../Controls/TreeList.ascx" TagName="TreeList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/miniui/miniui.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Comm/Scripts/CommonLibs/TreeSelectWindow.js" type="text/javascript"></script>
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
            if (selecedDept == "") {
                alert("请选择部门！");
                return;
            }
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

        function Cancel() {
            CloseWindow("cancel");
        }

        function onOk(e) {
            CloseWindow("ok");
        }
        function onCancel(e) {
            CloseWindow("cancel");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <ul id="tree1" class="mini-tree" style="width: 280px; padding: 5px;" textfield="text"
                idfield="id" url="<%=Url %>" showcheckbox="true" parentfield="pid">
            </ul>
            <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 5px;">选择</a>
            <a class="mini-button" onclick="onCancel" style="width: 60px; margin-right: 5px;">取消</a>
        </div>
    </div>
    </form>
</body>
</html>
