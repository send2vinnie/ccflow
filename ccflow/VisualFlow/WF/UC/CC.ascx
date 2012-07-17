<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CC.ascx.cs" Inherits="WF_UC_CC" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<div style="width:100%">
    <uc1:Pub ID="Pub1" runat="server" />
    </div>
    <script   type="text/javascript">
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
            for (i = 0; i <= 5000; i++) {
                if (document.getElementById(rowIdx + '_' + i) == null)
                    continue;
                document.getElementById(rowIdx + '_' + i).style.display = sta;
            }
        }
        function DoDelCC(mypk) {
            var url = 'Do.aspx?DoType=DelCC&MyPK=' + mypk;
            var v = window.showModalDialog(url, 'sd', 'dialogHeight: 10px; dialogWidth: 10px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
        }
        function WinOpen(ccid, fk_flow, fk_node, workid, fid, sta) {
            var url = '';
            if (sta == '0') {
                url = 'Do.aspx?DoType=DoOpenCC&FK_Flow=' + fk_flow + '&FK_Node=' + fk_node + '&WorkID=' + workid + '&FID=' + fid + '&Sta=' + sta + '&MyPK=' + ccid;
            }
            else {
                url = './WorkOpt/OneWork/Track.aspx?FK_Flow=' + fk_flow + '&FK_Node=' + fk_node + '&WorkID=' + workid + '&FID=' + fid + '&Sta=' + sta + '&MyPK=' + ccid;
            }
            var newWindow = window.open(url, 'z', 'help:1;resizable:1;dialogWidth:680px;dialogHeight:420px');
            newWindow.focus();
            return;
        }
    </script>
        <style>
        .TTD
        {
          word-wrap: break-word; 
      　　word-break: normal; 
        }
    </style>