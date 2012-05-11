<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavBar.ascx.cs" Inherits="XControls_NavBar" %>
<div id="navbar1" class="mini-navbarmenu" url="<%=MenuUrl %>" activeindex="0" style="width: 100%;
    height: 500px;" autocollapse="true" idfield="id" parentfield="pid" textfield="text"
    resultastree="false" onitemselect="onItemSelect">
</div>
<script type="text/javascript">

    function onItemSelect(e) {
        var item = e.item;
        //iframe.src = item.url;
        //alert(item.url);
        addTab(item.text, item.url);
    }
    var index = 1;
    function addTab(itemTitle, itemUrl) {
        var tabs = mini.get("tabs1");
        var objTabs = tabs.tabs;
        for (var i = 0; i < objTabs.length; i++) {
            if (objTabs[i].title == itemTitle) {
                tabs.activeTab(objTabs[i]);
                return;
            }
        }

        //add tab
        var i = index++;
        var tab = { title: itemTitle, url: itemUrl, showCloseButton: true };
        tab.ondestroy = function (e) {
            var tabs = e.sender;
            var iframe = tabs.getTabIFrameEl(e.tab);

            //获取子页面返回数据
            var pageReturnData = iframe.contentWindow.getData ? iframe.contentWindow.getData() : "";

            //alert(e.tab.removeAction + " : " + pageReturnData);

            //如果禁止销毁的时候，自动active一个新tab：e.autoActive = false;
        }
        tabs.addTab(tab);

        //active tab
        tabs.activeTab(tab);
    }
</script>
