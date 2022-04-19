//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;

$(document).ready(function () {
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
    changeName();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    txtKlienti.SetEnabled(false);
    txtPike.SetEnabled(false);
});

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    
      var  hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar    
      var hfRuaj = $('#hfRuaj');
 
    myMenu.menu_click(s, e, hfRuaj,hfId, PageControl, false, false, -1, pastrofusha, '', '', '', '', '');
}


function changeName() {

    myFaqeCelje.changeNameRegjistrime('ShperndaDhurate.aspx', 0);
}


//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtPike.SetText('');
    cmbKarta.SetText('');
    txtKlienti.SetText('');
    cmbDhurata.ClearItems();
 
}



/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");  
    var hfShtimModifikim = $('#hfRuaj'); 
    var hfId = $('#hfId');

    if (hf.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            window.mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar       
            pastrofusha();
            hf.val("false");

        }
    }
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfRuaj'));
}

 

function TextChangedKarta() {
    if (cmbKarta.GetSelectedItem() != null) {

        var karta = cmbKarta.GetSelectedItem();
        if (karta != null)
        callWebserviceKarta(parseInt(karta.value));
      
    }
}

function callWebserviceKarta(idKarta) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKarteKlienti"),
            data: JSON.stringify({ idKarte: idKarta })
        }).done(SucceededCallbacKarta);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }

}


function SucceededCallbacKarta(rezult) {
    if (rezult != null) {

        $("input[id$='hfKarta']").val(JSON.stringify(rezult));
        try {
            callWebServicePikeKarte(rezult.IdKarta);
            if (rezult.IdKlient != 0) {
                var idNdermarrje = hfState.Get('idNdermarrje');
                var idPerdoruesi = hfState.Get('idPerdoruesi');
                var idGjuha = hfState.Get('idGjuha');

                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheOKlientFurnitor"),
                    data: JSON.stringify({ idKlientFurnitor: rezult.IdKlient, date: new Date(), idKonfigurimi: 1, idNdermarrje: idNdermarrje, idPerdorues: idPerdoruesi, idKomponente: 2019, idGjuha: idGjuha, llojKursi: "1" })
                }).done(function (result) {
                    result.mosPlotesoTeDhena = false;
                    SucceededCallbacOKF(result);
                });

            }
            else txtKlienti.SetText(rezult.Emri);

          
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
    }
}



function callWebServicePikeKarte(idKarte) {

    try {
        var idNdermarrje = hfState.Get('idNdermarrje');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "kthePikeKarte"),
            data: JSON.stringify({ idkarta: idKarte, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackPike);        
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }

}

function SucceededCallbackPike(result) {
    if (result != null) {
        if (result == -999) txtPike.SetText('0');
        else
        txtPike.SetText(result);     
        var karta = JSON.parse($("input[id$='hfKarta']").val()); 
        if (karta != null && result > 0) callWebserviceKategoriDhuratash(karta.IdPolitike);
        else {
            cmbDhurata.ClearItems();
            cmbDhurata.SetText("Ju nuk fitoni dhurate", -1);
            
        }

    }


}


function callWebserviceKategoriDhuratash(idPolitike) {
    try {
        $.ajax({
           pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTrupiPolitikeKarte"),
            data: JSON.stringify({ idPolitike: idPolitike })
        }).done(SucceededCallbacKategoriDhuratash);

    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }

}

function SucceededCallbacKategoriDhuratash(result) {
    var piketotal = parseInt(txtPike.GetText());

    if (result != null) {
        cmbDhurata.ClearItems();
        for (i = 0; i < result.length ; i++) {
          
            if (piketotal < result[0].Pike)
            {
                cmbDhurata.SetText("Ju nuk fitoni dhurate", -1);
                return;
            }

            if (piketotal >= result[i].Pike) {
                cmbDhurata.AddItem(result[i].Pike.toString(), result[i].IdKategoria);
                cmbDhurata.SetText(result[i].Pike.toString());
            }

          
        }
    }


}
function SucceededCallbacOKF(result) {
    if (result != null)
    if (result.kf == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKFnukEkziston"));
        txtKlienti.SetText('');
    }
    else
        txtKlienti.SetText(result.kf.EmertimiKF);
}
 