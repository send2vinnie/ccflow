<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Control Language="c#" Inherits="BP.Web.Comm.UC.WF.UCEn" CodeFile="UCEn.ascx.cs"
    CodeFileBaseClass="BP.Web.UC.UCBase3" %>
<script language="javascript" type="text/javascript">
    function HidShowSta() {
        if (document.getElementById('RptTable').style.display == "none") {
            document.getElementById('RptTable').style.display = "block";
            document.getElementById('ImgUpDown').src = "../images/arrow_down.gif";
        }
        else {
            document.getElementById('ImgUpDown').src = "../images/arrow_up.gif";
            document.getElementById('RptTable').style.display = "none";
        }
    }
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
</script>
