﻿<%@ page language="C#" masterpagefile="~/Comm/MasterPage.master" autoeventwireup="true" inherits="Comm_Batch, App_Web_03e3ojx2" title="批处理" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="UC/UCSys.ascx" TagName="UCSys" TagPrefix="uc1" %>
<%@ Register src="UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <LINK href="Table<%=BP.Web.WebUser.Style%>.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="JScript.js"></script>
		<script language="JavaScript" src="ActiveX.js"></script>
		<script language="JavaScript" src="Menu.js"></script>
    <script language="javascript">
      function ShowEn(url, wName, h, w )
        {
           var s = "dialogWidth=" + parseInt(w) + "px;dialogHeight=" + parseInt(h) + "px;resizable:yes";
           var  val=window.showModalDialog( url,null,s);
           window.location.href=window.location.href;
        }
        function selectAll() {
            var arrObj = document.all;
            if (document.forms[0].checkedAll.checked) {
                for (var i = 0; i < arrObj.length; i++) {
                    if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox') {
                        arrObj[i].checked = true;
                    }
                }
            } else {
                for (var i = 0; i < arrObj.length; i++) {
                    if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox')
                        arrObj[i].checked = false;
                }
            }
        }
　　 	function OpenAttrs(ensName)
		{
	       var url= './Sys/EnsAppCfg.aspx?EnsName='+ensName;
           var s =  'dialogWidth=680px;dialogHeight=480px;status:no;center:1;resizable:yes'.toString() ;
		   val=window.showModalDialog( url , null ,  s);
           window.location.href=window.location.href;
       }
       function DDL_mvals_OnChange(ctrl, ensName, attrKey) {

           var idx_Old = ctrl.selectedIndex;

           if (ctrl.options[ctrl.selectedIndex].value != 'mvals')
               return;
           if (attrKey == null)
               return;

           var url = 'SelectMVals.aspx?EnsName=' + ensName + '&AttrKey=' + attrKey;
           var val = window.showModalDialog(url, 'dg', 'dialogHeight: 450px; dialogWidth: 450px; center: yes; help: no');

           if (val == '' || val == null) {
               // if (idx_Old==ctrl.options.cont
               ctrl.selectedIndex = 0;
               //    ctrl.options[0].selected = true;
           }
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div align=left>
    <table id="Table1" align="left" class="Table"  width='100%'>
        <tr>
            <td class='ToolBar' >
                <uc2:ToolBar ID="ToolBar1" runat="server" />
                </td>
        </tr>
                
                <tr>
         <td align=left>
                <uc1:UCSys ID="UCSys1" runat="server" />
            </td>
        </tr>
        <tr class='TRSum'>
            <td>
                <uc1:UCSys ID="UCSys3" runat="server" />
            </td>
        </tr>
        <tr>
            <td  Class='ToolBar' >
                    <uc1:UCSys ID="UCSys2" runat="server" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
