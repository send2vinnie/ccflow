<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Article_Single.ascx.cs"
    Inherits="CCOA_Controls_Article_Single" %>
<link href="../Style/control.css" rel="stylesheet" type="text/css" />
<script type="text/C#" runat="server">
   
    string MetaData;
    public bool ShowTools;
</script>
<div class="Article_Single">
    <div class="article mtop10">
        <div class="article_content">
            <h1>
                <%=ThisArticle.Title%>
            </h1>
            <!--网站内容开始-->
            <div id="articleContnet">
                <%=ThisArticle.Content%>
            </div>
            <!--网站内容结束-->
        </div>
    </div>
</div>
<script type="text/javascript" language="javascript">

    // $('#articleContnet img').imageResize();


    //双击鼠标滚动屏幕的代码
    var currentpos, timer;
    function initialize() {
        timer = setInterval("scrollwindow ()", 30);
    }
    function sc() {
        clearInterval(timer);
    }
    function scrollwindow() {
        currentpos = document.body.scrollTop;
        window.scroll(0, ++currentpos);
        if (currentpos != document.body.scrollTop)
            sc();
    }
    document.onmousedown = sc
    document.ondblclick = initialize

    //更改字体大小
    var status0 = '';
    var curfontsize = 10;
    var curlineheight = 18;
    function fontZoomA() {
        if (curfontsize > 8) {
            document.getElementById('fontzoom').style.fontSize = (--curfontsize) + 'pt';
            document.getElementById('fontzoom').style.lineHeight = (--curlineheight) + 'pt';
        }
    }
    function fontZoomB() {
        if (curfontsize < 64) {
            document.getElementById('fontzoom').style.fontSize = (++curfontsize) + 'pt';
            document.getElementById('fontzoom').style.lineHeight = (++curlineheight) + 'pt';
        }
    }
</script>
