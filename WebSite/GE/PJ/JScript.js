function GetVal(hid) {
     var selectedIndex = -1;
    var Num;
    var Radios = document.getElementsByName("RdPJ");
    for (i = 0; i < Radios.length; i++) {
        if (Radios[i].checked) {
            selectedIndex = i;
            Num = Radios[i].value;
            break;
        }
    }
    if (selectedIndex < 0) {
        alert("您没有选择任何项");
    }
    else {
        document.getElementById(hid).value = Num;
    }
}
function btnSubmit_Click() {
    var selectedIndex = -1;
    var Num;
    var Radios = document.getElementsByName("RdPJ");
    for (i = 0; i < Radios.length; i++) {
        if (Radios[i].checked) {
            selectedIndex = i;
            Num = Radios[i].value;
            break;
        }
    }
    if (selectedIndex < 0) {
        alert("您没有选择任何项");
        return false;
    }
    else {
        return true;
    }
}