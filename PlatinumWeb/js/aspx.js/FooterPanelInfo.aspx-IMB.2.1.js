/*
Function: ShfaqPeriudhen
Hap lupen e priudhave.
*/

function lostFocusPeriudha(vlera) {
    //            if (vlera != '') {
    //                callBackPanel.PerformCallback('skeme,' + vlera);
    //            }
}

//        function valueChangedPeriudha() {
//            var vleraLabel = lblPeriudhaAktuale.GetText();
//            var periudha = vleraLabel.split("-");
//            var dataDok = new Date();
//            dataDok = formatDate(dataDok, "dd/MM/yyyy");

//            periudha1 = periudha[0].split("/");
//            periudha2 = periudha[1].split("/");
//            dtDokumentit = dataDok.split("/");
//        }
//        function btnPeriudha_Init(s, e) {
////            window.parent.window["footerPeriudha"] = btnPeriudha;
//        }
function ShfaqPeriudhen() {
    var parentWindow = window.parent;
    parentWindow.document.getElementById('Container').src = 'LupaPeriudhaKontabel.aspx';
    parentWindow.popupUniversal.Show();
}
function NdryshoNdermarrje() {
    var parentWindow = window.parent;
    //var paneContent = parentWindow.splitter.GetPaneByName('paneKryesor');
    //contentUrl = "Login_Ndermarrje.aspx";
    window.parent.location.href ="Login_Ndermarrje.aspx?redirect=false";
 
    //paneContent.SetContentUrl(contentUrl);
    //paneContent.RefreshContentUrl();
}
$(document).ready(function () {//po
    //Utils.SetPeriudha(btnPeriudha.GetText());
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