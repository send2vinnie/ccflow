﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WorkOpt/OneWork/OneWork.master" AutoEventWireup="true" CodeFile="NDRpt.aspx.cs" Inherits="WF_WorkOpt_OneWork_NDRpt" %>
<%@ Register src="../../UC/UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function ReinitIframe(frmID, tdID) {
        try {

            var iframe = document.getElementById(frmID);
            var tdF = document.getElementById(tdID);

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <uc6:UCEn ID="UCEn1" runat="server" />
</asp:Content>

