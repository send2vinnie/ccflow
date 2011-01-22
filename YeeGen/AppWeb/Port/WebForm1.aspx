<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BP.YG.WebUI.Port.WebForm1" %>

<%@ Register src="UC/RegUser.ascx" tagname="RegUser" tagprefix="uc1" %>
<%@ Register src="UC/RequestPass.ascx" tagname="RequestPass" tagprefix="uc2" %>

<%@ Register src="UC/PerInfo.ascx" tagname="PerInfo" tagprefix="uc3" %>
<%@ Register src="UC/Login.ascx" tagname="Login" tagprefix="uc4" %>

<%@ Register src="UC/MyCent.ascx" tagname="MyCent" tagprefix="uc5" %>
<%@ Register src="UC/ChangePass.ascx" tagname="ChangePass" tagprefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <table width='100%' border=0 >
    <tr>
    <td>
        <uc2:RequestPass ID="RequestPass1" runat="server" />
        </td>
    </tr>
    
      <tr>
    <td>
          <uc3:PerInfo ID="PerInfo1" runat="server" />
          </td>
    </tr>
    
     <td>
         <uc4:Login ID="Login1" runat="server" />
        </td>
    </tr>
        <tr>
    
     <td>
         <uc1:RegUser ID="RegUser1" runat="server" />
          </td>
    </tr>
        <tr>
    
     <td>
         <uc5:MyCent ID="MyCent1" runat="server" />
          </td>
    </tr>
        <tr>
    
     <td>
         <uc6:ChangePass ID="ChangePass1" runat="server" />
          </td>
    </tr>
    </table>
    </form>
</body>
</html>
