<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChannelTree.ascx.cs" Inherits="CCOA_Admin_ChannelTree" %>
<script src="../../Comm/JS/jquery-1.6.min.js" type="text/javascript"></script>
<script src="../../Comm/JS/jquery.easyui.min.js" type="text/javascript"></script>
<link href="../../Comm/JS/themes/default/easyui.css" rel="stylesheet" type="text/css" />
<link href="../../Comm/JS/themes/icon.css" rel="stylesheet" type="text/css" />
<link href="../Style/demo.css" rel="stylesheet" type="text/css" />
<div class="ChannelTree">
    <ul id="tt1" class="easyui-tree" animate="true" dnd="true">
        <li><span>新闻栏目</span>
            <% foreach (BP.CCOA.Channel item in NewsChannels)
               {%>
            <ul>
                <li>
                    <%= item.Name %></li>
            </ul>
            <% } %>
        </li>
         <li><span>公告栏目</span>
            <% foreach (BP.CCOA.Channel item in NoticeChannels)
               {%>
            <ul>
                <li>
                    <%= item.Name %></li>
            </ul>
            <% } %>
        </li>
    </ul>
</div>
