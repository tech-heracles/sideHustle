var ndermarrjet = [];
var ndermarjefiltruar = [];
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
                break;
            default:
                break;
        }
    });
  
$(document).keyup(function(){
   if($('#PopUpNdermarrje').hasClass('in')){
         if ($('#searchnder').val() == '') {
            MbushPopUp(ndermarrjet);
         }
         else {
             var ndermarjefiltruar = ndermarrjet.filter(function (index) { return index.NdermarrjeKod.toLowerCase().includes($('#searchnder').val()); });
             MbushPopUp(ndermarjefiltruar);
         }
   }
});

    cmbRaporte = new MultiSelect({
        valueField: 'IdRap',
        labelField: 'RapEmri',
        searchField: 'RapEmri',
        maxItems: 1,
        multiSelectId: 'cmbRaporte',
        meLupe: false,
        init: true,
        visible: true,
        onChange: function () { MbushcmbFormate(); }
    });
    cmbFormate = new MultiSelect({
        valueField: 'IdDesign',
        labelField: 'Pershkrimi',
        searchField: 'Pershkrimi',
        maxItems: 1,
        multiSelectId: 'cmbFormate',
        meLupe: false,
        init: true,
        visible: true
    });

    Init();
    MerrRaportetMeFormatePrintimi();
});

function Init() {
    changeName();
}

function changeName() {
    myFaqeCelje.changeName('FormatePrintimi.aspx', 0, null);
}

function MerrRaportetMeFormatePrintimi() {
    var idgjuha = HfState.Get('idGjuha');
    $.ajax({
        pritpergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "RaporteMeFormatePrintimi"),
        data: JSON.stringify({ idGjuha: idgjuha })
    }).done(SucceededCallback);

}

function MerrNdermarrjetEPalidhura() {
    $('#searchnder').val('');
    $('#btnZgjidhTeGjithaNdermarrjet').html('Zgjidh te gjitha ndermarjet');

    var idDesign = cmbFormate.GetValue();
    $.ajax({
        pritpergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrNdermarrjeTePalidhura"),
        data: JSON.stringify({idDesign: idDesign })
    }).done(MbushNdermarrjet);
}

function MbushNdermarrjet(result){
  ndermarrjet = [];
  $.each(result, function(index, ndermarrje){
    ndermarrjet.push(ndermarrje);
  });
  MbushPopUp(ndermarrjet);
}

function MbushPopUp(ndermarrjet) {
    $('#TabeleNdermarrje tbody').empty();
    $.each(ndermarrjet, function (index, ndermarrje) {
        $('#TabeleNdermarrje tbody').append('<tr><td style="width:20px"><input type = "checkbox" id = '+ndermarrje.NdermarrjeId+'></td><td style="width:265px">' + ndermarrje.NdermarrjeKod + '</td><td style="width:265px">' + ndermarrje.NdermarrjePersh + '</td></tr>');
    });

}

function SucceededCallback(result) {
    if (!result)
        return;
    cmbRaporte.Options.options = result;
    cmbRaporte.Init();
    cmbRaporte.SetValue([result[0].IdRap]);
    MbushcmbFormate(cmbRaporte.Options.options[0].IdRap);
}

function MbushcmbFormate(IdRap) {

    var idRap = IdRap == null ? cmbRaporte.GetValue() : IdRap;
    MerrFormatPrintimiSipasRaportit(idRap);
}

function MerrFormatPrintimiSipasRaportit(idRap) {
    $.ajax({
        pritpergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "DizajneSipasRaportit"),
        data: JSON.stringify({ idRaporti: idRap })
    }).done(FormateSucceededCallback);
}

function FormateSucceededCallback(result) {
    if (!result)
        return;
    cmbFormate.Options.options = result;
    cmbFormate.Init();
    cmbFormate.SetValue([result[0].IdDesign]);
}

function btnPrintPreview_Clicked() {
    var idDesign = cmbFormate.GetValue();
    var idRap = cmbRaporte.GetValue();
    var url = "RaportiShpejte.aspx?Sesioni=false&vjen=testim&idDokumenti=-1&idraporti=" + idRap + "&iddesign=" + idDesign;
    var paneViewFatura = splitterFormatePrintimi.GetPaneByName('paneViewFatura');
    paneViewFatura.SetContentUrl(url);
    paneViewFatura.RefreshContentUrl();
    return false;
}

function GetSelectedRows() {
    var idNdermarrjeveTeZgjedhura = "";
    $('input:checkbox').each(function () {
        if ($(this).prop('checked'))
            idNdermarrjeveTeZgjedhura += (($(this).prop('id') + ","));
    });

    idNdermarrjeveTeZgjedhura.length -= 1;
    return idNdermarrjeveTeZgjedhura;
}

function DergoNdermarrjeNeServer() {

    var idTE = GetSelectedRows();
    if (idTE == "") {
        myMesazh.ShtoMesazh({ type: 'warning', text: "Ju nuk keni zgjedhur asnje ndermarrje!" });
        return;
    }

    $.ajax({
        pritpergjigje: true,
        shfaqLoading: true,
        url: Utils.getServerApiUrl("Rregjistrime", "lidhNdermarrjetEZgjedhuraMeFormatin"),
        data: JSON.stringify({ ndermarrjet: idTE, idDesign: cmbFormate.GetValue() })
    }).done(function () {
        MerrNdermarrjetEPalidhura();
        myMesazh.ShtoMesazh({ type: 'success', text: "Lidhja perfundoi me sukses." });
    }).fail(function () {
        myMesazh.ShtoMesazh({ type: 'error', text: "Nje gabim ka ndodhur gjate lidhjes!" });
    });
}

function ToogleTeGjithaNdermarrjet() {
  if ($('#btnZgjidhTeGjithaNdermarrjet').text() == "Zgjidh te gjitha ndermarjet") {
      $('input:checkbox').prop('checked', true);
      $('#btnZgjidhTeGjithaNdermarrjet').html('Pastro te gjitha ndermarjet');
  }
  else {
    $('input:checkbox').prop('checked', false);
    $('#btnZgjidhTeGjithaNdermarrjet').html('Zgjidh te gjitha ndermarjet');
  }
}
