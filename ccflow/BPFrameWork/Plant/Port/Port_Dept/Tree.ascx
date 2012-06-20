<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tree.ascx.cs" Inherits="Port_Port_Dept_Tree" %>
<div>
    组织结构：
    <ul id="tree1" class="mini-tree" url="Json.aspx" style="width: 300px; padding: 5px;"
        showtreeicon="true" textfield="text" idfield="id" parentfield="pid" contextmenu="#treeMenu">
    </ul>
    <ul id="treeMenu" class="mini-menu" style="display: none;" onbeforeopen="onBeforeOpen">
        <li iconcls="icon-move" onclick="onMoveNode">移动节点</li>
        <li class="separator"></li>
        <li><span iconcls="icon-add">新增节点</span>
            <ul>
                <li onclick="onAddBefore">插入节点前</li>
                <li onclick="onAddAfter">插入节点后</li>
                <li onclick="onAddNode">插入子节点</li>
            </ul>
        </li>
        <li name="edit" iconcls="icon-edit" onclick="onEditNode">编辑节点</li>
        <li name="remove" iconcls="icon-remove" onclick="onRemoveNode">删除节点</li>
    </ul>
</div>
<script type="text/javascript">
    function onAddBefore(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        var newNode = {};
        tree.addNode(newNode, "before", node);
    }
    function onAddAfter(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        var newNode = {};
        tree.addNode(newNode, "after", node);
    }
    function onAddNode(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        var newNode = {};
        tree.addNode(newNode, "add", node);
    }
    function onEditNode(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        tree.beginEdit(node);
    }
    function onRemoveNode(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        if (node) {
            if (confirm("确定删除选中节点?")) {
                tree.removeNode(node);
            }
        }
    }
    function onMoveNode(e) {
        var tree = mini.get("tree1");
        var node = tree.getSelectedNode();

        alert("moveNode");
    }
    function onBeforeOpen(e) {
        var menu = e.sender;
        var tree = mini.get("tree1");

        var node = tree.getSelectedNode();
        if (node && node.text == "Base") {
            e.cancel = true;
            //阻止浏览器默认右键菜单
            e.htmlEvent.preventDefault();
            return;
        }

        ////////////////////////////////
        var editItem = mini.getbyName("edit", menu);
        var removeItem = mini.getbyName("remove", menu);
        editItem.show();
        removeItem.enable();

        if (node.id == "forms") {
            editItem.hide();
        }
        if (node.id == "lists") {
            removeItem.disable();
        }
    }
</script>
