<%@ Page Language="C#" MasterPageFile="~/GE/Template/WinOpen.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" Title="无标题页" %>

<%@ Register assembly="BP.GE" namespace="BP.GE.Ctrl" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:GEComment ID="GEComment1" runat="server" GroupKey="News">
    </cc1:GEComment>
    <p>
    </p>
    <p>
    </p>
    <cc1:GEMyView ID="GEMyView1" runat="server">
    </cc1:GEMyView>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

