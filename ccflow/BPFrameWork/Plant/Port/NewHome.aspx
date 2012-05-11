<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true" CodeFile="NewHome.aspx.cs" Inherits="Port_NewHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .main { height:600px; border:1px solid #CCC; overflow:hidden; }
        .module { float:left; width:800px; margin-left:10px; margin-top:10px; display:inline;}
        
        ul.menu { margin-top:-2px; background:url(image/left_bg.png) repeat-y; height:100%; float:left; }
        ul.menu .menu_c { margin-bottom:10px; }
        ul.menu .menu_c p { width:197px; height:36px; line-height:36px; background:url(image/menu_left.png) no-repeat; text-indent:40px; font-size:14px; font-weight:bolder; cursor:pointer; }
        ul.menu .menu_c ul li { border-top: 1px solid #488BCB; border-bottom: 1px solid #2969A6; width:195px; height:24px; line-height:24px; background:url(image/sj.png) no-repeat 40px 9px; text-indent:60px; }
        ul.menu .menu_c ul li a { color:#DEEAF3; }
        ul.menu .menu_c ul li a:hover { color:#333; font-size:14px; font-weight:bold; }
        
        .column { width: 50%; float: left;}
	    .portlet { margin: 0 1em 1em 0; }
	    .portlet-header { margin: 0.3em; padding: 4px 4px; }
	    .portlet-header .ui-icon { float: right; }
	    .portlet-content { padding: 0.4em; height:80px; overflow:hidden; }
	    .ui-sortable-placeholder { border: 1px dotted black; visibility: visible !important; height: 50px !important; }
	    .ui-sortable-placeholder * { visibility: hidden; }
        
    </style>

    <script type="text/javascript">
        $(function () {

            var listleft = $("div[id = 'column_left']");
            var listright = $("div[id = 'column_right']"); 

            $(".column").sortable({
                connectWith: ".column",
                revert: true, //缓冲效果 
                cursor: 'move', 
            });

            $(".column").bind("sortcreate", function (event, ui) {
                //alert("sortcreate");
            });
            $(".column").bind("sortstart", function (event, ui) {
                //alert("sortstart");
            });
            $(".column").bind("sort", function (event, ui) {
                //alert("sort");
            });
            $(".column").bind("sortchange", function (event, ui) {
                //alert("sortchange");
            });
            $(".column").bind("sortbeforestop", function (event, ui) {
                //alert("sortbeforestop");
            });
            $(".column").bind("sortstop", function (event, ui) {

                var new_order_left = []; //左栏布局
                var new_order_right = [];//右栏布局

                listleft.children(".portlet").each(function() { 
                    new_order_left.push(this.title); 
                }); 
                listright.children(".portlet").each(function() { 
                    new_order_right.push(this.title); 
                });

                var newleftid = new_order_left.join(','); 
                var newrightid = new_order_right.join(',');


                alert(newleftid + ":" + newrightid);
            });
            $(".column").bind("sortupdate", function (event, ui) {
                //alert("sortupdate");
            });

            $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
				.addClass("ui-widget-header ui-corner-all")
				.prepend("<span class='ui-icon ui-icon-minusthick'></span>")
				.end()
			.find(".portlet-content");

            $(".portlet-header .ui-icon").click(function () {
                $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                $(this).parents(".portlet:first").find(".portlet-content").toggle();
            });

            $(".column").disableSelection();


            var listnode = $(".column").find(".portlet");

            listleft.children(".portlet").detach();
            listright.children(".portlet").detach();
                var leftNo = list.split(':')[0].split(',');
                var rightNo = list.split(':')[1].split(',');
                alert(leftNo + rightNo);
            
                
            $.each(leftNo, function(li, lobj){
                $.each(listnode, function(i, obj){
                    if(lobj == obj.title)
                        listleft.append(obj);
                });
            });

            $.each(rightNo, function(ri, robj){
                $.each(listnode, function(i, obj){
                if(robj == obj.title)
                    listright.append(obj);
                });
            });

        });
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="main">
    <ul class="menu">
    <% 
        foreach (string key in DataSource.Keys)
        {%>
        <li class="menu_c"><p><%= key %></p>
            <ul>
                <% foreach (System.Data.DataRow row in DataSource[key])
                    {%>
                <li><a href='<%= row["SysUrl"] %>'
                    target="_blank"><%= row["SysName"] %></a> </li>
                    <% }%>
            </ul>
        <%} %>
        </li>
    </ul>

    <div class="module">
<div class="column" id="column_left">
 
	<div class="portlet" title="1">
		<div class="portlet-header">自定义模块一</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="2">
		<div class="portlet-header">自定义模块二</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="3">
		<div class="portlet-header">自定义模块三</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="4">
		<div class="portlet-header">自定义模块四</div>
		<div class="portlet-content">
        </div>
	</div>

</div>

<div class="column" id="column_right">
 
	<div class="portlet" title="5">
		<div class="portlet-header">自定义模块五</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="6">
		<div class="portlet-header">自定义模块六</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="7">
		<div class="portlet-header">自定义模块七</div>
		<div class="portlet-content">
        </div>
	</div>
    <div class="portlet" title="8">
		<div class="portlet-header">自定义模块八</div>
		<div class="portlet-content">
        </div>
	</div>
</div>
    </div>

</div>
</asp:Content>

