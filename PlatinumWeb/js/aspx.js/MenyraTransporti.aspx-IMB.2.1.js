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
Function: Filtra_Click

Hap lupen e filtrave.
*/

/*
Function: changeName

Perdoret per te thirrur nje web service tek faqja prind, per te marre emrin e faqes qe do te shfaqet, si dhe per te ruajtur ne cookie faqen ku ndodhemi
*/
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('MenyraTransporti.aspx', 0);
    window.parent.createCookie('adresa', 'MenyraTransporti.aspx', 1);
}

function switchEditMode(index) {
    //            if (editmode) {
    //                if (index == indexEdit) {
    //                    grid_MenyraTransporti.CancelEdit();
    //                    editmode = !editmode;
    //                }
    //                else {
    grid_MenyraTransporti.StartEditRow(index);
    indexEdit = index;
    //                }
    //            }
    //            else {
    //                grid_MenyraTransporti.StartEditRow(index);
    //                indexEdit = index;
    //                editmode = !editmode;
    //            }
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
    var emer = 'MenyraTransporti.aspx';
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
        switchEditMode(indexModifiko);
    }
    else {
        alert("Nuk ke te drejta per te kryer kete veprim");
    }
}
function Item_Click(s, e) {
    if(e.item.name=='Fshi')
        popFshi.Show();

    if (e.item.name == 'Ruaj')
        popRuaj.Show();
    if ( e.item.name== 'Filtra')
        popZgjidhFiltrin.Show();
    if(e.item.name=='Modifiko')
    {   
              
        indexModifiko=  grid_MenyraTransporti.GetFocusedRowIndex();
        grid_MenyraTransporti.StartEditRow(indexModifiko);  
    }
    if(e.item.name=='Shto')
        document.location = 'Shto_MenyreTransporti.aspx';

              
} 