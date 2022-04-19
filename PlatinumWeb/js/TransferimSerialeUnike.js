;
if (typeof TransferimSerialeUnike == 'undefined') {
    TransferimSerialeUnike = {}
}
TransferimSerialeUnike.ndertoPopupTransferimSerialeUnike = function(options) {
    var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha: 0 }; 
    options = $.extend({}, defaults, options);
    var opsionMbyllje = hfState.Get("MenuItemMbyll");
    var titulliMesazhModal = "Transferim Dalje";
    var popUpOptions = { prependSelector: "body", titulli: titulliMesazhModal, contentClass: "mesazhe", text: { mbyll: opsionMbyllje } };

    var myPopup = Utils.ndertoPopup(popUpOptions);
    myPopup.modal("show");
    $("." + popUpOptions.contentClass).html("");
    var arrButonat = options.idGjuha == 0 ? ['Voucher Shitje', 'Karta Shitje', 'Aparate Shitje', 'Aparate Blerje'] : ['Voucher Sales', 'Cards sales', 'Device Sales', 'Device Purchases'];

    $("." + popUpOptions.contentClass).append($("<div class='row'> <div class='col-md-7'>" + 
                                                "<label>Transferimi i Karta dhe Voucher per Date:</label> </div>" +
                                                "<div class ='col-md-5'> <input type='text' id='dateTransferimDalje' > </div> </div>"));
    $('#dateTransferimDalje').datepicker().datepicker("option", "dateFormat", "dd/mm/yy");
    $('#dateTransferimDalje').datepicker().datepicker("setDate",new Date());

    $("." + popUpOptions.contentClass).append($("<br> <div class='row'> " +
                                                "<div class='col-md-3'> <button id='voucherShitje' type='button' class='btn btn-default' data-target='#webService1'>" + arrButonat[0] + "</button> </div>" +
                                                "<div class='col-md-3'> <button id='kartaShitje' type='button' class='btn btn-default' data-target='#webService2'>" + arrButonat[1] + "</button> </div>" +
                                                "<div class='col-md-3'> <button id='aparatShitje' type='button' class='btn btn-default' data-target='#webService3'>" + arrButonat[2] + "</button> </div>" +
                                                "<div class='col-md-3'> <button id='aparatBlerje' type='button' class='btn btn-default' data-target='#webService4'>" + arrButonat[3] + "</button> </div></div>"));
    $('#voucherShitje').on('click', function(event) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("SerialeUnike", "dergoFileTransferimiSerialeUnike"),
            data: JSON.stringify({ idNdermarje: options.idNdermarrje, kategoriSeriali: 'RINGARKUES', data: dateTransferimDalje.value, idLlojDokumentMag: 2, idMetoda: 0 })
        }
        ).done(TransferimSerialeUnike.SucceededCallbackDerguarSerialeUnike);
    });
    $('#kartaShitje').on('click', function (event) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("SerialeUnike", "dergoFileTransferimiSerialeUnikePerShumeMetoda"),
            data: JSON.stringify({ idNdermarje: options.idNdermarrje, kategoriSeriali: 'KARTA', data: dateTransferimDalje.value, idLlojDokumentMag: 2, idMetoda: [4,0] })
        }
        ).done(TransferimSerialeUnike.SucceededCallbackDerguarSerialeUnike);
    });
    $('#aparatShitje').on('click', function (event) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("SerialeUnike", "dergoFileTransferimiSerialeUnike"),
            data: JSON.stringify({ idNdermarje: options.idNdermarrje, kategoriSeriali: 'APARATE', data: dateTransferimDalje.value, idLlojDokumentMag: 2, idMetoda: 0 })
        }
        ).done(TransferimSerialeUnike.SucceededCallbackDerguarSerialeUnike);
    });
    $('#aparatBlerje').on('click', function (event) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("SerialeUnike", "dergoFileTransferimiSerialeUnike"),
            data: JSON.stringify({ idNdermarje: options.idNdermarrje, kategoriSeriali: 'APARATE', data: dateTransferimDalje.value, idLlojDokumentMag: 1, idMetoda: 0 })
        }
        ).done(TransferimSerialeUnike.SucceededCallbackDerguarSerialeUnike);
    });
}

TransferimSerialeUnike.SucceededCallbackDerguarSerialeUnike = function (result) {
    if (result.Item1.PershkrimMesazhi != "")
        myMesazh.ShtoMesazhSesioni(result.Item1);
    if (result.Item2.PershkrimMesazhi != "")
        myMesazh.ShtoMesazhSesioni(result.Item2);
}