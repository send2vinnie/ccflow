<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTabs.ascx.cs" Inherits="Comm_GE_Info_InfoTabs" %>
<%@ OutputCache Duration="300" VaryByParam="none" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<uc1:Pub ID="PubCSS" runat="server" />
<cc1:GeTab ID="GeTab1" runat="server" MouseAction="onclick">
</cc1:GeTab>