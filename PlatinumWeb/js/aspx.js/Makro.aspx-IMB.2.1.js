; var indexModifiko;

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
});

function OnGridDoubleClick(index) {
    indexModifiko = index;
    callWebservice1();
}

function OnGetRowValues(values) {
    window.location = 'Modifiko_Makro.aspx?id=' + values[0] + '&kod=' + values[1];
}


function callWebservice1() {
    var emerPlusVeprim = 'Makro.aspx;Modifiko';
   // PlatinumWeb.wsfunc.eshteVeprimILejuar(emerPlusVeprim, SucceededCallback1); //nuk perdoret ky  file
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback1(result) {
    if (result == "true") {
        gvMakro.GetRowValues(indexModifiko, 'IdKokaMakro;KodiKokaMakro', OnGetRowValues);
    }
    else {
        alert("Nuk ke te drejta per te kryer kete veprim");
    }
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('Makro.aspx', 0);
    window.parent.createCookie('adresa', 'Makro.aspx', 1);
}
function Item_Click(s, e) {
    if(e.item.name=='Fshi')
        popFshi.Show();

    if (e.item.name == 'Ruaj')
        popRuaj.Show();
    if ( e.item.name== 'Filtra')
        popZgjidhFiltrin.Show();
}