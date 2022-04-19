var popUpEinvoiceFunctions = {
    idDokumenti: 0,
    changedNiveli: null,
    showPopUp: null,
    idKatDok: 0
};
popUpEinvoiceFunctions.showPopUp = function (idNiveli, idKonfigurimi, idDokumenti, katDokAndKomponentObj, eics) {
    Utils.shfaqLoadingGif();
    this.eics = eics;
    cmbKonfigEinvoice.ClearItems();
    cmbKonfigEinvoice.AddItem("Aprovo");
    cmbKonfigEinvoice.AddItem("Refuzo");
    cmbKonfigEinvoice.SetValue("Aprovo");
    cmbKonfigEinvoice.Validate();
    Utils.hiqLoadingGif();
    popUpEinvoice.Show();

};
popUpEinvoiceFunctions.ndryshoStatus = function (s, e) {
    var eics = "";
    var value;
    for (var i = 0; i < this.eics.length; i++) {
        if (i + 1 == this.eics.length) {
            eics += this.eics[i];
        }
        else {
            eics += this.eics[i] + " ";
        }
    }
    var vleraStatusi = cmbKonfigEinvoice.GetValue();
    if (vleraStatusi == "Aprovo") {
        vleraStatusi = "Aprovuar";
    }
    else {
        vleraStatusi = "Refuzuar";
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "DergoNdryshimStatusiEinvoice"),
        data: JSON.stringify({ EIC: eics, statusi: vleraStatusi, idNdermarrje: hfState.Get("idNdermarrje") })
    }).done(function (result) {
        if (result) {
            myMesazh.ShtoMesazhSuksesi("Ndryshimi i statusit u krye me sukses!");
        }
        else {
            myMesazh.ShtoMesazhGabimi("Ndryshimi i statusit deshtoi!");
        }
    }).fail(function (result) {
        myMesazh.ShtoMesazhGabimi("Ndryshimi i statusit deshtoi!");
    });
    this.idDokumenti = 0;
    popUpEinvoice.Hide();
};