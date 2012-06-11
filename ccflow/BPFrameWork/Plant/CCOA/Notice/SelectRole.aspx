<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRole.aspx.cs" Inherits="CCOA_Notice_SelectRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc" -->
    <script type="text/javascript">

        function GetData() {
            //已选择
            var selectedList = mini.get("selectedList");
            selectedList.selectAll();
            var items = selectedList.getSelecteds();
            var valueField = selectedList.valueField;
            var selecedDept = "";
            for (var i = 0; i < items.length; i++) {
                var o = items[i];
                //var id = o[valueField];
                selecedDept += o.text + ",";
            }
            selecedDept = selecedDept.substr(0, selecedDept.length - 1);
            return selecedDept;
        }

        function GetSelectedIds() {
            //已选择
            var selectedList = mini.get("selectedList");
            selectedList.selectAll();
            var items = selectedList.getSelecteds();
            var valueField = selectedList.valueField;
            var selecedDept = "";
            for (var i = 0; i < items.length; i++) {
                var o = items[i];
                var id = o[valueField];
                selecedDept += id + ",";
            }
            selecedDept = selecedDept.substr(0, selecedDept.length - 1);
            return selecedDept;
        }

        function CloseWindow(action) {
            if (window.CloseOwnerWindow) window.CloseOwnerWindow(action);
            else window.close();
        }

        function onCancel() {
            CloseWindow("cancel");
        }

        function onOK() {
            var selectedList = mini.get("selectedList");
            selectedList.selectAll();
            var items = selectedList.getSelecteds();
            if (items.length == 0) {
                alert("请选择角色！");
                return;
            }
            CloseWindow("ok");
        }

        function QueryRole() {
            var txtRole = mini.get("txtRole");
            var roleName = txtRole.value;
            var selectPeopleList = mini.get("selectPeople");
            selectPeopleList.setUrl("../Common/LoadRoleTree.aspx?RoleName=" + roleName);
        }

        //选择
        function addSelected() {
            //已选择
            var selectedList = mini.get("selectedList");
            //选择
            var selectList = mini.get("selectPeople");
            var items = selectList.getSelecteds();

            var valueField = selectedList.valueField;

            for (var i = 0; i < items.length; i++) {
                var o = items[i];
                selectedList.addItem(o);
            }
        }

        //清空
        function ClearSelected() {
            if (!confirm("确定要清空吗？")) {
                return;
            }
            var selectedList = mini.get("selectedList");
            selectedList.removeAll();
        }

        //删除
        function DeleteSeleced() {
            if (confirm("确定要删除选中的角色吗？")) {
                var selectedList = mini.get("selectedList");
                var selectedItems = selectedList.getSelecteds();
                selectedList.removeItems(selectedItems);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                角色名称：
                <input id="txtRole" class="mini-textbox" style="width: 160px;" />
                <a href="#" class="mini-button" onclick="QueryRole()">查询</a>
            </td>
        </tr>
        <tr>
            <td>
                <%-- <ul id="tree1" class="mini-tree" style="width: 280px; padding: 5px;" textfield="text"
                    idfield="id" url="LoadRoleTree.aspx" showcheckbox="true" parentfield="pid">
                </ul>--%>
                <h4 style="margin: 0; line-height: 22px; font-size: 13px;">
                    选择角色：</h4>
                <div id="selectPeople" url="../Common/LoadRoleTree.aspx" class="mini-listbox" style="width: 250px;
                    height: 200px;" showcheckbox="true" multiselect="true">
                    <div property="columns">
                        <div field="text" width="150" headeralign="center" allowsort="true">
                            角色</div>
                    </div>
                </div>
            </td>
            <td style="padding: 5px;">
                <table>
                    <tr>
                        <td>
                            <a class="mini-button" onclick="addSelected() " style="padding: 5px;">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="mini-button" onclick="ClearSelected()" style="padding: 5px;">清空</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a class="mini-button" onclick="DeleteSeleced()" style="padding: 5px;">删除</a>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <h4 style="margin: 0; line-height: 22px; font-size: 13px;">
                    已选角色：</h4>
                <div id="selectedList" class="mini-listbox" style="width: 250px; height: 200px;"
                    showcheckbox="true" multiselect="true">
                    <div property="columns">
                        <div field="text" width="150" headeralign="center" allowsort="true">
                            角色</div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <a class="mini-button" onclick="onOK" style="width: 60px; margin-right: 5px;">确定</a>
                <a class="mini-button" onclick="onCancel" style="width: 60px; margin-right: 5px;">取消</a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
