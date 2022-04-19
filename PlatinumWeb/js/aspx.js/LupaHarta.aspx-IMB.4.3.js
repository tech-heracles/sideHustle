
var harta;
var vjenNga = "";
$(document).on('ready', function () {
    var options = {
            DOTS_PER_INCH: 97,
            option: { div: 'map' },
            drawPoint: true,
            deletePoints: true,
            devControlToStorePoint: window.parent.hfState,
            devControlToStorePointKey: "geom",
            devControlToShowPointCoordinates: koordinate,
            devControlToShowPointCoordinatesParent: window.parent.btneCaktoNeHarte
    };

    vjenNga = Utils.getUrlVar("vjenNga");

    switch (vjenNga) {
        case "koordinataKlienti":
            koordinate.SetText(window.parent.btnKoordinatatEKlientit.GetText());
            koordinate.SetEnabled(false);
            koordinate.ReadOnly = true;
            menuItem = ASPxMenu1.GetItemByName("OK");
            if (menuItem != null)
                menuItem.SetEnabled(false)
            ASPxMenu1.AdjustControl();
            var o = {
                devControlToStorePointKey: "geomKlienti",
                devControlToShowPointCoordinatesParent: null,
                drawPoint: false
            };
            options = Object.assign({}, options, o);
            break;
        case "koordinataTakimi":
            koordinate.SetText(window.parent.btnCaktoNeHarte.GetText());
            koordinate.SetEnabled(false);
            menuItem = ASPxMenu1.GetItemByName("OK");
            if (menuItem != null)
                menuItem.SetEnabled(false)
            ASPxMenu1.AdjustControl();
            var o = {
                devControlToStorePointKey: "geomTakimi",
                devControlToShowPointCoordinatesParent: null,
                drawPoint: false
            };
            options = Object.assign({}, options, o);
            break;
        case "celjeKlientFurnitor":
            var o = {
                devControlToShowPointCoordinatesParent: null
            };
            options = Object.assign({}, options, o);
            break;
    }

    harta = new Harta(options);

});

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            if (vjenNga !== "koordinataTakimi") {
                window.parent.btneCaktoNeHarte.SetText(koordinate.GetText());
            }
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        case "Anullo":
            if (vjenNga == "celjeKlientFurnitor") {
                var koordinatat = window.parent.btneCaktoNeHarte.GetText().split(', ');
                var koordinatePike = 'POINT (' + koordinatat[1] + ' ' + koordinatat[0] + ')';
                window.parent.hfState.Set("geom", JSON.stringify(([koordinatePike])));
            }
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

    
