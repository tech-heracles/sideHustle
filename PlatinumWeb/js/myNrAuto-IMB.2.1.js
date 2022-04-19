window.myNrAuto = (function () {

    var shtoNrAuto = function (devHf, nrAuto) {
        devHf.Set(nrAuto.kodKontrolli, JSON.stringify(nrAuto));
    };

    var vendosNrAutomatik = function (colAtrTrupi, colKontrollet, date, nrAutos) {
        for (var i = 0; i < colAtrTrupi.length; i++) {
            var kodKontrolli = colKontrollet[i].KodKontrolli;
            var IdNrAutomatik = colAtrTrupi[i].IdNrAutomatik;
            if (IdNrAutomatik !== 0) {
                if (nrAutos && Object.keys(nrAutos).length) {
                    SucceededCallbackNrAutoPaSerial(nrAutos[kodKontrolli]);
                    continue;
                }
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "merrVlerenNrAutomatik"),
                    data: JSON.stringify({ kodKontrolli: kodKontrolli, idNrAuto: IdNrAutomatik, date: date })
                }).done(function (nrAuto) { SucceededCallbackNrAutoPaSerial(nrAuto); });
            }
        }
    };

    var vendosNrAutomatikKodi = function (kodKontrolli, IdNrAutomatik, date) {
        if (IdNrAutomatik != 0) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "merrVlerenNrAutomatik"),
                data: JSON.stringify({ kodKontrolli: kodKontrolli, idNrAuto: IdNrAutomatik, date: date })
            }).done(function (nrAuto) { SucceededCallbackNrAutoPaSerial(nrAuto); });
        }
    };

    var vendosNrAutomatikNrSerial = function (colAtrTrupi, colKontrollet, date) {
        for (var i = 0; i < colAtrTrupi.length; i++) {
            var kodKontrolli = colKontrollet[i].KodKontrolli;
            var IdNrAutomatik = colAtrTrupi[i].IdNrAutomatik;
            if (kodKontrolli === 'txtNumerSerial' && IdNrAutomatik !== 0) {
                vendosNrAutomatikKodi(kodKontrolli, IdNrAutomatik, date);
            }
        }
    };

    var SucceededCallbackNrAuto = function (nrAuto) {
        if (!nrAuto)
            return;
        var kontroll = Utils.ktheKontroll(nrAuto.kodKontrolli);
        if (nrAuto.vlereNrAuto == null && kontroll.GetText() != "")
            return;
        kontroll.SetText(nrAuto.vlereNrAuto);
        shtoNrAuto(hfNrAuto, nrAuto);
        if (nrAuto.EshteNrDrejtFundit) {
            njoftoNrAutoDrejtFundit(nrAuto);
        }
    };

    var SucceededCallbackNrAutoPaSerial = function (nrAuto) {
        SucceededCallbackNrAuto(nrAuto);
        if (nrAuto.kodKontrolli === 'txtNumerSerial' && !kupon && Utils.getUrlVar('shitje_blerje') === 'shitje') {
            txtNumerSerial.SetText('');
            return;
        }
    };

    var njoftoNrAutoDrejtFundit = function (result) {
        if (result.EshteNrDrejtFundit) {
            var msg;
            if (result.kodKontrolli === 'nrSerial_TextBox' || result.kodKontrolli === 'txtNumerSerial')
                msg = "Kujdes numrat seriale po perfundojne, kane ngelur dhe " + result.countNrFundit + " numra seriale!";
            else
                msg = "Kujdes numrat e dokumentit po perfundojne, kane ngelur dhe " + result.countNrFundit + " numra dokumenti!";
            myMesazh.ShtoMesazh({
                text: msg
                , type: "warning"
                , modal: true
                , layout: "center"
                , timeout: 0
            });
        }
    };

    return {
        vendosNrAutomatik: vendosNrAutomatik,
        vendosNrAutomatikKodi: vendosNrAutomatikKodi,
        vendosNrAutomatikNrSerial: vendosNrAutomatikNrSerial,
        SucceededCallbackNrAutoPaSerial:SucceededCallbackNrAutoPaSerial
    }
})();