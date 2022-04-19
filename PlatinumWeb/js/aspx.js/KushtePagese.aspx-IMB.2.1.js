; var editmode = false;
var indexEdit = -1;
var indexModifiko;

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
    $(window).on('load', function () {
        Init();
    });
});

/*
Function: changeName

Perdoret per te thirrur nje web service tek faqja prind, per te marre emrin e faqes qe do te shfaqet, si dhe per te ruajtur ne cookie faqen ku ndodhemi
*/
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('KushtePagese.aspx',0);
    window.parent.createCookie('adresa', 'KushtePagese.aspx', 1);
}

function Init() {
    changeName();
    editmode = false;
    indexEdit = -1;
}

/*
Function: callWebservice

Perdoret per te thirrur nje web service per te kontrolluar nese perdoruesi ka te drejta per modifikim apo jo
Shiko funksionin <SucceededCallback>.
*/
function callWebservice() {
    var emer = 'KushtePagese.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

/*
Function: SucceededCallback

Perdoret per te procesuar pergjigjen e web service nese perdoruesi ka te drejta per modifikim ose jo dhe shfaqet mesazhi perkates.
*/
function SucceededCallback(result) {
    if (result == "true") {
        grid_KushtePagese.GetRowValues(indexModifiko, 'IdKoka', OnGetRowValues);
    }
    else {
        alert("Nuk ke te drejta per te kryer kete veprim");
    }
}

/*
Function: OnGetRowValues

Perdoret per te marre te dhenat e nje rreshti per tia kaluar ato faqes se modifikimit
*/
function OnGetRowValues(values) {
    var index = grid_KushtePagese.visibleIndex;
    window.location = 'Modifiko_KushtPagese.aspx?id=' + values + "&indexrow=" + indexModifiko;
}

/*
Function: OnGridDoubleClick

Perdoret ne rastet kur perdoruesi ben double click te nje rreshti per ta modifikuar ate.
*/
function OnGridDoubleClick(index) {
    indexModifiko = index;
    callWebservice();
}

//function RuajFilter_Click()
//{   
//if(Kodi_ASPxTextBox.GetText()!='')
//popRuaj.Hide();
//}
 
function Item_Click(s, e) {
    if(e.item.name=='Fshi')
        popFshi.Show();

    if (e.item.name == 'Ruaj')
        popRuaj.Show();
    if ( e.item.name== 'Filtra')
        popZgjidhFiltrin.Show();
}