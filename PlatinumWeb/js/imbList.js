(function ($) {


    // the widget definition, where "custom" is the namespace,
    // "imblist" the widget name
    $.widget("custom.imblist", {
        // This is the easiest way to have default options.
        // default options
        options: {
            konfigRap: {},
            lista: {},
            kerko: true,
            ruaj: true,
            resizable: true,
            voneseKerkimi: 200,
            shtoCanvas: false,
            ruajAspectRatio: false,
            aspectRatio: 4 / 3,
            urlPerRuatjeKonfigurimi: (typeof Utils !== 'undefined' ? Utils.getServerApiUrl("Konfigurime", "ruajKonfigListRaportesh") : "")
        },

        // the constructor
        _create: function () {

            this._initListFromJson();
            this._krijoMenu(this.options.selektorMenu);
            //this.options.konfigRap = rregulloKonfigRap(this.options.konfigRap, this.options);

        },
        // called when created, and later when changing options
        _refresh: function () {
            // trigger a callback/event
            this._initListFromJson();
            this._trigger("change");
        },
        _krijoMenu: function (selektorMenu) {
            var that = this;
            var rootDiv = $("<div></div>", { class: "form-group" });
            var subRootDiv = $("<div></div>", { class: "col-sm-2 input-group" }).appendTo(rootDiv);
            var kerkoInput = $("<input/>", { type: "text", placeholder: (pageState.idGjuha == 0 ? "Kerko" : "Search"), class: "form-control input-sm imb-liste-kerko noRadius" });
            var kerkoButton = $("<button></button>", { type: "button", class: "btn btn-default btn-sm noRadius" }).append($("<span></span>", { class: "glyphicon glyphicon-search" }));
            var kerkoButtonGrup = $("<span></span>", { class: "input-group-btn imb-liste-kerko" }).append(kerkoButton);
            var ruajButtonGrup = $("<span></span>", { class: "input-group-btn imb-liste-ruaj" }).append($("<button></button>", {
                type: "button",
                class: "btn btn-default btn-sm noRadius"
            }).append($("<span></span>", { class: "glyphicon glyphicon-floppy-disk" })));
            if (!this.options.kerko) {
                kerkoInput.hide();
                kerkoButtonGrup.hide();
            }
            else {
                if (that.options.voneseKerkimi) {
                    kerkoInput.on("keyup change", function () {
                        if (this.kerkoTimeout) {
                            clearTimeout(this.kerkoTimeout);
                        }
                        this.kerkoTimeout = setTimeout(function () {
                            kerkoButton.trigger("click");
                        }, that.options.voneseKerkimi);
                    });
                    kerkoButtonGrup.hide();
                }
                kerkoButton.on("click", function () {
                    that._kerkoTextChanged(that.options);
                });
            }
            ruajButtonGrup.on("click", function () {
                that._saveKonfig(true, pageState.params);
            });
            subRootDiv.append(kerkoInput, kerkoButtonGrup);
            if (!this.options.ruaj) {
                ruajButtonGrup.hide();
            } else
                subRootDiv.append(ruajButtonGrup);
            if (selektorMenu) {
                $(selektorMenu).append(rootDiv);
            }
            else {
                this.element.prepend(rootDiv);
            }
        },
        _initListFromJson: function () {
            this.options.lista = this._rregulloJsonRaporte(this.options);
            this.options.konfigRap = this._rregulloKonfigRap(this.options.konfigRap, this.options);
            this.options.myList = this.options.lista;
            this._krijoListen(this.options)
        },
        _rregulloJsonRaporte: function (params) {
            if (params.idModuliRaporteve != 19) {
                $.each(params.lista, function (i, item) {
                    if (!params.shtoCanvas)
                        item[params.fushaLinkut] = "'javascript:filtroButtonClick(" + item[params.fushaIdRaportit] + ",&quot;" + item[params.fushaEmrit] + "&quot;)'";
                });
            }
            return params.lista;

        },
        //Krijon konfigurim fillestar nese nuk vjen ne input
        _rregulloKonfigRap: function (konfigRap, params) {
            if (konfigRap) { //nese ekziston mos bej gje 
                return konfigRap;
            }
            var konfigList = params.lista.map(function (item) {
                return item[params.fushaIdRaportit].toString();
            });
         
            konfigRap = new Array();
            konfigRap.push({ "emerGrupi": (pageState.idGjuha == 0 ? "Kryesore": "Main"), "lista": konfigList });
            return konfigRap;
        },
        _kerkoTextChanged: function (options) {
            options.filterString = $("input.imb-liste-kerko").val();
            //this._krijoListen({ lista: options.myList, konfigRap: options.konfigRap, filterString: $("input.imb-liste-kerko").val(), fushaEmrit: pageState.fushaEmrit, fushaLinkut: pageState.fushaLinkut, selektorHomeMenu: pageState.selektorHomeMenu, fushaIdRaportit: pageState.idRaporti });
            this.options.lista = this.options.myList;

            this._krijoListen(options);
        },
        _krijoListen: function (params) {
            var that = this;
            that._filterListen(params);
            params.accordionOptions = {
                collapsible: true,
                heightStyle: "content"
            };
            var rootList = $(params.selektorHomeMenu);
            params.sortableOptions = {
                connectWith: ".imb-sortable"
                , receive: function (event, ui) {
                    that._sortableReceive(event, ui, params)
                }
                , remove: function (event, ui) {
                    that._sortableRemove(event, ui);
                }
                , helper: 'clone'
                , cancel: ".meHiqPoSjamVetem"
                , helper: 'clone'
                , placeholder: "sortable-placeholder"
                , start: function (e, ui) {
                    ui.placeholder.height(ui.item.height());
                    ui.placeholder.width(ui.item.width());
                }
                , disabled: (params.enableSortable ? !params.enableSortable : params.kerkim)
            };
            rootList.html("");
            if (!params.lista || params.lista.length == 0) {
                return;
            }
            var remainingList = $.extend(true, [], params.lista); //params.lista.slice(); //kopjojme listen
            $.each(params.konfigRap, function (i, item) {
                var grupList = that._krijoGrupList(item.lista, remainingList, params.fushaIdRaportit);
                that._listeShtoGrup(item.emerGrupi, grupList, params);
            });
            if (remainingList.length != 0) { //kemi raporte te reja
                var rapRejaGrupName = pageState.idGjuha == 0 ? "Raporte te reja" : "New Reports";
                var rapRejaGrup = Utils.findInArray(params.konfigRap, function (konfigRapElem) {
                    konfigRapElem.emerGrupi == rapRejaGrupName;
                });
                if (!rapRejaGrup) {
                    rapRejaGrup = { emerGrupi: rapRejaGrupName, lista: remainingList };
                    params.konfigRap.concat(rapRejaGrup);
                }
                else {
                    rapRejaGrup.lista.push(remainingList);
                }
                that._listeShtoGrup(rapRejaGrup.emerGrupi, rapRejaGrup.lista, params);
            }
            that._listeShtoGrupBosh(params);
            that._restoreSize({ selektor: ".imb-sortable li" });

            if (this.options.resizable) {
                $(".imb-sortable li").resizable({
                    stop: function (event, ui) {
                        that._saveSize({ selektor: ".imb-sortable li", size: ui.size });
                    },
                    minHeight: (params.minResizeHeight ? params.minResizeHeight : 28),
                    minWidth: (params.minRezizeWidth ? params.minRezizeWidth : 130),
                    autoHide: true,
                    aspectRatio: (this.options.ruajAspectRatio ? this.options.aspectRatio : false)
                });

                that._addClickList(params);
                $(".ui-resizable-handle.ui-resizable-se.ui-icon").removeClass("ui-icon-gripsmall-diagonal-se");
                $(".ui-resizable-handle.ui-resizable-se.ui-icon").addClass("ui-icon-grip-diagonal-se");
            }
            if (params.shtoCanvas && params.lista.length > 0)
                listeFactory.ngarkoGrafikNeList(params.lista);


            return params;
        },
        // events bound via _on are removed automatically
        // revert other modifications here
        _filterCriteria: function (element, filterString, key) {
            return element[key].toLowerCase().indexOf(filterString.toLowerCase()) !== -1;
        },
        _filterListen: function (params) {
            var that = this;
            if (params.filterString) {
                params.lista = params.lista.filter(function (item) {
                    return that._filterCriteria(item, params.filterString, params.fushaEmrit);
                });
                params.kerkim = true;
            }
            else
                params.kerkim = false;
            return params;
        },
        _sortableReceive: function (event, ui, params) {
            var elemBosh = $("#" + event.target.id + " .meHiqPoSjamVetem");
            if (elemBosh.length == 1)
                elemBosh.remove();
            this._shtoHiqGrupetBosh(params);
            this._saveKonfig(false, params);
        },
        _sortableRemove: function (event, ui) {
            var ul = $("#" + event.target.id + ".imb-sortable");
            if (!ul.children("li").length)
                ul.append(this._listeLiBosh(this));
        },
        _listeLiBosh: function (that) {
            return that._listeLi({ emri: (pageState.idGjuha == 0 ? "SHTO KETU..." : "ADD HERE...") });
        },
        _listeLi: function (params) {
            var liBoshClass = "";
            var canvasHtml = "";
            if (!params.linku)
                params.linku = "";
            if (!params.id && params.id !== 0) {
                params.id = "liBosh";
                liBoshClass = "class='meHiqPoSjamVetem'";
            }
            if (!params.emri) {
                console.error("listeLi(params) - params.emri bosh");
            }
            if (params.shtoCanvas)
                canvasHtml = "<div class='canvasContainer' id= containerRaportMenaxherial" + params.id + "><canvas id= myChart" + params.id + "></canvas></div>";
            return "<li id=" + params.id + " " + liBoshClass + "><div class='box'><div class='text'><a href=" + params.linku + ">" + params.emri + "</a></div>" + canvasHtml + "</div></li>";
        },
        _saveKonfig: function (saveKonfigOnServer, params) {
            var elemListOfGrups = $(".imb-sortable:not(.meHiqPoSjamVetem)");
            var lista = new Array();
            var safeKonfig;
            $.each(elemListOfGrups, function (i, item) {
                if ($(item).sortable("option", "disabled")) {
                    console.info("Je ne kerkim nuk duhet ta shpetosh");
                    return false;
                }
                if ($(item).find(".meHiqPoSjamVetem").length == 1)
                    return true;
                var textOfGrup = $(item).parent().find(".imb-nav-text").text();
                var listOfGrup = $(item).sortable("toArray");
                lista.push({ emerGrupi: textOfGrup, lista: listOfGrup });
                safeKonfig = true;
            });
            if (safeKonfig) {
                params.konfigRap = lista;
                if (saveKonfigOnServer) {
                    var myStringifiedKonfig = JSON.stringify(lista);
                    $.ajax({
                        pritPergjigje: true,
                        type: "POST",
                        contentType: "application/json",
                        url: this.options.urlPerRuatjeKonfigurimi,
                        data: JSON.stringify({
                            idPerdoruesi: pageState.idPerdoruesi,
                            idNdermarrje: pageState.idNdermarrje,
                            idModuli: pageState.idModuliRaporteve,
                            idGjuha :pageState.idGjuha,
                            konfigurimi: myStringifiedKonfig
                        }),       
                      error: function (err) {
                            myMesazh.ShtoMesazh({ text: 'Ndodhi gabim gjate ruajtjes se konfigurimit!', type: "error" });
                        }
                    }).done(function (result) {
                        if (result.Status) {
                            myMesazh.ShtoMesazh({ text: 'Ruajta e konfigurimit perfundoi me sukses!', type: "success" });
                        }
                        else {
                            myMesazh.ShtoMesazh({ text: 'Ndodhi gabim gjate ruajtjes se konfigurimit!', type: "error" });
                            //console.error('Ndodhi gabim gjate ruajtjes se konfigurimit!');
                        }
                    });
                }
            }
        },
        _shtoHiqGrupetBosh: function (params) {
            var nrElemBoshRemained = $(".meHiqPoSjamVetem").length;
            if (nrElemBoshRemained == 0) {
                this._listeShtoGrupBosh(params);
                return;
            }
            if (nrElemBoshRemained > 1) {
                $(".imb-sortable .meHiqPoSjamVetem").parents(".grup:last").remove();
                return;
            }
        },
        _listeShtoGrupBosh: function (params) {
            var emerGrupi = (pageState.idGjuha == 0 ? "Te Tjera " : "Other ") + ($(params.selektorHomeMenu + ' .grup').length + 1);
            var item = {};
            item[params.fushaEmrit] = (pageState.idGjuha == 0 ? "SHTO KETU..." : "ADD HERE...");
            item[params.fushaLinkut] = "javascript:void(0)";
            var lista = [item];
            this._listeShtoGrup(emerGrupi, lista, params);
        },
        _listeShtoGrup: function (emerGrupi, lista, params) {
            var newUl = $("<ul class='imb-sortable'></ul>");
            var that = this;
            $.each(lista, function (i, item) {
                newUl.append(that._listeLi({
                    emri: item[params.fushaEmrit],
                    linku: item[params.fushaLinkut],
                    id: item[params.fushaIdRaportit],
                    shtoCanvas: params.shtoCanvas
                }));
            });
            var div = $(that._listGrupHeader(emerGrupi)).append(newUl);
            $(params.selektorHomeMenu).append(div);
            div.accordion(params.accordionOptions);
            that._shtoClickHeader(div);
            that._restoreAccordion(div);
            newUl.sortable(params.sortableOptions);
            div.find('span.imb-navbar-modifiko').on("click", function (e) {
                that._clickModifikoEmerGrupi(e, params.kerkim);
            });
        },
        _listGrupHeader: function (grupNameTmp) {
            var spanRuajPencil = (this.options.ruaj ? "<span title='Ndrysho Emrin e grupit' class='ui-icon ui-icon-pencil imb-navbar-modifiko'></span>" : '');
            return "<div class='grup'><h3><div style='display:inline-flex;'><span class='imb-nav-text'>" + grupNameTmp + "</span>" + spanRuajPencil + "</div></h3></div>";
        },
        _shtoClickHeader: function (div) {
            $(div).find(".ui-accordion-header").on("click", function () {
                localStorage.setItem($(this).attr("id") + pageState.extendKeyCollapse, $(this).hasClass("ui-accordion-header-active") ? "true" : "false");
                return false;
            });
        },
        _restoreAccordion: function (div) {
            if (!localStorage)
                return;
            var header = div.find(".ui-accordion-header");
            var storedAccState = localStorage.getItem(header.attr("id") + pageState.extendKeyCollapse);
            if (storedAccState && storedAccState == "false")
                header.trigger("click");
        },
        _restoreSize: function (params) {
            if (params.size) {
                console.warn("restoreSize(params) - size nuk duhet ti kalohet si parameter");
            }
            params.save = false;
            this._saveRestoreSize(params);
        },
        _saveSize: function (params) {
            if (!params.size) {
                console.error("saveSize(params) - size-i per tu ruajtur mungon...");
                return;
            }
            params.save = true;
            this._saveRestoreSize(params);
        },
        ///mos perdor kete por saveSive ose restoreSize
        _saveRestoreSize: function (params) {
            if (!localStorage)
                return;
            var key = params.selektor.replace(" ", "_") + "_size" + pageState.extendKeySize;
            if (!params.save) {
                var localStorageSize = localStorage.getItem(key);
                params.size = localStorageSize ? JSON.parse(localStorageSize) : {
                    width: $(params.selektor).width(),
                    height: $(params.selektor).height()
                };
            }
            else
                localStorage.setItem(key, JSON.stringify(params.size));
            $(params.selektor).width(params.size.width).height(params.size.height);
        },
        _addClickList: function (params) {
            if (params.shtoCanvas) {
                $('div.text').click(function () {
                    window.location.href = $(this).find("a").attr("href");
                    return false;
                });
            } else
                $('div.box').click(function () {
                    window.location.href = $(this).find(".text a").attr("href");
                    return false;
                });
        },
        _clickModifikoEmerGrupi: function (e, kerkim) {
            e.stopImmediatePropagation();
            e.preventDefault();
            if (kerkim) {
                myMesazh.ShtoMesazh({
                    text: "Oops!!! Nuk mund te modifikosh emrin e grupit gjate kerkimit. Fshi kerkimin per te vazhduar!",
                    type: "error"
                });
                return;
            }
            var buttonModifiko = $(e.target);
            if (buttonModifiko.hasClass("ui-icon-check"))
                this._saveEmerGrupi(e.target);
            if (buttonModifiko.hasClass("ui-icon-pencil")) {
                this._modifikoEmerGrupi(e.target);
            }
            buttonModifiko.toggleClass("ui-icon-check");
            buttonModifiko.toggleClass("ui-icon-pencil");
            $(".stopClick").on("click keydown", function (e) {
                e.stopImmediatePropagation();
                if (e.which == 13)
                    buttonModifiko.trigger("click");
            });
        },
        _modifikoEmerGrupi: function (target) {
            var h3 = $(target).parents("h3");
            var h3SpanText = h3.find(".imb-nav-text");
            h3SpanText.html("<input type='text' class='stopClick' value='" + h3SpanText.text() + "'>");
        },
        _saveEmerGrupi: function (target) {
            var h3 = $(target).parents("h3");
            var h3SpanText = h3.find(".imb-nav-text");
            h3SpanText.html(h3.find(".stopClick").val());
            this._saveKonfig(false, this.options);
        },
        _krijoGrupList: function (grupKonfigList, remainingList, fushaIdRaportit) {
            var grupList = [];
            $.each(grupKonfigList, function (i, itemKonfig) {
                $.each(remainingList, function (j, item) {
                    if (itemKonfig == item[fushaIdRaportit]) {
                        grupList.push(item);
                        remainingList.splice(j, 1);
                        return false;
                    }
                });
            });
            return grupList;
        },
        _destroy: function () {
            // remove generated elements
            //this.changer.remove();

            //this.element
            //  .removeClass("custom-colorize")
            //  .enableSelection()
            //  .css("background-color", "transparent");
        },

        // _setOptions is called with a hash of all options that are changing
        // always refresh when changing options
        _setOptions: function () {
            // _super and _superApply handle keeping the right this-context
            this._superApply(arguments);
            this._refresh();
        },

        // _setOption is called for each individual option that is changing
        _setOption: function (key, value) {
            // prevent invalid color values
            //if (/red|green|blue/.test(key) && (value < 0 || value > 255)) {
            //    return;
            //}
            this._super(key, value);
        }

    });
})(jQuery);