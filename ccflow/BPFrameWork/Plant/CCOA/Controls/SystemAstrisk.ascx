<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemAstrisk.ascx.cs"
    Inherits="CCOA_Controls_SystemAstrisk" %>
<style type="text/css">
    .style1
    {
        color: #FF6600;
    }
    .style2
    {
    }
</style>
<div style="background: #FEF3B1; height: 24px; line-height: 24px; font-size: 14px;
    font-family: 微软雅黑; padding-top: 2px; padding-bottom: 2px; padding-left: 5px;
    border: solid 1px #DAC886;">
    最新系统消息：你共有<a href="#" class="style1">234</a>个待办事项，请安排时间处理。
    <div style="float: right; margin-right:10px;">
        <a href="#" onclick="openLayout()">布局设置</a> <a href="#" onclick="saveLayout()">保存配置</a>
    </div>
</div>
