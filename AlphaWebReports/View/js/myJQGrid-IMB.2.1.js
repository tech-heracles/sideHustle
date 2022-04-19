;
$.ui.autocomplete.prototype._renderMenu = autoCompleteMenuItem;
$.ui.autocomplete.prototype._renderItem = autoCompleteRenderItem;

function autoCompleteMenuItem(ul, items) {
    var showHeader = false,
        columns = [{
            name: 'Kodi',
            width: '100px',
            valueField: 'label'
        }, {
            name: 'Pershkrimi',
            width: '150px',
            valueField: 'desc'
        }];
    var that = this;
    if (showHeader) {
        table = $('<div class="ui-widget-header" style="width:100%"></div>');
        var item = columns[0];
        table.append('<span class="badge" style="padding:0 4px;float:left;"> ' + item.name + '&nbsp&nbsp&nbsp </span>');
        item = columns[1];
        table.append(' <span style="padding:0 4px;float:left;"> ' + item.name + ' </span>');
        //$.each(columns, function (index, item) {
        //    table.append('<span style="padding:0 4px;float:left;"> ' + item.name + ' |</span>');//width:' + item.width + ';
        //});
        table.append('<div style="clear: both;"></div>');
        ul.append(table);
    }
    $.each(items, function (index, item) {
        that._renderItemData(ul, item);
    });
    //$(ul).find("li:odd").addClass("autocomplete-odd");  per kastratin(zgjidhje e shpejt) 
}

function autoCompleteRenderItem(ul, item) {
    var pershk = ((typeof item.desc !== "undefined") ? item.desc : $(item.option).data("desc"));
    return $("<li style='display:inline;width:100%'>")
        .data('ui-autocomplete-item', item)
    .append($("<a class='bootstrap-iso' style='text-align:left;'><span class='badge' style='padding:0 4px;float:left;'>" + item.label + " </span><span class='pershk' style='padding:0 4px;float:left;'/<p>&nbsp&nbsp&nbsp" + pershk + "</span><div style='clear: both;'></div></a>"))
    .appendTo(ul);
}
function shtoAutoComlete(emerEditor, selectFunc, changeFunc, shtoDataKod, idRreshti) {
    if (!emerEditor || !selectFunc || !changeFunc)
        return;
    var selektorKodRreshti = "#" + emerEditor + idRreshti;
    var sourceAutocomplete = new Array();
    if (shtoDataKod)
        $(selektorKodRreshti).data('Kodi', $(selektorKodRreshti).val());
    $(selektorKodRreshti).autocomplete({
        minLength: 0,
        source: sourceAutocomplete,
        delay: 0,
        focus: function (event, ui) {
            return false;
        },
        select: function (event, ui) {
            selectFunc(event, ui, this.id, emerEditor);
            return false;
        },
        change: function (event, ui) {
            myJQGrid.cancelQuickChange({ event: event, ui: ui, emerKodi: emerEditor, changeFunc: changeFunc, idRreshti: idRreshti });
        }
    });
}

function shtoAutoComleteList(autocompleteList, idRreshti) {
    $.each(autocompleteList, function (key, value) {
        shtoAutoComlete(value.emerEditor, value.selectFunc, value.changeFunc, value.shtoDataKod, idRreshti);
    });
}

$.fn.extend({
    zevendeso: function (elemZevendesues) {
        var comboToAdd = false;
        if (typeof this.data("customCombobox") !== "undefined" && this.data("customCombobox") != null) {
            this.data("customCombobox")._destroy();
            comboToAdd = true;
        }
        this.replaceWith(elemZevendesues);
        if (comboToAdd)
            elemZevendesues.combobox();
        return elemZevendesues;
    }
});

if (typeof myJQGrid === 'undefined') {
    myJQGrid = {
        shifraMbasPresjes: 2,
        getRowData: function (grida, rowid) {
            var myLastSel = grida.getlastSel();
            grida.jqGrid('saveRow', myLastSel, false, 'clientArray');
            myLastSel = 0;
            var ids;
            var rreshtaGrida = [];
            if (rowid == undefined)
                ids = grida.jqGrid('getDataIDs');
            else
                ids = [rowid];
            $.each(ids, function (indeksi, idRreshti) {
                var rreshtiKorent = grida.jqGrid('getRowData', idRreshti);
                var reshtiKorentRezultat = new Object();
                $.each(rreshtiKorent, function (emerQelize, vlereQelize) {
                    alert(idRreshti);
                    reshtiKorentRezultat[emerQelize] = grida.getTekstQelize(emerQelize, idRreshti);
                });
                rreshtaGrida[indeksi] = reshtiKorentRezultat;
            });
            return rreshtaGrida;
        },
        setGridParams: function (emerGride, gridParams) {
            myJQGrid[emerGride + "gridParams"] = gridParams;
        },
        getGridParams: function (emerGride) {
            return myJQGrid[emerGride + "gridParams"];
        }
    };
};

$.jgrid.extend({
    _normaPaTvsh: 'Pa TVSH',
    heightRreshtiDefault: 37,
    //_lastSel: -1,
    getMaxLastSel: function () {
        var tmp = this.data("maxLastSel");
        return parseInt(tmp == null ? 0 : tmp);
    },
    setMaxLastSel: function (maxLastSel) {
        this.data("maxLastSel", maxLastSel);
        return this;
    },
    getLastSel2: function () {
        var tmp = this.data("lastsel2");
        return parseInt(tmp == null ? -1 : tmp);
        //return parseInt($(this)._lastSel);
    },
    setLastSel2: function (idRreshti) {
        this.data("lastsel2", idRreshti);
        //$(this)._lastSel = parseInt(idRreshti);
        return this;
    },
    getlastSel: function () {
        /// <summary>Vendos vleren default</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        return eval(this.data('lastsel'));
    },
    getlastSelSub: function () {
        /// <summary>Vendos vleren default</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        return eval(this.data('latstgrid')) + 's' + eval(this.data('latstsel'));
        return this;
    },
    setlastSel: function (emerLastsel) {
        /// <summary>Vendos emrin e lastselit per kte gride</summary>
        /// <param name="emerLastsel" type="String">emri i lastselit</param>
        if (!isNaN(emerLastsel)) alert('setLastSel() duhet ti kalohet emer jo numer');
        this.data('lastsel', emerLastsel);
        return this;
    },
    setlastGrid: function (emerLastgrid) {
        /// <summary>Vendos emrin e lastselit per kte gride</summary>
        /// <param name="emerLastsel" type="String">emri i lastselit</param>
        if (!isNaN(emerLastgrid)) alert('setLastSel() duhet ti kalohet emer jo numer');
        this.data('latstgrid', emerLastgrid);
        return this;
    },
    setVlereDefault: function (emerKolone, vlereDefault) {
        /// <summary>Vendos vleren default</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        this.data('vlereDefault.' + emerKolone, vlereDefault);
        return this;
    },
    getVlereDefault: function (emerKolone) {
        /// <summary>Kthen vleren default te ruajtur dhe e formaton me shifrat pas presjes</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        var shifraPasPresjes = this.getShifraPasPresjes(emerKolone, this.getlastSel());
        var vleraDefault = this.data('vlereDefault.' + emerKolone);
        if (shifraPasPresjes != null || typeof shifraPasPresjes != 'undefined')
            return parseFloat(this.formatoShifra(vleraDefault, shifraPasPresjes));
        return vleraDefault;
    },
    formoArrayOptinosNjesia: function (artikulli) {
        var arrayOptions = new Array();
        var tmpObjNjesi = new Object();
        tmpObjNjesi.value = artikulli.Njesi1Artikulli;
        tmpObjNjesi.text = artikulli.KodNjesia1;
        tmpObjNjesi.koeficienti = 1; //koeficienti per njesine e pare eshte 1;
        arrayOptions[0] = tmpObjNjesi;
        if (artikulli.Njesi1Artikulli !== artikulli.Njesi2Artikulli) {
            tmpObjNjesi = new Object();
            tmpObjNjesi.value = artikulli.Njesi2Artikulli;
            tmpObjNjesi.text = artikulli.KodNjesia2;
            tmpObjNjesi.koeficienti = artikulli.KoeficientArtikulli
            arrayOptions[1] = tmpObjNjesi;
        }
        return arrayOptions;
    },
    getVlereReale: function (emerQelize) {
        return this.data('vlereReale.' + emerQelize);
    },
    //ruan vleren reale te paformatuar, per perdorim te brendshem
    setVlereReale: function (emerQelize, vlereReale) {
        this.data('vlereReale.' + emerQelize, vlereReale);
        return this;
    },
    //kthen zero kur rreshti nuk eshte artikull dhe kur nuk e gjen
    getNetoNivelCmimi: function (HfArt, idRreshti, emerLloji) {
        if (this.getTekstQelize(emerLloji, idRreshti) == 'Artikull') {
            var idKodi = this.getTekstQelize('txtIdKodi', idRreshti);
            var art = HfArt.Get(idKodi);
            if (art)
                return art.brutoNetoNivelCmimi;
        }
        return 0;
    },
    formatoQelize: function (emerKolone, idRreshti, vlereDefault, idrreshtireal) {
        /// <summary>Formaton qelizen me shifrat perkatese mbas presjes. Ne rast se vlera aktuale nuk eshte numer vendoset vlera default qe kalohet si parameter</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        /// <param name="idRreshti" type="Number">id-ja rreshtit</param>        
        /// <param name:vlereDefault>nese eshte undefined merret nga grida</param>
        /// <returns>Kthen Griden</returns>    
        if (typeof vlereDefault == 'undefined')
            vlereDefault = this.getVlereDefault(emerKolone);
        if (isNaN(idRreshti)) return; //duhet te jete numer
        if (idrreshtireal != undefined)
            var vlereQelize = this.getTekstQelize(emerKolone, idRreshti, idrreshtireal);
        else vlereQelize = this.getTekstQelize(emerKolone, idRreshti);
        if (vlereQelize === '' || isNaN(vlereQelize) || typeof vlereQelize == 'undefined' || vlereQelize === 0) {
            this.setTekstQelize(emerKolone, idRreshti, vlereDefault, false, idrreshtireal);
        }
        else
            this.setTekstQelize(emerKolone, idRreshti, vlereQelize, false, idrreshtireal);
        return this;
    },
    //nuk ka rendesi nga thirret mjafton te jete jquery objekti
    formatoShifra: function (text, shifraPasPresjes) {
        if (!isNaN(text))
            return parseFloat(text).toFixed(shifraPasPresjes); //formato numrin
        return text; //nuk eshte numer
    },
    setShifraPasPresjes: function (emerQelize, nrShifraPas, rreshti) {
        if (!isNaN(nrShifraPas))
            if (typeof rreshti !== 'undefined')
                this.data('shifraPasPresjes.' + emerQelize + '.' + rreshti, nrShifraPas);
            else this.data('shifraPasPresjes.' + emerQelize, nrShifraPas);
        return this; // per te mundesuar lidhjen me thirrje te tjera        
    },
    getShifraPasPresjes: function (emerQelize, index) {
        if (this.data('shifraPasPresjes.' + emerQelize + '.' + index) != undefined)
            return this.data('shifraPasPresjes.' + emerQelize + '.' + index);
        return this.data('shifraPasPresjes.' + emerQelize);
    },
    getNorma: function (emerKolone, indexi, cmbLloji, txtIdKodi, memoryArt, memoryLlog) {
        return this.getData(emerKolone, indexi, cmbLloji, txtIdKodi, "norma", memoryArt, memoryLlog);
    },
    getCaktuar: function (emerKolone, indexi, cmbLloji, txtIdKodi, memoryArt, memoryLlog) {
        return this.getData(emerKolone, indexi, cmbLloji, txtIdKodi, "caktuar", memoryArt, memoryLlog);
    },
    getData: function (emerKolone, indexi, cmbLloji, txtIdKodi, data, memoryArt, memoryLlog) {
        var selektori = this.selector + ' #' + emerKolone + indexi;
        if (data && typeof $(selektori + ' option:selected').data(data) !== "undefined" && $(selektori + ' option:selected').data(data) != null)
            return $(selektori + ' option:selected').data(data);
        var listeTvsh = [];
        var artikulli = memoryArt.Get(this.getTekstQelize(txtIdKodi, indexi));
        if (this.getTekstQelize(cmbLloji, indexi) === "Artikull" && artikulli) {
            listeTvsh = artikulli.listeTvsh;
        }
        else {
            var llogaria = memoryLlog.Get(this.getTekstQelize(txtIdKodi, indexi));
            if (this.getTekstQelize(cmbLloji, indexi) === "Llogari" && llogaria) {
                listeTvsh = llogaria.listeTvsh;
            }
        }
        if (listeTvsh.length > 0) {
            var myTvsh = this.getTekstQelize(emerKolone, indexi);
            for (j = 0; j < listeTvsh.length; j++) {
                if (listeTvsh[j].text == myTvsh)
                    return listeTvsh[j][data];
            }
        }
    },
    getVlereQelize: function(emerKolone,indexi){
        var selektori = this.selector + ' #' + emerKolone + indexi;
        return $(selektori).val();
    },
    getTekstQelize: function (emerKolone, indexi, indexireal, data) {
        /// <summary>Merr tekstin e nje qelize. Nese eshte numer me format kthen vleren reale te qelizes dhe jo te formatuaren</summary>
        /// <param name="emerKolone" type="String">Emri i kolones</param>
        /// <param name="idRreshti" type="Number">id-ja rreshtit</param>    
        /// <returns>Kthen vleren e nje qelize</returns> 
        var selektori = this.selector +  ' #' + emerKolone + indexi;
        if (indexireal != undefined)
            selektori =this.selector + ' #' + emerKolone + indexireal;
        var vlera;

        //if ($(selektori).prop("type") == undefined)
        //    return $(selektori).val();

        if ($(selektori).prop("type") == "select-one") { //nese eshte kombo                
            vlera = $(selektori + ' option:selected').text();
            return vlera;
        }
        vlera = $(selektori).val();
        if (typeof vlera !== 'undefined' && isNaN(vlera)) {
            return vlera;
        }
        if (typeof vlera !== 'undefined') {
            var vlereReale = this.getVlereReale(emerKolone + indexi);
            if (vlereReale != null) { //nese eshte numer dhe ka vlere reale
                var shifraPasPresjes = this.getShifraPasPresjes(emerKolone, indexi);
                if (typeof (shifraPasPresjes) != 'undefined') { //nese ka shifra pas presjes                    
                    this.setVlereReale(emerKolone + indexi, vlera);
                    vlereReale = vlera;
                }
            }
            if (!isNaN(vlereReale))
                return parseFloat(vlereReale);
        }
        if (typeof vlera !== 'undefined')
            return vlera;
        //vlera = this.jqGrid('getRowData', indexi)[emerKolone];

        vlera = this.jqGrid('getCell', indexi, emerKolone);

        if (typeof vlera != 'undefined') {
            var vlereReale = this.getVlereReale(emerKolone + indexi);//shtuar nestila se gjate ruatjes nuk merrej vlera reale po ajo e formatuara

            if (!isNaN(vlereReale))
                return parseFloat(vlereReale);
            else
                return vlera;
        }
        // 
        return '';
    },
    setTekstQelize: function (emerKolone, indexi, text, forceFormat, indexireal, myelemCombo) {
        /// <summary>Vendos textin ne qelize dhe nese eshte vlere numer dhe ka nje format e formaton visualisht dhe ruan vleren reale te saj</summary>
        /// <param name:emerKolone>Nese nuk jepet si parameter atehere vendoset vleradefault e paracaktuar</param>
        /// <param name:indexi>Nese nuk jepet si parameter atehere vendoset vleradefault e paracaktuar</param>        
        /// <param name:forceFormat>Nese forceFormat nuk eshte undefined dhe eshte true formaton qelizen dhe pse eshte rreshti korent</param>
        var selektori = this.selector +  ' #' + emerKolone + indexi;
        if (indexireal != undefined)
            selektori = this.selector + ' #' + emerKolone + indexireal;
        //if ($(selektori).prop("type") == undefined)
        //    return this;
        if ($(selektori).prop("type") == "select-one") {
            if ($(selektori).val() != undefined) {
                if (typeof $(selektori).data("customCombobox") !== "undefined" && $(selektori).data("customCombobox") != null) {
                    $(selektori).data("customCombobox").setText(text);
                    return this;
                }
                //var myElem = $(selektori + " option:contains('" + text + "')");
                //if (myElem.text() == text)
                //    myElem.prop('selected', true);
                //else {
                if (myelemCombo)
                    myelemCombo(text, null, indexi, true);
                //}
                return this;
                //  return alert('Mos setTekstQelize per combo te thjeshta');
            }
            this.jqGrid('setCell', indexi, emerKolone, text, 'clientArray', '');
            return this;
        }
        if (typeof text == 'undefined')
            text = this.getVlereDefault(emerKolone);
        var shifraPasPresjes = this.getShifraPasPresjes(emerKolone, indexi);
        if (text == '' && typeof shifraPasPresjes !== 'undefined')
            text = 0;
        if ((!isNaN(text)) && typeof shifraPasPresjes !== 'undefined') {
            text = Math.round(text * 10000000) / 10000000;
            this.setVlereReale(emerKolone + indexi, text);
        }
        var formatedText;
        if (typeof (shifraPasPresjes) != 'undefined') {
            formatedText = this.formatoShifra(text, shifraPasPresjes);
            formatedText = Utils.FormatNumberBy3(formatedText, '.', ',');
        }
        if (typeof ($(selektori).val()) != 'undefined') {
            if (typeof formatedText != 'undefined' && typeof forceFormat != 'undefined' && forceFormat)
                text = formatedText;
            $(selektori).val(text);
            $(selektori).attr('title', text);
            return this;
        }
        if (typeof formatedText != 'undefined')
            text = formatedText;
        if (myelemCombo) {
            myelemCombo(text, null, indexi, true);
            return this;
        }
        this.jqGrid('setCell', indexi, emerKolone, text);
        return this;
    },
    getTeDhenaRreshti: function (idRreshti) {
        //console.time('getTeDhenaRreshti');
        var grida = this, myLastSel = grida.getlastSel(), ids, rreshtaGrida = [];
        grida.jqGrid('saveRow', myLastSel, false, 'clientArray');
        myLastSel = 0;
        if (typeof idRreshti == 'undefined')
            ids = grida.jqGrid('getDataIDs');
        else
            ids = [idRreshti];
        var colModel = grida.jqGrid('getGridParam', 'colModel');
        $.each(ids, function (indeksi, idRreshti) {
            var reshtiKorentRezultat = new Object();
            $.each(colModel, function (indeksKolone, kolona) {
                var emerQelize = kolona.name;
                if (emerQelize == 'txtFshi' || emerQelize == 'txtFshiR' || emerQelize == 'txtFshiZ')
                    reshtiKorentRezultat[emerQelize] = '';
                else
                    reshtiKorentRezultat[emerQelize] = grida.getTekstQelize(emerQelize, idRreshti);
            });
            rreshtaGrida[indeksi] = reshtiKorentRezultat;
        });
        //console.timeEnd('getTeDhenaRreshti');
        return rreshtaGrida;
    },
    getTeDhenaRreshtiAsync: function (callbackBosi) {
        console.time('getTeDhenaRreshtiAsync');
        var grida = this, myLastSel = grida.getlastSel(), ids, rreshtaGrida = [];
        grida.jqGrid('saveRow', myLastSel, false, 'clientArray');
        ids = grida.jqGrid('getDataIDs');
        var i = 0;
        var colModel = grida.jqGrid('getGridParam', 'colModel');
        async.map(ids, function (idRreshti, callbackOuter) {
            var reshtiKorentRezultat = new Object();
            async.map(colModel, function (item, callbackInner) {
                var emerQelize = item.name;
                if (emerQelize == 'txtFshi' || emerQelize == 'txtFshiR')
                    reshtiKorentRezultat[emerQelize] = '';
                else
                    reshtiKorentRezultat[emerQelize] = grida.getTekstQelize(emerQelize, idRreshti);
                callbackInner(null);
            }, function (err, result) {
                rreshtaGrida[i] = reshtiKorentRezultat;
                i++;
                callbackOuter(null);
            })

        },
       function (err) {
           if (err) {
               console.log(err);
               return;
           }
           callbackBosi(rreshtaGrida)

       })
    },
    gjejRreshtaNjesoj: function (IdKodi, lloji, emerIdKodi, emerCmbLloji) {
        var grida = this;
        //ky funksion kthen nje array me id-te e rreshtave qe kane kodin e njejte si kodi i selektuar ne gride qe po modifikojme.
        var gridIds = grida.jqGrid('getDataIDs'); //marrim nr e rreshtave
        var idMeKodNjesoj = new Array(); //ketu ruhen id-te e rreshtave qe kane kodin njesoj me kodin tone.
        var j = 0; //indeksi qe do perdorim per array ku do ruhen id-te e rreshtave.    
        for (i = 0; i < gridIds.length; i++) {
            var tmpIdKodi = grida.getTekstQelize(emerIdKodi, gridIds[i]);
            var tmpLloji;
            if (emerCmbLloji)
                tmpLloji = grida.getTekstQelize(emerCmbLloji, gridIds[i]);
            if ((emerCmbLloji && lloji == tmpLloji && tmpIdKodi == IdKodi) || (!emerCmbLloji && tmpIdKodi == IdKodi))
                idMeKodNjesoj[j++] = gridIds[i];
        }
        return idMeKodNjesoj;
    },
    rregulloNrRendorMeTeMadh: function (index) {
        var grida = this;
        this.resetSelection();
        var ids = grida.getDataIDs();
        for (var i = 0; i < ids.length; i++) {
            if (ids[i] <= index)
                continue;
            if (grida.getTekstQelize('txtNrRendor', ids[i]) == undefined || typeof (grida.getTekstQelize('txtNrRendor', ids[i])) == 'undefined' || grida.getTekstQelize('txtNrRendor', ids[i]) == '')
                continue;
            grida.setTekstQelize('txtNrRendor', ids[i], parseInt(grida.getTekstQelize('txtNrRendor', ids[i])) - 1);
        }
    },
    rregulloNrRendor: function (kodKontroller) {
        var grida = this;
        this.resetSelection();
        var ids = grida.getDataIDs();
        for (var i = 0; i < ids.length; i++) {
            grida.setTekstQelize('txtNrRendor', ids[i], i + 1);
        }
    },
    setFocus: function (emerKolone, indexi) {
        var editor = $(this.selector + ' #' + emerKolone + indexi);
        if (typeof (editor.val()) != 'undefined')
            editor.focus();
    },
    updateNrReshtaScroll: function (elemNrRreshta, nrRreshtaKey) {
        var nrRreshtash = elemNrRreshta.val()
        if (isNaN(nrRreshtash) || parseInt(nrRreshtash) > 99 || parseInt(nrRreshtash) < 0) {
            elemNrRreshta.val('');
            return false;
        }
        if (localStorage)
            localStorage.setItem(nrRreshtaKey, nrRreshtash);

        if (nrRreshtash == "" || nrRreshtash == 0)
            this.setGridHeight('auto');
        else
            this.setGridHeight(this.heightRreshtiDefault * nrRreshtash);
    },
    initNrReshta: function (identifikuesNrRreshta, emergride) {
        var nrRreshtash = 10, nrRreshtaKey = 'NrRreshtash' + identifikuesNrRreshta, grida = this;
        if ($(emergride + "_toppager_right table:last").val() != undefined) {
            $(emergride + "_toppager_right table:last").children("tbody").append('<tr><td><input class="clsBoxNrRreshtashTeShfaqura ui-corner-all" type="text" title="Numri rreshtave" id="txtBoxNrRreshtashTeShfaqura"/></td></tr>');
        }
        else
            $(emergride + "_toppager_right").append('<input class="clsBoxNrRreshtashTeShfaqura ui-corner-all" type="text" title="Numri rreshtave" id="txtBoxNrRreshtashTeShfaqura"/>');
        if (localStorage) {
            var tmpNr = localStorage.getItem(nrRreshtaKey);
            if (tmpNr != null)
                nrRreshtash = tmpNr;
        }
        $(".clsBoxNrRreshtashTeShfaqura").val(nrRreshtash);
        $(".clsBoxNrRreshtashTeShfaqura").on('change', function (e) {
            grida.updateNrReshtaScroll($(".clsBoxNrRreshtashTeShfaqura"), nrRreshtaKey);
        }).trigger('change');
    },
    updateScroll: function () {
        var nrRecords = this.jqGrid('getGridParam', 'records');
        if ($(".clsBoxNrRreshtashTeShfaqura").val() == nrRecords + 1)
            $(".clsBoxNrRreshtashTeShfaqura").trigger('change');
    },
    ngjyrosEkziston: function (idRreshti, txtKodbari, ekziston) {
        if (ekziston)
            this.setCell(idRreshti, txtKodbari, '', {
                'background-color': "#00a74f",
                'background-image': 'none',
                'color': 'white'
            });
        else
            this.setCell(idRreshti, txtKodbari, '', {
                'background-color': "#d73d32",
                'background-image': 'none',
                'color': 'white'
            });
    },
    ngjyrosEkzistonFushat: function (idRreshti, ekziston) {
        var colModel = this.jqGrid('getGridParam', 'colModel');
        for (var i = 0; i < colModel.length; i++) {
            this.ngjyrosEkziston(idRreshti, colModel[i].name, ekziston);
            
        }
    },
    myElemTextBoxFormatNumri: function (params) { //zevendeson myJQGrid.myElemTextBoxVlefteSipasFormatNumri
        var value = params.value, options = params.options, disabled = params.disabled, indexRow = params.indexRow, id = params.id, onKeyDown = params.onKeyDown, onFocusout = params.onFocusout, onFocus = params.onFocus, button = params.button, buttonClick = params.buttonClick, buttonValue = params.buttonValue, buttonClickOnEnter = params.buttonClickOnEnter, changed = params.changed, onTrueKeyDown = params.onTrueKeyDown, returnOnEnter = params.returnOnEnter , onKeyUp = params.onKeyUp;
        var grida = this;
        var divi = $('<div></div>');
        var textField = $('<input type="text" />');
        textField.attr('id', id + indexRow);
        if (params.value == "" || params.value == "NaN" || params.value == undefined) {
            params.value = grida.getVlereDefault(id);
        }
        var realValue = grida.getVlereReale(textField.attr('id'));
        if (realValue != null && typeof realValue != 'undefined' && realValue != '')
            params.value = realValue;
        textField.val(params.value);
        textField.width('95%');
        if (onTrueKeyDown) {
            textField.on("keydown focusout", function (e) {
                if (buttonClickOnEnter && buttonClick && e.which == 13) {
                    buttonClick(e);
                }
                if (returnOnEnter != false && e.which && e.which < 45 && e.which != 32 && e.which != 8) {
                    return;
                }
                //console.log(id + indexRow+ " char: " +String.fromCharCode(e.keyCode));
                onTrueKeyDown(id, indexRow, e);
            });
        }
        if (onKeyDown)
            textField.on("keyup focusout", function (e) {
                if (buttonClickOnEnter && buttonClick && e.which == 13) {
                    buttonClick(e);
                }
                if (returnOnEnter != false && e.which && e.which < 45 && e.which != 32 && e.which != 8) {
                    return;
                }
                //console.log(id + indexRow+ " char: " +String.fromCharCode(e.keyCode));
                onKeyDown(id, indexRow, e);
            });
        textField.on("focusout", function (e) {
            if (this.value == "") {
                //var grida = $(this).closest('table');
                textField.val(grida.getVlereDefault(this.id));
            }
            if (onFocusout)
                onFocusout(id, indexRow, e);
        });

        textField.on("focus", function (e) {
            if (onFocus)
                onFocus(this.id);
        });

        textField.on("change", function (e) {
            if (changed)
                changed();
        });

        textField.on("keyup", function (e) {
            if (onKeyUp) {
                myJQGrid.cancelQuickChangeSasiAktualeEP(indexRow, onKeyUp);
            }
        });

        if (disabled == "True" || disabled === true)
            textField.attr("disabled", "disabled");
        textField.appendTo(divi);
        if (button && buttonClick) {
            var button = $('<input type="button" />');
            if (!buttonValue)
                buttonValue = "...";
            button.val(buttonValue);
            if (disabled == 'True' || disabled === true)
                button.attr("disabled", "disabled");
            var buttonID = "btn" + id + indexRow;
            button.attr("name", buttonID);
            button.attr("id", buttonID);
            button.click(buttonClick);
            button.width('18%');
            textField.width('76%');
            button.appendTo(divi);
        }
        return divi;
    },
    shtoRreshtinEpare: function (arrayReadOnlyKolonaGrides, lostFocusKoloneFundit, emergride) {
        var grida = $(this);
        if (grida.jqGrid('getGridParam', 'reccount') < 1) { //eshte grida bosh?            
            var idRreshti = grida.shtoRresht(arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True', emergride);
            if (($("#hfShtimModifikim").val() == 'shtim' && ($('#hfKthehu').val() == undefined || $('#hfKthehu').val() == '')) || !$("#hfShtimModifikim"))
                grida.jqGrid('setSelection', idRreshti);
        }
        else
            console.warn("Grida " + emergride + " ishte bosh dhe u thirr shtoRreshtinEpare");
    },
    shtoRresht: function (disabled, emergride, datarow) {
        var grida = $(this);
        var maxLastSel = grida.getMaxLastSel();
        grida.setMaxLastSel(++maxLastSel);
        var be = myJQGrid.myElemButtonFshi(disabled, maxLastSel, emergride, lostFocusKoloneFundit);//.clone().wrap('<p>').parent().html().toString();
        if (!datarow)
            datarow = {};
        datarow = $.extend({}, (emergride == "#rowed6" ? { txtFshiR: be } : (emergride == "#rowed7" ? { txtFshiZ: be } : { txtFshi: be })), datarow);
        grida.jqGrid('addRowData', maxLastSel, datarow);
        return maxLastSel;
    },
    isValidDetajimArtikulli: function (editorKodi, emerEditorDetajimi1, idRreshti) {
        if ($(this).getTekstQelize('cmbLloji', idRreshti) == "Llogari" || editorKodi.val() == ""
            || $(this).getTekstQelize('txtIdKodi', idRreshti) == "" //nqs s'eshte ngarkuar akoma artikulli (rasti me enter)
            || hfState.Get('plotesoDetajim1') != true
            || (memoryArt.Get($(this).getTekstQelize('txtIdKodi', idRreshti)) != null && memoryArt.Get($(this).getTekstQelize('txtIdKodi', idRreshti)).DetajimArtikulli != true)
            || $(this).getTekstQelize(emerEditorDetajimi1, idRreshti))
            return true;
        else
            return false;
    },
    lostFocusKoloneFundit: function () {
        //idRreshti = this.getLastSel2();
        var ids = $(this).jqGrid('getDataIDs');
        var index = $(this).jqGrid('getInd', this.getLastSel2());
        $(this).jqGrid('setSelection', ids[index], true);
    },
    pergatitGridePerExport: function (formateText, emerEditorKodi) {
        var GridDatas = {
            gridIDs: $(this).getDataIDs(),
            gridRows: $(this).jqGrid('getTeDhenaRreshti'),
            arrPershkrime: $(this).jqGrid("getGridParam", "colNames"),
            arrElements: $(this).jqGrid("getGridParam", "colModel"),
            formateText: formateText,
            emerEditorKodi: emerEditorKodi
        }
        return myJQGrid.krijoTableHtmlNgaJQGrida(GridDatas);
    },
    gjeRreshtBosh: function (fusheKontrolli, fusheNdihmese, vlereFusheNdihmese) {
        var rreshtiBosh = -1;
        var IDs = this.jqGrid('getDataIDs');
        for (var i = 0; i < IDs.length; i++) {
            if ((!fusheNdihmese && this.getTekstQelize(fusheKontrolli, IDs[i]) == "")
                || (fusheNdihmese && this.getTekstQelize(fusheKontrolli, IDs[i]) == ""
                    && (this.getTekstQelize(fusheNdihmese, IDs[i]) == vlereFusheNdihmese || this.getTekstQelize(fusheNdihmese, IDs[i]) == ""))) {
                rreshtiBosh = IDs[i];
                break;
            }
        }
        return rreshtiBosh;
    },
    gjeRreshtBoshPasKetijRreshti: function (fusheKontrolli, fusheNdihmese, vlereFusheNdihmese, idRow) {
        var rreshtiBosh = -1;
        var IDs = this.jqGrid('getDataIDs');
        for (var i = 0; i < IDs.length; i++) {
            if (IDs[i] <= idRow)
                continue;
            if ((!fusheNdihmese && this.getTekstQelize(fusheKontrolli, IDs[i]) == "")
                || (fusheNdihmese && this.getTekstQelize(fusheKontrolli, IDs[i]) == ""
                    && (this.getTekstQelize(fusheNdihmese, IDs[i]) == vlereFusheNdihmese || this.getTekstQelize(fusheNdihmese, IDs[i]) == ""))) {
                rreshtiBosh = IDs[i];
                break;
            }
        }
        return rreshtiBosh;
    },
    ktheSasiPerArtikullinMeDetajim: function (kodArtShtim, fushat) {
        dataset = this.getTeDhenaRreshti();
        var detajimSasi = new Array();

        var rreshtaArt = dataset.filter(function (el) {
            return (el[fushat.kodi] == kodArtShtim || el[fushat.kodbari] == kodArtShtim)
        });

        if (rreshtaArt.length > 0 ) {

            var koeficientiArt = 1;
            var njesia2Art;
            var count = 0;
            var PozicioniArtikullitTePare = 0;
            var check = false;
            for (var i = 0; i < rreshtaArt.length; ++i) {
                if (rreshtaArt[i].cmbLloji == "Artikull"){
                    if (!check){
                        PozicioniArtikullitTePare = i;
                        check = true;
                    }
                    count++;
                }
            }
            if (rreshtaArt[PozicioniArtikullitTePare][fushat.idKodi] != undefined && rreshtaArt[PozicioniArtikullitTePare][fushat.idKodi] != '' && count > 1) {
                var artikulliMemory = memoryArt.Get(rreshtaArt[PozicioniArtikullitTePare][fushat.idKodi]);
                koeficientiArt = parseFloat(artikulliMemory.KoeficientArtikulli);
                njesia2Art = artikulliMemory.KodNjesia2;
            }

            rreshtaArt.forEach(function (rreshti) {
                //vendos tek objekti detajimSasi, sasite ne rreshta per artikullin sipas detajimeve
                vendosSasiPerDetajimNeObjekt(detajimSasi, fushat.detajimi, rreshti, koeficientiArt, njesia2Art, fushat);
                vendosSasiPerDetajimNeObjekt(detajimSasi, fushat.detajimi2, rreshti, koeficientiArt, njesia2Art, fushat);
            });
        }
        return detajimSasi;
    },
    selektoRreshtin: function (idRow, thirrOnSelectRow) {
        $(this).jqGrid('setSelection', idRow, thirrOnSelectRow);
    },
    gjejIdRreshti: function (fusheKontrolli, vlera) {
        var ids = this.getDataIDs();
        for (var i = 0; i < ids.length; i++) {
            if (this.getTekstQelize(fusheKontrolli, ids[i]) === vlera)
                return ids[i];
        }
        return undefined;
    },
    gjejIdRreshtiSipasFunksionit: function(funksionPerKontroll){
        var ids = this.getDataIDs();
        var data = this.getTeDhenaRreshti();
        for(var i = 0; i < data.length; i++)
        {
            if (funksionPerKontroll(data[i]))
                return ids[i];
        }

        return undefined;
    },
    shtoNeRreshtinEPareBosh: function (fusheKontrolli, butonFshiDisabled, emerGride) {
        var rreshtILire = this.gjeRreshtBosh(fusheKontrolli);
        var idRow;
        if (rreshtILire == -1) {
            idRow = this.shtoRresht(butonFshiDisabled, emerGride);
            this.setLastSel2(-1);
        }
        else
            idRow = rreshtILire;
        this.selektoRreshtin(idRow, true);
        return idRow;
    },
    vendosTeDhenaPerQelizen: function (fusha, idRow, name, value) {
        this.data(name + "." + fusha + idRow, value);
    },
    merrTeDhenaPerQelizen: function (fusha, idRow, name) {
        return this.data(name + "." + fusha + idRow);
    },
    disable: function (fusha, idrow, disable) {
        $(this.selector + ' #' + fusha + idrow).attr('disabled', disable);
}
});

//Kthen fushat qe sherbejne per te kontrolluar nese rreshti eshte bosh, apo ka element te selektuar
myJQGrid.ktheFushaIdentity = function (emerGride) {
    var gridParams = myJQGrid.getGridParams(emerGride);
    return { fusheKontrolli: gridParams.emerEditorKodi, fusheNdihmese: gridParams.emerEditorLloji };
}

////Per tu perdorur ne vend te getRowData te grides
//myJQGrid.getRowData = function (grida, rowid) {
//    var myLastSel = grida.getlastSel();
//    grida.jqGrid('saveRow', myLastSel, false, 'clientArray');
//    myLastSel = 0;
//    var ids;
//    var rreshtaGrida = [];
//    if (rowid == undefined)
//        ids = grida.jqGrid('getDataIDs');
//    else
//        ids = [rowid];
//    $.each(ids, function (indeksi, idRreshti) {
//        var rreshtiKorent = grida.jqGrid('getRowData', idRreshti);
//        var reshtiKorentRezultat = new Object();
//        $.each(rreshtiKorent, function (emerQelize, vlereQelize) {
//            alert(idRreshti);
//            reshtiKorentRezultat[emerQelize] = grida.getTekstQelize(emerQelize, idRreshti);
//        });
//        rreshtaGrida[indeksi] = reshtiKorentRezultat;
//    });
//    return rreshtaGrida;
//};

function formatRow(rowid, rowelem) {
    $.each(rowelem, function (key, value)
    {
        if (!isNaN(value))
            $(emergride).setTekstQelize(key, rowid, value);
    });
}

var arrayRenditjeKolonaGridesSub = [];
function formatRow(rowid, rowelem) {
    $.each(rowelem, function (key, value) {
        if (!isNaN(value))
            $(emergride).setTekstQelize(key, rowid, value);
    });
};

function ndertoJqueryUi(colMagazina, emerEditorMagazina, emerEditorMagazina2, idRreshti) {
    if (typeof colMagazina !== "undefined" && $(emerEditorMagazina + idRreshti).val() != undefined) {
        $(emerEditorMagazina + idRreshti).combobox().click(ndaloPropaganden);
    }
    if (typeof colMagazina !== "undefined" && $(emerEditorMagazina2 + idRreshti).val() != undefined) {
        $(emerEditorMagazina2 + idRreshti).combobox().click(ndaloPropaganden);
    }
    $(".imb-jqgrid-buttonEdit .ikon-drop:button").button({
        text: false,
        icons: {
            primary: "ui-icon-triangle-1-s"
        }
    }).click(ndaloPropaganden);
    $(".imb-jqgrid-buttonEdit .ikon-lupe:button").button({
        text: false,
        icons: {
            primary: "ui-icon-search"
        }
    }).click(ndaloPropaganden);
};

function ndaloPropaganden(e) {
    e.preventDefault();
    e.stopPropagation();
}

myJQGrid.resizeGrid = function (emergride, selektorDivgride3, selektorDivgride2, niveli) {
    if ($(selektorDivgride3).length != 0) {
        myJQGrid.fixGridWidth($(emergride), $(selektorDivgride3), niveli);
    }
    else
        myJQGrid.fixGridWidth($(emergride), $(selektorDivgride2), niveli);
};

myJQGrid.resizeStart = function (even, index, emergride) {
    var colName = $(emergride).jqGrid('getGridParam', 'colModel')[index].name;
    $(emergride).jqGrid('setColProp', colName, { fixed: true });
};

myJQGrid.beforeSelectRow = function (id, e, afterSaveFunc, niveli, emergride, caption) {
    var idRreshti = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if (niveli === hfState.Get('lastniveli') - 1) {
        hfState.Set('lastniveli', niveli);
        return false;
    }
    if (id && (id != idRreshti || (id == idRreshti && emergride !== "#" + hfState.Get('lastselgrid')))) {
        if ($("#" + hfState.Get('lastselgrid')).length > 0)
            $("#" + hfState.Get('lastselgrid')).jqGrid('saveRow', idRreshti, null, 'clientArray', {}, afterSaveFunc);
        $("#" + hfState.Get('lastselgrid')).resetSelection();
        hfState.Set('lastniveli', niveli);
        return true;
    }
    hfState.Set('lastniveli', niveli);
    return true;

};

myJQGrid.pjesaBrendaCaptionPerRaportetPASH = function (rowid, emergride, caption, autocompleteList, selectFunc, changeFunc, arrayReadOnlyKolonaGrides, selektorKodi) {
    var idRreshti = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if (rowid && (rowid !== idRreshti || (rowid === idRreshti && emergride !== "#" + hfState.Get('lastselgrid')))) {
        $('#' + hfState.Get('lastselgrid')).setLastSel2(rowid);
        hfState.Set('lastselgrid', emergride.substring(1, emergride.length));
        idRreshti = rowid;
        jQuery(emergride).jqGrid('editRow', rowid, false);
        if (caption === "Niveli ")
            var idkontrolli = $(emergride).jqGrid('getGridParam', 'colModel')[1].index + hfState.Get('lastselgrid') + idRreshti;
        else idkontrolli = $(emergride).jqGrid('getGridParam', 'colModel')[0].index + hfState.Get('lastselgrid') + idRreshti;
        if (!($(idkontrolli).attr("disabled") === true))
            $(idkontrolli).focus();
        else {
            if (caption === "Niveli ")
                idkontrolli = $(emergride).jqGrid('getGridParam', 'colModel')[2].index + hfState.Get('lastselgrid') + idRreshti;
            else idkontrolli = $(emergride).jqGrid('getGridParam', 'colModel')[1].index + hfState.Get('lastselgrid') + idRreshti
            $(idkontrolli).focus();
        }
    }
    if (selektorKodi != "#" && $(selektorKodi + hfState.Get('lastselgrid') + idRreshti).val() != undefined) {
        shtoAutoComleteList(autocompleteList, hfState.Get('lastselgrid') + idRreshti);
        // shtoAutoComlete(emerEditorKodi, selectFunc, changeFunc, false,idRreshti);
    }


    myJQGrid.keyPressPershkrimi(hfState.Get('lastselgrid'), idRreshti, arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1], caption);
};

myJQGrid.onSelectRow = function (rowid, status, e, params, selektorKodi, grida) {
    if (params.caption != '')
        myJQGrid.pjesaBrendaCaptionPerRaportetPASH(rowid, params.emergride, params.caption, params.autocompleteList, params.selectFunc, params.changeFunc, params.arrayReadOnlyKolonaGrides, selektorKodi);

    if (params.lidhur)
        $(params.emergride).setLastSel2(rowid);
    else {
        var idRreshti = grida.getLastSel2();
        if (rowid && parseInt(rowid) !== idRreshti) {
            var editorKodi = $(selektorKodi + idRreshti);
            if (idRreshti != -1 && typeof editorKodi.val() != "undefined") {
                if ("#" + params.emerEditorCmimi != null && params.emerEditorLloji != null && params.cmimzero != null && params.emerEditorKodi != null) {

                    editorCmimi = $("#" + params.emerEditorCmimi + idRreshti)[0];
                    editorLloji = $("#" + params.emerEditorLloji + idRreshti)[0];
                    if (editorCmimi != undefined && editorLloji != undefined && params.cmimzero != undefined && params.cmimzero == 2
                       && parseFloat(editorCmimi.value) == 0.00 && editorKodi.val() != '' && editorLloji[editorLloji.selectedIndex].value == "1"
                       && ($("#" + params.emerEditorCmimi + idRreshti).data('pending') == 0 || !$("#" + params.emerEditorCmimi + idRreshti).data('pending'))) {
                        myMesazh.ShtoMesazhGabimi('Nuk lejohet cmim zero ne gride');
                        editorCmimi.focus();
                        return;
                    }
                    if (editorCmimi != undefined && editorLloji != undefined && params.cmimzero != undefined && params.cmimzero == 1
                        && parseFloat(editorCmimi.value) == 0.00 && editorKodi.val() != '' && editorLloji[editorLloji.selectedIndex].value == "1"
                        && ($("#" + params.emerEditorCmimi + idRreshti).data('pending') == 0 || !$("#" + params.emerEditorCmimi + idRreshti).data('pending'))) {
                        myMesazh.ShtoMesazhInformues('Kujdes ka cmim zero ne gride');
                    }
                    var editorSasia = $("#txtSasia" + idRreshti)[0];
                    if (editorSasia != undefined && typeof (editorSasia) !== 'undefined' && editorKodi.val() != '' && params.emerEditorLloji != null && editorLloji[editorLloji.selectedIndex].value == "1") {
                        var sasia = editorSasia.value;
                        if (sasia == 0) {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
                            editorSasia.focus();
                            return;
                        }
                        if (sasia === "" || isNaN(sasia)) {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
                            editorSasia.focus();
                            return;
                        }
                    }
                    if (typeof (params.kontrolloCmiminArtSipasKushtit) !== 'undefined' && params.btnKlienti && params.btnKlienti.GetValue() != null && editorCmimi != undefined && editorLloji != undefined && editorKodi.val() != '' && editorLloji[editorLloji.selectedIndex].value == "1") {
                        if (!params.kontrolloCmiminArtSipasKushtit(parseFloat(editorCmimi.value))) {
                            editorCmimi.focus();
                            return;
                        }
                    }

                    if (!grida.isValidDetajimArtikulli(editorKodi, params.emerEditorDetajimi1, idRreshti)) {
                        myMesazh.ShtoMesazhGabimi('Plotesoni detajimin e artikullit!');
                        $("#" + params.emerEditorDetajimi1 + idRreshti).focus();
                        return;
                    }
                }

                if (idRreshti != -1) {
                    var magDetyrueshme = (typeof (pageState) !== 'undefined' && pageState.kushte != undefined && pageState.kushte.GJDM != undefined) ? pageState.kushte.GJDM : true;
                    if (magDetyrueshme && params.emerEditorMagazina != undefined && Utils.IsNullOrEmpty(grida.getTekstQelize(params.emerEditorMagazina, idRreshti))) {
                        grida.setFocus(params.emerEditorMagazina, idRreshti);
                        return;
                    }
                    if (params.emerEditorMagazina2 != undefined && hfState.Get("kushtTransferim") === "Po" && Utils.IsNullOrEmpty(grida.getTekstQelize(params.emerEditorMagazina2, idRreshti))) {
                        grida.setFocus(params.emerEditorMagazina2, idRreshti);
                        return;
                    }
                }
                if (!myJQGrid.isValidTransferimMagazine(params.emerMag1, params.emerMag2, params.cmbKonfigurimi, myMesazh))
                    return;
                grida.jqGrid('saveRow', idRreshti, null, 'clientArray', {}, params.afterSaveFunc);
                if (editorKodi != undefined && editorKodi[0] != undefined && editorKodi.val() == '' && params.resetRreshtKorent) {
                    params.resetRreshtKorent(idRreshti);
                }
            }
            
            idRreshti = rowid;
            grida.setLastSel2(idRreshti);
            grida.jqGrid('editRow', rowid, false);
            if ($(selektorKodi + idRreshti).attr("disabled") !== 'disabled')
                $(selektorKodi + idRreshti).focus();
            
            myJQGrid.vendosMagazineDefault(params, idRreshti, grida);
            ndertoJqueryUi(idRreshti);
            //autocomplete ishte ktu                     
            shtoAutoComleteList(params.autocompleteList, idRreshti);
            if (params.enable)
                params.enable(idRreshti);
            if (!params.subgrid && !params.mosshtorresht)
                myJQGrid.keyPressKodi(grida, idRreshti, params.arrayReadOnlyKolonaGrides[params.arrayReadOnlyKolonaGrides.length - 1], params.emergride);
        }
    }

    if (typeof (params.callWebServiceInfoRow) !== "undefined")
        params.callWebServiceInfoRow(rowid);
}

//vendosim magazinen default sa here qe krijojme nje rresht te ri, nese ka magazine tek koka vendoset e kokes, perndryshe magazina e pare fare
myJQGrid.vendosMagazineDefault = function (params, idRreshti, grida) {
    if ((!Utils.IsNullOrEmpty(grida.getTekstQelize('txtKodi', idRreshti)) && idRreshti != -1))
        return;

    var kodiMagazinaPare = !params.magazinaPare ? "" : params.magazinaPare.Kodi;

    if (params.emerEditorMagazina != undefined && grida.getTekstQelize(params.emerEditorMagazina, idRreshti) == "") {
        var KodiMag = kodiMagazinaPare;
        var PershkrimiMag = !params.magazinaPare ? "" : params.magazinaPare.Pershkrimi;
        if (params.butonMagazina.GetSelectedItem() != null) {
            KodiMag = params.butonMagazina.GetSelectedItem().GetColumnText('Kodi');
            PershkrimiMag = params.butonMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
        }
        grida.setTekstQelize(params.emerEditorMagazina, idRreshti, KodiMag);
        grida.setTekstQelize(params.emerEditorPershkrimMagazina, idRreshti, PershkrimiMag);
    }
    if (params.emerEditorMagazina2 != undefined && grida.getTekstQelize(params.emerEditorMagazina2, idRreshti) == "") {
        var KodiMag2 = params.butonMagazina2.GetSelectedItem() == null ? kodiMagazinaPare : params.butonMagazina2.GetSelectedItem().GetColumnText('Kodi');
        grida.setTekstQelize(params.emerEditorMagazina2, idRreshti, KodiMag2);
    }
}

myJQGrid.resizeStop = function (newWidth, index, emergride, selektorDivgride3, selektorDivgride2, subgrid) {
    var grid = $(emergride);
    var colName = grid.jqGrid('getGridParam', 'colModel')[index].name;
    grid.jqGrid('setColProp', colName, { widthOrg: newWidth });
    var selDivGrid = $(selektorDivgride3).width() != null && $(selektorDivgride3).width() != null ? $(selektorDivgride3) : $(selektorDivgride2);
    myJQGrid.fixGridWidth(grid, selDivGrid, subgrid);
    grid.jqGrid('setColProp', colName, { fixed: false });
    return;
};

myJQGrid.loadComplete = function (emergride, arrayRenditjeKolonaGridesName, subgrid, niveli) {
    if (typeof (window[arrayRenditjeKolonaGridesName]) != "undefined")
        if (subgrid && $(emergride).jqGrid('getGridParam', 'colModel').length != window[arrayRenditjeKolonaGridesName].length) {
            $.each(window[arrayRenditjeKolonaGridesName], function (index, vlere) {
                window[arrayRenditjeKolonaGridesName][index] = vlere + 1;
            });
            window[arrayRenditjeKolonaGridesName].unshift(0);
        }
    myJQGrid.renditKolonatEGrides($(emergride), window[arrayRenditjeKolonaGridesName], niveli);
};

myJQGrid.afterInsertRow = function (rowid, rowdata, rowelem, emergride) {
    $(emergride).updateScroll();
    $.each(rowelem, function (key, value) {
        if (!isNaN(parseFloat(value)))
            $(emergride).setTekstQelize(key, rowid, value);
    });
    var maxLastSel2 = $(emergride).getMaxLastSel();
    if (maxLastSel2 < parseInt(rowid)) {
        $(emergride).setMaxLastSel(parseInt(rowid));
    }
};

myJQGrid.shtoToolbarGride = function (emergride, selektorDivgride3, selektorDivgride2, hfState, konfigToolbar, niveli, emerEditorKodi, lang, shtoNrRreshta) {
    $(emergride).jqGrid('navGrid', emergride + '_toppager', { edit: false, add: false, del: false, search: false, cloneToTop: true, refresh: false });
    $(emergride).initNrReshta(konfigToolbar.identifikuesNrRreshta, emergride);
    if (!shtoNrRreshta)
        $(".clsBoxNrRreshtashTeShfaqura").hide()
    if (konfigToolbar.konfigGrid && konfigToolbar.konfigGrid == 'True') {
        shtoKonfigGride(emergride, selektorDivgride3, selektorDivgride2, hfState, konfigToolbar.ruajKolonatEGrides, niveli,lang);
    }
    if (konfigToolbar.shtoArtikull && konfigToolbar.shtoArtikull == 'True') {
        shtoShtoArtikull(emergride, hfState, konfigToolbar.hapPopUpRi);
    }
    if (konfigToolbar.modArtikull != undefined && konfigToolbar.modArtikull == 'True') {
        shtoModArtikull(emergride, hfState, konfigToolbar.hapPopUpModifikoArt);
    }
    if (konfigToolbar.infoArtikullPerberes != undefined && konfigToolbar.infoArtikullPerberes == 'True') {
        shtoArtInfoArtPerb(emergride, hfState, konfigToolbar.hapPopUpInfoArtPerberes);
    }
    if (konfigToolbar.infoGrupimPerberes != undefined && konfigToolbar.infoGrupimPerberes == 'True') {
        shtoInfoGrupimPerberes(emergride, hfState, konfigToolbar.hapPopUpinfoGrupimPerberes);
    }
    if (konfigToolbar.shtoKf != undefined && konfigToolbar.shtoKf == 'True') {
        shtoKf(emergride, hfState, konfigToolbar.hapPopUpRi);
    }
    if (konfigToolbar.exportExcel) {
        exportoNeExcel(emergride, hfState, konfigToolbar.EmerExporti, konfigToolbar.FormateText, emerEditorKodi);
    }
    if (konfigToolbar.kaTeDrejtaArkive != undefined && konfigToolbar.kaTeDrejtaArkive == 'True') {
        shtoImazheArkive(emergride, hfState, konfigToolbar.hapPopUpImazheArkive);
        shtoLupeArkive(emergride, hfState, konfigToolbar.hapPopUpArkive);
    }
    if (konfigToolbar.importoSeriale != undefined && konfigToolbar.importoSeriale == 'True')
        shtoNgarkimSerialesh(emergride, hfState, konfigToolbar.hapNgarkimSerialesh);
    
   
    function shtoKonfigGride(emergride, selektorDivgride3, selektorDivgride2, hfState, ruajKolonatEGrides, niveli, lang) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",// "Zgjidh kolonat",
            title: hfState.Get("JQgridZgjidhKolona"),
            buttonicon: 'ui-icon-wrench',
            onClickButton: function () {
                $(emergride).jqGrid('columnChooser', {
                    done: function (perm) {
                        if (!perm) { return false; }
                        
                        this.jqGrid('remapColumns', perm, true);
                        myJQGrid.resizeGrid(emergride, selektorDivgride3, selektorDivgride2, niveli);
                    }
                });
                if (lang == 0) {
                    $('.ui-dialog-title').text(hfState.Get("JQgridZgjidhKolona"));
                   $('.remove-all').text('Hiqi te gjithe');
                   $('.add-all').text("Shto te gjithe");
                   $('.search ').css("width","70px");
                   $('.count').text($('.count').text().replace('items selected', 'te zgjedhur'));
                   $('.ui-dialog-buttonset').children().children('.ui-button-text')[0].innerHTML = "Ok";
                   $('.ui-dialog-buttonset').children().children('.ui-button-text')[1].innerHTML = "Anullo";
             
                }
            },
            position: "last"
           
        });
       
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",//"Ruaj Kolonat",
            title: hfState.Get("JQgridRuajKolona"),
            buttonicon: 'ui-icon-disk',
            onClickButton: function () {
                ruajKolonatEGrides($(emergride));
            },
            position: "last"
        });
    };

    function exportoNeExcel(emergride, hfState, EmerExporti, FormateText, emerEditorKodi) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",//"Exporto ne Excel",
            title: hfState.Get("JQgridExport"),
            buttonicon: 'ui-icon-arrowthickstop-1-s',
            onClickButton: function () {
                var htmlTable = $(emergride).pergatitGridePerExport(FormateText, emerEditorKodi);
                myJQGrid.krijoExportExcelPerGride(htmlTable, EmerExporti);
            },
            position: "last"
        });
    };
    function shtoShtoArtikull(emergride, hfState, hapPopUpRi) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",//"Shto Artikull",
            buttonicon: 'ui-icon-circle-plus',
            title: hfState.Get("JQgridShtoArtikull"),        /////"Shto artikull",
            onClickButton: function () {
                hapPopUpRi(false);
            },
            position: "last"
        });
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",//"Shto Artikull",
            buttonicon: 'ui-icon-plusthick',
            title: hfState.Get("JQgridShtoArtikullAqt"),                                      //// "Shto artikull afatgjate",
            onClickButton: function () {
                hapPopUpRi(true);
            },
            position: "last"
        });
    };
    function shtoModArtikull(emergride, hfState, hapPopUpModifikoArt) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            //  hfState.Get("JQgridModifikoArtikull"),// "Modifiko Artikull",
            buttonicon: 'ui-icon-pencil',
            title: hfState.Get("JQgridModifikoArtikull"),
            onClickButton: function () {
                hapPopUpModifikoArt(false);
            },
            position: "last"
        });
    };
    function shtoArtInfoArtPerb(emergride, hfState, hapPopUpInfoArtPerberes) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            title: hfState.Get("JQgridShikoPerberesit"),
            buttonicon: 'ui-icon-zoomin',
            onClickButton: function () {
                hapPopUpInfoArtPerberes();
            },
            position: "last"
        });
    };
    function shtoInfoGrupimPerberes(emergride, hfState, hapPopUpinfoGrupimPerberes) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            //  hfState.Get("JQgridShikoPerberesit"),
            title: hfState.Get("JQgridGrupimPerberesit"),
            buttonicon: 'ui-icon-zoomin',
            onClickButton: function () {
                hapPopUpinfoGrupimPerberes();
            },
            position: "last"
        });
    };        
    function shtoKf(emergride, hfState, hapPopUpRi) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",// "Shto Klient",
            title: hfState.Get("JQgridShtoKlient"),
            buttonicon: 'ui-icon-circle-plus',
            onClickButton: function () {
                hapPopUpRi('klient');
            },
            position: "last"
        });
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",// "Shto Furnitor",
            title: hfState.Get("JQgridShtoFurnitor"),
            buttonicon: 'ui-icon-circle-plus',
            onClickButton: function () {
                hapPopUpRi('furnitor');
            },
            position: "last"
        });
    };
    function shtoImazheArkive(emergride, hfState, onButtonClick) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            title: hfState.Get("JQgridImazhe"),
            buttonicon: 'ui-icon-image',
            onClickButton: function () {
                onButtonClick();
            },
            position: "last"
        });
    };
    function shtoLupeArkive(emergride, hfState, onButtonClick) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            title: "Arkiva",//hfState.Get("JQgridImazhe"),
            buttonicon: 'ui-icon-folder-open',
            onClickButton: function () {
                onButtonClick();
            },
            position: "last"
        });
    };
    function shtoNgarkimSerialesh(emergride, hfState, onButtonClick) {
        $(emergride).jqGrid('navButtonAdd', emergride + '_toppager', { // "#list_toppager_left"
            caption: "",
            title: "Ngarko Seriale",//hfState.Get("JQgridImazhe"),
            buttonicon: 'ui-icon-arrowthickstop-1-n',
            onClickButton: function () {
                onButtonClick();
            },
            position: "last"
        });
    };
};

myJQGrid.subGridRowExpanded = function (subgrid_id, row_id, inicializoSubGride, mbushSubGridenRreshtit, lastselgrid, afterSave, emergride, widthi, niveli, lloji, hfState) {
    if (lastselgrid != "") {
        var idrreshti = $("#" + lastselgrid).getLastSel2();
        if ($("#" + lastselgrid).length > 0) {
            $("#" + lastselgrid).jqGrid('saveRow', idrreshti, null, 'clientArray', {}, afterSave);
        }
        var pershkrimi = $(emergride).getTekstQelize('txtPershkrimi', row_id, subgrid_id + row_id);
        hfState.Set('lastprindi', pershkrimi);

    }
    var subgrid_table_id;
    subgrid_table_id = subgrid_id + "_t";
    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table>");
    if (lastselgrid != undefined && lastselgrid != "") {
        inicializoSubGride(subgrid_table_id, widthi - 25, parseInt(niveli) + 1, lloji);
        mbushSubGridenRreshtit(pershkrimi, parseInt(niveli) + 1, subgrid_table_id, widthi - 100, lloji);
        return;
    }
    inicializoSubGride(subgrid_table_id);
    mbushSubGridenRreshtit(row_id);
};
myJQGrid.isValidTransferimMagazine = function (emerMag1, emerMag2, cmbKonfigurimi, myMesazh) {
    if (!emerMag1 || !emerMag2 || !$("select[id$='" + emerMag1 + "']")[0] || cmbKonfigurimi.GetText() != "FDT")
        return true;
    mag1 = $("select[id$='" + emerMag1 + "']")[0][$("select[id$='" + emerMag1 + "']")[0].selectedIndex].text;
    mag2 = $("select[id$='" + emerMag2 + "']")[0][$("select[id$='" + emerMag2 + "']")[0].selectedIndex].text;
    if (mag1 == mag2 && mag1 != "") {
        myMesazh.ShtoMesazhGabimi('Nuk mund te beni transferim ne te njejten magazine');
        return false;
    }
    return true;
}

myJQGrid.initGride = function (params) {
   var defaults = {
       emergride: "#rowed5", emerEditorKodi: "", emerEditorLloji: null, emerEditorCmimi: "", cmimzero: null, emerMag1: null, emerMag2: null, subgrid: undefined, fokus: undefined, afterSaveFunc: null, doubleclickfunction: null, mosshtorresht: undefined, emerEditorMagazina: undefined, emerEditorMagazina2: undefined, arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaGrides", arrayReadOnlyKolonaGrides: arrayReadOnlyKolonaGrides, hfState: hfState, mbushSubGridenRreshtit: undefined, inicializoSubGride: undefined, kontrolloCmiminArtSipasKushtit: undefined, resetRreshtKorent: undefined, enable: undefined, lostFocusKoloneFundit: lostFocusKoloneFundit, cmbKonfigurimi: undefined, caption: "", niveli: 1, lloji: "", widthi: 100, selektorDivgride2: "#divgride2", selektorDivgride3: "#divgride3", hidegrid: false, autocompleteList: [{
           emerEditor: undefined, selectFunc: undefined, changeFunc: undefined, shtoDataKod: false,
           readOnly: false
       }
        ], konfigToolbar:
        { identifikuesNrRreshta: "", 
        ruajKolonatEGrides: undefined,
        hapPopUpRi: undefined,
        hapPopUpModifikoArt: undefined,
        hapPopUpInfoArtPerberes: undefined,
        hapPopUpinfoGrupimPerberes: undefined,
        hapPopUpImazheArkive: undefined,
        hapPopUpArkive: undefined,
        shtoArtikull: undefined,
        shtoKf: undefined,
        modArtikull: undefined,
        infoArtikullPerberes: undefined,
        infoGrupimPerberes: undefined,
        kaTeDrejtaArkive: false,
        exportExcel: false,
        EmerExporti: "RaportGride",
        importoSeriale: undefined}
    };
    params = $.extend({}, defaults, params);
    var selektorKodi = "#" + params.emerEditorKodi;
    var opsioneGride = {
        datatype: "local",
        colNames: params.arrayPershkrime,
        colModel: params.arrayModel,
        caption: (params.caption == 'Niveli ') ? params.caption + params.niveli : params.caption,
        hidegrid: params.hidegrid,
        sortable: false,
        cellsubmit: 'clientArray',
        //scrollrows: false,
        rowNum: 100000,
        height: 'auto',//height == 0 ? 'auto': height.toString(),
        //rownumbers: true,
        width: params.widthi,
        toppager: true,
        pgbuttons: false,
        pginput: false,
        //pagerpos: "right",--hap subgridat e medha per raportet financiare
        recordpos: "right",
        shrinkToFit: true,
        recreateForm: true,
        multiselect: false,
        
        resizeStart: function (even, index) {
            myJQGrid.resizeStart(even, index, params.emergride);
        },
        resizeStop: function (newWidth, index) {
            myJQGrid.resizeStop(newWidth, index, params.emergride, params.selektorDivgride3, params.selektorDivgride2, params.subgrid);
        },
        loadComplete: function () {
            myJQGrid.loadComplete(params.emergride, params.arrayRenditjeKolonaGridesName, params.subgrid, params.niveli);
        },
        beforeSelectRow: function (id, e) {
            if (params.caption == '')
                return true;
            else
                return myJQGrid.beforeSelectRow(id, e, params.afterSaveFunc, params.niveli, params.emergride, params.caption);
        },
        onSelectRow: function (rowid, status, e) {
            myJQGrid.onSelectRow(rowid, status, e, params, selektorKodi, $(this));
        },
        afterInsertRow: function (rowid, rowdata, rowelem) {
            if (params.caption != "")
                myJQGrid.afterInsertRow(params.emergride.substring(1, params.emergride.length) + rowid, rowdata, rowelem, params.emergride);
            else
                myJQGrid.afterInsertRow(rowid, rowdata, rowelem, params.emergride);
        },
        subGrid: params.subgrid,
        subGridRowExpanded: function (subgrid_id, row_id) {
            myJQGrid.subGridRowExpanded(subgrid_id, row_id, params.inicializoSubGride, params.mbushSubGridenRreshtit, hfState.Get('lastselgrid'), params.afterSaveFunc, params.emergride, params.widthi, params.niveli, params.lloji, hfState);
        },
        subGridRowColapsed: function (subgrid_id, row_id) {
            if (params.caption == '') //pash case
                return;
            var idRreshti = $("#" + hfState.Get('lastselgrid')).getLastSel2();
            $("#" + hfState.Get('lastselgrid')).jqGrid('saveRow', idRreshti, null, 'clientArray', {}, params.afterSaveFunc);
            $("#" + hfState.Get('lastselgrid')).setLastSel2(1);
            hfState.Set('lastselgrid', "ASPxPageControl1_rowed5");
        },
        ondblClickRow: function (rowid, iRow, iCol, e) {
            if (params.doubleclickfunction && params.doubleclickfunction !== null)
                params.doubleclickfunction(rowid);
        }
    }
    if (params.readOnly) {
        opsioneGride.pager = '#pager';
        opsioneGride.rowNum = 10;
        opsioneGride.rowList = [5, 10, 20, 50];
        opsioneGride.viewrecords = true;
        opsioneGride.data = params.data;
        opsioneGride.scroll = 1;
        opsioneGride.loadonce = true;
    }
    $(params.emergride).jqGrid(opsioneGride);
    myJQGrid.shtoToolbarGride(params.emergride, params.selektorDivgride3, params.selektorDivgride2, params.hfState, params.konfigToolbar, params.niveli, params.emerEditorKodi, params.idGjuha, !params.readOnly);
    myJQGrid.resizeGrid(params.emergride, params.selektorDivgride3, params.selektorDivgride2, params.niveli);
    $(params.emergride).shtoRreshtinEpare(params.arrayReadOnlyKolonaGrides, params.lostFocusKoloneFundit, params.emergride);
    myJQGrid.setGridParams(params.emergride, params);
    return $(params.emergride);
};

myJQGrid.closeAutocomplete = function (emerKodi, index) {
    if ($('#' + emerKodi + index).val() !== undefined)
        $('#' + emerKodi + index).autocomplete("close");
};

myJQGrid.ruajKolonatEGrides = function (grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi) {
    var gridColumns = [];
    var indexSubgride = 0;
    $.each(grida.jqGrid('getGridParam', 'colModel'), function (indexi, opsioneKolone) {
        if (opsioneKolone.name == "subgrid")
            indexSubgride++;
        else {
            gridColumns.push({ KodiTrupi: opsioneKolone.name, WidthTrupi: opsioneKolone.width, IndexTrupi: indexi - indexSubgride, VisibleTrupi: !opsioneKolone.hidden });
            var oldIndex = $.inArray(opsioneKolone.name, arrayIdKolonaGrides);
            if (oldIndex != -1) {
                arrayRenditjeKolonaGrides[oldIndex] = indexi - indexSubgride;
                arrayWidthKolonaGrides[oldIndex] = opsioneKolone.width;
                arrayVisibleKolonaGrides[oldIndex] = !opsioneKolone.hidden;
            }
        }
    });
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ruajKolonaGride"),
        data: JSON.stringify({ idGride: idGride, gridColumns: gridColumns, idGjuha: idGjuha, idNdermarrje: idNdermarrje, idViti: idViti, idPerdoruesi: idPerdoruesi })
    }).done(myJQGrid.SuccedcallbackRuajKolonaGride);
};

myJQGrid.SuccedcallbackRuajKolonaGride = function (result) {
    myMesazh.ShtoMesazhSesioni(result);
};

myJQGrid.renditKolonatEGrides = function (grida, arrayPerm, niveli) {//po
    if (!arrayPerm) { return false; }
    if (arrayPerm.length > 0) {
        var perm = myJQGrid.getRemapArray(arrayPerm);
        grida.jqGrid('remapColumns', perm, true);
        myJQGrid.fixGridWidth(grida, $('#divgride2'), niveli);
    }
}

myJQGrid.getRemapArray = function (arrayPerm) {
    var remapArray = new Array(arrayPerm.length);
    for (i = 0; i < arrayPerm.length; i++) {
        remapArray[arrayPerm[i]] = i
    }
    return remapArray;
};

function split(val) {
    return val.split(/,\s*/);
};

function extractLast(term) {
    return split(term).pop();
};
var tmpVal = [];

/*
event = eventi i kontrollit qe e ka shkaktuar
idKod = 'txtKodi'
*/
myJQGrid.getIndexFromEvent = function (event, idkod) {
    if (!event) {
        console.error("myJQGrid.getIndexFromEvent(" + event + ", " + idkod + ") -- !event perdor lastsel2");
        return lastsel2;
    }
    return event.target.id.split(idkod)[1];
};

//myJQGrid.fixGridWidth = function (grid, mainDiv, niveli) {
//    if (!niveli)
//        niveli = 1;
//    grid.jqGrid('setGridWidth', mainDiv.width() - (27 * niveli), true);
//};
myJQGrid.fixGridWidth = function (grid, mainDiv, niveli) {
    if (!niveli)
        niveli = 1;
    var myAccordion = $("#accordition");
    if (myAccordion.width() != 0 || mainDiv.width() != 0) {
        var widthToSet = ((myAccordion.width() == null ? mainDiv.width() : myAccordion.width()) - 30 - (30 * (niveli - 1)));
        if (widthToSet <= 0)
            return;
        //console.log("SetWidth: " + widthToSet+"; niveli: "+niveli);
        grid.jqGrid('setGridWidth', widthToSet, true);
    }
};

// ne elementin e fundit siper grides duhet vene ky clientsideevent
// LostFocus="function(s,e){myJQGrid.focusGrid('#rowed5');}" 
//params shembull: {emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]}
myJQGrid.focusGrid = function (params) {
    var defaults = { idRreshti: 1 };
    params = $.extend({}, defaults, params);
    if (params.lidhur)
        return;
    $(params.emergride).jqGrid('setSelection', params.idRreshti, true);
    $(params.emergride).jqGrid('editRow', params.idRreshti, false);
    $("#" + params.idKoloneGrideFokus + params.idRreshti).focus();
};

myJQGrid.WarnGjendje = function (minArt, maxArt, sasia, magazina, kodart, hyrjeOseBlerje) {
    if (minArt != 0.00 && parseFloat(sasia) < parseFloat(minArt)) {
        if (magazina != '')
            myMesazh.ShtoMesazhGabimi('Kujdes! Sasia minimale e artikullit me kod ' + kodart + ' ne magazinen me kod ' + magazina + ' eshte ' + minArt);
        else
            myMesazh.ShtoMesazhGabimi('Kujdes! Sasia minimale e pergjithshme e artikullit me kod ' + kodart + ' eshte ' + minArt);
    }
    if (maxArt != 0.00 && parseFloat(sasia) > parseFloat(maxArt)) {
        if (hyrjeOseBlerje) {
            if (magazina != '')
                myMesazh.ShtoMesazhGabimi('Kujdes! Sasia maksimale e artikullit me kod ' + kodart + ' ne magazinen me kod ' + magazina + ' eshte ' + maxArt);
            else
                myMesazh.ShtoMesazhGabimi('Kujdes! Sasia maksimale e pergjithshme e artikullit me kod ' + kodart + ' eshte ' + maxArt);
        }
    }
        
};

myJQGrid.Fmatter2To3 = function (cellvalue, options, rowObject) {

    if (cellvalue !== undefined && cellvalue !== "") {
        cellvalue = parseFloat(cellvalue);
        cellvalue = Math.round(cellvalue * 1000) / 1000;
        var new_format_value = cellvalue.toString().split('.')[0];
        if (cellvalue.toString().split('.').length === 1) {
            new_format_value = new_format_value + ".00"; return new_format_value;
        }
        var decimal = cellvalue.toString().split('.')[1];
        if (decimal.length === 1) {
            new_format_value = new_format_value + "." + decimal + "0";
            return new_format_value;
        }
        new_format_value = new_format_value + "." + decimal;
        return new_format_value;
    }
    else return "";
};

myJQGrid.Fmatter2To6 = function (cellvalue, options, rowObject) {
    if (cellvalue !== undefined && cellvalue !== "") {
        cellvalue = parseFloat(cellvalue);
        cellvalue = Math.round(cellvalue * 1000000) / 1000000;
        var new_format_value = cellvalue.toString().split('.')[0];
        if (cellvalue.toString().split('.').length === 1) {
            return new_format_value + ".00";
        }
        var decimal = cellvalue.toString().split('.')[1];
        if (decimal.length === 1) {
            return new_format_value + "." + decimal + "0";
        }
        return new_format_value + "." + decimal;
    }
    else return "";
};

myJQGrid.kursiFmatter = function (cellvalue, options, rowObject) {

    if (cellvalue !== undefined && cellvalue !== "") {
        cellvalue = parseFloat(cellvalue);
        cellvalue = Math.round(cellvalue * 1000000) / 1000000;
        var new_format_value = cellvalue.toString().split('.')[0];
        if (cellvalue.toString().split('.').length === 1) {
            return new_format_value + ".00";
        }
        var decimal = cellvalue.toString().split('.')[1];
        if (decimal.length === 1) {
            return new_format_value + "." + decimal + "0";
        }
        return new_format_value + "." + decimal;
    }
    else return "";
};

myJQGrid.formArrayKolGrides = function (HfGridCol, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur,
     arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides) {
    if (HfGridCol.val() == undefined || HfGridCol.val() == "")
        return;
    var colTrupiGrida = $.parseJSON(HfGridCol.val());
    myJQGrid.formArrayKolGridesNew(colTrupiGrida, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur,
         arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
};
myJQGrid.formArrayKolGridesNew = function (colTrupiGrida, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur,
     arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides) {
    if (colTrupiGrida == undefined || colTrupiGrida == "" )
        return;
    for (var i = 0; i < (colTrupiGrida.length) ; i++) {
        var indexGrid = colTrupiGrida[i].IndexTrupiOrigjinal;
        arrayRenditjeKolonaGrides[indexGrid] = colTrupiGrida[i].IndexTrupi;
        arrayIdKolonaGrides[indexGrid] = colTrupiGrida[i].KodiTrupi;
        arrayPershkrimiKolonaGrides[indexGrid] = colTrupiGrida[i].PershkrimiTrupi;
        arrayWidthKolonaGrides[indexGrid] = colTrupiGrida[i].WidthTrupi;
        if (colTrupiGrida[i].VisibleTrupi == true)
            arrayVisibleKolonaGrides[indexGrid] = false;
        else
            arrayVisibleKolonaGrides[indexGrid] = true;
        if (lidhur == true)
            arrayReadOnlyKolonaGrides[indexGrid] = 'True';
        else
            if (colTrupiGrida[i].ReadonlyTrupi === true)
                arrayReadOnlyKolonaGrides[indexGrid] = 'True'
            else
                arrayReadOnlyKolonaGrides[indexGrid] = 'False';
        for (var j = 0; j < IdKonfigAmbjenteLupat.length; j++) {
            if (colTrupiGrida[i].KodiTrupi == IdKonfigAmbjenteLupat[j].kodiText)
                IdKonfigAmbjenteLupat[j].hfVar.val(colTrupiGrida[i].IdKonfigLupaMultiple);
        }
    }
};

myJQGrid.renditKolonatGrides = function (emergride, arrayRenditjeKolonaGrides) {
    $(emergride).jqGrid('remapColumns', arrayRenditjeKolonaGrides, true);
};

myJQGrid.keyPressKodi = function (grida, idRreshti, disabled, emergride) {
    if (grida.jqGrid('getInd', idRreshti) == grida.jqGrid('getGridParam', 'reccount')) {
        grida.shtoRresht(disabled, emergride);
    }
};

//myJQGrid.keyPressKodiR = function (emergride, lastsel3, disabled) {
//    if (jQuery(emergride).jqGrid('getInd', lastsel3) == jQuery(emergride).jqGrid('getGridParam', 'reccount')) {
//        var index2 = parseInt(lastsel3) + 1;
//        if (disabled == 'True')
//            be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png'  onclick='fshiClicked2(" + index2 + ")'/>";

//        else
//            be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + index2 + ")'/>";

//        var datarow = { txtFshiR: be };
//        var su = $(emergride).jqGrid('addRowData', parseInt(lastsel3) + 1, datarow);
//    }
//};

var changedTimer, changedTimerIndex;
myJQGrid.cancelQuickChange = function (params) {//event, ui, emerKodi, changeFunc
    var event = params.event, ui = params.ui, emerKodi = params.emerKodi, changeFunc = params.changeFunc, index = params.idRreshti;
    if (changedTimerIndex == index) {
        clearTimeout(changedTimer);
    }
    changedTimerIndex = index;
    changedTimer = window.setTimeout(function () {
        changeFunc(event, ui, emerKodi, index);
        changedTimerIndex = -1;
        if (Utils.nrWsRrugesManager.kanePerfunduarWs() && Utils.KaFunksionPending())
            Utils.execFunksionNeRadhe();
    }, 400);
};

var changedTimerSasi, changedTimerIndexSasi;
myJQGrid.cancelQuickChangeSasi = function (emerKodi, kodi, sasia, njesia, changeFunc) {
    var index = -1;
    var idKod = 'txtSasia';
    if (emerKodi === undefined || emerKodi === null)
        index = myJQGrid.getIndexFromEvent(event, idKod);
    else
        index = emerKodi.split(idKod)[1];
    if (changedTimerIndexSasi == index) {
        clearTimeout(changedTimerSasi);
    }
    changedTimerIndexSasi = index;
    changedTimerSasi = window.setTimeout(function () {
        changeFunc(kodi, sasia, index, njesia);
        changedTimerIndexSasi = -1;
        if (Utils.nrWsRrugesManager.kanePerfunduarWs() && Utils.KaFunksionPending())
            Utils.execFunksionNeRadhe();
    }, 200);
};


myJQGrid.cancelQuickChangeSasiAktualeEP = function (index, changeFunc) {
    if (changedTimerIndexSasi == index) {
        clearTimeout(changedTimerSasi);
    }
    changedTimerIndexSasi = index;
    changedTimerSasi = window.setTimeout(function () {
        changeFunc();
        changedTimerIndexSasi = -1;
    }, 400);
};

myJQGrid.checkIsPageReadyToSave = function (menuItemName) {
    if (menuItemName === 'Ruaj' || menuItemName === 'Draft') { //pritja behet vetem per ruajtje dhe per draft
        if ((changedTimerIndex !== undefined && changedTimerIndex !== null && changedTimerIndex !== -1) //pritet nese ka event change per te ndodhur
            || (changedTimerIndexSasi !== undefined && changedTimerIndexSasi !== null && changedTimerIndexSasi !== -1) //pritet nese ka event change te sasia per te ndodhur
            || !Utils.nrWsRrugesManager.kanePerfunduarWs()) //pritet nese ka akoma webservice pa u kthyer
            return false;
    }
    return true;
};

//myJQGrid.pritTimer = function (toCall, menuItemName) {
//    if (menuItemName === 'Ruaj' || menuItemName === 'Draft') { //pritja behet vetem per ruajtje dhe per draft
//        if ((changedTimerIndex !== undefined && changedTimerIndex !== null && changedTimerIndex !== -1) //pritet nese ka event change per te ndodhur
//            || (changedTimerIndexSasi !== undefined && changedTimerIndexSasi !== null && changedTimerIndexSasi !== -1) //pritet nese ka event change te sasia per te ndodhur
//            || !Utils.nrWsRrugesManager.kanePerfunduarWs()) { //pritet nese ka akoma webservice pa u kthyer           
//            if (!this.prisniPakSekonda) {
//                this.prisniPakSekonda = myMesazh.ShtoMesazh({ type: "alert", text: 'Ju lutem prisni pak sekonda...', timeout: false });
//            }
//            window.setTimeout(function () {
//                e = {};
//                e.item = {};
//                e.item.name = menuItemName;
//                e.processOnServer = true;
//                var sender = 'tastiera';
//                toCall(sender, e);
//            }, 201);
//            return true;
//        }
//    }
//    if (this.prisniPakSekonda) {
//        this.prisniPakSekonda.close();
//        this.prisniPakSekonda = 0;
//    }
//    return false;
//};
myJQGrid.setFokus = function (fokusi, lostFocusKoloneFundit, idRreshti, sasiaSelektor, sasiaSelektorRe) {
    if (typeof fokusi != "undefined") {
        switch (fokusi) {
            case 2:
                lostFocusKoloneFundit();
                break;
            case 1:
                $(sasiaSelektor + idRreshti).focus();
                try { $(sasiaSelektorRe + idRreshti).focus(); }
                catch (ee) {
                }
                break;
            default:
                break;
        }
    }
};
myJQGrid.myElemKodi = function (value, option, disabled, idRreshti, id, buttonClickKodi, keyUpKodi, changefunction, fokusi, lostFocusKoloneFundit) {
    var el3 = $('<div></div>');
    el3.addClass("imb-jqgrid-buttonEdit");
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);
    textField.val(value);
    textField.on("keyup", function (event) {
        if (event.which == 13) { //enter
            // event.preventDefault();
            if (id == 'txtSerial') {
                keyUpKodi(event, idRreshti);
                return false;
            }

            if (changefunction)
                changefunction(event, null, id, idRreshti);
                //myJQGrid.cancelQuickChange({ event: event, ui: null, emerKodi: id, changeFunc: changefunction, idRreshti: idRreshti, timeouti: 0 }); //changeFunc(event, null, null);
            else
                console.error("changefunction mungon tek myelemkodi");
            myJQGrid.setFokus(fokusi, lostFocusKoloneFundit, idRreshti, '#txtSasia', '#txtSasiaRe');
            return false;
        }
        if (event.which == 32 || event.which == 8 || event.which == 46 || event.which > 45) {
            if ($('#' + id + idRreshti).val() != "" && event.which != 37 && event.which != 38)
                myJQGrid.cancelQuickKeyUp(keyUpKodi, event, idRreshti, id); //keyUpKodi(event);
        }
        if (id == 'txtSerial')
            return;
        if (event.target.value == "") {
            $('#' + id + idRreshti).autocomplete('close');
            return;
        }
    });
    textField.on("focusout", function (event) {
        if (changefunction)
            myJQGrid.cancelQuickChange({ event: event, ui: null, emerKodi: id, changeFunc: changefunction, idRreshti: idRreshti }); //changeFunc(event, null, null);
        else
            console.log("changefunction mungon tek myelemkodi");
    });
    if (disabled == 'True' || disabled === true)
        textField.attr("disabled", "disabled");
    textField.appendTo(el3);
    var button = $('<button />');
    if (disabled == 'True' || disabled === true)
        button.attr("disabled", "disabled");
    button.attr("name", id + idRreshti);
    button.addClass("ikon-lupe");
    button.on("click", function (event) { buttonClickKodi(event); return false; });
    button.appendTo(el3);
    return el3;
};

myJQGrid.myElemCmimiAutocomplete = function (params) {
    var value = params.value, options = params.options, disabled = params.disabled, idRreshti = params.idRreshti, id = params.id, buttonClickKodi = params.buttonClickKodi, keyUpKodi = params.keyUpKodi, changefunction = params.changefunction, fokusi = params.fokusi, lostFocusKoloneFundit = params.lostFocusKoloneFundit, shitje = params.shitje,
            hfFormatNumri = params.hfFormatNumri;

    var el3 = $('<div></div>');
    el3.addClass("imb-jqgrid-buttonEdit");
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);
    var grida = $('#rowed5');
    if (value == "" || value == "NaN" || value == undefined) {
        value = grida.getVlereDefault(id);
    }
    var realValue = grida.getVlereReale(textField.attr('id'));
    if (realValue != null && typeof realValue != 'undefined')
        value = realValue;
    textField.val(value);
    textField.on("keyup focusout", function (event) {
        if (event.which == 13 && fokusi && fokusi == 2) {
            lostFocusKoloneFundit();
        }
        if ((event.which == 32 || event.which == 8 || event.which >= 45) && $('#' + id + idRreshti).val() != "")
            myJQGrid.cancelQuickKeyUp(keyUpKodi, event, idRreshti, id);
        if (changefunction)
            myJQGrid.cancelQuickChange({ event: event, ui: null, emerKodi: id, changeFunc: changefunction, idRreshti: idRreshti });
        else
            console.log("changefunction eshte " + changefunction + "tek myElemCmimiAutocomplete");
        if (event.target.value == "") {
            $('#' + id + idRreshti).autocomplete('close');
            return;
        }
    });
    el3.on("focusout", function (event) {
        if (event.target.value == "") {
            $('#' + id + idRreshti).autocomplete('close');
            return;
        }
    });
    textField.on("focusout", function (event) {
        if (this.value == "") {
            var grida = $('#rowed5');
            textField.val(grida.getVlereDefault(this.id));
        }
    });
    if (disabled == 'True' || disabled === true)
        textField.attr("disabled", "disabled");
    textField.appendTo(el3);
    var button = $('<button />');
    if (disabled == 'True' || disabled === true)
        button.attr("disabled", "disabled");
    button.attr("name", id + idRreshti);
    button.addClass("ikon-drop");
    //button.click(function (e) { e.preventDefault() });
    button.click(function () { buttonClickKodi(id); });
    button.appendTo(el3);
    return el3;
};

var keyUpTimer;
var keyUpTimerId;

myJQGrid.cancelQuickKeyUp = function (keyup, event, idRreshti, id) {
    var idKod = event.target.id;
    if (keyUpTimerId == idKod) {
        clearTimeout(keyUpTimer);
    }
    keyUpTimerId = idKod;
    keyUpTimer = window.setTimeout(function () {
        keyup(event, idRreshti, id);
    }, 200);
};

myJQGrid.myElemNrPersonal = function (value, option, disabled, idRreshti, id, buttonClickKodi, disabledbuton) {
    var el3 = $('<div></div>');
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);
    textField.val(value);
    textField.width('60%');
    textField.keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            event.preventDefault();
            $('#' + id + idRreshti).blur();
        }
    });

    if (disabled == 'True' || disabled === true)
        textField.attr("disabled", "disabled");
    textField.appendTo(el3);
    var button = $('<input type="button" />');
    button.val('...');
    if (disabledbuton == 'True' || disabled === true)
        button.attr("disabled", "disabled");
    button.attr("name", "button1");
    button.click(buttonClickKodi);
    button.width('18%');
    button.appendTo(el3);
    return el3;
};

myJQGrid.myElemIdKodi = function (value, option, disabled, idRreshti, id) {
    var el3 = $('<div></div>');
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);
    textField.val(value);
    textField.attr("disabled", "disabled");
    textField.appendTo(el3);
    return el3;
};

myJQGrid.myElemButtonFshi = function (disabled, lastsel, emergride, onblur, raporte) {
    //var buttonField = $('<input type="image" />');
    //if (raporte)
    //    buttonField.attr('id', 'butonFshi' + emergride + lastsel);
    //buttonField.attr('id', 'butonFshi' + lastsel);
    //buttonField.val('Fshi');
    //buttonField.attr('src', 'images/square-icon.png');
    //if (disabled)
    //    buttonField.attr("disabled", "disabled");
    //if (onblur !== undefined)
    //    buttonField.focusout(function () { onblur(); });
    //buttonField.mouseover(function () { myJQGrid.ndryshoImazhin(1, lastsel); });
    //buttonField.mouseout(function () { myJQGrid.ndryshoImazhin(0, lastsel); });
    //buttonField.click(function (e) { (emergride == "#rowed6" ? fshiClicked2(lastsel.toString(), e) :(emergride == "#rowed7" ? fshiClicked3(lastsel.toString(), e) : raporte ? fshiClicked(emergride, lastsel.toString(), e) : fshiClicked(lastsel.toString(), e))); });
    //return buttonField;

    return "<input id='butonFshi" + ((raporte ? emergride : "") + lastsel) + "' type='image' value='Fshi' " + (disabled ? " disabled='disabled' " : " ") + (onblur !== undefined ? " onfocusout='" + onblur.name + "()' " : " ") + "  onmouseover='ndryshoImazhin(1," + lastsel + ")' onmouseout='ndryshoImazhin(0," + lastsel + ")'  src='images/square-icon.png'  onclick='fshiClicked" + (emergride == "#rowed6" ? "2" : (emergride == "#rowed7" ? "3" : "")) + "(" + (raporte ? ("&quot;" + emergride + "&quot;" + ',') : '') + lastsel + ",event)'/>";
};

myJQGrid.ndryshoImazhin = function (nr, index) {
    var id = "butonFshi" + index;
    var buttoni = $('#' + id);
    if (buttoni.val() != "") {
        if (nr == 1)
            buttoni.attr('src', 'images/blue-square-icon.png');
        else
            if (nr == 0)
                buttonField.attr('src', 'images/square-icon.png');
    }
}

myJQGrid.myElemButtonFshiR = function (disabled, lastsel, onblur) {
    var elemDiv = $('<div><div/>');
    var buttonField = $('<input type="image" />');
    buttonField.attr('id', 'butonFshi' + lastsel);
    buttonField.val('Fshi');
    buttonField.attr('src', 'images/square-icon.png');
    if (disabled)
        buttonField.attr("disabled", "disabled");
    if (onblur !== undefined)
        buttonField.focusout(function () { onblur(); });
    buttonField.mouseover(function () { ndryshoImazhin(1, lastsel.toString()); });
    buttonField.mouseout(function () { ndryshoImazhin(0, lastsel.toString()); });
    buttonField.click(function () { fshiClicked2(lastsel.toString()); });
    buttonField.appendTo(elemDiv);
    return elemDiv;
};

myJQGrid.myValueButtonFshi = function (disabled, lastsel, emergride, raporte) {
    if (disabled == 'True' || disabled == true)
        be = "<input id='butonFshi" + ((raporte ? emergride : "") + lastsel) + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + lastsel + ")' onmouseout='ndryshoImazhin(0," + lastsel + ")'  src='images/square-icon.png'  onclick='fshiClicked" + (emergride == "#rowed6" ? "2" : (emergride == "#rowed7" ? "3" : "")) + "(" + (raporte ? ("&quot;" + emergride + "&quot;" + ',') : '') + lastsel + ",event)'/>";

    else
        be = "<input id='butonFshi" + ((raporte ? emergride : "") + lastsel) + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel + ")' onmouseout='ndryshoImazhin(0," + lastsel + ")'  src='images/square-icon.png' onclick='fshiClicked" + (emergride == "#rowed6" ? "2" : (emergride == "#rowed7" ? "3" : "")) + "(" + (raporte ? ("&quot;" + emergride + "&quot;" + ',') : '') + lastsel + ",event)'/>";

    return be; //duhet ndryshuar per crossbrowser
};

myJQGrid.myValueButtonFshiR = function (disabled, lastsel) {
    if (disabled)
        be = "<input id='butonFshi" + lastsel + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + lastsel + ")' onmouseout='ndryshoImazhin(0," + lastsel + ")'  src='images/square-icon.png'  onclick='fshiClicked2(" + lastsel + ")'/>";

    else
        be = "<input id='butonFshi" + lastsel + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel + ")' onmouseout='ndryshoImazhin(0," + lastsel + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + lastsel + ")'/>";
    return be; //duhet ndryshuar per crossbrowser
};
$.fn.outer = function (val) {
    if (val) {
        $(val).insertBefore(this);
        $(this).remove();
    }
    else { return $("<div>").append($(this).clone()).html(); }
};

myJQGrid.myElemButon = function (disabled, idRreshti, onblur) {
    var blur = "";
    if (onblur != "")
        blur = "onBlur= '" + onblur + "()'"
    if (disabled == 'True' || disabled === true)
        el = "<input id='butonFshi" + idRreshti + "' type='image' value='Fshi' disabled='disabled' " + blur + " onmouseover='ndryshoImazhin(1," + idRreshti + ")' onmouseout='ndryshoImazhin(0," + idRreshti + ")'  src='images/square-icon.png' onclick='fshiClicked(" + idRreshti + ")'/>";

    else el = "<input id='butonFshi" + idRreshti + "' type='image' value='Fshi' " + blur + " onmouseover='ndryshoImazhin(1," + idRreshti + ")' onmouseout='ndryshoImazhin(0," + idRreshti + ")'  src='images/square-icon.png' onclick='fshiClicked(" + idRreshti + ")'/>";

    return el;
};

myJQGrid.myElemCheckBox = function (value, disabled, lastgrid, idRreshti, id, onchange) {
    var change = ""
    if (onchange !== "")
        change = "onclick= '" + onchange + "()'";
    var el3 = document.createElement("div");
    if (disabled === 'True') el3.innerHTML = "<input  id ='" + id + lastgrid + idRreshti + "' disabled='disabled' " + change + "   type ='checkbox'  style='width: 100%' > ";
    else el3.innerHTML = "<input  id ='" + id + lastgrid + idRreshti + "'  type ='checkbox' " + change + "   style='width: 100%' > ";
    if (value === 'false') el3.firstChild.checked = false;
    else if (value === 'true') el3.firstChild.checked = true;
    else if (value.toLowerCase().search('checked') === -1) el3.firstChild.checked = false;
    else if (value.toLowerCase().search('checked') !== -1) el3.firstChild.checked = true;
    el3.value = value;
    return el3;
};

myJQGrid.myElemEmertimGjate = function (value, disabled, idRreshti, id, onchange) {
    var elemDiv = $('<div></div>');
    elemDiv.width("100%");
    var elemField = $('<textarea></textarea>');
    elemField.attr('id', id + idRreshti);
    elemField.val(value);
    elemField.attr('title', value);
    if (onchange !== undefined)
        elemField.change(function () { onchange(); });
    if (disabled == 'True' || disabled === true)
        elemField.attr("disabled", "disabled");
    elemField.width('100%');
    elemField.appendTo(elemDiv);
    return elemDiv;
};

myJQGrid.myValueTextArea = function (elem, operation, value) {
    if (operation === 'get') {
        return $(elem).find("textarea").val(); //.val()
    }
    else if (operation === 'set') {
        $('textarea', elem).val(value);
    }
};

myJQGrid.textAreaFormat = function (cellvalue, options, rowObject) { //return '<img src="'+cellvalue+'" />';
    var texti;
    if (cellvalue === undefined)
        texti = ' ';
    else
        texti = cellvalue;
    var elemField = $('<textarea></textarea>');
    elemField.text(texti);
    elemField.attr("disabled", "disabled");
    elemField.width('98%');
    return elemField.clone().wrap('<p>').parent().html().toString();
};

myJQGrid.textAreaUnFormat = function (cellvalue, options, cell) {
    return $('textarea', cell).val();
};

myJQGrid.comboUnFormat = function (cellvalue, options, cell) {
    return $(cell).find("select option:selected").text();
};

myJQGrid.comboFormat = function (cellvalue, options, rowObject) { //return '<img src="'+cellvalue+'" />';   
    var elemDiv = $('<div></div>');
    if (cellvalue === undefined || cellvalue === "") {
        var texti = '';
        var elemField = $('<select></select>');
        elemField.width('98%');
        elemField.attr('disabled', 'disabled');
        var option = $('<option></option>');
        option.val('');
        option.html(texti);
        option.attr('selected', 'selected');
        elemField.append(option);
        return elemDiv.clone().wrap('<p>').parent().html().toString();
    }
    $(cellvalue).attr('disabled', 'disabled');
    $(cellvalue).appendTo(elemDiv);
    return elemDiv.clone().wrap('<p>').parent().html().toString();
};

myJQGrid.myValueComboSup = function (elem, operation, value) {
    if (operation == 'get') {
        return $(elem);
    }
    if (operation == 'set') {
        $('select option', elem).attr('selected', false);
        $("select option[text='" + value + "']", elem).attr('selected', true);
    }
};

myJQGrid.myElemComboSup = function (value, id, idRreshti, onchange, arrayOptions, disabled, arrayParam) {
    var elemDiv = $('<div></div>');
    var elemField = $('<select></select>');
    elemField.width('98%');
    if (value == "") {
        elemField.attr('disabled', 'disabled');
        //        return elemField;//.clone().wrap('<p>').parent().html().toString(); 
    }
    else
        if (disabled !== null && disabled !== undefined && disabled === true)
            elemField.attr('disabled', 'disabled');
        else
            elemField.removeAttr('disabled');
    elemField.attr('id', id + idRreshti);
    if (onchange !== null)
        if (arrayParam === undefined)
            elemField.change(function () { onchange(idRreshti); });
        else {
            switch (arrayParam.length) {
                case 1:
                    elemField.change(function () { onchange(idRreshti, arrayParam[0]); });
                    break;
                case 2:
                    elemField.change(function () { onchange(idRreshti, arrayParam[0], arrayParam[1]); });
                    break;
                case 3:
                    elemField.change(function () { onchange(idRreshti, arrayParam[0], arrayParam[1], arrayParam[2]); });
                    break;
                default:
                    elemField.change(function () { onchange(idRreshti); });
                    break;
            }
        }
    if (arrayOptions !== undefined && arrayOptions !== null) {
        for (var i = 0; i < arrayOptions.length; i++) {
            var option = $('<option></option>');
            option.val(arrayOptions[i].value);
            option.html(arrayOptions[i].text);
            if (value === arrayOptions[i].text)
                option.attr('selected', 'selected');
            if (arrayOptions[i].norma !== undefined) {
                option.data("norma", arrayOptions[i].norma);
                option.data("caktuar", arrayOptions[i].caktuar);
            }
            if (arrayOptions[i].koeficienti !== undefined) {
                option.data("koeficienti", arrayOptions[i].koeficienti);
            }
            elemField.append(option);
        }
    }
    elemField.appendTo(elemDiv);
    return elemDiv;
};

myJQGrid.myElemEmertimi = function (value, disabled, idRreshti, id, onchange, onKeydown) {
    var elemDiv = $('<div></div>');
    var elemField = $('<input type="text" />');
    elemField.attr('id', id + idRreshti);
    elemField.val(value);
    elemField.attr('title', value);
    if (onchange)
        elemField.on("change", (function () {
            onchange(id, idRreshti);
        }));
    if (onKeydown)
        elemField.on("keydown", (function (e) {            
            onKeydown(id, idRreshti, e);
        }));
    if (disabled == 'True' || disabled === true)
        elemField.attr("disabled", "disabled");
    elemField.width('100%');
    elemField.addClass("border-textbox");
    elemField.appendTo(elemDiv);
    return elemDiv;
};

myJQGrid.myElemNrRendor = function (value, options, idRreshti, id, grida) {
    var elemDiv = $('<div></div>');
    var elemField = $('<input type="text" />');
    elemField.attr('id', id + idRreshti);
    var vlera = grida.getInd(idRreshti, false);
    if (!vlera)
        console.log("myElemNrRendor - getInd: " + vlera);
    elemField.val(vlera);
    elemField.attr('title', value);
    elemField.attr("disabled", "disabled");
    elemField.width('95%');
    elemField.appendTo(elemDiv);
    return elemDiv;
};

myJQGrid.myelemData = function (value, disabled, idRreshti, id, onclick, onchange) {
    var elemDiv = $('<div></div>');
    var buttonField = $('<input type="text" />');
    buttonField.attr('id', id + idRreshti);
    buttonField.width('65px');
    buttonField.val(value);
    if (disabled == 'True' || disabled === true) 
        buttonField.attr("disabled", "disabled");
    buttonField.change(function () { onchange(); })

    if ($.datepicker.regional["sq"] != undefined)
        $.datepicker.setDefaults($.datepicker.regional['sq']);
    setTimeout(function () {
        (disabled == 'True' || disabled === true) ?
            buttonField.datepicker({ showOn: 'both', autosize: true, constrainInput: false, buttonImage: 'images/calendar.gif', buttonImageOnly: true, buttonText: '...', disabled: true }) :
            buttonField.datepicker({ showOn: 'both', autosize: true, constrainInput: false, buttonImage: 'images/calendar.gif', buttonImageOnly: true, buttonText: '...' });
            $('img.ui-datepicker-trigger').css({
                position: "relative",
                top: "4px"
            });
        }, 100);
    buttonField.appendTo(elemDiv);
    return elemDiv;
};
myJQGrid.myelemTimePicker = function (value, disabled, idRreshti, id, onclick, onchange, onkeyup) {
    var elemDiv = $('<div></div>');
    var buttonField = $('<input type="text" />');
    buttonField.attr('id', id + idRreshti);
    buttonField.width('65px');
    buttonField.val(value);
    if (disabled == 'True' || disabled === true)
        buttonField.attr("disabled", "disabled");
    buttonField.change(function () { onchange(); })
    buttonField.keyup(function () { onkeyup(); })
    buttonField.timepicker({
        timeFormat: "HH:mm:ss", currentText: 'Tani', closeText: 'Ok', timeOnlyTitle: 'Zgjidhni oren', timeText: 'Koha', hourText: 'Ore', minuteText: 'Minuta', secondText: 'Seconda', controlType: 'slider', addSliderAccess: true,
        sliderAccessArgs: { touchonly: false }

    });

    buttonField.appendTo(elemDiv);
    return elemDiv;
};
myJQGrid.getMyCombo = function (grida, emertimi, idRreshti, arrayOptions, value, replace, disabled, onChange) {
    if (value === '')
        value = arrayOptions[0].text;
    if (idRreshti == grida.getLastSel2()) {
        var myCombo = myJQGrid.myElemCombo(value, emertimi, idRreshti, onChange, arrayOptions, disabled);
        if (replace === true)
            $("#" + emertimi + idRreshti).replaceWith(myCombo.children()[0]);
        return myCombo;
    }
    grida.jqGrid('setCell', idRreshti, emertimi, value, '', '', disabled);
    return arrayOptions[0];
};
myJQGrid.myElemCombo = function (value, id, idRreshti, onchange, arrayOptions, disabled, arrayParam) {
    if (arrayOptions == null || arrayOptions.length == 0)
        return myJQGrid.myElemEmertimi(value, disabled, idRreshti, id, onchange);
    var divi = $('<div></div>');
    divi.addClass("imb-jqgrid-combo");
    var combo = $('<select></select>');
    if (disabled !== null && disabled !== undefined && disabled === true)
        combo.attr('disabled', 'disabled');
    combo.attr('id', id + idRreshti);
    if (onchange !== null)
        if (arrayParam === undefined || arrayParam === null)
            combo.on('change', function () { onchange(id, idRreshti) });
        else {
            switch (arrayParam.length) {
                case 1:
                    combo.on('change', function () { onchange(arrayParam[0]); });
                    break;
                case 2:
                    combo.on('change', function () { onchange(arrayParam[0], arrayParam[1]); });
                    break;
                case 3:
                    combo.on('change', function () { onchange(arrayParam[0], arrayParam[1], arrayParam[2]); });
                    break;
                default:
                    combo.on('change', onchange);
                    break;
            }
        }
    if (arrayOptions !== undefined) {
        for (var i = 0; i < arrayOptions.length; i++) {
            var option = $('<option></option>');
            option.val(arrayOptions[i].value);
            option.html(arrayOptions[i].text);
            if (value === arrayOptions[i].text)
                option.attr('selected', 'selected');
            if (arrayOptions[i].norma !== undefined) {
                option.data("norma", arrayOptions[i].norma);
                option.data("caktuar", arrayOptions[i].caktuar);
            }
            if (arrayOptions[i].koeficienti !== undefined) {
                option.data("koeficienti", arrayOptions[i].koeficienti);
            }
            if (arrayOptions[i].desc !== undefined) {
                option.data("desc", arrayOptions[i].desc);
            }
            combo.append(option);
        }
    }
    combo.appendTo(divi);
    return divi;
};

myJQGrid.myElemTextBoxVlefte = function (value, options, disabled, idRreshti, id, onkeyup, defaultVal, onfocusout) {
    var divi = $('<div></div>');
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);
    if (value == "" || value == "NaN")
        if (defaultVal == undefined)
            textField.val('0.00');
        else
            textField.val(defaultVal);
    else
        textField.val(value);
    textField.width('95%');
    textField.keyup(function (event) {
        if (event.which < 45 && event.which != 32 && event.which != 8) {
            return;
        }
        onkeyup();
    });

    textField.focusout(function () {
        if ($('#' + id + idRreshti).val() == "" || isNaN($('#' + id + idRreshti).val())) {
            if (defaultVal == undefined)
                textField.val('0.00');
            else
                textField.val(defaultVal);
        }
        else
            $('#' + id + idRreshti).val(parseFloat($('#' + id + idRreshti).val()).toFixed(2));

        if (onfocusout != undefined)
            onfocusout();
    });
    if (disabled == 'True' || disabled === true)
        textField.attr("disabled", "disabled");
    textField.appendTo(divi);
    return divi;
};
//Deprecated - use grida.myElemTextBoxFormatNumri({})
myJQGrid.myElemTextBoxVlefteSipasFormatNumri = function (grida, value, options, disabled, idRreshti, id, onkeyup, hfFormatNumri, onfocusout, button, buttonClick, buttonValue, onKeyDown) {
    var divi = $('<div></div>');
    var textField = $('<input type="text" />');
    textField.attr('id', id + idRreshti);

    if (value == "" || value == "NaN" || value == undefined) {
        value = grida.getVlereDefault(id);
    }
    var realValue = grida.getVlereReale(textField.attr('id'));
    if (realValue != null && typeof realValue != 'undefined' && realValue != '')
        value = realValue;
    textField.val(value);
    if (!button) {
        textField.width('100%');
        textField.addClass("border-textbox");
    }
    else {
        textField.width('100%');
        textField.addClass("border-textbox");
    }

    if (onKeyDown)
        textField.on("keydown", function (e) {
            if (onKeyDown && e.which == 13) {
                //e.preventDefault();
                onKeyDown();
            }
        });
    textField.keyup(function (event) {
        if (event.which < 45 && event.which != 32 && event.which != 8) {
            return;
        }
        onkeyup(id, idRreshti);
    });

    textField.focusout(function () {
        if (this.value == "") {
            var grida = $(this).closest('table');
            textField.val(grida.getVlereDefault(this.id));
        }
        if (typeof onfocusout != 'undefined')
            onfocusout();
    });
    if (disabled == 'True' || disabled === true)
        textField.attr("disabled", "disabled");
    textField.appendTo(divi);
    if (button) {
        var button = $('<input type="button" />');
        if (!buttonValue)
            buttonValue = "...";
        button.val(buttonValue);
        if (disabled == 'True' || disabled === true)
            button.attr("disabled", "disabled");
        button.attr("name", "button1");
        button.attr("id", "btn" + id + idRreshti);
        button.click(buttonClick);
        button.width('18%');
        button.appendTo(divi);
    }
    return divi;
};

myJQGrid.myElemVlefta = function (value, options, disabled, idRreshti, id, onkeyup) {
    var el3 = document.createElement("div");
    if (value == "")
        value = "0.00";
    var blur = "";
    if (onkeyup != "")
        blur = " onkeyup='" + onkeyup + "()'"                 // onBlur= '" + onblur + "()'

    if (disabled == 'True' || disabled === true)
        el3.innerHTML = "<input  id ='" + id + idRreshti + "'  type ='text' disabled='disabled' " + blur + " value='" + value + "' style='width: 95%' > "
    else el3.innerHTML = "<input  id ='" + id + idRreshti + "'  type ='text' " + blur + " value='" + value + "' style='width: 95%' > "

    el3.value = value;
    return el3;
}

myJQGrid.myElemVleftaSipasFormatNumri = function (value, options, disabled, idRreshti, id, onkeyup) {
    if (value == "")
        value = hfFormatNumri.Get("FormatZgjedhurVlefta");
    var el3 = document.createElement("div");
    var blur = "";
    if (onkeyup != "")
        blur = " onkeyup='" + onkeyup + "()'"                 // onBlur= '" + onblur + "()'

    if (disabled == 'True' || disabled === true)
        el3.innerHTML = "<input  id ='" + id + idRreshti + "'  type ='text' disabled='disabled' " + blur + " value='" + value + "' style='width: 95%' > "
    else el3.innerHTML = "<input  id ='" + id + idRreshti + "'  type ='text' " + blur + " value='" + value + "' style='width: 95%' > "

    el3.value = value;
    return el3;
}


myJQGrid.fshiClicked = function (index, emerGride, inicalizoGride) {
    var grida = $(emerGride);
    if (grida.jqGrid('getGridParam', 'reccount') > 1) {
        grida.jqGrid('delRowData', index);
    }
    else {

        var idRreshti = grida.getLastSel2();
        grida.jqGrid('clearGridData');
        var disabled = emerGride == "#rowed6" ? (arrayReadOnlyKolonaSubGrides[arrayReadOnlyKolonaSubGrides.lengh - 1]) : (emerGride == "#rowed7" ? arrayReadOnlyKolonaGridesV[arrayReadOnlyKolonaGridesV.length - 1] : (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1]));
        if (disabled == 'True' || disabled === true)
            be = "<input id='butonFshi" + idRreshti + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + idRreshti + ")' onmouseout='ndryshoImazhin(0," + idRreshti + ")'  src='images/square-icon.png'  onclick='fshiClicked" + (emerGride == "#rowed6" ? "2" : (emerGride == "#rowed7" ? "3" : "")) + "(" + idRreshti + ",event)'/>";

        else
            be = "<input id='butonFshi" + idRreshti + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + idRreshti + ")' onmouseout='ndryshoImazhin(0," + idRreshti + ")'  src='images/square-icon.png' onclick='fshiClicked" + (emerGride == "#rowed6" ? "2" : (emerGride == "#rowed7" ? "3" : "")) + "(" + idRreshti + ",event)'/>";

        var datarow = emerGride == "#rowed6" ? { txtFshiR: be } : (emerGride == "#rowed7" ? { txtFshiZ: be } : { txtFshi: be });
        var su = grida.jqGrid('addRowData', parseInt(idRreshti), datarow);
        grida.setLastSel2(-1);
    }

}

myJQGrid.myValueCombo = function (elem, operation, value) {
    if (operation == 'get') {
        return $(elem).find("select option:selected").text();
    }
    if (operation == 'set') {
        $('select option', elem).attr('selected', false);
        $("select option[text='" + value + "']", elem).attr('selected', true);
    }
}

myJQGrid.myValueTextBox = function (elem, operation, value) {
    if (operation === 'get') {
        var inputElem = $(elem).find("input");
        var emerQelize = $(elem).attr('name');
        var index = inputElem.attr("id").substring(emerQelize.length);
        $(this).setTekstQelize(emerQelize, index, inputElem.val(), true);
        return inputElem.val();
    }
    else if (operation === 'set') {
        $('input', elem).val(value);
    }
}

myJQGrid.myValueTextBoxSub = function (elem, operation, value) {
    if (operation === 'get') {
        var inputElem = $(elem).find("input");
        var emerQelize = $(elem).attr('name');
        var index = inputElem.attr("id").substring(emerQelize.length);
        $(this).setTekstQelize(emerQelize, index, inputElem.val(), true, $(this).getlastSelSub());
        return inputElem.val();
    }
    else if (operation === 'set') {
        $('input', elem).val(value);
    }
}

myJQGrid.SucceededCallbackKodi = function (result, emerfushe) {
    if ($(emerfushe).val() !== "" && result && result.length > 0) {
        $(emerfushe).autocomplete("option", "source", result);
        $(emerfushe).autocomplete("search", '');
    }
    else {
        $(emerfushe).autocomplete("option", "source", new Array());
        $(emerfushe).autocomplete("close");
    }
}

myJQGrid.SucceededCallbackKodiDetajimi = function (result, emerfushe, kategoria, teksti) {
    if ($(emerfushe).val() !== "" && result && result.length > 0) {
        $(emerfushe).autocomplete("option", "source", result);
        $(emerfushe).autocomplete("search", '');
    }
    else {
    }
}

myJQGrid.SucceededCallbackCmimi = function (result, emerfushe, emerkodi) {
    if ($(emerkodi).val() !== "" && result && result.length > 0) {
        $(emerfushe).autocomplete("option", "source", result);
        $(emerfushe).autocomplete("search", '');
    }
    else {
        $(emerfushe).autocomplete("option", "source", new Array());
        $(emerfushe).autocomplete("close");
    }
}

myJQGrid.myvalueNormal = function (elem) {
    return elem[0].firstChild.value;
}

myJQGrid.ndryshoImazhin = function (nr, index) {
    var id = "#butonFshi" + index;
    if ($(id)[0] != null) {
        if (nr == 1)
            $(id)[0].src = "images/blue-square-icon.png";
        else if (nr == 0)
            $(id)[0].src = "images/square-icon.png";
    }
}

//TODO PATI: Ky funksion duhet rregulluar, perdoret vetem ne 4 ambiente. Duhet kaluar tek klasa myMenu, dhe duhet pershtatur per te gjithe ambientet.
myJQGrid.menuClick = function (s, e, adresa, adresashto) {
    if (e.item.name == 'Ruaj') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi('Ju nuk keni autorizim per te ruajtur kete dokument!');
            Utils.hiqLoadingGif();;
            e.processOnServer = false; click = false;
            return;
        }
        Utils.shfaqLoadingGif();;
        //validim(s, e);
        var validimEntries = myFaqeCelje.validim(s, e);
        if (!validimEntries) {
            Utils.hiqLoadingGif();
            // nuk ka perse te behet e.processOnServer = false; ne kete rast sepse behet tek myFaqeCelje.validim
            click = false;
            return;
        }
           
        if (isValidKoka()) {
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false; click = false;
        }
    }
    else if (e.item.name == 'Draft') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi('Ju nuk keni autorizim per te ruajtur kete dokument!');
            Utils.hiqLoadingGif();;
            e.processOnServer = false; click = false;
            return;
        }
        Utils.shfaqLoadingGif();;
        myFaqeCelje.validim(s, e);

        if (isValidKoka()) {
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false; click = false;
        }
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta(adresa, null, true); e.processOnServer = false;
    }
    else if (e.item.name == 'Shto') {
        myFaqeCelje.kontrolloTeDrejta(adresashto, true); e.processOnServer = false;
    }
    else if (e.item.name == 'Pastro') {
        myFaqeCelje.kontrolloTeDrejta(adresashto, true); e.processOnServer = false;
    }
    else if (e.item.name == 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi('Ju nuk keni autorizim per te fshire kete dokument!');
            Utils.hiqLoadingGif();;
            e.processOnServer = false; click = false;
            return;
        } popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta(adresa);
        e.processOnServer = false;
        click = false;
    }
}


myJQGrid.keyPressPershkrimi = function (emergride, idRreshti, disabled, caption) {
    if (!kaPrind())
        return;

    if (idRreshti === $("#" + emergride).jqGrid('getDataIDs')[$("#" + emergride).jqGrid('getDataIDs').length - 1]) {
        var index2 = parseInt(idRreshti) + 1;
        if (disabled === 'True')
            var be = "<input id='butonFshi" + emergride + index2 + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + "&quot;" + emergride + "&quot;" + "," + index2 + ")'/>";

        else
            be = "<input id='butonFshi" + emergride + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + "&quot;" + emergride + "&quot;" + "," + index2 + ")'/>";
        mydata2 = { txtFshi: be };
        $("#" + emergride).jqGrid('addRowData', parseInt(idRreshti) + 1, mydata2);
    }
}

var getColumnIndexByName = function (grid, columnName) {
    var cm = grid.jqGrid('getGridParam', 'colModel'), i, l;
    for (i = 1, l = cm.length; i < l; i += 1) {
        if (cm[i].name === columnName) {
            return i; // return the index
        }
    }
    return -1;
};

var resetAltRows = function () {
    $(this).children("tbody:first").children('tr.jqgrow').removeClass('myAltRowClass').addClass('myAltRowEvenClass');
    $(this).children("tbody:first").children('tr.jqgrow:visible:odd').removeClass('myAltRowEvenClass').addClass('myAltRowClass');
};

myJQGrid.inicializoTreeGride = function (emergride, arrayPershkrime, arrayModel, mydata, widthi, level, parent, leaf, expanded, expandColumn, idSelektuar) {
    $(emergride).jqGrid(
        {
            treeGrid: true,
            treeGridModel: 'adjacency',
            datatype: "json",
            data: mydata,
            colNames: arrayPershkrime,
            colModel: arrayModel,
            caption: "",
            hidegrid: false,
            height: 'auto',
            cellsubmit: 'clientArray',
            shrinkToFit: true,
            recreateForm: true,
            UpdButton: true,
            ExpandColumn: expandColumn,
            loadonce: false,
            treeReader: {
                level_field: level,
                parent_id_field: parent,
                leaf_field: leaf,
                expanded_field: expanded
            },
            jsonReader: {
                root: "rows",
                page: 1,
                total: 1,
                records: mydata.length,
                repeatitems: false
            },
            onSelectRow: function (id, icol) {
                window.idSeleketuar = id;
                $(emergride + ' #' + id).removeClass('ui-state-highlight');
                
            },
            gridComplete: function () {
                var grid = this;
                resetAltRows.call(this);
                $(this).find('tr.jqgrow td div.treeclick').click(function () {
                    resetAltRows.call(grid);
                });
            },
        });
    $(emergride).parents('div.ui-jqgrid-bdiv').css("max-height", "700px");
    return idSeleketuar;
};

myJQGrid.checkIfIsTheSame = function (grida, artikulli, detajimi, idRreshti, lloji, kodbariArtNeGride, kodbarisele, kerkoMeKodbar) {
    if (grida.getTekstQelize('txtIdKodi', idRreshti) == artikulli.IdArtikulli && lloji == "Artikull") {
        if (kodbariArtNeGride != kodbarisele || (grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "kerkoMeKodbar") != undefined && grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "kerkoMeKodbar") != kerkoMeKodbar))//kur kerkon me kodbar ne fillim pastaj me kod te artikullit (ose anasjelltas) duhet marre sikur artikulli ndryshon
            return false;
        if ((artikulli.IdKategoriDetajimi == 3 && artikulli.IdKategoriDetajimi2 == 4) || (artikulli.IdKategoriDetajimi == 4 && artikulli.IdKategoriDetajimi2 == 3))
            return true;
        if (detajimi == null || detajimi.KodDetajimArtikulli == null || (detajimi != null && (!$('#txtDetajimi').data('VendosDetajim') || detajimi.KodDetajimArtikulli == grida.getTekstQelize('txtDetajimi', idRreshti)))) {
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            return true; //eshte i njejti artikull 
        }
    }
    return false;
};

(function ($, undefined) {
    $.fn.getCursorPosition = function () {
        var el = $(this).get(0);
        var pos = 0;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            var Sel = document.selection.createRange();
            var SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        return pos;
    }
    jQuery.fn.setCursorPosition = function (position, pos2) {
        if (this.lengh == 0) return this;
        return $(this).setSelection(position, pos2);
    }
    jQuery.fn.setSelection = function (selectionStart, selectionEnd) {
        if (this.lengh == 0) return this;
        input = this[0];

        if (input.createTextRange) {
            var range = input.createTextRange();
            range.collapse(true);
            range.moveEnd('character', selectionEnd);
            range.moveStart('character', selectionStart);
            range.select();
        } else if (input.setSelectionRange) {
            input.focus();
            input.setSelectionRange(selectionStart, selectionEnd);
        }

        return this;
    }
})(jQuery);




myJQGrid.ktheObjektMeSasitePerArtikullinMeDetajimNeGride = function (kodArtShtim, grida, shtimModifikim, idDok, dokShitje, rreshtRi, index, fushat) {
    var objektSasiArtikulli = {
        shtimModifikim: shtimModifikim, idDok: idDok, dokShitje: dokShitje, detSasite: grida.ktheSasiPerArtikullinMeDetajim(kodArtShtim, fushat)
    };

    if (!rreshtRi && index != undefined) {// nqs kalon fokusin ne rresht te ri, te 0-het sasia e artikullit te fundit ne gride qe mos ndryshojne vlerat
        var kodDetajimi = grida.getTekstQelize(fushat.detajimi, index);
        var kodDetajimi2 = grida.getTekstQelize(fushat.detajimi2, index);

        objektSasiArtikulli["detSasite"].map(function (item) {
            if (item["Detajim"] == kodDetajimi || item["Detajim"] == kodDetajimi2)
                item["Sasi"] = 0;
        });
    }
    return objektSasiArtikulli;
};

function vendosSasiPerDetajimNeObjekt(detajimSasi, fushaDetID, rreshti, koeficientArt, njesia2Art, fushat) {
    var sasiArt = parseFloat(rreshti[fushat.sasia]);
    if (rreshti[fushat.njesia] == njesia2Art) //merr sasine sipas njesive
        sasiArt = parseFloat(rreshti[fushat.sasia]) * koeficientArt;
    //filtron nga detajimSasi vetem objektin me detajimin e rreshtit
    var ArtDet = detajimSasi.filter(function (el) { return el["Detajim"] == rreshti[fushaDetID] })[0];

    if (ArtDet == undefined) //nqs nuk ekziston e shton
        detajimSasi.push({ Detajim: rreshti[fushaDetID], Sasi: sasiArt });
    else                     //nqs ekziston shton sasine
        ArtDet.Sasi = parseFloat(ArtDet["Sasi"]) + sasiArt;

    return detajimSasi;
}

myJQGrid.krijoExportExcelPerGride = function (htmlTable, EmerExporti) {
    var fileName = "";
    fileName += EmerExporti.replace(/ /g, "_");

    var link = document.createElement("a");
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><meta charset="UTF-8"/><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };

    var ctx = {
        worksheet: EmerExporti,
        table: htmlTable
    }

    link.href = uri + base64(format(template, ctx));
    link.style = "visibility:hidden";
    link.download = fileName + ".xls";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

myJQGrid.krijoTableHtmlNgaJQGrida = function (GridaDatas) {
    var gridIDs = GridaDatas.gridIDs;
    var gridRows = GridaDatas.gridRows;
    var arrPershkrime = GridaDatas.arrPershkrime;
    var arrElements = GridaDatas.arrElements;
    var formateText = GridaDatas.formateText ? GridaDatas.formateText : [];
    var emerEditorKodi = GridaDatas.emerEditorKodi;
    var htmlTable = $("<table></table");

    var headerRow = $("<tr></tr>");
    for (var j = 0; j < arrPershkrime.length; j++) {
        var th = $("<th style='font-size: 15px;'></th>");
        if (!GridaDatas.arrElements[j].hidden && arrElements[j].name != 'txtFshi') {
            th.append(arrPershkrime[j]);
            headerRow.append(th);
        }
    }

    htmlTable.append(headerRow);

    for (var i = 0; i < gridRows.length; i++) {
        var row = $("<tr></tr>");
        var bosh = false;
        for (var j = 0; j < arrElements.length; j++) {
            var index = arrElements[j].name;
            if (!arrElements[j].hidden && index != 'txtFshi') {
                var td = $("<td></td>");
                var style = (jQuery.inArray(index, formateText) != -1) ? 'mso-number-format:\"\@\";font-size: 15px;' : 'font-size: 15px;';
                var teksti = gridRows[i][index];
                if (index == emerEditorKodi && Utils.IsNullOrEmpty(teksti)) {
                    bosh = true;
                    break;
                }
                td.attr("style", style);
                td.append(teksti.toLocaleString());
                row.append(td);
            }
        }
        if (bosh)
            continue;
        htmlTable.append(row);
    }
    return htmlTable.html();
};