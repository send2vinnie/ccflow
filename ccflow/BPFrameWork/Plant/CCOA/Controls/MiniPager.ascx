<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniPager.ascx.cs" Inherits="XControls_MiniPager" %>
<div id="minipager" class="mini-pager" style="background: #ccc;" totalcount="123" onpagechanged="onPageChanged"
    sizelist="[5,10,20,100]" showtotalcount="true">
</div>
<script type="text/javascript">
    function onPageChanged(e) {
        //alert(e.pageIndex + ":" + e.pageSize);
        //window.location.href = "list.aspx?pageIndex=" + e.pageIndex + "&pageSize=" + e.pageSize;
        //pager.update(index, size, total);
    }

    function QueryString() {
        var name, value, i;
        var str = location.href;
        var num = str.indexOf("?")
        str = str.substr(num + 1);
        var arrtmp = str.split("&");
        for (i = 0; i < arrtmp.length; i++) {
            num = arrtmp[i].indexOf("=");
            if (num > 0) {
                name = arrtmp[i].substring(0, num);
                value = arrtmp[i].substr(num + 1);
                this[name] = value;
            }
        }
    }

    $(function () {
        var Request = new QueryString();

        var index = Request["pageIndex"]
        var size = Request["pageSize"]
        var total = 20;

        alert(index+":"+size);

        var pager = mini.get("minipager");
        pager.update(index, size, total);
    });
     
</script>
