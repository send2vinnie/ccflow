<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="Frm.aspx.cs" Inherits="WF_Frm" %>
<%@ Register src="UC/UCEn.ascx" tagname="UCEn" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" >
    var isChange = false;
    function SaveDtlData() {
        if (isChange == false)
            return;
        var btn = document.getElementById('Button1');
        btn.click();
        isChange = false;
    }
    function TROver(ctrl) {
        ctrl.style.backgroundColor = 'LightSteelBlue';
    }

    function TROut(ctrl) {
        ctrl.style.backgroundColor = 'white';
    }
    function Del(id, ens, refPk, pageIdx) {
        if (window.confirm('您确定要执行删除吗？') == false)
            return;

        var url = 'Do.aspx?DoType=DelDtl&OID=' + id + '&EnsName=' + ens;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = 'Dtl.aspx?EnsName=' + ens + '&RefPKVal=' + refPk + '&PageIdx=' + pageIdx;
    }

    function DtlOpt(workId, fk_mapdtl) {
        var url = 'DtlOpt.aspx?WorkID=' + workId + '&FK_MapDtl=' + fk_mapdtl;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = 'Dtl.aspx?EnsName=' + fk_mapdtl + '&RefPKVal=' + workId;
    }
    </script>
    <style type="text/css">
        .HBtn
        {
        	/* display:none; */
        	visibility:visible;
        }
    </style>
	<script language="JavaScript" src="./../Comm/JScript.js"></script>
   <script language="JavaScript" src="./../Comm/JS/Calendar/WdatePicker.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Button ID="Button1" runat="server" Text="Save"  CssClass="HBtn" Visible=true onclick="Button1_Click" />
    <uc1:UCEn ID="UCEn1" runat="server" />
</asp:Content>

