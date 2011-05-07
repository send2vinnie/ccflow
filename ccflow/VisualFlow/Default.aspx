<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <base target=_blank />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 200px">
                </td>
                <td valign=top>
        <uc1:ucsys ID="Ucsys1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
