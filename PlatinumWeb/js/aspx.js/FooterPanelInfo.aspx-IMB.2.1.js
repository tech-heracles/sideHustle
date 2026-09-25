/*
Function: ShfaqPeriudhen
Hap lupen e priudhave.
*/

function lostFocusPeriudha(vlera) {
}


////            window.parent.window["footerPeriudha"] = btnPeriudha;
function ShfaqPeriudhen() {
    var parentWindow = window.parent;
    parentWindow.document.getElementById('Container').src = 'LupaPeriudhaKontabel.aspx';
    parentWindow.popupUniversal.Show();
}

$(document).ready(function () {//po
    window.parent.document.getElementById("hfPeriudha").value = btnPeriudha.GetText();    
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
});