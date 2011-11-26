<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="WF_Rpt_Search" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
		<script language="JavaScript" src="../../Comm/JScript.js"></script>
		<script language="JavaScript" src="../../Comm/ActiveX.js"></script>
		<script language="JavaScript" src="../../Comm/Menu.js"></script>
		<script language="JavaScript" src="../../Comm/ShortKey.js"></script>
		<script language="javascript">
		    function ShowEn(url, wName, h, w) {
		        h = 700;
		        w = 900;
		        var s = "dialogWidth=" + parseInt(w) + "px;dialogHeight=" + parseInt(h) + "px;resizable:yes";
		        var val = window.showModalDialog(url, null, s);
		        window.location.href = window.location.href;
		    }
		    function ImgClick() {
		    }
		    function OpenAttrs(ensName) {
		        var url = './../../Sys/EnsAppCfg.aspx?EnsName=' + ensName;
		        var s = 'dialogWidth=680px;dialogHeight=480px;status:no;center:1;resizable:yes'.toString();
		        val = window.showModalDialog(url, null, s);
		        window.location.href = window.location.href;
		    }
		    function DDL_mvals_OnChange(ctrl, ensName, attrKey) {

		        var idx_Old = ctrl.selectedIndex;

		        if (ctrl.options[ctrl.selectedIndex].value != 'mvals')
		            return;
		        if (attrKey == null)
		            return;

		        var url = '../../SelectMVals.aspx?EnsName=' + ensName + '&AttrKey=' + attrKey;
		        var val = window.showModalDialog(url, 'dg', 'dialogHeight: 450px; dialogWidth: 450px; center: yes; help: no');
		        if (val == '' || val == null) {
		            // if (idx_Old==ctrl.options.cont
		            ctrl.selectedIndex = 0;
		            //    ctrl.options[0].selected = true;
		        }
		    }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='100%' border=0>
<tr>
<td  align=left class='ToolBar'  >
    <uc2:ToolBar ID="ToolBar1" runat="server" />
    </td>
    </tr>
<tr>
<td  align=left>
    <uc3:UCSys ID="UCSys1" runat="server" />
    </td>
    </tr>
    <tr>
<td  align=left>
    <uc1:Pub ID="Pub2" runat="server" />
    </td>
    </tr>
    </table>
</asp:Content>

