var popUpKlonimiFunctions = {
    idDokumenti : 0,
    changedNiveli : null,
    showPopUp: null,
    klonoDokument: null,
    idKatDok: 0
};

popUpKlonimiFunctions.changedNiveli = function (s, e) {
    cmbKonfigKlono.ClearItems();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigurimAmbjentiSipasNenkategorise"),
        data: JSON.stringify({ idNiveli: s.GetValue() })
    }).done(function (result) {
        result.map(function (item) { cmbKonfigKlono.AddItem(item.KodKonfigAmbjente, item.IdKonfigAmbjente); });
        cmbKonfigKlono.SetSelectedIndex(0);
    })
};

popUpKlonimiFunctions.showPopUp = function (idNiveli, idKonfigurimi, idDokumenti, katDokAndKomponentObj) {
    Utils.shfaqLoadingGif();

    var wrapper = this;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheNenkategoriSipasKategorive"),
        data: JSON.stringify({ katDokAndKomponentObj: katDokAndKomponentObj, idNiveli: idNiveli })
    }).done(function (result) {

        cmbNiveliKlono.ClearItems();
        result.allNivelet.map(function (item) { cmbNiveliKlono.AddItem(item.KODI, item.IDNIVEL); });

        cmbKonfigKlono.ClearItems();
        result.konfigAmbientiSlim.map(function (item) { cmbKonfigKlono.AddItem(item.KodKonfigAmbjente, item.IdKonfigAmbjente); });

        cmbNiveliKlono.SetValue(idNiveli);
        cmbKonfigKlono.SetValue(idKonfigurimi);
        wrapper.idDokumenti = idDokumenti;
        wrapper.idKatDok = result.idKatDok;
        cmbKonfigKlono.Validate();
        cmbNiveliKlono.Validate();
        popUpKlonimi.Show();
        Utils.hiqLoadingGif();
    });
};

popUpKlonimiFunctions.klonoDokument = function (s, e) {
    e.processOnServer = false;
    var idNiveliKlono = cmbNiveliKlono.GetValue();
    var idKonfigKlono = cmbKonfigKlono.GetValue();
    if (!idKonfigKlono || !idNiveliKlono) {
        console.log("Klonim: ska vlera te zgjedhura te zgjedhura");
        return;
    }

    kategoriNjejte = false;
    var komponente;
    switch (cmbNiveliKlono.GetText()) {
        case "FSH":
        case "USH":
        case "KSH":
        case "OSH":
            kategoriNjejte = (this.idKatDok == 1);
            komponente = 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=klonim';
            break;
        case "FB":
        case "UB":
        case "KB":
        case "OB":
            kategoriNjejte = (this.idKatDok == 2);
            komponente = 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=klonim';
            break;
        case "UD":
        case "FD":
            kategoriNjejte = (this.idKatDok == 6);
            komponente = 'Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=klonim';
            break;
        case "FH":
        case "UH":
            kategoriNjejte = (this.idKatDok == 6);
            komponente = 'Shto_RegjistrimMagazine.aspx?lloj=hyrje&shtim_modifikim=klonim';
            break;
        default:
            console.log("Klonim: ska nivel te sakte zgjedhur");
            return;
    }

    if (komponente && this.idDokumenti > 0)
        myFaqeCelje.kontrolloTeDrejta(komponente + '&id=' + this.idDokumenti + '&niveli=' + idNiveliKlono + '&konfigurim=' + idKonfigKlono + '&kategoriNjejte=' + kategoriNjejte + '&idKatDokKlon=' + this.idKatDok + '&pageCacheId=' + window['CurrentPageId']);

    this.idDokumenti = 0;
    popUpKlonimi.Hide(); 
};