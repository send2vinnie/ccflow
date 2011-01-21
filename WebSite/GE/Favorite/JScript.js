function Save(WinUrl, RefUrl, Title) {
    var url = WinUrl + "?url=" + RefUrl + "&title=" + Title;
    window.showModalDialog(url, window, "dialogWidth=400px;dialogHeight=220px;status=no;scroll=no;");
}
function PromptInfo() {
    alert("对不起请先登录!");
}