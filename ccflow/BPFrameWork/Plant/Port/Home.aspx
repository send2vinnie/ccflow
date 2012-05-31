<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Port_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/master.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/jquery.js" type="text/javascript"></script>
    <script src="Javascript/drupal.js" type="text/javascript"></script>
    <script src="Javascript/jstools.js" type="text/javascript"></script>
    <script src="Javascript/collapsiblock.js" type="text/javascript"></script>
    <script src="Javascript/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">
        Drupal.extend(
        {
            settings:
             { "jstools":
                {
                    "cleanurls": false,
                    "basePath": "/portal/"
                },
                 "collapsiblock":
                 {
                     "block-block-1": "2",
                     "block-block-2": "2",
                     "block-block-3": "2",
                     "block-block-4": "2",
                     "block-block-5": "2",
                     "block-block-6": "2",
                     "block-user-3": "2",
                     "block-block-7": "2",
                     "block-block-8": "2",
                     "block-views-calendar": "2"
                 }
             }
        });
    </script>
    <script src="Scripts/Portal.js" type="text/javascript"></script>
    <link href="Scripts/portal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var portal = null;

        $.get("Ajax.aspx", { Action: "get", Name: "lulu" }, function (data, textStatus) {
            //返回的 data 可以是 xmlDoc, jsonObj, html, text, 等等.
            this;

            portal = new mini.ux.Portal();
            portal.set({
                style: "width: 100%;height:500px",
                columns: [250, "100%", 260]
            });
            portal.render(content);

            //panel
            var panels = JSON.parse(data);
            for (var i = 0; i < panels.length; i++) {
                panels[i].column = parseInt(panels[i].column);
            }
            //alert(JSON.stringify(test));
            portal.setPanels(panels);

            var bodyEl = portal.getPanelBodyEl("p2");
            //bodyEl.appendChild(document.getElementById(""));

            //获取配置的panels信息
            var panels = portal.getPanels();

        });

        function getJsonData() {
            var panels = portal.getPanels();
            var values = "";
            for (var i = 0; i < panels.length; i++) {
                var panelid = panels[i].id;
                var column = $("#" + panelid).parent().attr('id');
                var seq = i;
                values = values + panelid + ":" + column + "#" + seq + ",";
            }
            $("#txtjson").val(values);
        }

        function saveLayout() {
            var panels = portal.getPanels();
            var values = "";
            for (var i = 0; i < panels.length; i++) {
                var panelid = panels[i].id;
                var column = $("#" + panelid).parent().attr('id');
                var seq = i;
                values = values + panelid + ":" + column + "|" + seq + ",";
            }

            values = values.substring(0, values.length - 1);

            $.ajax({
                type: "POST",
                datatype: "text",
                url: "SaveLayout.aspx?params=" + values,
                success: function (data) {
                    alert(data);
                }
            });
        }

        function openLayout() {

            var index = 1;
            var tabs = mini.get("tabs1");
            var objTabs = tabs.tabs;
            for (var i = 0; i < objTabs.length; i++) {
                if (objTabs[i].title == "布局设置") {
                    tabs.activeTab(objTabs[i]);
                    return;
                }
            }

            //add tab
            var i = index++;
            var tab = { title: "布局设置", url: "LayoutSetting.aspx", showCloseButton: true };
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
    <style type="text/css">
        *
        {
            font-family: "微软雅黑";
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="tabs1" class="mini-tabs" activeindex="0" style="width: 100%; height: 515px;">
        <div title="工作台" iconcls="icon-add">
            <a href="#" onclick="openLayout()">布局设置</a> <a href="#" onclick="saveLayout()">保存配置</a>
            <div id="content">
                <div id="page" class="one-sidebar two-sidebars">
                    <div id="container" class="withleft withright clear-block">
                        <div id="main-wrapper">
                            <div id="main" class="clear-block">
                                <div class="panel-2col-stacked">
                                    <div class="panel-col-top">
                                        <div>
                                        </div>
                                    </div>
                                    <div class="panel-col-first">
                                        <div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-1">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪体育新闻
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-2">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪港台新闻
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-5">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪娱乐新闻
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-col-last">
                                        <div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-3">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪科技新闻
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-4">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪财经新闻
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block block-aggregator" id="block-aggregator-feed-6">
                                                <div class="blockinner">
                                                    <h2 class="title">
                                                        新浪文化教育
                                                    </h2>
                                                    <div class="content">
                                                        <div class="item-list">
                                                        </div>
                                                        <div class="more-link">
                                                            <a href="#" title="查看此feed的最新新闻。">更多</a></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-col-bottom">
                                        <div>
                                        </div>
                                    </div>
                                </div>
                                <br class="panel-clearer" />
                            </div>
                        </div>
                        <div id="sidebar-left" class="sidebar">
                            <% 
                                foreach (string key in DataSource.Keys)
                                {%>
                            <div class="block block-block">
                                <div class="blockinner">
                                    <h2 class="title">
                                        <%= key %>
                                    </h2>
                                    <div class="content">
                                        <ul>
                                            <% foreach (System.Data.DataRow row in DataSource[key])
                                               {%>
                                            <li><a href='<%= row["SysUrl"] %>' target="_blank">
                                                <%= row["SysName"] %></a> </li>
                                            <% }%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <%} %>
                        </div>
                        <div id="sidebar-right" class="sidebar">
                            <div class="block block-block" id="block-block-7">
                                <div class="blockinner">
                                    <h2 class="title">
                                        待办事宜
                                    </h2>
                                    <div class="content">
                                    </div>
                                </div>
                            </div>
                            <div class="block block-block" id="block-block-8">
                                <div class="blockinner">
                                    <h2 class="title">
                                        天气预报
                                    </h2>
                                    <div class="content">
                                        <div id="weather">
                                            <iframe id='ifm2' width='189' height='190' align='CENTER' marginwidth='0' marginheight='0'
                                                hspace='0' vspace='0' frameborder='0' scrolling='NO' src='http://weather.qq.com/inc/ss125.htm'>
                                            </iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="block block-views" id="block-views-calendar">
                                <div class="blockinner">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
