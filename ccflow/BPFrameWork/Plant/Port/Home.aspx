<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Port_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="Style/master.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/jquery.js" type="text/javascript"></script>
    <script src="Javascript/drupal.js" type="text/javascript"></script>
    <script src="Javascript/jstools.js" type="text/javascript"></script>
    <script src="Javascript/collapsiblock.js" type="text/javascript"></script>
    <script src="Javascript/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">        Drupal.extend({ settings: { "jstools": { "cleanurls": false, "basePath": "/portal/" }, "collapsiblock": { "block-block-1": "2", "block-block-2": "2", "block-block-3": "2", "block-block-4": "2", "block-block-5": "2", "block-block-6": "2", "block-user-3": "2", "block-block-7": "2", "block-block-8": "2", "block-views-calendar": "2"}} });</script>

    <style type="text/css">
        * { font-family: "微软雅黑" }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="page" class="one-sidebar two-sidebars">
        <div id="header">
            <div id="logo-title">
                <h1 id='site-name'>
                    <a href="javascript:void(0)">单点登录系统 </a>
                </h1>
                <div id='site-slogan'>
                    让登录更简单
                </div>
            </div>
            <div class="menu withprimary ">
                <div id="primary" class="clear-block">
                    <ul class="links-menu">
                        <li><a href="#" title="退出">退出</a></li>
                        <li><a href="#" title="修改配置" target="_blank">
                            修改配置</a></li>
                        <li><a href="#" title="修改口令"
                            target="_blank">修改口令</a></li>
                        <li class="active"><a href="#" title="主页" class="active">主页</a></li>
                    </ul>
                </div>
            </div>
        </div>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://sports.sina.com.cn/n/2012-04-10/08346016419.shtml"
                                                        target="_blank">国少邀请赛0比3不敌日本国少 4...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://sports.sina.com.cn/j/2012-04-10/08286016417.shtml"
                                                        target="_blank">阿尔贝茨敬告巴里奥斯入恒大:风险...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://sports.sina.com.cn/o/2012-04-10/08246016416.shtml"
                                                        target="_blank">游泳奥运选拔赛中日PK 中国17...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://sports.sina.com.cn/o/2012-04-10/07116016369.shtml"
                                                        target="_blank">湖南羽协:不会让黄穗退还三年工资...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://sports.sina.com.cn/j/2012-04-10/06556016363.shtml"
                                                        target="_blank">谈蒂加纳帅位朱骏懒得做调整 申花...</a> </li>
                                                </ul>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://news.sina.com.cn/c/2012-04-09/145024242272.shtml"
                                                        target="_blank">音著协回应著作权法草案争议：防唱...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://news.sina.com.cn/c/2012-04-09/092124241201.shtml"
                                                        target="_blank">台湾花莲东部外海今晨发生5.5级...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://news.sina.com.cn/c/2012-04-09/085924241057.shtml"
                                                        target="_blank">马英九访问非洲途中过境印度引发关...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://news.sina.com.cn/c/2012-04-09/084824241030.shtml"
                                                        target="_blank">陈菊重申不参选民进党下届党主席...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://news.sina.com.cn/c/2012-04-09/083124240857.shtml"
                                                        target="_blank">高雄一家化工厂昨晚发生爆炸...</a> </li>
                                                </ul>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://ent.sina.com.cn/s/h/2012-04-10/08153600959.shtml"
                                                        target="_blank">苗侨伟自曝不懂投资 未听说子女想...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://ent.sina.com.cn/v/m/2012-04-10/08133600958.shtml"
                                                        target="_blank">马浚伟首演民初剧 期待金像奖设计...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://ent.sina.com.cn/s/h/2012-04-10/08043600953.shtml"
                                                        target="_blank">洪金宝爱驹初战夺冠 为其赢得30...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://ent.sina.com.cn/s/h/2012-04-10/07583600952.shtml"
                                                        target="_blank">周志文拒认袁圣殷是女友 马赛否认...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://ent.sina.com.cn/v/j/2012-04-10/07453600940.shtml"
                                                        target="_blank">具惠善甜美造势新剧 汪东城称收视...</a> </li>
                                                </ul>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://tech.sina.com.cn/i/2012-04-10/09036933298.shtml"
                                                        target="_blank">消息称暴风影音申请国内创业板上市...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://tech.sina.com.cn/t/2012-04-10/08376933014.shtml"
                                                        target="_blank">工信部：正制定管理办法应对恶意扣...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://tech.sina.com.cn/i/2012-04-10/08316933000.shtml"
                                                        target="_blank">Instagram以10亿美元出...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://tech.sina.com.cn/i/2012-04-10/08266932982.shtml"
                                                        target="_blank">暴风影音申请创业板上市 称不参与...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://tech.sina.com.cn/d/2012-04-10/08236932966.shtml"
                                                        target="_blank">火星地下发现火山隧道或存在生命(...</a> </li>
                                                </ul>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://blog.sina.com.cn/s/blog_719bee0e0100y7we.html"
                                                        target="_blank">多盈财经：缩量小阴透露主力意图...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://finance.sina.com.cn/stock/jsy/20120409/180311779027.shtml"
                                                        target="_blank">申银万国：中线难有大牛市...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://finance.sina.com.cn/stock/jsy/20120409/175511778967.shtml"
                                                        target="_blank">股商财富报告：反弹不会就此结束...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://weibo.com/1640239893/ydPppc0cJ"
                                                        target="_blank">盘面上看5日均线支撑较为强劲...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://video.sina.com.cn/p/finance/stock/jsy/20120409/173261716531.html"
                                                        target="_blank">视频：大盘回落盘整 揭秘压力支撑...</a> </li>
                                                </ul>
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
                                                <ul>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://edu.sina.com.cn/official/2012-04-10/0903333774.shtml"
                                                        target="_blank">骗子伪造红头文件勒索公考生 考前...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://edu.sina.com.cn/zxx/2012-04-10/0851333771.shtml"
                                                        target="_blank">校长诉苦：一件“黄衣服”卡住校车...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://edu.sina.com.cn/zxx/2012-04-10/0835333769.shtml"
                                                        target="_blank">北京一家民办学校陷停课风波 数位...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://edu.sina.com.cn/official/2012-04-09/1628333750.shtml"
                                                        target="_blank">代考费用日翻新高 公务员枪手高达...</a> </li>
                                                    <li><a href="http://go.rss.sina.com.cn/redirect.php?url=http://edu.sina.com.cn/gaokao/2012-04-09/1544333724.shtml"
                                                        target="_blank">2012年4月调查：如何上网收集...</a> </li>
                                                </ul>
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
                                <div class="panel-custom">
                                    <h2 class="title">
                                        雅虎股市行情及汇率</h2>
                                    <table width="500">
                                        <tr>
                                            <td>
                                                <iframe src='http://yahoo.compass.cn/stock/modblock.php' width='350' height='236'
                                                    scrolling='no' frameborder='0'></iframe>
                                            </td>
                                            <td>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br class="panel-clearer" />
                </div>
            </div>
            <div id="sidebar-left" class="sidebar">
                <div class="block block-block" id="block-block-1">
                    <div class="blockinner">
                        <h2 class="title">
                            免费邮箱
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/mail.sina.com.cn.php?sso_host=mail.sina.com.cn"
                                    target="_blank">新浪免费邮箱</a> </li>
                                <li><a href="http://portal.ssodemo.com/mail.sohu.com.cn.php?sso_host=mail.sohu.com.cn"
                                    target="_blank">搜狐免费邮箱</a> </li>
                                <li><a href="http://portal.ssodemo.com/cn.mail.yahoo.com.php?sso_host=cn.mail.yahoo.com"
                                    target="_blank">雅虎免费邮箱</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="block block-block" id="block-block-2">
                    <div class="blockinner">
                        <h2 class="title">
                            论坛社区
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/bbs.chinabroadcast.cn.php?sso_host=bbs.chinabroadcast.cn"
                                    target="_blank">国际在线论坛</a> </li>
                                <li><a href="http://portal.ssodemo.com/pop.pcpop.com.php?sso_host=pop.pcpop.com"
                                    target="_blank">泡泡俱乐部</a> </li>
                                <li><a href="http://portal.ssodemo.com/club.autohome.com.cn.php?sso_host=club.autohome.com.cn"
                                    target="_blank">汽车之家</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="block block-block" id="block-block-4">
                    <div class="blockinner">
                        <h2 class="title">
                            商城
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/www.joyo.com.php?sso_host=www.joyo.com" target="_blank">
                                    卓越网</a> </li>
                                <li><a href="http://portal.ssodemo.com/www.taobao.com.php?sso_host=www.taobao.com"
                                    target="_blank">淘宝网</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="block block-block" id="block-block-6">
                    <div class="blockinner">
                        <h2 class="title">
                            科教
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/matrix.hongen.com.php?sso_host=matrix.hongen.com"
                                    target="_blank">洪恩在线</a> </li>
                                <li><a href="http://portal.ssodemo.com/www.china-pub.com.php?sso_host=www.china-pub.com"
                                    target="_blank">博客中国</a> </li>
                                <li><a href="http://portal.ssodemo.com/pcbbs.pconline.com.cn.php?sso_host=pcbbs.pconline.com.cn"
                                    target="_blank">太平洋电脑网</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="block block-block" id="block-block-5">
                    <div class="blockinner">
                        <h2 class="title">
                            游戏
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/www.egchina.com.php?sso_host=www.egchina.com"
                                    target="_blank">电玩中国</a> </li>
                                <li><a href="http://portal.ssodemo.com/bbs.52pk.net.php?sso_host=bbs.52pk.net" target="_blank">
                                    52pk游戏论坛</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="sidebar-right" class="sidebar">
                <div class="block block-block" id="block-block-7">
                    <div class="blockinner">
                        <h2 class="title">
                            待办事宜
                        </h2>
                        <div class="content">
                            <ul>
                                <li><a href="http://portal.ssodemo.com/mail.sina.com.cn.php?sso_host=mail.sina.com.cn"
                                    target="_blank">新浪邮件（2）</a> </li>
                                <li><a href="http://portal.ssodemo.com/mail.sohu.com.cn.php?sso_host=mail.sohu.com.cn"
                                    target="_blank">搜狐邮件（4）</a> </li>
                                <li><a href="http://portal.ssodemo.com/cn.mail.yahoo.com.php?sso_host=cn.mail.yahoo.com"
                                    target="_blank">雅虎邮件（3）</a> </li>
                            </ul>
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
                        <h2 class="title">
                            日历
                        </h2>
                        <div class="content">
                            <div class='view view-calendar'>
                                <div class='view-empty view-empty-calendar'>
                                    <div class="calendar-calendar">
                                        <div class="month-view">
                                            <table class="mini">
                                                <thead>
                                                    <tr>
                                                        <th class="heading" colspan="7">
                                                            <a href="/portal/index.php?q=calendar/2012/04">Apr 2012</a>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr class="odd">
                                                        <td class="days mon">
                                                            一
                                                        </td>
                                                        <td class="days tue">
                                                            二
                                                        </td>
                                                        <td class="days wed">
                                                            三
                                                        </td>
                                                        <td class="days thu">
                                                            四
                                                        </td>
                                                        <td class="days fri">
                                                            五
                                                        </td>
                                                        <td class="days sat">
                                                            六
                                                        </td>
                                                        <td class="days sun">
                                                            日
                                                        </td>
                                                    </tr>
                                                    <tr class="even">
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="四月 sun mini" id="四月1">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/1">1</a></div>
                                                        </td>
                                                    </tr>
                                                    <tr class="odd">
                                                        <td class="四月 mon mini" id="四月2">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/2">2</a></div>
                                                        </td>
                                                        <td class="四月 tue mini" id="四月3">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/3">3</a></div>
                                                        </td>
                                                        <td class="四月 wed mini" id="四月4">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/4">4</a></div>
                                                        </td>
                                                        <td class="四月 thu mini" id="四月5">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/5">5</a></div>
                                                        </td>
                                                        <td class="四月 fri mini" id="四月6">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/6">6</a></div>
                                                        </td>
                                                        <td class="四月 sat mini" id="四月7">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/7">7</a></div>
                                                        </td>
                                                        <td class="四月 sun mini" id="四月8">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/8">8</a></div>
                                                        </td>
                                                    </tr>
                                                    <tr class="even">
                                                        <td class="四月 mon mini" id="四月9">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/9">9</a></div>
                                                        </td>
                                                        <td class="四月 tue today mini" id="四月10">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/10">10</a></div>
                                                        </td>
                                                        <td class="四月 wed mini" id="四月11">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/11">11</a></div>
                                                        </td>
                                                        <td class="四月 thu mini" id="四月12">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/12">12</a></div>
                                                        </td>
                                                        <td class="四月 fri mini" id="四月13">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/13">13</a></div>
                                                        </td>
                                                        <td class="四月 sat mini" id="四月14">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/14">14</a></div>
                                                        </td>
                                                        <td class="四月 sun mini" id="四月15">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/15">15</a></div>
                                                        </td>
                                                    </tr>
                                                    <tr class="odd">
                                                        <td class="四月 mon mini" id="四月16">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/16">16</a></div>
                                                        </td>
                                                        <td class="四月 tue mini" id="四月17">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/17">17</a></div>
                                                        </td>
                                                        <td class="四月 wed mini" id="四月18">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/18">18</a></div>
                                                        </td>
                                                        <td class="四月 thu mini" id="四月19">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/19">19</a></div>
                                                        </td>
                                                        <td class="四月 fri mini" id="四月20">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/20">20</a></div>
                                                        </td>
                                                        <td class="四月 sat mini" id="四月21">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/21">21</a></div>
                                                        </td>
                                                        <td class="四月 sun mini" id="四月22">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/22">22</a></div>
                                                        </td>
                                                    </tr>
                                                    <tr class="even">
                                                        <td class="四月 mon mini" id="四月23">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/23">23</a></div>
                                                        </td>
                                                        <td class="四月 tue mini" id="四月24">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/24">24</a></div>
                                                        </td>
                                                        <td class="四月 wed mini" id="四月25">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/25">25</a></div>
                                                        </td>
                                                        <td class="四月 thu mini" id="四月26">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/26">26</a></div>
                                                        </td>
                                                        <td class="四月 fri mini" id="四月27">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/27">27</a></div>
                                                        </td>
                                                        <td class="四月 sat mini" id="四月28">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/28">28</a></div>
                                                        </td>
                                                        <td class="四月 sun mini" id="四月29">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/29">29</a></div>
                                                        </td>
                                                    </tr>
                                                    <tr class="odd">
                                                        <td class="四月 mon mini" id="四月30">
                                                            <div class="mini-day-off">
                                                                <a href="/portal/index.php?q=calendar/2012/04/30">30</a></div>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="footer">
            &#169; 2006-2007 Hewlett-Packard Development Company, L.P.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系电话8610-82783473、8610-82783469
        </div>
    </div>
</asp:Content>

