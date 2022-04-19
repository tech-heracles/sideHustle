;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;

jQuery(document).ready(function () {
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
    $(window).on('load', function () {
        Init();
    });

});

function Init() {
    changeName();
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function changeName() {
    myFaqeCelje.changeName('Asistenti.aspx', 0, null);    
}



/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvPajisje, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvPajisje, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvPajisje, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvPajisje, indexSel);
}


/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}



function EndRequestHandler(sender, args) {

}

function ThirrStoredProcedureRregulluese(result) {
    if (result) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "rregulloGabimAsistenti"),
            data: JSON.stringify({ id: result, idNderm: hfState.Get("idNdermarrje"), idViti: hfState.Get("idViti")})
        }).done(SucceededCallback);
    }
    else
        myMesazh.ShtoMesazhInformues("Nuk ka sp rregulluese!");
}

function SucceededCallback(result) {
    if (!result)
        return;
    var mesazhetTotal = "";
    $.each(result, function (index, item) {
        mesazhetTotal += item.PershkrimMesazhi + "; ";
    });
    mesazhetTotal = mesazhetTotal.substring(0, mesazhetTotal.length - 1);
    myMesazh.ShtoMesazhSuksesi(mesazhetTotal);
    gvAsistenti.PerformCallback("");   
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "Rifresko":
        case "Kontrollo":
            e.processOnServer = false;
            gvAsistenti.GetSelectedFieldValues('Id', CustomCallbackMeIdPerEkzekutim);
            break;
        case "Rregullo":
            e.processOnServer = false;
            gvAsistenti.GetSelectedFieldValues('Id', RregulloDisaSp);
            break;
        default:
            break;
    }
}

function CustomCallbackMeIdPerEkzekutim(result) {
    var idPerEkzekutim = "";
    $.each(result, function (index, item) {
        idPerEkzekutim += item + ",";
    });
    idPerEkzekutim = idPerEkzekutim.substring(0, idPerEkzekutim.length - 1);
    gvAsistenti.PerformCallback(idPerEkzekutim);
}

function RregulloDisaSp(result) {
    var idPerEkzekutim = "";
    $.each(result, function (index, item) {
        idPerEkzekutim += item + ",";
    });
    idPerEkzekutim = idPerEkzekutim.substring(0, idPerEkzekutim.length - 1);
    ThirrStoredProcedureRregulluese(idPerEkzekutim);
}