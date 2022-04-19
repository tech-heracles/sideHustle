window.menuTypes = (function(){return {menu: "ASPxMenu", navbar: "ASPxNavBar"};})();

function imbMenu(menu, stringKonfig, menuType, idPerdoruesi, idNdermarrje, options) {

    var defaults = {
        voneseKerkimi: 300, fushaEmrit: "Text"
        , mesazhe: {
            "pastrimKerkimi": function(){return "Ju lutem pastroni kerkimin para se te personalizoni menune!";},
            "mungonEmerGrupi": function (tekst) { return "Grupi me tekst: {{tekst}} i mungon atributti Name".replace("{{tekst}}", tekst); },
            "menuPaPersonalizim": function () { return "Kjo menu nuk mund te personalizohet!"; }
        }
    };

    var _tmpFilterString = "";

    var _kerkoInput = null,_kerkoButton = null, intMenuJson = null, menuChanging = null,_kerkoTimeout;
    //var menuSearching = [];

    var setGroupItemsVisibility = function (myTmpGroup, grupItem) {
        var grupVisibility = false;
        //if (grupItem["Name"] && grupItem["Name"] == "settings")
        //    return true;
        var nrItemsCount = myTmpGroup.GetItemCount ? myTmpGroup.GetItemCount() : 0;
        if (nrItemsCount == 0) {
            myTmpGroup.SetVisible(grupItem["Visible"]);
            return grupItem["Visible"];
        }
        for (var j = 0; j < nrItemsCount; j++) {
            var myTmpGroupItem = myTmpGroup.GetItem(j);
            var myTmpGroupItemName = myTmpGroupItem.name;
            $.each(grupItem["Items"], function (index, item) {
                if (myTmpGroupItemName == item["Name"]) {
                    var itemVisibility = item["Visible"];
                    if (!grupVisibility && itemVisibility)
                        grupVisibility = true;
                    myTmpGroupItem.SetVisible(itemVisibility);
                    setGroupItemsVisibility(myTmpGroupItem, item);
                    return false;
                }
            });
        }
        return grupVisibility;
    };

    var applyJsonToMenuItems = function (myMenuJson, menu) {
        if (!myMenuJson)
            return;
        var firstVisibleGrupExpanded = false;
        var autoCollapseDisabled = false;
        if (menu.autoCollapse) {
            menu.autoCollapse = false;
            autoCollapseDisabled = true;
        }
        $.each(myMenuJson, function (i, grupItem) {
            var grupName = grupItem["Name"];
            if (grupName == "")
                myMesazh.ShtoMesazh({ type: "warning", text: options.mesazhe["mungonEmerGrupi"](grupItem["Text"]) });
            if (grupName == "settings" || grupName == "mobile")
                return true;
            var myTmpGroup = menu.GetGroupByName ? menu.GetGroupByName(grupName) : menu.GetItemByName(grupName);
            if (myTmpGroup == null)
                return true;
            var grupVisibility = setGroupItemsVisibility(myTmpGroup, grupItem);
            myTmpGroup.SetVisible(grupVisibility);
            if (!grupVisibility && myTmpGroup.SetExpanded) {
                myTmpGroup.SetExpanded(false);
                return true;
            }
            if (!firstVisibleGrupExpanded && myTmpGroup.SetExpanded) {
                firstVisibleGrupExpanded = true;
                myTmpGroup.SetExpanded(true);
                return true;
            }
            if (myTmpGroup.SetExpanded)
                myTmpGroup.SetExpanded(false);
        });
        if (autoCollapseDisabled) {
            menu.autoCollapse = true;
        }
    };

    var filterCriteria = function (element, filterString, key) {
        return element[key].toLowerCase().replace("ë", "e").replace("Ë", "E").replace("ç", "c").replace("Ç", "C").indexOf(filterString.toLowerCase()) !== -1;
    };

    var filterElements = function (menuSearching, parentMatchedCriteria) {
        var anyVisible = false;
        menuSearching = $.map(menuSearching, function (elem, index) {
            elem["Visible"] = parentMatchedCriteria ? true : filterCriteria(elem, _tmpFilterString, options.fushaEmrit) && elem["Visible"];
            if (!anyVisible && elem["Visible"])
                anyVisible = true;
            if (typeof elem["Items"] == "undefined" || elem["Items"].length ==0) { //nqs nuk ka elemente bija
                return elem;
            }
            var myFilteredElems = filterElements(elem["Items"], elem["Visible"]);
            elem["Items"] = myFilteredElems.menuSearching;
            if (myFilteredElems.anyVisible) {
                elem["Visible"] = true;
                anyVisible = true;
            }
            return elem;
        });
        return { anyVisible: anyVisible, menuSearching: menuSearching };
    };

    var kerkoTextChanged = function (myFilterString) {        
        if (_tmpFilterString == myFilterString)
            return;
        _tmpFilterString = myFilterString;
        var menuMajtasJson = parseStringKonfig(window.parent.hfState.Get("konfigMenuMajtas"));
        if (!menuMajtasJson)
            menuMajtasJson = menuJson;
        if (_tmpFilterString) {
            var menuSearching = $.extend(true, [], menuMajtasJson);
            menuSearching = filterElements(menuSearching).menuSearching;
            applyJsonToMenuItems(menuSearching, menu);
        }
        else
            applyJsonToMenuItems(menuMajtasJson, menu);
    };

    var onSearch = function (searchText, voneseKerkimi) {
        if (_kerkoTimeout) {
            clearTimeout(_kerkoTimeout);
        }
        _kerkoTimeout = setTimeout(function () {
            kerkoTextChanged(searchText);
        }, voneseKerkimi);
    };

    var shtoSearchHandlers = function (kerkoInput, kerkoButton, voneseKerkimi) {
        kerkoInput.on("search", function () { onSearch($(kerkoInput).val(), voneseKerkimi); });
        kerkoButton.on("click", function () { kerkoTextChanged($(kerkoInput).val()); });
    };

    var ndertoKerko = function (menu, voneseKerkimi) {
        if (typeof menu.GetMainElement == "function")
            menu = $(menu.GetMainElement());
        var subRootDiv = $("<div></div>", { class: "input-group" });
        var kerkoButton = $("<button></button>", { type: "button", class: "btn btn-default btn-sm noRadius" }).append($("<span></span>", { class: "glyphicon glyphicon-search" }));
        var kerkoButtonGrup = $("<span></span>", { class: "input-group-btn imb-liste-kerko" }).append(kerkoButton);
        var kerkoInput = $("<input/>", { type: "search", placeholder: "Kerko", class: "form-control input-sm kerko noRadius", incremental: "incremental" });        
        subRootDiv.append(kerkoInput, kerkoButtonGrup);
        menu.parent().prepend(subRootDiv);
        shtoSearchHandlers(kerkoInput, kerkoButton, voneseKerkimi);
        setKerko(kerkoInput, kerkoButton);
    };

    var createJson = function (menu) {        
        var menuItems = [];
        var GetItemCount = "GetItemCount", GetItem = "GetItem";
        if (menu.GetGroupCount && menu.GetGroup) { //eshte grup
            GetItemCount = "GetGroupCount", GetItem = "GetGroup";
        }
        if (!menu[GetItemCount])
            return menuItems;
        var itemCount = menu[GetItemCount]();
        if (itemCount <= 0 && GetItemCount == "GetGroupCount")//ne navbar nqs eshte grup dhe eshte bosh nuk e shtojme
            return menuItems;
        for (var i = 0; i < itemCount ; i++) {
            var item = menu[GetItem](i);                
            var myNewItem = { "Name": item.name, "Text": item.GetText(), "Visible": item.GetVisible(), "Items": [] };
            myNewItem["Items"] = createJson(item);
            if (myNewItem != [])
                menuItems.push(myNewItem);
        }
        return menuItems;
    };

    var mbushElemGrupi = function (grupMenuJson, aktivGrupName, previousAktiv) {
        if ($(".ui-multiselect").children().length != 0) {
            if (previousAktiv)
                changeJson(menuChanging, $(".multiselect").val(), previousAktiv);
            $(".multiselect").multiselect("destroy");
            $(".multiselect").html("");
        }
        $.each(grupMenuJson, function (index, item) {
            var tmpOption = new Option(item["Text"], item["Name"]);
            if (item["Visible"])
                $(tmpOption).attr("selected", "selected"); //select it
            $(".multiselect").append(tmpOption);
        });
        $(".multiselect").multiselect({
            sortable: false, dividerLocation: 0.5,lang:options.idGjuha, nodeComparator: function (node1, node2) {
                var text1 = node1.val(),
                    text2 = node2.val();
                return text1 == text2 ? 0 : (text1 < text2 ? -1 : 1);
            }
        });
        $(".ui-multiselect").addClass("panel panel-default");
    };

    var findGrupMenuJson = function (menuJson, grupiAktiName) {
        var grupMenuJson;
        $.each(menuJson, function (index, item) {
            if (item["Name"] == grupiAktiName) {
                grupMenuJson = item["Items"];
                return false;
            }
        });
        return grupMenuJson;
    };

    var changeJson = function (myMenuJson, elemsSelected, selectedGrupName) {
        var grupJson = findGrupMenuJson(myMenuJson, selectedGrupName);
        $.each(grupJson, function (index, item) {
            if (elemsSelected && elemsSelected.find(function (elem) {
                return elem == item["Name"];
            }))
                item["Visible"] = true;
            else
                item["Visible"] = false;
        });
    };

    var changeGrup = function (aktivGrupName, previousAktiv) {
        var grupAktivJson = findGrupMenuJson(menuChanging, aktivGrupName);
        mbushElemGrupi(grupAktivJson, aktivGrupName, previousAktiv);
    };

    var mbushComboGrupe = function (menuJson, aktivGrupName) {
        var comboGrupe = $(".multiselectCombo");
        comboGrupe.html("");
        $.each(menuJson, function (index, item) {
            var tmpOption = new Option(item["Text"], item["Name"]);
            if (aktivGrupName && item["Name"] == aktivGrupName)
                $(tmpOption).attr("selected", "selected"); //select it
            if (item["Items"].length)
                comboGrupe.append(tmpOption);
        });
        if (!aktivGrupName)
            comboGrupe.val($(comboGrupe).find("option").val());
    };

    var saveOnServer = function (menuJson, idPerdoruesi, idNdermarrje) {
        var myStringifiedKonfigMenuMajtas = stringifyStringKonfig(menuJson);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ruajKonfigMenuMajtas"),
            data: JSON.stringify({
                idPerdoruesi: idPerdoruesi,
                idNdermarrje: idNdermarrje,
                konfigurimi: myStringifiedKonfigMenuMajtas
            })
        }).done(function (result) {
            if (result) {
                noty({ text: 'Ruajta e konfigurimit perfundoi me sukses!', type: "success" });
                console.info('Ruajta e konfigurimit perfundoi me sukses!');
            }
            else {
                noty({ text: 'Ndodhi gabim gjate ruajtjes se konfigurimit!', type: "error" });
                console.error('Ndodhi gabim gjate ruajtjes se konfigurimit!');
            }
        }).error(function (err) {
            noty({ text: 'Ndodhi gabim gjate ruajtjes se konfigurimit!', type: "error" });
            console.error('Ndodhi gabim gjate ruajtjes se konfigurimit! ' + err);
        });
    };

    var saveKonfig = function () {
        console.debug("saving work in progress");
        changeJson(menuChanging, $(".multiselect").val(), $(".multiselectCombo").val());
        applyJsonToMenuItems(menuChanging, menu);
        menuJson = menuChanging;
        //console.debug("saving konfig in server //todo");
        saveOnServer(menuJson, idPerdoruesi, idNdermarrje);
        window.parent.hfState.Set("konfigMenuMajtas", JSON.stringify(menuJson));
        $(".dialog-select-menu").modal("hide");
    };

    var ndertoSettings = function () {
        if (_tmpFilterString) {
            myMesazh.ShtoMesazhGabimi(options.mesazhe["pastrimKerkimi"]);
            return;
        }
        if (menuType == menuTypes.menu) {
            myMesazh.ShtoMesazhGabimi(options.mesazhe["menuPaPersonalizim"]);
            return;
        }

        var opsionMbyllje = hfState.Get("MenuItemMbyll");
        var opsionRuajtje = hfState.Get("labelRuajNdryshimet");
        var titulliModal = hfState.Get("labelTitulliModal");
        var popUpOptions = { prependSelector: "#backDiv", dialogClass: "dialog-select-menu", contentClass: "content-select-menu", titulli: titulliModal, text: { mbyll: opsionMbyllje, ruaj: opsionRuajtje }, saveClick: saveKonfig };
        var myPopup = Utils.ndertoPopup(popUpOptions);

        $("." + popUpOptions.contentClass).html("");
        $("." + popUpOptions.contentClass).prepend("<select class='form-control multiselectCombo'></select><select class='form-control multiselect' multiple='multiple'></select>");
        $(".multiselectCombo").on("change", function (e) {
            if (typeof this.previousAktiv == "undefined")
                this.previousAktiv = null;
            changeGrup($(this).val(), this.previousAktiv);
            this.previousAktiv = $(this).val();
        });
        myPopup.modal("show");
        menuChanging = jQuery.extend(true, [], menuJson);
        var aktivGrup = menu.GetActiveGroup();
        mbushComboGrupe(menuChanging, aktivGrup == null ? "" : aktivGrup.name);
        $(".multiselectCombo").trigger("change");
    };
    var addMesazhe = function (mesazhe) {
        options.mesazhe = jQuery.extend({}, options.mesazhe, mesazhe);
    };
    var setKerko = function (input, button) {
        _kerkoInput = input;
        _kerkoButton = button;
    }
    var parseStringKonfig = function (stringKonfig) {
        return stringKonfig ? JSON.parse(stringKonfig) : false;
    };

    var stringifyStringKonfig = function (toStringifyJson) {
        return JSON.stringify(toStringifyJson);
    };
    var checkLang = function (options) {
        if (!options.mesazhe) //qe mos te ndodhe kjo duhet qe ti kalosh si parameter mesazhet ne varesi te gjuhes
            console.warn("nuk jane dhene mesazhet sipas gjuhes per menune");
    };
    var init = function (menu) {
        //checkLang(options);
        options = jQuery.extend({}, defaults, options);
        intMenuJson = parseStringKonfig(stringKonfig);
        if (window.parent && window.parent.hfState)
            menuJson = parseStringKonfig(window.parent.hfState.Get("konfigMenuMajtas"));
       
        if (options.kerko) {
            setKerko(options.kerko.input, options.kerko.button);
            shtoSearchHandlers(options.kerko.input, options.kerko.button,options.voneseKerkimi);
        }
        else
            ndertoKerko(menu, options.voneseKerkimi);

        applyJsonToMenuItems(intMenuJson, menu);
        applyJsonToMenuItems(menuJson, menu);
        intMenuJson = createJson(menu);
        menuJson = createJson(menu);//qe te marresh item-at e rinj qe sjane ne json
    };

    init(menu);

    return {
        personalizo: ndertoSettings,
        getKerko: function (){
            return {input:_kerkoInput, button: _kerkoButton};
        },
        extendMesazhe: function (mesazheJson) {
            options.mesazhe = jQuery.extend({}, options.mesazhe, mesazheJson);
        }
    };
};
    