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

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('KushteDergimi.aspx',0);
    window.parent.createCookie('adresa', 'KushteDergimi.aspx', 1);
}

function switchEditMode(index) {
    //            if (editmode) {
    //                if (index == indexEdit) {
    //                    grid_KushteDergimi.CancelEdit();
    //                    editmode = !editmode;
    //                }
    //                else {
    grid_KushteDergimi.StartEditRow(index);
    indexEdit = index;
    //                }
    //            }
    //            else {
    //                grid_KushteDergimi.StartEditRow(index);
    //                indexEdit = index;
    //                editmode = !editmode;
    //            }
}

function Init() {
    changeName();
    editmode = false;
    indexEdit = -1;
}

function callWebservice() {
    var emer = 'KushteDergimi.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

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
              
        indexModifiko=  grid_KushteDergimi.GetFocusedRowIndex();
        grid_KushteDergimi.StartEditRow(indexModifiko);  
    }
    if(e.item.name=='Shto')
        document.location = 'Shto_KushtDergimi.aspx';

              
} 