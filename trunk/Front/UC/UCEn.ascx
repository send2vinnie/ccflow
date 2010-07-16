<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Control Language="c#" Inherits="BP.Web.Comm.UC.UCEn" CodeFile="UCEn.ascx.cs" CodeFileBaseClass="BP.Web.UC.UCBase" %>
<script language="javascript">
function HidShowSta() {
if(document.getElementById('RptTable').style.display == "none")
{
  document.getElementById('RptTable').style.display = "block";
  document.getElementById('ImgUpDown').src ="../images/arrow_down.gif";
}
else 
{ 
   document.getElementById('ImgUpDown').src ="../images/arrow_up.gif";
   document.getElementById('RptTable').style.display = "none";
 }
}
function GroupBarClick( Field )
{
       alert( Field);
       var i=0
       for (i=0;i<=10;i++)
       {
       
          if (document.getElementById( Field + i )==null)
                   continue;
                   
         if (document.getElementById( Field + i ).style.display = "block" )
            document.getElementById( Field + i ).style.display = "display"; 
         else      
            document.getElementById( Field + i ).style.display = "block" ;
      }
}    
</script>