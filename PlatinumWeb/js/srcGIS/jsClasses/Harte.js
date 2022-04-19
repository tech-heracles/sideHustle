;

function Harte(options) {
    var RestrictExtent = new OpenLayers.Bounds(2015310, 4748008, 2451874, 5312366);
    this.defaults = {
        resolutions: [979.9999999999999, 560.0, 280.0, 140.0, 70.0, 27.999999999999996, 13.999999999999998, 6.999999999999999, 2.8, 2.0999999999999996, 1.4, 0.7, 0.27999999999999997, 0.13999999999999999],
        units: 'm',
        div: "map",

        projection: projeksioniGeo,
        projeksioniGeo: projeksioniGeo,
        projeksioniGeoV: projeksioniGeoV,
        projeksioniDisplay: projeksioniDisplay,
        restrictedExtent: RestrictExtent,

        vektoriPerkohshemPerInfo: {},
        kontrollet: {},
        controls: [
            new OpenLayers.Control.Navigation({
                "zoomWheelEnabled": true,
                "mouseWheelOptions": {
                    "interval": 250,
                    "cumulative": true
                }
            })
        ],

        tileManager: null,
        urlDokumentimiUpload: urlDokumentimiUpload,
    };
    var options = options ? options : {};

    this.settings = $.extend(true, {}, this.defaults, options);
    this.map = new OpenLayers.Map('map', this.settings);
};

//-- FILLO: Konfigurime DEFAULT -----------------------------------------------------
Harte.prototype.konfigurimeDefaultExt = function (options) {

    Ext.grid.PropertyColumnModel.prototype.nameText = perkthe("GP_GRID_PrColModelNameText");
    Ext.grid.PropertyColumnModel.prototype.valueText = perkthe("GP_GRID_PrColModelValueText");

    Ext.MessageBox.buttonText.yes = perkthe("GP_MSG_buttonTextYes");
    Ext.MessageBox.buttonText.no = perkthe("GP_MSG_buttonTextNo");
    Ext.MessageBox.buttonText.ok = perkthe("GP_MSG_buttonTextOk");
    Ext.MessageBox.buttonText.cancel = perkthe("GP_MSG_buttonTextCancel");

    Ext.Ajax.timeout = 600000;
    Ext.QuickTips.init();

    Ext.override(Ext.form.Field, {
        alignErrorIcon: function () {
            if (this.el.dom) {
                this.errorIcon.alignTo(this.el, 'tl-tr', [2, 0]);
            }
        }
    });

    Ext.form.Field.prototype.msgTarget = 'side';
};
Harte.prototype.variableGlobaleNgaHiddenField = function (options) {
    varSettings.theme.urlCustom = "../images/GIS/" + hfState.Get("GISTheme") + "/";
    varSettings.theme.color = varSettings.theme.urlCustom.substring(0, varSettings.theme.urlCustom.length - 1).substring(14);

    MerrProjeksionePerProjeksion(JSON.parse($("#projeksioneHF").val()));
    varSettings.translateArray = JSON.parse(hfState.Get("perkthimeHF"));
    varSettings.webConfig.idPerdoruesi = hfState.Get("idPerdoruesi");
    varSettings.webConfig.idGjuha = hfState.Get("gjuhaHF");
    varSettings.webConfig.idNdermVit = hfState.Get("GISNdermVitHF");
    varSettings.webConfig.layers = JSON.parse($("#GISLayersHF").val());
    varSettings.webConfig.layersType = JSON.parse($("#GISLayersTypeHF").val());
    MerrTeDrejtaSipasPerdoruesit(varSettings.webConfig.layers);
    varSettings.webConfig.treeStructure = JSON.parse($("#folderatShfaqurHF").val());

    varSettings.webConfig.ndermarrjeLogin = JSON.parse($("#GISNdermarrjeHF").val());
    varSettings.webConfig.idNdermarrja = varSettings.webConfig.ndermarrjeLogin.IdNdermarrje;
    var allUsers = JSON.parse($("#GISAllUsersHF").val());
    varSettings.webConfig.userLogin = allUsers.filter(function (user) { return (user.IdPerdorues == varSettings.webConfig.idPerdoruesi); }).map(function (user, index) { user = { Id: user.IdPerdorues, Pershkrim: user.EmriPerdorues + " " + user.MbiemriPerdorues, Email: user.PerdoruesEmail, Emer: user.EmriPerdorues }; return user; })[0];
    varSettings.webConfig.sendEmails = allUsers.filter(function (user) { return (user.IdStatusDok == 1 && (user.PerdoruesEmail != "") && (user.IdPerdorues != varSettings.webConfig.idPerdoruesi)); }).map(function (user, index) { user = { Id: user.IdPerdorues, Pershkrim: user.EmriPerdorues + " " + user.MbiemriPerdorues, Email: user.PerdoruesEmail, Emer: user.EmriPerdorues }; return user; }).sort(dynamicSortMultiple("Pershkrim", "Email"));
    varSettings.webConfig.sendEmails.unshift({ Email: varSettings.webConfig.ndermarrjeLogin.NdermarrjeEMail, Id: 0, Pershkrim: varSettings.webConfig.ndermarrjeLogin.NdermarrjeKodi, Emer: "" });

    varSettings.webConfig.userAutoLogin = hfState.Get("GISAutoUserLogin");
    varSettings.webConfig.urlImazhPerdoruesi = hfState.Get("GISUrlImazhPerdoruesi");

    //boshatisen qe mos ngarkohet ne callback te hartes
    $('#projeksioneHF').val('');
    $('#perkthimeHF').val('');
    $('#GISLayersHF').val('');
    $('#GISLayersTypeHF').val('');
    $('#folderatShfaqurHF').val('');
    $('#GISAllUsersHF').val('');
    $('#GISNdermarrjeHF').val('');
};

//-- FILLO: LAYERAT -----------------------------------------------------------------------
Harte.prototype.shtoMAPLayers = function (options) {
    var AllLayersForMap = options ? options : varSettings.webConfig.layers;

    var defaultLayersForMap = AllLayersForMap.filter(function (layers) {
        return layers.D_AMB;
    }).map(function (layer, index) {
        var parameters = createLayerParametersForOpenLayers(layer.LAYERSTRUPIGIS);
        addParametersToOpenLayersStandart(layer, parameters);
        return addLayerToOpenLayers(layer, parameters, varSettings.webConfig.layers.length, index);
    }).filter(function (layers) {
        return layers != undefined;
    });
    map.addLayers(defaultLayersForMap);
};

//-- FILLO: Seksioni i kontrolleve ne harte -----------------------------------------------
Harte.prototype.shtoMAPControlLoadingPanel = function (options) {
    var tempControl = new OpenLayers.Control.LoadingPanel({
        id: "MAPControlLoadingPanel"
    });
    map.addControl(tempControl);
};
Harte.prototype.shtoMAPControlMousePosition = function (options) {
    var MousePositionControl = new OpenLayers.Control.MousePosition({
        id: "MAPControlMousePosition",
        displayProjection: projeksioniDisplay,
        formatOutput: formatoKordinatatMouseNav
    });
    map.addControl(MousePositionControl);

};
Harte.prototype.shtoMAPControlGraticuleXYT = function (options) {
    var tempControl = new OpenLayers.Control.GraticuleXYT({
        id: "MAPControlGraticuleXYT",
        labelled: true,
        labelFmt: "%7.0f",
        numPoints: 2,
        targetSize: 200,
        displayInLayerSwitcher: false,

        realProjT: projeksioniGeo,
        displayProjT: projeksioniDisplay,
    });
    map.addControl(tempControl);
    tempControl.deactivate();
};
Harte.prototype.shtoMAPControlLayerSwitcher = function (options) {
    layerSwitch = new OpenLayers.Control.LayerSwitcher({ id: "MAPControlLayerSwitcher" });
    layerSwitch.redraw = function () { //e mbishkruajme kete qe te afishojme vetem layerat base
        if (!this.checkRedraw()) {
            return this.div;
        }
        this.clearLayersArray("base");
        this.clearLayersArray("data");

        var containsOverlays = false;
        var containsBaseLayers = false;

        // Save state -- for checking layer if the map state changed.
        // We save this before redrawing, because in the process of redrawing
        // we will trigger more visibility changes, and we want to not redraw
        // and enter an infinite loop.
        var len = this.map.layers.length;
        this.layerStates = new Array(len);
        for (var i = 0; i < len; i++) {
            var layer = this.map.layers[i];
            this.layerStates[i] = {
                'name': layer.name,
                'visibility': layer.visibility,
                'inRange': layer.inRange,
                'id': layer.id
            };
        }

        var layers = this.map.layers.slice();
        if (!this.ascending) {
            layers.reverse();
        }
        for (var i = 0, len = layers.length; i < len; i++) {
            var layer = layers[i];
            var baseLayer = layer.isBaseLayer;

            if (layer.displayInLayerSwitcherBase) //ketu eshte ndryshimi qe duhet ta trashegonim kete klase
            {
                if (baseLayer) {
                    containsBaseLayers = true;
                } else {
                    containsOverlays = true;
                }
                var checked = layer.getVisibility();
                var inputElem = document.createElement("input");
                inputElem.id = this.id + "_input_" + layer.name;
                inputElem.name = (baseLayer) ? this.id + "_baseLayers" : layer.name;
                inputElem.type = (baseLayer) ? "radio" : "checkbox";
                inputElem.value = layer.name;
                inputElem.checked = checked;
                inputElem.defaultChecked = checked;
                inputElem.className = "olButton";
                inputElem._layer = layer.id;
                inputElem._layerSwitcher = this.id;

                if (!baseLayer && !layer.inRange) {
                    inputElem.disabled = true;
                }

                var labelSpan = document.createElement("label");
                labelSpan["for"] = inputElem.id;
                OpenLayers.Element.addClass(labelSpan, "labelSpan olButton");
                labelSpan._layer = layer.id;
                labelSpan._layerSwitcher = this.id;
                if (!baseLayer && !layer.inRange) {
                    labelSpan.style.color = "gray";
                }
                labelSpan.innerHTML = layer.name;
                labelSpan.style.verticalAlign = (baseLayer) ? "bottom" : "baseline";
                var br = document.createElement("br");

                var groupArray = (baseLayer) ? this.baseLayers : this.dataLayers;
                groupArray.push({
                    'layer': layer,
                    'inputElem': inputElem,
                    'labelSpan': labelSpan
                });

                var groupDiv = (baseLayer) ? this.baseLayersDiv : this.dataLayersDiv;
                groupDiv.appendChild(inputElem);
                groupDiv.appendChild(labelSpan);
                groupDiv.appendChild(br);
            }
        }
        this.dataLbl.style.display = (containsOverlays) ? "" : "none";
        this.baseLbl.style.display = (containsBaseLayers) ? "" : "none";
        return this.div;
    };
    map.addControl(layerSwitch);
    //layerSwitch.maximizeControl();    //Komentuar Denisa per 15289  

    var dataLblDiv = document.getElementsByClassName("dataLbl")[0];
    dataLblDiv.className += ' layerSwitchTitulli' + varSettings.theme.color;

    var baseLblDiv = document.getElementsByClassName("baseLbl")[0];
    baseLblDiv.className += ' layerSwitchTitulli' + varSettings.theme.color;
    baseLblDiv.innerHTML = perkthe("GP_BLAYERPAN_TIT_Kryesor");
};
Harte.prototype.shtoMAPControlOverviewMap = function (options) {
    //Harta e vogel djathtas
    OverviewMapControl = new OpenLayers.Control.OverviewMap({
        id: "MAPControlOverviewMap",
        maximized: true,
        mapOptions: {
            projection: new OpenLayers.Projection(projeksioniGeo),
            displayProjection: new OpenLayers.Projection(projeksioniGeoV),
            units: "m",
            maxExtent: extent
        }
    });
    map.addControl(OverviewMapControl);
};
Harte.prototype.shtoMAPControlMAPControlScaleLine = function (options) {
    var tempControl = new OpenLayers.Control.ScaleLine({
        id: "MAPControlScaleLine",
        geodesic: true,
        mapOptions: {
            projection: new OpenLayers.Projection(projeksioniGeo),
            displayProjection: new OpenLayers.Projection(projeksioniGeoV),
            units: "m",
            maxExtent: extent
        }
    });
    map.addControl(tempControl)
};
Harte.prototype.shtoMAPControlNavigationHistory = function (options) {
    var tempControl = new OpenLayers.Control.NavigationHistory({
        id: "MAPControlNavigationHistory",
    });
    map.addControl(tempControl);
    map.addControl(tempControl.next);
    map.addControl(tempControl.previous);
};

Harte.prototype.shtoVektorNeHarte = function (options) {
    var defaults = {
        vektori:
                {
                    titulli: "VektorOverInfo",
                    VektPerkohshemLayer: true,
                    styleMap: new OpenLayers.StyleMap({
                        "default": new OpenLayers.Style({
                            fillColor: "#EEB4B4",
                            fillOpacity: 0.0,
                            strokeColor: "#EEB4B4",
                            strokeWidth: 3,
                            pointRadius: 6
                        }),
                        "select": new OpenLayers.Style({
                            fillColor: "#ff0000",
                            fillOpacity: 1.0,
                            graphicZIndex: 123
                        }),
                        "temporary": new OpenLayers.Style({
                            fillColor: "#EEB4B4",
                            fillOpacity: 1.0
                        })
                    }),
                    projection: this.settings.projeksioniGeoV,
                    strategies: [new OpenLayers.Strategy.BBOX()]
                }
    };

    var options = options ? options : {};
    var settings = $.extend(true, {}, defaults, options);

    var vektoriOverLayInfo = new OpenLayers.Layer.Vector(settings.vektori.titulli, {
        VektPerkohshemLayer: settings.vektori.VektPerkohshemLayer,
        styleMap: settings.vektori.styleMap,
        projection: settings.vektori.projection,
        strategies: settings.vektori.strategies,
        protocol: new OpenLayers.Protocol.HTTP({
            url: urlApp + "phpUI/merrTeDhenaPerObjektetOverLayInfo.php",
            format: new OpenLayers.Format.GeoJSON(),
            readWithPOST: true
        })
    });
    this.map.addLayer(vektoriOverLayInfo);

    var highlightCtrl = new OpenLayers.Control.SelectFeature(vektoriOverLayInfo, {
        id: "MAPControlSelectFeatureHighLight",
        hover: true,
        highlightOnly: true,
        renderIntent: "temporary",
        eventListeners: {
        }
    });
    this.map.addControl(highlightCtrl);
    highlightCtrl.activate();

    var selectCtrl = new OpenLayers.Control.SelectFeature(vektoriOverLayInfo, {
        id: "MAPControlSelectFeatureClickOut",
        clickout: true,
        hover: false
    });
    this.map.addControl(selectCtrl);
    selectCtrl.activate();

    var tooltip;
    vektoriOverLayInfo.events.on({
        "featureselected": function (e) {
            if (tooltip) {
                tooltip.destroy();
            }
            tooltip = new Ext.ToolTip({
                html: 'GID' + e.feature.attributes["gid"],
                dismissDelay: 3000
            });
            tooltip.showAt([10, 10]);
        }
    });
};

Harte.prototype.AfishoHarteAtributeObjektGeo = function(options)
{
    var defaults = {
        response: {},
        emri: 'Informacion',
        zoomToFeature: false,
        pozicioniDritAlign: "br-br",
        diffpozicioniDritAlign: [0, 0],
        idDritare: "",
        idElementiDritare: "",
        kerkimShpejte: false,
        overlayInfo: false,
        dritare: {
            width: 450,
            height: 600,
            panelElement: {
                emriKolonesSiTitull: ""
            }
        },
        vektori: {
            titulli: "VektorOverSelect",
            VektPerkohshemLayer: true,
            styleMap: new OpenLayers.StyleMap({
                "default": new OpenLayers.Style({
                    fillColor: "#94f9ec", 
                    fillOpacity: 0.2,
                    strokeColor: "#94f9ec",
                    strokeWidth: 4,
                    pointRadius: 7

                }),
                "select": new OpenLayers.Style({
                    fillColor: "#0000ff",
                    fillOpacity: 1.0,
                    graphicZIndex: 123
                }),
                "temporary": new OpenLayers.Style({
                    fillColor: "#EEB4B4",
                    fillOpacity: 1.0
                })
            })
        }
    };
    var options = options ? options : {};

    var settings = $.extend(true, {}, defaults, options);
    var arrayObjektiGeo = [] ;
    arrayObjektiGeo = settings.response;

    if (arrayObjektiGeo.length>0) {
        if (settings.kerkimShpejte)
            arrayObjektiGeo = arrayObjektiGeo;
        else
            arrayObjektiGeo = arrayObjektiGeo.data[0];

        if (settings.kerkimShpejte) {
            if (this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt)
                this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt.removeAllFeatures();
            else {
                this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt = new OpenLayers.Layer.Vector("ZoomObjektKerkimShpejte", {
                    styleMap: settings.vektori.styleMap,
                    VektPerkohshemLayer: true
                });
                map.addLayer(this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt);
            }
            settings.vektTempZoomObjekt = this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt;
        }

        if (settings.overlayInfo) {
            if (this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo)
                this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo.removeAllFeatures();
            else
                this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo = new OpenLayers.Layer.Vector("ZoomObjektKerkimShpejte", {styleMap: settings.vektori.styleMap, VektPerkohshemLayer: true});
                map.addLayer(this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo);
            settings.vektTempObjektOverLayInfo = this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo;
        }

        for (var i in arrayObjektiGeo) {
            var feature;
            if (settings.kerkimShpejte) 
                feature = new OpenLayers.Format.WKT().read(arrayObjektiGeo[0]["the_geom"]);
            if (settings.overlayInfo) {
                if (i == "the_geom")
                    feature = new OpenLayers.Format.WKT().read(arrayObjektiGeo["the_geom"]["vlera"]);
            }

            if (feature) {
                feature.geometry.transform(new OpenLayers.Projection(this.settings.projeksioniDisplay), new OpenLayers.Projection(this.settings.projeksioniGeo))
                if (settings.kerkimShpejte)
                    this.settings.vektoriPerkohshemPerInfo.vektTempZoomObjekt.addFeatures(feature);
                if (settings.overlayInfo) {
                    this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo.addFeatures(feature);
                    break;//do hequr kjo
                }
                if (settings.zoomToFeature) {
                    var bounds = feature.geometry.getBounds();
                    map.zoomToExtent(bounds);
                }
            }
        }

        var itemDritare;
        if (settings.kerkimShpejte) {
            if (!Ext.getCmp(settings.idElementiDritare))
            {
                itemDritare = new Ext.grid.PropertyGrid({
                    id: settings.idElementiDritare,
                    clicksToEdit: 0,
                    source: arrayObjektiGeo[0],
                    listeners: {
                        "beforerender": function(grida)
                        {
                            grida.getView().getRowClass = function(row, index) {
                                if (row.data.name == "the_geom" || row.data.name == "gid")
                                {
                                    return 'fshiheRecNeInfo';//eshte css class
                                }
                            }
                        }
                    }
                });
                delete itemDritare.getStore().sortInfo;
            }
            else
            {
                if (Ext.getCmp(settings.idElementiDritare).getStore().sortInfo)
                    delete Ext.getCmp(settings.idElementiDritare).getStore().sortInfo;

                Ext.getCmp(settings.idElementiDritare).setSource(arrayObjektiGeo[0]);
                Ext.getCmp(settings.idElementiDritare).doLayout();
            }
        }
        else if (settings.overlayInfo && (arrayObjektiGeo && (arrayObjektiGeo.length > 0 || Object.keys(arrayObjektiGeo).length)))
        {
            var html = "";
            html = html + "<div id='tedhenatInfoId' class='divInformacionOverLayInfo'> ";
            for (var key in arrayObjektiGeo) {
                if (key != 'the_geom' && arrayObjektiGeo[key] && arrayObjektiGeo[key]['vlera'])
                {
                    if (key == settings.dritare.panelElement.emriKolonesSiTitull) {
                        html = html + "<p class='titulliInformacionit'>" + arrayObjektiGeo[key]['vlera'] + "</p><br>";
                    }             
                }
            }
            html = html + "</div>";
                       
            if (!Ext.getCmp(settings.idElementiDritare))
            {
                itemDritare = {
                     cls:"paneliInfoOverlayCss",
                    id: settings.idElementiDritare,
                    html: html,
                    autoScroll: true,
                    xtype: "panel"
                };
            } else {
                Ext.getCmp(settings.idElementiDritare).update(html);
                Ext.getCmp(settings.idElementiDritare).doLayout();
            }
        }

        if (arrayObjektiGeo && (arrayObjektiGeo.length > 0 || Object.keys(arrayObjektiGeo).length)) {
            if (!settings.overlayInfo)
            {
                settings.dritare.height = settings.dritare.height * arrayObjektiGeo.length + 20;
            }
            var titulli = settings.emri;
            
            if (!Ext.getCmp(settings.idDritare))
            {
                var ObjektHarteAtributeIdWindow = new Ext.Window({
                    title: titulli,
                    hideBorders: true,
                    bodyBorder: false,
                    border: false,
                    header: false,
                    id: settings.idDritare,
                    layout: 'fit',
                    closable: true,
                    constrain: true,
                    width: settings.dritare.width,
                    height: settings.dritare.height,
                    closeAction: 'close',
                    items: [itemDritare],
                    animateTarget: "comboKerkimShpejteDritareId", //bej objektin gjeo ketu
                    showAnimDuration: 0.25,
                    cls: 'popWindow' + varSettings.theme.color,
                    listeners: {
                        'close': function(win)
                        {
                            if (settings.vektTempZoomObjekt && settings.kerkimShpejte)
                            {
                                settings.vektTempZoomObjekt.removeAllFeatures();
                                Ext.getCmp("ComboKerkimShpejteId").clearValue();
                            }
                            if (settings.vektTempObjektOverLayInfo && settings.overlayInfo)
                            {
                                settings.vektTempObjektOverLayInfo.removeAllFeatures();
                            }
                        }
                    }
                })
                
            }
            else
            {
                Ext.getCmp(settings.idDritare).setTitle(settings.emri);
                Ext.getCmp(settings.idDritare).setHeight(settings.dritare.height);
                Ext.getCmp(settings.idDritare).setWidth(settings.dritare.width);
                Ext.getCmp(settings.idDritare).doLayout();
            }

            Ext.getCmp(settings.idDritare).show();
            if (!settings.pozDritaresNeObjGeo)
            {
                Ext.getCmp(settings.idDritare).alignTo(Ext.get("mappanel"), settings.pozicioniDritAlign, settings.diffpozicioniDritAlign);
            }
            else if (settings.pozicionoNeQender)
            {
                Ext.getCmp(settings.idDritare).center();
            }
            else
            {
                var pozDrit = this.map.getPixelFromLonLat(this.settings.vektoriPerkohshemPerInfo.vektTempObjektOverLayInfo.features[0].geometry.getBounds().getCenterLonLat());
                Ext.getCmp(settings.idDritare).setPosition(pozDrit.x, pozDrit.y, true);
            }
        }
        else if (Ext.getCmp(settings.idDritare))
        {
            Ext.getCmp(settings.idDritare).close();
        }
    }
    else
    {
        //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"),perkthe("GP_MSG_MSG_gabim"));
    }
};

Harte.prototype.krijoDivMeFigura = function(options)
{
    var defaults = {
        arrayFigurat: [],
        tipetLejuarfigura: [],
        idDivFigura: 'links',
        folderImages: 'images',
    };
    var options = options ? options : {};
    var settings = $.extend(true, {}, defaults, options);

    var div = document.createElement("div");
    var divFigura = document.createElement("div");
    divFigura.className = "divMeFiguratIdCss";
    divFigura.id = settings.idDivFigura;

    for (var i in settings.arrayFigurat) {
        if (settings.tipetLejuarfigura.indexOf(settings.arrayFigurat[i]['tipi_skedari']) > -1 && settings.arrayFigurat[i]['emer_skedari'] != "background.png") {            var a = document.createElement('a');
            a.setAttribute('data-dialog', '');
            a.href = this.settings.urlDokumentimiUpload + "/" + settings.arrayFigurat[i]['emer_skedari_ruajtur'];
            a.innerHTML = '<img class="imbOverlayInfoCss" width="220px" height="130px" src="' + this.settings.urlDokumentimiUpload + "/" + settings.arrayFigurat[i]['emer_skedari_ruajtur'] + '" alt="">'
            divFigura.appendChild(a);
        }

        if (settings.arrayFigurat[i]['tipi_skedari'] == "pdf")
            this.settings.urlInfoPdf = settings.arrayFigurat[i]['emer_skedari_ruajtur'];

        if (settings.arrayFigurat[i]['emer_skedari'] == "background.png") {
            if (settings.mapBgdiv)
                settings.mapBgdiv.style.backgroundImage = 'url(DokumentimiUpload/' + settings.arrayFigurat[i]['emer_skedari_ruajtur'] + ')';
        }
    }
    div.appendChild(divFigura);
    return div;
};

Harte.prototype.shtoElementTabele = function (options) {
    var defaults = {
        tblBody: '',
        arrayTeDhena: {},
        emriKolones: ''
    };
    var options = options ? options : {};

    var settings = $.extend(true, {}, defaults, options);
    var tr = document.createElement('tr');
    var td = document.createElement("td");
    td.width = '80%';
    tr.appendChild(td);

    if (settings.arrayTeDhena[settings.emriKolones]['kolonaPershkrim'][0]['prapashtesa']) {
        qeliza2Text = document.createTextNode(settings.arrayTeDhena[settings.emriKolones]['kolonaPershkrim'][0]['prapashtesa'] + "\n")
        td.appendChild(qeliza2Text);
    }

    if (settings.arrayTeDhena[settings.emriKolones]['vlera']) {
        td = document.createElement("td");
        td.className = "vleraCssTab";
        td.width = '20%';
        tr.appendChild(td);
        if (settings.arrayTeDhena[settings.emriKolones]['vlera']) {
            var qeliza2Text = document.createTextNode(settings.arrayTeDhena[settings.emriKolones]['vlera'])
            td.appendChild(qeliza2Text);
        }
    }
    settings.tblBody.appendChild(tr);

};

//TODO Denisa: Shtoje si funksionalitet ose hiqe Fare
Harte.prototype.hapDritareBuffer = function (options) {
    this.settings.buffer.idTextFieldDiam = "textFieldDiamBufferId";
    var defaults = {
        comboLayerBuffer: new Ext.form.ComboBox({}),
        idcomboLayerBuffer: "comboLayerBufferId",
        widthDritarja: 350,
        heightDritarja: 300,
        widthCombo: 200,
        widthTextFieldDiam: 100,
        widthLabelTextFieldDiam: 200,
        valueDiamDef: 1000
    };
    var options = options ? options : {};
    var settings = $.extend(true, {}, defaults, options);

    if (!Ext.getCmp(settings.idcomboLayerBuffer)) {
        var LayerStoreBuffer = new Ext.data.JsonStore({ fields: ["IdLayer", "Name", "Title"], data: {} });

        var LayerStoreBufferMask = new Ext.LoadMask(Ext.getBody(), { msg: perkthe("GP_MSG_Load_layer_buffer"), store: LayerStoreBuffer });
        LayerStoreBuffer.load({
            callback: function (records, operation, success) {
                if (records.length == 0) {
                    noty({ text: perkthe("GP_MSG_MSG_NukKeniLayerBuffer"), type: "warning" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_NukKeniLayerBuffer"));
                }
            }
        });

        var LayerBufferCmb = new Ext.form.ComboBox({
            id: settings.idcomboLayerBuffer,
            objektiThirres: this,
            store: LayerStoreBuffer,
            displayField: 'Title',
            tooltipType: "title",
            tooltip: perkthe("GP_COMBO_TOOLTIP_bufferLayer"),
            valueField: 'Name',
            mode: 'local',
            forceSelection: true,
            typeAhead: true,
            triggerAction: 'all',
            emptyText: perkthe("GP_COMBO_EmtyText_bufferLayer"),
            selectOnFocus: true,
            hideTrigger: false,
            enableKeyEvents: true,
            //   width: settings.widthCombo,
            listeners: {
                select: function (combo, record, index) {
                    //   merrElementinGeoKerkim(combo.getValue(), combo.getRawValue());
                    this.objektiThirres.aktivizoSelektiminNeHartePerBuffer(combo.getValue(), combo.getRawValue());
                }
            }
        });

        var LayerBufferDiam = new Ext.form.TextField({
            // xtype: 'textfield',
            //   width: settings.widthTextFieldDiam,
            id: this.settings.buffer.idTextFieldDiam,
            allowBlank: true,
            // blankText: "",
            fieldLabel: perkthe("GP_BUFFER_DiameterLabel"),
            labelWidth: settings.widthLabelTextFieldDiam,
            value: settings.valueDiamDef
        });

        var layerBufferLabelInfo = new Ext.form.Label({
            name: 'label_buffer_info',
            id: 'label_buffer_info_id',
            text: perkthe("GP_BufferMsgInfo"),
            cls: 'x-form-item-label x-form-item'
        });
    }

    if (!Ext.getCmp(settings.idDritareBuffer)) {
        var BufferLayerComboWin = new Ext.Window({
            title: perkthe("GP_WIN_TIT_BufferLayerWin"),
            id: settings.idDritareBuffer,
            harteObj: this,
            layout: 'form',
            closable: true,
            constrain: true,
            autoHeight: true,
            width: settings.widthDritarja,
            height: settings.heightDritarja,
            closeAction: 'hide',
            bodyStyle: 'padding:1px 1px 1px 1px',
            cls: 'popWindow' + varSettings.theme.color,
            items: [{
                layout: 'form',
                defaults: { anchor: '100%' },
                items: [LayerBufferDiam, LayerBufferCmb, layerBufferLabelInfo]
            }],
            listeners: {
                'close': function (win) {
                },
                'hide': function (win) {
                    if (settings.btnBuffer.pressed) {
                        settings.btnBuffer.toggle();
                    }
                    if (this.harteObj.settings.buffer.vektoriBuffer && this.harteObj.settings.buffer.vektoriBuffer.features) {
                        this.harteObj.settings.buffer.vektoriBuffer.removeAllFeatures();
                    }
                    if (this.harteObj.settings.buffer.controlMerrObjekteBuffer) {
                        this.harteObj.settings.buffer.controlMerrObjekteBuffer.deactivate();
                    }
                }
            }
        });
    }
    Ext.getCmp(settings.idDritareBuffer).doLayout();

    Ext.getCmp(settings.idDritareBuffer).show();
    Ext.getCmp(settings.idcomboLayerBuffer).reset();
    Ext.getCmp(settings.idDritareBuffer).alignTo(Ext.getCmp("paneliHartesId").body, "tl-tl", [10, 150]);
};

Harte.prototype.mbyllDritareBuffer = function (options) {
    var defaults = {
    };
    var options = options ? options : {};
    var settings = $.extend(true, {}, defaults, options);
    Ext.getCmp(settings.idDritareBuffer).close();
};
Harte.prototype.aktivizoSelektiminNeHartePerBuffer = function (layerName, layerTitle) {
    this.map.getLayersByName(layerTitle)[0].setVisibility(true);

    shkaterroDritareModifikimMenuObj();//nqs ekziston dritarja fshije ate
    if (this.settings.buffer.controlMerrObjekteBuffer) {
        this.settings.buffer.controlMerrObjekteBuffer.destroy();
    }

    //var httpProtocol = new OpenLayers.Protocol.HTTP_T({
    //    url: "WebService_GIS.asmx/MerrObjektinGeoBufferSelect",
    //    headers: { 'Content-type': 'application/json' },

    //    format: new OpenLayers.Format.GeoJSON(),
    //    params: {
    //        layer: layerName
    //    },
    //});

    this.settings.buffer.controlMerrObjekteBuffer = new OpenLayers.Control.GetFeatureT({
        protocol: httpProtocol,
        box: true,
        click: true,
        toggleKey: "ctrlKey",
        clickout: false,
        toggle: false,
        hover: false,
        clickTolerance: 1,
    });
    this.map.addControl(this.settings.buffer.controlMerrObjekteBuffer);

    this.settings.buffer.controlMerrObjekteBuffer.events.register("featuresselected", this, function (e) {
        for (var i = 0; i < e.features.length; i++) {
            e.features[i].geometry = e.features[i].geometry.transform(new OpenLayers.Projection(projeksioniDisplay), new OpenLayers.Projection(projeksioniGeo))
            e.features[i].fid = layerTitle + "." + e.features[i].attributes["gid"];
        }
        if (e.features.length > 1) {

            (e.features, [], "buffer", layerName);
        }
        else {
            this.krijoBufferPerObjektin(e.features[0], layerName);
        }
    });

    this.settings.buffer.controlMerrObjekteBuffer.activate();
};
Harte.prototype.krijoBufferPerObjektin = function (objektiBuffer, emriLayer) {
    if (!this.settings.buffer.vektoriBuffer) {
        this.settings.buffer.vektoriBuffer = new OpenLayers.Layer.Vector("Elementet ne buffer", {
            VektPerkohshemLayer: true,
            styleMap: new OpenLayers.Style({
                strokeColor: "#f4f8b9",
                fillColor: "#f4f8b9",
                strokeWidth: 2,
                strokeOpacity: 1,
                fillOpacity: 0.7,
                strokeOpacity: 1,
                pointRadius: 5
            })
        });
        this.map.addLayer(this.settings.buffer.vektoriBuffer);
    }
    var wpsClient = new OpenLayers.WPSClient({
        servers: {
            local: 'http://localhost:8090/geoserver/wps?&'
        }
    });

    var harteObj = this;
    objektiBuffer.geometry.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniDisplay));
    wpsClient.execute({
        server: 'local',
        process: 'JTS:buffer',
        inputs: {
            geom: objektiBuffer.geometry,
            distance: Ext.getCmp(this.settings.buffer.idTextFieldDiam).getValue()
        },
        success: function (outputs) {
            harteObj.shtoBufferNeHarte(outputs);
        }
    });
};
Harte.prototype.shtoBufferNeHarte = function (featuresOutput) {
    var gj = featuresOutput.result.length
    for (var i = 0; i < gj; i++) {
        featuresOutput.result[i].geometry.transform(new OpenLayers.Projection(projeksioniDisplay), new OpenLayers.Projection(projeksioniGeo));
        this.settings.buffer.vektoriBuffer.addFeatures(featuresOutput.result[i]);
    }
};

Harte.prototype.goToUserLocation = function (options) {
    var options = options ? options : {};
    this.settings = $.extend(true, this.defaults, options);

    if (navigator.geolocation) {
        var optionsGeolocation = {
            frequency: 10000,
            maximumAge: 10000,
            timeout: 60000,
            enableHighAccuracy: true
        };
        var thisObject = this;
        this.settings.idWatchGeolocation = navigator.geolocation.watchPosition( function (position) {
                if (!thisObject.map.getLayersByName(thisObject.settings.idVectorPerkohGeolocation)[0]) {
                    var markerLayerTemp = new OpenLayers.Layer.Vector(thisObject.settings.idVectorPerkohGeolocation, {
                        styleMap: new OpenLayers.StyleMap({
                            externalGraphic: varSettings.theme.urlDefault + 'menuMarker.png',
                            graphicOpacity: 1.0,
                            graphicWith: 24,
                            graphicHeight: 24
                        }),
                        VektPerkohshemLayer: true
                    });
                    thisObject.map.addLayer(markerLayerTemp);
                    delete markerLayerTemp;
                }
                else
                    thisObject.map.getLayersByName(thisObject.settings.idVectorPerkohGeolocation)[0].removeAllFeatures();

                var latlon = new OpenLayers.LonLat(position.coords.longitude, position.coords.latitude).transform(new OpenLayers.Projection(projektioniKorGeo), new OpenLayers.Projection(projeksioniGeo));
                var pikageoLocationFea = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(latlon.lon, latlon.lat));
                thisObject.map.getLayersByName(thisObject.settings.idVectorPerkohGeolocation)[0].addFeatures(pikageoLocationFea);
                var zoomLevels = thisObject.map.getNumZoomLevels() - 2;
                var zoomLevelAktual = thisObject.map.getZoom();
                var zoomToGo;
                if (zoomLevels - 2 >= zoomLevelAktual)
                    zoomToGo = zoomLevels;
                else
                    zoomToGo = zoomLevelAktual;

                thisObject.map.setCenter(latlon, zoomToGo);

        }, function (err) {
            if (err.code == 1)
                console.log("Error: Access is denied!");
            else if (err.code == 2)
                console.log("Error: Position is unavailable!");
        },optionsGeolocation);
    }
    else
        console.log("no geolocation");
};

Harte.prototype.fshijNgaHartaGeolocation = function () {
    if (this.map.getLayersByName(this.settings.idVectorPerkohGeolocation)[0]) 
        this.map.getLayersByName(this.settings.idVectorPerkohGeolocation)[0].destroy();

    if (this.settings.idWatchGeolocation != null) {
        navigator.geolocation.clearWatch(this.settings.idWatchGeolocation);
        this.settings.idWatchGeolocation = null;
    }
};

clsGISEditimExport = function () {
    //Ndertohet Dritarja e Ambjentit per Exportin e Layerave
    this.inicializoWin = function (outputFormat, titulliDritares, id_layer_type, nrStatusi) {
        var disabledExportItem = (varSettings.editim.objLidhjeGisWeb.gid === 0 ? true : false);
        var hiddenItemAseti = ((StatuseLayeri[0].WEBLLOJI == "MAGAZINA" || StatuseLayeri[0].WEBLLOJI == "GIS" || nrStatusi == 3) ? true : false);

        var panelProjeksioneExportItems = [
             new Ext.form.ComboBox({
                 id: "ExportComboProjeksioneID", mode: 'local', triggerAction: 'all', width: 200, editable: false, hideLabel: true,
                 store: varSettings.webConfig.projeksioneArrayStore, valueField: 'DisplayCode', displayField: 'Description', value: projektioniKorGeo,
                 listeners: {
                     select: function (combo, record, index) {
                         Ext.getCmp("idPanelExportProjectionCode").setText(combo.getValue());
                     }
                 }
             }),
             { xtype: 'label', text: "   " + projektioniKorGeo, name: 'lblPanelExportProjectionCode', id: 'idPanelExportProjectionCode', style: 'font-weight:bold;font-size:12px;color:#1A1A1A;padding-left:10px;' }
        ];
        gisElements.createGISElement("FormPanel", { title: perkthe("GP_SHKOXYWIN_Tab1Title"), id: "PanelProjeksioneExportId", height: 60, layout: { type: 'hbox', align: 'middle' }, items: panelProjeksioneExportItems });

        var ExportPanelItems = [
            new Ext.form.RadioGroup({
                id: 'ExportPanelItemsId', xtype: 'radiogroup', columns: 3, hideLabel: true,
                items: [
                    { xtype: 'radio', boxLabel: 'Objektin', name: "LayerExportEditim", id: "ExportPanelCheckObject", disabled: disabledExportItem, checked: !disabledExportItem },
                    { xtype: 'radio', boxLabel: 'Asetin  ', name: "LayerExportEditim", id: "ExportPanelCheckAsset", disabled: disabledExportItem, hidden: hiddenItemAseti },
                    { xtype: 'radio', boxLabel: 'Layerin ', name: "LayerExportEditim", id: "ExportPanelCheckLayer", checked: disabledExportItem }
                ]
            })
        ];
        var ExportPanelButtons = [
            {
                text: perkthe("GP_EXPMOD_WIN_BTN_TEXT"), formBind: true, waitMsg: perkthe("GP_EXPMOD_MSG_WAIT"),
                handler: function (toggled) {
                    new clsGISEditimExport().ExportButtonHandler(toggled, this, outputFormat);
                }
            }, {
                text: perkthe("GP_MSG_buttonTextCancel"),
                handler: function (toggled) {
                    Ext.getCmp("ExportDritareID").destroy();
                }
            }
        ];
        gisElements.createGISElement("FormPanel", { title: perkthe("GP_EXPMOD_WIN_BTN_TEXT"), id: "ExportPanelID", items: ExportPanelItems, buttons: ExportPanelButtons });

        gisElements.createGISElement("Window", { title: titulliDritares, id: "ExportDritareID", width: 320, items: [gisElements.getGISElement("PanelProjeksioneExportId"), gisElements.getGISElement("ExportPanelID")] });
    };
    
    this.ExportButtonHandler = function (toggled, objekti, outputFormat) {
        if (!toggled)
            return;
        var layerType = varSettings.webConfig.layersType.filter(function (el) { return el.IDLAYERSTYPE == varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE; })[0];
        var nrStatusi = varSettings.editim.objLidhjeGisWeb.NRSTATUSI;
        var url = adresaServer + "/" + varSettings.proxyConfig.url + "wfs?LAYER=" + Ext.getCmp('comboLayer2ID').getValue() + "&SERVICE=WFS&version=1.0.0&REQUEST=GetFeature&TYPENAME=GISEXPORTLAYERS&srsName=" + Ext.getCmp("ExportComboProjeksioneID").getValue();

        url = url + this.KrijoExportUrlSipasLlojitDheStatusitWeb(layerType.WEBLLOJI, layerType.KODI, nrStatusi);
        url = url + this.KrijoExportUrlSipasKerkeses(Ext.getCmp("ExportPanelItemsId").getValue().id, varSettings.editim.objLidhjeGisWeb.gid, varSettings.editim.objLidhjeGisWeb.IDARTIKULLI, Ext.getCmp('comboLayer2ID').getValue());
        url = url + this.KrijoExportUrlOutputFormat(outputFormat);

        downloadURL(url);
        gisElements.destroyGISElement("ExportDritareID");
    };

    this.KrijoExportUrlOutputFormat = function (outputFormat) {
        var url = "";
        switch (outputFormat) {
            case "SHAPE-ZIP":
                var url = "&outputFormat=SHAPE-ZIP&format_options=filename:GISEXPORTSHAPE.zip;format_options=sortBy=KODI";
                break;
            case "AUTOCAD":
                var url = "&outputFormat=DXF-ZIP&format_options=filename:ExportCad.zip;format_options=withattributes:false";
                break;
            case "EXCEL":
                var url = "&outputFormat=excel2007";
                break;
            case "CSV":
                var url = "&outputFormat=csv";
                break;
            default:
                break;
        };
        return url;
    };

    this.KrijoExportUrlSipasLlojitDheStatusitWeb = function (webLloji, webKodi, nrStatusi) {
        var url = "&propertyName=(KODI,PERSHKRIMI,LIDHJA,KOORDINATA)";
        switch (webLloji) {
            case "MAGAZINA":
                switch (webKodi) {
                    case "NENSTACIONE":
                        if (nrStatusi == 3)
                            url = "&propertyName=(KODI,PERSHKRIMI,KOORDINATA)";
                        else
                            url = "&propertyName=(KODI,PERSHKRIMI,DEGA,KOORDINATA)";
                        break;
                    case "KABINA":
                        if (nrStatusi != 3)
                            url = "&propertyName=(KODI,PERSHKRIMI,LIDHJA,MAGAZINA,DEGA,KOORDINATA)";
                        break;
                    default:
                        break;
                };
                break;
            case "SERIALE":
                switch (webKodi) {
                    case "FIDERA":
                    case "SHTYLLA":
                    case "NYJE":
                    case "PUSETA":
                    case "LINJA":
                        if (nrStatusi != 3)
                            url = "&propertyName=(SERIALI,KODI,PERSHKRIMI,LIDHJA,MAGAZINA,DEGA,KOORDINATA)";
                        break;
                    default:
                        break;
                };
                break;
            default:
                break;
        };
        return url;
    };

    this.KrijoExportUrlSipasKerkeses = function (exportPanelItemValue, gid, artid, layer) {
        var url = "";
        switch (exportPanelItemValue) {
            case "ExportPanelCheckObject":
                url = "&cql_filter=gid='" + gid + "'";
                break;
            case "ExportPanelCheckAsset":
                url = "&cql_filter=IDARTIKULLI='" + artid + "'";
                break;
            case "ExportPanelCheckLayer":
                var withFiscalYear = varSettings.webConfig.layers.filter(function (lay) { return lay.IDENTIFICATION == layer; })[0].WITHFISCALYEAR;
                url = "&cql_filter=LAYER='" + layer + "'" + (withFiscalYear ? " AND IDNDERMVIT=" + varSettings.webConfig.idNdermVit : "");
                break;
            default:
                break;
        };
        return url;
    };
};

clsGISEditimImport = function () {
    // Incializohet Dritarja e Importit Fileve
    this.inicializoWin = function (outputFormat, titulliDritares, id_layer_type, nrStatusi) {
        var menuButtonNgarkoObjekt = gisElements.createGISElement("Button", { id: "IDEditimImportMenuNgarko", disabled: true, tooltip: 'Ngarko', icon: varSettings.theme.urlCustom + 'editUpload.png' });
        var menuButtonZoomToObjekt = gisElements.createGISElement("Button", { id: "IDEditimImportMenuZoomTo", disabled: true, tooltip: 'Shko tek Objekti', icon: varSettings.theme.urlCustom + 'goToXYZoomIn.png' });
        var menuButtonFshiObjekt = gisElements.createGISElement("Button", { id: "IDEditimImportMenuFshi", disabled: true, tooltip: 'Fshi Objekt', icon: varSettings.theme.urlCustom + 'fileDelete.png' });
        var menuButtonRuajObjekt = gisElements.createGISElement("Button", { id: "IDEditimImportMenuRuaj", disabled: true, tooltip: 'Ruaj Ndryshimet', icon: varSettings.theme.urlCustom + 'editSave.png' });
        var tempTbar = [menuButtonNgarkoObjekt, '-', menuButtonZoomToObjekt, '-', menuButtonFshiObjekt, '-', menuButtonRuajObjekt];

        var bntUploadSkedar = gisElements.createGISElement("FileUploadField", { id: "IDEditimBntImportSkedar", name: 'filenameGpx', emptyText: 'Zgjidh Skedar ...', buttonCfg: { icon: varSettings.theme.urlCustom + 'editUploadSkedar.png' } });
        var formUploadSkedar = gisElements.createGISElement("FormPanelBasic", { renderTo: Ext.getBody(), fileUpload: true, formId: 'IDEditimImportForm', id: "IDEditimImportFormPanel", width: 310, bodyStyle: 'padding: 10px 15px 10px 15px;', items: [bntUploadSkedar] });

        gisElements.createGISElement("Window", { title: titulliDritares, id: "ImportDritareID", width: 320, tbar: tempTbar, items: [formUploadSkedar] });
        this.inicializoObjectsListener();
    };

    // Shkaterrohet Dritarja e Importit Fileve
    this.destroyWin = function () {
        gisEditimImport.removeLayerByName("V_GIS_Layer_GPS");
        gisElements.destroyGISElement("ImportDritareID");
    };

    // Heq shtresen nga harta 
    this.removeLayerByName = function (name) {
        if (map.getLayersByName(name)[0] != undefined)
            map.removeLayer(map.getLayersByName(name)[0]);
    };

    // Ben enable sipas rastit objektet ne dritare
    this.enableWindowsElements = function (menuNgarko, menuZoomTo, menuFshi, menuRuaj, bntFileUpload) {
        this.enableButtonElement("IDEditimImportMenuNgarko", menuNgarko);
        this.enableButtonElement("IDEditimImportMenuZoomTo", menuZoomTo);
        this.enableButtonElement("IDEditimImportMenuFshi", menuFshi);
        this.enableButtonElement("IDEditimBntImportSkedar", bntFileUpload);

        if (varSettings.editim.objLidhjeGisWeb.veprimi != "DEFAULT")
            this.enableButtonElement("IDEditimImportMenuRuaj", menuRuaj);
        else
            this.enableButtonElement("IDEditimImportMenuRuaj", false);
    };

    // Enable Button Element
    this.enableButtonElement = function (ID, enable) {
        Ext.getCmp(ID).setDisabled(true);
        if (enable)
            Ext.getCmp(ID).enable(true);
    };



    // Tipi POLYGON ose MULTIPOLYGON
    this.addPolygonFeatureToEditWS = function () {
        var merrShtresat = map.getLayersByName("V_GIS_Layer_GPS")[0];
        var linearRing = new OpenLayers.Geometry.LinearRing(merrShtresat.features[0].geometry.components);

        var polygon = new OpenLayers.Geometry.Polygon([linearRing]);
        var rectangleFeature = new OpenLayers.Feature.Vector(polygon);

        if (gisEditimImport.validateFeatureBeforeSave(linearRing, rectangleFeature)) {
            gisEditimImport.addNewFeaturesToEditWFS(rectangleFeature);
            gisEditimImport.destroyWin();
        }
    };

    // Validon te dhenat e gpx per tipin e te dhenave edhe nese mund te konvertohen ne tipin e layerit
    this.validateFeatureBeforeSave = function (linearRing, feature) {
        var validate = true;
        if (linearRing.components.length == 0 || feature.geometry.CLASS_NAME != "OpenLayers.Geometry.Polygon") {
            validate = false;
            noty({ text: perkthe("GP_ERROR_TIPI_MSG_UPLOAD_SHP"), type: "error" });
        };
        return validate;
    };

    // Ngarkon ne gjeometrine e file GPX ne objektin e editimit
    this.addNewFeaturesToEditWFS = function (feature) {
        if (varSettings.editim.objLidhjeGisWeb.veprimi == "INSERT") {
            drawPolygon.deactivate();
            wfsEdit.events.remove('beforefeaturemodified');
            AfishoPopupInsertUpdate({ feature: feature }, varSettings.editim.objLidhjeGisWeb.IDLAYER, { ListatAtributeve: [] }, "INSERT");
        }
        else {
            feature.attributes = wfsEdit.features[0].attributes;
            feature.data = wfsEdit.features[0].data;
        }

        wfsEdit.destroyFeatures();
        wfsEdit.addFeatures([feature]);
        eventInserUpdateAtribute = null;
        modifiko.activate();
        modifiko.selectFeature(feature);
    };

    // Krijohet Layeri Temporal qe ben te mundur 
    this.createTempLayerGPX = function (urlUploadSkedar) {
        var gpxStyles = createOpenLayerStyleMap("EditimImportFileGPX");

        gisEditimImport.removeLayerByName("V_GIS_Layer_GPS");
        var tempLayer = new OpenLayers.Layer.Vector("V_GIS_Layer_GPS", {
            strategies: [new OpenLayers.Strategy.Fixed()],
            protocol: new OpenLayers.Protocol.HTTP({
                url: urlUploadSkedar,
                format: new OpenLayers.Format.GPX({
                    extractAttributes: true,
                    externalProjection: new OpenLayers.Projection(projektioniKorGeo), 
                    internalProjection: new OpenLayers.Projection(projeksioniGeo)
                })
            }),
            styleMap: gpxStyles,
        });
        map.addLayer(tempLayer);
    };



    // Kapen Eventet e Objekteve pas deklarimit qe te jene bashke ne nje klase
    this.inicializoObjectsListener = function () {
        Ext.getCmp("IDEditimBntImportSkedar").addListener("validator", this.handlerListenerEditimBntValidatorSkedar, true);
        Ext.getCmp("IDEditimBntImportSkedar").addListener("fileselected", this.handlerListenerEditimBntImportSkedar, false);

        Ext.getCmp("IDEditimImportMenuNgarko").on('click', this.handlerClickListenerEditimImportMenuNgarko);
        Ext.getCmp("IDEditimImportMenuZoomTo").on('click', this.handlerClickListenerEditimImportMenuZoomTo);
        Ext.getCmp("IDEditimImportMenuFshi").on('click', this.handlerClickListenerEditimImportMenuFshi);
        Ext.getCmp("IDEditimImportMenuRuaj").on('click', this.handlerClickListenerEditimBntRuajObjekt);

        Ext.getCmp("ImportDritareID").on('close', this.destroyWin);
        Ext.getCmp("ImportDritareID").on('destroy', this.destroyWin);
    };

    // Eventi i Zgjedhjes se File nga Kompjuteri
    this.handlerListenerEditimBntImportSkedar = function (v) {
        gisEditimImport.enableWindowsElements(true, false, false, false, true);
    };

    // Eventi i Zgjedhjes se File nga Kompjuteri
    this.handlerListenerEditimBntValidatorSkedar = function (v) {
        if (!/\.gpx$/.test(v)) {
            noty({ text: perkthe("GP_ERROR_TIPI_MSG_UPLOAD_SHP"), type: "error" });
            return false;
        }
        return true;
    };

    // Eventi i Upload te file ne sistem
    this.handlerClickListenerEditimImportMenuNgarko = function (v) {
        var formUploadSkedar = gisElements.getGISElement("IDEditimImportFormPanel");

        formUploadSkedar.getForm().submit({
            url:'GISUpload.aspx', 
            waitMsg: 'Uploading ...',
            success: function (formUploadSkedar, action) {
                var resultMsg = JSON.parse(action.result.message);
                var tempLayerGPS = gisEditimImport.createTempLayerGPX(resultMsg.PATH + resultMsg.FILENAME);
                gisEditimImport.enableWindowsElements(false, true, true, true, true);
            },
            failure: function (formUploadSkedar, action) {
                noty({ text: action.result.message, type: "error" });
            }
        });
    };

    // Afrohesh ne elementin e kerkuar
    this.handlerClickListenerEditimImportMenuZoomTo = function (v) {
        var merrShtresatGPS = map.getLayersByName("V_GIS_Layer_GPS")[0];
        map.zoomToExtent(merrShtresatGPS.getDataExtent());
    };

    // Heq zgjedhjen e bere
    this.handlerClickListenerEditimImportMenuFshi = function (v) {
        gisElements.getGISElement("IDEditimImportFormPanel").getForm().reset();
        gisEditimImport.enableWindowsElements(false, false, false, false, true);

        gisEditimImport.removeLayerByName("V_GIS_Layer_GPS");
    };

    // Eventi per importin e Skedarit tek shtresa e kerkuar
    this.handlerClickListenerEditimBntRuajObjekt = function (v) {
        var layerEditimGeomType = varSettings.webConfig.layers.filter(function (layer) {
            return (layer.IDLAYER.toString() == varSettings.editim.objLidhjeGisWeb.IDLAYER)
        })[0].GEOMETRYLAYER;

        switch (layerEditimGeomType) {
            case "POLYGON":
            case "MULTIPOLYGON":
                gisEditimImport.addPolygonFeatureToEditWS();
                break;
            default:
                break;
        }
    };
};

MzhuDritareInfo = function ()
{
    //Ketu fillon krijimi i dritares se personalizuar per MZHU
    this.inicializo = function (items) {
        this.shkaterroDritareTePersonalizuarPerMZHU();
        var mzhuDritareInfoTabet = items.map(function (item, index) {
            return this.ndertoTabDritareTePersonalizuarPerMZHU(item);
        }, this);

        this.krijoDritareTePersonalizuarPerMZHU(mzhuDritareInfoTabet);
    };

    //Nese dritare ekziston, ajo shkaterrohet pasi nuk eshte me ne interes
    this.shkaterroDritareTePersonalizuarPerMZHU = function () {
        if (Ext.getCmp("IDMzhuDritareInfoPersonalizuar") != undefined)
            Ext.getCmp("IDMzhuDritareInfoPersonalizuar").destroy();
    };

    //Krijohet dritarja
    this.krijoDritareTePersonalizuarPerMZHU = function (mzhuDritareInfoTabet) {
        var mzhuWinTitle = "<img border='0' src='" + varSettings.theme.urlCustom + "infoTitle.png' />  " + perkthe("GP_WIN_MZHUINFO_Title");
        var mzhuTabPanel = gisElements.createGISElement("TabPanel", { id: "IDMzhuDritareTabePersonalizuar", width: 760, cls: 'mzhuInfoHeaderPanelTabe', items: mzhuDritareInfoTabet });
        gisElements.createGISElement("Window", { title: mzhuWinTitle, id: "IDMzhuDritareInfoPersonalizuar", width: 770, height: 640, border: false, cls: ' mzhuInfoHeaderWindow', items: mzhuTabPanel });
    };

    //Cdo venddepozitim krijohet si tab me vete per dritaren
    this.ndertoTabDritareTePersonalizuarPerMZHU = function (item) {
        var mzhuDritareInfoTabPanelTitle = this.ktheMzhuInfoAttributeValue(item.item.Bashkia, item.item.Municipality);
        var mzhuDritareInfoPersonalizuarHtml = '<div id="idMzhuInfoHeaderPanel" class="mzhuInfoHeaderPanel">' + this.ktheMzhuInfoHeader(item) + this.ktheMzhuInfoBody(item) + this.ktheMzhuInfoFooter(item) + '</div>';
        mzhuDritareInfoPersonalizuarHtml = new Ext.XTemplate("<a href='#'>{value}").apply({
            value: mzhuDritareInfoPersonalizuarHtml
        });
        return gisElements.createGISElement("Panel", { title: mzhuDritareInfoTabPanelTitle, closable: false, html: mzhuDritareInfoPersonalizuarHtml });
    };

    //Krijohet Header per cdo tab qe permban imazhin e venddepozitimit, te dhena baze (popullsi, sanitar), si dhe hapet dokumenti i skices
    this.ktheMzhuInfoHeader = function (item) {
        var mzhuInfoHeaderIsSVal = this.ktheMzhuInfoAttributeValue(item.item.EshteSanitar, item.item.Sanitary);
        var mzhuInfoHeaderIsSLanguage = (mzhuInfoHeaderIsSVal == "SANITAR") ? perkthe("GP_WIN_MZHUINFO_HeaderSanitar") : perkthe("GP_WIN_MZHUINFO_HeaderJoSanitar");
        var tableClassName = (mzhuInfoHeaderIsSVal == "SANITAR") ? "mzhuInfoHeaderTabelaGreen" : "mzhuInfoHeaderTabelaRed";
        var mzhuDritareInfoPersonalizuarHtml =
        ' <div id="idMzhuInfoHeaderBackground" class="mzhuInfoHeaderBackground" style="background-image: url(' + this.ktheMZHULinksPerDritareInfo(item.arkiva, "png") + ');"> ' +
        '   <table class=' + tableClassName + '><tbody> ' +
        '       <tr class="mzhuInfoHeaderTabelaTrCss1"><td class="mzhuInfoHeaderTabelaTdCss1">' + perkthe("GP_WIN_MZHUINFO_HeaderPopVal") + '</td><td class="mzhuInfoHeaderTabelaTdCss2">' + this.ktheMzhuInfoAttributeValue(item.item.Popullsia, item.item.Population) + '</td><td class="mzhuInfoHeaderTabelaTdCss3">' + perkthe("GP_WIN_MZHUINFO_HeaderHabitant") + '</td></tr> ' +
        '       <tr class="mzhuInfoHeaderTabelaTrCss1"><td class="mzhuInfoHeaderTabelaTdCss1">' + perkthe("GP_WIN_MZHUINFO_HeaderQuaVal") + '</td><td class="mzhuInfoHeaderTabelaTdCss2">' + this.ktheMzhuInfoAttributeValue(item.item.Sasia, item.item.Quantity) + '</td><td class="mzhuInfoHeaderTabelaTdCss3">' + perkthe("GP_WIN_MZHUINFO_HeaderTonDay") + '</td></tr> ' +
        '       <tr class="mzhuInfoHeaderTabelaTrCss2"><td class="mzhuInfoHeaderTabelaTdCss1"><label class="mzhuInfoHeaderTabelaLabelCss1">' + mzhuInfoHeaderIsSLanguage + '</label></td></tr>' +
        '   </tbody></table>' +
        '   <div class="mzhuInfoHeaderDivSkica">' +
        '       <button class="mzhuInfoHeaderSkicaButton" onclick="hapDritareDokumentim(' + this.ktheMZHULinksPerDritareInfo(item.arkiva, "pdf") + ')">' + perkthe("GP_WIN_MZHUINFO_Skica") + '</button> ' +
        '   </div> ' +
        ' </div>';

        return mzhuDritareInfoPersonalizuarHtml;
    };

    //Krijohet Body per cdo tab me te dhenat tabelare e atributeve te cdo venddepozitimi
    this.ktheMzhuInfoBody = function (item) {
        var mzhuDritareInfoPersonalizuarHtml =
        ' <div id="idMzhuInfoBodyPanel" class="mzhuInfoBodyPanel">' +
        ' <div class="mzhuInfoBodyDivAttribute">' +
        '   <table class="mzhuInfoBodyTable"><tbody> ' +
        '       <tr><td rowspan="15" class="mzhuInfoBodyTableTdCss1">' + perkthe("GP_WIN_MZHUINFO_BodyDistance") + '</td></tr> ' +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodySettVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_vendbanimet, item.item.Distance_from_settlements)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyNatiVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_rruget_nacionale, item.item.Distance_from_national_roads)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyWatLVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_brigje_ujore, item.item.Distance_from_water_banks)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyWatPVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_burime_ujore, item.item.Distance_from_water_resources)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyAirpVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_aeroport, item.item.Distance_from_airports)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyHousVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_shtepite, item.item.Distance_from_houses)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyRailVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_hekurudha, item.item.Distance_from_railway)) +
                this.createMzhuBodyHtmlElement(perkthe("GP_WIN_MZHUINFO_BodyCentVal"), this.ktheMzhuInfoAttributeValue(item.item.Distanca_nga_qendra_e_qytetit, item.item.Distance_from_center)) +
        '       <tr><td width="100%" font-style="italic">' + this.ktheMzhuInfoAttributeValue(item.item.Tjeter, item.item.Other) + '</td></tr>' +
        '   </tbody></table>' +
        ' </div></div>';

        return mzhuDritareInfoPersonalizuarHtml;
    };

    //Krijohet Footer per cdo tab me imazhet per cdo venddepozitimi (format .jpg)
    this.ktheMzhuInfoFooter = function (item) {
        var mzhuDritareInfoPersonalizuarHtml =
        ' <div id="idMzhuInfoFooterPanel" class="mzhuInfoFooterPanel"> ' +
        ' <div class="mzhuInfoFooterDivImages" id="links"> ' + this.ktheMZHULinksPerDritareInfo(item.arkiva, "jpg") + ' </div></div>';

        return mzhuDritareInfoPersonalizuarHtml;
    };

    //Krijohet linku per cdo imazh/dokument te kerkuar. Skica eshte ne format .pdf, venddepozitimi ne format .png, te tjerat ne .jpg, jpeg etj 
    this.ktheMZHULinksPerDritareInfo = function (item, lloji) {
        var keywords = [];
        keywords.push(lloji);
        var url = "";

        switch (lloji) {
            case "png":
                var filterArkive = $.grep(item, function (s) { return s.FileName.match(keywords) });
                url = ((filterArkive.length > 0) ? this.createMzhuUrlFromArkivaPath(filterArkive[0].Path) : this.createMzhuDefaultUrlMissingData("Background.png"));
                break;
            case "pdf":
                var filterArkive = $.grep(item, function (s) { return s.FileType.match(keywords) });
                url = ((filterArkive.length > 0) ? "\'" + this.createMzhuUrlFromArkivaPath(filterArkive[0].Path) + "\'" : "\'" + this.createMzhuDefaultUrlMissingData("Skica.pdf") + "\'");
                break;
            default:
                var filterArkive = $.grep(item, function (s) { return s.FileType.match(/^(?!.*pdf).*$/m) && s.FileType.match(/^(?!.*png).*$/m) });
                if (filterArkive.length > 0)
                    url = filterArkive.map(function (elementFooter, index) {
                        return this.createMzhuFooterHtmlElement(this.createMzhuUrlFromArkivaPath(elementFooter.Path));
                    }, this).join('');
                else
                    url = this.createMzhuFooterHtmlElement(this.createMzhuDefaultUrlMissingData("Missing.jpg")) + this.createMzhuFooterHtmlElement(this.createMzhuDefaultUrlMissingData("Missing.jpg")) + this.createMzhuFooterHtmlElement(this.createMzhuDefaultUrlMissingData("Missing.jpg"));
                break;
        }
        return url;
    };

    //Kthen vleren e atributeve sipas gjuhes se loguar ne sistem
    this.ktheMzhuInfoAttributeValue = function (valueAl, valueEn) {
        return ((valueAl != undefined) ? valueAl : ((valueEn != undefined) ? valueEn : '-'));
    };

    //Kthen vlera default nese mungojne dokumentat ose imazhet qe te mos dale i bardhe
    this.createMzhuDefaultUrlMissingData = function (element) {
        return window.location.origin + "/Arkiva/" + varSettings.webConfig.ndermarrjeLogin.IdNdermarrje + "/142/" + element;
    };

    //Kthen linkun e arkives duke shfrytezuar fushen 'Path', te ruajtur ne db
    this.createMzhuUrlFromArkivaPath = function (path) {
        return window.location.origin + "/Arkiva/" + path.replace(/\\/g, "/").split('Arkiva/')[1];
    };

    //Kthen Html per nje imazh te footer
    this.createMzhuFooterHtmlElement = function (elementUrl) {
        return '<a data-dialog="" href="' + elementUrl + '"><img class="mzhuInfoFooterImage" src="' + elementUrl + '" alt=""></a> '
    };

    //Kthen Html per nje rresht atributesh 
    this.createMzhuBodyHtmlElement = function (elementDescription, elementValue) {
        return '<tr><td width="80%">' + elementDescription + '</td><td class="mzhuInfoBodyTableTdCss2" width="20%">' + elementValue + '</td></tr>'
    };
};

