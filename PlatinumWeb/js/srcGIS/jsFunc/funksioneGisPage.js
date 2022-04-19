/// <reference path="../jsClasses/UndoRedo.js" />
var identifikuesPerPopupMagazina = 'GisDefault';
var identifikuesPerPopupAsete = 'GisDefault';
var objSelektuarNgaLupa;
var selektuarVleraNgaKerkimi;
var LayerStoreKerkimShpejte = {};

//Seksioni i stileve
stilVektorTempPerSelectObjekti = new OpenLayers.StyleMap({
    "select": new OpenLayers.Style({
        strokeColor: "#32ead6",
        fillColor: "#32ead6",
        strokeWidth: 2,
        strokeOpacity: 1,
        fillOpacity: 0.7
    }),
    "default": new OpenLayers.Style({
        strokeColor: "${colorObj}",
        fillColor: "${colorObj}",
        strokeWidth: 2,
        strokeOpacity: 1,
        fillOpacity: 0.7,
        pointRadius: 5
    }, {
        context: {
            colorObj: function (feature) {
                if (varSettings.editim.objZgjedhurMerge) return '#f6e487';
                else return '#32ead6';
            }
        }
    })
});

stilWfsEditVertex = new OpenLayers.Style({
    strokeColor: "${colorVertex}",
    fillColor: "${colorVertex}",
    strokeOpacity: 0.6,
    strokeWidth: 2,
    pointRadius: 4,
    graphicName: "circle",
}, {
    context: {
        colorVertex: function (feature) {
            if (varSettings.editim.objLidhjeGisWeb.veprimi == "UPDATE") return '#ff0000';
            else return '#7782ee';
        }
    }
});

stilWfsEditVertexVirtual = {
    strokeWidth: 0.5,
    strokeColor: "#ff0000",
    strokeOpacity: 0.5,
    fillColor: "#ff0000",
    fillOpacity: 0.5,
    pointRadius: 4,
    graphicName: "cross"
};

stilWfsEdit = new OpenLayers.StyleMap({
    "default": new OpenLayers.Style({
        strokeColor: "${colorDefaultEdit}", //externalGraphic: varSettings.theme.urlCustom + 'editMove.png', graphicWidth: 7, graphicHeight: 7, graphicYOffset: 0, graphicOpacity: 1
        fillColor: "${colorDefaultEdit}",
        strokeWidth: 2,
        strokeOpacity: 1,
        fillOpacity: 0.7,
        pointRadius: 5
    }, {
        context: {
            colorDefaultEdit: function (feature) {
                if (feature.state == "Insert") return '#f6e487';
                else if (modifiko && modifiko.feature == feature) return '#f6e487';
                else if (feature.state == "Update") return '#f6e487';
                else return 'transparent';
            }
        }
    }),
    "select": new OpenLayers.Style({
        strokeColor: "${colorSelectEdit}",
        fillColor: "${colorSelectEdit}",
        strokeWidth: 2,
        strokeOpacity: 1,
        fillOpacity: 0.7,
        pointRadius: 5
    }, {
        context: {
            colorSelectEdit: function (feature) {
                return '#32ead6';
            }
        }
    }),
    "vertex": stilWfsEditVertex
}, { extendDefault: true });


function createOpenLayerStyleMap(ID) {
    switch (ID) {
        case "EditimImportFileGPX":
            return new OpenLayers.StyleMap({
                "default": new OpenLayers.Style({
                    pointRadius: "4",
                    label: "${name}",
                    labelAlign: 'cb',
                    fontSize: 11,
                    fontFamily: "Arial",
                    fontColor: "red",
                    labelYOffset: 6,
                    fillColor: "#ffffff",
                    strokeColor: "#d55d5d",
                    strokeWidth: 3,
                    strokeOpacity: 1,
                    strokeDashstyle: 'solid'
                }),
                "select": new OpenLayers.Style({
                    fillColor: "#66ccff",
                    strokeColor: "#3399ff",
                    graphicZIndex: 2
                })
            });
        default:
            break;
    }
};

function perkthe(celesi) {
    return varSettings.translateArray[celesi];
}

$.ajax({
    async: false,
    url: Utils.getServerApiUrl("GIS", "getAllObjectsForQuickSearch")
}).done(function (result) {
    LayerStoreKerkimShpejte = new Ext.data.JsonStore({ fields: [emriKolKerkimShpejteComboValue, emriKolKerkimShpejteComboAfish, emriKolKerkimShpejteComboAfish2], data: result });
});

function MerrAtribute() {
    var AllLayersForMap = varSettings.webConfig.layers;
    varSettings.info.layesStore = AllLayersForMap.filter(function (layers) {
        return ( ((layers.IDGRSTRUCTURE == 1 && layers.IDLAYERSTYPE == 3) || layers.IDGRSTRUCTURE == 2 ) && layers.D_AMB );
    }).map(function (layer, index) {
        layer = map.getLayersBy("IdLayer", layer.IDLAYER)[0];
        varSettings.info.windowTitles[layer.IDENTIFICATION] = { emerLayerPerkthyer: layer.name };
        return layer;
    });

    featureInfo = addInfoControlToMap(varSettings.info.layesStore.filter(function (layers) { return layers })); 
    map.addControl(featureInfo);
};

function addInfoControlToMap(layers){
    tempfeatureInfo = new OpenLayers.Control.WMSGetFeatureInfo({
        queryVisible: true,
        highlightOnly: false,
        drillDown: true,
        maxFeatures: 15,
        infoFormat: 'application/json',
        layers: layers,
        eventListeners: {
            "beforegetfeatureinfo": function onBeforeGetFeatureInfo(event) {
                BeforeGetFeatureInfo(featureInfo, event)
            },
            "getfeatureinfo": function (response) {
                GetFeatureInfo(featureInfo, response)
            }
        }
    });
    return tempfeatureInfo;
}

function BeforeGetFeatureInfo(featureInfoSipasStruktures, event) {
    var layers = featureInfoSipasStruktures.findLayers();
    var myFilter = [];
    var filter = "";
    for (var i = 0, len = layers.length; i < len; i++) {
        var lyrCQL = layers[i].params.CQL_FILTER
        if (lyrCQL != null) {
            myFilter.push(lyrCQL);
        }
    }
    myFilter = myFilter.join(';');
    featureInfoSipasStruktures.vendorParams = { 'CQL_FILTER': myFilter };
};

function GetFeatureInfo(featureInfoSipasStruktures, response) {
    var items = [];
    var windowInfoItems = [];
    tabelaAtributeve = {};
    features = JSON.parse(response.text);
    var idFeature = 0; var layerName = ''; var gid = 0;
    if (typeof features["features"] == "undefined" || typeof features["features"][0] == "undefined") {
        console.log('(typeof features["features"] == "undefined" || typeof features["features"][0] == "undefined") == true');
        return;
    }

    var featuresPaPerseritje = ktheArrayPaPerseritje(features["features"], "gid");

    for (var i = 0; i < featuresPaPerseritje.length; i++) {
        var idFeature = featuresPaPerseritje[i].id;
        var layerName = idFeature.substring(0, idFeature.indexOf('.'));
        var gid = featuresPaPerseritje[i].properties["gid"];

        $.ajax({
            url: Utils.getServerApiUrl("GIS", "GetFeatureInfo"), 
            data: JSON.stringify({ gId: gid, layerName: layerName, indeksi: i })
        }).done(function (result) {
            var teDhenat = result.teDhenat;
            var indeksi = result.indeksi;
            var idLayerType = result.idLayerType;
            var webLloji = result.webLloji;
            feature = null;
            feature = featuresPaPerseritje[indeksi];
            feature.gml = {};
            feature.gml.featureType = feature.id.split(".")[0];
            feature.fid = feature.id;
            feature.attributes = feature.properties;
            var statusLayer = feature.properties.NRSTATUSI;
            delete feature.properties.NRSTATUSI;
            tabelaAtributeve = {};
            var kodiUnik = '';

            if (teDhenat !== "") {
                $.extend(feature.attributes, JSON.parse(teDhenat)[0]);
                var atrFushaShtese = JSON.parse(teDhenat)[0];
                var keyFushaShtese = Object.keys(atrFushaShtese);
                var keyKolonaAfishuara = Object.keys(varSettings.info.windowTitles[feature.gml.featureType]);

                for (var l = 0; l < keyKolonaAfishuara.length; l++) {
                    if (keyFushaShtese.indexOf(keyKolonaAfishuara[l]) !== -1)
                        delete atrFushaShtese[keyKolonaAfishuara[l]];
                }
                if (webLloji == "MAGAZINA")
                    kodiUnik = atrFushaShtese[perkthe("GP_INFO_Kodi")]; 
                else if (webLloji == "SERIALE")
                    kodiUnik = atrFushaShtese[perkthe("GP_INFO_Serial")];
                else
                    kodiUnik = gid
            }
            for (var k in feature.attributes) {
                if (varSettings.info.windowTitles[feature.gml.featureType][k]) {
                    tabelaAtributeve[varSettings.info.windowTitles[feature.gml.featureType][k]] = feature.attributes[k];
                }
            }
            if (teDhenat !== "")
                $.extend(tabelaAtributeve, atrFushaShtese);
            feature.attributes = tabelaAtributeve;//tabelaAtributeve

            createListOfItems(items, varSettings.info.windowTitles, kolonatFshehurneInfo, tabelaAtributeve, '', gid, webLloji, idLayerType, kodiUnik, statusLayer, feature.gml.featureType);
            afishoPopupInformacion(response, items, featuresPaPerseritje, idLayerType);

            if (feature.gml.featureType == "V_GIS_Layer_MZHULANDFILLS") {
                windowInfoItems.push({ gid: gid, item: tabelaAtributeve, arkiva: result.arkivaDok });
            }
            if (windowInfoItems.length > 0 && i == featuresPaPerseritje.length) {
                new MzhuDritareInfo().inicializo(windowInfoItems);
            }
        });
    }
};

function ktheArrayPaPerseritje(array, key) {
    var arr = {};
    for (var i = 0, len = array.length; i < len; i++)
        arr[array[i].properties[key]] = array[i];

    var featuresPaPerseritje = new Array();
    for (var key in arr)
        featuresPaPerseritje.push(arr[key]);
    return featuresPaPerseritje;
}

function createListOfItems(items, titles, kolonatFshehurneInfo, newFeatureAttributes, pergjigjja, gIdPerTitle, webLloji, idLayerType, kodiUnik, statusLayer, layerIdentification) {
    var newFeatureAttributes = JSON.parse(JSON.stringify(newFeatureAttributes).replace(/_/g, " "));

    if (typeof newFeatureAttributes.gid != "undefined")
        delete newFeatureAttributes.gid;

    if (webLloji == "MAGAZINA" && statusLayer != 3)
        newFeatureAttributes["Elementet"] = kodiUnik;

    newFeatureAttributes["File"] = kodiUnik;    

    if (layerIdentification == "T_GIS_Layer_NEWALBANIAMUNICIPALITIES") {
        var newKolone = (newFeatureAttributes.Code == undefined ? newFeatureAttributes.Kodi : newFeatureAttributes.Code);
        newFeatureAttributes["MZHU"] = newKolone;
    }
    if (layerIdentification == "V_GIS_Layer_DPSH_RASTEKB_NR1" || layerIdentification == "V_GIS_Layer_DPSH_KONTROLLTERRITORI_NR1" || layerIdentification == "V_GIS_Layer_DPSH_MONITORIMIAJROR_NR1" || layerIdentification == "V_GIS_Layer_DPSH_INFOSHKRESA_NR1") {
        delete newFeatureAttributes.Seriali;
        delete newFeatureAttributes.Serial;
    }

    items.push({
        xtype: "propertygridT",
        id: "grid" + feature.fid,
        title: titles[feature.gml.featureType]["emerLayerPerkthyer"], //+ "." + gIdPerTitle,
        clicksToEdit: 1,
        source: newFeatureAttributes,//feature.attributes,
        cls: 'popPanel' + varSettings.theme.color,
        listeners: {
            'beforeedit': {
                fn: function () {
                    return false;
                }
            }
        },
        customRenderers: {
            'dokumentim': function (v) {
                return pergjigjja;
                if (v) {
                    return '<a href=\'javascript:hapDritareDokumentim("' + urlDokumentimiUpload + v + '");\'>' + v + '</a>';
                }
                else {
                    return "";
                }
            },
            'Elementet': function (elem) {
                if (elem)
                    return '<a href=\'javascript:hapRaportin("idraporti=234","kodiMagGis=' + elem + '");\'> ' + elem + '</a>';
                else
                    return "";
            },
            'File': function (elem) {
                if (elem)
                    return '<a href=\'javascript:hapArkiven("' + elem + '", "' + webLloji + '", "' + idLayerType + '", "' + statusLayer + '");\'>' + perkthe("GP_INFO_Arkiva") + '</a>';
                else
                    return "";
            },
            'MZHU': function (elem) {
                if (elem)
                    return '<a href=\'javascript:hapRaportin("emriReal=MZHUKostoPerVit","kodDegeAdministrative=' + elem + '");\'>Kosto_Ton/Vit</a>' +
                    '&nbsp;&nbsp;&nbsp;<a href=\'javascript:hapRaportin("emriReal=MZHUKostoPerBanor","kodDegeAdministrative=' + elem + '");\'>Kosto_Banor/Vit</a>';
                else
                    return "";
            },
        },
        propertyNames: {
            'dokumentim': titles[feature.gml.featureType]['dokumentim'], //ndryshon label
            'Elementet': perkthe("GP_INFO_Elementet"), 
            'MZHU': perkthe("GP_INFO_Raport"),
        }
    });
}

function hapArkiven(elem, webLloji, idLayerType, statusLayer) {
    switch (webLloji)
    {
        case "MAGAZINA":
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "ktheIdMagNgaKodi"),
                data: JSON.stringify({ kodMag: elem, idNdermarrje: varSettings.webConfig.idNdermarrja })
            }).done(hapLupeArkive);
            break;
        case "SERIALE":
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "ktheIdSerialNgaKodi"),
                data: JSON.stringify({ kodSeriali: elem, idNdermarrje: varSettings.webConfig.idNdermarrja })
            }).done(hapLupeArkive);
            break;
        default:
            hapLupeArkive({ idObjekti: elem, veprimi: "gis" })
            break;
    }
}

function hapLupeArkive(result) {
    if (result.d)
        result = result.d;
    popupUniversal.SetHeaderText(perkthe("GP_INFO_Arkiva"));
    popupUniversal.SetContentUrl('LupaArkiva.aspx');
    popupUniversal.SetSize(720, 570);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=GIS&veprimi=' + result.veprimi + '&idDok=' + result.idObjekti);
    popupUniversal.Show();
}

function afishoPopupInformacion(e, items, featuresPaPerseritje, idLayerType) {
    var features = (JSON.parse(e.text))['features'];
    if (featuresPaPerseritje.length == items.length)
        afishoPopupInformacionSipasNrTeSelectuar(e, items, features, idLayerType);
}

function afishoPopupInformacionSipasNrTeSelectuar(e, items, features, idLayerType) {
    return new GeoExt.Popup({
        title: perkthe("GP_INFORMACION_WIN_TIT"),
        region: "east",
        width: 400,
        height: 350,
        layout: "accordion",
        autoScroll: true,
        map: map,
        anchored: true,
        anchorPosition: "bottom-right",
        location: e.xy,
        maximizable: true,
        resizable: true,
        collapsible: false,
        bodyStyle: 'background-color:#FFF;font-size:14px;',
        cls: 'popWindow' + varSettings.theme.color,
        items: items
    }).show();
}

function afishoMesazhMungonDok() {
    noty({ text: perkthe("GP_MSG_MSG_ERR_skaDokument"), type: "warning" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_ERR_skaDokument"))
}

function hapDritareDokumentim(bigurl) {
    var dokumentPanelItems = {
        xtype: 'component',
        autoEl: {
            tag: 'iframe',
            style: 'height: 100%; width: 100%; border: none',
            src: bigurl
        }
    };
    gisElements.createGISElement("Panel", { id: "dokumentPanelId", width: 600, height: 400, items: dokumentPanelItems });
    gisElements.createGISElement("Window", { title: emerKoloneDokumentinKonf, id: "windowDokumentimId", width: 700, x: 400, y: 100, maximizable: true, resizable: true, layout: 'fit', closeAction: 'close', items: gisElements.getGISElement("dokumentPanelId") });
}

function fromDegreeToKartezian(grade, min, sek) {
    if (typeof grade != "undefined" && grade != '')
        grade = parseFloat(grade);
    else
        grade = 0;
    if (typeof min != "undefined" && min != '')
        min = parseFloat(min);
    else
        min = 0;
    if (typeof sek != "undefined" && sek != '')
        sek = parseFloat(sek);
    else
        sek = 0;

    var result = grade + min / 60 + sek / 3600;
    return parseFloat(result).toFixed(6);
}

function formatoKoordinatat(coordinate, axis, dmsOption) {
    var p = new Object();
    if (!dmsOption) {
        dmsOption = 'dms';    //default to show degree, minutes, seconds
    }
    coordinate = (coordinate + 540) % 360 - 180; // normalize for sphere being round

    var abscoordinate = Math.abs(coordinate);
    var coordinatedegrees = Math.floor(abscoordinate);

    var coordinateminutes = (abscoordinate - coordinatedegrees) / (1 / 60);
    var tempcoordinateminutes = coordinateminutes;
    coordinateminutes = Math.floor(coordinateminutes);
    var coordinateseconds = (tempcoordinateminutes - coordinateminutes) / (1 / 60);
    coordinateseconds = Math.round(coordinateseconds * 10);
    coordinateseconds /= 10;

    if (coordinateseconds >= 60) {
        coordinateseconds -= 60;
        coordinateminutes += 1;
        if (coordinateminutes >= 60) {
            coordinateminutes -= 60;
            coordinatedegrees += 1;
        }
    }

    if (coordinatedegrees < 10) {
        coordinatedegrees = "0" + coordinatedegrees;
    }
    var str = coordinatedegrees + "\u00B0";

    if (dmsOption.indexOf('dm') >= 0) {
        if (coordinateminutes < 10) {
            coordinateminutes = "0" + coordinateminutes;
        }
        str += coordinateminutes + "'";

        if (dmsOption.indexOf('dms') >= 0) {
            if (coordinateseconds < 10) {
                coordinateseconds = "0" + coordinateseconds;
            }
            str += coordinateseconds + '"';
        }
    }
    p.koordinate = str;
    var str1;
    if (axis == "lon") {
        str1 = coordinate < 0 ? OpenLayers.i18n("W") : OpenLayers.i18n("E");
    } else {
        str1 = coordinate < 0 ? OpenLayers.i18n("S") : OpenLayers.i18n("N");
    }
    p.str = str1;
    return p;
}

function merrMousePositionFL(lonLat) {
    lonLat = lonLat.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projektioniKorGeo));
    var lat = lonLat.lat;
    var lon = lonLat.lon;
    var ns = formatoKoordinatat(lat);
    var ew = formatoKoordinatat(lon, 'lon');

    return '<table style="width:200px;height:15px;table-layout:fixed "><td width=15px>' + ew.str + '</td><td width=75px>' + ew.koordinate + '</td><td width=15px>' + ns.str + '</td><td width=75px>' + ns.koordinate + ' </td> </table>';
}

function formatoKordinatatMouseNav(lonLat) {
    var showMouseNavigation = "X:" + lonLat.lon + ", Y:" + lonLat.lat;

    lonLat = lonLat.transform(new OpenLayers.Projection(projeksioniGeoV), new OpenLayers.Projection(projektioniKorGeo));
    var ns = formatoKoordinatat(lonLat.lat);
    var ew = formatoKoordinatat(lonLat.lon, 'lon');

    var projeksioneDescription = $.grep(varSettings.webConfig.projeksione, function (e) { return e.DisplayCode == projeksioniGeoV; })[0].Description;
    showMouseNavigation = projeksioneDescription + " - " + showMouseNavigation + " || WGS 84 - " + ew.str + ":" + ew.koordinate + ", " + ns.str + ":" + ns.koordinate;

    return '<div class="kordinatatPoshte"><table><td width=100%>' + showMouseNavigation + '</td></table></div>';
}

function caktivizoKontrolletAktivizoKontrollinEditim(kontrolliAktivizohetEditim, arrayKontrolletEditim, arrayButtonatEditim) {
    for (nrArrEdit = 0; nrArrEdit < arrayKontrolletEditim.length; nrArrEdit++) {
        if (kontrolliAktivizohetEditim == arrayKontrolletEditim[nrArrEdit])//kjo duhet te vleje si krahasim sepse shenjojne ne te njejtin objekt,krahasim i tille eshte me reference
        {
            if (!kontrolliAktivizohetEditim.active) {
                kontrolliAktivizohetEditim.activate();
            }
        }
        else if (arrayKontrolletEditim[nrArrEdit].active) {
            arrayKontrolletEditim[nrArrEdit].deactivate();
        }
    }
}

function rregulloToggleKerkimHapesinor(vektoriButonatKerkimHapesinor) {
    for (i = 0; i < vektoriButonatKerkimHapesinor.length; i++) {
        if (vektoriButonatKerkimHapesinor[i].pressed) {
            vektoriButonatKerkimHapesinor[i].toggle();
        }
    }
}

function caktivizoKontrolletAktivizoKontrollin(KontrolliAktivizohet, vektoriKontrollet) {
    var duhetAktivizuarKontrolli = true;
    if (KontrolliAktivizohet.active)
        duhetAktivizuarKontrolli = false;

    if (KontrolliAktivizohet == '') {
        for (i = 0; i < vektoriKontrollet.length; i++) {
            if (vektoriKontrollet[i] && vektoriKontrollet[i].active) {
                vektoriKontrollet[i].deactivate();
            }
        }
    }
    else {
        if (duhetAktivizuarKontrolli) {
            for (i = 0; i < vektoriKontrollet.length; i++) {
                if (vektoriKontrollet[i] && vektoriKontrollet[i].active) {
                    vektoriKontrollet[i].deactivate();
                }
            }
            KontrolliAktivizohet.activate();
        }
        else {
            KontrolliAktivizohet.deactivate()
            if (mapPanel.map.layers.indexOf(polygonLayerRreth) != -1)
                mapPanel.map.removeLayer(polygonLayerRreth);
        }
    }
}

function caktivizoButonatAktivizoButonin(KontrolliqeAktivizohet, ActionsVektor) {
    var duhetAktivizuar = false;
    if (KontrolliqeAktivizohet.active)
        duhetAktivizuar = true;

    $.each(ActionsVektor, function (elem) { if (ActionsVektor[elem].control.active) ActionsVektor[elem].control.deactivate(); });

    if (KontrolliqeAktivizohet != '' && duhetAktivizuar) {
        KontrolliqeAktivizohet.activate();
    }
}

function  KrijoVektorinGPX(urlSkedar, emerRuajturSkedar) {
    var gpxStyles = createOpenLayerStyleMap("EditimImportFileGPX");

    var emerLayerUnik = varSettings.uploadConfig.lgpx + emerRuajturSkedar;
    tempLayer = new OpenLayers.Layer.Vector(emerLayerUnik, {
        strategies: [new OpenLayers.Strategy.Fixed()],
        protocol: new OpenLayers.Protocol.HTTP({
            url: urlSkedar,
            format: new OpenLayers.Format.GPX({
                extractAttributes: true,
                externalProjection: new OpenLayers.Projection(projektioniKorGeo), //geografike
                internalProjection: new OpenLayers.Projection(projeksioniGeo)
            })
        }),
        styleMap: gpxStyles,
    });
    return tempLayer;
}

function ZhdukVektorinGPX(emerRuajturSkedare) {
    var emerLayerUnik = varSettings.uploadConfig.lgpx + emerRuajturSkedar;
    if (map.getLayersByName(emerLayerUnik).length > 0)
        map.removeLayer(map.getLayersByName(emerLayerUnik));

    //if (map.layers.indexOf(window[varSettings.uploadConfig.lgpx + emerRuajturSkedare]) != -1)
    //    map.removeLayer(window[varSettings.uploadConfig.lgpx + emerRuajturSkedare]);
}

function FshijSkedare(idPanel, idSkedaridCheck, btn, emRuajturFolder) {
    if (btn == "yes") {
        Ext.Ajax.request({
            url: Utils.getServerApiUrl("GIS", "fshiSkedareUpload"),
            params: { idSkedari: idSkedaridCheck, filename: emRuajturFolder, lloji: "gpx" },
            success: function (result, request) {
                ZhdukVektorinGPX(emRuajturFolder)
            }
        })
        Ext.getCmp(idPanel).remove(idSkedaridCheck);
        Ext.getCmp(idPanel).remove(idSkedaridCheck + 'deleteButon');
        Ext.getCmp(idPanel).doLayout();
    }
}

function KrijopanelinMeCheckPerSkedareUpload() {
    var checkBoxSkedarArray = new Array();
    Ext.Ajax.request({
        url: Utils.getServerApiUrl("GIS", "merrTeGjitheSkedaretUpload"),
        params: { lloji: "gpx" },
        success: function (result, request) {
            varSettings.uploadConfig.skedaretUploadArray = JSON.parse(JSON.parse(result.responseText).d);
            for (var index = 0; index < varSettings.uploadConfig.skedaretUploadArray.length; index++) {
                checkBoxSkedarArray.push({
                    xtype: "button",
                    tooltipType: "title",
                    tooltip: perkthe("GP_GPSPAN_DELBTN_TOOLTIP"),
                    icon: varSettings.theme.urlCustom + 'fileDelete.png',
                    scale: 'small',
                    width: 12,
                    height: 12,
                    id: varSettings.uploadConfig.skedaretUploadArray[index].IDSKEDARET + "deleteButon",
                    idSkedari: varSettings.uploadConfig.skedaretUploadArray[index].IDSKEDARET,
                    emerRuajtur: varSettings.uploadConfig.skedaretUploadArray[index].FILENAME,
                    handler: function (toggled) {
                        if (toggled) {
                            var idSked = this.idSkedari;
                            var emRuajtur = this.emerRuajtur;

                            Ext.MessageBox.show({
                                title: perkthe("GP_MSG_TIT_kujdes"),
                                msg: perkthe("GP_MSG_MSG_FshijGpsSkedare"),
                                buttons: Ext.MessageBox.YESNO,
                                fn: function (btn) {
                                    FshijSkedare('checkBoxSkedaret', idSked, btn, emRuajtur)
                                },
                                icon: Ext.MessageBox.QUESTION
                            });
                        }
                    }//fund butoni
                }, {
                    xtype: 'checkbox',
                    width: 220,
                    checked: false,
                    fieldLabel: '',
                    labelSeparator: '',
                    fieldValue: varSettings.uploadConfig.skedaretUploadArray[index].FILENAME,
                    boxLabel: varSettings.uploadConfig.skedaretUploadArray[index].SHENIME + "." + varSettings.uploadConfig.skedaretUploadArray[index].LLOJI,
                    name: varSettings.uploadConfig.skedaretUploadArray[index].IDSKEDARET,
                    id: varSettings.uploadConfig.skedaretUploadArray[index].IDSKEDARET,
                    inputValue: "prove",
                    tipiValue: varSettings.uploadConfig.skedaretUploadArray[index].LLOJI,
                    listeners: {
                        check: function (checkbox, checked) {
                            if (checked) {
                                if (checkbox.tipiValue == 'gpx') {
                                    var tempLayer = KrijoVektorinGPX("UploadFiles/gpxFiles/" + checkbox.fieldValue, checkbox.fieldValue);
                                    map.addLayer(tempLayer);
                                }
                            }
                            else
                                ZhdukVektorinGPX(checkbox.fieldValue);
                        }
                    }
                }
                );
            }
            
            gisElements.createGISElement("Panel", { id: "checkBoxSkedaret", width: 600, renderTo: 'formcheckBoxSkedareUpload', bodyStyle: 'padding:0 10px 0; background-color: transparent', layout: { type: 'table', columns: 4 }, items: [checkBoxSkedarArray] });
            Ext.getCmp('checkBoxSkedaret').doLayout();
        },
        failure: function (result, request) {
            noty({ text: perkthe("GP_MSG_MSG_ERR_skedaretGps"), type: "error" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_ERR_skedaretGps"))
        }
    });
}

function suksesUploadGpx(response) {
    var karakteristikatSkedarUpload = new Array();

    karakteristikatSkedarUpload = response; //JSON.parse(response);
    if (karakteristikatSkedarUpload['success'] == 'false') {
        josuksesUploadSkedare(karakteristikatSkedarUpload['message']);
    }
    else {
        karakteristikatSkedarUpload = JSON.parse(response.message);
        Ext.getCmp('checkBoxSkedaret').add([
            {
                xtype: "button",
                tooltipType: "title",
                tooltip: perkthe("GP_GPSPAN_DELBTN_TOOLTIP"),
                icon: varSettings.theme.urlCustom + 'fileDelete.png',
                width: 12,
                height: 12,
                id: karakteristikatSkedarUpload.IDSKEDARET + "deleteButon",
                idSkedari: karakteristikatSkedarUpload.IDSKEDARET,
                emerRuajtur: karakteristikatSkedarUpload.FILENAME,
                handler: function (toggled) {
                    if (toggled) {
                        var idSked = this.idSkedari;
                        var emRuajtur = this.emerRuajtur;
                        Ext.MessageBox.show({
                            title: perkthe("GP_MSG_TIT_kujdes"),
                            msg: perkthe("GP_MSG_MSG_FshijGpsSkedare"),
                            buttons: Ext.MessageBox.YESNO,
                            fn: function (btn) {
                                FshijSkedare('checkBoxSkedaret', idSked, btn, emRuajtur)
                            },
                            icon: Ext.MessageBox.QUESTION
                        });
                    }
                }
            },
            {
                xtype: 'checkbox',
                checked: true,
                fieldLabel: '',
                labelSeparator: '',
                fieldValue: karakteristikatSkedarUpload.FILENAME,
                boxLabel: karakteristikatSkedarUpload.SHENIME + "." + karakteristikatSkedarUpload.LLOJI,
                name: karakteristikatSkedarUpload.IDSKEDARET,
                id: karakteristikatSkedarUpload.LLOJI,
                inputValue: "prove",
                tipiValue: karakteristikatSkedarUpload.LLOJI,
                listeners: {
                    check: function (checkbox, checked) {
                        if (checked) {
                            if (checkbox.tipiValue == 'gpx') {
                                var tempLayer = KrijoVektorinGPX(karakteristikatSkedarUpload.PATH + checkbox.fieldValue, checkbox.fieldValue);
                                map.addLayer(tempLayer);
                            }
                        }
                        else
                            ZhdukVektorinGPX(checkbox.fieldValue);
                    }
                }
            }
        ]);

        var tempLayerGPS = KrijoVektorinGPX(karakteristikatSkedarUpload.PATH + karakteristikatSkedarUpload.FILENAME, karakteristikatSkedarUpload.FILENAME);
        map.addLayer(tempLayerGPS);
        Ext.getCmp('checkBoxSkedaret').doLayout();

        formpanelSkedaretUpload.getForm().reset();
    }
}

function josuksesUploadSkedare(response) {
    noty({ text: response, type: "error" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), response)
}
function fshihDritareSkedaretUpload() {
    SkedaretUploadWindow.hide();
}

var formpanelSkedaretUpload;
var SkedaretUploadWindow;
function afishoDritareSkedaretUpload() {
    if (!formpanelSkedaretUpload) {
        formpanelSkedaretUpload = new Ext.FormPanel({
            renderTo: Ext.getBody(), //'gpxpaneldiv',
            formId: 'formgpx',
            fileUpload: true,
            id: "formPanelGpxUploadId",
            width: 500,
            frame: true,
            autoHeight: true,
            bodyStyle: 'padding: 10px 10px 0 10px;',
            labelWidth: 50,
            defaults: {
                anchor: '95%',
                allowBlank: false,
                msgTarget: 'side',
                isUpload: true
            },
            items: [{
                xtype: 'textfield',
                fieldLabel: perkthe("GP_GPSPAN_FORM_FIELDNAME_EMR"),
                id: 'emriRiSkedarit'
            }, {
                xtype: 'fileuploadfield',
                id: 'filenameGpx',
                name: 'filenameGpx',
                fieldLabel: perkthe("GP_GPSPAN_FORM_FIELDNAME_SKD"),
                buttonText: perkthe("GP_GPSPAN_FORM_BTN_SKD")
            }],
            buttons: [{
                text: perkthe("GP_GPSPAN_FORM_BTN_NGARKO"),
                handler: function () {
                    if (formpanelSkedaretUpload.getForm().isValid()) {
                        formpanelSkedaretUpload.getForm().submit({
                            url: 'GISUpload.aspx',
                            success: function (formpanelSkedaretUpload, action) {
                                suksesUploadGpx(action.result);
                            },
                            failure: function (formpanelSkedaretUpload, action) {
                                josuksesUploadSkedare(action.result.message);
                            }
                        });
                    }
                }
            }, {
                text: perkthe("GP_GPSPAN_FORM_BTN_PastroFushat"),
                handler: function () {
                    formpanelSkedaretUpload.getForm().reset();
                }
            }]
        });
        KrijopanelinMeCheckPerSkedareUpload();
    }

    if (!SkedaretUploadWindow) {
        SkedaretUploadWindow = new Ext.Window({
            title: perkthe("GP_WIN_TIT_NgarkoGpsFile"),
            layout: 'fit',
            closable: true,
            constrain: true,
            autoHeight: true,
            x: 400,
            y: 100,
            width: 500,
            closeAction: 'hide',
            collapsible: true,
            cls: 'popWindow' + varSettings.theme.color,
            items: formpanelSkedaretUpload,
            listeners: {
                'close': function (win) {
                },
                'hide': function (win) {
                    if (ImportSkedaretBtn.pressed)
                        ImportSkedaretBtn.toggle();
                }
            },
            html: "<div id='formcheckBoxSkedareUpload'></div>"
        });
    }
    SkedaretUploadWindow.doLayout();
    SkedaretUploadWindow.show();
}

function transformoKordinatatPike(objektiGeo, sourceProj, destProj) {
    var pika = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(objektiGeo.geometry.x.toString(), objektiGeo.geometry.y.toString()));
    pika.geometry = OpenLayers.Projection.transform(pika.geometry, new OpenLayers.Projection(sourceProj), new OpenLayers.Projection(destProj))
    return pika;
}

var printProvider1;
var printForm1;

function afishoDritarenPrintim() {
    printProvider1 = new GeoExt.data.PrintProviderT({
        method: "POST",
        encoding: 'UTF-8',
        geodetic: true,
        adresaServerit: adresaProxy + "?", //"localhost:23347/GISProxyGEO.ashx?"
        capabilities: printCapabilities,
        listeners: {
            "print": function () {
                Ext.getCmp('printimDritare1').close();
            }
        }
    });

    var extentLayerPrintim = new OpenLayers.Layer.Vector("print", {
        displayInLayerSwitcher: false,
        styleMap: new OpenLayers.StyleMap(new OpenLayers.Style(Ext.applyIf({
            pointRadius: 4,
            strokeWidth: 2,
            graphicName: "square",
            rotation: "${getRotation}",
            strokeColor: "${getStrokeColor}",
            fillOpacity: "${getFillOpacity}",
            fillColor: "${getFillColor}"
        }, OpenLayers.Feature.Vector.style["default"]), {
            context: {
                getStrokeColor: function (feature) {
                    return feature.geometry.CLASS_NAME == "OpenLayers.Geometry.Point" ? "#000" : "#4D4D9A";
                },
                getFillOpacity: function (feature) {
                    return feature.geometry.CLASS_NAME == "OpenLayers.Geometry.Point" ? 0 : 0.3;
                },
                getFillColor: function (feature) {
                    return "#7878bc";
                }
            }
        }))
    });
    printForm1 = new GeoExt.ux.MultiPagePrintT({
        id: "printForm1Id",
        mapPanel: mapPanel,
        layer: extentLayerPrintim,
        printOptions: { legend: legendpanel },
        layoutText: perkthe("GP_PrintimLayout"),
        dpiText: perkthe("GP_WIN_PRINTMF_dpiText"),
        scaleText: perkthe("GP_WIN_PRINTMF_scaleText"),
        rotationText: perkthe("GP_WIN_PRINTMF_rotationText"),
        printText: perkthe("GP_WIN_PRINTMF_printText"),
        creatingPdfText: perkthe("GP_WIN_PRINTMF_creatingPdfText"),
        addPageText: perkthe("GP_PrintimAddPage"),
        titleFieldLabel: perkthe("GP_WIN_PRINTMF_LabelTitText"),
        commentFieldLabel: perkthe("GP_WIN_PRINTMF_LabelShenText"),
        defaultTitleText: perkthe("GP_PrintimDefaultTitle"),
        defaultCommentText: "",
        legendTitleText: perkthe("GP_LEGPAN_TIT_Shpjegues"),
        legendValue: perkthe("GP_LEGPAN_TIT_Shpjegues"),
        autoFit: true,
        printProvider: printProvider1,
        bodyStyle: { padding: "5px" },
        labelWidth: 120,
        fieldsWidth: 115,
        region: "east",
        border: false,
        width: 200,
        logoNdermarrjeUrl: window.location.origin + "/" + varSettings.proxyConfig.url + 'merrLogoNderrmarrje',
        vendosLogonText: perkthe("GP_WIN_PRINTMF_LabelText"),
        GP_MSG_TIT_kujdes_text: perkthe("GP_MSG_TIT_kujdes"),
        GP_MSG_Text_NoLogoUrlPrintim_text: perkthe("GP_MSG_Text_NoLogoUrlPrintim")
    });
    
    gisElements.createGISElement("Panel", { id: "formPrintimPanelid", layout: "fit", width: 300, height: 355, items: printForm1 });

    var listenersPrintWindow = {
        'close': function (win) {
            if (varSettings.printimiPdf.ngaEditimi) {
                varSettings.printimiPdf.ngaEditimi = false;
                fshihLayerTemp("TempLayerPrindFemi", { "CQL_FILTER": 'IDAUTORIZUESI=0 AND IDNDERMVIT=0' });
            }
        }
    };
    gisElements.createGISElement("Window", { title: perkthe("GP_WIN_TIT_PRINTMF"), id: "printimDritare1", closeAction: 'close', cls: 'printWindow', items: gisElements.getGISElement("formPrintimPanelid"), listeners: listenersPrintWindow });
}

function downloadURL(url) {
    var hiddenIFrameID = 'hiddenDownloader',
    iframe = document.getElementById(hiddenIFrameID);
    if (iframe === null) {
        iframe = document.createElement('iframe');
        iframe.id = hiddenIFrameID;
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    }
    iframe.src = url;
};

var nrClick = 0;
var nrClickZoom = 0;
var nrClickKerko2 = 0;
var actions = {};

var vektoriKontrollet = new Array();//eshte vektori qe mban kontrollet(info,vizatovijePikePoligon tek kerkim hapesinor,tek editimi)

var measureStyleMap = new OpenLayers.StyleMap({
    'default': {
        strokeColor: "${favColor}",
        strokeOpacity: 1,
        strokeWidth: 1.5,
        fillColor: "#4D4D9A",
        fillOpacity: 0.8,
        pointRadius: 4,
        pointerEvents: "visiblePainted",
        label: "${labelName}\n",            //"name: ${name}\n\nage: ${age}",
        fontColor: "black",
        fontSize: "10px",
        fontFamily: "Tahoma",
        fontWeight: "bold",
        labelAlign: "${align}",
        labelXOffset: "${xOffset}",
        labelYOffset: "${yOffset}",
        labelOutlineColor: "white",
        labelOutlineWidth: 2
    }
});

var sketchSymbolizersMat = {
    "Point": {
        pointRadius: 4,
        graphicName: "circle",
        fillColor: "#4D4D9A",
        fillOpacity: 1,
        strokeWidth: 1,
        strokeOpacity: 1,
        strokeColor: "#4D4D9A"
    },
    "Line": {
        strokeWidth: 3,
        strokeOpacity: 1,
        strokeColor: "#4D4D9A",
        strokeDashstyle: "dash"
    },
    "Polygon": {
        strokeWidth: 3,
        strokeOpacity: 1,
        strokeColor: "#4D4D9A",
        fillColor: "#dfe6f0",
        fillOpacity: 0.3
    }
};
var styleMat = new OpenLayers.Style();
styleMat.addRules([
    new OpenLayers.Rule({ symbolizer: sketchSymbolizersMat })
]);
var styleMapMat = new OpenLayers.StyleMap({
    "default": styleMat
});

function merrElementinGeoKerkim(gid, emri) {
    map.zoomToExtent(extent);
    $.ajax({
        url: Utils.getServerApiUrl("GIS", "getObjectForQuickSearch"),
        data: JSON.stringify({ gid: gid }),
    }).done(function (result) {
        result = JSON.parse(result);
        DoneMerrElementinGeoKerkim(result, emri)
    });
}

function DoneMerrElementinGeoKerkim(result, emri) {
    var options = {
        response: result,
        emri: emri,
        zoomToFeature: true,
        idDritare: "DritareObjektHarteAtributeId",
        idElementiDritare: "gridAtributeKerkimShpejtId",
        kerkimShpejte: true,
        dritare: {
            height: 90,
            width: 190
        }
    }
    harte.AfishoHarteAtributeObjektGeo(options);
}

function drawAnimatedLineAll(pikaMenuTo, diffGjatesiaVijes) {
    drawAnimatedLine(new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y + diffGjatesiaVijes), new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y), styleGoToXYAnim, 15, 20);
    drawAnimatedLine(new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y - diffGjatesiaVijes), new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y), styleGoToXYAnim, 15, 20);
    drawAnimatedLine(new OpenLayers.Geometry.Point(pikaMenuTo.x + diffGjatesiaVijes, pikaMenuTo.y), new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y), styleGoToXYAnim, 15, 20);
    drawAnimatedLine(new OpenLayers.Geometry.Point(pikaMenuTo.x - diffGjatesiaVijes, pikaMenuTo.y), new OpenLayers.Geometry.Point(pikaMenuTo.x, pikaMenuTo.y), styleGoToXYAnim, 15, 20);
}
function drawAnimatedLine(startPt, endPt, style, steps, time, fn) {
    var directionX = (endPt.x - startPt.x) / steps;
    var directionY = (endPt.y - startPt.y) / steps;
    var i = 0;
    var prevLayer;
    var newStartPt;
    var newEndPt = startPt;
    var ivlDraw = setInterval(function () {
        if (i > steps) {
            clearInterval(ivlDraw);
            if (fn)
                fn();
            setTimeout(function () {
                map.removeLayer(prevLayer);
            }, 700);
            return;
        }
        if (newEndPt) {
            newStartPt = newEndPt;
        }
        newEndPt = new OpenLayers.Geometry.Point(startPt.x + i * directionX, startPt.y + i * directionY);
        var line = new OpenLayers.Geometry.LineString([newStartPt, newEndPt]);
        var fea = new OpenLayers.Feature.Vector(line, {}, style);
        var vec = new OpenLayers.Layer.Vector("", { VektPerkohshemLayer: true });
        vec.addFeatures([fea]);
        map.addLayer(vec);

        if (prevLayer)
            map.removeLayer(prevLayer);
        prevLayer = vec;
        i++;
    }, time / steps);
}

var styleGoToXYAnim = {
    strokeColor: "#0500bd",
    strokeWidth: 5,
    strokeOpacity: 0.5,
    strokeColor: '#0000ff'
};

function shfaqDritareEmail() {

    var storeComboEmailToAdress = new Ext.data.JsonStore({ fields: ["Pershkrim", "Id"], data: varSettings.webConfig.sendEmails });
    var panelEmailToAdressItems = [
        new Ext.form.ComboBox({
            id: "cmbPanelEmailToAdressId", mode: 'local', triggerAction: 'all', width: 295, editable: false, fieldLabel: "To",
            store: storeComboEmailToAdress, valueField: 'Id', displayField: 'Pershkrim', value: 0,
        })
    ];
    gisElements.createGISElement("FormPanel", { id: "PanelEmailToAdressId", height: 50, bodyStyle: 'padding: 10px 0px 0 10px;', items: panelEmailToAdressItems });

    var formPanelDritareEmailItems = [
        { xtype: 'textfield', fieldLabel: perkthe("ADUI_GRUSERS_TIT_Emr"), id: 'txtPanelEmailFromName', value: varSettings.webConfig.userLogin.Pershkrim },
        { xtype: 'textfield', fieldLabel: perkthe("ADUI_GRUSERS_TIT_Email"), id: 'txtPanelEmailFromEmail', value: varSettings.webConfig.userLogin.Email, regex: /^([\w\-\’\-]+)(\.[\w-\’\-]+)*@([\w\-]+\.){1,5}([A-Za-z]){2,4}$/ },
        { xtype: 'textfield', fieldLabel: perkthe("ADUI_GRLAYERS_TIT_LayTit"), id: 'txtPanelEmailSubject' },
        { xtype: 'textarea', fieldLabel: perkthe("ADUI_GRUSERS_TIT_Mesazhi"), id: 'txtPanelEmailBody', height: 170, },
    ];
    gisElements.createGISElement("FormPanel", { id: "formPanelDritareEmailId", height: 280, bodyStyle: 'padding: 10px 10px 0 10px;', defaults: { anchor: '95%', allowBlank: false }, items: formPanelDritareEmailItems });

    var panelEmailDergoItems = [
        new Ext.Button({
            id: 'bntPanelEmailDergo', text: perkthe("ADUI_GRUSERS_TIT_Dergo"), height: 25, width: 50,
            listeners:
            {
                click: function () {
                    if (gisElements.getGISElement("formPanelDritareEmailId").getForm().isValid()) {
                        var email = varSettings.webConfig.ndermarrjeLogin.NdermarrjeEMail;
                        if (gisElements.getGISElement("PanelEmailToAdressId") != 0)
                            email = varSettings.webConfig.sendEmails.filter(function (user) { return user.Id = gisElements.getGISElement("PanelEmailToAdressId") })[0].Email;
                        $.ajax({
                            url: Utils.getServerApiUrl("GIS", "DergoEmailNgaGIS"),
                            data: JSON.stringify({ toEmail: email, fromName: Ext.getCmp("txtPanelEmailFromName").getValue(), fromEmail: Ext.getCmp("txtPanelEmailFromEmail").getValue(), subject: Ext.getCmp("txtPanelEmailSubject").getValue(), bodyMesazh: Ext.getCmp("txtPanelEmailBody").getValue() })
                        }).done(function (result) {                                    
                            noty({ text: result.PershkrimMesazhi, type: "success" }); 
                            gisElements.getGISElement("formPanelDritareEmailId").getForm().reset();
                            Ext.getCmp("FeedbackBtnID").toggle(false);
                            Ext.getCmp("DritareEmailWindowID").hide();
                        }).fail(function (errMsg) {                                    
                            noty({ text: result.PershkrimMesazhi, type: "error" }); 
                        });
                    }
                }
            }
        })
    ];
    gisElements.createGISElement("FormPanel", { id: "PanelEmailDergoId", height: 50, bodyStyle: 'padding: 5px 0px 5px 10px;', defaults: { width: 100 }, items: panelEmailDergoItems });

    var listenersEmailWindow = {
        'close': function (win) {
            Ext.getCmp("FeedbackBtnID").toggle(false);
        },
        'hide': function (win) {
            Ext.getCmp("FeedbackBtnID").toggle(false);
        }
    };
    var tempEmailWindowsItems = [{
        layout: { type: 'vbox', align: 'stretch' },
        items: [gisElements.getGISElement("PanelEmailToAdressId"), gisElements.getGISElement("formPanelDritareEmailId"), gisElements.getGISElement("PanelEmailDergoId")]
    }];

    gisElements.createGISElement("Window", { title: "Kontakt Email", id: "DritareEmailWindowID", layout: 'fit', width: 450, height: 410, items: tempEmailWindowsItems, listeners: listenersEmailWindow });
}

function shfaqDritaregoToXY() {

    var panelKoordinateItemsGoToXYItems = [
        new Ext.form.RadioGroup({
            id: 'PanelKoordinateItemsGoToXYId', xtype: 'radiogroup', columns: 3, frame: true, height: 20, hideLabel: true,
            items: [
                { xtype: 'radio', inputValue: '1', name: "PanelGoToXYCoordinates", boxLabel: "Metrike", disabled: true },
                { xtype: 'radio', inputValue: '2', name: "PanelGoToXYCoordinates", boxLabel: "Decimal", checked: true },
                { xtype: 'radio', inputValue: '3', name: "PanelGoToXYCoordinates", boxLabel: "D\u00B0M'S''" }
            ],
            listeners: {
                change: function (field, newValue, oldValue) {
                    ndryshoSipasTipitGoToXY(newValue['inputValue']);
                }
            }
        })
    ];
    gisElements.createGISElement("FormPanel", { id: "PanelKoordinateTypeGoToXYId",  bodyStyle: 'padding: 0px 10px 0 10px;', items: panelKoordinateItemsGoToXYItems });
    

    var panelProjeksioneGoToXYItems = [
        new Ext.form.ComboBox({
            id: "XYComboProjeksioneID", mode: 'local', triggerAction: 'all', width: 250, hideLabel: true, editable: false,
            store: varSettings.webConfig.projeksioneArrayStore, valueField: 'DisplayCode', displayField: 'Description', value: projektioniKorGeo,
            listeners: {
                select: function (combo, record, index) {
                    ndryshoSipasProjeksionitGoToXY(combo, record, index);
                }
            }
        })
    ];
    gisElements.createGISElement("FormPanel", { title: perkthe("GP_SHKOXYWIN_Tab1Title"), id: "PanelProjeksioneGoToXYId", width: 270, items: panelProjeksioneGoToXYItems });

    var panelgoToXYItems = [];
        panelgoToXYItems.push(krijoExtformNumberFieldMeDecimal('VleraXGoToXY', "VleraXGoToXY", true, perkthe("GP_SHKOXYWIN_GradeE"), "", 147, 7, 0));
        panelgoToXYItems.push(krijoExtformNumberFieldMeDecimal('VleraYGoToXY', "VleraYGoToXY", true, perkthe("GP_SHKOXYWIN_GradeN"), "", 147, 7, 0));
    gisElements.createGISElement("FormPanel", { title: "Decimal Degree", id: "PanelgoToXYId", width: 270, items: panelgoToXYItems });
    
    var panelgoToXYDegreeItems = [];
        panelgoToXYDegreeItems.push({
            layout: 'form', labelWidth: 100,
            items: [
                krijoExtformNumberFieldMeDecimal('VleraXGoToXYDegree1', "VleraXGoToXYDegree1", true, perkthe("GP_SHKOXYWIN_GradeE"), "", 35, 0, 1),
                krijoExtformNumberFieldMeDecimal('VleraYGoToXYDegree1', "VleraYGoToXYDegree1", true, perkthe("GP_SHKOXYWIN_GradeN"), "", 35, 0, 4),
            ]
        });
        panelgoToXYDegreeItems.push({ layout: 'form', labelWidth: 2, items: [{ xtype: 'label', text: "  \u00B0" }] });
        panelgoToXYDegreeItems.push({
            layout: 'form', labelWidth: 2,
            items: [
                krijoExtformNumberFieldMeDecimal('VleraXGoToXYDegree2', "VleraXGoToXYDegree2", true, "", "", 35, 0, 2),
                krijoExtformNumberFieldMeDecimal('VleraYGoToXYDegree2', "VleraYGoToXYDegree2", true, "", "", 35, 0, 5),
            ]
        });
        panelgoToXYDegreeItems.push({ layout: 'form', labelWidth: 2, items: [{ xtype: 'label', text: "  '" }] });
        panelgoToXYDegreeItems.push({
            layout: 'form', labelWidth: 2,
            items: [
                krijoExtformNumberFieldMeDecimal('VleraXGoToXYDegree3', "VleraXGoToXYDegree3", true, "", "", 55, 7, 3),
                krijoExtformNumberFieldMeDecimal('VleraYGoToXYDegree3', "VleraYGoToXYDegree3", true, "", "", 55, 7, 6),
            ]
        });
        panelgoToXYDegreeItems.push({
            layout: 'form', labelWidth: 2, items: [{ xtype: 'label', text: '  "' }]
        });
        panelgoToXYDegreeItems = [{ layout: 'column', items: panelgoToXYDegreeItems }];
    gisElements.createGISElement("FormPanel", { title: "Degrees, Minutes, Seconds", id: "PanelgoToXYIdDegree", width: 270, hidden: true, items: panelgoToXYDegreeItems });


    if (!Ext.getCmp("DritaregoToXYId")) {
        if (!Ext.getCmp("PanToXYBtnId")) {
            var PanToXYBtn = new Ext.Button({
                id: "PanToXYBtnId", title: perkthe("GP_SHKOXYWIN_PANBTN_Title"), tooltipType: "title", tooltip: perkthe("GP_SHKOXYWIN_PANBTN_ToolTip"), icon: varSettings.theme.urlCustom + 'goToXYPan.png',
                handler: function (toggled) {
                    pozicionohuNeHarteHandler(toggled, "PAN", vectorAddPtToXY);
                }
            });
        }
        if (!Ext.getCmp("ZoomToXYBtnId")) {
            var ZoomToXYBtn = new Ext.Button({
                id: "ZoomToXYBtnId", title: perkthe("GP_SHKOXYWIN_ZOOMBTN_Title"), tooltipType: "title", tooltip: perkthe("GP_SHKOXYWIN_ZOOMBTN_ToolTip"), icon: varSettings.theme.urlCustom + 'goToXYZoomIn.png',
                handler: function (toggled) {
                    pozicionohuNeHarteHandler(toggled, "ZOOM", vectorAddPtToXY);
                }
            });
        }
        if (!Ext.getCmp("AddPtToXYBtnId")) {
            var AddPtToXYBtn = new Ext.Button({
                id: "AddPtToXYBtnId", title: perkthe("GP_SHKOXYWIN_SHTOBTN_Title"), tooltipType: "title", tooltip: perkthe("GP_SHKOXYWIN_SHTOBTN_ToolTip"), icon: varSettings.theme.urlCustom + 'goToXYAddFeature.png',
                handler: function (toggled) {
                    if (varSettings.shkoNeXY.ngaEditimi)
                        pozicionohuNeHarteHandler(toggled, "DRAW", vectorAddPtToXY);
                    else
                        pozicionohuNeHarteHandler(toggled, "ADD", vectorAddPtToXY);
                }
            });
        }
        if (!Ext.getCmp("CleanXYBtnId")) {
            var CleanXYBtn = new Ext.Button({
                id: "CleanXYBtnId", title: perkthe("GP_SHPPAN_FORM_BTN_PastroFushat"), tooltipType: "title", tooltip: perkthe("GP_SHPPAN_FORM_BTN_PastroFushat"), icon: varSettings.theme.urlCustom + 'goToXYClean.png',
                handler: function (toggled) {
                    if (toggled) {
                        pastroFushatGoToXY();
                        this.toggle(false);
                    }
                }
            });
        }
        if (!Ext.getCmp("HistorikuPikaveMenuXYId")) {
            var HistorikuPikaveMenu = new Ext.Button({
                id: "HistorikuPikaveMenuXYId", title: perkthe("GP_SHKOXYWIN_HistoriToolTipText"), tooltipType: "title", tooltip: perkthe("GP_SHKOXYWIN_HistoriToolTipText"), icon: varSettings.theme.urlCustom + 'goToXYArchive.png',
                menu: new Ext.menu.Menu({ id: "HistorikuPikaveMenuId", autoDestroy: false, items: [] })
            })
        }

        var vectorAddPtToXY = new OpenLayers.Layer.Vector("", { styleMap: styleMapMat, VektPerkohshemLayer: true });
        map.addLayer(vectorAddPtToXY);

        if (varSettings.shkoNeXY.ngaEditimi)
            var tempTbar = [Ext.getCmp("AddPtToXYBtnId"), Ext.getCmp("CleanXYBtnId")];
        else
            var tempTbar = [Ext.getCmp("PanToXYBtnId"), Ext.getCmp("ZoomToXYBtnId"), Ext.getCmp("AddPtToXYBtnId"), Ext.getCmp("CleanXYBtnId"), Ext.getCmp("HistorikuPikaveMenuXYId")];
    }


    var panelRightGoToXYItems = [
                { xtype: 'label', text: "EPSG:4326", name: 'lblPanelGoToXYProjectionCode', id: 'idPanelGoToXYProjectionCode', style: 'font-weight:bold;font-size:14px;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '', labelSeparator: ' ' },
                { xtype: 'label', text: "Geodetic coordinate system", name: 'lblPanelGoToXYProjectionType', id: 'idPanelGoToXYProjectionType', style: 'color:#474747;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '&nbsp;', labelSeparator: ' ' },
                { xtype: 'label', text: "Unit: ", style: 'font-weight:bold;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'label', text: "degree", name: 'lblPanelGoToXYUnit', id: 'idPanelGoToXYUnit', style: 'color:#474747;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '', labelSeparator: ' ' },
                { xtype: 'label', text: "Geodetic CRS: ", style: 'font-weight:bold;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'label', text: "WGS 84", name: 'lblPanelGoToXYCRS', id: 'idPanelGoToXYCRS', style: 'color:#474747;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '', labelSeparator: ' ' },
                { xtype: 'label', text: "Datum: ", style: 'font-weight:bold;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'label', text: "World Geodetic System 1984", name: 'lblPanelGoToXYDatum', id: 'idPanelGoToXYDatum', style: 'color:#474747;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '', labelSeparator: ' ' },
                { xtype: 'label', text: "Ellipsoid: ", style: 'font-weight:bold;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'label', text: "WGS 84", name: 'lblPanelGoToXYEllipsoid', id: 'idPanelGoToXYEllipsoid', style: 'color:#474747;', anchor: '93%' },
                { xtype: 'component', fieldLabel: '', labelSeparator: ' ' },
                { xtype: 'label', text: "Prime meridian: ", style: 'font-weight:bold;color:#1A1A1A;', anchor: '93%' },
                { xtype: 'label', text: "Greenwich", name: 'lblPanelGoToXYMeridian', id: 'idPanelGoToXYMeridian', style: 'font-family:Arial;color:#474747;', anchor: '93%' }
    ];
    gisElements.createGISElement("FormPanel", { id: "PanelRightGoToXYId", width: 140, height: 185, items: panelRightGoToXYItems });
    
    var listenersGoToXYWindow = {
        'destroy': function (win) {
            if (Ext.getCmp("goToXYBtnId").pressed) {
                Ext.getCmp("goToXYBtnId").toggle();
            }
            vectorAddPtToXY.destroyFeatures();
        }
    };
    var tempGoToXYWindowsItems = [{
        layout: { type: 'hbox', align: 'stretch' },
        items: [
            { type: 'vbox', align: 'stretch', pack: 'start', items: [gisElements.getGISElement("PanelKoordinateTypeGoToXYId"), gisElements.getGISElement("PanelProjeksioneGoToXYId"), gisElements.getGISElement("PanelgoToXYId"), gisElements.getGISElement("PanelgoToXYIdDegree")] },
            gisElements.getGISElement("PanelRightGoToXYId")
        ]
    }];

    gisElements.createGISElement("Window", { title: varSettings.shkoNeXY.title, id: "DritaregoToXYId", layout: 'fit', width: 425, height: 245, tbar: tempTbar, items: tempGoToXYWindowsItems, listeners: listenersGoToXYWindow });
};

function shtoPozicionHarteNeHistorik(vectorAddPtToXY, pikaMenuTo, pikaMenuToFea, textHistorik) {
    var pikaNdodhet = vectorAddPtToXY.getFeatureBy('geometry', pikaMenuToFea.geometry.toString());
    if (!pikaNdodhet) {
        vectorAddPtToXY.addFeatures(pikaMenuToFea);
        Ext.getCmp("HistorikuPikaveMenuId").addItem({
            text: textHistorik,
            id: pikaMenuTo.x.toString() + ", " + pikaMenuTo.y.toString() + "id", //jane koordinata te konvertuara keto
            floating: false,
            menu: new Ext.menu.Menu({
                items: [
                    {
                        xtype: 'menuitem',
                        text: perkthe("GP_SHKOXYWIN_HistoriMenuZmadhoText"),
                        icon: varSettings.theme.urlCustom + 'goToXYArchiveZoomIn.png',
                        ObjPikaAddTo: pikaMenuTo,
                        handler: function () {
                            var zoomLevels = map.getNumZoomLevels() - 1;
                            map.setCenter(new OpenLayers.LonLat(this.ObjPikaAddTo.x, this.ObjPikaAddTo.y), zoomLevels)
                        }
                    },
                    {
                        xtype: 'menuitem',
                        text: perkthe("GP_SHKOXYWIN_HistoriMenuFshijText"),
                        icon: varSettings.theme.urlCustom + 'goToXYArchiveDelete.png',
                        ObjPikaAddTo: pikaMenuTo,
                        ObjFeaPikaAddTo: pikaMenuToFea,
                        handler: function () {
                            vectorAddPtToXY.destroyFeatures(this.ObjFeaPikaAddTo);
                            if (Ext.getCmp("HistorikuPikaveMenuId").items.item(this.ObjPikaAddTo.x.toString() + ", " + this.ObjPikaAddTo.y.toString() + "id")) {
                                Ext.getCmp("HistorikuPikaveMenuId").setVisible(false)
                                Ext.getCmp("HistorikuPikaveMenuId").remove(Ext.getCmp(this.ObjPikaAddTo.x.toString() + ", " + this.ObjPikaAddTo.y.toString() + "id"), true);
                            }
                        }
                    }
                ]
            })
        })
    }
}

function mbyllDritaregoToXY() {
    if (Ext.getCmp("DritaregoToXYId")) {
        Ext.getCmp('DritaregoToXYId').destroy();
    }
};

function shfaqDivMeasurementsSipasRastit(evt, divEl, visible, widthVl, heightVl, rightVl) {
    shfaqDivElementSipasRastit(divEl, visible, widthVl, heightVl, rightVl);
    var textAfisho = handleMeasurements(evt);
    document.getElementById(divEl).innerHTML = textAfisho;
}; 

function handleMeasurements(event) {
    var out = calculateMeasurements(event.geometry, event.units, event.order);
    if (event.order == 1) {
        if (event.units == 'km') {
            out = perkthe("GP_KRYEJMATJE_MSG_Gjat") + " " + out + " km";
        } else {
            out = perkthe("GP_KRYEJMATJE_MSG_Gjat") + " " + out + " m";
        }
    } else {
        if (event.units == 'km') {
            out = perkthe("GP_KRYEJMATJE_MSG_Sip") + " " + out + " km<sup>2</sup>";
        } else {
            out = perkthe("GP_KRYEJMATJE_MSG_Sip") + " " + out + " m<sup>2</sup>";
        }
    }
    return out;
};

function calculateMeasurements(geometry, units, order) {
    var out = 0;
    var geometry = geometry.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
    if (order == 1) {
        if (units == 'km') {
            out = (geometry.getLength() / 1000).toFixed(3);
        } else {
            out = geometry.getLength().toFixed(3);
        }
    } else {
        if (units == 'km') {
            out = (geometry.getArea() / 1000000).toFixed(3);
        } else {
            out = geometry.getArea().toFixed(3);
        }
    }
    return out;
};

function callbackAddLabelsToLineVertexs(control, point, line, mapTempLayerForDistanceMeasure) {
    var len = line.geometry.components.length;
    var from = line.geometry.components[len - 2];
    var to = line.geometry.components[len - 1];
    var ls = new OpenLayers.Geometry.LineString([from, to]);
    var dist = control.getBestLength(ls);
    if (!dist[0]) {
        return;
    }
    var total = control.getBestLength(line.geometry);
    var label = dist[0].toFixed(3) + " " + dist[1];
    var textNode = control.textNodes[len - 2] || null;
    if (textNode && !textNode.layer) {
        control.textNodes.pop();
        textNode = null;
    }
    if (!textNode) {
        var c = ls.getCentroid();
        textNode = new OpenLayers.Feature.Vector(
        new OpenLayers.Geometry.Point(c.x, c.y), {}, {
            label: '',
            fontColor: "Black",
            fontSize: "10px",
            fontOpacity: 1,
            fontFamily: "Tahoma",
            fontWeight: "bold",
            labelOutlineColor: "white",
            labelOutlineWidth: 2,
            labelAlign: "cm",
            labelXOffset: 10,
            labelYOffset: 10,
        });
        control.textNodes.push(textNode);
        mapTempLayerForDistanceMeasure.addFeatures([textNode]);
    }
    textNode.geometry.x = (from.x + to.x) / 2;
    textNode.geometry.y = (from.y + to.y) / 2;
    textNode.style.label = label;
    textNode.layer.drawFeature(textNode);
};

//Shpjegim: funksioni qe krijon te gjithe butonat e menuse Lart
function KrijoMenuLart() {
    //1: Ikona Kryesore qe te ridrejton ne faqen kryesore
    toolbarItems.push("<div class='headerdivTitlePanel'><div class='headerImgPanel'><a  target='_blank' href='" + homePageUrl + "'><img src='" + varSettings.theme.urlDefault + "menuHome.png' /></a></div></div>");
    toolbarItems.push("->");

    //2: Butoni Email
    var EmailBtn = new Ext.Button({
        id: "FeedbackBtnID",
        scale: "medium",
        enableToggle: true,
        tooltipType: "title",
        icon: varSettings.theme.urlDefault + 'menuEmail.png',
        tooltip: "Kontakto stafin",
        handler: function (toggled) {
            if (toggled) {
                shfaqDritareEmail();
            }
        }
    });
    toolbarItems.push(EmailBtn);
    toolbarItems.push("-");

    //3: Lokalizo Vendndodhjen
    //var geolocationBtn = new Ext.Button({
    //    enableToggle: true,
    //    scale: "medium",
    //    icon: varSettings.theme.urlDefault + 'menuGeoLocation.png',
    //    tooltipType: "title",
    //    tooltip: perkthe("GP_WIN_TOOLTIP_geolocation"),
    //    handler: function (toggled) {
    //        if (navigator.geolocation) {
    //            var options = {
    //                idVectorPerkohGeolocation: "idVectorPerkohGeoloc",
    //                idWatchGeolocation: null
    //            };
    //            if (this.pressed) {
    //                harte.goToUserLocation(options);
    //            } else {
    //                harte.fshijNgaHartaGeolocation();
    //            }
    //        } else {
    //            Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_NoGeolocation"));
    //        }
    //    }
    //});
    //toolbarItems.push(geolocationBtn);
    //toolbarItems.push("-");

    //4: Shiko te gjithe Harten
    var fullExtent = new Ext.Button({
        title: '',
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_FullExtent"),
        icon: varSettings.theme.urlDefault + 'menuHomeExtend.png',
        scale: "medium",
        handler: function (toggled) {
            if (toggled) {
                map.zoomToExtent(extent);
            } else { }
        }
    });
    toolbarItems.push(fullExtent);
    toolbarItems.push("-");

    //5: Rrjeti Koordinativ
    var GraduleBnt = new Ext.Button({
        enableToggle: true,
        title: '',
        tooltipType: "title",
        tooltip: "Rrjeti Koordinativ",
        icon: varSettings.theme.urlDefault + 'menuGradule.png',
        scale: "medium",
        handler: function (toggled) {
            if (this.pressed) {
                map.getControlsBy("id", "MAPControlGraticuleXYT")[0].activate();
            } else {
                map.getControlsBy("id", "MAPControlGraticuleXYT")[0].deactivate();
            }
        }
    });
    toolbarItems.push(GraduleBnt);
    toolbarItems.push("-");

    //6: Pan
    dragPanControl = new OpenLayers.Control.DragPan({
        isDefault: false
    })

    dragPan = new GeoExt.Action({
        control: dragPanControl,
        tooltipType: "title",
        enableToggle: true,
        tooltip: perkthe("GP_BTN_TOOLTIP_Pan"),
        map: map,
        scale: "medium",
        toggleGroup: "panButton",
        icon: varSettings.theme.urlDefault + 'menuPan.png',
        handler: function () {
            caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
            caktivizoButonatAktivizoButonin(dragPanControl, actions);
        }
    });
    toolbarItems.push(dragPan);
    toolbarItems.push("-");

    //7: Zvogelo
    var zoomOutControl = new OpenLayers.Control.ZoomBox({
        id: "MAPControlZoomOutBox",
        out: true
    });
    actions.zoomOut = new GeoExt.Action({
        control: zoomOutControl,
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_ZoomOut"),
        map: map,
        scale: "medium",
        icon: varSettings.theme.urlDefault + 'menuZoomOut.png',
        toggleGroup: "panButton",
        handler: function () {
            caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
            caktivizoButonatAktivizoButonin(zoomOutControl, actions);
        }
    });
    toolbarItems.push(actions.zoomOut);
    toolbarItems.push("-");

    //8: Zmadho
    var zoomInControl = new OpenLayers.Control.ZoomBox({
        id: "MAPControlZoomInBox",
    });
    actions.zoomIn = new GeoExt.Action({
        control: zoomInControl,
        tooltipType: "title",
        scale: "medium",
        tooltip: perkthe("GP_BTN_TOOLTIP_ZoomIn"),
        map: map,
        icon: varSettings.theme.urlDefault + 'menuZoomIn.png',
        toggleGroup: "panButton",
        handler: function () {
            caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
            caktivizoButonatAktivizoButonin(zoomInControl, actions);
        }
    });
    toolbarItems.push(actions.zoomIn);
    toolbarItems.push("-");

    //9: Shko Para
    var nav = map.getControlsBy("id", "MAPControlNavigationHistory")[0];
    var buttonPrevious = new Ext.Toolbar.Button({
        id: "MAPButtonNavigationHistoryPrevious",
        iconCls: 'prevoff',
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_Previous"),
        disabled: true,
        scale: "medium",
        handler: nav.previous.trigger
    });
    toolbarItems.push(buttonPrevious);
    toolbarItems.push("-");
    buttonNextPreviousHandlerEventRegister(nav.previous, buttonPrevious, 'prevon', 'prevoff');

    //10: Kthehu pas
    var buttonNext = new Ext.Toolbar.Button({
        id: "MAPButtonNavigationHistoryNext",
        iconCls: 'nextoff',
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_Next"),
        disabled: true,
        scale: "medium",
        handler: nav.next.trigger
    });
    toolbarItems.push(buttonNext);
    toolbarItems.push("-");
    buttonNextPreviousHandlerEventRegister(nav.next, buttonNext, 'nexton', 'nextoff');


    //11: Shko ne pozicionin XY
    var goToXYBtn = new Ext.Button({
        id: "goToXYBtnId",
        enableToggle: true,
        icon: varSettings.theme.urlDefault + 'menuGoToXY.png',
        tooltipType: "title",
        scale: "medium",
        tooltip: perkthe("GP_BTN_TOOLTIP_ShkoNeXY"),
        handler: function (toggled) {
            if (toggled) {
                if (this.pressed) {
                    varSettings.shkoNeXY.ngaEditimi = false;
                    varSettings.shkoNeXY.title = perkthe("GP_SHKOXYWIN_Title"),
                    shfaqDritaregoToXY();
                } else {
                    mbyllDritaregoToXY();
                }
            }
        }
    });
    toolbarItems.push(goToXYBtn);
    toolbarItems.push("-");

    //12:   Vizore
    actions.matDistance = new GeoExt.Action({
        map: map,
        control: new OpenLayers.Control.Measure(OpenLayers.Handler.Path, {
            id: "MAPControlMeasureDistance",
            persist: true,
            geodesic: true,
            handlerOptions: {
                layerOptions: {
                    styleMap: styleMapMat
                }
            },
            eventListeners: {
                'activate': function () {
                    if (map.getLayersByName("MAPTempLayerForDistanceMeasure")[0] == undefined) {
                        var mapTempLayerForDistanceMeasure = new OpenLayers.Layer.Vector("MAPTempLayerForDistanceMeasure", { styleMap: measureStyleMap });
                        map.addLayer(mapTempLayerForDistanceMeasure);
                    }
                },
                'deactivate': function () {
                    map.removeLayer(map.getLayersByName("MAPTempLayerForDistanceMeasure")[0]);
                    shfaqDivElementSipasRastit("njoftimeRezultate", false, "", "", "");
                },
                measure: function (geometry, eventType) {
                    shfaqDivMeasurementsSipasRastit(geometry, "njoftimeRezultate", true, "200px", "auto", "");
                    this.textNodes = [];
                },
                measurepartial: function (evt) {
                    shfaqDivMeasurementsSipasRastit(evt, "njoftimeRezultate", true, "200px", "auto", "");
                },
            },            
            callbacks: {
                create: function () {
                    this.removeAllFeaturesBeforeFirstPoint = true;
                    this.textNodes = [];
                },
                modify: function (point, line) {
                    callbackAddLabelsToLineVertexs(this, point, line, map.getLayersByName("MAPTempLayerForDistanceMeasure")[0]);
                },
                point: function (point) {
                    if (this.removeAllFeaturesBeforeFirstPoint) {
                        var mapTempLayerForDistanceMeasure = map.getLayersByName("MAPTempLayerForDistanceMeasure")[0];
                        mapTempLayerForDistanceMeasure.removeFeatures(mapTempLayerForDistanceMeasure.features);
                        this.removeAllFeaturesBeforeFirstPoint = false;
                    }
                },
            }
        }),
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_MENU_TOOLTIP_MatDist"),
        toggleGroup: "panButton",
        icon: varSettings.theme.urlCustom + 'measureLength.png',
        text: perkthe("GP_BTN_MENU_TEXT_MatDist"),
        group: "measure"
    });

    actions.matSiperfaqje = new GeoExt.Action({
        map: map,
        control: new OpenLayers.Control.Measure(OpenLayers.Handler.Polygon, {
            id: "MAPControlMeasureArea",
            persist: true,
            handlerOptions: {
                layerOptions: {
                    styleMap: styleMapMat
                }
            },
            eventListeners: {
                'deactivate': function () {
                    shfaqDivElementSipasRastit("njoftimeRezultate", false, "", "", "");
                },
                measure: function (evt) {
                    shfaqDivMeasurementsSipasRastit(evt, "njoftimeRezultate", true, "200px", "auto", "");
                },
                measurepartial: function (evt) {
                    shfaqDivMeasurementsSipasRastit(evt, "njoftimeRezultate", true, "200px", "auto", "");
                },
            }
        }),
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_MENU_TOOLTIP_MatSip"),
        toggleGroup: "panButton",
        icon: varSettings.theme.urlCustom + 'measureArea.png',
        text: perkthe("GP_BTN_MENU_TEXT_MatSip"),
        group: "measure"
    });

    actions.matKordinate = new GeoExt.Action({
        map: map,
        control: new OpenLayers.Control.DrawFeature(varSettings.measure.merrKoordinata, OpenLayers.Handler.Point, {
                id: "MAPControlMeasureCoordinate",
                title: perkthe("GP_BTN_MENU_TEXT_MatKoord"),
                multi: false,
                map: map,
                handlerOptions: {
                    'style': sketchSymbolizersMat["Point"]
                },
                eventListeners: {
                    'activate': function () {
                        varSettings.measure.merrKoordinata.events.register('sketchcomplete', varSettings.measure.merrKoordinata, function (event) {
                            var proceed = true;
                            varSettings.measure.merrKoordinata.destroyFeatures();

                            var geoTrans = event.feature.geometry.clone();
                            var pikaTrans = geoTrans.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniDisplay));

                            var geo = event.feature.geometry.clone();
                            var koord = geo.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projektioniKorGeo))
                            var ns = formatoKoordinatat(koord.y);
                            var ew = formatoKoordinatat(koord.x, 'lon');

                            var geoAlb86 = event.feature.geometry.clone();
                            var pikaAlb86 = geoAlb86.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projektioniAlb86));

                            shfaqDivElementSipasRastit("njoftimeRezultate", true, "250px", "auto", "40%");
                            document.getElementById("njoftimeRezultate").innerHTML = '<table><tr><th class="elem1">' + perkthe('GP_KRYEJMATJE_MSG_koord_headSistGeo') + '</th ><th>' + perkthe('GP_KRYEJMATJE_MSG_koord_headLind') + '</th><th>' + perkthe('GP_KRYEJMATJE_MSG_koord_headV') + '</th></tr><tr><td class="elem1">' + perkthe('GP_KRYEJMATJE_MSG_koord_UTM_em') + '</td><td>' + pikaTrans.x.toFixed(3) + '</td><td>' + pikaTrans.y.toFixed(3) + ' </td></tr><tr><td class="elem1">' + perkthe('GP_KRYEJMATJE_MSG_koord_Alb86_em') + '</td><td>' + pikaAlb86.x.toFixed(3) + '</td><td>' + pikaAlb86.y.toFixed(3) + ' </td></tr><tr><td class="elem1">' + perkthe('GP_KRYEJMATJE_MSG_koord_WGS84_em') + '</td><td>' + ew.str + ew.koordinate + '</td><td>' + ns.str + ns.koordinate + ' </td> </tr> </table>';
                           
                            event.feature.attributes = {
                                labelName: 'X:' + pikaTrans.x.toFixed(3) + '  Y:' + pikaTrans.y.toFixed(3),
                                favColor: 'yellow',
                                align: "cm",
                                xOffset: 10,
                                yOffset: 10
                            };

                            return true;
                        });
                    },
                    'deactivate': function () {
                        varSettings.measure.merrKoordinata.destroyFeatures();
                        shfaqDivElementSipasRastit("njoftimeRezultate", false, "", "", "");
                    }
                }
            }),
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_MENU_TOOLTIP_MatKoord"),
        toggleGroup: "panButton",
        icon: varSettings.theme.urlCustom + 'measureCoordinate.png',
        text: perkthe("GP_BTN_MENU_TEXT_MatKoord"),
        group: "measure"
    });

    matButton = new Ext.Button({
        split: false,
        scale: "medium",
        enableToggle: true,
        icon: varSettings.theme.urlDefault + 'menuMeasure.png',
        allowDepress: false,
        toggleGroup: "panButton",
        menu: {
            items: [
                new Ext.menu.CheckItem(actions.matKordinate),
                new Ext.menu.CheckItem(actions.matDistance),
                new Ext.menu.CheckItem(actions.matSiperfaqje)
            ]
        },
        handler: function (toggled) {
            if (toggled) {
                if (Ext.getCmp("ButoniEditimTb").pressed) {
                    Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_KryejMatjePanEditHap"), function () {
                        caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
                        caktivizoButonatAktivizoButonin('', actions);
                    });
                    if (matButton.pressed) {
                        matButton.toggle();
                    }
                } else {
                    caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
                    caktivizoButonatAktivizoButonin('', actions);
                }
            }
        }
    })
    toolbarItems.push(matButton);

    //13: Printimi
    var hiddenPrinto = (varSettings.webConfig.layers.filter(function (layer) { return (layer.IDGRSTRUCTURE == 2 && layer.D_PRINTO) }).length > 0 ? false : true);
    var printimButton = new Ext.Button({
        scale: "medium",
        icon: varSettings.theme.urlDefault + 'menuPrint.png',
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_MENU_PrintoHartenMF"),
        hidden: hiddenPrinto,
        handler: function () {
            if (editWindow != null && editWindow != "undefined" && editWindow.isVisible()) {
                KrijoWhereQuerySipasFunksioneveTeEditimit();
                varSettings.printimiPdf.ngaEditimi = true;

                Ext.getCmp('EditButtonId').toggle(false);
                eventInserUpdateAtribute = null;
                thirrSave = false;
                editWindow.hide();
            }
            else
                varSettings.printimiPdf.ngaEditimi = false;

            if (!Ext.getCmp("printimDritare1")) {
                afishoDritarenPrintim();
            }
        }
    });
    toolbarItems.push(printimButton);
    if (!hiddenPrinto)
        toolbarItems.push("-");

    /*Komentuar shihe me vone kuptimin
    //13: Buffer
    if (arrayButonat.indexOf(3) > -1) {
        var BufferBtn = new Ext.Button({
            enableToggle: true,
            scale: "medium",
            icon: varSettings.theme.urlDefault + 'menuBuffer.png',
            tooltipType: "title",
            tooltip: perkthe("BTN_TOOLTIP_BufferGeo"),
            id: "bufferBtnId",
            handler: function (toggled) {
                if (toggled) {
                    var options = {
                        btnBuffer: this,
                        idDritareBuffer: "DritareBufferId"
                    }
                    if (this.pressed) {
                        harte.hapDritareBuffer(options);
                    } else {
                        harte.mbyllDritareBuffer(options);
                    }
                }
            }
        });
        toolbarItems.push(BufferBtn);
        toolbarItems.push("-");
    } */

    //14: Informacioni
    MerrAtribute();
    InfoBtn = new Ext.Button({
        toggleGroup: "panButton",
        enableToggle: true,
        scale: "medium",
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_MerrInfo"),
        title: perkthe("GP_BTN_TEXT_MerrInfo"),
        icon: varSettings.theme.urlDefault + 'menuInformation.png',
        handler: function (toggled) {
            if (toggled) {
                caktivizoButonatAktivizoButonin('', actions);
                caktivizoKontrolletAktivizoKontrollin(featureInfo, vektoriKontrollet);
                
                featureInfo.activate();
            }
        }
    });
    vektoriKontrollet.push(featureInfo);

    //14: Kerkimi
    var KerkoButonTabelar = new Ext.Button({
        id: "KerkoButonTabelarID",
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_Kerkim"),
        icon: varSettings.theme.urlCustom + "searchTabular.png",
        scale: "medium",
        enableToggle: true,
        toggleGroup: "panButton",
        text: perkthe("GP_KERKIMTABPAN_TIT"),
        handler: function (toggled) {
            if (toggled) {
                popupUniversal2.SetHeaderText(perkthe("GP_KERKIMTABPAN_TIT"));
                popupUniversal2.SetContentUrl("GISLupaKerko.aspx");
                popupUniversal2.SetSize(900, 600);
                popupUniversal2.Show();
                popupUniversal2.SetCollapsed(false);
            }
        }
    });

    var KerkoButonHapsinor = new Ext.Button({
        id: "KerkoButonHapsinorID",
        tooltipType: "title",
        tooltip: perkthe("GP_BTN_TOOLTIP_Kerkim"),
        icon: varSettings.theme.urlCustom + "searchSpatial.png",
        scale: "medium",
        enableToggle: true,
        text: perkthe("GP_KERKIMHAPTABPAN_TIT"),
        toggleGroup: "panButton",
        handler: function (toggled) {
            KerkoButtonHapsinorHandler(toggled, this);
        }
    });


    var hiddenKerko = (varSettings.webConfig.layers.filter(function (layer) { return (layer.IDGRSTRUCTURE == 2 && layer.D_KERKO) }).length > 0 ? false : true);
    KerkoBtn = new Ext.Button({
        id: "KerkoBtnAll",
        split: false,
        scale: "medium",
        enableToggle: false,
        icon: varSettings.theme.urlDefault + 'menuSearch.png',
        toggleGroup: "panButton",
        hidden: hiddenKerko,
        menu: {
            items: [
                new Ext.menu.Item(KerkoButonTabelar),
                new Ext.menu.Item(KerkoButonHapsinor)
            ],
        },
        handler: function (toggled) {
            if (toggled) {
                caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
                caktivizoButonatAktivizoButonin('', actions);
                Ext.getCmp("KerkoBtnAll").toggle(false);
            }
        }
    });

    toolbarItems.push(matButton);
    toolbarItems.push("-");

    //15: Editimi
    var hiddenEdito = (varSettings.webConfig.layers.filter(function (layer) { return (layer.IDGRSTRUCTURE == 2 && (layer.D_SHTIM || layer.D_MOD || layer.D_FSH)) }).length > 0 ? false : true);
    var EditimBtn = new Ext.Button({
        id: "ButoniEditimTb", icon: varSettings.theme.urlDefault + "menuEdit.png", scale: "medium", enableToggle: true, tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_Editim"),
        hidden: hiddenEdito,
        handler: function (toggled) {
            if (toggled) {
                caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet);
                caktivizoButonatAktivizoButonin('', actions);
                if (matButton.pressed)
                    matButton.toggle();
                if (this.pressed)
                    shfaqPanelEditim();
                else
                    zhdukPanelEditim();
            }
        }
    });

    for (var i = 0; i < arrayButonat.length; i++) {
        if (arrayButonat[i] == 2) {
            toolbarItems.push(InfoBtn);
            toolbarItems.push("-");
        }
        if (arrayButonat[i] == 3) {
            toolbarItems.push(KerkoBtn);
            if (!hiddenKerko)
                toolbarItems.push("-");
        }
        if (arrayButonat[i] == 4) {
            toolbarItems.push(EditimBtn);
            if (!hiddenEdito)
                toolbarItems.push("-");
        }
    }

    //18: Shkalla
    var scaleStore = new GeoExt.data.ScaleStore({
        map: map
    });
    var zoomSelector = new Ext.form.ComboBox({
        store: scaleStore,
        emptyText: "Zoom Level",
        width: 100,
        tpl: '<tpl for="."><div class="x-combo-list-item" style="width: 60px;" >1 : {[parseInt(values.scale)]}</div></tpl>',
        editable: true,
        typeAhead: true,
        selectOnFocus: true,
        forceSelection: false,
        triggerAction: 'all', // needed so that the combo box doesn't filter by its current content
        mode: 'local', // keep the combo box from forcing a lot of unneeded data refreshes
        listeners: {
            onclick: function (combo, record, index) {
                map.zoomTo(record.data.level);
            },
            select:function (combo, record, index) {
                map.zoomTo(record.data.level);
            }
        }
    });

    map.events.register('zoomend', this, function () {
        var scale = scaleStore.queryBy(function (record) {
            return this.map.getZoom() == record.data.level;
        });

        if (scale.length > 0) {
            scale = scale.items[0];
            zoomSelector.setValue("1 : " + parseInt(scale.data.scale));
        } else {
            if (!zoomSelector.rendered)
                return;
            zoomSelector.clearValue();
        }
    });
    toolbarItems.push(zoomSelector);
    toolbarItems.push("-");

    //19: Kerkimi i menjehershem
    var KerkimIShpejteBtn = new Ext.form.ComboBox({
        id: "ComboKerkimShpejteId",
        store: LayerStoreKerkimShpejte,
        displayField: emriKolKerkimShpejteComboAfish,
        valueField: emriKolKerkimShpejteComboValue,
        tooltipType: "title",
        tooltip: perkthe("GP_COMBO_TOOLTIP_KerkimShpejte"),
        emptyText: perkthe("GP_COMBO_EmtyText_KerkimShpejte"),
        mode: 'local',
        forceSelection: true,
        typeAhead: true,
        triggerAction: 'all',
        selectOnFocus: true,
        hideTrigger: true,
        hidden: false,
        width: 150,
        enableKeyEvents: true,
        tpl: '<tpl for="."><div class="x-combo-list-item"><span class="combBlackText">{' + emriKolKerkimShpejteComboAfish + '}</span> - <i><span class="cmbKerkimShpejt' + varSettings.theme.color + '">{' + emriKolKerkimShpejteComboAfish2 + '}</span></i></div></tpl>',
        listeners: {
            select: function (combo, record, index) {
                merrElementinGeoKerkim(combo.getValue(), combo.getRawValue());
            }
        }
    })
    toolbarItems.push(KerkimIShpejteBtn);
    toolbarItems.push("-");
    
    //20: Perdoruesi
    var userPersonalizoBtn = new Ext.Button({
        split: false, scale: "medium", enableToggle: false, text: varSettings.webConfig.userLogin.Emer, icon: varSettings.theme.urlCustom + 'userAnonymous.png', toggleGroup: "userButton",
        handler: function (b, e) {
            Ext.getCmp("idUserLoginBtn").toggle(false);
            myButtonClickLupa.LupaUniversal_Click("Perdorues", "LupaPersonalizoPerdorues.aspx", 870, 550);
        }
    });   

    var textName = "";
    if (varSettings.webConfig.userLogin.Emer != varSettings.webConfig.userAutoLogin)
        textName = perkthe("GP_BTN_MENU_TEXT_UserGjuha");

    if (varSettings.webConfig.idGjuha == 0) {
        var icoMenuLanguage = "menuLangEN.png";
        var changeLanguageHref = Paths.defaultLoginPath + '?arsye=ndryshoGjuhe&gjuha=EN&ktheNe=GISDefault.aspx';
    }
    else {
        var icoMenuLanguage = "menuLangAL.png";
        var changeLanguageHref = Paths.defaultLoginPath + '?arsye=ndryshoGjuhe&gjuha=AL&ktheNe=GISDefault.aspx';
    }

    var userAlphaWEBBtn = new Ext.Button({
        split: false, scale: "medium", enableToggle: false, text: perkthe("GP_BTN_MENU_TEXT_AlphaWEB"), icon: varSettings.theme.urlDefault + 'menuWEB.png', toggleGroup: "userButton",
        handler: function (b, e) {
            window.location.href = 'FaqeKryesore.aspx?vjenNga=GIS';
        }
    });
    
    var userChangeLanguageBtn = new Ext.Button({
        split: false, scale: "medium", enableToggle: false, text: textName, icon: varSettings.theme.urlDefault + icoMenuLanguage, toggleGroup: "userButton", toggleGroup: "userButton",
        handler: function (b, e) {
            window.location.href = changeLanguageHref;
        }
    });

    var userLogoutBtn = new Ext.Button({
        split: false, scale: "medium", enableToggle: false, text: perkthe("GP_BTN_MENU_TEXT_LogOut"), icon: varSettings.theme.urlCustom + 'userLogout.png', toggleGroup: "userButton",
        handler: function (b, e) {
            window.location.href = Paths.defaultLoginPath + '?arsye=logout';
        }
    });

    var userLoginBtn = new Ext.Button({
        id: "idUserLoginBtn", split: false, enableToggle: false, icon: varSettings.webConfig.urlImazhPerdoruesi, iconCls: 'bntUserMenu', toggleGroup: "userButton",
        menu: { items: [new Ext.menu.Item(userPersonalizoBtn), new Ext.menu.Item(userAlphaWEBBtn), new Ext.menu.Item(userChangeLanguageBtn), new Ext.menu.Item(userLogoutBtn)] },
    });

    if (varSettings.webConfig.userLogin.Emer == varSettings.webConfig.userAutoLogin)
        toolbarItems.push(userChangeLanguageBtn);
    else
        toolbarItems.push(userLoginBtn);
    toolbarItems.push("-");
};

function merrImazhPerdoruesi() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Celje", "ktheImazhPerdoruesi"),
        data: JSON.stringify({ idPerdoruesi: varSettings.webConfig.idPerdoruesi })
    }).done(vendosImazhPerdoruesi);
}

function vendosImazhPerdoruesi(result) {
    if (result) {
        Ext.getCmp("idUserLoginBtn").setIcon(result);
    }
}


//Funksioni i meposhtem ben shfaqjen e div sipas rastit te kerkimit.
function shfaqDivElementSipasRastit(divEl, visible, widthVl, heightVl, rightVl) {
    if (visible) {
        document.getElementById(divEl).style.width = widthVl;
        document.getElementById(divEl).style.height = heightVl;
        if (rightVl != "") {
            document.getElementById(divEl).style.right = rightVl;
        }
        document.getElementById(divEl).style.visibility = "visible";
    }
    else {
        document.getElementById(divEl).style.visibility = "hidden";
    }
}

function populloPanelinDokumentUpload(vleraUnikeIdDytesore) {
    Ext.Ajax.request({
        url: Utils.getServerApiUrl("GIS", "merrTeGjitheSkedaretUploadPerOBjektGeo"),
        params: { vleraUnikeIdDytesore: vleraUnikeIdDytesore },
        success: function (result, request) {
            varSettings.uploadConfig.skedaretUploadArray = JSON.parse(JSON.parse(result.responseText).d);
            for (var index = 0; index < varSettings.uploadConfig.skedaretUploadArray.length; index++) {
                var deleteElementetUpload = {
                    id: varSettings.uploadConfig.skedaretUploadArray[index]["FILENAME"] + 'delete',
                    style: "padding-left:10px",
                    xtype: 'box',
                    elementArray: varSettings.uploadConfig.skedaretUploadArray[index],
                    autoEl: {
                        tag: 'a',
                        href: '#',
                        html: '<img src="' + varSettings.theme.urlCustom + 'fileDelete.png">'
                    },
                    listeners: {
                        render: function (c) {
                            c.getEl().on('click', function () {
                                var elementArrayObj = this.elementArray;
                                Ext.MessageBox.show({
                                    title: perkthe("GP_MSG_TIT_kujdes"),
                                    msg: perkthe("GP_EDITIMPAN_MSG_FshijDok"),
                                    buttons: Ext.MessageBox.YESNO,
                                    fn: function (btn) {
                                        //FshijDokumentinUploadFolderDb(btn, elementArrayObj, vleraUnikeIdDytesore)
                                    },
                                    icon: Ext.MessageBox.QUESTION
                                });
                            }, c, { stopEvent: true });
                        }
                    }
                }

                var elementetUpload = {
                    id: varSettings.uploadConfig.skedaretUploadArray[index]["FILENAME"],
                    style: "padding-left:1px;",
                    xtype: 'box',
                    emerRuajtur: varSettings.uploadConfig.skedaretUploadArray[index]["FILENAME"],
                    autoEl: {
                        tag: 'a',
                        href: '#',
                        html: varSettings.uploadConfig.skedaretUploadArray[index]["SHENIME"]
                    },
                    listeners: {
                        render: function (c) {
                            c.getEl().on('click', function () {
                                hapDritareDokumentim(urlDokumentimiUpload + this.emerRuajtur + "")
                            }, c, { stopEvent: true });
                        }
                    }
                }
                Ext.getCmp('dokumentatUploadEditimi').add(deleteElementetUpload)
                Ext.getCmp('dokumentatUploadEditimi').add(elementetUpload)
                Ext.getCmp('dokumentatUploadEditimi').doLayout();
            }
        }
    })
}

var editWindow = null;
var tbar;
var PanelEditim = null;
var vleraEComboParaMbylljes = "bosh";
var comboLayer;
function shfaqPanelEditim() {
    gisElements.createGISElement("FormPanel", { id: "formEditimi", hidden: true });
    varSettings.editim.objPanel.frmeditim = gisElements.getGISElement("formEditimi");
    varSettings.editim.objPanel.frmeditim.hide();
    varSettings.editim.objPanel.frmeditim.doLayout();

    varSettings.editim.objPanel.bbarItems = gisElements.createGISElement("Panel", { id: "bbarPanelEditimiId", width: 362 });
    PanelEditim = gisElements.createGISElement("Panel", { id: "PanelEditimi", items: varSettings.editim.objPanel.frmeditim });

    comboLayer = new Ext.form.ComboBox({
        store: varSettings.editim.cmbLayesStore,
        displayField: 'DESCRIPTION',
        valueField: 'IDENTIFICATION',
        IdLayer: 'IDLAYER',
        mode: 'local',
        forceSelection: true,
        triggerAction: 'all',
        emptyText: perkthe("GP_EDITIMPAN_COMBO_emptyText"),
        selectOnFocus: true,
        listeners: {
            select: function (combo, record, index) {
                varSettings.editim.objOpenLayer.layerSelId = record.get('IDLAYER');
                varSettings.editim.objOpenLayer.layerSelNrStatus = record.json['NRSTATUSI'];
                Editim(combo.getValue(), combo.getRawValue(), '', record.get('IDLAYER'), record.json['IDLAYERSTYPE'], record.json['NRSTATUSI']);
            }
        }
    });
    
    varSettings.editim.objPanel.tbar = panelItems(PanelEditim.getTopToolbar(), true, true, comboLayer, false, true);
    varSettings.editim.objPanel.bbarItems1 = panelItems(Ext.getCmp("bbarPanelEditimiId").getTopToolbar(), true, false, undefined, true, true);
    varSettings.editim.objPanel.bbarItems2 = panelItems(Ext.getCmp("bbarPanelEditimiId").getBottomToolbar(), true, false, undefined, true, true);
    varSettings.editim.objPanel.bbar = panelItems(PanelEditim.getBottomToolbar(), true, true, varSettings.editim.objPanel.bbarItems, true, true);
    
    var listenersEditimWindow = {
        'hide': function (win) {
            zhdukPanelEditim();
            if (Ext.getCmp("ButoniEditimTb").pressed) {
                Ext.getCmp("ButoniEditimTb").toggle();
            }
        }
    };
    gisElements.createGISElement("Window", { title: perkthe("GP_EDITIM_WIN_TIT"), id: "WindowEditimi", width: 380, closeAction: 'hide', items: [PanelEditim], listeners: listenersEditimWindow });
    editWindow = gisElements.getGISElement("WindowEditimi");
    editWindow.alignTo(mapPanel.body, 'tr-tr', [-10, 10]);
}


function panelItems(panelItem, removeAll, addElement, element, hide, doLayout) {
    if (removeAll) panelItem.removeAll();
    if (addElement) panelItem.add(element);
    if (hide)
        panelItem.hide();
    else
        panelItem.show();
    if (doLayout) panelItem.doLayout();

    return panelItem;
}

function zhdukPanelEditim() {
    if (eventInserUpdateAtribute != null || thirrSave) {
        Ext.MessageBox.show({
            title: perkthe("GP_EDITIM_MSG_TIT_RUAJ"),
            msg: perkthe("GP_EDITIM_MSG_MSG_RUAJ"),
            buttons: Ext.MessageBox.YESNOCANCEL,
            fn: YESNOCANCELResult,
            icon: Ext.MessageBox.QUESTION
        });
    }
    else
        shkaterroDritareEditimi();
}

function YESNOCANCELResult(btn) {
    if (btn == "yes") {
        RuajButton.handler.call(RuajButton.scope || RuajButton, RuajButton, "mbyllEditim");
    }

    if (btn == "no") {
        thirrSave = false;
        shkaterroDritareEditimi();
    }
};

function shkaterroDritareEditimi() {
    pastroObjektLidhjeWeb(varSettings.editim.objLidhjeGisWeb.IDLAYER, varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, varSettings.editim.objLidhjeGisWeb.NRSTATUSI);

    if (!varSettings.printimiPdf.ngaEditimi)
        pastroLayersTemp();

    if (layerPerEditim && (!layerEditimiEshteHapur))
        layerPerEditim.setVisibility(false);

    if (mapPanel.map.layers.indexOf(wfsEdit) != -1)
        mapPanel.map.removeLayer(wfsEdit);
    if (varSettings.editim.objPanel.frmeditim)
        varSettings.editim.objPanel.frmeditim.removeAll();
    if (varSettings.editim.objPanel.bbar)
        varSettings.editim.objPanel.bbar.removeAll();
    Ext.getCmp('PanelEditimi').doLayout();

    eventInserUpdateAtribute = null;

    caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
    if (varSettings.editim.allControls.snapControl && varSettings.editim.allControls.snapControl.active)
        varSettings.editim.allControls.snapControl.deactivate();

    if (rightClick)
        rightClick.deactivate();

    thirrSave = false;
    editWindow.hide();
}

function shtoObjektetKopjeNeWfsEdit(objektet) {
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    var objektiRi;
    for (var i = 0; i < objektet.length; i++) {
        objektiRi = objektet[i].clone();
        objektiRi.state = OpenLayers.State.INSERT;
        wfsEdit.addFeatures(objektiRi);
        thirrSave = true;
    }
}

var koordinataXmouseRight;
var koordinataYmouseRight;
var rightClick;
var kontrolliRightClick = null;

function KlikoDjathte(kontrolli) {
    document.oncontextmenu = function noContextMenu(e) {
        return false;
    };//zhduk eventin right click te browserit

    if (!rightClick || kontrolliRightClick != kontrolli) {
        kontrolliRightClick = kontrolli;

        rightClick = new OpenLayers.Control.Click({
            eventMethods: {
                'rightclick': function (e) {
                    var menuItemRightClick = new Array();
                    if (kontrolli != varSettings.editim.controlClickFromWmsLayer) {
                        if (kontrolli == drawPoint && kontrolli != modifiko) {
                            undoRedo1.undo();
                        }
                        else if (kontrolli != modifiko) {
                            kontrolli.undo();
                        }
                    }
                    document.getElementById("rightClickMenuDiv").innerHTML = "";
                    koordinataXmouseRight = e.xy.x + panelLayer.getWidth();
                    koordinataYmouseRight = e.xy.y;
                    event1 = e;
                    var gjatesiaMenu = 35;

                    switch (kontrolli) {
                        case modifiko:
                            {
                                if (modifiko.feature) {
                                    menuItemRightClick.push({
                                        text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_PerfMod"),
                                        handler: finishModifikim
                                    });
                                }
                            }
                            break;
                        case drawPoint:
                            {
                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_XY"),
                                    handler: VendosXYmeKordinata
                                });
                            }
                            break;
                        case varSettings.editim.controlClickFromWmsLayer:
                            {
                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_Copy"),
                                    handler: function () {
                                        shtoObjektetKopjeNeWfsEdit(featureSelected);
                                    }
                                });
                            }
                            break;
                        default:
                            {
                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_XY"),
                                    handler: VendosXYmeKordinata
                                });

                                gjatesiaMenu = gjatesiaMenu * 4;
                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_dXdY"),
                                    handler: VendosdXdYmeKordinata
                                });

                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_DrejtGjat"),
                                    handler: VendosDrejtiminGjatesine
                                });

                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_PerfEdit"),
                                    handler: finishSketch
                                });

                                menuItemRightClick.push({
                                    text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_AnullEdit"),
                                    handler: cancelSketch
                                });
                            }
                            break;
                    }

                    /*if (kontrolli != modifiko) {
                         menuItemRightClick.push({
                         text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_XY"),
                         handler: VendosXYmeKordinata
                         });

                         if (kontrolli != drawPoint)
                         {
                             gjatesiaMenu = gjatesiaMenu * 4;
                             menuItemRightClick.push({
                             text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_dXdY"),
                             handler: VendosdXdYmeKordinata
                             });

                             menuItemRightClick.push({
                             text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_DrejtGjat"),
                             handler: VendosDrejtiminGjatesine
                             });

                             menuItemRightClick.push({
                             text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_PerfEdit"),
                             handler: finishSketch
                             });

                             menuItemRightClick.push({
                             text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_AnullEdit"),
                             handler: cancelSketch
                             });
                         }
                     }
                     else
                     {
                         if (modifiko.feature) {
                             menuItemRightClick.push({
                             text: perkthe("GP_EDITIM_RIGHTMENU_TEXT_PerfMod"),
                             handler: finishModifikim
                             });
                         }
                    }*/

                    var menuClickDjathte = new Ext.menu.Menu({
                        width: 180,
                        height: gjatesiaMenu,
                        closable: true,
                        floating: false, // usually you want this set to True (default)
                        renderTo: 'rightClickMenuDiv', // usually rendered by it's containing component
                        items: menuItemRightClick
                    });

                    if (menuItemRightClick.length > 0) {
                        menuClickDjathte.showAt([koordinataXmouseRight, koordinataYmouseRight]);
                    }
                }
            }
        })

        map.addControl(rightClick);
        rightClick.activate();
    }
    else {
        if (!rightClick.active) {
            rightClick.activate();
        }
    }
}

function hapDritarenInput2Fields(input1, input2) {
    var vendosInputEditPanelBItems = [
        {
            xtype: 'numberfield',
            width: 150,
            fieldLabel: input1,
            id: input1 + 'Vlera1',
            name: input1,
            decimalPrecision: 5
        }, {
            xtype: 'numberfield',
            width: 150,
            fieldLabel: input2,
            id: input2 + 'Vlera2',
            name: input2,
            decimalPrecision: 5
        }
    ];

    var vendosInputEditPanelButtons = [
        {
            text: perkthe("GP_MSG_buttonTextOk"),
            formBind: true,
            handler: function (toggled) {
                if (toggled) {
                    var vl1 = Ext.getCmp(input1 + "Vlera1").getValue();
                    var vl2 = Ext.getCmp(input2 + "Vlera2").getValue();
                    if (vl1 && vl2) {
                        if (input1 == "X" && input2 == "Y") {
                            var vlerat = new Array(vl1, vl2)
                            ndertoPikenXY('ok', vlerat, projeksioniDisplay, projeksioniGeo);
                            Ext.getCmp(input1 + input2 + 'Dritare').close();
                        }
                        else if (input1 == "dX" && input2 == "dY") {
                            var vlerat = new Array(Ext.getCmp(input1 + "Vlera1").getValue(), Ext.getCmp(input2 + "Vlera2").getValue());
                            ndertoPikendXdY('ok', vlerat);
                            Ext.getCmp(input1 + input2 + 'Dritare').close();
                        }
                        else if (input1 == 'grade' && input2 == 'meter') {
                            var vlerat = new Array(Ext.getCmp(input1 + "Vlera1").getValue(), Ext.getCmp(input2 + "Vlera2").getValue());
                            ndertoPikenSipasDrejtimitGjatesise('ok', vlerat);
                            Ext.getCmp(input1 + input2 + 'Dritare').close();
                        }
                    }
                    else {
                        Ext.getCmp(input1 + input2 + 'Dritare').close();
                    }
                }
            }
        }, {
            text: perkthe("GP_MSG_buttonTextCancel"),
            handler: function (toggled) {
                if (toggled) {
                    Ext.getCmp(input1 + input2 + 'Dritare').close();
                }
            }
        }
    ];
    gisElements.createGISElement("FormPanel", { id: "vendosInputEditPanelId", labelWidth: 30, bodyStyle: 'padding:5px 5px 0', items: vendosInputEditPanelBItems, buttons: vendosInputEditPanelButtons });
    gisElements.createGISElement("Window", { title: perkthe("GP_EDITIM_RIGHT_WIN_TIT_PlotXY") + ' ' + input1 + ' ' + input2, id: input1 + input2 + 'Dritare', width: 230, items: [gisElements.getGISElement("vendosInputEditPanelId")] });
};

function VendosXYmeKordinata() {
    var a = "a";
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    varSettings.shkoNeXY.ngaEditimi = true;
    varSettings.shkoNeXY.title = perkthe("GP_EDITIM_RIGHTCLICK_SHTO");
    shfaqDritaregoToXY();

}

function ndertoPikenXY(btn, values, sourceProj, destProj) { //, projeksioniDisplay, projeksioniGeo
    if (btn == 'ok') {
        if (values != null) {
            if (kontrolliRightClick == drawPoint) {
                var point = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(values[0], values[1]))
                var pikaTrans = transformoKordinatatPike(point, sourceProj, destProj);
                var point = new OpenLayers.Geometry.Point(pikaTrans.geometry.x, pikaTrans.geometry.y)
                kontrolliRightClick.drawFeature(point);
            }
            else {
                var point = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(values[0], values[1]))
                var pikaTrans = transformoKordinatatPike(point, sourceProj, destProj);
                kontrolliRightClick.insertXY(pikaTrans.geometry.x, pikaTrans.geometry.y);
            }
        }
    }
}

function VendosdXdYmeKordinata() {
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    hapDritarenInput2Fields('dX', 'dY');
}

function ndertoPikendXdY(btn, values) {
    if (btn == 'ok') {
        // var values = parseInput(text);
        if (values != null) {
            kontrolliRightClick.insertDeltaXY(values[0], values[1]);
        }
    }
}

function VendosDrejtiminGjatesine() {
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    // Ext.MessageBox.prompt('', 'Vendos drejtimin/gjatesine(grade,meter)', ndertoPikenSipasDrejtimitGjatesise);
    hapDritarenInput2Fields('grade', 'meter');
}

function ndertoPikenSipasDrejtimitGjatesise(btn, values) {
    if (btn == 'ok') {
        // var values = parseInput(text);
        if (values != null) {
            kontrolliRightClick.insertDirectionLength(values[0], values[1]);
        }
    }
}

function finishModifikim() {
    caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
    varSettings.editim.controlClickFromWmsLayer.activate();
    modifiko.activate();
}

function finishSketch() {
    kontrolliRightClick.finishSketch();
}

function cancelSketch() {
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    kontrolliRightClick.cancel();
}

function Editim(layer, nameLayer, oldLayer, id_layer, id_layer_type, nrStatusi) {
    if (eventInserUpdateAtribute != null || thirrSave) {
        Ext.MessageBox.show({
            title: perkthe("GP_EDITIM_MSG_TIT_RUAJ"),
            msg: perkthe("GP_EDITIM_MSG_MSG_RUAJ"),
            buttons: Ext.MessageBox.YESNOCANCEL,
            fn: function (btn) {
                YESNOCANCELResultLayerChanged(layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi);
            },
            icon: Ext.MessageBox.QUESTION
        });
    }
    else {
        pastroObjektLidhjeWeb(varSettings.editim.objLidhjeGisWeb.IDLAYER, varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, varSettings.editim.objLidhjeGisWeb.NRSTATUSI);
        merrKolonaLayerDheStatuseAfishimAsync(layer, nameLayer, id_layer, id_layer_type, nrStatusi);
    }
}

var vleraComboLayerEditim = "";
function YESNOCANCELResultLayerChanged(layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi) {
    switch (btn) {
        case "yes":
            RuajButton.handler.call(RuajButton.scope || RuajButton, RuajButton, "ndryshimLayeri", layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi);
            break;
        case "no":
            caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim);
            eventInserUpdateAtribute = null;
            thirrSave = false;
            pastroObjektLidhjeWeb(varSettings.editim.objLidhjeGisWeb.IDLAYER, varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, varSettings.editim.objLidhjeGisWeb.NRSTATUSI);
            merrKolonaLayerDheStatuseAfishimAsync(layer, nameLayer, id_layer, id_layer_type, nrStatusi);
            break;
        case "cancel":
            comboLayer2.store.clearFilter(); //duhet hequr filtrimi nga combo se perndryshe nuk e selekton elementin sic duhet.
            comboLayer2.setValue(oldLayer);
            break;
    }
}

function report(event) {
    OpenLayers.Console.log(event.type, event.feature ? event.feature.id : event.components);
}

function ShoqerojiObjektitTeDhenat() {
    if (eventInserUpdateAtribute != null) {
        thirrSave = true;
        var objF = eventInserUpdateAtribute.feature;

        objF.state = OpenLayers.State[STATE];
        for (l = 0; l < KolonatAfishimEditimGJ.length; l++) {
            var kolona = KolonatAfishimEditimGJ[l];
            if (kolona.EmerKolona != "the_geom" && kolona.EmerKolona != "gid") {
                fusha = kolona.EmerKolona;

                if (!(document.getElementById("" + kolona.EmerKolona + objF.id + ""))) {
                        noty({ text: perkthe("GP_EDITIM_MSG_MSG_RUAJErr"), type: "error" });
                }
                else {
                    if (document.getElementById("" + kolona.EmerKolona + objF.id + "").value == '') {
                        if (kolona.ObjColSchemaIsNull == "NO" && (kolona.ObjColSchemaDataType == "date" || kolona.ObjColSchemaDataType == "datetime")) {
                            objF.attributes[fusha] = '1899-12-29';
                        }
                        else if (kolona.ObjColSchemaIsNull == "NO" && (kolona.ObjColSchemaDataType != "date" || kolona.ObjColSchemaDataType != "datetime")) {
                            objF.attributes[fusha] = 0;
                        }
                        else {
                            objF.attributes[fusha] = null;
                        }
                    }
                    else {
                        objF.attributes[fusha] = document.getElementById("" + kolona.EmerKolona + objF.id + "").value;
                    }
                }
            }
        }
        eventInserUpdateAtribute = null;
    }
}

function vendosSipGjatesiPozXYGjateEditimit(event) {
    varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(event.feature);
    var geoTrans = event.feature.geometry.clone();
    geoTrans.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
    if ((document.getElementById("siperfaqja" + event.feature.id + ""))) {
        document.getElementById("siperfaqja" + event.feature.id + "").value = geoTrans.getArea();
    }
    if ((document.getElementById("gjatesia" + event.feature.id + ""))) {
        document.getElementById("gjatesia" + event.feature.id + "").value = geoTrans.getLength();
    }

    if ((document.getElementById("pozicioni_x" + event.feature.id + ""))) {
        var pikaGeomTranf = null;
        var pikaGeomTranf = transformoKordinatatPike(event.feature, projeksioniGeo, projeksioniDisplay)
        document.getElementById("pozicioni_x" + event.feature.id + "").value = pikaGeomTranf.geometry.x;
    }
    if ((document.getElementById("pozicioni_y" + event.feature.id + ""))) {
        if (pikaGeomTranf) {
            document.getElementById("pozicioni_y" + event.feature.id + "").value = pikaGeomTranf.geometry.y;
        }
        else {
            var pikaGeomTranf = transformoKordinatatPike(event.feature, projeksioniGeo, projeksioniDisplay)
            document.getElementById("pozicioni_y" + event.feature.id + "").value = pikaGeomTranf.geometry.y;
        }
    }
}

function josuksesUploadShpFile(mesazhi) {
    noty({ text: perkthe("GP_MSG_TIT_kujdes"), type: "error" });
}

function merrTipinGeometryAppNgaKlasa(featureClass) {
    var tipi;
    switch (featureClass) {
        case 'OpenLayers.Geometry.Polygon':
            tipi = "MULTIPOLYGON";
            break;
        case 'OpenLayers.Geometry.MultiPolygon':
            tipi = "MULTIPOLYGON";
            break;
        case 'OpenLayers.Geometry.LineString':
            tipi = "MULTILINESTRING";
            break;
        case 'OpenLayers.Geometry.MultiLineString':
            tipi = "MULTILINESTRING";
            break;
        case 'OpenLayers.Geometry.Point':
            tipi = "POINT";
            break;
        case 'OpenLayers.Geometry.MultiPoint':
            tipi = "POINT";
            break;
        default:
            break;
    }
    return tipi;
}

function kopjoShapeFileData(response) {
    mbyllDritareUploadShp();
    var loadShpMask = new Ext.LoadMask(Ext.getBody(), { msg: perkthe("GP_LOADMASK_LOAD_SHP") });
    loadShpMask.show();
    var karakteristikatShpUpload = new Array();

    karakteristikatShpUpload = response;
    if (karakteristikatShpUpload['success'] == 'false') {
        josuksesUploadShpFile(karakteristikatShpUpload['message']);
    }
    else {
        var parser = new OpenLayers.Format.GeoJSON();
        var vector = new OpenLayers.Layer.Vector("ShapeUpload");
        var shapefile = new Shapefile({ shp: '../../../UploadFiles/shapeFiles/' + karakteristikatShpUpload["message"] }, function (data) {
            //fshijShpUpload(karakteristikatShpUpload["message"]);
            var features = parser.read(data.geojson);
            if (features.length > 0) {
                var tipi;
                if (features[0]) {
                    /* switch (features[0].geometry.CLASS_NAME)
                     {
                     case 'OpenLayers.Geometry.Polygon':
                     tipi = "MULTIPOLYGON";
                     break;
                     case 'OpenLayers.Geometry.MultiPolygon':
                     tipi = "MULTIPOLYGON";
                     break;
                     case 'OpenLayers.Geometry.LineString':
                     tipi = "MULTILINESTRING";
                     break;
                     case 'OpenLayers.Geometry.MultiLineString':
                     tipi = "MULTILINESTRING";
                     break;
                     case 'OpenLayers.Geometry.Point':
                     tipi = "POINT";
                     break;
                     case 'OpenLayers.Geometry.MultiPoint':
                     tipi = "POINT";
                     break;

                     default:

                     break;
                     }*/
                    tipi = merrTipinGeometryAppNgaKlasa(features[0].geometry.CLASS_NAME);
                }
                if (tipi && tipi == TipiPikeVijePoligon) {
                    //var wktRead = new OpenLayers.Format.WKT();

                    for (var i = 0; i < features.length; i++) {
                        thirrSave = true;
                        features[i].state = OpenLayers.State.INSERT;
                        features[i].geometry.transform(new OpenLayers.Projection(projeksioniGeoV), new OpenLayers.Projection(projeksioniGeo));

                        switch (features[i].geometry.CLASS_NAME) {
                            case 'OpenLayers.Geometry.Polygon':
                                {
                                    /* var geoStr=features[i].geometry.toString();
                                     geoStr=geoStr.replace("POLYGON", "MULTIPOLYGON");
                                     geoStr=geoStr.replace("MULTIMULTIPOLYGON", "MULTIPOLYGON");
                                     features[i].geometry = wktRead.read(geoStr).geometry;*/

                                    var multiPol = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.MultiPolygon());
                                    multiPol.geometry.components.push(features[i].geometry);
                                    features[i].geometry = multiPol.geometry;
                                    delete multiPol;
                                    break;
                                }

                            case 'OpenLayers.Geometry.LineString':
                                {
                                    /*var geoStr=features[i].geometry.toString();
                                     geoStr.replace("LINESTRING", "MULTILINESTRING");
                                     geoStr.replace("MULTIMULTILINESTRING", "LINESTRING");
                                     features[i].geometry = wktRead.read(geoStr).geometry;*/

                                    var multiLin = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.MultiLineString());
                                    multiLin.geometry.components.push(features[i].geometry);
                                    features[i].geometry = multiLin.geometry;
                                    delete multiLin;
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    vector.addFeatures(features);
                    wfsEdit.addFeatures(features);
                    map.zoomToExtent(vector.getDataExtent());
                    delete vector;

                    loadShpMask.hide();
                    noty({ text: perkthe("GP_MSG_MSG_LoadShpSukses_msg"), type: "success" }); //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_Informacion"), perkthe("GP_MSG_MSG_LoadShpSukses_msg"));
                }
                else {
                    josuksesUploadShpFile(perkthe("GP_TIPI_GEOM_SHAPE_LOAD_GAB"))
                }
            } else {
                josuksesUploadShpFile(perkthe("GP_MSG_MSG_asnjeEdheneUploadShp"))
            }
        })
    }
}

function mbyllDritareUploadShp() {
    if (Ext.getCmp("formPanelShpUploadId")) {
        Ext.getCmp("formPanelShpUploadId").getForm().reset();
    }
    if (Ext.getCmp("winPanelShpUploadId")) {
        Ext.getCmp("winPanelShpUploadId").hide();
    }
}

function shfaqDritareUploadShp() {
    if (!Ext.getCmp("formPanelShpUploadId")) {
        var formpanelShUpload = new Ext.FormPanel({
            renderTo: Ext.getBody(),
            formId: 'formShpLoadId',
            fileUpload: true,
            id: "formPanelShpUploadId",
            width: 500,
            frame: true,
            autoHeight: true,
            bodyStyle: 'padding: 10px 10px 0 10px;',
            labelWidth: 50,
            defaults: {
                anchor: '95%',
                allowBlank: false,
                msgTarget: 'side',
                isUpload: true
            },
            items: [{
                xtype: 'fileuploadfield',
                id: 'filenameShp',
                name: 'filenameShp',
                fieldLabel: perkthe("GP_GPSPAN_FORM_FIELDNAME_SHP"),
                buttonText: perkthe("GP_GPSPAN_FORM_BTN_UPLOAD_SHP")
            }],
            buttons: [{
                text: perkthe("GP_SHPPAN_FORM_BTN_NGARKO"),
                formBind: true,
                handler: function () {
                    if (formpanelShUpload.getForm().isValid()) {
                        formpanelShUpload.getForm().submit({
                            url: 'GISUpload.aspx',
                            success: function (formpanelShUpload, action) {
                                kopjoShapeFileData(action.result);
                            },
                            failure: function (formpanelShUpload, action) {
                                josuksesUploadShpFile(action.result.message);
                            }
                        });
                    }
                }
            }, {
                text: perkthe("GP_SHPPAN_FORM_BTN_PastroFushat"),
                handler: function () {
                    Ext.getCmp("formPanelShpUploadId").getForm().reset();
                }
            }]
        });
    }

    var listenersUploadWindow = {
        'hide': function (win) {
            if (Ext.getCmp("uploadFileShpId").pressed)
                Ext.getCmp("uploadFileShpId").toggle();
        }
    };
    gisElements.createGISElement("Window", { title: perkthe("GP_WIN_TIT_NgarkoShpFile"), id: "winPanelShpUploadId", width: 500, closeAction: 'hide', items: Ext.getCmp("formPanelShpUploadId"), listeners: listenersUploadWindow });
}

var wfsEdit;
var saveStrategyEditim;
var refreshProtocol;
var wfsProtocol1;
var drawPolygon;
var drawLine;
var drawPoint;
var modifiko;
var selekto;
var featureSelected = [];
var STATE;
var eventUndoRedo;
var RuajButton;
var thirrSave = false;
var undoRedo1 = null;
var layerPerEditim;
var layerEditimiEshteHapur = true;
var featureNdodhetNeVektor = new Array();
var featureNukNdodhetNeVektor = new Array();
var featureSelectedEditim = new Array();
var arrayKontrolletEditim = new Array();



function KrijoEditimControls(layer, responseTextPostgresCombo) {
    drawPolygon = new OpenLayers.Control.DrawFeature(
            wfsEdit, OpenLayers.Handler.Polygon,
            {
                map: map,
                eventListeners: {
                    'activate': function () {
                        if (selekto) {
                            selekto.unselectAll();
                        }
                        caktivizoKontrolletAktivizoKontrollinEditim(drawPolygon, arrayKontrolletEditim);
                        KlikoDjathte(drawPolygon);
                        wfsEdit.events.register('sketchcomplete', wfsEdit, function (event) {
                            sketchcompleteEventHandler(event, responseTextPostgresCombo);
                        });
                        wfsEdit.events.register('featureadded', wfsEdit, function (event) {
                            varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(event.feature);
                        });
                        addUndoRedoFunction(drawPolygon);
                    },
                    'deactivate': function () {
                        if (varSettings.editim.objPanel.mbyllPopupEditimInserti) {
                            if (rightClick)
                                rightClick.deactivate();
                            wfsEdit.events.remove('sketchcomplete');
                            ShoqerojiObjektitTeDhenat();
                            pastroFormEditimi();
                            deleteUndoRedoFunction();
                        }
                    }
                }
            }
    );
    arrayKontrolletEditim.push(drawPolygon);

    drawLine = new OpenLayers.Control.DrawFeature(
            wfsEdit, OpenLayers.Handler.Path,
            {
                map: map,
                eventListeners: {
                    'activate': function () {
                        KlikoDjathte(drawLine);
                        if (selekto)
                            selekto.unselectAll();
                        caktivizoKontrolletAktivizoKontrollinEditim(drawLine, arrayKontrolletEditim);
                        wfsEdit.events.register('sketchcomplete', wfsEdit, function (event) {
                            sketchcompleteEventHandler(event, responseTextPostgresCombo);
                        });
                        wfsEdit.events.register('featureadded', wfsEdit, function (event) {
                            varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(event.feature);
                        });
                        addUndoRedoFunction(drawLine);
                    },
                    'deactivate': function () {
                        if (varSettings.editim.objPanel.mbyllPopupEditimInserti) {
                            rightClick.deactivate();
                            wfsEdit.events.remove('sketchcomplete');
                            ShoqerojiObjektitTeDhenat();
                            pastroFormEditimi();
                            deleteUndoRedoFunction();
                        }
                    }
                }
            }
    );
    arrayKontrolletEditim.push(drawLine);

    drawPoint = new OpenLayers.Control.DrawFeature(
            wfsEdit, OpenLayers.Handler.Point,
            {
                map: map,
                eventListeners: {
                    'activate': function () {
                        KlikoDjathte(drawPoint);
                        if (selekto)
                            selekto.unselectAll();
                        caktivizoKontrolletAktivizoKontrollinEditim(drawPoint, arrayKontrolletEditim);
                        wfsEdit.events.register('sketchcomplete', wfsEdit, function (event) {
                            sketchcompleteEventHandler(event, responseTextPostgresCombo);
                        });
                        wfsEdit.events.register('featureadded', wfsEdit, function (event) {
                            varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(event.feature);
                        });
                        addUndoRedoFunction(drawPoint);
                    },
                    'deactivate': function () {
                        if (varSettings.editim.objPanel.mbyllPopupEditimInserti) {
                            rightClick.deactivate();
                            wfsEdit.events.remove('sketchcomplete');
                            ShoqerojiObjektitTeDhenat();
                            pastroFormEditimi();
                            deleteUndoRedoFunction();
                        }
                    }
                }
            }
    );
    arrayKontrolletEditim.push(drawPoint);
}

// Tastiera funksionet per undo/redo ne insert
function addUndoRedoFunction(kontrolli) {
    varSettings.editim.allControls.undoRedoDrawFeatureControl = kontrolli;
    document.removeEventListener("keydown", UndoRedoFunction, false);
    document.oncontextmenu = function noContextMenu(e) { return false; };
    OpenLayers.Event.observe(document, "keydown", UndoRedoFunction);
};

function deleteUndoRedoFunction() {
    document.removeEventListener("keydown", UndoRedoFunction, false);
};

function UndoRedoFunction(evt) {
    var handled = false;
    switch (evt.keyCode) {
        case 90: // z
            if (evt.metaKey || evt.ctrlKey) {
                varSettings.editim.allControls.undoRedoDrawFeatureControl.undo();
                handled = true;
            }
            break;
        case 89: // y
            if (evt.metaKey || evt.ctrlKey) {
                varSettings.editim.allControls.undoRedoDrawFeatureControl.redo();
                handled = true;
            }
            break;
        case 27: // esc
            varSettings.editim.allControls.undoRedoDrawFeatureControl.cancel();
            handled = true;
            break;
    }
    if (handled) { OpenLayers.Event.stop(evt); }
};

function patroFilterWMSGjateEditim() {
    filterlayerPerEditimDelete2.clear();
    filterlayerPerEditimDeleteProt.clear();
    layerPerEditim.redraw();
}

var filterlayerPerEditimDelete2 = new Array();
var filterlayerPerEditimDeleteProt = new Array();

function pastroFormEditimi() {
    if (varSettings.editim.objPanel.frmeditim) {
        varSettings.editim.objPanel.frmeditim.removeAll();
        varSettings.editim.objPanel.bbar.hide();
    }
    if (mapPanel.map.layers.indexOf(varSettings.editim.allVectors.vektorMergePerSelectObjekti) != -1) {
        varSettings.editim.allVectors.vektorMergePerSelectObjekti.removeAllFeatures()
        mapPanel.map.removeLayer(varSettings.editim.allVectors.vektorMergePerSelectObjekti);
        varSettings.editim.allVectors.vektorMergePerSelectObjekti = null;
        Ext.getCmp("MergeButtonId").menu.removeAll();
    }
    Ext.getCmp('PanelEditimi').doLayout();

    document.getElementById("rightClickMenuDiv").innerHTML = "";
    Ext.getCmp('WindowEditimi').doLayout();
}

function Editim3(layer, TipiPikeVijePolig, nameLayer, id_layer, id_layer_type, listatAtributeveCombo, nrStatusi) {
    if (mapPanel.map.layers.indexOf(wfsEdit) != -1) {
        mapPanel.map.removeLayer(wfsEdit);
    }
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    varSettings.editim.objPanel.frmeditim.removeAll();

    Ext.getCmp('WindowEditimi').doLayout();

    eventInserUpdateAtribute = null;

    caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim);

    if (rightClick) rightClick.deactivate();

    comboLayer2 = new Ext.form.ComboBox({
        store: varSettings.editim.cmbLayesStore,
        displayField: 'DESCRIPTION',
        valueField: 'IDENTIFICATION',
        IdLayer: 'IDLAYER',
        mode: 'local',
        forceSelection: true,
        triggerAction: 'all',
        emptyText: perkthe("GP_EDITIMPAN_COMBO_emptyText"),
        selectOnFocus: true,
        id: "comboLayer2ID",
        listeners: {
            select: function (combo, record, index) {
                varSettings.editim.objOpenLayer.layerSelId = record.get('IDLAYER');
                varSettings.editim.objOpenLayer.layerSelNrStatus = record.json['NRSTATUSI'];
                Editim(combo.getValue(), combo.getRawValue(), combo.startValue, record.get('IDLAYER'), record.json['IDLAYERSTYPE'], record.json['NRSTATUSI']);
            }
        }
    });
    comboLayer2.setValue(layer);

    if (layer && layer != "bosh") {
        saveStrategyEditim = new OpenLayers.Strategy.Save({
            onCommit: function (response) {
                if (response.success()) {
                    patroFilterWMSGjateEditim();
                    showSuccessMsg(layer);
                }
                else {
                    showFailureMsg(response.priv.responseText)
                }
            }
        });

        wfsProtocol1 = new OpenLayers.Protocol.WFS.v1_1_0({
            url: "wfs",
            geometryName: "the_geom",
            featureNS: "GISnameSpace",
            featureType: layer,
            srsName: projeksioniDisplay,
            outputFormat: 'JSON'
        });
        var FixedStrategy = new OpenLayers.Strategy.Fixed();
        refreshProtocol = new OpenLayers.Strategy.Refresh({ interval: 1000, force: true });
        wfsEdit = new OpenLayers.Layer.Vector(layer + "_tempEdit", {
            strategies: [refreshProtocol, saveStrategyEditim],
            protocol: wfsProtocol1,
            styleMap: stilWfsEdit
        });
        refreshProtocol.deactivate();

        if (layerPerEditim && (!layerEditimiEshteHapur))
            layerPerEditim.setVisibility(false);

        layerPerEditim = map.getLayersByName(nameLayer)[0];
        layerEditimiEshteHapur = layerPerEditim.getVisibility();
        layerPerEditim.setVisibility(true);

        protocolFromWmsLayer = null;
        protocolFromWmsLayer = new OpenLayers.Protocol.WFS.fromWMSLayer(layerPerEditim);

        map.addLayer(wfsEdit);

        arrayKontrolletEditim = new Array();
        KrijoEditimControls(layer, listatAtributeveCombo);                          //Ketu krijohen kontrollet vizato poligon,pike ,vije
        krijoControlletPerEditimin(nameLayer, id_layer, layer, id_layer_type, comboLayer2);   //Te gjithe kontrollet e tjera qe lidhen me editimin

        if (undoRedo1 != null) {
            wfsEdit.events.remove('featureadded');
            wfsEdit.events.remove('beforefeatureremoved');
            wfsEdit.events.remove('beforefeaturemodified');
        }
        undoRedo1 = new UndoRedo(wfsEdit);
        undoRedo1.currentEditIndex = 0;
        undoRedo1.undoFeatures = [];
        undoRedo1.redoFeatures = [];
        undoRedo1.isEditMulty = false;

        var vizatoPoligonButton = new Ext.Button({
            enableToggle: true, id: "VizatoPoligonBntId", icon: varSettings.theme.urlCustom + 'editDrawPolygon.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_VizPol"), toggleGroup: "EditimGroup",
            handler: function (toggled) { vizatoObjekteButtonHandler(toggled, this, id_layer, id_layer_type, this.pressed, nrStatusi); }
        });

        var vizatoVijeButton = new Ext.Button({
            enableToggle: true, id: "VizatoVijeBntId", icon: varSettings.theme.urlCustom + 'editDrawLine.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_VizVije"), toggleGroup: "EditimGroup",
            handler: function (toggled) { vizatoObjekteButtonHandler(toggled, this, id_layer, id_layer_type, this.pressed, nrStatusi); }
        });

        var vizatoPikeButton = new Ext.Button({
            enableToggle: true, id: "VizatoPikeBntId", icon: varSettings.theme.urlCustom + 'editDrawPoint.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_VizPike"), toggleGroup: "EditimGroup",
            handler: function (toggled) { vizatoObjekteButtonHandler(toggled, this, id_layer, id_layer_type, this.pressed, nrStatusi); }
        });

        var EditButton = new Ext.Button({
            enableToggle: true, id: "EditButtonId", icon: varSettings.theme.urlCustom + 'editChange.png', tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Mod"), tooltipType: "title", toggleGroup: "EditimGroup",
            handler: function (toggled) {
                EditButtonHandler(toggled, this, listatAtributeveCombo, id_layer, id_layer_type, nrStatusi);
            }
        });

        var uploadButton = new Ext.Button({
            id: "uploadFileShpId", icon: varSettings.theme.urlCustom + 'editDownLoadShapefile.png', tooltip: perkthe("GP_TOLTIP_BTN_Upload_edit"), tooltipType: "title", enableToggle: true,
            handler: function (tooggled) {
                uploadButtonHandler(tooggled, this);
            }
        });

        RuajButton = new Ext.Button({
            id: "btnRuaj", icon: varSettings.theme.urlCustom + 'editSave.png', tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Ruaj"), tooltipType: "title",
            handler: function (toggled, veprimi, layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi) {
                if (varSettings.editim.objLidhjeGisWeb.veprimi == "DELETE") {
                    Ext.MessageBox.show({
                        title: perkthe("ADUI_MSG_Kujdes"), msg: perkthe("GP_BTN_RUAJBUTTON_Attention"), icon: Ext.MessageBox.QUESTION, buttons: Ext.MessageBox.YESNOCANCEL,
                        fn: function (bnt) {
                            if (bnt == "yes")
                                RuajBtnHandler(toggled, this, veprimi, layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi);
                            if (bnt == "no")
                                YesNoCancelEditimHapur(bnt);
                        },
                    });
                }
                else
                    RuajBtnHandler(toggled, this, veprimi, layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi);
            }
        });

        HapKarteleButton = new Ext.Button({
            id: "btnHapKarteleWeb", icon: varSettings.theme.urlCustom + 'editCartel.png', tooltip: perkthe("GP_BTN_TOOLTIP_Kartela"), tooltipType: "title",
            handler: function (toggled) { HapKarteleBtnHandler(toggled, this, id_layer, id_layer_type); }
        });

        HapAmbjentBlerjeButton = new Ext.Button({
            id: "btnHapBlerjeWeb", icon: varSettings.theme.urlCustom + 'editPurchase.png', tooltip: perkthe("GP_BTN_TOOLTIP_FatureBlerje"), tooltipType: "title",
            handler: function (toggled) { HapBlerjeBtnHandler(toggled, this, id_layer, id_layer_type); }
        });

        var selektoButton = new Ext.Button({
            id: "selectBntId", enableToggle: true, tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Selekt"), icon: varSettings.theme.urlCustom + 'editSelectFeature.png', toggleGroup: "EditimGroup", hidden: true,
            handler: function (toggled) { selektoButtonHandler(toggled, this); }
        });

        var FshijButton = new Ext.Button({
            id: "FshiButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editDelete.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Fshij"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                FshijButtonHandler(toggled, this, listatAtributeveCombo, id_layer, id_layer_type, nrStatusi);
            }
        });

        var ZoomButtonEditim = new Ext.Button({
            id: "ZoomButtonEditimId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editZoomIn.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Zoom"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                ZoomEditimButtonsHandler(toggled, this);
            }
        });

        var DragButtonEditim = new Ext.Button({
            id: "DragButtonEditimId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editMove.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_Drag"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                DragEditimButtonsHandler(toggled, this, varSettings.editim.allControls.dragControlEditim);
            }
        });

        var SnapButton = {
            id: "SnapButtonId", icon: varSettings.theme.urlCustom + 'editSnap.png',
            menu: new Ext.menu.Menu({
                items: [{
                    text: perkthe("GP_EDITIM_MEN_TEXT_SnapNF"), xtype: 'menucheckitem', checked: true,
                    checkHandler: function (elementi, checked) { SnapItemsButtonHandler(elementi, checked, varSettings.editim.allControls.snapControl, "node"); }
                }, {
                    text: perkthe("GP_EDITIM_MEN_TEXT_SnapN"), xtype: 'menucheckitem', checked: true,
                    checkHandler: function (elementi, checked) { SnapItemsButtonHandler(elementi, checked, varSettings.editim.allControls.snapControl, "vertex"); }
                }, {
                    text: perkthe("GP_EDITIM_MEN_TEXT_SnapGjObj"), xtype: 'menucheckitem', checked: true,
                    checkHandler: function (elementi, checked) { SnapItemsButtonHandler(elementi, checked, varSettings.editim.allControls.snapControl, "edge"); }
                }]
            })
        }

        var ToleranceField = krijoExtformNumberFieldPaDecimal('ToleranceFieldID', "ToleranceFieldID", false, '', '', 29, 10, 0);
        var RefreshButton = new Ext.Button({
            id: "RefreshButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editSnapRefresh.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_RefreshSnap"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                RefreshEditimButtonsHandler(toggled, this);
            }
        });

        var SplitButton = new Ext.Button({
            id: "SplitButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editSplitLine.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_SplitV"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                if (varSettings.editim.objLidhjeGisWebAfishim.cols.length == 0) {
                    Ext.getCmp('MergeButtonId').disable();
                    GeneralEditimButtonsHandler(toggled, this, varSettings.editim.allControls.splitControl);
                }
            }
        });

        var SplitButtonPolygon = new Ext.Button({
            id: "SplitButtonPolygonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editSplitPolygon.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_BTN_TOOLTIP_SplitP"), toggleGroup: "EditimGroup",
            handler: function (toggled) {
                Ext.getCmp('MergeButtonId').disable();
                GeneralEditimButtonsHandler(toggled, this, varSettings.editim.allControls.splitControlPolygon);
            }
        });

        var MergeButtonEditim = {
            id: "MergeButtonId", icon: varSettings.theme.urlCustom + 'editUnion.png', tooltipType: "title", tooltip: perkthe("GP_EDITIM_MEN_TEXT_BashkNeNje"), //enableToggle: true, toggleGroup: "EditimGroup",
            menu: new Ext.menu.Menu({
                listeners: {
                    "beforeshow": function (thisMenu, e, menuItem) {
                        varSettings.editim.objZgjedhurMerge = true;
                        Ext.getCmp('SplitButtonId').disable();
                        Ext.getCmp('SplitButtonPolygonId').disable();
                        MergeButtonHandler(thisMenu, nameLayer, id_layer_type);
                    },
                    "hide": function (thisMenu) {
                        varSettings.editim.objZgjedhurMerge = false;
                        if (varSettings.editim.controlClickFromWmsLayerMerge)
                            varSettings.editim.controlClickFromWmsLayerMerge.deactivate();
                        buttonUnPressedHandler(Ext.getCmp('MergeButtonId'));
                    }
                },
                items: []
            })
        };

        var ConnButton = new Ext.Button({
            id: "ConnButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editConnect.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_LidhObjektet"), toggleGroup: "EditimGroup", disabled: true,
            handler: function (toggled) {
                ConnButtonHandler(toggled, this, nameLayer, id_layer_type);
            }
        });

        var DeleteConnButton = new Ext.Button({
            id: "DeleteConnButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editDisconnect.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_HiqLidhjen"), toggleGroup: "EditimGroup", disabled: true,
            handler: function (toggled) {
                DeleteConnButtonHandler(toggled, this, nameLayer, id_layer_type);
            }
        });

        var NotConnButton = new Ext.Button({
            id: "NotConnButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editNotConnect.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_ObjekteTePalidhura"),
            handler: function (toggled) {
                TempLayerNotConnectedButtonHandler(toggled, this, id_layer, "TempLayerNotConnected");
            }
        });

        var PrindNgaLidhjeButton = new Ext.Button({
            id: "PrindNgaLidhjeButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editParent.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_Prinderit"),
            handler: function (toggled) {
                TempLayersFemijeButtonHandler(toggled, this, nameLayer, id_layer_type, "TempLayerFemiPrind");
            }
        });

        var HapElementeNgaBlerjeButton = new Ext.Button({
            id: "HapElementeNgaBlerjeButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editChild.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_BlerjeFemijet"),
            handler: function (toggled) {
                var paramsLloji = (varSettings.editim.objLidhjeGisWeb.IDMAGAZINA == 0 ? "" : ("FUNKSLLOJI=" + (id_layer_type == 5 ? "1" : "2") + " AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.IDMAGAZINA));
                TempLayersPrindButtonHandler(toggled, this, "TempLayerPrindFemi", paramsLloji );
            }
        });

        var FemijeLidhjeButton = new Ext.Button({
            id: "FemijeLidhjeButtonId", enableToggle: true, icon: varSettings.theme.urlCustom + 'editChildConn.png', tooltipType: "title", tooltip: perkthe("GP_BTN_TOOLTIP_LidhjeFemijet"),
            handler: function (toggled) {
                var paramsLloji = (varSettings.editim.objLidhjeGisWeb.gid == 0 ? "" : ("FUNKSLLOJI=4 AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.gid));
                TempLayersPrindButtonHandler(toggled, this, "TempLayerPrindFemi", paramsLloji );
            }
        });

        var ExportButton = {
            id: "ExportButtonId", icon: varSettings.theme.urlCustom + 'editDownload.png',
            menu: new Ext.menu.Menu({
                items: [
                    {
                        text: perkthe("GP_EXPMOD_BTN_MENU_ExShp"), icon: varSettings.theme.urlCustom + 'editDownloadShp.png',
                        handler: function () { new clsGISEditimExport().inicializoWin('SHAPE-ZIP', perkthe("GP_EXPMOD_BTN_MENU_ExShp"), id_layer_type, nrStatusi); } 
                    },
                    {
                        text: perkthe("GP_EXPMOD_BTN_MENU_ExCad"), icon: varSettings.theme.urlCustom + 'editDownloadAutoCad.png',
                        handler: function () { new clsGISEditimExport().inicializoWin('AUTOCAD', perkthe("GP_EXPMOD_BTN_MENU_ExCad"), id_layer_type, nrStatusi); } 
                    }, 
                    {
                        text: perkthe("GP_EXPMOD_BTN_MENU_ExExcel"), icon: varSettings.theme.urlCustom + 'editDownloadExcel.png',
                        handler: function () { new clsGISEditimExport().inicializoWin('EXCEL', perkthe("GP_EXPMOD_BTN_MENU_ExExcel"), id_layer_type, nrStatusi); } 
                    }, 
                    {
                        text: perkthe("GP_EXPMOD_BTN_MENU_ExCSV"), icon: varSettings.theme.urlCustom + 'editDownloadCsv.png',
                        handler: function () { new clsGISEditimExport().inicializoWin('CSV', perkthe("GP_EXPMOD_BTN_MENU_ExCSV"), id_layer_type, nrStatusi); }
                    }                    
                ]
            })
        };

        var ImportButton = {
            id: "ImportButtonId", icon: varSettings.theme.urlCustom + 'editUpload.png',
            menu: new Ext.menu.Menu({
                items: [
                    {
                        text: "Import nga GPS (.gpx)", icon: varSettings.theme.urlCustom + 'editUploadGPX.png',
                        handler: function () { new clsGISEditimImport().inicializoWin('GPX', "Import nga GPS (.gpx)", id_layer_type, nrStatusi); }
                    }
                ]
            })
        };

        var TopologyBtnEditim = new Ext.Button({
            id: "topologjiBtnId", icon: varSettings.theme.urlCustom + 'editTopology.png', tooltip: perkthe("GP_TOLTIP_BTN_Topologji_edit"), tooltipType: "title", enableToggle: true,
            handler: function (toggled) {
                var paramsLloji = (varSettings.editim.objLidhjeGisWeb.gid == 0 ? "" : ("FUNKSLLOJI=5 AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.gid));
                TempLayersPrindButtonHandler(toggled, this, "TempLayerPrindFemi", paramsLloji); //TopologyBtnEditimHandler(toggled, this);
            }
        });

        //Seksioni i vizatimit te butonave sipas llojit te geometrise se Layerit
        var teDrejtaPerLayer = varSettings.webConfig.teDrejta.filter(function (obj) { return obj.IdLayer == id_layer; });

        var arrayOfTopBarButtons = new Array();
        arrayOfTopBarButtons.push(comboLayer2);
        arrayOfTopBarButtons.push(vizatoPoligonButton);
        arrayOfTopBarButtons.push(vizatoVijeButton);
        arrayOfTopBarButtons.push(vizatoPikeButton);
        arrayOfTopBarButtons.push(EditButton);
        arrayOfTopBarButtons.push(FshijButton);
        arrayOfTopBarButtons.push(RuajButton);
        arrayOfTopBarButtons.push(NotConnButton);
        shtoButonatNeToolBar(id_layer_type, varSettings.editim.objPanel.tbar, arrayOfTopBarButtons, TipiPikeVijePolig, teDrejtaPerLayer);

        var arrayOfBottomBar1Buttons = new Array();
        arrayOfBottomBar1Buttons.push(ImportButton);
        arrayOfBottomBar1Buttons.push(selektoButton);
        arrayOfBottomBar1Buttons.push(HapKarteleButton);
        arrayOfBottomBar1Buttons.push(HapAmbjentBlerjeButton);
        arrayOfBottomBar1Buttons.push(HapElementeNgaBlerjeButton);
        arrayOfBottomBar1Buttons.push(ConnButton);
        arrayOfBottomBar1Buttons.push(PrindNgaLidhjeButton);
        arrayOfBottomBar1Buttons.push(FemijeLidhjeButton);
        arrayOfBottomBar1Buttons.push(DeleteConnButton);

        var arrayOfBottomBar2Buttons = new Array();
        arrayOfBottomBar2Buttons.push(ZoomButtonEditim);
        arrayOfBottomBar2Buttons.push(ExportButton);
        arrayOfBottomBar2Buttons.push(TopologyBtnEditim);
        arrayOfBottomBar2Buttons.push(DragButtonEditim);
        arrayOfBottomBar2Buttons.push(SplitButton);
        arrayOfBottomBar2Buttons.push(SplitButtonPolygon);
        arrayOfBottomBar2Buttons.push(MergeButtonEditim);
        arrayOfBottomBar2Buttons.push(SnapButton);
        arrayOfBottomBar2Buttons.push(ToleranceField);
        arrayOfBottomBar2Buttons.push(RefreshButton);

        shtoButonatNeToolBar(id_layer_type, varSettings.editim.objPanel.bbarItems1, arrayOfBottomBar1Buttons, TipiPikeVijePolig, teDrejtaPerLayer);
        shtoButonatNeToolBar(id_layer_type, varSettings.editim.objPanel.bbarItems2, arrayOfBottomBar2Buttons, TipiPikeVijePolig, teDrejtaPerLayer);
        editWindow.doLayout();
    }
    else//eshte zgjedhur Select Layer
    {
        varSettings.editim.objPanel.tbar.removeAll();
        varSettings.editim.objPanel.tbar.add(comboLayer2);
        varSettings.editim.objPanel.tbar.doLayout();
        editWindow.doLayout();
    }
};

function krijoControlletPerEditimin(nameLayer, id_layer, layer, id_layer_type, comboLayer2) {
    // Default per selektin e objekteve direkt ne sapo shtypet butoni i Editimit
    var urlEdit = Utils.getServerApiUrl("GIS", "MerrTeDhenatGeoEditimSelect");
    varSettings.editim.controlClickFromWmsLayer = krijoProtokollPerControlClickGetFeature(urlEdit, layer, filterlayerPerEditimDeleteProt, "", "0");
    varSettings.editim.controlClickFromWmsLayer.events.register("featuresselected", this, function (e) {
        featureSelectedEvent(e, Ext.getCmp("EditButtonId"), nameLayer, id_layer_type)
    });
    map.addControl(varSettings.editim.controlClickFromWmsLayer);

    //Default per selektin e objekteve direkt sapo shtypet butoni i fshirjes
    var urlDel = Utils.getServerApiUrl("GIS", "MerrTeDhenatGeoEditimSelect");
    varSettings.editim.controlClickFromWmsLayerDel = krijoProtokollPerControlClickGetFeature(urlDel, comboLayer2.value, [], "", "0");
    varSettings.editim.controlClickFromWmsLayerDel.events.register("featuresselected", this, function (e) {
        featureSelectedEvent(e, Ext.getCmp("FshiButtonId"), nameLayer, id_layer_type)
    });
    map.addControl(varSettings.editim.controlClickFromWmsLayerDel);

    // Kontrolli per selektimin
    selekto = new OpenLayers.Control.SelectFeature(wfsEdit, {
        toggle: false, multiple: false, hover: false, toggleKey: "ctrlKey", multipleKey: "shiftKey", onSelect: addSelected, onUnselect: clearSelected,
        eventListeners: {
            'activate': function () {
            },
            'deactivate': function () {
                clearSelected();
            }
        }
    })
    map.addControl(selekto);
    arrayKontrolletEditim.push(selekto);

    //Kontrolli i modifikimit
    modifiko = new OpenLayers.Control.ModifyFeatureT(wfsEdit, {
        eventListeners: {
            'activate': function () {
                KlikoDjathte(modifiko);
            },
            'deactivate': function () {
                if (rightClick && rightClick.active) {
                    rightClick.deactivate();
                }
                var shkaterro = true;

                if (typeof (Ext.getCmp("SplitButtonId")) != "undefined") {
                    if (Ext.getCmp("SplitButtonId").pressed || Ext.getCmp("DragButtonEditimId").pressed) shkaterro = false;
                }
                if (typeof (Ext.getCmp("SplitButtonPolygonId")) != "undefined") {
                    if (Ext.getCmp("SplitButtonPolygonId").pressed || Ext.getCmp("DragButtonEditimId").pressed) shkaterro = false;
                }
                if (shkaterro) {
                    shkaterroDritareModifikimMenuObj();
                    ShoqerojiObjektitTeDhenat();
                    pastroFormEditimi();
                }
            }
        },
        standalone: true,
        documentDrag: true,
        vertexRenderIntent: "vertex",
        virtualStyle: stilWfsEditVertexVirtual,
        toggle: false,
        onModificationEnd: function (el) {
        }
    });
    modifiko.mode = OpenLayers.Control.ModifyFeature.RESHAPE | OpenLayers.Control.ModifyFeature.DRAG | OpenLayers.Control.ModifyFeature.ROTATE; // | OpenLayers.Control.ModifyFeature.RESIZE; 
    map.addControl(modifiko);
    arrayKontrolletEditim.push(modifiko);

    //Kontrolli drag
    varSettings.editim.allControls.dragControlEditim = new OpenLayers.Control.DragFeature(wfsEdit, {
        onComplete: function (feature) {
            if (feature.attributes["gid"] && !feature.state) {
                feature.state = OpenLayers.State.UPDATE;
                varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(feature);
            }
        }
    });
    map.addControl(varSettings.editim.allControls.dragControlEditim);
    arrayKontrolletEditim.push(varSettings.editim.allControls.dragControlEditim);

    //Kontrolli per ndarjen e objekteve vije
    varSettings.editim.allControls.splitControl = new OpenLayers.Control.Split({
        layer: wfsEdit,
        deferDelete: true,
        eventListeners: {
            beforesplit: function (event) {
            },
            split: function (event) {
                SplitLineHandler(event);
            },
            aftersplit: function (event) {
                flashFeaturesSplit(event.features);
            }
        }
    });
    map.addControl(varSettings.editim.allControls.splitControl);
    arrayKontrolletEditim.push(varSettings.editim.allControls.splitControl);

    //Kontrolli per ndarjen e objekteve poligon
    varSettings.editim.allControls.splitControlPolygon = new OpenLayers.Control.DrawFeature(wfsEdit, OpenLayers.Handler.Path, {
        title: "Split",
        multi: true,
        map: map,
        handlerOptions: { 'style': sketchSymbolizersMat["Polygon"] },
        eventListeners: {
            'activate': function () {
                wfsEdit.events.register('sketchcomplete', wfsEdit, function (event) {
                    var proceed = true;
                    if (event.feature.geometry instanceof OpenLayers.Geometry.MultiLineString) {
                        varSettings.editim.objLidhjeGisWeb.gid = wfsEdit.features[0].attributes.gid;
                        SplitDrawPoligonHandler(event);
                        proceed = false;
                    }
                    return proceed;
                });
            }
        }
    });
    map.addControl(varSettings.editim.allControls.splitControlPolygon);
    arrayKontrolletEditim.push(varSettings.editim.allControls.splitControlPolygon);

    //Krijohet nje layer temp qe ruan objektet qe do te behen snap   
    var layerAktive = map.getLayersBy("visibility", true).filter(function (el) { return el.IDLAYERSTYPE > 2; }).map(function (value, index) { return value.IdLayer; });
    if (layerAktive.indexOf(id_layer) == -1) layerAktive.push(id_layer);
    if (map.getLayersBy("name", "SplitLayerEditim").length == 0) {
        var tempSplitLayer = new OpenLayers.Layer.Vector("SplitLayerEditim", {
            projection: projeksioniDisplay,
            strategies: [new OpenLayers.Strategy.BBOX({ resFactor: 1 }), new OpenLayers.Strategy.Refresh({ force: true })],
            protocol: new OpenLayers.Protocol.HTTP({ //protocol: new OpenLayers.Protocol.WFS.fromWMSLayer(map.getLayersBy("emriLayerGeo", "GISASETEOSHE:V_GIS_Layer_KERKIMISPECIFIK")[0]);
                url: Utils.getServerApiUrl("GIS", "MerrTeDhenatGeoEditimSnap"),
                format: new OpenLayers.Format.GeoJSON(),
                params: {
                    layers: JSON.stringify(layerAktive),
                    excludeGid: 0,
                    scopeID: Utils.getUrlVar("scopeID")
                },
                callbackKey: 'callback',
                readWithPOST: true
            }),
            minScale: 2000,
            styleMap: stilWfsEdit
        });
        map.addLayer(tempSplitLayer);
    }
    else {
        var tempSplitLayer = map.getLayersBy("name", "SplitLayerEditim")[0];
        tempSplitLayer.protocol.params.layers = JSON.stringify(map.getLayersBy("visibility", true).filter(function (el) { return el.IDLAYERSTYPE > 2; }).map(function (value, index) { return value.IdLayer; })),
        tempSplitLayer.protocol.params.excludeGid = varSettings.editim.objLidhjeGisWeb.gid;
        tempSplitLayer.strategies[1].refresh();
    };

    varSettings.editim.allControls.snapControl = new OpenLayers.Control.Snapping({
        layer: wfsEdit,
        greedy: true,
        tolerance: 10,
        targets: [{
            layer: tempSplitLayer,
            tolerance: 10,
            edge: true,
            node: true,
            vertex: true
        }]
    });
    varSettings.editim.allControls.snapControl.activate();
}

function addSeparatorIfVisible(tempToolbar, myTmpButton) {
    if (!myTmpButton.hidden) {
        tempToolbar.add('-');
        tempToolbar.add(' ');
    }
    return;
};

function shtoButonatNeToolBar(id_layer_type, tempToolbar, arrayOfTbButtons, TipiPikeVijePolig, teDrejtaPerLayer) {
    tempToolbar.removeAll();
    var showKujdes = false; // ishte default true dhe kur shfaqej butoni i insert behej false pse???
    for (var i = 0, nrButtonash = arrayOfTbButtons.length; i < nrButtonash; i++) {
        var myTmpButton = arrayOfTbButtons[i];
        switch (myTmpButton.id) {
            case "VizatoPoligonBntId":
                if (((TipiPikeVijePolig == "MULTIPOLYGON") || (TipiPikeVijePolig == "POLYGON")) && teDrejtaPerLayer[0].DShtim) {
                    tempToolbar.add(myTmpButton);  //tempToolbar.add(snapButton);tempToolbar.add(splitButtonPolygon);tempToolbar.add(dragButtonEditim);tempToolbar.add(mergeButtonEditim);tempToolbar.add(TopologyBtnEditim);tempToolbar.add(uploadButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "VizatoVijeBntId":
                if (((TipiPikeVijePolig == "MULTILINESTRING") || (TipiPikeVijePolig == "LINESTRING")) && teDrejtaPerLayer[0].DShtim) {
                    tempToolbar.add(myTmpButton);     //tempToolbar.add(snapButton);tempToolbar.add(splitButton);tempToolbar.add(dragButtonEditim);tempToolbar.add(mergeButtonEditim);tempToolbar.add(uploadButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "VizatoPikeBntId":
                if (((TipiPikeVijePolig == "POINT") || (TipiPikeVijePolig == "MULTIPOINT")) && teDrejtaPerLayer[0].DShtim) {
                    tempToolbar.add(myTmpButton);    //tempToolbar.add(dragButtonEditim);tempToolbar.add(TopologyBtnEditim);tempToolbar.add(uploadButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "EditButtonId":
                if (teDrejtaPerLayer[0].DMod || (!teDrejtaPerLayer[0].DMod && !teDrejtaPerLayer[0].DShtim && !teDrejtaPerLayer[0].DFsh && teDrejtaPerLayer[0].DAmb)) {
                    tempToolbar.add(myTmpButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "FshiButtonId":
                if (teDrejtaPerLayer[0].DFsh) {
                    tempToolbar.add(myTmpButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "ConnButtonId":
            case "DeleteConnButtonId":
                if (StatuseLayeri[0].WEBLLOJI == "MAGAZINA" && id_layer_type != 6)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "btnRuaj":
                myTmpButton.disabled = true;
                if (!teDrejtaPerLayer[0].DShtim && !teDrejtaPerLayer[0].DMod && !teDrejtaPerLayer[0].DFsh)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "btnHapKarteleWeb":
                myTmpButton.disabled = true;
                if (varSettings.editim.objLidhjeGisWeb.NRSTATUSI == 3 && StatuseLayeri[0].WEBLLOJI != "GIS")
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "btnHapBlerjeWeb":
                if (StatuseLayeri[0].WEBLLOJI != "GIS") {
                    myTmpButton.disabled = true;
                    tempToolbar.add(myTmpButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "PrindNgaLidhjeButtonId":
                if ((StatuseLayeri[0].WEBLLOJI == "MAGAZINA" && id_layer_type != 6) || varSettings.editim.objLidhjeGisWeb.NRSTATUSI == 3)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "HapElementeNgaBlerjeButtonId":
                if (!(StatuseLayeri[0].WEBLLOJI == "MAGAZINA") || varSettings.editim.objLidhjeGisWeb.NRSTATUSI == 3)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "NotConnButtonId":
                if (StatuseLayeri[0].WEBLLOJI == "MAGAZINA" && id_layer_type != 6)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "SplitButtonId":
                if (TipiPikeVijePolig != "MULTILINESTRING" && TipiPikeVijePolig != "LINESTRING")
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "ImportButtonId":
            case "SplitButtonPolygonId":
                if (TipiPikeVijePolig != "MULTIPOLYGON" && TipiPikeVijePolig != "POLYGON")
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "MergeButtonId":
                if (TipiPikeVijePolig != "MULTIPOLYGON" && TipiPikeVijePolig != "POLYGON" && TipiPikeVijePolig != "MULTILINESTRING" && TipiPikeVijePolig != "LINESTRING")
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "SnapButtonId":
            case "ToleranceFieldID":
                tempToolbar.add(myTmpButton);
                break;
            case "RefreshButtonId":
                tempToolbar.add('    ');
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
            case "DragButtonEditimId":
                if (varSettings.editim.objLidhjeGisWeb.veprimi == "INSERT") {
                    tempToolbar.add(myTmpButton);
                    addSeparatorIfVisible(tempToolbar, myTmpButton);
                }
                break;
            case "ExportButtonId":
                if (!teDrejtaPerLayer[0].DEksporto)
                    myTmpButton.hidden = true;
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;

            case "ZoomButtonEditimId":
            case "FemijeLidhjeButton":
            case "topologjiBtnId":
            default:
                tempToolbar.add(myTmpButton);
                addSeparatorIfVisible(tempToolbar, myTmpButton);
                break;
        }
    }
    if (showKujdes)
        noty({ text: perkthe("GP_EDITIM_MSG_MSG_ErrServ"), type: "error" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_ErrServ"));

    if (tempToolbar.length == 0)
        tempToolbar.hide();
    else
        tempToolbar.show();
    tempToolbar.doLayout();
};
function enableButonaEditimSipasRastit(enableSelect, enableVizPike, enableVizVije, enableVizPoligon, enableEdit, enableFshi, enableObjConn, enableRuaj, enableSplit, enableMerge, enableSnap, enableExporto) {
    enableSelect ? Ext.getCmp('selectBntId').enable() : Ext.getCmp('selectBntId').disable();
    enableVizPike ? Ext.getCmp('VizatoPikeBntId').enable() : Ext.getCmp('VizatoPikeBntId').disable();
    enableVizVije ? Ext.getCmp('VizatoVijeBntId').enable() : Ext.getCmp('VizatoVijeBntId').disable();
    enableVizPoligon ? Ext.getCmp('VizatoPoligonBntId').enable() : Ext.getCmp('VizatoPoligonBntId').disable();
    enableEdit ? Ext.getCmp('EditButtonId').enable() : Ext.getCmp('EditButtonId').disable();
    enableFshi ? Ext.getCmp('FshiButtonId').enable() : Ext.getCmp('FshiButtonId').disable();
    enableObjConn ? Ext.getCmp('ConnButtonId').enable() : Ext.getCmp('ConnButtonId').disable();
    enableObjConn ? Ext.getCmp('DeleteConnButtonId').enable() : Ext.getCmp('DeleteConnButtonId').disable();
    enableRuaj ? Ext.getCmp('btnRuaj').enable() : Ext.getCmp('btnRuaj').disable();
    enableRuaj ? Ext.getCmp('btnHapKarteleWeb').enable() : Ext.getCmp('btnHapKarteleWeb').disable();
    enableRuaj ? Ext.getCmp('btnHapBlerjeWeb').enable() : Ext.getCmp('btnHapBlerjeWeb').disable();

    enableSplit ? Ext.getCmp('SplitButtonId').enable() : Ext.getCmp('SplitButtonId').disable();
    enableSplit ? Ext.getCmp('SplitButtonPolygonId').enable() : Ext.getCmp('SplitButtonPolygonId').disable();

    enableMerge ? Ext.getCmp('MergeButtonId').enable() : Ext.getCmp('MergeButtonId').disable();

    enableSnap ? Ext.getCmp('SnapButtonId').enable() : Ext.getCmp('SnapButtonId').disable();
    enableSnap ? Ext.getCmp('RefreshButtonId').enable() : Ext.getCmp('RefreshButtonId').disable();

    enableExporto ? Ext.getCmp('ExportButtonId').enable() : Ext.getCmp('ExportButtonId').disable();
};

function MerrTeDrejtaSipasPerdoruesit(teDrejtat) {
    varSettings.webConfig.teDrejta = teDrejtat.map(function (eDrejta, index) {
        var resDrejta = {
            IdLayer: eDrejta.IDLAYER,
            DAmb: eDrejta.D_AMB,
            DShtim: eDrejta.D_SHTIM,
            DMod: eDrejta.D_MOD,
            DFsh: eDrejta.D_FSH,
            DKerko: eDrejta.D_KERKO,
            DEksporto: eDrejta.D_EKSPORTO,
            DPrinto: eDrejta.D_PRINTO
        }
        return resDrejta;
    });
};

function MerrProjeksionePerProjeksion(projeksione) {
    varSettings.webConfig.projeksione = projeksione.map(function (projeksioni, index) {
        var resProjeksioni = {
            AreaOfUse: projeksioni.AREAOFUSE,
            AuthorityName: projeksioni.AUTHORITYNAME,
            AuthoritySpatialRefId: projeksioni.AUTHORITYSPATIALREFID,
            Crs: projeksioni.CRS,
            Datum: projeksioni.DATUM,
            Description: projeksioni.DESCRIPTION,
            DisplayCode: projeksioni.AUTHORITYNAME + ":" + projeksioni.AUTHORITYSPATIALREFID,
            Ellipsoid: projeksioni.ELLIPSOID,
            IdGeoProjection: projeksioni.IDGEOPROJECTION,
            PrimeMeridian: projeksioni.PRIMEMERIDIAN,
            Proj4: projeksioni.PROJ4,
            RevisionDate: projeksioni.REVISIONDATE,
            SpatialRefId: projeksioni.SPATIALREFID,
            Type: projeksioni.TYPE,
            Unit: projeksioni.UNIT,
            WellKnowText: projeksioni.WELLKNOWTEXT,
        }
        Proj4js.defs[resProjeksioni.DisplayCode] = resProjeksioni.Proj4;
        return resProjeksioni;
    });
    ConvertProjeksioneFromArrayOfObjectsToArrayOfArrays();
};

function ConvertProjeksioneFromArrayOfObjectsToArrayOfArrays() {
    varSettings.webConfig.projeksioneArrayStore = new Ext.data.ArrayStore({
        fields: ['AreaOfUse', 'AuthorityName', 'AuthoritySpatialRefId', 'Crs', 'Datum', 'Description', 'DisplayCode', 'Ellipsoid', 'IdGeoProjection', 'PrimeMeridian', 'Proj4', 'RevisionDate', 'SpatialRefId', 'Type', 'Unit', 'WellKnowText'],
        data: ConvertArrayOfObjectsToArrayOfArrays(varSettings.webConfig.projeksione)
    });
}

function ConvertArrayOfObjectsToArrayOfArrays(input) {
    var output = input.map(function (obj) {
        return Object.keys(obj).sort().map(function (key) {
            return obj[key];
        });
    });

    return output;
};

function dynamicSort(property) {
    return function (obj1, obj2) {
        return obj1[property] > obj2[property] ? 1
            : obj1[property] < obj2[property] ? -1 : 0;
    }
};

function dynamicSortMultiple() {
    var props = arguments;
    return function (obj1, obj2) {
        var i = 0, result = 0, numberOfProperties = props.length;
        while (result === 0 && i < numberOfProperties) {
            result = dynamicSort(props[i])(obj1, obj2);
            i++;
        }
        return result;
    }
};

function uniqueArrayByType(dublicateArrayOfObjects) {
    var unique = [];
    $.each(dublicateArrayOfObjects, function (i, el) {
        if ($.inArray(el.TYPE, unique) === -1) unique.push(el.TYPE);
    });
    return unique;
}

function ConvertValueTypeByLlojFushe(idllojFushe, vleraFushe) {
    var convertVlera;

    switch (idllojFushe) {
        case 1:
            convertVlera = parseInt(vleraFushe);
            break;
        case 2:
            convertVlera = parseFloat(vleraFushe);
            break;
        case 8:
            convertVlera = ((vleraFushe == 'true') ? true : false);
            break;
        case 9:
            convertVlera = new OpenLayers.Size(parseInt(vleraFushe), parseInt(vleraFushe));
            break;
        default:
            convertVlera = vleraFushe;
            break;
    };

    return convertVlera;
};

function createLayerParametersForOpenLayers(filteredFieldsForBaseLayer) {
    var parameters = { name: "", url: [], params: {}, options: {} };    

    for (var j = 0; j < filteredFieldsForBaseLayer.length; j++) {
        switch (filteredFieldsForBaseLayer[j].PARAMETERS) {
            case "name":
                parameters.name = ConvertValueTypeByLlojFushe(filteredFieldsForBaseLayer[j].IDLLOJFUSHE, filteredFieldsForBaseLayer[j].BASEVALUE);
                break;
            case "url":
                parameters.url.push(ConvertValueTypeByLlojFushe(filteredFieldsForBaseLayer[j].IDLLOJFUSHE, filteredFieldsForBaseLayer[j].BASEVALUE));
                break;
            case "params":
                parameters.params[filteredFieldsForBaseLayer[j].BASEKEY] = ConvertValueTypeByLlojFushe(filteredFieldsForBaseLayer[j].IDLLOJFUSHE, filteredFieldsForBaseLayer[j].BASEVALUE);
                break;
            default:
                parameters.options[filteredFieldsForBaseLayer[j].BASEKEY] = ConvertValueTypeByLlojFushe(filteredFieldsForBaseLayer[j].IDLLOJFUSHE, filteredFieldsForBaseLayer[j].BASEVALUE);
                break;
        }
    };
    return parameters;
};

function addParametersToOpenLayersStandart(layer, parameters) {
    parameters.params["styles"] = layer.STYLEIDENTIFICATION;

    parameters.options["IdLayer"] = layer.IDLAYER;
    parameters.options["IDLAYERSTYPE"] = layer.IDLAYERSTYPE;
    parameters.options["IDENTIFICATION"] = layer.IDENTIFICATION;
    parameters.options["NRSTATUSI"] = layer.NRSTATUSI;
    parameters.options["GEOMETRYLAYER"] = layer.GEOMETRYLAYER;
    parameters.options["DEFAULTOBJEKT"] = layer;

    if (layer.IDLAYERSTYPE != 1) {
        var cqlFilter;
        if (layer.IDGRSTRUCTURE == 1 && layer.IDLAYERSTYPE == 2)
            cqlFilter = 'IDAUTORIZUESI=0 ';
        else if ((layer.IDGRSTRUCTURE == 2 || layer.IDGRSTRUCTURE == 3) && layer.WITHAUTHORIZATION)
            cqlFilter = 'IDAUTORIZUESI=' + varSettings.webConfig.idPerdoruesi + ' ';
        else if (((layer.IDGRSTRUCTURE == 2 || layer.IDGRSTRUCTURE == 3) && !layer.WITHAUTHORIZATION) || (layer.IDLAYERSTYPE == 3))
            cqlFilter = 'IDAUTORIZUESI=-1 ';
        else
            cqlFilter = '1=1 ';

        if (layer.WITHFISCALYEAR)
            cqlFilter += 'AND IDNDERMVIT=' + varSettings.webConfig.idNdermVit;
        else
            cqlFilter += 'AND IDNDERMVIT=0';

        parameters.params["cql_filter"] = cqlFilter;
    }

    if (parameters.options.displayInLayerSwitcher)
        parameters.options["group"] = layer.TREEPATH;
};

function addLayerToOpenLayers(layer, parameters, countLayers, currentLayerIndex) {
    var baseLayerTemp = undefined;

    switch (layer.IDGROPENLAYERS) {
        case 'Google':
            if ((typeof google === 'object' && typeof google.maps === 'object'))
                baseLayerTemp = new OpenLayers.Layer.Google(parameters.name, parameters.options);            
            break;
        case 'Bing':
            baseLayerTemp = new OpenLayers.Layer.Bing(parameters.options);
            break;
        case 'XYZ':
            baseLayerTemp = new OpenLayers.Layer.XYZ(parameters.name, parameters.url, parameters.options);
            break;
        case 'XYZ_A_T':
            baseLayerTemp = new OpenLayers.Layer.XYZ_A_T(parameters.name, parameters.url, parameters.options);
            break;
        case 'OSM':
            baseLayerTemp = new OpenLayers.Layer.OSM(parameters.name, parameters.url, parameters.options);
            break;
        case 'WMS':
            baseLayerTemp = new OpenLayers.Layer.WMS(parameters.name, parameters.url[0], parameters.params, parameters.options);
            break;
    };

    return baseLayerTemp;
};

function krijoProtokollPerControlClickGetFeature(urlWebService, layer, filterGid, geoJSON, idObjFillestar) {
    var tempControlOpenLayer = new OpenLayers.Control.GetFeatureT({
        protocol: new OpenLayers.Protocol.HTTP({
            url: urlWebService,
            format: new OpenLayers.Format.GeoJSON(),
            params: { layer: layer, filterGid: JSON.stringify(filterGid), geometry: geoJSON, idObjFillestar: idObjFillestar, scopeID: Utils.getUrlVar("scopeID") },
            readWithPOST: true
        }),
        box: true, click: true, toggleKey: "ctrlKey", clickout: false, toggle: false, hover: false, clickTolerance: 1,
        eventListeners: {
            'deactivate': function () {
                if (document.getElementById("rightClickMenuDiv")) {
                    document.getElementById("rightClickMenuDiv").innerHTML = "";
                }
                if (rightClick && rightClick.active) {
                    rightClick.deactivate();
                }
            }
        }
    });
    arrayKontrolletEditim.push(tempControlOpenLayer);
    return tempControlOpenLayer
};

function ndryshoProtokollParamsPerControlClickGetFeature(tempControlOpenLayer, layer, filterGid, geoJSON, idObjFillestar) {
    tempControlOpenLayer.protocol.params.layer = layer;
    tempControlOpenLayer.protocol.params.filterGid = JSON.stringify(filterGid);
    tempControlOpenLayer.protocol.params.geometry = geoJSON;
    tempControlOpenLayer.protocol.params.idObjFillestar = idObjFillestar;

    return tempControlOpenLayer;
}

function transaksionLidhjeObjekteshGISWEB() {
    var keys = Object.keys(varSettings.editim.objLidhjeGisWeb);
    for (var i = 0; i < keys.length; i++) {
        if (varSettings.editim.objLidhjeGisWeb[keys[i]] == "" && (keys[i] == "IDKODIFIKIMI" || keys[i] == "IDARTIKULLI" || keys[i] == "IDSERIALI" || keys[i] == "IDKOKADOK" || keys[i] == "IDTRUPIDOKLIDHES" || keys[i] == "IDPERDORUESI" || keys[i] == "IDNDERMARJE" || keys[i] == "gidPrindi"))
            varSettings.editim.objLidhjeGisWeb[keys[i]] = 0;
    }
    var colObjLidhjeGisWeb = [];
    if (varSettings.editim.objLidhjeGisWebAfishim.cols.length == 0)
        colObjLidhjeGisWeb.push(varSettings.editim.objLidhjeGisWeb);
    else
        var colObjLidhjeGisWeb = varSettings.editim.objLidhjeGisWebAfishim.cols.map(function (value, index) {
            var tempObj = $.extend({}, varSettings.editim.objLidhjeGisWeb);
            tempObj.Veprimi = value.Veprimi;
            tempObj.gid = value.gid;
            tempObj.the_geom = value.the_geom;
            return tempObj;
        });

    $.ajax({
        url: Utils.getServerApiUrl("GIS", "transaksionLidhjeObjekteshGISWEB"),
        data: JSON.stringify({ veprimi: varSettings.editim.objLidhjeGisWeb.veprimi, colObjekte: colObjLidhjeGisWeb })
    }).done(function () {
        switch (varSettings.editim.objLidhjeGisWeb.veprimi) {
            case "INSERT": noty({ text: perkthe("GP_EDITIM_MSG_RUAJOK"), type: "success" }); break; // Ext.MessageBox.alert("GIS", perkthe("GP_EDITIM_MSG_RUAJOK")); break;
            case "UPDATE": noty({ text: perkthe("GP_EDITIM_MSG_MODIFIKOOK"), type: "success" }); break; // Ext.MessageBox.alert("GIS", perkthe("GP_EDITIM_MSG_MODIFIKOOK")); break;
            case "DELETE": noty({ text: perkthe("GP_EDITIM_MSG_FSHIOK"), type: "success" }); break; // Ext.MessageBox.alert("GIS", perkthe("GP_EDITIM_MSG_FSHIOK")); break;
        }
        var layerPerRifreskim = map.getLayersBy("IdLayer", varSettings.editim.objLidhjeGisWeb.IDLAYER);
        layerPerRifreskim[0].redraw(true);
        pastroObjektLidhjeWeb(varSettings.editim.objOpenLayer.layerSelId, varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, varSettings.editim.objOpenLayer.layerSelNrStatus);
        enableButonaEditimSipasRastit(true, true, true, true, true, true, false, false, false, false, false, false);
        showSuccessMsg(layerPerRifreskim);
        dragPanControl.activate();
    }).fail(function (errMsg) {
        noty({ text: perkthe("GP_EDITIM_MSG_MSG_RuajError"), type: "error" }); //Ext.MessageBox.alert("GIS", perkthe("GP_EDITIM_MSG_MSG_RuajError"));
    });
};

function pastroObjektLidhjeWeb(id_layer, id_layer_type, nrStatusi) {
    varSettings.editim.objLidhjeGisWeb = {
        veprimi: 'DEFAULT',
        gid: 0,
        the_geom: 'POLYGON((2206019.853779966 5060175.552445232,2206036.241747844 5060178.842506439,2206019.8990954123 5060257.708884846,2206004.154248417 5060254.558201708,2206019.853779966 5060175.552445232))',
        IDLAYER: id_layer,
        IDLAYERSTYPE: id_layer_type,
        IDMAGAZINA: 0,
        IDKODIFIKIMI: 0,
        IDARTIKULLI: 0,
        IDSERIALI: 0,
        IDKOKADOK: 0,
        IDTRUPIDOKLIDHES: 0,
        DTMODIFIKIMI: '01/01/1990',
        IDPERDORUESI: varSettings.webConfig.idPerdoruesi,
        IDNDERMARJE: varSettings.webConfig.idNdermarrja,
        KODI: '',
        PERSHKRIMI: '',
        SERIALKOD: '',
        KODKODIFIKIMI: '',
        gidPrindi: 0,
        kodPrindi: '',
        NRSTATUSI: nrStatusi,
    };
    varSettings.editim.objLidhjeGisWebAfishim = varSettings.editim.objLidhjeGisWeb;
    varSettings.editim.objLidhjeGisWebAfishim.ADRESA = '';
    varSettings.editim.objLidhjeGisWebAfishim.cols = [];
};

function flashFeaturesSplit(features, index) {
    if (!index)
        index = 0;
    var current = features[index];
    if (current && current.layer === wfsEdit)
        wfsEdit.drawFeature(features[index], "select");

    var prev = features[index - 1];
    if (prev && prev.layer === wfsEdit)
        wfsEdit.drawFeature(prev, "default");

    ++index;
    if (index <= features.length) {
        window.setTimeout(function () { flashFeaturesSplit(features, index) }, 400);
    }
}


function merrKolonaLayerDheStatuseAfishimAsync(layer, nameLayer, id_layer, id_layer_type, nrStatusi) {
    $.ajax({
        url: Utils.getServerApiUrl("GIS", "getRowsForEditWindow"),
        data: JSON.stringify({ layerType:id_layer_type, statusi:nrStatusi })
    }).done(function (result) {
        KolonatAfishimEditimGJ = result.tempLC;
        StatuseLayeri = JSON.parse(result.statuset);
        hapLayerTjeterEditim(layer, nameLayer, id_layer, id_layer_type, nrStatusi);
    });
}

var KolonatAfishimEditimGJ;
var TipiPikeVijePoligon;
var WebllojLayer;

function hapLayerTjeterEditim(layer, nameLayer, id_layer, id_layer_type, nrStatusi) {
    varSettings.editim.objLidhjeGisWeb.IDLAYER = id_layer;
    varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE = id_layer_type;
    varSettings.editim.objLidhjeGisWeb.NRSTATUSI = nrStatusi;
    $.extend(varSettings.editim.objLidhjeGisWebAfishim, varSettings.editim.objLidhjeGisWeb);
    shfaqLayerTempMeParametra("TempLayerPrindFemi", { "CQL_FILTER": 'IDAUTORIZUESI=0 AND IDNDERMVIT=0' }, false, false);
    shfaqLayerTempMeParametra("TempLayerNotConnected", { "CQL_FILTER": 'IDAUTORIZUESI=0 AND IDNDERMVIT=0' }, false, false);
    shfaqLayerTempMeParametra("TempLayerFemiPrind", { "viewparams": 'gid:0' }, false, false);
    TipiPikeVijePoligon = KolonatAfishimEditimGJ[0]["ObjType"];
    merrVleratPerComboAndCallEditim(layer, TipiPikeVijePoligon, nameLayer, id_layer, id_layer_type, nrStatusi);
}

function resetVleraArrayGlobalesh() {
}

function merrVleratPerComboAndCallEditim(layer, TipiPikeVijePoligon, nameLayer, id_layer, id_layer_type, nrStatusi) {
    //TODO Denisa te zhduket kudo perdorimi i saj{ ListatAtributeve: [] }
    Editim3(layer, TipiPikeVijePoligon, nameLayer, id_layer, id_layer_type, { ListatAtributeve: [] }, nrStatusi);
}

function MerrIdUnikePerIdDytesore() {
    var rezultati;
    $.ajax({
        async: false,
        url: Utils.getServerApiUrl("GIS", "merrIdDytesoreUnike"),
    }).done(function (result) {
        rezultati = result.d;
    }).fail(function () {
        noty({ text: perkthe("GP_EDITIM_MSG_MSG_IdDytErr"), type: "error" }); //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_IdDytErr"));
    });
    return rezultati;
}

function MerrIdUnikePerIdDytesoreAsync(layer, event, responseComboListAtr, feature, veprimi, emerKoloneIdDytesore) {
    $.ajax({
        url: Utils.getServerApiUrl("GIS", "merrIdDytesoreUnike"),
    }).done(function (result) {
        idunike = result.d;
        if (idunike) {
            if (veprimi == 'UPDATE')
                feature.attributes[emerKoloneIdDytesore] = idunike;
            krijoPopupEditimInsert(event, responseComboListAtr, idunike, veprimi);
            if (veprimi == 'INSERT')
                enableButonaEditimSipasRastit(true, true, true, true, false, false, true, true, false, false, true, false);
        }
    }).fail(function () {
        noty({ text: perkthe("GP_EDITIM_MSG_MSG_IdDytErr"), type: "error" }); //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_IdDytErr"));
    });
}

var eventInserUpdateAtribute = null;

function AfishoPopupInsertUpdate(event, layer, response2, shtimApoModifikim) {
    var objF = event.feature;
    if (shtimApoModifikim == 'UPDATE' || shtimApoModifikim == 'DELETE') {
        var keys = Object.keys(objF.attributes);

        for (var i = 0; i < keys.length; i++) {
            if (typeof varSettings.editim.objLidhjeGisWeb[keys[i]] !== "undefined")
                varSettings.editim.objLidhjeGisWeb[keys[i]] = objF.attributes[keys[i]];
            if (typeof varSettings.editim.objLidhjeGisWebAfishim[keys[i]] !== "undefined")
                varSettings.editim.objLidhjeGisWebAfishim[keys[i]] = objF.attributes[keys[i]];
        }
        varSettings.editim.objLidhjeGisWeb.veprimi = shtimApoModifikim;
        varSettings.editim.objLidhjeGisWeb.the_geom = ktheGjeometriNeString(objF);
        $.extend(varSettings.editim.objLidhjeGisWebAfishim, varSettings.editim.objLidhjeGisWeb);
    }
    if (KolonatAfishimEditimGJ.map(function (a) { return a.EmerKolona; }).indexOf(emerKoloneIdDytesore) > -1 && (shtimApoModifikim == 'INSERT' || (shtimApoModifikim == 'UPDATE' && !objF.attributes[emerKoloneIdDytesore]))) {
        MerrIdUnikePerIdDytesoreAsync(layer, event, response2, objF, 'UPDATE', emerKoloneIdDytesore);
        return;
    }
    krijoPopupEditimInsert(event, response2, '', shtimApoModifikim);
    if (shtimApoModifikim == 'INSERT') {
        if (varSettings.editim.objLidhjeGisWeb.veprimi == 'INSERT' && varSettings.editim.objLidhjeGisWeb.NRSTATUSI !== 3 && varSettings.editim.objLidhjeGisWeb.KODI == '')
            enableButonaEditimSipasRastit(true, true, true, true, false, false, false, true, false, false, true, false);
        else
            enableButonaEditimSipasRastit(true, true, true, true, false, false, true, true, false, false, true, true);
    }
}

function getFields(input, field) {
    var output = [];
    for (var i = 0; i < input.length ; ++i)
        output.push(input[i][field]);
    return output;
}

function krijoPopupEditimInsert(event, responseComboListAtr, vleraUnikeIdDytesore, shtimApoModifikim) {
    varSettings.editim.objPanel.bbar.doLayout();
    varSettings.editim.objPanel.bbar.show();
    varSettings.editim.objPanel.frmeditim.removeAll();
    varSettings.editim.objPanel.frmeditim.show();
    varSettings.editim.objPanel.frmeditim.doLayout();

    if (event != null) {
        tabeleAt = responseComboListAtr.ListatAtributeve;
        eventInserUpdateAtribute = event;
        var objF = event.feature;
        if (shtimApoModifikim == 'INSERT')
            varSettings.editim.objLidhjeGisWeb.gid = 0;

        varSettings.editim.objOpenLayer.id = objF.id;

        for (i = 0; i < KolonatAfishimEditimGJ.length; i++) {
            var kolonaPerAfishim = KolonatAfishimEditimGJ[i];
            if (kolonaPerAfishim.EmerKolona == "the_geom")
                continue;

            var lejobosh = true;
            var etiketa = kolonaPerAfishim.EmertimPerkthyer;
            var labelStyleCss = 'width:195px';
            if (kolonaPerAfishim.Detyrueshme && kolonaPerAfishim.EmerKolona != "gid") {
                lejobosh = false;
                etiketa += '  *';
                labelStyleCss += '; color:red'
            }

            var KoloneList = false;
            var indexTabele;

            //for (var k = 0; k < tabeleAt.length; k++) {
            //    if (kolonaPerAfishim.EmerKolona == tabeleAt[k].Emer_kolone) {
            //        KoloneList = true;
            //        indexTabele = k;
            //    }
            //}
            if (KoloneList) {
                var data1 = new Array();
                data1.push(['']);
                for (var m = 0; m < tabeleAt[indexTabele].Elementet.length; m++) {
                    data1.push([tabeleAt[indexTabele].Elementet[m]]);
                }

                var combo_box = new Ext.form.ComboBox({
                    typeAhead: true,
                    triggerAction: 'all',
                    emptyText: '',
                    width: 150,
                    fieldLabel: etiketa,
                    resizable: true,
                    labelStyle: labelStyleCss,
                    editable: false,
                    allowBlank: lejobosh,
                    forceSelection: true,
                    blankText: perkthe("GP_EDITIM_COMBOFIELDblankText"),
                    tpl: '<tpl for="."><div class="x-combo-list-item">{vlera:defaultValue("&nbsp;")}</div></tpl>',
                    validateOnBlur: true,
                    mode: 'local',
                    id: "" + kolonaPerAfishim.EmerKolona + objF.id + "",
                    name: "" + i + "",
                    store: new Ext.data.ArrayStore({
                        fields: [
                            'vlera'
                        ],
                        data: data1
                    }),
                    valueField: 'vlera',
                    displayField: 'vlera'
                });

                varSettings.editim.objPanel.frmeditim.add(combo_box);
                if (shtimApoModifikim == 'UPDATE') {
                    if (objF.attributes[kolonaPerAfishim.EmerKolona] == undefined) {
                        combo_box.setValue('');
                    }
                    else {
                        combo_box.value = objF.attributes[kolonaPerAfishim.EmerKolona];
                    }
                }
            }
            else {
                var textFieldEditim = [];
                switch (kolonaPerAfishim.IdTipiKontrollit) {
                    case 0:
                    case 1:
                        textFieldEditim = krijoExtformNumberFieldPaDecimal(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, kolonaPerAfishim.EmerKolona, 150);
                        break;
                    case 2:
                        textFieldEditim = krijoExtformNumberFieldMeDecimal(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, kolonaPerAfishim.EmerKolona, 150, 4, 1);
                        break;
                    case 3:
                        textFieldEditim = krijoExtformDateField(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, kolonaPerAfishim.EmerKolona);
                        break;
                    case 4:
                        //Ka qene per dokumentat. Tani nuk ruhen me ne kete menyre, perdoret arkiva ne web
                        break;
                    case 5:
                        textFieldEditim = krijoExtformTextField(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, kolonaPerAfishim.EmerKolona);
                        break;
                    case 6:
                        textFieldEditim = krijoExtformTriggerField(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, kolonaPerAfishim.EmerKolona);
                        break;
                    case 7:
                        textFieldEditim = krijoExtformComboBox(kolonaPerAfishim.EmerKolona + objF.id, i, lejobosh, etiketa, labelStyleCss, kolonaPerAfishim.EmerKolona);
                        break;
                };
                if (shtimApoModifikim == 'UPDATE') {
                    textFieldEditim.setValue(objF.attributes[kolonaPerAfishim.EmerKolona]);

                    if (objF.attributes[kolonaPerAfishim.EmerKolona] == undefined) {
                        textFieldEditim.setValue('');
                    }
                    else {
                        if (kolonaPerAfishim.IdTipiKontrollit == 3) {
                            var date
                            if (typeof (objF.attributes[kolonaPerAfishim.EmerKolona]) != "string") {
                                date = new Date(objF.attributes[kolonaPerAfishim.EmerKolona]["date"]);

                                var y = date.getFullYear();
                                var m = date.getMonth() + 1;
                                var d = date.getDate();
                                var date = y + '-' + (m < 10 ? 0 : '') + m + '-' + (d < 10 ? 0 : '') + d;
                            }
                            else {
                                date = objF.attributes[kolonaPerAfishim.EmerKolona];
                            }
                            objF.attributes[kolonaPerAfishim.EmerKolona] = date;

                            if (objF.attributes[kolonaPerAfishim.EmerKolona].search("Z") > -1) {
                                objF.attributes[kolonaPerAfishim.EmerKolona] = objF.attributes[kolonaPerAfishim.EmerKolona]["date"].replace('Z', '');
                            }
                        }
                    }
                }

                if (typeof varSettings.editim.objLidhjeGisWebAfishim[kolonaPerAfishim.EmerKolona] != "undefined")
                    textFieldEditim.setValue(varSettings.editim.objLidhjeGisWebAfishim[kolonaPerAfishim.EmerKolona]);

                //DELETE do te kete visibilitetin e njejte me update por asnje nga fushat nuk mund te ndryshohet ne fshirje thjesht te shihen
                if ((!kolonaPerAfishim.ObjVisible && shtimApoModifikim == 'UPDATE') || (!kolonaPerAfishim.ObjVisible && shtimApoModifikim == 'DELETE') || (!kolonaPerAfishim.ObjVisibleInsert && shtimApoModifikim == 'INSERT'))
                    textFieldEditim.hide();
                if (shtimApoModifikim == 'UPDATE')
                    textFieldEditim.setDisabled(!kolonaPerAfishim.Enabled);
                else if (shtimApoModifikim == 'INSERT')
                    textFieldEditim.setDisabled(!kolonaPerAfishim.EnabledInsert);
                else
                    textFieldEditim.setDisabled(true);


                var pikaGeomTranf = null;

                switch (kolonaPerAfishim.EmerKolona) {
                    case 'siperfaqja':
                        var geoTrans = eventInserUpdateAtribute.feature.geometry.clone();
                        geoTrans.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
                        textFieldEditim.setValue(geoTrans.getArea());
                        break;
                    case 'gjatesia':
                        var geoTrans = eventInserUpdateAtribute.feature.geometry.clone();
                        geoTrans.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
                        textFieldEditim.setValue(geoTrans.getLength());
                        break;
                    case 'pozicioni_x':
                        pikaGeomTranf = transformoKordinatatPike(objF, projeksioniGeo, projeksioniDisplay)
                        textFieldEditim.setValue(pikaGeomTranf.geometry.x);
                        break;
                    case 'pozicioni_y':
                        if (pikaGeomTranf)
                            textFieldEditim.setValue(pikaGeomTranf.geometry.y);
                        else {
                            pikaGeomTranf = transformoKordinatatPike(objF, projeksioniGeo, projeksioniDisplay)
                            textFieldEditim.setValue(pikaGeomTranf.geometry.y);
                        }
                        break;
                    case "gidPrindi":
                        break;
                    case modifikuarNeFund:
                        textFieldEditim.setValue(userLoguar);
                        break;
                    case emerKoloneIdLidheseGis:
                    case emerKoloneIdLidheseGisZ:
                        textFieldEditim.enableKeyEvents = true;
                        textFieldEditim.on('keyup', function (e, t, o) { keyUpTextFieldIdLidheseGis(e, t, o, this, objF.id, kolonaPerAfishim.EmerKolona); });
                        break;
                    default:
                        break;
                }

                varSettings.editim.objPanel.frmeditim.add(textFieldEditim);
            }

            bejDisableFushatMeTeDhenaNgaJashte(objF.id, shtimApoModifikim);
            Ext.getCmp('WindowEditimi').doLayout();
        }
        if (shtimApoModifikim == 'INSERT') {
            varSettings.editim.objPanel.mbyllPopupEditimInserti = false;
            drawPolygon.deactivate();
            drawLine.deactivate();
            drawPoint.deactivate();
            varSettings.editim.objPanel.mbyllPopupEditimInserti = true;
        }
    }
}

var objektiSelectModifiko;

function addselectModifiko(feature) {
    objektiSelectModifiko = feature[0];
}

function addSelected(feature) {
    featureSelected.push(feature);
    shfaqDivElementSipasRastit("njoftimeRezultate", true, "300px", "auto", "");
    document.getElementById("njoftimeRezultate").innerHTML = perkthe("GP_EDITIM_MSG_TEXT_ObjSelekt") + " " + featureSelected.length;
    if (!Ext.getCmp("EditButtonId").pressed) {
        KlikoDjathte(varSettings.editim.controlClickFromWmsLayer);
    }
}

function clearSelected(feature) {
    if (document.getElementById("rightClickMenuDiv")) {
        document.getElementById("rightClickMenuDiv").innerHTML = "";
    }
    featureSelected = new Array();
    shfaqDivElementSipasRastit("njoftimeRezultate", false, "", "", "");
}

function shfaqPanelKerkim2() {
    Ext.getCmp('mainview').findById(harteUI.settings.paneliKerkimit.idPanelKerkimi).show();
    Ext.getCmp('mainview').doLayout();
}

function fshihPanelKerkim2() {
    Ext.getCmp('mainview').findById(harteUI.settings.paneliKerkimit.idPanelKerkimi).hide();
    Ext.getCmp('mainview').doLayout();
}

function pastroElementeteKerkimit() {
    if (Ext.getCmp("gridaPanelKerkimId")) {
        Ext.getCmp("grideKerkimPanel").remove("gridaPanelKerkimId", true);
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
    }

    if (mapPanel.map.layers.indexOf(teGjitheWfsKerkim) != -1)//wfs per select
        mapPanel.map.removeLayer(teGjitheWfsKerkim);

    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);

    if (LayeriNeKerkim) {
        LayeriNeKerkim.mbyllLayer();
    }
}

function ZhdukFormKerkim() {
    if (Ext.getCmp("gridaPanelKerkimId")) {
        Ext.getCmp("grideKerkimPanel").remove("gridaPanelKerkimId", true);
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
    }
    if (Ext.getCmp("kolonatKerkimIdCmb")) {
        Ext.getCmp("kolonatKerkimIdCmb").hide();
    }

    if (Ext.getCmp("kolonatOperatoretIdCmb")) {
        Ext.getCmp("kolonatOperatoretIdCmb").hide();
    }

    if (Ext.getCmp("searchText")) {
        Ext.getCmp("searchText").hide();
    }

    if (Ext.getCmp("searchBtnTabid")) {
        Ext.getCmp("searchBtnTabid").hide();
    }

    if (Ext.getCmp("anulloKerkimBtnTabid")) {
        Ext.getCmp("anulloKerkimBtnTabid").hide();
    }

    Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();

    if (mapPanel.map.layers.indexOf(teGjitheWfsKerkim) != -1)//wfs per select
        mapPanel.map.removeLayer(teGjitheWfsKerkim);

    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);

    if (LayeriNeKerkim) {
        LayeriNeKerkim.mbyllLayer();
    }
}

var LayeriNeKerkim;
var teGjitheWfsKerkim;
var AtributeSearchArray = [];
var AtributeSearchArrayAfish = [];
var TipiAtributeSearchArray = [];

function ZhdukFormKerkimHapesinor() {
    if (mapPanel.map.layers.indexOf(teGjitheWfsKerkim) != -1)
        mapPanel.map.removeLayer(teGjitheWfsKerkim);

    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);

    if (Ext.getCmp("GridIdKerkimHap")) {
        Ext.getCmp("grideKerkimPanelHapesinor").remove("GridIdKerkimHap", true);
        Ext.getCmp("grideKerkimPanelHapesinor").doLayout();
    }
    
    if (Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId")) {
        Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId").toggle(false);
        Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId").hide();
    }
    if (Ext.getCmp("vizatoVijeBtnKerkimHapesinorId")) {
        Ext.getCmp("vizatoVijeBtnKerkimHapesinorId").toggle(false);
        Ext.getCmp("vizatoVijeBtnKerkimHapesinorId").hide();
    }

    if (Ext.getCmp("vizatoPikeBtnKerkimHapesinorId")) {
        Ext.getCmp("vizatoPikeBtnKerkimHapesinorId").toggle(false);
        Ext.getCmp("vizatoPikeBtnKerkimHapesinorId").hide();
    }

    if (Ext.getCmp("fshiWfsBtnKerkimHapesinorId")) {
        Ext.getCmp("fshiWfsBtnKerkimHapesinorId").hide();
    }

    if (Ext.getCmp("exportToExcelHapBtnId")) {
        Ext.getCmp("kerkimhtmlPanelHapesinor").remove('exportToExcelHapBtnId', true);
        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
    }

    caktivizoButonatAktivizoButonin('', actions)
    caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet)
    if (LayeriNeKerkimHapesinor) {
        LayeriNeKerkimHapesinor.mbyllLayer();
    }
}

var vektoriButonatKerkimHapesinor = new Array();
var LayeriNeKerkimHapesinor;//variabel global ne menyre qe ta mbyllim layerin kur mbarojme kerkimin

function KtheKolonatKerkimHapesinor(layerKarakteristika) {
    var layerKerkimKarakterArr = new Array();
    layerKerkimKarakterArr = JSON.parse(layerKarakteristika);
    var layer = layerKerkimKarakterArr['layer'];
    var workspace_name = layerKerkimKarakterArr['workspace_name'];
    var urlservicewfs = layerKerkimKarakterArr['url_servicewfs'];
    var layer_name_app = layerKerkimKarakterArr['layer_name_app'];
    AtributeSearchArrayKerkimHapesinor = [];
    TipiAtributeSearchArrayKerkimHapesinor = [];
    AtributeSearchArrKerHapAfish = [];
    ZhdukFormKerkimHapesinor();
    if (Ext.getCmp(harteUI.settings.paneliKerkimit.idComboLayersFilter)) {
        Ext.getCmp(harteUI.settings.paneliKerkimit.idComboLayersFilter).setValue('{"layer":"bosh","url_servicewfs":"","workspace_name":"","layer_title":"","url_servicewms":""}');
    }
    ZhdukFormKerkim();
    if (layer && layer != "bosh") {
        LayeriNeKerkimHapesinor = new LayerApp(layer_name_app);//LayeriNeKerkimHapesinor eshte variabel globale
        LayeriNeKerkimHapesinor.shfaqLayer();

        if (!map.getControl("drawKerkimHapesinorPoligonContrId")) {
            VizatoPoligonKerkimHapesinor(layer, workspace_name, urlservicewfs);
            vektoriKontrollet.push(drawKerkimHapesinorPoligon);
        }

        if (!map.getControl("drawKerkimHapesinorVijeContrId")) {
            VizatoVijeKerkimHapesinor(layer, workspace_name, urlservicewfs);
            vektoriKontrollet.push(drawKerkimHapesinorVije);
        }
        if (!map.getControl("polygonControlRrethContrId")) {
            VizatoPikeKerkimHapesinor(layer, workspace_name, urlservicewfs);
            vektoriKontrollet.push(polygonControlRreth);
        }

        if (!Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId")) {
            vizatoPoligonBtnKerkimHapesinor = new Ext.Button({
                enableToggle: true,
                toggleGroup: "panButton",
                id: "vizatoPoligonBtnKerkimHapesinorId",
                tooltipType: "title",
                tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerkoPol"),
                icon: varSettings.theme.urlCustom + 'searchSpatial.png',
                cls: "butoniKerkimHapesinor" + varSettings.theme.color,
                handler: function (toggled) {
                    if (toggled) {
                        caktivizoButonatAktivizoButonin('', actions);
                        caktivizoKontrolletAktivizoKontrollin(drawKerkimHapesinorPoligon, vektoriKontrollet)
                    }
                }
            });
            Ext.getCmp("kerkimhtmlPanelHapesinor").add(vizatoPoligonBtnKerkimHapesinor);
        }
        else {
            Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId").show();
        }

        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
        vektoriButonatKerkimHapesinor.push(vizatoPoligonBtnKerkimHapesinor);
        if (!Ext.getCmp("vizatoVijeBtnKerkimHapesinorId")) {
            vizatoVijeBtnKerkimHapesinor = new Ext.Button({
                enableToggle: true,
                toggleGroup: "panButton",
                id: "vizatoVijeBtnKerkimHapesinorId",
                icon: varSettings.theme.urlCustom + 'searchSpatial.png',
                tooltipType: "title",
                tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerVije"),
                cls: "butoniKerkimHapesinor" + varSettings.theme.color,
                handler: function (toggled) {
                    if (toggled) {
                        caktivizoButonatAktivizoButonin('', actions);
                        caktivizoKontrolletAktivizoKontrollin(drawKerkimHapesinorVije, vektoriKontrollet)
                    }
                }
            });
            Ext.getCmp("kerkimhtmlPanelHapesinor").add(vizatoVijeBtnKerkimHapesinor);
        }
        else {
            Ext.getCmp("vizatoVijeBtnKerkimHapesinorId").show();
        }
        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
        vektoriButonatKerkimHapesinor.push(vizatoVijeBtnKerkimHapesinor);

        if (!Ext.getCmp("vizatoPikeBtnKerkimHapesinorId")) {
            vizatoPikeBtnKerkimHapesinor = new Ext.Button({
                enableToggle: true,
                id: "vizatoPikeBtnKerkimHapesinorId",
                toggleGroup: "panButton",
                tooltipType: "title",
                tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerRR"),
                icon: varSettings.theme.urlCustom + 'searchSpatial.png',
                cls: "butoniKerkimHapesinor" + varSettings.theme.color,
                handler: function (toggled) {
                    if (toggled) {
                        caktivizoButonatAktivizoButonin('', actions);
                        caktivizoKontrolletAktivizoKontrollin(polygonControlRreth, vektoriKontrollet)
                    }
                    else {
                    }
                }
            });
            Ext.getCmp("kerkimhtmlPanelHapesinor").add(vizatoPikeBtnKerkimHapesinor);
        }
        else {
            Ext.getCmp("vizatoPikeBtnKerkimHapesinorId").show();
        }
        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
        vektoriButonatKerkimHapesinor.push(vizatoPikeBtnKerkimHapesinor);
        if (!Ext.getCmp("fshiWfsBtnKerkimHapesinorId")) {
            var fshiWfsBtnKerkimHapesinor = new Ext.Button({
                id: "fshiWfsBtnKerkimHapesinorId",
                icon: 'img/cross.png',
                tooltipType: "title",
                tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerFshij"),
                cls: "butoniKerkimHapesinor" + varSettings.theme.color,
                handler: function (toggled) {
                    if (toggled) {
                        caktivizoButonatAktivizoButonin('', actions);
                        caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet)
                        rregulloToggleKerkimHapesinor(vektoriButonatKerkimHapesinor);
                        if (InfoBtn.pressed) {
                            caktivizoKontrolletAktivizoKontrollin(featureInfo, vektoriKontrollet);                         
                        }
                        if (matButton.pressed) {
                            matButton.toggle();
                        }

                        if (mapPanel.map.layers.indexOf(teGjitheWfsKerkim) != -1)//wfs per te gjithe wfs qe ndertohet me rez e kerkimit
                            mapPanel.map.removeLayer(teGjitheWfsKerkim);

                        if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)//wfs qe krijohet kur vizatohet poligon ose vije
                            mapPanel.map.removeLayer(wfsKerkimSelect);

                        //   if (sentGridKerkimHapesinor) {
                        if (typeof (sentGridKerkimHapesinor) != "undefined") {
                            sentGridKerkimHapesinor.getStore().removeAll();
                        }
                    }
                    else {
                    }
                }
            });
            Ext.getCmp("kerkimhtmlPanelHapesinor").add(fshiWfsBtnKerkimHapesinor);
        }
        else {
            Ext.getCmp("fshiWfsBtnKerkimHapesinorId").show();
        }
        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
    }
}

function ZgjidhOperatoret(layer, kolona, workspace_name, urlservicewfs) {
    if (Ext.getCmp("searchText")) {
        Ext.getCmp("searchText").setValue();
        Ext.getCmp("searchText").setDisabled(true);
    }
    if (Ext.getCmp("kolonatOperatoretIdCmb")) {
        Ext.getCmp('kolonatOperatoretIdCmb').clearValue();
        Ext.getCmp('kolonatOperatoretIdCmb').setDisabled(false);
    }
    Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
}

function krijoFormenRezultat(layer, kolona, operatori, workspace_name, urlservicewfs) {
    if (Ext.getCmp("searchText")) {
        Ext.getCmp("searchText").setDisabled(false);
        Ext.getCmp("searchText").setValue('');
    }
}

function anullo() {
    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);
    hiqNgaHartaVektorKerkim();
    Ext.getCmp("grideKerkimPanel").removeAll();
}

var rrezjatooltip;
function displayRadius(evt, point) {
    if (rrezjatooltip) {
        rrezjatooltip.destroy();
        rrezjatooltip = null;
    }

    var mapBox = Ext.fly(map.div).getBox();
    var centerLonLat = point.getBounds().getCenterLonLat();
    var centerPx = map.getViewPortPxFromLonLat(centerLonLat);
    rrezjatooltip = new Ext.ToolTip({
        html: perkthe("GP_KERKIMHAP_MSG_MSG_RrezKerk") + " " + evt.getLength().toFixed(0)
    });
    rrezjatooltip.showAt([centerPx.x + mapBox.x + 10, centerPx.y + mapBox.y - 15]);
}

var polygonLayerRreth;
var polygonControlRreth;

var stilPerDrawKerkimHapesinor = {
    'strokeColor': '#0ca961',
    'strokeOpacity': 1,
    'strokeWidth': 2,
    'fillColor': '#d3e9cb',
    'fillOpacity': 0.5,
    'pointRadius': 6
}

function VizatoPikeKerkimHapesinor(layer, workspace_name, urlservicewfs) {
    var polygonLayerRreth = new OpenLayers.Layer.Vector("PoligonRrethKerkim", { VektPerkohshemLayer: true });
    polyOptions = {
        sides: 40,
        'style': stilPerDrawKerkimHapesinor
    };
    polygonControlRreth = new OpenLayers.Control.DrawFeature(polygonLayerRreth,
        OpenLayers.Handler.RegularPolygonDR,
        {
            id: "polygonControlRrethContrId",
            handlerOptions: polyOptions,
            map: map
        }
    );
    map.addControl(polygonControlRreth);
    map.addLayer(polygonLayerRreth);

    polygonLayerRreth.events.on({
        beforefeatureadded: function (event) {
            map.getLayersByName("drawingsPoligon")[0].removeAllFeatures();
            map.getLayersByName("drawingsVije")[0].removeAllFeatures();
            map.getLayersByName("PoligonRrethKerkim")[0].removeAllFeatures();
        }
    });
    vektoriKontrollet.push(polygonControlRreth);
}

function VizatoVijeKerkimHapesinor(layer, workspace_name, urlservicewfs) {
    var drawingsVije = new OpenLayers.Layer.Vector("drawingsVije", { VektPerkohshemLayer: true });
    drawKerkimHapesinorVije = new OpenLayers.Control.DrawFeature(drawingsVije,
            OpenLayers.Handler.Path,
            {
                id: "drawKerkimHapesinorVijeContrId",
                handlerOptions: { 'style': stilPerDrawKerkimHapesinor }
            }
    );
    map.addLayer(drawingsVije);
    map.addControl(drawKerkimHapesinorVije);

    drawingsVije.events.on({
        beforefeatureadded: function (event) {
            map.getLayersByName("drawingsPoligon")[0].removeAllFeatures();
            map.getLayersByName("drawingsVije")[0].removeAllFeatures();
            map.getLayersByName("PoligonRrethKerkim")[0].removeAllFeatures();
        }
    });
    vektoriKontrollet.push(drawKerkimHapesinorVije);
}

function VizatoPoligonKerkimHapesinor(layer, workspace_name, urlservicewfs) {
    var drawingsPoligon = new OpenLayers.Layer.Vector("drawingsPoligon", { VektPerkohshemLayer: true });
    drawKerkimHapesinorPoligon = new OpenLayers.Control.DrawFeature(drawingsPoligon, OpenLayers.Handler.Polygon, {
        id: "drawKerkimHapesinorPoligonContrId",
        handlerOptions: { 'style': stilPerDrawKerkimHapesinor },
    });
    map.addLayer(drawingsPoligon);
    map.addControl(drawKerkimHapesinorPoligon);

    drawingsPoligon.events.on({
        beforefeatureadded: function (event) {
            map.getLayersByName("drawingsPoligon")[0].removeAllFeatures();
            map.getLayersByName("drawingsVije")[0].removeAllFeatures();
            map.getLayersByName("PoligonRrethKerkim")[0].removeAllFeatures();
        }
    });
    vektoriKontrollet.push(drawKerkimHapesinorPoligon);
}

var wfsKerkimSelect;
function krijoGrideKerkimHapesinor(layer, filterPoligonHapesinor, workspace_name, urlservicewfs, event) {
    if (mapPanel.map.layers.indexOf(teGjitheWfsKerkim) != -1)//wfs per select
        mapPanel.map.removeLayer(teGjitheWfsKerkim);

    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)//wfs qe krijohet kur vizatohet poligon ose vije
        mapPanel.map.removeLayer(wfsKerkimSelect);

    if (Ext.getCmp("GridIdKerkimHap")) {
        Ext.getCmp("grideKerkimPanelHapesinor").remove("GridIdKerkimHap", true);
        Ext.getCmp("grideKerkimPanelHapesinor").doLayout();
    }

    if (layer != "bosh") {
        var fushatKerkimHapesinor = new Array();
        var kolonaKerkimHapesinor = new Array();

        for (var index = 0; index < AtributeSearchArrayKerkimHapesinor.length; index++) {
            var n = AtributeSearchArrayKerkimHapesinor[index];
            var t = TipiAtributeSearchArrayKerkimHapesinor[index];
            if (n != "the_geom" && n != emerKoloneIdDytesore && n != emerKoloneDokumentinKonf) {
                fushatKerkimHapesinor.push({ name: n, type: t });
                kolonaKerkimHapesinor.push({
                    header: AtributeSearchArrKerHapAfish[n],
                    width: 150,
                    sortable: true,
                    dataIndex: n
                });
            }
        }
        var geoPoligonKerkim = event.feature.geometry.clone();
        var geoPoligonKerkim = geoPoligonKerkim.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniDisplay));

        teGjitheWfsKerkim = new OpenLayers.Layer.Vector("teGjitheWfsKerkimIdHap", {
            VektPerkohshemLayer: true,
            styleMap: new OpenLayers.StyleMap({
                fillColor: "#ffffff",
                fillOpacity: 0.0,
                strokeColor: "#EEB4B4",
                strokeWidth: 3,
                pointRadius: 6
            }),
            projection: new OpenLayers.Projection(projeksioniGeoV),
            strategies: [new OpenLayers.Strategy.Fixed()],
            protocol: new OpenLayers.Protocol.HTTP({
                url: urlApp + "phpUI/MerrTeDhenatGeoKerkim.php",
                format: new OpenLayers.Format.GeoJSON(),
                params: {
                    geo: geoPoligonKerkim,
                    layer: layer
                },
                readWithPOST: true
            })
        });
        map.addLayer(teGjitheWfsKerkim);

        store1 = new GeoExt.data.FeatureStore({
            fields: fushatKerkimHapesinor,
            layer: teGjitheWfsKerkim,
            selType: 'featuremodel',
            autoLoad: false
        });

        sentGridKerkimHapesinor = new Ext.grid.GridPanel({
            store: store1,
            tbar: [],
            id: "GridIdKerkimHap",
            columns: kolonaKerkimHapesinor,
            layout: "fit",
            autoScroll: true,
            loadMask: {
                msg: perkthe("GP_KERKIM_MSG_MSG_LoadGrid")
            }
        });
        Ext.getCmp("grideKerkimPanelHapesinor").add(sentGridKerkimHapesinor);
        Ext.getCmp("grideKerkimPanelHapesinor").doLayout();

        sentGridKerkimHapesinor.getSelectionModel().on('rowselect', function (grid, rowIndex, e) {
            var rec = sentGridKerkimHapesinor.getStore().getAt(rowIndex);
            var VleraGid = rec.get('gid');
            var fushe = 'gid';
            ZoomKerkimHapesinor(layer, VleraGid, fushe, workspace_name, urlservicewfs);
        });
        if (Ext.getCmp("exportToExcelHapBtnId")) {
            Ext.getCmp("kerkimhtmlPanelHapesinor").remove('exportToExcelHapBtnId', true);
            Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
        }

        var exportButton = new Ext.ux.Exporter.Button({
            component: sentGridKerkimHapesinor,
            id: "exportToExcelHapBtnId",
            disabled: false,
            text: perkthe("GP_KERKIM_BTN_TEXT_ExEx"),
            cls: 'x-btn-text-icon',
            cls: 'ExcelButtonKerkim',
            title: perkthe("GP_KERKIM_BTN_Title_ExEx"),
            scale: 'large',
            tooltipType: "title",
            toogle: true,
            type: 'button',
            tooltip: perkthe("GP_KERKIM_BTN_TOOLTIP_ExEx"),
            icon: 'img/bluebg.png',
            colspan: 4
        });
        Ext.getCmp("kerkimhtmlPanelHapesinor").add(exportButton);
        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
    }
}

function ZoomKerkimHapesinor(layer, VleraGid, fushe, workspace_name, urlservicewfs) {
    var objektiSelect = map.getLayersByName("teGjitheWfsKerkimIdHap")[0].getFeaturesByAttribute('gid', VleraGid);
    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);

    wfsKerkimSelect = new OpenLayers.Layer.Vector(VleraGid, {
        VektPerkohshemLayer: true,
        styleMap: new OpenLayers.StyleMap({
            fillColor: "#ffffff",
            fillOpacity: 0.0,
            strokeColor: "#48D1CC",
            strokeWidth: 3,
            pointRadius: 6
        }),
        eventListeners: {
            "featuresadded": function (features) {
                var objectFeatures = features;
                if (objectFeatures.length != 0) {
                    var bounds = wfsKerkimSelect.getDataExtent();
                    map.zoomToExtent(bounds);
                }
            }
        }
    });
    wfsKerkimSelect.addFeatures(objektiSelect);
    map.addLayer(wfsKerkimSelect);

    if (teGjitheWfsKerkim.getZIndex() > wfsKerkimSelect.getZIndex()) {
        var tmp = teGjitheWfsKerkim.getZIndex();
        teGjitheWfsKerkim.setZIndex(wfsKerkimSelect.getZIndex());
        wfsKerkimSelect.setZIndex(tmp)
    }
}

function hiqNgaHartaVektorKerkim() {
    LayerStoreKerkim.each(function (record) {
        if (map.getLayersByName(record.data.Title)[0]) {
            var layerProp = JSON.parse(record.data.ObjPerKerkim);
            if (map.getLayersByName('VectorPerCdoLayerKerkimId' + layerProp['layer']).length >= 1)//wfs per select
            {
                map.removeLayer(map.getLayersByName('VectorPerCdoLayerKerkimId' + layerProp['layer'])[0]);
            }
        }
    })
}

function Zoom(layer, VleraGid, fushe, workspace_name, urlservicewfs) {
    if (mapPanel.map.layers.indexOf(wfsKerkimSelect) != -1)
        mapPanel.map.removeLayer(wfsKerkimSelect);

    var objektiSelect = map.getLayersByName('VectorPerCdoLayerKerkimId' + layer)[0].getFeaturesByAttribute('gid', VleraGid);

    wfsKerkimSelect = new OpenLayers.Layer.Vector(VleraGid, {
        VektPerkohshemLayer: true,
        styleMap: new OpenLayers.StyleMap({
            fillColor: "#ffffff",
            fillOpacity: 0.0,
            strokeColor: "#48D1CC",
            strokeWidth: 3,
            pointRadius: 6
        }),
        eventListeners: {
            "featuresadded": function (features) {
                var objectFeatures = features;
                if (objectFeatures.length != 0) {
                    var bounds = wfsKerkimSelect.getDataExtent();
                    map.zoomToExtent(bounds);
                }
            }
        }
    });

    wfsKerkimSelect.addFeatures(objektiSelect);
    map.addLayer(wfsKerkimSelect);

    if (map.getLayersByName('VectorPerCdoLayerKerkimId' + layer)[0].getZIndex() > wfsKerkimSelect.getZIndex()) {
        var tmp = map.getLayersByName('VectorPerCdoLayerKerkimId' + layer)[0].getZIndex();
        map.getLayersByName('VectorPerCdoLayerKerkimId' + layer)[0].setZIndex(wfsKerkimSelect.getZIndex());
        wfsKerkimSelect.setZIndex(tmp)
    }
}

function dil() {
    window.location.href = 'logOut.php';
}

function filloProcessWPS(wpsUrl, capabilitiesVersion, WPSIdentifier, vektoriMeObjektet, objektiMeAtribute, kontrolli) {
    OpenLayers.Request.GET({
        url: wpsUrl,
        params: {
            "SERVICE": "WPS",
            "REQUEST": "DescribeProcess",
            "VERSION": capabilitiesVersion,
            "IDENTIFIER": WPSIdentifier
        },
        success: function (response) {
            var process = new OpenLayers.Format.WPSDescribeProcess().read(
                    response.responseText
                    ).processDescriptions[WPSIdentifier];
            executeWPSProcess(wpsUrl, capabilitiesVersion, WPSIdentifier, vektoriMeObjektet, process, objektiMeAtribute, kontrolli)
        }
    });
}

function executeWPSProcess(wpsUrl, capabilitiesVersion, WPSIdentifier, vektoriMeObjektet, processWPS, objektiMeAtribute, kontrolli) {
    var output = processWPS.processOutputs[0];
    processWPS.dataInputs.clear();
    for (var i = 0; i < vektoriMeObjektet.length; i++) {
        var inputData = new Object();
        inputData.title = "geom"
        inputData.identifier = "geom"
        inputData.data = {
            complexData: {
                mimeType: "application/wkt",
                value: new OpenLayers.Format.WKT().write(vektoriMeObjektet[i])
            }
        };
        var newInput = OpenLayers.Util.extend({}, inputData);//ben kopjim
        processWPS.dataInputs.push(newInput);
    }
    var input;
    for (var i = processWPS.dataInputs.length - 1; i >= 0; --i) {
        input = processWPS.dataInputs[i];
        if ((input.minOccurs === 0 || input.occurrence) && !input.data && !input.reference) {
            OpenLayers.Util.removeItem(processWPS.dataInputs, input);
        }
    }
    processWPS.responseForm = {
        rawDataOutput: {
            identifier: output.identifier
        }
    };

    if (output.complexOutput && output.complexOutput.supported.formats["application/wkt"]) {
        processWPS.responseForm.rawDataOutput.mimeType = "application/wkt";
    }
    OpenLayers.Request.POST({
        url: wpsUrl,
        data: new OpenLayers.Format.WPSExecute().write(processWPS),
        success: function (response) {
            PerfundoWPSProcess(response, vektoriMeObjektet, objektiMeAtribute, kontrolli);
        }
    });
}

function PerfundoWPSProcess(response, vektoriMeObjektet, objektiMeAtribute, kontrolli) {
    var features;
    var contentType = response.getResponseHeader("Content-Type");
    response.responseText = response.responseText.replace("POLYGON", "MULTIPOLYGON");
    response.responseText = response.responseText.replace("MULTIMULTIPOLYGON", "MULTIPOLYGON");

    if (kontrolli == "UnionSingleT") {
        response.responseText = response.responseText.replace(/\)\,\ \(/g, ",");//eshte replace global,ne te gjithe stringun
    }

    if (contentType == "application/wkt") {
        features = new OpenLayers.Format.WKT().read(response.responseText);
    } else if (contentType == "text/xml; subtype=wfs-collection/1.0") {
        features = new OpenLayers.Format.WFST.v1_0_0().read(response.responseText);
    }
    else {
        if (response.responseText.search("MULTIPOLYGON") > -1) {
            features = new OpenLayers.Format.WKT().read(response.responseText);
        }
        else if (response.responseText.search("MULTILINESTRING") > -1 && kontrolli == "UnionSingleT") {
            features = new OpenLayers.Format.WKT().read(response.responseText);
            var arrayPikat = new Array();
            arrayPikat = features.geometry.getVertices();

            var arrayPaDub = new Array();
            arrayPaDub = arrayPikat.filter(function (elem, pos) {
                for (i = 0; i < arrayPikat.length; i++) {
                    if (arrayPikat[i].toShortString() == elem.toShortString()) {
                        if (i == arrayPikat.indexOf(elem)) {
                            return true;
                        }
                        else if (arrayPikat.indexOf(elem) == i + 1) {
                            return false;
                        }
                    }
                }
                return false;
            });

            features.geometry = new OpenLayers.Geometry.MultiLineString(new OpenLayers.Geometry.LineString(arrayPaDub));
        }
    }

    if (features && (features instanceof OpenLayers.Feature.Vector || features.length)) {
        ManipuloObjektetNgaRezWPS(features, vektoriMeObjektet, objektiMeAtribute);
    }
}

function ManipuloObjektetNgaRezWPS(feature, vektoriMeObjektet, objektiMeAtribute) {
    for (var i = 0; i < vektoriMeObjektet.length; i++) {
        FshiObjektNgaVektoriEditimit(vektoriMeObjektet[i])
    }
    feature.state = OpenLayers.State.INSERT;
    wfsEdit.addFeatures(feature);
    thirrSave = true;
    kopjoAtributetPerObjekt(objektiMeAtribute, feature)
}

function intersectSegmentT(arrayVertexFundore1, arrayVertexFundore2) {
    var nrMe1 = 0;
    var pergjigje = new Object();
    for (var i = 0; i < 2; i++) {
        var count = 1;
        for (var j = 0; j < 2; j++) {
            if (arrayVertexFundore1[i].toString() == arrayVertexFundore2[j].toString()) {
                count = count + 1;
                pergjigje.pika2 = j;
                nrMe1 = nrMe1 + 1;
                pergjigje.pika1 = i;
                break;
            }
        }
    }

    if ((nrMe1 == 1)) {
        pergjigje.intersect = true;
        return pergjigje;
    }
    else {
        pergjigje.intersect = false;
        return pergjigje;
    }
}

function getOrientation(pt1, pt2) {
    var x = pt2.x - pt1.x;
    var y = pt2.y - pt1.y;
    var rad = Math.acos(y / Math.sqrt(x * x + y * y));
    var factor = x > 0 ? 1 : -1;
    return Math.round(factor * rad * 180 / Math.PI);
};

function handleMerge(featuresSelektuara, tipiMerge) {
    if (selekto) {
        selekto.unselectAll();
    }
    if (tipiMerge == "UnionMultiT") {
        AfishoListenEObjekteveSelektuarModifik([], featuresSelektuara, tipiMerge)
    }
    else if (tipiMerge == "UnionSingleT") {
        var nrElementetMerge = 0;
        var gjatFeat = featuresSelektuara.length;
        if (featuresSelektuara[0].geometry instanceof OpenLayers.Geometry.MultiPolygon) {
            for (var i = 0; i < gjatFeat; i++) {
                for (var j = 0; j < gjatFeat; j++) {
                    if (featuresSelektuara[i].geometry.intersects(featuresSelektuara[j].geometry) && i != j) {
                        nrElementetMerge = nrElementetMerge + 1;
                        break;
                    }
                }
            }

            if (nrElementetMerge == gjatFeat) {
                AfishoListenEObjekteveSelektuarModifik([], featuresSelektuara, tipiMerge)
            }
            else {
                noty({ text: perkthe("GP_EDITIM_MSG_MSG_MergeGeo"), type: "info" }); //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_MergeGeo"));
            }
        }
        else if (featuresSelektuara[0].geometry instanceof OpenLayers.Geometry.MultiLineString) {
            if (featuresSelektuara.length == 2) {
                var p = intersectSegmentT(featuresSelektuara[0].geometry.getVertices(true), featuresSelektuara[1].geometry.getVertices(true))

                if (p.intersect == true) {
                    if (p.pika1 == 0 && p.pika2 == 0) {
                        featuresSelektuara[0].geometry = new OpenLayers.Geometry.MultiLineString(new OpenLayers.Geometry.LineString(featuresSelektuara[0].geometry.getVertices().reverse()));
                    }
                    else if (p.pika1 == 1 && p.pika2 == 1) {
                        featuresSelektuara[1].geometry = new OpenLayers.Geometry.MultiLineString(new OpenLayers.Geometry.LineString(featuresSelektuara[1].geometry.getVertices().reverse()));
                    }
                    else if (p.pika1 == 0 && p.pika2 == 1) {
                        featuresSelektuara[1].geometry = new OpenLayers.Geometry.MultiLineString(new OpenLayers.Geometry.LineString(featuresSelektuara[1].geometry.getVertices().reverse()));
                        featuresSelektuara[0].geometry = new OpenLayers.Geometry.MultiLineString(new OpenLayers.Geometry.LineString(featuresSelektuara[0].geometry.getVertices().reverse()));
                    }
                    AfishoListenEObjekteveSelektuarModifik([], featuresSelektuara, tipiMerge)
                }
                else {
                    noty({ text: perkthe("GP_EDITIM_MSG_MSG_MergeGeo"), type: "info" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_MergeGeo"));
                }
            }
            else {
                noty({ text: perkthe("GP_EDITIM_MSG_MSG_MergeGeoVije"), type: "info" }); // Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_EDITIM_MSG_MSG_MergeGeoVije"));
            }
        }
    }
}

var wktFormat = new OpenLayers.Format.WKT();

function executeMergeEditim(candidatesMerge, objektiMeAtribute, kontrolli) {
    var wpsUrl = 'http://localhost:8090/geoserver/wps?&';
    var capabilitiesVersion = "1.0.0";
    var WPSIdentifier = "geo:union";
    filloProcessWPS(wpsUrl, capabilitiesVersion, WPSIdentifier, candidatesMerge, objektiMeAtribute, kontrolli);
}

function kopjoAtributetPerObjekt(fromFeature, toFeature) {
    var geoTrans = toFeature.geometry.clone();
    geoTrans.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
    for (var property in fromFeature.attributes) {
        if (fromFeature.attributes.hasOwnProperty(property)) {
            switch (property) {
                case 'gid':
                    break;

                case 'siperfaqja':
                    toFeature.attributes[property] = geoTrans.getArea();
                    break;

                case 'gjatesia':
                    toFeature.attributes[property] = geoTrans.getLength();
                    break;

                case 'pozicioni_x':
                    toFeature.attributes[property] = geoTrans.x
                    break;

                case 'pozicioni_y':
                    toFeature.attributes[property] = geoTrans.y
                    break;

                default:
                    toFeature.attributes[property] = fromFeature.attributes[property];
                    break;
            }
        }
    }
}


function showSuccessMsg(layer) {
    thirrSave = false;
    if (Ext.getCmp('btnRuaj')) {
        Ext.getCmp('btnRuaj').setDisabled(false);
    }
    refreshProtocol.deactivate();

    if (layerPerEditim) {
        layerPerEditim.redraw(true);
    }
    wfsEdit.destroyFeatures();
    if (Ext.getCmp("EditButtonId").pressed) {
        Ext.getCmp("EditButtonId").toggle(false);
    }
    else if (Ext.getCmp("FshiButtonId").pressed) {
        Ext.getCmp("FshiButtonId").toggle(false);
    }

    if (Ext.getCmp("VizatoPoligonBntId").pressed) {
        Ext.getCmp("VizatoPoligonBntId").toggle(false);
    }
    else if (Ext.getCmp("VizatoVijeBntId").pressed) {
        Ext.getCmp("VizatoVijeBntId").toggle(false);
    }
    else if (Ext.getCmp("VizatoPikeBntId").pressed) {
        Ext.getCmp("VizatoPikeBntId").toggle(false);
    }

    pastroLayersTemp();
};

function pastroLayersTemp() {
    switch (true) {
        case Ext.getCmp("NotConnButtonId").pressed:
            fshihLayerTemp("TempLayerNotConnected", { "CQL_FILTER": 'IDAUTORIZUESI=0 AND IDNDERMVIT=0' });
            Ext.getCmp("NotConnButtonId").toggle(false);
            break;
        case Ext.getCmp("PrindNgaLidhjeButtonId").pressed:
            fshihLayerTemp("TempLayerFemiPrind", { "viewparams": 'gid:0' });
            Ext.getCmp("PrindNgaLidhjeButtonId").toggle(false);
            break;
        case Ext.getCmp("HapElementeNgaBlerjeButtonId").pressed:
        case Ext.getCmp("FemijeLidhjeButtonId").pressed:
        case Ext.getCmp("topologjiBtnId").pressed:
            fshihLayerTemp("TempLayerPrindFemi", { "CQL_FILTER": 'IDAUTORIZUESI=0 AND IDNDERMVIT=0' });
            Ext.getCmp("HapElementeNgaBlerjeButtonId").toggle(false);
            Ext.getCmp("FemijeLidhjeButtonId").toggle(false);
            Ext.getCmp("topologjiBtnId").toggle(false);
            break;
        default:
            break;
    }
}

function showFailureMsg(pergjigjja) {
    var mesazhGabimi = perkthe("GP_EDITIM_MSG_MSG_RuajError");
    var bejLogOut = false;
    if (pergjigjja.indexOf("Parsing failed for LinearRing") != -1) {
        mesazhGabimi = perkthe("GP_EDITIM_MSG_MSG_GeoGabim");
    }
    else if (pergjigjja == "gabimNeHistori") {
        mesazhGabimi = perkthe("GP_EDITIM_MSG_MSG_GeoGabimHistori");
        bejLogOut = true;
    }

    Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), mesazhGabimi, function (btn) {
        if (bejLogOut) {
            window.location.href = 'GisPage.php';
        }
    });

    Ext.getCmp('btnRuaj').setDisabled(false);
}

function showCompleteMsg() {
};

var featureInfo;

function zhdukBaseLayer() {
    if (winBaseLayer) {
        winBaseLayer.hide();
    }
}

function CaktivizoAtribute() {
    featureInfo.deactivate();
}



function executeWpsClient(procesiKerkuar, objInputs) {
    var wpsClient = new OpenLayers.WPSClient({
        servers: { local: varSettings.proxyConfig.url + 'wps&' }
    });

    wpsClient.execute({
        server: 'local',
        process: procesiKerkuar,
        inputs: objInputs,
        success: function (objOutputs) {
            SuccessExecuteWpsClient(procesiKerkuar, objInputs, objOutputs);
        }
    });
};

function SuccessExecuteWpsClient(procesiKerkuar, objInputs, objOutputs) {
    switch (procesiKerkuar) {
        case "JTS:splitPolygon":
            SplitPoligonSuccessExecWpsClient(objOutputs, objInputs.polygon, objInputs.line);
            break;
        default:
            break;
    }
};

function vizatoObjekteButtonHandler(toggled, objekti, id_layer, id_layer_type, pressed, nrStatusi) {
    if (!toggled)
        return;

    ShoqerojiObjektitTeDhenat();

    /*ne fakt ketu nuk ka shume nevoje se e shumta te dhenat mund te jene nga modifikimi qe e kap si mundesi after modified ,eshte bere ndryshime tek after modified edhe kjo duhet*/
    pastroObjektLidhjeWeb(id_layer, id_layer_type, nrStatusi);
    ndryshoStatusin(nrStatusi, id_layer);
    if (pressed) {
        varSettings.editim.objLidhjeGisWeb.veprimi = 'INSERT';
        switch (objekti.id) {
            case 'VizatoPoligonBntId':
                drawPolygon.activate();
                break;
            case 'VizatoVijeBntId':
                drawLine.activate();
                break;
            case 'VizatoPikeBntId':
                drawPoint.activate();
                break;
        }
        enableButonaEditimSipasRastit(true, true, true, true, false, false, false, false, false, false, true, false);
    }
    else {
        //varSettings.editim.objPanel.mbyllPopupEditimInserti = true;
        switch (objekti.id) {
            case 'VizatoPoligonBntId':
                drawPolygon.deactivate();
                break;
            case 'VizatoVijeBntId':
                drawLine.deactivate();
                break;
            case 'VizatoPikeBntId':
                drawPoint.deactivate();
                break;
        }
        enableButonaEditimSipasRastit(true, true, true, true, true, true, false, false, false, false, true, false);
    }
}

function hapLupen(WEBLLOJI, id_layer_type, id) {
    if ($("#" + id).prop("disabled"))
        return;
    objSelektuarNgaLupa = false;
    switch (StatuseLayeri[0].WEBLLOJI) {
        case "MAGAZINA":
            myButtonClickLupa.LupaUniversal_Click("Zgjidh Magazinen", 'LupaMagazina.aspx?idKonfigAmbjente=1&vjenNga=gis&idLlojLayer=' + id_layer_type, 750, 500);
            break;
        case "SERIALE":
            myButtonClickLupa.LupaUniversal_Click("Zgjidh Asetin", 'LupaAseteJoNeHarte.aspx?idLlojLayer=' + id_layer_type, 1100, 500);
            break;
    }
}

function GeneralEditimButtonsHandler(toggled, objekti, objektiControl) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
        objektiControl.activate();
    }
    else {
        objektiControl.deactivate();
    }
}

function DragEditimButtonsHandler(toggled, objekti, objektiControl) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
        objektiControl.activate();
    }
    else {
        objektiControl.deactivate();
    }
}

function ZoomEditimButtonsHandler(toggled, objekti) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        KrijoWhereQuerySipasFunksioneveTeEditimit();
        varSettings.printimiPdf.fromQuery = 'V_GIS_Layer_TEMPLAYERPRINDFEMI'
        zoomToExtendSipasFunksionit(objekti, varSettings.printimiPdf.fromQuery, varSettings.printimiPdf.whereQuery);
    }
};

function KrijoWhereQuerySipasFunksioneveTeEditimit() {
    varSettings.printimiPdf.whereQuery = 'IDAUTORIZUESI=0';

    switch (true) {
        case Ext.getCmp("FemijeLidhjeButtonId").pressed:
            varSettings.printimiPdf.whereQuery = '(IDAUTORIZUESI =' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI = -1) AND ' + (varSettings.editim.objLidhjeGisWeb.gid == 0 ? "" : ("FUNKSLLOJI=4 AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.gid));
            break;
        case Ext.getCmp("HapElementeNgaBlerjeButtonId").pressed:
            varSettings.printimiPdf.whereQuery = '(IDAUTORIZUESI =' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI = -1) AND ' + (varSettings.editim.objLidhjeGisWeb.IDMAGAZINA == 0 ? "" : ("FUNKSLLOJI=" + (varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE == 5 ? "1" : "2") + " AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.IDMAGAZINA));
            break;
        case Ext.getCmp("topologjiBtnId").pressed:
            varSettings.printimiPdf.whereQuery = '(IDAUTORIZUESI =' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI = -1) AND ' + (varSettings.editim.objLidhjeGisWeb.gid == 0 ? "" : ("FUNKSLLOJI=5 AND IDELEMENT=" + varSettings.editim.objLidhjeGisWeb.gid));
            break;
        default:
    };
}

function zoomToExtendSipasFunksionit(objekti, fromQuery, whereQuery){
    var out; 
    if (whereQuery != 'IDAUTORIZUESI=0') {
        $.ajax({
            url: Utils.getServerApiUrl("GIS", "merrGjeometryPerExtendSipasFunksionit"),
            data: JSON.stringify({ fromQuery: fromQuery, whereQuery: whereQuery })
        }).done(function (result) {
            var geojson_format = new OpenLayers.Format.GeoJSON();
            var testFeature = geojson_format.read(result);
            geom = new OpenLayers.Format.WKT({}).write(testFeature).replace("GEOMETRYCOLLECTION(", "").replace(")))", "))");
            zoomToExtendSipasGjeometrise(objekti, geom);
        }).fail(function (errMsg) {
            noty({ text: perkthe("GP_EDITIM_MSG_MSG_RuajError"), type: "error" }); // Ext.MessageBox.alert("GIS", perkthe("GP_EDITIM_MSG_MSG_RuajError"));
        });
    }
    else {
        geom = varSettings.editim.objLidhjeGisWeb.the_geom;
        zoomToExtendSipasGjeometrise(objekti, geom);
    }
};

function zoomToExtendSipasGjeometrise(objekti, geom){
    var feature = new OpenLayers.Format.WKT().read(geom);
    var featureProjected = feature.geometry.transform(new OpenLayers.Projection(projeksioniGeoV), new OpenLayers.Projection(projeksioniGeo)); 
    var featureZoomEditim = new OpenLayers.Feature.Vector(featureProjected);

    var tempVektorZoomEditim = new OpenLayers.Layer.Vector("TempLayerZoomEditim", { isBaseLayer: false });
    tempVektorZoomEditim.addFeatures(featureZoomEditim);
    map.zoomToExtent(tempVektorZoomEditim.getDataExtent());

    objekti.toggle(false);

    Ext.getCmp("RefreshButtonId").toggle();
    RefreshEditimButtonsHandler(true, Ext.getCmp("RefreshButtonId"))
};

function RefreshEditimButtonsHandler(toggled, objekti) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        var tempSplitLayer = map.getLayersBy("name", "SplitLayerEditim")[0];
        var tempTolerance = Ext.getCmp("ToleranceFieldID").value;
        if (tempTolerance != "") {
            tempTolerance = parseInt(tempTolerance);
            varSettings.editim.allControls.snapControl.tolerance = tempTolerance;
            varSettings.editim.allControls.snapControl.targets[0].tolerance = tempTolerance;
            varSettings.editim.allControls.snapControl.targets[0].nodeTolerance = tempTolerance;
            varSettings.editim.allControls.snapControl.targets[0].vertexTolerance = tempTolerance;
            varSettings.editim.allControls.snapControl.targets[0].edgeTolerance = tempTolerance;
        }
        tempSplitLayer.protocol.params.layers = JSON.stringify(map.getLayersBy("visibility", true).filter(function (el) { return el.IDLAYERSTYPE > 2; }).map(function (value, index) { return value.IdLayer; })),
        tempSplitLayer.protocol.params.excludeGid = varSettings.editim.objLidhjeGisWeb.gid;
        tempSplitLayer.strategies[1].refresh();
        objekti.toggle(false);
        Ext.getCmp("EditButtonId").toggle();
    }
}

function SnapItemsButtonHandler(elementi, checked, objektiControl, lloji) {
    if (checked) {
        if (!objektiControl.active) {
            objektiControl.activate();
        }
        objektiControl.targets[0][lloji] = true;
    }
    else {
        objektiControl.targets[0][lloji] = false;
    }
}

function ConnButtonHandler(toggled, objekti, nameLayer, id_layer_type) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        pastroLayersTemp();
        varSettings.editim.controlClickFromWmsLayer.deactivate();

        var arrayElement = [];
        arrayElement.push(varSettings.editim.objLidhjeGisWeb.gid);

        var url = Utils.getServerApiUrl("GIS", "MerrTeDhenatGeoPerLidhjeObjekteshNeSelect");
        if (StatuseLayeri[0].WEBLLOJI == "GIS")
            varSettings.editim.controlClickFromWmsLayerWeb = krijoProtokollPerControlClickGetFeature(url, comboLayer2.value, arrayElement, varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.gid);
        else if (StatuseLayeri[0].WEBLLOJI == "MAGAZINA")
            varSettings.editim.controlClickFromWmsLayerWeb = krijoProtokollPerControlClickGetFeature(url, comboLayer2.value, arrayElement, varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.IDMAGAZINA);
        else
            varSettings.editim.controlClickFromWmsLayerWeb = krijoProtokollPerControlClickGetFeature(url, comboLayer2.value, arrayElement, varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.IDSERIALI);
        varSettings.editim.controlClickFromWmsLayerWeb.events.register("featuresselected", this, function (e) { featureSelectedEvent(e, objekti, nameLayer, id_layer_type) });
        map.addControl(varSettings.editim.controlClickFromWmsLayerWeb);
        varSettings.editim.controlClickFromWmsLayerWeb.activate();
    }
    else {
        varSettings.editim.controlClickFromWmsLayerWeb.deactivate();
        buttonUnPressedHandler(objekti);
    }
};

function buttonUnPressedHandler(objekti) {
    shkaterroDritareModifikimMenuObj();
    Ext.getCmp("EditButtonId").toggle();
    varSettings.editim.controlClickFromWmsLayer.activate();
    objekti.toggle(false);
}

function MergeButtonHandler(objekti, nameLayer, id_layer_type) {
    pastroLayersTemp();
    varSettings.editim.controlClickFromWmsLayer.deactivate();

    var arrayElement = varSettings.editim.objLidhjeGisWebAfishim.cols.map(function (value, index) { return value.gid; });

    if (varSettings.editim.controlClickFromWmsLayerMerge == null) {
        var url = Utils.getServerApiUrl("GIS", "MerrTeDhenatGeoPerMergeObjekteshNeSelect");
        varSettings.editim.controlClickFromWmsLayerMerge = krijoProtokollPerControlClickGetFeature(url, comboLayer2.value, arrayElement, varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.gid);
        varSettings.editim.controlClickFromWmsLayerMerge.events.register("featuresselected", this, function (e) { featureSelectedEvent(e, objekti, nameLayer, id_layer_type) });
        map.addControl(varSettings.editim.controlClickFromWmsLayerMerge);
        varSettings.editim.controlClickFromWmsLayerMerge.activate();
    }
    else {
        varSettings.editim.controlClickFromWmsLayerMerge.activate();
        ndryshoProtokollParamsPerControlClickGetFeature(varSettings.editim.controlClickFromWmsLayerMerge, comboLayer2.value, arrayElement, varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.gid);
    }
};

function DeleteConnButtonHandler(toggled, objekti, nameLayer, id_layer_type) {
    if (!toggled)
        return;
    if (objekti.pressed) {

        Ext.MessageBox.show({
            title: perkthe("GP_EDITIM_WIN_TIT") + " - " + perkthe("ADUI_MSG_Kujdes") + "!",
            msg: perkthe("GP_EDITIM_MSG_HIQLIDHJE"),
            buttons: Ext.MessageBox.YESNO,
            fn: function (btn) {
                if (btn == "yes") {
                    varSettings.editim.objLidhjeGisWeb.gidPrindi = 0;
                    varSettings.editim.objLidhjeGisWeb.kodPrindi = '';
                    varSettings.editim.objLidhjeGisWebAfishim.gidPrindi = 0;
                    varSettings.editim.objLidhjeGisWebAfishim.kodPrindi = '';
                    varSettings.editim.objPanel.frmeditim.find("id", "kodPrindi" + varSettings.editim.objOpenLayer.id + "")[0].setRawValue('');
                }
            },
            icon: Ext.MessageBox.QUESTION
        });
    }
    Ext.getCmp("DeleteConnButtonId").toggle(false);
    Ext.getCmp("EditButtonId").toggle();
};

function TempLayersFemijeButtonHandler(toggled, objekti, nameLayer, id_layer_type, nameLayerTemp) {
    if (!toggled) {
        console.log(objekti.id + ' is not toggled');
        return;
    }
    if (objekti.pressed) {
        if (varSettings.editim.objLidhjeGisWeb.gid != 0) {
            var viewparamsLayers = 'gid:' + varSettings.editim.objLidhjeGisWeb.gid + '';
            shfaqLayerTempMeParametra(nameLayerTemp, { "viewparams": viewparamsLayers }, true, true);
        }
    }
    else {
        fshihLayerTemp(nameLayerTemp, { "viewparams": 'gid:0' });
    }
};

function TempLayersPrindButtonHandler(toggled, objekti, nameLayerTemp, paramsLloji) {
    if (!toggled) {
        console.log(objekti.id + ' is not toggled');
        return;
    }
    hiqFunksionePerButtonaNeNjeLayer(objekti, nameLayerTemp);
    if (objekti.pressed) {
        var withFiscalYear = map.getLayersBy("visibility", true).filter(function (el) { return el.IdLayer == varSettings.editim.objLidhjeGisWeb.IDLAYER; })[0].DEFAULTOBJEKT.WITHFISCALYEAR;

        layerParams = '(IDAUTORIZUESI=' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI=-1) ' + (paramsLloji == "" ? "" : ' AND (' + paramsLloji + ')');
        layerParams += (withFiscalYear ? " AND IDNDERMVIT=" + varSettings.webConfig.idNdermVit : "");
        
        shfaqLayerTempMeParametra(nameLayerTemp, { "CQL_FILTER": layerParams }, true, true);
    }
    else {
        fshihLayerTemp(nameLayerTemp, { "CQL_FILTER": 'IDAUTORIZUESI=0' });
        varSettings.printimiPdf.whereQuery = '';
    }
};

function hiqFunksionePerButtonaNeNjeLayer(objekti, nameLayerTemp) {
    shfaqLayerTempMeParametra(nameLayerTemp, { "CQL_FILTER": 'IDAUTORIZUESI=0' }, true, true);

    if (Ext.getCmp("FemijeLidhjeButtonId") != objekti && Ext.getCmp("FemijeLidhjeButtonId").pressed)
        Ext.getCmp("FemijeBrezi1ButtonId").toggle(false);

    if (Ext.getCmp("HapElementeNgaBlerjeButtonId") != objekti && Ext.getCmp("HapElementeNgaBlerjeButtonId").pressed)
        Ext.getCmp("HapElementeNgaBlerjeButtonId").toggle(false);

    if (Ext.getCmp("topologjiBtnId") != objekti && Ext.getCmp("topologjiBtnId").pressed)
        Ext.getCmp("topologjiBtnId").toggle(false);
}

function TempLayerNotConnectedButtonHandler(toggled, objekti, id_layer, nameLayer) {
    if (!toggled)
        return;
    if (objekti.pressed) {
        var withFiscalYear = map.getLayersBy("visibility", true).filter(function (el) { return el.IdLayer == id_layer; })[0].DEFAULTOBJEKT.WITHFISCALYEAR;
        var layerParams = ' (IDAUTORIZUESI=' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI=-1) AND IDLAYER = ' + id_layer;
        layerParams += (withFiscalYear ? " AND IDNDERMVIT=" + varSettings.webConfig.idNdermVit : "");
        shfaqLayerTempMeParametra(nameLayer, { "CQL_FILTER": layerParams }, true, true);
    }
    else {
        fshihLayerTemp(nameLayer, { "CQL_FILTER": 'IDAUTORIZUESI=0' });
    }
}

function KerkoButtonHapsinorHandler(toggled, objekti) {
    if (!toggled)
        return;
    varSettings.kerkimHapsinor.polygonLayer = new OpenLayers.Layer.Vector("PoligonKerkimHapsinor", { VektPerkohshemLayer: true });

    varSettings.kerkimHapsinor.polygonControl = new OpenLayers.Control.DrawFeature(varSettings.kerkimHapsinor.polygonLayer, OpenLayers.Handler.Polygon, {
        id: "drawKerkimHapesinorPoligonContrId",
        handlerOptions: { 'style': stilPerDrawKerkimHapesinor },
    });
    map.addLayer(varSettings.kerkimHapsinor.polygonLayer);
    map.addControl(varSettings.kerkimHapsinor.polygonControl);

    varSettings.kerkimHapsinor.polygonLayer.events.on({
        beforefeatureadded: function (event) {
            map.getLayersByName("PoligonKerkimHapsinor")[0].removeAllFeatures();
        }
    });
    varSettings.kerkimHapsinor.polygonLayer.events.on({
        featureadded: function (event) {
            var bBox = ktheGjeometriNeString(event.feature);
            map.getLayersByName("PoligonKerkimHapsinor")[0].removeAllFeatures();
            varSettings.kerkimHapsinor.polygonControl.deactivate();

            popupUniversal2.SetHeaderText(perkthe("GP_KERKIMHAPTABPAN_TIT"));
            popupUniversal2.SetContentUrl('GISLupaKerkimHapsinor.aspx?bBox=' + bBox);
            popupUniversal2.SetSize(900, 600);
            popupUniversal2.Show();
            popupUniversal2.SetCollapsed(false);

            if (Ext.getCmp("KerkoBtnAll").pressed) {
                Ext.getCmp("KerkoBtnAll").toggle(false);
            }
        }
    });
    varSettings.kerkimHapsinor.polygonControl.activate();
}

function buttonNextPreviousHandlerEventRegister(control, button, classActivate, classDeactivate) { //(nav.previous, buttonPrevious, 'prevon', 'prevoff')
    control.events.register(
        "activate",
        button,
        function () {
            this.setDisabled(false);
            this.setIconClass(classActivate); // set the icon when activated
        }
    );
    control.events.register(
        "deactivate",
        button,
        function () {
            this.setDisabled(true);
            this.setIconClass(classDeactivate); // set the icon when deactivated
        }
    );
}

function pressedHandlerTransaction(objekti, listatAtributeveCombo, id_layer, id_layer_type, statusVeprimi) {
    if (selekto)
        selekto.unselectAll();
    caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
    ShoqerojiObjektitTeDhenat();
    varSettings.editim.controlClickFromWmsLayer.activate();
    modifiko.activate();
    wfsEdit.events.remove('beforefeaturemodified');
    wfsEdit.events.register('beforefeaturemodified', wfsEdit, function (event) {
        ShoqerojiObjektitTeDhenat();
        STATE = statusVeprimi;
        AfishoPopupInsertUpdate(event, id_layer, listatAtributeveCombo, STATE);
    });

    wfsEdit.events.remove('afterfeaturemodified');
    wfsEdit.events.register('afterfeaturemodified', wfsEdit, function (event) {
        thirrSave = true;
    });
    wfsEdit.events.register('featuremodified', wfsEdit, function (event) {
        vendosSipGjatesiPozXYGjateEditimit(event);
    });
}

function EditButtonHandler(toggled, objekti, listatAtributeveCombo, id_layer, id_layer_type, nrStatusi) {
    if (!toggled)
        return;

    pastroObjektLidhjeWeb(id_layer, id_layer_type, nrStatusi);
    ndryshoStatusin(nrStatusi, id_layer);
    if (objekti.pressed) {
        pressedHandlerTransaction(objekti, listatAtributeveCombo, id_layer, id_layer_type, "UPDATE");
        enableButonaEditimSipasRastit(true, false, false, false, true, false, true, true, true, true, true, true);
    }
    else {
        Ext.MessageBox.show({
            title: perkthe("GP_EDITIM_MSG_TIT_RUAJ"),
            msg: perkthe("GP_EDITIM_MSG_MSG_RUAJ"),
            buttons: Ext.MessageBox.YESNOCANCEL,
            fn: YesNoCancelEditimHapur,
            icon: Ext.MessageBox.QUESTION
        });
    }

}

function FshijButtonHandler(toggled, objekti, listatAtributeveCombo, id_layer, id_layer_type, nrStatusi) {
    if (!toggled)
        return;
    pastroObjektLidhjeWeb(id_layer, id_layer_type, nrStatusi);
    if (objekti.pressed) {
        pressedHandlerTransaction(objekti, listatAtributeveCombo, id_layer, id_layer_type, "DELETE");
        enableButonaEditimSipasRastit(true, false, false, false, false, true, false, true, false, false, false, true);
    }
    else {
        Ext.MessageBox.show({
            title: perkthe("GP_EDITIM_MSG_TIT_RUAJ"),
            msg: perkthe("GP_EDITIM_MSG_MSG_RUAJ"),
            buttons: Ext.MessageBox.YESNOCANCEL,
            fn: YesNoCancelEditimHapur,
            icon: Ext.MessageBox.QUESTION
        });
    }
}
function YesNoCancelEditimHapur(btn) {
    if (btn == "yes") {
        RuajButton.handler.call(RuajButton.scope || RuajButton, RuajButton, "");
    }
    if (btn == "no") {
        caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim);
        thirrSave = false;
        if (varSettings.editim.allControls.snapControl && varSettings.editim.allControls.snapControl.active) {
            varSettings.editim.allControls.snapControl.deactivate();
        }
        if (rightClick) {
            rightClick.deactivate();
        }
        wfsEdit.destroyFeatures();
        enableButonaEditimSipasRastit(true, true, true, true, true, true, false, false, false, false, true, false);
    }
}

function ktheGjeometriNeString(feature) {
    var geoJSON = feature.clone();
    geoJSON.geometry = geoJSON.geometry.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
    geoJSON = new OpenLayers.Format.WKT().write(geoJSON);
    return geoJSON;
}

// Seksioni i zgjedhjes se objekteve per veprime si editim, fshirje, lidhje objektesh, merge
function transformoGjeometri(el) {
    return el.geometry.transform(new OpenLayers.Projection(projeksioniGeoV), new OpenLayers.Projection(projeksioniGeo));
};

function featureSelectedEvent(e, ButtonObj, emriLayer, id_layer_type) {
    shkaterroDritareModifikimMenuObj();
    for (var i = 0; i < e.features.length; i++) {
        transformoGjeometri(e.features[i]);
        e.features[i].fid = emriLayer + "." + e.features[i].attributes["gid"];
    }
    featureNukNdodhetNeVektor = e.features;

    switch (true) {
        case (Ext.getCmp("ConnButtonId").pressed):
            elementetZgjedhurNeEditim(e, ButtonObj, emriLayer, id_layer_type, "ConnButtonZgjedhje");
            break;
        case varSettings.editim.objZgjedhurMerge:
            elementetZgjedhurNeEditim(e, ButtonObj, emriLayer, id_layer_type, "MergeButtonZgjedhje");
            break;
        case Ext.getCmp("EditButtonId").pressed:
        case Ext.getCmp("FshiButtonId").pressed:
            if (varSettings.editim.objLidhjeGisWeb.gid == 0) {
                wfsEdit.destroyFeatures();
                wfsEdit.addFeatures(e.features);
                elementetZgjedhurNeEditim(e, ButtonObj, emriLayer, id_layer_type, "EditButtonZgjedhje");
            }
            break;
        default:
            wfsEdit.destroyFeatures();
            wfsEdit.addFeatures(e.features);
            elementetZgjedhurNeSelect(e, ButtonObj, emriLayer, id_layer_type);
    }
};

function elementetZgjedhurNeEditim(e, buttonObj, emriLayer, id_layer_type, llojiButtoni) {
    selekto.unselectAll();
    selekto.deactivate();

    eventInserUpdateAtribute = null;
    modifiko.activate();
    if (modifiko && modifiko.feature) {
        modifiko.unselectFeature(modifiko.feature)
    }

    var nrFeatures = e.features.length;
    if (nrFeatures > 1) {
        switch (llojiButtoni) {
            case "ConnButtonZgjedhje":
            case "MergeButtonZgjedhje":
                AfishoListenEObjekteveSelektuarModifik(featureNukNdodhetNeVektor, featureNdodhetNeVektor, llojiButtoni, emriLayer);
                break;
            default:
                AfishoListenEObjekteveSelektuarModifik(featureNukNdodhetNeVektor, featureNdodhetNeVektor, modifiko, emriLayer);
        }
    }
    else {
        switch (llojiButtoni) {
            case "ConnButtonZgjedhje":
                selekto.activate();
                selekto.activate(featureNukNdodhetNeVektor[0]);
                endConnButtonZgjedhje(e.features[0]);
                break;
            case "MergeButtonZgjedhje":
                endMergeButtonZgjedhje(e.features[0], true);
                break;
            default:
                endEditButtonZgjedhje(featureNukNdodhetNeVektor[0]);
        }
        featureSelectedEditim = e.features[0];
    }
};

function elementetZgjedhurNeSelect(e, buttonObj, emriLayer, id_layer_type) {
    selekto.activate();
    selekto.unselectAll();

    for (var i = 0; i < featureNukNdodhetNeVektor.length; i++) {
        selekto.select(featureNukNdodhetNeVektor[i]);
    }

};

function AfishoListenEObjekteveSelektuarModifik(featureNukNdodhetNeVektor1, featureNdodhetNeVektor1, kontrolli, emriLayer) {

    shkaterroDritareModifikimMenuObj();

    if (!varSettings.editim.allVectors.vektorTempPerSelectObjekti) {
        varSettings.editim.allVectors.vektorTempPerSelectObjekti = new OpenLayers.Layer.Vector("vektorTempPerSelectObjekti", {
            styleMap: stilVektorTempPerSelectObjekti,
            VektPerkohshemLayer: true
        });
        map.addLayer(varSettings.editim.allVectors.vektorTempPerSelectObjekti);
    }

    var menuSelektObjekteModifiko = new Ext.menu.Menu({
        title: "Objektet e perzgjedhura",
        width: 250,
        height: (featureNukNdodhetNeVektor1.length) * 28,
        closable: true,
        id: "objModMenu",
        floating: false,
        items: [],
        listeners: {
            "mouseover": function (thisMenu, e, menuItem) {
                if (menuItem) {
                    varSettings.editim.allVectors.vektorTempPerSelectObjekti.removeAllFeatures();
                    varSettings.editim.allVectors.vektorTempPerSelectObjekti.drawFeature(menuItem.objektiS);
                }
            },
            "beforedestroy": function () {
                varSettings.editim.allVectors.vektorTempPerSelectObjekti.removeAllFeatures()
                if (mapPanel.map.layers.indexOf(varSettings.editim.allVectors.vektorTempPerSelectObjekti) != -1) {
                    mapPanel.map.removeLayer(varSettings.editim.allVectors.vektorTempPerSelectObjekti);
                    varSettings.editim.allVectors.vektorTempPerSelectObjekti = null;
                }
            }
        }
    });

    for (var i = 0; i < featureNukNdodhetNeVektor1.length; i++) {
        shtoElementNeMenuMod(featureNukNdodhetNeVektor1[i], kontrolli, emriLayer)
    }
    Ext.getCmp("objModMenu").doLayout(true, true);

    krijoDritareModifikimMenuObj(kontrolli, menuSelektObjekteModifiko);
}

function shtoElementNeMenuMod(geoObj, kontrolli, emriLayer) {
    switch (kontrolli) {
        case "ConnButtonZgjedhje":
            emriLayer = geoObj.attributes.Layer + " - " + geoObj.attributes.Kodi + " ( " + perkthe("GP_EDITIM_CONN_DISTANCE") + " : ( " + geoObj.attributes.Distanca + " m )";
            break;
        case "MergeButtonZgjedhje":
            emriLayer = geoObj.attributes.Layer + " - " + geoObj.attributes.Pershkrimi;
            break;
        case modifiko:
            emriLayer = geoObj.attributes.KODI + " - " + geoObj.attributes.PERSHKRIMI;
            break;
        default:
            emriLayer = emriLayer + geoObj.attributes.gid;
    }

    Ext.getCmp("objModMenu").addItem({
        text: emriLayer,
        objektiS: geoObj,
        handler: function (toggled) {
            objModMenuHandler(toggled, this, kontrolli);
        }
    });
};

function krijoDritareModifikimMenuObj(kontrolli, menuSelektObjekteModifiko) {
    var titleModMenuWindow = "";
    switch (kontrolli) {
        case "ConnButtonZgjedhje":
            titleModMenuWindow = perkthe("GP_EDITIM_CONN_WINTITLE");
            break;
        case "MergeButtonZgjedhje":
            titleModMenuWindow = perkthe("GP_EDITIM_MERGE_WINTITLE");
            break;
        case modifiko:
            titleModMenuWindow = perkthe("GP_WIN_TIT_ModMultiSelect");
            break;
        case ('UnionSingleT' || 'UnionMultiT'):
            titleModMenuWindow = perkthe("GP_WIN_TIT_JoinMultiSelect");
            break;
        case 'buffer':
            titleModMenuWindow = perkthe("GP_WIN_TIT_BufferMultiSelect");
            break;
    };
    gisElements.createGISElement("Window", { title: titleModMenuWindow, id: "objModMenuWindow", border: false, items: [menuSelektObjekteModifiko] });
};

function shkaterroDritareModifikimMenuObj() {
    if (Ext.getCmp("objModMenu")) {
        Ext.getCmp("objModMenu").destroy()
    }

    if (Ext.getCmp("objModMenuWindow")) {
        Ext.getCmp("objModMenuWindow").destroy()
    }
}

function objModMenuHandler(toggled, objekti, kontrolli) {
    if (!toggled)
        return;
    switch (kontrolli) {
        case "ConnButtonZgjedhje":
            endConnButtonZgjedhje(objekti.objektiS);
            break;
        case "MergeButtonZgjedhje":
            endMergeButtonZgjedhje(objekti.objektiS, false);
            break;
        case modifiko:
            endEditButtonZgjedhje(objekti.objektiS);
            break;
        case (('UnionSingleT' || 'UnionMultiT') && featureNdodhetNeVektor1.length > 0):
            executeMergeEditim(featureNdodhetNeVektor1, objekti.objektiS, kontrolli);
            break;
        case "buffer":
            harte.krijoBufferPerObjektin(objekti.objektiS, emriLayer);
            break;
    }
    shkaterroDritareModifikimMenuObj();
};

function endEditButtonZgjedhje(feature) {
    var geometryType = feature.geometry.CLASS_NAME;
    if (geometryType == "OpenLayers.Geometry.MultiLineString" || geometryType == "OpenLayers.Geometry.MultiPolygon") {
        Ext.getCmp('SplitButtonId').disable();
        Ext.getCmp('SplitButtonPolygonId').disable();
    }
    modifiko.selectFeature(feature);
};

function endConnButtonZgjedhje(feature) {
    varSettings.editim.objLidhjeGisWeb.gidPrindi = feature.attributes.gid;
    if (feature.attributes.gid > 0 && feature.attributes.Kodi === '')
        varSettings.editim.objPanel.frmeditim.find("id", "kodPrindi" + varSettings.editim.objOpenLayer.id + "")[0].setRawValue('(E papercaktuar)');
    else
        varSettings.editim.objPanel.frmeditim.find("id", "kodPrindi" + varSettings.editim.objOpenLayer.id + "")[0].setRawValue(feature.attributes.Kodi);

    varSettings.editim.controlClickFromWmsLayerWeb.deactivate();
    buttonUnPressedHandler(Ext.getCmp("ConnButtonId"));
};

function endMergeButtonZgjedhje(feature, withDraw) {
    var newGeomMerge = ktheGjeometriNeString(feature);
    varSettings.editim.objLidhjeGisWeb.the_geom = newGeomMerge;

    if (!varSettings.editim.allVectors.vektorMergePerSelectObjekti) {
        varSettings.editim.allVectors.vektorMergePerSelectObjekti = new OpenLayers.Layer.Vector("vektorMergePerSelectObjekti", { styleMap: stilVektorTempPerSelectObjekti, VektPerkohshemLayer: true });
        map.addLayer(varSettings.editim.allVectors.vektorMergePerSelectObjekti);
    }

    var foundMain = varSettings.editim.objLidhjeGisWebAfishim.cols.filter(function (el) { return el.gid == varSettings.editim.objLidhjeGisWeb.gid; });
    if (foundMain.length == 0)
        varSettings.editim.objLidhjeGisWebAfishim.cols.push({ gid: varSettings.editim.objLidhjeGisWeb.gid, the_geom: newGeomMerge, Veprimi: "UPDATE" });
    else
        varSettings.editim.objLidhjeGisWebAfishim.cols[0].the_geom = newGeomMerge;

    var foundMerge = varSettings.editim.objLidhjeGisWebAfishim.cols.filter(function (el) { return el.gid == feature.attributes.gid; });
    if (foundMerge.length == 0) {
        varSettings.editim.objLidhjeGisWebAfishim.cols.push({ gid: feature.attributes.gid, the_geom: newGeomMerge, Veprimi: "DELETE" });
        Ext.getCmp("MergeButtonId").menu.add({ text: feature.attributes.Layer + " - " + feature.attributes.Pershkrimi });
        varSettings.editim.allVectors.vektorMergePerSelectObjekti.drawFeature(feature);
    };

    ndryshoProtokollParamsPerControlClickGetFeature(varSettings.editim.controlClickFromWmsLayerMerge, varSettings.editim.controlClickFromWmsLayerMerge.protocol.params.layer, varSettings.editim.objLidhjeGisWebAfishim.cols.map(function (value, index) { return value.gid; }), varSettings.editim.objLidhjeGisWeb.the_geom, varSettings.editim.objLidhjeGisWeb.gid);
};

function shfaqLayerTempMeParametra(layerName, mergeParamsValues, visibility, beMerge) {
    tempLayerZgjedhur = map.getLayersByName(layerName)[0];
    tempLayerZgjedhur.setVisibility(visibility);
    if (beMerge) {
        tempLayerZgjedhur.mergeNewParams(mergeParamsValues);
        tempLayerZgjedhur.redraw(true);
    }
};

function fshihLayerTemp(layerName, params) {
    shfaqLayerTempMeParametra(layerName, params, true, true);
    tempLayerZgjedhur = map.getLayersByName(layerName)[0]
    tempLayerZgjedhur.setVisibility(false);
};

function uploadButtonHandler(tooggled, objekti) {
    if (objekti.pressed) {
        shfaqDritareUploadShp();
    }
    else {
        mbyllDritareUploadShp();
    }
};

function RuajBtnHandler(toggled, objekti, ngaThirret, layer, nameLayer, btn, oldLayer, id_layer, id_layer_type, nrStatusi) {
    if (toggled) {
        var layerMeAutorizim = varSettings.webConfig.layers.filter(function (layer) {
            return (layer.IDLAYER.toString() == varSettings.editim.objLidhjeGisWeb.IDLAYER)
        })[0].WITHAUTHORIZATION; 

        if (varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE !== 1 && layerMeAutorizim) {
            if ((varSettings.editim.objLidhjeGisWeb.NRSTATUSI == 1 || varSettings.editim.objLidhjeGisWeb.NRSTATUSI == 2) && varSettings.editim.objLidhjeGisWeb.IDMAGAZINA == 0) {
                noty({ text: perkthe("GP_EDITIM_BTN_TOOLTIP_Selekt"), type: "info" }); 
                if (ngaThirret == "ndryshimLayeri") {
                    comboLayer2.store.clearFilter();
                    comboLayer2.setValue(oldLayer);
                }
                return;
            }
        }

        Ext.getCmp('btnRuaj').setDisabled(true);
        if (modifiko.feature) {
            modifiko.unselectFeature(modifiko.feature);
        }
        ShoqerojiObjektitTeDhenat();

        if (featureSelectedEditim) {
            selekto.unselectAll();
        }

        if (wfsEdit.features.length > 0) {
            // Per IDLAYERSTYPE=1 ruajtja nepermjet geoserverit
            if (varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE == 1)
                saveStrategyEditim.save();
            else
                transaksionLidhjeObjekteshGISWEB();
        }
        else {
            showSuccessMsg(layer);
        }
        pastroFormEditimi();
        refreshProtocol.activate();
        wfsEdit.events.register('sketchcomplete', wfsEdit, function (event) { });
        wfsEdit.destroyFeatures();
    }
    switch (ngaThirret) {
        case "ndryshimLayeri":
            merrKolonaLayerDheStatuseAfishimAsync(layer, nameLayer, id_layer, id_layer_type, nrStatusi);
            break;
        case "mbyllEditim":
            shkaterroDritareEditimi();
            break;
    }

    enableButonaEditimSipasRastit(true, true, true, true, true, true, false, false, false, false, true, false);
}

function HapKarteleBtnHandler(toggled, objekti, id_layer, id_lloj_layer) {
    if (!toggled)
        return;
    switch (StatuseLayeri[0].WEBLLOJI){
        case "MAGAZINA":
            if (varSettings.editim.objLidhjeGisWeb.IDMAGAZINA > 0) {
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "kaTeDrejteTeHapeAmbjentin"),
                    data: JSON.stringify({
                        emerkomponente: 'Shto_NjesiAdministrative.aspx',
                        url: 'Shto_NjesiAdministrative.aspx?vjenNga=GIS&idmagazina=' + varSettings.editim.objLidhjeGisWeb.IDMAGAZINA, newTab: true,
                        idNdermarje: varSettings.webConfig.idNdermarrja, idPerdoruesi: varSettings.webConfig.idPerdoruesi, idGjuha: varSettings.webConfig.idGjuha
                    })
                }).done(SuccededCallbackTeDrejta);
            }
            break;
        case "SERIALE":
            if (varSettings.editim.objLidhjeGisWeb.IDSERIALI > 0) {
                var url = Utils.getServerApiUrl("Konfigurime", "ktheIdKonfigurimiSipasIdKodifikimi");
                $.ajax({
                    url: url,
                    data: JSON.stringify({ idKodifikimi: varSettings.editim.objLidhjeGisWeb.IDKODIFIKIMI, idNderm: varSettings.webConfig.idNdermarrja })
                }).done(function (result) {
                    $.ajax({
                        url: Utils.getServerApiUrl("Autorizime", "kaTeDrejteTeHapeAmbjentin"),
                        data: JSON.stringify({
                            emerkomponente: 'Seriale.aspx', url: 'Seriale.aspx?vjenNga=GIS&idSeriali=' + varSettings.editim.objLidhjeGisWeb.IDSERIALI + '&idartikulli=' + varSettings.editim.objLidhjeGisWeb.IDARTIKULLI + '&idKonfigAmbjente=' + result,
                            newTab: true, idNdermarje: varSettings.webConfig.idNdermarrja, idPerdoruesi: varSettings.webConfig.idPerdoruesi, idGjuha: varSettings.webConfig.idGjuha
                        })
                    }).done(
                                        SuccededCallbackTeDrejta);
                });
            }
            break;
        case "GIS":
            if (varSettings.editim.objLidhjeGisWeb.gid > 0) {
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "kaTeDrejteTeHapeAmbjentin"),
                    data: JSON.stringify({
                        emerkomponente: 'GISLupaElemente.aspx', url: 'GISLupaElemente.aspx?vjenNga=GIS&gid=' + varSettings.editim.objLidhjeGisWeb.gid + '&kodi=' + varSettings.editim.objLidhjeGisWeb.KODI + '&kodi=' + varSettings.editim.objLidhjeGisWeb.KODI + '&pershkrimi=' + varSettings.editim.objLidhjeGisWeb.PERSHKRIMI,
                        newTab: true, idNdermarje: varSettings.webConfig.idNdermarrja, idPerdoruesi: varSettings.webConfig.idPerdoruesi, idGjuha: varSettings.webConfig.idGjuha
                    })
                }).done(SuccededCallbackTeDrejta);
            }
            break;
    }
};

function HapBlerjeBtnHandler(toggled, objekti, id_layer, id_lloj_layer) {
    if (!toggled)
        return;
    switch (StatuseLayeri[0].WEBLLOJI) {
        case "MAGAZINA":
            if (varSettings.editim.objLidhjeGisWeb.IDMAGAZINA > 0)
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "kaTeDrejteTeHapeAmbjentin"),
                    data: JSON.stringify({
                        emerkomponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje', url: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim',
                        newTab: true, idNdermarje: varSettings.webConfig.idNdermarrja, idPerdoruesi: varSettings.webConfig.idPerdoruesi, idGjuha: varSettings.webConfig.idGjuha
                    })
                }).done(SuccededCallbackTeDrejta);
            break;
        case "SERIALE":
            if (varSettings.editim.objLidhjeGisWeb.IDSERIALI > 0 && varSettings.editim.objLidhjeGisWeb.IDKOKADOK > 0)
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "kaTeDrejteTeHapeAmbjentin"),
                    data: JSON.stringify({
                        emerkomponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje', url: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=' + varSettings.editim.objLidhjeGisWeb.IDKOKADOK + '&shtim_modifikim=modifikim',
                        newTab: true, idNdermarje: varSettings.webConfig.idNdermarrja, idPerdoruesi: varSettings.webConfig.idPerdoruesi, idGjuha: varSettings.webConfig.idGjuha
                    })
                }).done(SuccededCallbackTeDrejta);
                break;
            break;
    }
};

//Seksioni Split handlers
function shtoObjLidhjeGisWebNeCol(isFirst, feature) {
    var tempObjLidhjeGisWeb = { gid: varSettings.editim.objLidhjeGisWeb.gid, the_geom: '', Veprimi: "UPDATE" };
    if (!isFirst) {
        tempObjLidhjeGisWeb.gid = 0;
        tempObjLidhjeGisWeb.Veprimi = "INSERT";
    }
    else {
        tempObjLidhjeGisWeb.Veprimi = "UPDATE";
    }
    tempObjLidhjeGisWeb.the_geom = ktheGjeometriNeString(feature);
    varSettings.editim.objLidhjeGisWebAfishim.cols.push(tempObjLidhjeGisWeb);
};

function SplitLineHandler(event) {
    varSettings.editim.allControls.splitControl.handler.style = sketchSymbolizersMat["Polygon"];
    varSettings.editim.objLidhjeGisWebAfishim.cols = [];
    for (var i = 0; i < event.features.length; ++i) {
        shtoObjLidhjeGisWebNeCol((i == 0 ? true : false), event.features[i]);
    }
    Ext.getCmp("SplitButtonId").toggle(false);
    varSettings.editim.allControls.splitControl.deactivate();
};

function SplitDrawPoligonHandler(event) {
    var splitter = event.feature;
    var candidates = OpenLayers.Array.filter(wfsEdit.features, function (feature) {
        var hit = false;
        if (feature.geometry.intersects(splitter.geometry)) hit = true;
        return hit;
    });

    var candidate;
    for (var i = 0, ii = candidates.length; i < ii; ++i) {
        candidate = candidates[i];
        if (candidate.geometry.intersects(splitter.geometry) && candidate.state != "Delete") {
            executeWpsClient('JTS:splitPolygon', { polygon: candidate, line: splitter });
        }
    }
    return false;
};

function SplitPoligonSuccessExecWpsClient(outputs, poly, splitter) {
    var features = new Array();
    if (outputs.result != "undefined") {
        for (i = 0; i < outputs.result.length; i++) {
            var res = outputs.result[i].geometry.toString().replace("POLYGON", "MULTIPOLYGON");
            features[i] = wktFormat.read(res);
            shtoObjLidhjeGisWebNeCol((i == 0 ? true : false), features[i]);
            wfsEdit.addFeatures(features[i]);
        }

        if (features && features.length > 0) {
            flashFeaturesSplit(features);
        }
    }
    Ext.getCmp("SplitButtonPolygonId").toggle(false);
};
//END Seksioni Split handlers

function SuccededCallbackTeDrejta(result) {
    if (result.d)
        result = result.d;
    if (result[0] == true) {
        window.open(result[1], '_blank');
        return;
    }
    else
        alert(result[1]);
}

function selektoButtonHandler(toggled, objekti) {
    if (!toggled)
        return;
    if (modifiko.feature) {
        modifiko.unselectFeature(modifiko.feature)
    }

    if (objekti.pressed) {
        caktivizoKontrolletAktivizoKontrollinEditim('', arrayKontrolletEditim)
        pastroFormEditimi();
        varSettings.editim.controlClickFromWmsLayer.activate();
        selekto.activate();
        clearSelected();
    }
    else {
        clearSelected();
        varSettings.editim.controlClickFromWmsLayer.deactivate();
        selekto.unselectAll();
        selekto.deactivate();
    }
}

function sketchcompleteEventHandler(event, responseTextPostgresCombo) {
    document.getElementById("rightClickMenuDiv").innerHTML = "";
    thirrSave = true;
    ShoqerojiObjektitTeDhenat();
    STATE = "INSERT";
    AfishoPopupInsertUpdate(event, 0, responseTextPostgresCombo, STATE);
};

function ndryshoSipasProjeksionitGoToXY(combo, record, index) {
    var projeksioni = $.grep(varSettings.webConfig.projeksione, function (e) { return e.DisplayCode == combo.getValue(); })[0];

    pastroFushatGoToXY();
    if (projeksioni.Unit == "metre") {
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[0].setValue(true);

        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[0].enable();
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[1].disable();
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[2].disable();
    }
    else {
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[1].setValue(true);

        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[0].disable();
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[1].enable();
        Ext.getCmp("PanelKoordinateItemsGoToXYId").items.items[2].enable();
    }

    Ext.getCmp("idPanelGoToXYProjectionCode").setText(projeksioni.DisplayCode);
    Ext.getCmp("idPanelGoToXYProjectionType").setText(projeksioni.Type);
    Ext.getCmp("idPanelGoToXYUnit").setText(projeksioni.Unit);
    Ext.getCmp("idPanelGoToXYCRS").setText(projeksioni.Crs);
    Ext.getCmp("idPanelGoToXYDatum").setText(projeksioni.Datum);
    Ext.getCmp("idPanelGoToXYEllipsoid").setText(projeksioni.Ellipsoid);
    Ext.getCmp("idPanelGoToXYMeridian").setText(projeksioni.PrimeMeridian);
}

function ndryshoSipasTipitGoToXY(tipiZgjedhur) { 
    switch (tipiZgjedhur) { 
        case '1':
            Ext.getCmp("PanelgoToXYIdDegree").setVisible(false);
            Ext.getCmp("PanelgoToXYId").setVisible(true);

            Ext.getCmp("PanelgoToXYId").setTitle(perkthe("GP_SHKOXYWIN_Metrike"));
            Ext.getCmp("VleraXGoToXY").label.update(perkthe("GP_SHKOXYWIN_MetrikeX") + ":");
            Ext.getCmp("VleraYGoToXY").label.update(perkthe("GP_SHKOXYWIN_MetrikeY") + ":");
            break;
        case '2':
            Ext.getCmp("PanelgoToXYIdDegree").setVisible(false);
            Ext.getCmp("PanelgoToXYId").setVisible(true);

            Ext.getCmp("PanelgoToXYId").setTitle('Decimal Degree');
            Ext.getCmp("VleraXGoToXY").label.update(perkthe("GP_SHKOXYWIN_GradeE") + ":");
            Ext.getCmp("VleraYGoToXY").label.update(perkthe("GP_SHKOXYWIN_GradeN") + ":");
            break;
        case '3':
            Ext.getCmp("PanelgoToXYId").setVisible(false);
            Ext.getCmp("PanelgoToXYIdDegree").setVisible(true);
            break;
    }
}

function pastroFushatGoToXY() {
    Ext.getCmp("PanelgoToXYId").getForm().reset();
    Ext.getCmp("PanelgoToXYIdDegree").getForm().reset();
}

function pozicionohuNeHarteHandler(toggled, menu, vectorAddPtToXY) {
    if (!toggled) {
        return;
    }
    var xValue, yValue;
    var fromProjection = Ext.getCmp("XYComboProjeksioneID").value;

    if (typeof Ext.getCmp("VleraXGoToXY").value != "undefined" && Ext.getCmp("VleraXGoToXY").value != '') {
        xValue = Ext.getCmp("VleraXGoToXY").value;
        yValue = Ext.getCmp("VleraYGoToXY").value;
        var pikaMenuTo = new OpenLayers.Geometry.Point(xValue, yValue).transform(new OpenLayers.Projection(fromProjection), new OpenLayers.Projection(projeksioniGeo));
        }
    else {
        xValue = fromDegreeToKartezian(Ext.getCmp("VleraXGoToXYDegree1").value, Ext.getCmp("VleraXGoToXYDegree2").value, Ext.getCmp("VleraXGoToXYDegree3").value);
        yValue = fromDegreeToKartezian(Ext.getCmp("VleraYGoToXYDegree1").value, Ext.getCmp("VleraYGoToXYDegree2").value, Ext.getCmp("VleraYGoToXYDegree3").value);
        if (typeof Ext.getCmp("VleraXGoToXYDegree1").value != "undefined" && Ext.getCmp("VleraXGoToXYDegree1").value != '')
            var pikaMenuTo = new OpenLayers.Geometry.Point(xValue, yValue).transform(new OpenLayers.Projection(fromProjection), new OpenLayers.Projection(projeksioniGeo));
        else
            return;
        //var textHistorik = Ext.getCmp("VleraXGoToXYDegree1").value + "\u00B0" + Ext.getCmp("VleraXGoToXYDegree2").value + "'" + Ext.getCmp("VleraXGoToXYDegree3").value + '"E' + " " + Ext.getCmp("VleraYGoToXYDegree1").value + "\u00B0" + Ext.getCmp("VleraYGoToXYDegree2").value + "'" + Ext.getCmp("VleraYGoToXYDegree3").value + '"N'
    }
    var textHistorik = xValue + ", " + yValue;
    var pika = new Array(xValue, yValue);

    //if (!eshtePikeBrendaPolygon(pika, varSettings.kufiShqiperi.WGS84)) {
    //    noty({ text: perkthe("GP_SHKOXYWIN_EnderValueAttention"), type: "info" }); //Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_SHKOXYWIN_EnderValueAttention"));
    //    return;
    //}

    switch (menu) {
        case "PAN":
            map.panTo(new OpenLayers.LonLat(pikaMenuTo.x, pikaMenuTo.y))
            var diffGjatesiaVijes;
            if (map.getScale() <= 17061)
                diffGjatesiaVijes = 20000;
            else
                diffGjatesiaVijes = 30000;
            break;
        case "ZOOM":
            map.setCenter(new OpenLayers.LonLat(pikaMenuTo.x, pikaMenuTo.y), map.getNumZoomLevels() - 1);
            var diffGjatesiaVijes = 10000;
            break;
        case "ADD":
            var pikaMenuToFea = new OpenLayers.Feature.Vector(pikaMenuTo);
            var diffGjatesiaVijes = 10000;
            shtoPozicionHarteNeHistorik(vectorAddPtToXY, pikaMenuTo, pikaMenuToFea, textHistorik);
            break;
        case "DRAW":
            map.setCenter(new OpenLayers.LonLat(pikaMenuTo.x, pikaMenuTo.y), map.getNumZoomLevels() - 1);
            ndertoPikenXY('ok', pika, projektioniKorGeo, projeksioniGeo);
            mbyllDritaregoToXY();
            break;
        default:
            break;
    }
    drawAnimatedLineAll(pikaMenuTo, diffGjatesiaVijes);
}

function eshtePikeBrendaPolygon(point, vs) {
    var x = point[0], y = point[1];

    var inside = false;
    for (var i = 0, j = vs.length - 1; i < vs.length; j = i++) {
        var xi = vs[i][0], yi = vs[i][1];
        var xj = vs[j][0], yj = vs[j][1];

        var intersect = ((yi > y) != (yj > y))
            && (x < (xj - xi) * (y - yi) / (yj - yi) + xi);
        if (intersect) inside = !inside;
    }

    return inside;
};

function krijoExtformTextField(id, i, lejobosh, etiketa, labelStyleCss, kolona) {
    return new Ext.form.TextField({
        xtype: 'textfield',
        width: 150,
        id: "" + id + "",
        name: "" + i + "",
        allowBlank: lejobosh,
        blankText: perkthe("GP_EDITIM_TEXTFIELDblankText"),
        fieldLabel: etiketa,
        labelStyle: labelStyleCss,
        listeners: {
            'change': function (textfield, newValue, oldValue) {
                switch (kolona) {
                    case "PERSHKRIMI":
                        varSettings.editim.objLidhjeGisWeb[kolona] = newValue;
                        break;
                    default:
                        varSettings.editim.objLidhjeGisWeb[kolona] = newValue;
                        break;
                }
            }
        }
    });
}

function krijoExtformTriggerField(id, i, lejobosh, etiketa, labelStyleCss, idLayerType, kolona) {
    return new Ext.form.TriggerField({
        width: 150,
        triggerAction: 'all',
        id: "" + id + "",
        name: "" + i + "",
        triggerClass: 'x-form-search-trigger',
        fieldLabel: etiketa,
        labelStyle: labelStyleCss,
        onTriggerClick: function (textfield, newValue, oldValue) {
            switch (kolona) {
                case "KODI":
                    var comboStatusNeEditim = Ext.getCmp("formEditimi").find("id", "NRSTATUSI" + varSettings.editim.objOpenLayer.id + "")[0];
                    if (typeof (comboStatusNeEditim) != "undefined" && comboStatusNeEditim.value != 3)
                        hapLupen(StatuseLayeri[0].WEBLLOJI, idLayerType, id);
                    break;
                default:
                    break;
            }
        },
        listeners: {
            'change': function (textfield, newValue, oldValue) {
                switch (kolona) {
                    case "KODI":
                        varSettings.editim.objLidhjeGisWeb[kolona] = newValue;
                        break;
                    default:
                        varSettings.editim.objLidhjeGisWeb[kolona] = newValue;
                        break;
                }
            }
        }
    });
}

function krijoExtformComboBox(id, i, lejobosh, etiketa, labelStyleCss, kolona) {
    var cmbStore = [];
    var valueField = '';
    var displayField = '';
    switch (kolona) {
        case "NRSTATUSI":
            var arrayStatusesh = new Array();
            for (var i = 0; i < StatuseLayeri.length; i++) {
                if (StatuseLayeri[i]["LLOJI"] == varSettings.editim.objLidhjeGisWeb.veprimi)
                    arrayStatusesh.push([StatuseLayeri[i]['id_layer'], StatuseLayeri[i]['LLOJI'], StatuseLayeri[i]['NRSTATUSI'], StatuseLayeri[i]['PERSHKRIMI']]);
            }
            valueField = 'NRSTATUSI';
            displayField = 'PERSHKRIMI';
            cmbStore = new Ext.data.ArrayStore({
                id: 0,
                fields: ['id_layer', 'LLOJI', 'NRSTATUSI', 'PERSHKRIMI'],
                data: arrayStatusesh
            });
            break;
        default:
            break;
    }
    return new Ext.form.ComboBox({
        width: 150,
        triggerAction: 'all',
        typeAhead: true,
        id: "" + id + "",
        name: "" + i + "",
        mode: 'local',
        store: cmbStore,
        valueField: valueField,
        displayField: displayField,
        fieldLabel: etiketa,
        labelStyle: labelStyleCss,
        listeners: {
            select: function (combo, record, index) {
                switch (kolona) {
                    case "NRSTATUSI":
                        ndryshoStatusHandler(combo.getValue(), record.get('id_layer'));
                        break;
                    default:
                        varSettings.editim.objLidhjeGisWeb[kolona] = newValue;
                        break;
                }
            }
        }
    });
}

function krijoExtformDateField(id, i, lejobosh, etiketa, labelStyleCss) {
    return new Ext.form.DateField({
        xtype: 'datefield',
        format: 'Y-m-d',
        width: 150,
        id: "" + id + "",
        name: "" + i + "",
        allowBlank: lejobosh,
        blankText: perkthe("GP_EDITIM_TEXTFIELDblankText"),
        fieldLabel: etiketa,
        labelStyle: labelStyleCss
    });
}

function krijoExtformNumberFieldMeDecimal(id, i, lejobosh, etiketa, labelStyleCss, width, decPrec, tabIndex) {
    return new Ext.form.NumberField({
        xtype: 'numberfield',
        width: width,
        id: "" + id + "",
        name: "" + i + "",
        allowBlank: lejobosh,
        decimalPrecision: decPrec,
        blankText: perkthe("GP_EDITIM_TEXTFIELDblankText"),
        fieldLabel: etiketa,
        labelStyle: labelStyleCss,
        tabIndex: tabIndex
    });
};

function krijoExtformNumberFieldPaDecimal(id, i, lejobosh, etiketa, labelStyleCss, width, value, minValue) {
    return new Ext.form.NumberField({
        xtype: 'numberfield',
        width: width,
        allowDecimals: false,
        id: "" + id + "",
        name: "" + i + "",
        allowBlank: lejobosh,
        blankText: perkthe("GP_EDITIM_TEXTFIELDblankText"),
        fieldLabel: etiketa,
        labelStyle: labelStyleCss,
        value: value,
        minValue: minValue
    });
};

function keyUpTextFieldIdLidheseGis(e, t, o, objekti, idEventOpenLayer, emerKolone) {
    if (objekti.getValue()) {
        if (emerKolone == emerKoloneIdLidheseGis) {
            merrTeDhenatHyresePerObjektGis(objekti.getValue(), idEventOpenLayer);
        }
        else if (emerKolone == emerKoloneIdLidheseGisZ) {
            merrTeDhenatPerZonatPerZoneId(objekti.getValue(), idEventOpenLayer);
        }
    }
    else {
        bejBoshFushatMeTeDhenaNgaJashte(idEventOpenLayer)
    }
};

function hapRaportin(identifikuesRpt, filtratDefault) {
    var width = $(window).width();
    window.open("Raporti.aspx?" + identifikuesRpt + "&vjenNga=GIS&printo=false&windowWidth=" + width + "&" + filtratDefault + "&scopeID=" + Utils.getUrlVar("scopeID"), '_blank');
}

function PopupCloseUp(s, e) {
}

function PopupCloseUp2(s, e) {
    neFundKerkimiMbyllDritare();
    if (typeof varSettings.kerkimHapsinor.polygonLayer != "undefined") {
        varSettings.kerkimHapsinor.polygonLayer.setVisibility(false)
    }
    return;
}

function neFundKerkimiPozicionoRezultate(rezultatet, rezultateLayerID, llojKerkimi) {
    var layerParams;
    if (typeof rezultatet != "undefined" && rezultatet.length > 0) {
        if (llojKerkimi == 'H') {
            shkoTekRezultateKerkimi(rezultatet);
            layerParams = ' (IDAUTORIZUESI=' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI=-1) ';
            shfaqRezultateKerkimi(rezultatet, "Rezultate Kerkimi", layerParams);
        }
        else {
            if (rezultatet[0].NrStatusi != -1) {
                layerParams = ' (IDAUTORIZUESI=' + varSettings.webConfig.idPerdoruesi + ' OR IDAUTORIZUESI=-1) AND GRUPO = ' + rezultatet[0].Grupo;
                shfaqRezultateKerkimi(rezultatet, "Rezultate Kerkimi", layerParams);
            }
            else {
                layerParams = ' IDLAYER = ' + rezultateLayerID[0];
                shfaqRezultateKerkimi(rezultatet, "Rezultate Kerkimi 2", layerParams);
            }
            shkoTekRezultateKerkimi(rezultatet);
        }
    }
};

function shkoTekRezultateKerkimi(rezultatet) {
    var rezultateGeomPerLayer = rezultatet.map(function (value, index) { return value.the_geom; });

    var tempVektorKerkimi = new OpenLayers.Layer.Vector("TempLayerKerkimiVektor", { isBaseLayer: false });
    var featuresKerkimi = rezultateGeomPerLayer.map(function (value, index) {
        var feature = new OpenLayers.Format.WKT().read(value);
        var featureProjected = feature.geometry.transform(new OpenLayers.Projection(projeksioniGeoV), new OpenLayers.Projection(projeksioniGeo));
        return new OpenLayers.Feature.Vector(featureProjected);
    });
    tempVektorKerkimi.addFeatures(featuresKerkimi);
    map.zoomToExtent(tempVektorKerkimi.getDataExtent());
}

function shfaqRezultateKerkimi(rezultatet, layerKerkimi, layerParams) {
    var rezultateGidPerLayer = rezultatet.map(function (value, index) { return value.gid; });

    var layerParamsAll = layerParams + ' and gid in (' + rezultateGidPerLayer.toString() + ')';
    shfaqLayerTempMeParametra(layerKerkimi, { "CQL_FILTER": layerParamsAll }, true, true);
}

function neFundKerkimiMbyllDritare() {
    shfaqLayerTempMeParametra("Rezultate Kerkimi", { "CQL_FILTER": ' 1=1 ' }, false, true);
    shfaqLayerTempMeParametra("Rezultate Kerkimi 2", { "CQL_FILTER": ' 1=1 ' }, false, true);
}

function plotesoTeDhenaPaneliUpdateInsert() {
    for (i = 0; i < KolonatAfishimEditimGJ.length; i++) {
        var kolonaPerAfishim = KolonatAfishimEditimGJ[i];
        var textFieldEditim = varSettings.editim.objPanel.frmeditim.find("id", "" + kolonaPerAfishim.EmerKolona + varSettings.editim.objOpenLayer.id + "")[0];
        if (varSettings.editim.objLidhjeGisWebAfishim[kolonaPerAfishim.EmerKolona] != null && textFieldEditim != null && typeof varSettings.editim.objLidhjeGisWebAfishim[kolonaPerAfishim.EmerKolona] != "undefined" && typeof textFieldEditim != "undefined")
            textFieldEditim.setValue(varSettings.editim.objLidhjeGisWebAfishim[kolonaPerAfishim.EmerKolona]);
    }
}

function ndryshoStatusHandler(newStatus, newIdLayer) {
    if (varSettings.editim.objLidhjeGisWeb.NRSTATUSI.toString() !== newStatus.toString()) {
        if ((varSettings.editim.objLidhjeGisWeb.NRSTATUSI.toString() == "1" || varSettings.editim.objLidhjeGisWeb.NRSTATUSI.toString() == "2") && newStatus.toString() == "3") {
            Ext.MessageBox.show({
                title: perkthe("GP_EDITIM_WIN_TIT"),
                msg: perkthe("GP_EDITIM_MSG_MSG_NDRYSHOSTATUS"),
                buttons: Ext.MessageBox.OK,
                fn: function (btn) { YESNONdryshoStatus(btn, newStatus, newIdLayer) },
                icon: Ext.MessageBox.QUESTION
            });
        }
        else
            ndryshoStatusin(newStatus, newIdLayer);
    }
}

function YESNONdryshoStatus(btn, newStatus, newIdLayer) {
    // rastet yes dhe no duheshin ne fillim per rastet kur 
    if (btn == "yes") {
        varSettings.editim.objLidhjeGisWeb.KODI = '';
        Ext.getCmp("formEditimi").find("id", "KODI" + varSettings.editim.objOpenLayer.id + "")[0].setValue('');
        varSettings.editim.objLidhjeGisWeb.PERSHKRIMI = '';
        Ext.getCmp("formEditimi").find("id", "PERSHKRIMI" + varSettings.editim.objOpenLayer.id + "")[0].setValue('');
        varSettings.editim.objLidhjeGisWeb.SERIALKOD = '';
        if (Ext.getCmp("formEditimi").find("id", "SERIALKOD" + varSettings.editim.objOpenLayer.id + "")[0])
            Ext.getCmp("formEditimi").find("id", "SERIALKOD" + varSettings.editim.objOpenLayer.id + "")[0].setValue('');
        varSettings.editim.objLidhjeGisWeb.KODKODIFIKIMI = '';
        if (Ext.getCmp("formEditimi").find("id", "KODKODIFIKIMI" + varSettings.editim.objOpenLayer.id + "")[0])
            Ext.getCmp("formEditimi").find("id", "KODKODIFIKIMI" + varSettings.editim.objOpenLayer.id + "")[0].setValue('');
        varSettings.editim.objLidhjeGisWebAfishim.ADRESA = '';
        if (Ext.getCmp("formEditimi").find("id", "ADRESA" + varSettings.editim.objOpenLayer.id + "")[0])
            Ext.getCmp("formEditimi").find("id", "ADRESA" + varSettings.editim.objOpenLayer.id + "")[0].setValue('');
        varSettings.editim.objLidhjeGisWeb.IDMAGAZINA = 0;
        varSettings.editim.objLidhjeGisWeb.IDKODIFIKIMI = 0;
        varSettings.editim.objLidhjeGisWeb.IDARTIKULLI = 0;
        varSettings.editim.objLidhjeGisWeb.IDSERIALI = 0;
        varSettings.editim.objLidhjeGisWeb.IDKOKADOK = 0;
        varSettings.editim.objLidhjeGisWeb.IDTRUPIDOKLIDHES = 0;
        ndryshoStatusin(newStatus, newIdLayer);
    }

    if (btn == "no" || btn == "ok") {
        Ext.getCmp("formEditimi").find("id", "NRSTATUSI" + varSettings.editim.objOpenLayer.id + "")[0].setValue(varSettings.editim.objLidhjeGisWeb.NRSTATUSI);
    }
}

function ndryshoStatusin(newStatus, newIdLayer) {
    varSettings.editim.objLidhjeGisWeb.NRSTATUSI = newStatus;
    varSettings.editim.objLidhjeGisWeb.IDLAYER = newIdLayer
    $.extend(varSettings.editim.objLidhjeGisWebAfishim, varSettings.editim.objLidhjeGisWeb);
    $.ajax({
        url: Utils.getServerApiUrl("GIS", "getRowsForEditWindow"),
        data: JSON.stringify({ layerType: varSettings.editim.objLidhjeGisWeb.IDLAYERSTYPE, statusi: newStatus })
    }).done(function (result) {
        KolonatAfishimEditimGJ = result.tempLC;
        StatuseLayeri = JSON.parse(result.statuset);
        TipiPikeVijePoligon = KolonatAfishimEditimGJ[0]["ObjType"];
        krijoPopupEditimInsert(eventInserUpdateAtribute, varSettings.responseComboListAtr, '', varSettings.editim.objLidhjeGisWeb.veprimi);
    });
}

//Artifice per DPSH  setTimeout(myFunction, 3000)
function ndryshoStilLayer() {
    setInterval(function () {
        var layerPerRifreskim = map.getLayersBy("IdLayer", varSettings.ndryshoStilPerLayer.idLayer)[0];
        if (layerPerRifreskim.visibility) {
            if ((varSettings.ndryshoStilPerLayer.count % 2) == 0)
                layerPerRifreskim.params.STYLES = varSettings.ndryshoStilPerLayer.style1;
            else
                layerPerRifreskim.params.STYLES = varSettings.ndryshoStilPerLayer.style2;
            varSettings.ndryshoStilPerLayer.count++;
            layerPerRifreskim.redraw(true);
        }
    }, varSettings.ndryshoStilPerLayer.interval);
};
