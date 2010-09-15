<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
   <head>
		<title>主页</title>
		<meta http-equiv="Content-Language" content="zh-cn">
		<script language="javascript">
		function toHome()
		{
                  window.location.href='./Port/Signin.aspx';
                   return;
		}
		</script>
	</head>
	<body onload="toHome()">
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
