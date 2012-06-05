﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlow.ascx.cs" Inherits="WF_UC_MyFlow" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
<script language=javascript>
    // 获取DDL值
    function ReqDDL(ddlID) {
        var v = document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_DDL_' + ddlID).value;
        if (v == null) {
            alert('没有找到ID=' + ddlID + '的下拉框控件.');
        }
        return v;
    }
    // 获取TB值
    function ReqTB(tbID) {
        var v = document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_TB_' + tbID).value;
        if (v == null) {
            alert('没有找到ID=' + tbID + '的文本框控件.');
        }
        return v;
    }
    // 获取CheckBox值
    function ReqCB(cbID) {
        var v = document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_CB_' + cbID).value;
        if (v == null) {
            alert('没有找到ID=' + cbID + '的单选控件.');
        }
        return v;
    }
    // 设置值.
    function SetCtrlVal(ctrlID, val) {
        document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_TB_' + ctrlID).value = val;
        document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_DDL_' + ctrlID).value = val;
        document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_UCEn1_CB_' + ctrlID).value = val;
    }
    //执行分支流程退回到分合流节点。
    function DoSubFlowReturn(fid, workid, fk_node) {
        var url = 'ReturnWorkSubFlowToFHL.aspx?FID=' + fid + '&WorkID=' + workid + '&FK_Node=' + fk_node;
        var v = WinShowModalDialog(url, 'df');
        window.location.href = window.history.url;
    }
    function To(url) {
        window.location.href = url;
    }
    function WinOpen(url, winName) {
        var newWindow = window.open(url, winName, 'width=700,height=400,top=100,left=300,scrollbars=yes,resizable=yes,toolbar=false,location=false,center=yes,center: yes;');
        newWindow.focus();
        return;
    }
</script>

<script language="javascript">
    function DoDelSubFlow(fk_flow, workid) {
        if (window.confirm('您确定要终止进程吗？') == false)
            return;
        var url = 'Do.aspx?DoType=DelSubFlow&FK_Flow=' + fk_flow + '&WorkID=' + workid;
        WinShowModalDialog(url, '');
        window.location.href = window.location.href; //aspxPage + '.aspx?WorkID=';
    }

    function Do(warning, url) {
        if (window.confirm(warning) == false)
            return;

        window.location.href = url;
        // WinOpen(url);
    }
 
</script>

<style type="text/css">
.Bar
{
    width:500px;
    text-align:center;
}

#tabForm, D
{
    width:960px;
    text-align:left;
    margin:0 auto;
    margin-bottom:5px;
}

#divFreeFrm {
 position:relative;
 left:25PX;
 width:960px;
}
</style>

<div id=tabForm >
<uc4:ToolBar ID="ToolBar1" runat="server" />
</div>
<div id="D" >
  <uc5:Pub ID="FlowMsg" runat="server" />
  <uc5:Pub ID="Pub1" runat="server" />
  <uc6:UCEn ID="UCEn1" runat="server" />
  <uc5:Pub ID="Pub2" runat="server" />
</div>