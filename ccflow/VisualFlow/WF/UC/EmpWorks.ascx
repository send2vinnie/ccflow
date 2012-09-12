<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpWorks.ascx.cs" Inherits="WF_UC_EmpWorks" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
    <div style="width:100%;" >
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

        function WinOpenIt(url) {
            var newWindow = window.open(url, 'newwindow', 'height=600,width=850,top=50,left=50,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
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
        .ImgPRI
        {
            width:20px;
            height:20px;
            border:0px;
        }
    </style>
