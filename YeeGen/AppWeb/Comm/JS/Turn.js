<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>支持用按键盘方向键来实现翻页跳转的JS代码-简洁JS代码</title>
</head>

<body>
<p>
  <SCRIPT language=javascript>
    document.onkeydown = chang_page;
    function chang_page() {
        if (event.keyCode == 37 || event.keyCode == 33)
         location = 'http://www.baidu.com';
        if (event.keyCode == 39 || event.keyCode == 34)
         location = 'http://www.jianjie8.com'
    }
  </SCRIPT>
说明：按键盘← →方向键 或 PageUp PageDown键直接翻页<br />
按←方向键是跳转到 www.baidu.com 按右方向键是跳转到www.jianjie8.com</p>
<p>查找更多代码，请访问：<a href="http://js.jianjie8.com" target="_blank">简洁JS代码</a></p>
</body>
</html>