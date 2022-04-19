function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        if (window.parent.hfSeriale.Contains(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"))) {
            hfSeriale.Add(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"), window.parent.hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")));
        }
        
        if (Utils.getUrlVar("vjennga") != 'rillogaritja') {
            var grida = window.parent.$('#rowed5');
            var gridIds = grida.jqGrid('getDataIDs');
            var a = 0;
            for (i = 0; i < gridIds.length; i++) {
                if (Utils.getUrlVar("lastsel") == gridIds[i])
                    continue;
                if (window.parent.hfSeriale.Contains(Utils.getUrlVar("id") + '_' + gridIds[i])) {
                    hfSerialeTePerdorura.Add(a, window.parent.hfSeriale.Get(Utils.getUrlVar("id") + '_' + gridIds[i]));
                    a++;
                }
                if (window.parent.hfSasiSeriale.Contains(Utils.getUrlVar("id") + '_' + gridIds[i])) {
                    hfSasiSeriale.Add(a-1, window.parent.hfSasiSeriale.Get(Utils.getUrlVar("id") + '_' + gridIds[i]));
                   
                }
            }
        }
        gvFushat.PerformCallback('pastro');
    }
    catch (err) {
        ///alert('gabim');    
    }
}

function EndRequestHandler(sender, args) {//po  
    if (mbyll) {
        var grida = window.parent.$('#rowed5');
        if (!window.parent.hfSeriale.Contains(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"))) {
            window.parent.hfSeriale.Add(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"), hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")));
        }
        else window.parent.hfSeriale.Set(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"), hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")));
        if (Utils.getUrlVar("vjennga") !== 'rillogaritja') {
            if (!window.parent.hfSasiSeriale.Contains(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"))) {
                window.parent.hfSasiSeriale.Add(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"), (Utils.getUrlVar("mecope") == 'false') ? 1 : grida.getTekstQelize('txtSasia', Utils.getUrlVar("lastsel")));
            }
            else window.parent.hfSasiSeriale.Set(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel"), (Utils.getUrlVar("mecope") == 'false') ? 1 : grida.getTekstQelize('txtSasia', Utils.getUrlVar("lastsel")));
        }

        if (Utils.getUrlVar("vjennga") == 'rillogaritja') {
            if (hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")) != "[]") {
                window.parent.editor.SetText("Me serial");
            }
            else window.parent.editor.SetText("Pa serial");
        }
        else if (Utils.getUrlVar("vjennga") == 'amortizim' || Utils.getUrlVar("vjennga") == 'rivleresim') {
            var idRow = grida.getLastSel2();
            if (hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")) != "[]") {
                var seriale = JSON.parse(hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")));
                grida.setTekstQelize('txtSerial', idRow, seriale[0].AqtSerialKod);
                window.parent.merrGjendjePerSerial(idRow);
            }

            else grida.setTekstQelize('txtSerial', idRow, "");
        }
        else {
            var idRow = grida.getLastSel2();
            if (hfSeriale.Get(Utils.getUrlVar("id") + '_' + Utils.getUrlVar("lastsel")) != "[]") {
                grida.setTekstQelize('txtSerial', idRow, "Me serial");
                if (Utils.getUrlVar("mecope") == 'false')
                    grida.setTekstQelize('txtSasia', idRow, gvZgjedhur.cpNoRows);
                (Utils.getUrlVar("vjennga") == "shitje" || Utils.getUrlVar("vjennga") == "blerje") ? window.parent.changedSasia() : window.parent.vendosVleftat();
            }
            else grida.setTekstQelize('txtSerial', idRow, "Pa serial");
        }
        window.parent.popupUniversal.Hide();
    }
}

function Seriali_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgSerialetLupa"), 'Seriale.aspx?idartikulli=' + Utils.getUrlVar("id"), 900, 500);
}

var mbyll = false;
function menu_click(e) {
    if (e.item.name == "Anullo") {
        e.processOnServer = false;
        window.parent.popupUniversal.Hide();
    }
    if (e.item.name == "Shto") {
        e.processOnServer = false;
        Seriali_Click();
    }
    if (e.item.name == "OK") {
        mbyll = true;
    }
    else mbyll = false;
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);

    }
    catch (e) {
    }
}).trigger('resize');

function UpdateButtonState() {
    btnDjathtasGjitha.SetEnabled(gvFushat.cpNoRows > 0);
    btnMajtaGjitha.SetEnabled(gvZgjedhur.cpNoRows > 0);
    btnDjathtas1.SetEnabled(gvFushat.GetFocusedRowIndex() >= 0);
    btnMajtas1.SetEnabled(gvZgjedhur.GetFocusedRowIndex() >= 0);
}

