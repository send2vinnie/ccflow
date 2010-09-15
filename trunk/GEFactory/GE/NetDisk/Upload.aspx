<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="GE_NetDisk_Upload" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head id="Head1" runat="server">
    <title>文件上传</title>
</head>
<body style="height: 100%; margin: 0;">
    <form id="form2" runat="server" style="height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="height: 100%;">
        <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/UPFile.xap" MinimumVersion="2.0.31005.0"  HtmlAccess ="Enabled"
            Width="650" Height="280" InitParameters="MaxFileSizeKB=,MaxUploads=2,FileFilter=,CustomParam=myPara,DefaultColor=LightBlue" />
        提示：如未安装Silverlight插件，请下载安装： <a href='Silverlight.rar' target='_blank'>Silverlight 3.0</a>
        <asp:Label ID="lbRLiang" runat="server" Width="50px" ForeColor="White" Visible="true"></asp:Label>
    </div>
    </form>
</body>
</html>
