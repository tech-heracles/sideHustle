;
$(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idVitNdermarrje = hfState.Get("idVitNdermarrje");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
}).keypress(function (e) {
    switch (e.which) {
        case 13:
            if (e.target.name == "btnAgjenti")
                skeduler.Focus();
            return false;
            break;
    }
});

var identikuesPerPopupAgjenteShitje = 'Router';
var identikuesPerPopupKlientFurnitori = "CRM";
var kd = null;//klient ose detyre;
/*
Function: ButtonClickAgjenti

Hap lupen e agjenteve te shitjes.
*/
function ButtonClickAgjenti(agjenti) {//po
    var queryStr = '';
    popupUniversal.SetHeaderText("Zgjidhni agjentin!");
    popupUniversal.SetContentUrl('LupaAgjenteShitje.aspx?theme=Moderno&idKonfigAmbjente=' + queryStr + '&agjenti=' + agjenti + '&crm=po');
    popupUniversal.SetSize(600, 600);
    popupUniversal.Show();
}
function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
}
function menu_click(s, e) {
    if (e.item.name === 'Modifiko') {
        if (skeduler.GetSelectedAppointmentIds() == '')
            myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje takim');
        else skeduler.ShowAppointmentFormByClientId(skeduler.GetSelectedAppointmentIds());
        e.processOnServer = false;
    }

    else if (e.item.name === 'ShtoKlient' || e.item.name == "ShtoDetyre") {
        if (btnAgjenti.GetValue() != null && btnAgjenti.GetSelectedItem() != null) {
            if (e.item.name === 'ShtoKlient')
                $('#hfLlojDetyre').val(1);
            else if (e.item.name === 'ShtoDetyre')
                $('#hfLlojDetyre').val(2);

            skeduler.RaiseCallback("MNUVIEW|NewAppointment");
        }
        else myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje agjent');
        e.processOnServer = false;
    }
    else if (e.item.name === "Klono") {
        if (btnAgjenti.GetValue() != null && btnAgjenti.GetSelectedItem() != null)
            popupDateRanges.Show();
        else
            myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje agjent');
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        if (skeduler.GetSelectedAppointmentIds() < 1)
            myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje takim ose detyre!');
        else {
            var ids = skeduler.GetSelectedAppointmentIds();

            for (var i = 0; i < ids.length; i++)
                skeduler.DeleteAppointment(skeduler.GetAppointmentById(ids[i]));
        }
        e.processOnServer = false;
    }
}

function EndCallbackGrida(s, e) {
    // $('#hfklienti').val('');
    //  $('#hfLlojDetyre').val('');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    }).done(SucceededCallbackMesazhi);
    if (skeduler.cpUShtuaTakim) {
        delete skeduler.cpUShtuaTakim;
        skeduler.PerformCallback();
    }
}

function DateChanged(s, e) {
    //behem me qellim qe nese eshte ndryshuar periudha te kerkohet perseri konfirmim
    delete popupDateRanges.cpKerkoKonfirmim;
}
function popupDateRangesEndCallback(s, e) {
    if (popupDateRanges.cpKlonimMeSukses == true) {
        mbyllPopupKlonimi(s, e);

        skeduler.PerformCallback();

        delete popupDateRanges.cpKlonimMeSukses;
    }
}
function mbyllPopupKlonimi(s, e) {
    // ASPxClientEdit.ClearEditorsInContainer(popupDateRanges.GetMainElement());
    lblInfo.SetText('');

    popupDateRanges.Hide();
}
function KlonoTakimet(s, e) {
    var isValid = ASPxClientEdit.ValidateGroup('periudha');

    if (isValid) {
        if (popupDateRanges.cpKerkoKonfirmim == true) {
            popupDateRanges.PerformCallback('Konfirmuar');
            delete popupDateRanges.cpKerkoKonfirmim;
        }
        else {
            popupDateRanges.PerformCallback();
        }
    }
}
function KontrolloTakimet() {
    //kontrollo periudhat

    var fromDtStart = fromDateStart.GetValue();
    var fromDtEnd = fromDateEnd.GetValue();
    var toDtStart = toDateStart.GetValue();
    var toDtEnd = toDateEnd.GetValue();
}

function SucceededCallbackMesazhi(result) {
    if (result == null) return;
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    //if (result != null)
    {
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
}

function LostFocusKlientDetyra(s, e) {
    $('#hfklienti').val(ddKlienti.GetValue());
    //  llojDetyreChanged(s, e);
}

function ButtonClickLupaKlientDetyra(s, e) {
    var llojDetyre = $('#hfLlojDetyre').val();

    if (llojDetyre == 1) {//klient
        popupUniversal.SetHeaderText('Zgjidhni klientin');
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?theme=Moderno&veprimi=1&meAgjent=1&crm=po&vjenNgaRoute=true');
    }
    else if (llojDetyre == 2)//detyre
    {
        popupUniversal.SetHeaderText('Zgjidhni Detyren');
        popupUniversal.SetContentUrl('CRMDetyra.aspx?theme=Moderno&veprimi=1&kategoria=1&eshteLupe=true&vjenNgaRoute=true');
    }
    popupUniversal.SetSize(755, 700);
    popupUniversal.Show();
}
function InitKlientDetyraCmb(s, e) {
    $('#hfklienti').val(ddKlienti.GetValue());
}

function RuajTakimeIDneHiddenField(s, e) {
    $('#SelectedIDs').val(s.GetSelectedAppointmentIds().toString());
}

//nuk eshte funksional por mund ta perdorim me vone
function OnSchedulerControlInit(s, e) {
    var selectedId;
    ASPxClientUtils.AttachEventToElement(skeduler.GetMainElement(), 'keydown', function (evt) {
        if (evt.ctrlKey && evt.key == "c") {
            var selectedIds = skeduler.GetSelectedAppointmentIds();
            if (selectedIds.length > 0) {
                selectedId = selectedIds[0];
            }
        }
        if (evt.ctrlKey && evt.key == "v") {
            skeduler.PerformCallback("PasteID" + selectedId);
        }
    });
}

function MerrVleratNgaCombot(s, e) {
    $('#hfklienti').val(ddKlienti.GetValue());
    //  $('#hfLlojDetyre').val(ddLlojDetyre.GetValue());
}

function RuajTakim(s, e) {
    $('#SelectedIDs').val('');//ska vlera te selektuara
    MerrVleratNgaCombot();
    skeduler.AppointmentFormSave();
}

function chkbCheckedChanged(s, e) {
    btnKlono.SetEnabled(DetyraChkb.GetChecked() || KlientChkb.GetChecked());
}
function InitBtnKlono(s, e) {
    s.SetEnabled(DetyraChkb.GetChecked() || KlientChkb.GetChecked());
}

function MenuItemClicked(s, e) {
    //nese eshte zgjedhur nje agjent mund te behet shtimi i nje takimi

    if (e.itemName == "Klient" && btnAgjenti.GetValue() != null && btnAgjenti.GetSelectedItem() != null) {
        $('#hfLlojDetyre').val(1);
        skeduler.RaiseCallback("MNUVIEW|NewAppointment");
    }
    else if (e.itemName == "Detyre" && btnAgjenti.GetValue() != null && btnAgjenti.GetSelectedItem() != null) {
        $('#hfLlojDetyre').val(2);
        skeduler.RaiseCallback("MNUVIEW|NewAppointment");
    }

    else if (e.itemName == "Shiko")
        skeduler.RaiseCallback("MNUAPT|OpenAppointment")
    else if (e.itemName == "Fshi") {
        skeduler.DeleteAppointment((skeduler.GetAppointmentById(skeduler.GetSelectedAppointmentIds()[0])));
    }
    else if (e.itemName == "ShikoHistorik") {
        ShikoDetajePerKlientin("CRMHistoriku.aspx?");
    }
    else if (e.itemName == "ShikoRapAnketa") {
        ShikoDetajePerKlientin("Raporti.aspx?", {
            idraporti: 301,
            windowWidth: $(window).width(),
            radButon: 1,
            idFiltri: 0,
            Filtro: false,
            vjenNga: 'CRM'
        });
    }
    e.handled = true;
}
function detajeClick(s, e, ambjenti) {
    if (ambjenti == "Historiku")
        ShikoDetajePerKlientin("CRMHistoriku.aspx?");
    else if (ambjenti == "Raporti")
        ShikoDetajePerKlientin("Raporti.aspx?", {
            idraporti: 301,
            windowWidth: $(window).width(),
            radButon: 1,
            idFiltri: 0,
            Filtro: false,
            vjenNga: 'CRM'
        });
}
//merr URL e ambjentit ku do shikohet per detaje ne lidhje me klientin e selektuar
function ShikoDetajePerKlientin(url, params) {
    var idTakimi = skeduler.GetSelectedAppointmentIds()[0];
    if (!isNaN(Number(idTakimi))) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "MerrIdKlientiByTakimi"),
            data: JSON.stringify({ idTakimi: idTakimi })
        }).done(function (result) {
            if (result && result.d)
                result = result.d;

            //rasti kur nuk eshte klient,por detyre
            if (result == -1) {
                return;
            }
            //skeduler = new ASPxClientScheduler();

            var app = skeduler.GetAppointmentById(idTakimi);
            if (url == "Raporti.aspx?") {
                params = $.extend(params, {
                    dateNga: app.GetStart().format("yyyy-MM-dd"),
                    dateDeri: app.GetEnd().format("yyyy-MM-dd"),
                    idKlienti: result
                });
            }
            else {
                params = {
                    idKlienti: result,
                    data: app.GetStart().format("yyyy-MM-dd")
                };
            }

            window.open(url + Utils.KonvertoObjectQueryString(params));
        });
    }
}

function btnDetajeInit(s, e) {
    var idTakimi = skeduler.GetSelectedAppointmentIds()[0];
    if (!isNaN(Number(idTakimi))) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "MerrIdKlientiByTakimi"),
            data: JSON.stringify({ idTakimi: idTakimi })
        }).done(function (result) {
            if (result && result.d)
                result = result.d;

            s.SetEnabled(result > 0);
        });
    }
}
function AppointementDblClick(s, e) {
    skeduler.ShowAppointmentFormByClientId(skeduler.GetSelectedAppointmentIds());
    e.processOnServer = false;
}

function OnAppointmentsSelectionChanged(scheduler, appointmentIds) {
    if (appointmentIds != null && appointmentIds.length == 1) {
        scheduler.GetAppointmentProperties(appointmentIds[0], 'Subject;Description;TeKlienti', OnGetAppointmentProps);
    } else
        OnGetAppointmentProps(null);
}
function OnGetAppointmentProps(values) {
    var subj = document.getElementById('aptsubj');
    var desc = document.getElementById('aptdesc');
    var teKlienti = document.getElementById('teklienti');

    if (values != null) {
        pnlAptDetails.SetVisible(true);
        subj.innerHTML = values[0];

        desc.innerHTML = values[1];
        teKlienti.innerHTML = values[2];
        //contact.innerHTML = (values[5] == null) ? "" : values[5];
    } else {
        pnlAptDetails.SetVisible(false);
        var emptyStr = '&nbsp';
        subj.innerHTML = emptyStr;
        desc.innerHTML = emptyStr;
        teKlienti.innerHTML = emptyStr;
    }
}
function initpnlAptDetails(s, e) {
    pnlAptDetails.SetVisible(skeduler.GetSelectedAppointmentIds().length > 0);
}

///funksion i cili inicializon datat me daten e sotme ne shfaqje te popupit te klonimit
function klonoPopupShown(s, e) {
    var sot = Utils.HiqOren(new Date());

    fromDateStart.SetDate(sot);
    fromDateEnd.SetDate(sot);
    toDateStart.SetDate(sot);
    toDateEnd.SetDate(sot);
}
function btnAgjentiLostFocus(s, e) {
    if (s.GetValue() != undefined)
        skeduler.PerformCallback();
}
function btnAgjentiCloseUp(s, e) {
    //hehe
    btnAgjenti.LostFocus.FireEvent(s)
}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_btnPrev(s, e) {
    btnAgjenti.SetSelectedIndex(btnAgjenti.GetSelectedIndex()-1);
    skeduler.PerformCallback();
}
function Click_btnNext(s, e) {
    btnAgjenti.SetSelectedIndex(btnAgjenti.GetSelectedIndex()+1);
    skeduler.PerformCallback();
}
function Click_Anullo(s, e) {
    popupDateRanges.Hide();
    e.processOnServer = false;
}