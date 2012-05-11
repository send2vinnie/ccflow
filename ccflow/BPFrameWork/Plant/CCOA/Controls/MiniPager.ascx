<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniPager.ascx.cs" Inherits="XControls_MiniPager" %>
<div class="mini-pager" style="background: #ccc;" totalcount="123"
    onpagechanged="onPageChanged" sizelist="[5,10,20,100]" showtotalcount="true">
</div>
<script type="text/javascript">
    function onPageChanged(e) {
        alert(e.pageIndex + ":" + e.pageSize);
    }       
</script>
