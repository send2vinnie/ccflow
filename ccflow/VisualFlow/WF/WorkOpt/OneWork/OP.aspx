﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WorkOpt/OneWork/OneWork.master" AutoEventWireup="true" CodeFile="OP.aspx.cs" Inherits="WF_WorkOpt_OneWork_OP" %>
<%@ Register src="../../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function NoSubmit(ev) {
        if (window.event.srcElement.tagName == "TEXTAREA")
            return true;

        if (ev.keyCode == 13) {
            window.event.keyCode = 9;
            ev.keyCode = 9;
            return true;
        }
        return true;
    }
    function DoFunc(doType, workid, fk_flow, fk_node) {
       // if (doType == 'Del' || doType == 'Reset') {
            if (confirm('您确定要执行吗？') == false)
                return;
        //}
        var url = 'OP.aspx?DoType=' + doType + '&WorkID=' + workid + '&FK_Flow=' + fk_flow + '&FK_Node=' + fk_node;
        window.location.href = url;
    }
    function Takeback(workid, fk_flow, fk_node,toNode) {
        if (confirm('您确定要执行吗？') == false)
            return;
        var url = '../../GetTaskSmall.aspx?DoType=Tackback&FK_Flow=' + fk_flow + '&FK_Node=' + fk_node + '&ToNode=' + toNode + '&WorkID=' + workid;
        window.location.href = url;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub2" runat="server" />
</asp:Content>

