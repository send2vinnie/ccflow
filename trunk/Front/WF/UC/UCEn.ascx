<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Control Language="c#" Inherits="BP.Web.Comm.UC.UCEn" CodeFile="UCEn.ascx.cs" CodeFileBaseClass="BP.Web.UC.UCBase" %>
<script language="javascript" type="text/javascript" >
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

var isInser = "";
function ReinitIframe(dtlid) {

    try {
        var iframe = document.getElementById("F" + dtlid);
        var tdF = document.getElementById("TD" + dtlid);
        iframe.height = iframe.contentWindow.document.body.scrollHeight;
        iframe.width = iframe.contentWindow.document.body.scrollWidth;

        if (tdF.width < iframe.width) {
            //alert(tdF.width +'  ' + iframe.width);
            tdF.width = iframe.width;
        } else {
            iframe.width = tdF.width;

        }

        tdF.height = iframe.height;
        return;
    } catch (ex) {
        return;
    }
    return;
}
</script>