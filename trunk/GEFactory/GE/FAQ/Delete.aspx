<%@ Page Title="" Language="C#" MasterPageFile="~/Style/MasterPage.master" AutoEventWireup="true"
    CodeFile="Delete.aspx.cs" Inherits="FAQ_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <base target="_self" />

    <script type="text/javascript" language="javascript">
    
    function Esc()
    {
        if (event.keyCode == 27)     
            window.close();
        return true;
    }
    </script>

    <link href="../Style/Glo.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Part.css" rel="stylesheet" type="text/css" />
    <div style="border: 1px; margin-top: 10%; margin-left: 15%;">
        <asp:Panel ID="Panel1" runat="server">
            <div class="messageBox clearfix">
                <div class="inner_01">
                    <dl class="dl_170 mg20b">
                        <dt>
                            <asp:RadioButton ID="RbtnCon" runat="server" GroupName="a" Text="存在反面信息，删除！" />
                            <dd>
                                * 选择该项，该资源上传者将被扣除<em class="iconP">8</em></dd>
                            <dt>
                                <asp:RadioButton ID="RbtnFalse" runat="server" GroupName="a" Text="提出无聊问题，删除！" />
                                <dd>
                                    * 选择该项，该资源上传者将被扣除<em class="iconP">5</em></dd>
                                <dt>
                                    <asp:RadioButton ID="RbtnHave" runat="server" GroupName="a" Text="存在类似问题，删除！" />
                                    <dd>
                                        * 选择该项，该资源上传者将被扣除<em class="iconP">1</em></dd>
                    </dl>
                    <p class="txtRight mg20b">
                        <asp:Button ID="BtnDelAdmin" runat="server" OnClick="BtnDelAdmin_Click" Text="删除" />
                    </p>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <div class="messageBox clearfix">
                <div class="inner_01">
                    <dl class="dl_170 mg20b">
                        <dt>
                            <asp:RadioButton ID="RbtnCon1" runat="server" GroupName="a" Text="存在反面信息，删除！" />
                            <dd>
                                * 选择该项，该资源上传者将被扣除<em class="iconP">8</em></dd>
                            <dt>
                                <asp:RadioButton ID="RbtnFalse1" runat="server" GroupName="a" Text="存在虚假信息，删除！" />
                                <dd>
                                    * 选择该项，该资源上传者将被扣除<em class="iconP">5</em></dd>
                                <dt>
                                    <asp:RadioButton ID="RbtnHave1" runat="server" GroupName="a" Text="存在类似信息，删除！" />
                                    <dd>
                                        * 选择该项，该资源上传者将被扣除<em class="iconP">1</em></dd>
                    </dl>
                    <p class="txtRight mg20b">
                        <asp:Button ID="BtnDelAdmin1" runat="server" Text="删除" OnClick="BtnDelAdmin1_Click" />
                    </p>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
