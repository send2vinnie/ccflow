<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Tax666.AppWeb.Manager.Index" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo()%>
      <script type="text/javascript">
    if(top!=self){
        top.location.href = "<%=Global.WebPath%>/Manager/default.aspx";
    }
    </script>   
</head>
<frameset rows="67,*" cols="*" frameborder="no" border="0" framespacing="0">
    <frame src="AdminTop.aspx" name="topframe" scrolling="no" noresize="noresize" id="topframe" />
    <frameset cols="204,*" frameborder="no" border="0" framespacing="0">
        <frame src="LeftMenu.aspx" name="leftframe" scrolling="no" noresize="noresize" id="leftframe" />
        <frame src="AdminMain.aspx" name="mainframe" id="mainframe" scrolling="auto" />
    </frameset>
</frameset>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
