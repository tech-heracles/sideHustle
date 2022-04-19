function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaDetArtRegj, "613", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}


function Init() {
    window.parent.window.parent.SessionTimeout.sendKeepAlive();
    window.parent.myFaqeCelje.shtoHandlerSession();
    //btnOk.Focus();
}

function menu_click(s, e) {
    //    if (e.item.name == 'Filtra')
    //        popZgjidhFiltrin.Show();
    //    if (e.item.name == 'Ruaj')
    //        popRuaj.Show();
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        case 'Shto':
            e.processOnServer = false;
            var idArt = Utils.getUrlVar('idArtikulli');
            if (!(typeof (idArt) == "undefined" || idArt == undefined || idArt == "0" || idArt == "")) {
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ktheTeDhenaArtikullit"),
                    data: JSON.stringify({ idArt: Utils.getUrlVar('idArtikulli'), idNdermarrje: hfState.Get('idNdermarrje') })
                }).done(SuccededShto1);
            }
            break;
        case 'Lidh':
            e.processOnServer = false;
            var idArt = Utils.getUrlVar('idArtikulli');
            if (!(typeof (idArt) == "undefined" || idArt == undefined || idArt == "0" || idArt == "")) {
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ktheTeDhenaArtikullit"),
                    data: JSON.stringify({ idArt: Utils.getUrlVar('idArtikulli'), idNdermarrje: hfState.Get('idNdermarrje') })
                }).done(SuccededLidh);
            }
            break;
    }
}
function SuccededLidh(value) {
    var artikulli = value[0];
    var lidhur = value[1];
    var llojDetajimi = Utils.getUrlVar('lloji'); //detajimi i pare ose i dyte
    var veprimi = 0;

    if (artikulli != null) {
        if (artikulli.DetajimArtikulli) {
            if (llojDetajimi == 1)
                veprimi = artikulli.IdKategoriDetajimi;
            else if (llojDetajimi == 2)
                veprimi = artikulli.IdKategoriDetajimi2;

            myButtonClickLupa.LupaUniversal_Click('Lidh Detajim', 'LupaDetajimeLidhje.aspx?veprimi=' + veprimi + '&lloji=' + llojDetajimi + ' &idArtikulli=' + Utils.getUrlVar('idArtikulli'), 800, 700);
        }
        else myMesazh.ShtoMesazhGabimi('Artikulli qe keni zgjedhur nuk eshte i lidhur me detajime!');
    }
}
function SuccededShto1(value) {
    var artikulli = value[0];
    var lidhur = value[1];
    var llojDetajimi = Utils.getUrlVar('lloji'); //detajimi i pare ose i dyte
    var veprimi = 0;

    if (artikulli != null) {
        if (artikulli.DetajimArtikulli) {
            if (llojDetajimi == 1)
                veprimi = artikulli.IdKategoriDetajimi;
            else if (llojDetajimi == 2)
                veprimi = artikulli.IdKategoriDetajimi2;
            if (lidhur != true) {
                if (window.parent.identifikuesPerPopupDetajime == 'RegjistrimDokumentash') {
                    queryStr = 'vjenNga=Shto_RegjistrimDokumentash&idArtikulli=' + Utils.getUrlVar('idArtikulli') + '&lloji=' + llojDetajimi + '&veprimi=' + veprimi;
                    window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
                }
                else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimMagazine') {
                    queryStr = 'vjenNga=Shto_RegjistrimMagazina&idArtikulli=' + Utils.getUrlVar('idArtikulli') + '&lloji=' + llojDetajimi + '&veprimi=' + veprimi;
                    window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
                }
                else {
                    $.ajax({
                      url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionURL"),
                        data: JSON.stringify({ url: window.location.href })
                    }).done(function () { Succeded(veprimi); });
                }
            }
            else myMesazh.ShtoMesazhGabimi('Artikulli që keni zgjedhur nuk mund të lidhet me detajime sepse jane bërë veprime!');
        }
        else myMesazh.ShtoMesazhGabimi('Artikulli qe keni zgjedhur nuk eshte i lidhur me detajime!');
    }
    else myMesazh.ShtoMesazhGabimi('Ju lutemi zgjidhni artikullin!');
}

function SuccededShto(value) {
    if (window.parent.identifikuesPerPopupDetajime == 'RegjistrimDokumentash') {
        queryStr = 'vjenNga=Shto_RegjistrimDokumentash&idArtikulli=' + Utils.getUrlVar('idArtikulli') + '&lloji=' + Utils.getUrlVar('lloji') + '&veprimi=' + value;
        window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimMagazine') {
        queryStr = 'vjenNga=Shto_RegjistrimMagazina&idArtikulli=' + Utils.getUrlVar('idArtikulli') + '&lloji=' + Utils.getUrlVar('lloji') + '&veprimi=' + value;
        window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
    }
    else {
        $.ajax({
          url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionURL"),
            data: JSON.stringify({ url: window.location.href })
        }).done(function () { Succeded(value); });
       
    }
}

function Succeded(value) {
    queryStr = 'vjenNga=ekze&idArtikulli=' + Utils.getUrlVar('idArtikulli') + '&veprimi=' + Utils.getUrlVar('veprimi') + '&lloji=' + Utils.getUrlVar('lloji') + '&veprimi=' + value;
    window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaDetArtRegj.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaDetArtRegj.GetVisibleRowsOnPage() - 1) {
            gvLupaDetArtRegj.SetFocusedRowIndex(0);
        }
        else {
            gvLupaDetArtRegj.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaDetArtRegj.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaDetArtRegj.GetSelectedFieldValues('IdDetajimArtikulli;KodDetajimArtikulli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (Utils.getUrlVar("vjenNga") === "Shto_Planifikim") {
        window.parent.vendosDetajim({ IdDetajimArtikulli: values[0][0], KodDetajimArtikulli: values[0][1] }, Utils.getUrlVar("lloji"));
        window.parent.popupUniversal.Hide();
    }
    else Utils.ShtoDetajimeNeGride(values);
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDetArtRegj.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDetArtRegj.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');