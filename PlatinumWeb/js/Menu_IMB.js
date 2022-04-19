var Menu_IMB = function () {

    //Map-imi me ID dhe klasat e HTML-se
    var mapHTML = {
        divIDPageContentMenuMajtasWrapper: 'page-content-menu-majtas-wrapper',
        divIDMenuMajtasWrapper: 'menu-majtas-wrapper',
        divIDSidebarMenuMajtasWrapper: 'sidebar-menu-majtas-wrapper',
        divIDPanel: 'accordion',
        divIDModule: 'divModul',
        divIDLinkMenu: 'linkMenuja',
        divIDCollapse: 'collapse',
        divIDMenuSettings: 'menuSettings',
        btnIDMenu: 'menu-toggle',
        classNavbarHeader: 'navbar-header',
        classBtnNavbar: 'navbar-toggle',
        classContainer: 'container',
        classPanelCollapse: 'panel-collapse',
        classPanelCollapseTrue: 'collapse',
        classPanelGroup: 'panel-group',
        classPanel: 'panel',
        classPanelDefault: 'panel-default',
        classPanelHeading: 'panel-heading',
        classPanelTitle: 'panel-title',
        classKolonat: 'col-sm-3 col-md-3',
        classRow: 'row',
        classMain: 'main',
        classContainerFluid: 'container-fluid',
        classListGroup: 'list-group',
        classListGroupItem: 'list-group-item',
        classFormControlPersonalizo: 'form-control',
        classMultiSelectComboPersonalizo: 'multiselectCombo',
        classMultiSelectPersonalizo: 'multiselect',
        classUIMultiSelectPersonalizo: 'ui-multiselect'
    }

    //Varibla global gjendjeje
    var pageState = { menuPlote: [], myPopup: "" };

    //Krijimi i struktures per menune anesore
    var krijoStruktureHtmlFaqeje = function (appendTo) {
        var linkButoni = $('<a href="#' + mapHTML.btnIDMenu + '" class="' + mapHTML.classBtnNavbar + '" id="' + mapHTML.btnIDMenu + '"><img src="images/themesDevEx/Menu_Button.png"/></a>');
        var divHeader = $('<div class="' + mapHTML.classNavbarHeader + '"></div');
        $(divHeader).append(linkButoni);

        var divList = $('<div class="' + mapHTML.classListGroup + '"></div>');
        var divCollapse = $('<div class="' + mapHTML.classPanelCollapse + ' ' + mapHTML.classPanelCollapseTrue + '"></div');
        divCollapse.append(divList);
        var divPanel = $('<div class="' + mapHTML.classPanelGroup + '" id="' + mapHTML.divIDPanel + '"></div>');
        divPanel.append(divCollapse);
        var divCol = $('<div class="' + mapHTML.classKolonat + '"></div>');
        divCol.append(divPanel);
        var divCont = $('<div class="' + mapHTML.classContainer + '"></div>');
        divCont.append(divCol);
        var divSidebar = $('<div id="' + mapHTML.divIDSidebarMenuMajtasWrapper + '"></div');
        divSidebar.append(divCont);
        /*var form = $('body>form');
        var divRow = $('<div class="' + mapHTML.classRow + '"></div>');
        $(divRow).append(form);
        var divContFluid = $('<div class="' + mapHTML.classContainerFluid + '"></div>');
        $(divContFluid).append(divRow);
        var divPageCont = $('<div id="' + mapHTML.divIDPageContentMenuMajtasWrapper + '"></div>');
        $(divPageCont).append(divContFluid);
        var divMain = $('<div class="' + mapHTML.classMain + '" style="width: 100%"></div>');
        $(divMain).append(divPageCont);*/

        var DivMenuMajtas = $('<div id="' + mapHTML.divIDMenuMajtasWrapper + '"></div>');
        DivMenuMajtas.append(divHeader);
        DivMenuMajtas.append(divSidebar);
        //$(DivMenuMajtas).append(divMain);
        DivMenuMajtas.appendTo(appendTo);
    }

    //Krijimi i dritares Personalizo
    var krijimiDritaresPersonalizo = function () {

        var opsionMbyllje = hfState.Get("MenuItemMbyll");
        var opsionRuajtje = hfState.Get("labelRuajNdryshimet");
        var titulliModal = hfState.Get("labelTitulliModal");
        var popUpOptions = { prependSelector: "body", dialogClass: "dialog-select-menu", contentClass: "content-select-menu",titulli: titulliModal, text:{mbyll: opsionMbyllje, ruaj: opsionRuajtje}, saveClick: saveKonfig };
        pageState.myPopup = Utils.ndertoPopup(popUpOptions);

        $("." + popUpOptions.contentClass).html("");
        $("." + popUpOptions.contentClass).prepend("<select class='" + mapHTML.classFormControlPersonalizo + ' ' + mapHTML.classMultiSelectComboPersonalizo + "'></select><select class='"
            + mapHTML.classFormControlPersonalizo + ' ' + mapHTML.classMultiSelectPersonalizo + "' multiple='multiple'></select>");

        function saveKonfig() {
            console.debug("saving work in progress");
            changeJson(pageState.menuPlote, $("." + mapHTML.classMultiSelectPersonalizo).val(), $("." + mapHTML.classMultiSelectComboPersonalizo).val());
            sessionStorage.setItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti, JSON.stringify(pageState.menuPlote));
            krijoStringMenuPerShfaqje(sessionStorage.getItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti));
            saveOnServer(sessionStorage.getItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti));
            pageState.myPopup.modal("hide");
        }

        function saveOnServer(menuJson) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "ruajKonfigMenuMajtas"),
                data: JSON.stringify({
                    konfigurimi: menuJson
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
        }
    }

    //Percaktohen te gjithe handler-at per butonat e ndryshem
    var handleVeprimeMeButonat = function () {

        $("#" + mapHTML.btnIDMenu).click(function (e) {
            e.preventDefault();
            $("#" + mapHTML.divIDMenuMajtasWrapper).toggleClass("toggled");
        });

        $("#" + mapHTML.divIDPanel).on('click', 'a', function (event) {
            event.preventDefault();
            var x = $(this);
            var prindiNext = $(this).parents().eq(1).next();
            if (x.context.id == mapHTML.divIDLinkMenu + "settings") {
                pageState.myPopup.modal("show");
                $('.modal-backdrop').remove();
                $("." + mapHTML.classMultiSelectComboPersonalizo).trigger("change");
                mbushComboGrupe("");
                return;
            }
            if (prindiNext.hasClass(mapHTML.classPanelCollapse + " " + mapHTML.classPanelCollapseTrue + " in")) {
                $("#" + mapHTML.divIDPanel + " a").each(function () {
                    $(this).attr('data-toggle', mapHTML.classPanelCollapseTrue);
                });
                x.removeAttr('data-toggle');
            }
            if (prindiNext.hasClass(mapHTML.classListGroup))
                pageState.activeMenuName = x[0].name;

        });

        function mbushComboGrupe(aktivGrupName) {
            var comboGrupe = $("." + mapHTML.classMultiSelectComboPersonalizo);
            comboGrupe.html("");
            $.each(pageState.menuPlote, function (index, item) {
                var tmpOption = new Option(item["Text"], item["Name"]);
                /*if (aktivGrupName && item["Name"] == aktivGrupName)
                    $(tmpOption).attr("selected", "selected"); //select it*/
                if (item["Items"].length)
                    comboGrupe.append(tmpOption);
            });
            if (!aktivGrupName)
                comboGrupe.val($(comboGrupe).find("option").val());
        }

        $("." + mapHTML.classMultiSelectComboPersonalizo).on("change", function (e) {
            if (typeof pageState.previousAktiv == "undefined")
                pageState.previousAktiv = null;
            ndryshimiGrupeve($(this).val(), pageState.previousAktiv);
            pageState.previousAktiv = $(this).val();
        });

        function ndryshimiGrupeve(grupiZgjedhur, previousAktiv) {
            var grupAktivJson = findGrupMenuJson(pageState.menuPlote, grupiZgjedhur);
            mbushElemGrupi(grupAktivJson, previousAktiv);
        }

        function findGrupMenuJson(menuJson, grupiZgjedhur) {
            var grupMenuJson = [];
            if (!grupiZgjedhur && menuJson.length > 0) {
                grupMenuJson = menuJson[0].Items
            }
            else if (grupiZgjedhur !== "") {
                $.each(menuJson, function (index, item) {
                    if (item["Name"] == grupiZgjedhur) {
                        grupMenuJson = item["Items"];
                        return false;
                    }
                });
            }
            return grupMenuJson;
        }

        function mbushElemGrupi(grupMenuJson, previousAktiv) {
            if ($("." + mapHTML.classUIMultiSelectPersonalizo).children().length != 0) {
                if (previousAktiv)
                    changeJson(pageState.menuPlote, $("." + mapHTML.classMultiSelectPersonalizo).val(), previousAktiv);
                $("." + mapHTML.classMultiSelectPersonalizo).multiselect("destroy");
                $("." + mapHTML.classMultiSelectPersonalizo).html("");
            }
            $.each(grupMenuJson, function (index, item) {
                var tmpOption = new Option(item["Text"], item["Name"]);
                if (item["Visible"])
                    $(tmpOption).attr("selected", "selected"); //select it
                $("." + mapHTML.classMultiSelectPersonalizo).append(tmpOption);
            });
            $("." + mapHTML.classMultiSelectPersonalizo).multiselect({
                sortable: false, dividerLocation: 0.5, nodeComparator: function (node1, node2) {
                    var text1 = node1.val(),
                        text2 = node2.val();
                    return text1 == text2 ? 0 : (text1 < text2 ? -1 : 1);
                }
            });
            $("." + mapHTML.classUIMultiSelectPersonalizo).addClass(mapHTML.classPanel + " " + mapHTML.classPanelDefault);
        }

    }

    //Krijimi i grupeve dhe nenmenume sipas te drejtave dhe konfigurimeve personale
    var krijoMenuneSipasTeDrejtave = function () {
        $('#' + mapHTML.divIDSidebarMenuMajtasWrapper).css('cursor', 'pointer');

        merrMenuSipasTeDrejtave();

        //Grupon kategorine e te drejtave sipas ambjenteve
        function grupimiSipasKategorive(array, f) {
            var grupe = [];
            array.forEach(function (objekt) {
                var group = f(objekt);
                var filtrimGrupi = grupe.filter(function (el) {
                    return el.Name == group;
                });

                if (filtrimGrupi.length == 0) {
                    var items = [];
                    items.push({ "Name": objekt.KOMPONEMRI, "Text": objekt.PERSHKRIMKOMPONENTE, "Visible": false });
                    grupe.push({ "Name": group, "Text": group, "Visible": false, "Items": items });
                }
                else if (filtrimGrupi.length == 1) {
                    filtrimGrupi[0].Items.push({ "Name": objekt.KOMPONEMRI, "Text": objekt.PERSHKRIMKOMPONENTE, "Visible": false })
                }
                pageState.idPerdorues = objekt.IDPERDORUES;
                pageState.idNdermarrja = objekt.IDNDERMARRJE;
                pageState.idViti = objekt.IDVITI;
            });
            grupe.push({ "Name": "settings", "Text": "Personalizo", "Visible": true, "Items": [] });

            return grupe;
        }

        //Marrja e gjithe te drejtave qe useri ka
        function merrMenuSipasTeDrejtave() {           
            $.ajax({
                type: "GET",
                url: Utils.getServerApiUrl("Konfigurime", "merrMenuSipasTeDrejtave"),
                data: JSON.stringify({ idPerdoruesi: pageState.idPerdorues, idNdermarrje: pageState.idNdermarrja, idviti: pageState.idViti })
            }).done(konvertoMenuSipasTeDrejtave);
        }

        //Marrja e menuse se personalizuar te userit, ndermarrjes ose default
        function merrMenuPersonalizuar() {
            //Nese menuja eshte ne sessionStorage atehere nuk thirret webservice
            if (sessionStorage.getItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti)) {
                krijoStringMenuPerShfaqje(sessionStorage.getItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti));
                return;
            }

            $.ajax({
                type: "GET",
                url: Utils.getServerApiUrl("Konfigurime", "merrMenuPersonalizuar"),
                data: JSON.stringify({ idNdermarrje: pageState.idNdermarrja, idPerdoruesi: pageState.idPerdorues })
            }).done(krijoStringMenuPerShfaqje);
        }

        //Konverton menune e te drejtave sipas formatit qe njeh struktura e menuse majtas
        function konvertoMenuSipasTeDrejtave(arrayMenu) {

            pageState.menuPlote = grupimiSipasKategorive(arrayMenu, function (item) {
                return item.TEXTMODUL;
            });

            //Therret marrjen e menuse se personalizuar nga useri, ndermarrja ose default
            merrMenuPersonalizuar();
        }
    }

    var krijoStringMenuPerShfaqje = function (menuPersonalizuar) {
        if (!sessionStorage.getItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti))
            sessionStorage.setItem("menuPersonalizuar_" + pageState.idPerdorues + "_" + pageState.idNdermarrja + "_" + pageState.idViti, menuPersonalizuar)
        var menuPersonalizuarJSON = JSON.parse(menuPersonalizuar);
        $.each(pageState.menuPlote, function (index, item) {
            var nenMenuVisible = false;
            if (item.Name !== "settings") {
                var menu = ktheDuhetShfaqurMenu(menuPersonalizuarJSON, item.Text);
                if (menu !== "") {
                    $.each(item.Items, function (indexEach2, element) {
                        var nenMenu = ktheDuhetShfaqurMenu(menu.Items, element.Name);
                        if (nenMenu !== "") {
                            if (nenMenu.Visible) {
                                nenMenuVisible = true;
                                element.Visible = true;
                            }
                        }
                    });
                    if (nenMenuVisible)
                        item.Visible = true;
                    else
                        item.Visible = false;
                }
            }
        });
        krijoMenune(pageState.menuPlote);
    }

    var ktheDuhetShfaqurMenu = function (menuPersonalizuarJSON, emriKrahasues) {
        var filtrimGrupi = menuPersonalizuarJSON.filter(function (el) {
            return el.Name == emriKrahasues;
        });

        if (filtrimGrupi.length == 1) {
            return filtrimGrupi[0]
        }
        else
            return "";

    }

    var krijoMenune = function (menuPersonalizuar) {
        $("#" + mapHTML.divIDPanel).empty();
        $.each(menuPersonalizuar, function (index, item) {
            if (item.Visible == true) {
                var linkMenuja = $('<a data-toggle="' + mapHTML.classPanelCollapseTrue + '" data-parent="#' + mapHTML.divIDPanel + '" style="display:block">' + item.Text + '</a>');
                $(linkMenuja).attr('id', mapHTML.divIDLinkMenu + item.Name);
                $(linkMenuja).attr('name', item.Name);

                if (item.Name !== "settings") {
                    var hrefIdDivi = "#" + mapHTML.divIDCollapse + item.Name;//reference tek koka e modulit
                    $(linkMenuja).attr('href', hrefIdDivi);
                }

                var h4Moduli = $('<h4 class="' + mapHTML.classPanelTitle + '"></h4>');
                $(h4Moduli).append(linkMenuja);

                var divModuli = $('<div class="' + mapHTML.classPanelHeading + '" data-toggle="' + mapHTML.classPanelCollapseTrue + '"></div>');
                $(divModuli).attr("id", mapHTML.divIDModule + item.Text);
                $(divModuli).data("moduli", item.Text);
                $(divModuli).append(h4Moduli);

                var diviKryesor = $('<div class="' + mapHTML.classPanel + ' ' + mapHTML.classPanelDefault + '"></div>');
                $(diviKryesor).append(divModuli);

                $(divModuli).append(h4Moduli);
                $(diviKryesor).append(divModuli);

                var idDivi = mapHTML.divIDCollapse + item.Text;
                var divTrupi = $('<div class="' + mapHTML.classPanelCollapse + ' ' + mapHTML.classPanelCollapseTrue + '"></div>');
                $(divTrupi).attr("id", idDivi);

                var divListGroup = $('<div class="' + mapHTML.classListGroup + '"></div>');

                $.each(item.Items, function (indexEach2, element) {
                    if (element.Visible == true) {
                        var a = $('<a class="' + mapHTML.classListGroupItem + '"></a>');
                        $(a).text(element.Text);
                        $(a).data('url', element.Name);
                        $(a).on("click", function (e) {
                            window.parent.ndryshoUrlFrame($(this).data('url'), false);
                        });
                        $(divListGroup).append(a);
                    }
                });


                $(divTrupi).append(divListGroup);
                $(diviKryesor).append(divTrupi);
                $("#" + mapHTML.divIDPanel).append(diviKryesor);
            }

        });
    }

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
    }

    var findGrupMenuJson = function (menuJson, grupiAktiName) {
        var grupMenuJson;
        $.each(menuJson, function (index, item) {
            if (item["Name"] == grupiAktiName) {
                grupMenuJson = item["Items"];
                return false;
            }
        });
        return grupMenuJson;
    }

    return {
        //Funksioni kryesor per te nisur modulin
        init: function (appendTo) {
            krijoStruktureHtmlFaqeje(appendTo);
            krijimiDritaresPersonalizo();
            handleVeprimeMeButonat();
            krijoMenuneSipasTeDrejtave();
        }
    };
};

Menu_IMB.prototype = {
    constructor: Menu_IMB,
    //Metoda publike per krijimin e menuve, qe duhet te thirret nga cdo faqe
    krijoMenuNeDocReady: function (appendTo) {
        var menuWidget = new Menu_IMB();
        menuWidget.init(appendTo);
    },
};