﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Desktop.aspx.cs" Inherits="CCOA_Desktop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="Shortcut Icon" type="image/x-icon" href="/mgt/images/ico/eim_app.ico" />
    <link rel="icon" type="image/x-icon" href="/mgt/images/ico/eim_app.ico" />
    <link href="/mgt/styles/mergedstyle.css?version=20111219" media="all" rel="Stylesheet"
        type="text/css" />
    <link href="/mgt/styles2/top/top.css?version=201112191" rel="stylesheet" type="text/css">
    <link href="https://img.jingoal.com/ig/style/main.css?version=20111219" rel="stylesheet"
        type="text/css">
    <link href="https://img.jingoal.com/ig/style/public.css?version=20111219" rel="stylesheet"
        type="text/css">
    <link href="https://img.jingoal.com/ig/style/font.css?version=20111219" rel="stylesheet"
        type="text/css">
    <link href="/mgt/styles2/petal/system/sysmnghint.css?version=20111219" rel="stylesheet"
        type="text/css">
    <script type='text/javascript' src='/mgt/scripts/merged/mergedjs.js?version=20111219'></script>
    <script type='text/javascript' src='/mgt/scripts/top/tm.js?version=20111219'></script>
    <script type='text/javascript' src='/mgt/scripts/merged/mergedjsB.js?version=20111219'></script>
    <script type='text/javascript' src='/mgt/scripts/mgt/jin-crossframe.js?version=20111219'></script>
    <script type="text/javascript" src="/mgt/dwr/interface/dwrService.js?version=20111219"></script>
    <script type='text/javascript' src="/mgt/include/resource.jsp?lang=zh_CN&version=20111219"></script>
    <script type="text/javascript">
        validatorItemHash.init();

        global_language = 'zh_CN';
        globalCp = '/mgt';

        var gVar = {
            close: "关闭",
            more: "更多",
            noright: "您无权访问此页面"
        }

        var curPageUser = {
            id: 1116685
        }
        window.addEvent("load", function () {
            tm.menu = [{ 'name': '工作台', 'url': 'h', 'domId': 'h,MGT_MA', 'pic': 'null', 'out': false }, { 'name': '考勤', 'url': '/Apps/Attendance.jsp', 'domId': 'MGT_ATTENDANCE,MGT_MY_ATTENDANCE,MGT_ATTENDANCE_STATISTICS', 'pic': 'j_ico_attend', 'out': true }, { 'name': '微博', 'url': '/WeiBo/index.jsp', 'domId': 'weibo,RA19', 'pic': 'j_ico_twitter', 'out': true }, { 'name': '主线', 'url': '/Apps/Igoal.jsp', 'domId': 'igoal,RA18', 'pic': 'j_ico_pline', 'out': true }, { 'name': '公告', 'url': 'RA9,MGT_EG_MANAGER', 'domId': 'RA9,MGT_EG_MANAGER', 'pic': 'j_ico_notice', 'out': false }, { 'name': '备忘', 'url': 'RA15', 'domId': 'RA15', 'pic': 'j_ico_memo', 'out': false }, { 'name': '邮件', 'url': 'RA8B4', 'domId': 'RA8B4', 'pic': 'j_ico_email', 'out': false }, { 'name': '文档', 'url': 'MGT_DOC', 'domId': 'MGT_DOC,RA17,RA14', 'pic': 'j_ico_docu', 'out': false }, { 'name': '沟通', 'url': 'MGT_IM_SR', 'domId': 'MGT_IM_SR,RA8B3,RA8B5,RA8B1,MGT_CLIENT_SMS,MGT_IM_MANAGER', 'pic': 'j_ico_comm', 'out': false }, { 'name': '日志', 'url': 'MGT_CLIENT_WORKLOG', 'domId': 'MGT_CLIENT_WORKLOG,RA1,MGT_CLIENT_WORKLOG_MANAGER,MGT_DAYPLAN', 'pic': 'j_ico_log', 'out': false }, { 'name': '计划', 'url': 'MGT_PLN', 'domId': 'MGT_PLN,RA7', 'pic': 'j_ico_plan', 'out': false }, { 'name': '审批', 'url': 'RA16', 'domId': 'RA16', 'pic': 'j_ico_approve', 'out': false }, { 'name': '任务', 'url': 'MGT_TSK_MY', 'domId': 'MGT_TSK_MY,MGT_TSK_T2,MGT_TSK_T1M0,MGT_TSK_T1M2,MGT_IMP,MGT_TSK_MANAGER', 'pic': 'j_ico_task', 'out': false }, { 'name': '项目', 'url': 'MGT_PROJECT_MY', 'domId': 'MGT_PROJECT_MY,RA2,MGT_PROJECT', 'pic': 'j_ico_project', 'out': false }, { 'name': '知识', 'url': 'RA6B1,MGT_KNOWLEDGE_AUDIT', 'domId': 'RA6B1,MGT_KNOWLEDGE_AUDIT', 'pic': 'j_ico_konw', 'out': false }, { 'name': '论坛', 'url': 'MGT_BBS', 'domId': 'MGT_BBS,RA5', 'pic': 'j_ico_bbs', 'out': false }, { 'name': '通讯录', 'url': 'INFO_ADDRESS', 'domId': 'INFO_ADDRESS', 'pic': 'j_ico_address', 'out': false }, { 'name': '讨论', 'url': 'MGT_TALK_AREA', 'domId': 'MGT_TALK_AREA,MGT_TLK', 'pic': 'j_ico_discuss', 'out': false }, { 'name': '关注', 'url': 'MGT_ATT', 'domId': 'MGT_ATT', 'pic': 'j_ico_attent', 'out': false }, { 'name': '工作历程', 'url': 'MGT_TRK', 'domId': 'MGT_TRK', 'pic': 'j_ico_experience', 'out': false }, { 'name': '系统提醒', 'url': '/Apps/MessageCenter.jsp', 'domId': 'SysNoti', 'pic': 'j_ico_sysremd', 'out': true }, { 'name': '友好企业', 'url': '/Apps/Enc.jsp', 'domId': 'MGT_CLIENT_NEIGHBOR,MGT_TYPE_CLIENT_INVITE,MGT_ENC', 'pic': 'j_ico_relacomp', 'out': true}];

            if (!Browser.Engine.trident4) {
                window.addEvent("resize", tm.wresize);
                window.jincrossframe.addListener(tm.getCrossDoaminContent);
            }
            tm.wresize();
            tm.showMenu();

            //prompt("location.href " + location.href );
            var u = new URI(location.href);
            var url = u.getData("url");
            var q = "";

            if (!url) {
                if (!Browser.Engine.trident4) {
                    url = "h";
                }
            } else {
                if (!Browser.Engine.trident4) {
                    tm.tab.insertHead();
                }
                q = u.get("query");
            }
            if (!Browser.Engine.trident4) {
                tm.tab.show(url, q);
            }

            //showUpdateTip();

            if (0 < 1) {
                modifyPassword(url, q);
            } else {
                if (Browser.Engine.trident4) {
                    tm.tab.show(url, q);
                }
            }
            return;
        });

        function showOnline() {
            dwrService.getCompanyName(278713, toShowOnline);
        };
        function toShowOnline(cName) {
            try {
                var url = globalCp + "/login/online/main.jsp?mgtUser=" + encodeURIComponent('278713@@王晓伟') + "&cid=2&cName=" + encodeURIComponent(cName);
                var sub = "fullscreen=2,width=500,height=400,top=" + screen.height / 3 + ",left=" + screen.width / 3 + ",toolbar=0,location=" + (common_is_ie ? 0 : 1) + ",status=1,menubar=0,resizable=0,scrollbars=0";
                window.open(url, "今目标企业工作平台", sub);
            } catch (ex) { }
        };
        /**
        *设置elem2相对于elem1的位置
        */
        function setAbsoluteLeft(elem1, elem2, width) {
            var left = 0;
            var curr = elem1;
            while (curr.offsetParent) {
                left -= curr.scrollLeft;
                curr = curr.parentNode;
            }
            while (elem1) {
                left += elem1.offsetLeft;
                elem1 = elem1.offsetParent;
            }
            elem2.style.left = (left - width) + "px";
        }



        function hidetSysMngHint() {
            dwrService.setUserProperty('sys_hint', 'true', function (m) {
                if ($('r_toolbar_tipDiv')) $('r_toolbar_tipDiv').setStyle('display', 'none');
            });
        }
        function showUpdateTip() {
            var t = 0;
            $('winpop').style.height = "0px";
            $('winpop').style.display = "block";
            var s = setInterval(function () {
                t += 1;
                $('winpop').style.height = 1.55 * t + "px";
                if (t == 100) {
                    clearInterval(s);
                }
            }, 10);
        }
        function hideUpdateTip() {
            var t = 0;
            var s = setInterval(function () {
                t -= 1;
                $('winpop').style.height = (155 + 1.55 * t) + "px";
                if (t == -100) {
                    $('winpop').style.display = "none";
                    clearInterval(s);
                }
            }, 10);
        }
        function closeUpdateTip() {
            dwrService.setUserProperty('update_tip3', 'true', function (m) {
                hideUpdateTip();
            });
        }
        function closeMyFrame() {
            $('attendanceAwokeIframe').style.height = "0px";
            $('attendanceAwokeIframe').set('height', "0px");
        }
        function openMyFrame() {
            $('attendanceAwokeIframe').style.height = "190px";
            $('attendanceAwokeIframe').set('height', "190px");
        }
        function logout() {
            var myRequest = new Request({
                url: '/mgt/login/logout.jsp',
                onComplete: function (result) {//是否是alibaba用户
                    if (result.trim() == 'false') {
                        top.location.href = "/mgt";
                    } else {
                        try {
                            window.close();
                        } catch (e) { top.location.href = "/mgt"; }
                    }
                },
                onException: function (headerName, value) {
                    alert(value);
                }
            });
            myRequest.send();
        };

        function modifyPassword(url, q) {
            var dwr = new DwrBackCall();
            if (Browser.Engine.trident4) {
                window.arg1 = url;
                window.arg2 = q;
            }
            dwr.addOneUrl("/ma/ma_pass_pop.jsp?fi=true", mgtPopupId());
            dwr.backCallFunc = function () {
                mgtPopup();
            };
            dwr.dwrProxy();
        };
	
	
    </script>
    <div class="wlbg">
        <div class="top">
            <div class="t_shortcut">
                <div class="t_menu">
                    <ul>
                        <li><a href="http://service.jingoal.com" target="_blank">帮助</a></li>
                        <li><a href="#" onclick="javascript:showOnline();">咨询</a></li>
                        <li class="bgnone"><a href="javascript:logout();">退出</a></a></li>
                    </ul>
                </div>
                <!-- 容量提示 -->
                <div id="t_space_div_click" class="t_space_box" style="cursor: hand;" onclick="tm.showAccountInfo(event);"
                    title="剩余2G">
                    <div class="t_space">
                        <a href="javascript:void(0);" class="t_long" style="width: 0%; background: #108c0a;">
                        </a>
                    </div>
                    <div class="t_space_text" style="color: #108c0a;">
                        2G
                    </div>
                </div>
                <!-- 容量提示结束 -->
                <!-- 公司容量信息 -->
                <div class="t_space_div" style="display: none; z-index: 100" id="accountInfoArea">
                    <div class="t_space_div_one">
                        <p>
                            容量：2G</p>
                        <p>
                            剩余：2G</p>
                        <p>
                            单文件限制：10M</p>
                    </div>
                    <div class="t_space_div_two">
                        可使用短信：0条
                    </div>
                    <div class="t_space_div_three">
                        <a href="javascript:void(0);" onclick="g.loadpop1('/login/cn/enlargeSpace.jsp');tm.closeAccountInfo();">
                            扩容 </a>
                    </div>
                </div>
                <!-- 公司容量信息结束 -->
            </div>
            <div class="cb">
            </div>
            <div class="t_name">
                <h1>
                    天津益港劳务有限责任公司
                </h1>
                </h1>
            </div>
            <div class="t_nav_li">
                <ul id="bigTitleId">
                </ul>
            </div>
            <div class="cb">
            </div>
            <!--更多-->
            <div class="t_pop_navli" id="t_pop_navli" style="z-index: 100; display: none">
                <div class="bgt">
                </div>
                <div class="bgm">
                    <ul id="moreTitleId">
                    </ul>
                    <div class="cb">
                    </div>
                </div>
                <div class="bgb">
                </div>
            </div>
            <div class="t_search" id="t_search_div">
                <div class="t_tab_norl">
                    <div class="t_tab_norm">
                        <div class="t_tab_norr">
                            <b class="srh_ico_nav"></b><a href="javascript:void(0)">搜索</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="t_nav" id="header">
                <div id="menu">
                </div>
            </div>
            <div class="jgcont" id="tmmain" style="height: 531px; width: 1000px; position: relative;">
                <iframe id="attendanceAwokeIframe" name="attendanceAwokeIframe" frameborder="0" scrolling="no"
                    src="/Apps/Attendance.jsp?place=AttendanceFrame&locale=zh_CN" style="width: 330px;
                    border: 0pt none; display: block; position: absolute; right: -10px; bottom: 0;
                    overflow: hidden;"></iframe>
            </div>
            <div style="display: none" class="pop_win" id="mgtAlertDivId">
</asp:Content>
