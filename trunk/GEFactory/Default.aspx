<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="GE/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="./Comm/Table2.css" rel="stylesheet" type="text/css" />
    <style>
        body 
        {
        	line-height:20px;
        	color:#666;
        	font-family:宋体;
        }
    .demo,.tab_work
    {
    	margin-bottom:5px;
    }
    .yj_style td 
    { 
        display:block;
        height:5px;
        }
   
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="demo">
    <uc1:Pub ID="Pub1" runat="server" />
    </div>
    
    
    <br>
    <br>
    
    
    <div class="demo">
    <uc1:Pub ID="Pub2" runat="server" />
    </div>
    <br/>
    <br/>
    
    <div class="demo">
    <uc1:Pub ID="Pub3" runat="server" />
    </div>
        <br/>
    <br/>
    
    
    <div class="demo">
    <uc1:Pub ID="Pub4" runat="server" />
    </div>
    
        <br/>
    <br/>
    
     <div class="demo">
    <uc1:Pub ID="Pub5" runat="server" />
    </div>
    </form>
</body>
</html>
