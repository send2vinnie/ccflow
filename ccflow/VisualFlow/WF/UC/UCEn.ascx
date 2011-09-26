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
</script>
