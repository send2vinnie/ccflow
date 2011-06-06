<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlow.ascx.cs" Inherits="WF_UC_MyFlow" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
<script language=javascript>
    //执行分支流程退回到分合流节点。
    function DoSubFlowReturn(fid, workid, fk_node) {
        var url = 'ReturnWorkSubFlowToFHL.aspx?FID=' + fid + '&WorkID=' + workid + '&FK_Node=' + fk_node;
        var v = WinShowModalDialog(url, 'df');
        window.location.href = window.history.url;
    }
</script>

<script language="javascript">
    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 40; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            document.getElementById(rowIdx + '_' + i).style.display = sta;
        }
    }

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
    width:70%;
    text-align:center;
}

#divFreeFrm {
 position:relative;
 top:20px;
 left:40px;
}
    </style>
</style>
<div  style="float:left;" id=tabForm >
<uc4:ToolBar ID="ToolBar1" runat="server" />
</div>
<div id="D" >
                    <uc5:Pub ID="FlowMsg" runat="server" />
                    <uc5:Pub ID="Pub1" runat="server" />
                    <uc6:UCEn ID="UCEn1" runat="server" />
                    <uc5:Pub ID="Pub2" runat="server" />
</div>
