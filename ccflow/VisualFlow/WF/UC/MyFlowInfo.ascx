<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowInfo.ascx.cs" Inherits="WF_UC_MyFlowInfo" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="MyFlowInfoWap.ascx" tagname="MyFlowInfoWap" tagprefix="uc3" %>
<%--<script>
 if (window.opener && !window.opener.closed) {
        window.opener.location.href = window.opener.location.href;
        window.opener.top.leftFrame.location.href = window.opener.top.leftFrame.location.href;
    }
</script>--%>
<br>
<div align="center">
<span >
    <uc4:ToolBar ID="ToolBar1" runat="server"  />
      <uc3:MyFlowInfoWap ID="MyFlowInfoWap1" runat="server" />
      </span>
</div>
