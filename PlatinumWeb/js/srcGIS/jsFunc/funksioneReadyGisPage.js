var map, extent, harte, toolbarItems = [];
var mapPanel;
var harteUI;
var myRedraw = 0;

Ext.onReady(function () {
    Utils.PushToGoogleAnalytics(hfState.Get("googleAnalytics"), hfState.Get("googleAnalyticsTrackingId"))
    OpenLayers.DOTS_PER_INCH = 90.71428571428572;
    gisElements = new GISElements();
    gisEditimImport = new clsGISEditimImport();
    harte = new Harte({});
    map = harte.map;

    extent = new OpenLayers.Bounds(2144439.665956, 4814471.53712596, 2344093.456301, 5260548.63642133); //rregulloji jo hardcode

    harte.variableGlobaleNgaHiddenField();
    harte.konfigurimeDefaultExt({});

    harte.shtoMAPControlLoadingPanel({});
    harte.shtoMAPLayers(varSettings.webConfig.layers);
    if (typeof google === 'object' && typeof google.maps === 'object' && map.getLayersBy("IDENTIFICATION", "google.SATELLITE")[0] != undefined)
        var GmapLayerLoadListener = google.maps.event.addListener(map.getLayersBy("IDENTIFICATION", "google.SATELLITE")[0].mapObject, 'tilesloaded', function () { if ($('.gm-style')) { $('.gm-style').removeClass(); google.maps.event.removeListener(GmapLayerLoadListener); } })
    
    harte.shtoMAPControlGraticuleXYT({});
    harte.shtoMAPControlLayerSwitcher({});
    harte.shtoMAPControlOverviewMap({});
    harte.shtoMAPControlMAPControlScaleLine({});
    harte.shtoMAPControlMousePosition({});
    harte.shtoMAPControlNavigationHistory({});

    varSettings.measure.merrKoordinata = new OpenLayers.Layer.Vector("MAPTempLayerForPointMeasure", { styleMap: measureStyleMap });
    map.addLayer(varSettings.measure.merrKoordinata);
   
    //Merret nga js file GISMenuLart
    KrijoMenuLart();

    mapPanel = new GeoExt.MapPanel({
        region: "center",
        id: "mappanel",
        xtype: "gx_mappanel",
        map: map,
    });
    harte.settings.idMappanel = "mappanel";
    var treeRoot = new Ext.tree.TreeNode({
        text: perkthe("GP_TREENODEPAN_TIT"),
        expanded: true,
        isTarget: false,
        allowDrop: false
    });
    
    OpenLayers.Control.DragPan.prototype.interval = 0;

    var options = {
        paneliLayers: {
            foldersLayers: varSettings.webConfig.treeStructure,
            treeRoot: treeRoot,
            mapPanel: mapPanel,
            idFolderContainerVersioni1: idFolderContainerVersioni1,
            idFolderContainerVersioni2: idFolderContainerVersioni2,
            titulliFolderContainerVersioni1: titulliFolderContainerVersioni1,
            titulliFolderContainerVersioni2: titulliFolderContainerVersioni2,
            titulliLayerAktiv1: titulliLayerOverLayerInfo46,
            titulliLayerAktiv2: titulliLayerOverLayerInfo39,
            treeRoot: treeRoot,
            idGroupDisableFirst: idFolderContainerVersioni2
        },
        paneliKerkimit: {
            idPanelKerkimi: "panelKerkimJugId",
            item1Id: "kerkimFiltersPanelId",
            idComboLayersFilter: "kerkimLayersComboId",
            paneliMeButonatKerkimId: 'paneliMeButonatKerkimId'
        }
    };

    harteUI = new HarteUI(options);

    var lookupFolders = {};
    for (var i = 0, len = varSettings.webConfig.treeStructure.length; i < len; i++) {
        lookupFolders[varSettings.webConfig.treeStructure[i].FolderName] = varSettings.webConfig.treeStructure[i];
    }
    tree = new GeoExt.ux.tree.LayerTreeBuilder({ //trashegon nga Ext.Tree.TreePanel
        id: "MenuLayersTree",
        region: 'center',
        collapsible: false,
        split: false,

        autoScroll: true,
        rootVisible: false,
        enableDD: true,
        lines: false,
        wmsLegendNodes: false,
        vectorLegendNodes: true,

        cls: 'x-tree-noicon',
        bodyStyle: "padding:5px;",
        title: "",
        selModel: new Ext.tree.DefaultSelectionModel({
            id: "MenuLayersTreeSelectionModel",
            listeners: {
                scope: this
            }
        }),
        listeners: {
            append: function (tree, parent, node, index) {
                if (lookupFolders[node.text] && node.text == lookupFolders[node.text].FolderName && lookupFolders[node.text].Expand == 1) {
                    node.expanded = true;
                }
            },
            beforeappend: function (tree, parent, node) {
                if (node.text == tree.otherLayersText) {
                    return false;
                }
            },
            contextmenu: function (node, e) {
                if (node && node.layer) {
                    node.select();
                    var c = node.getOwnerTree().contextMenu;
                    c.contextNode = node;
                    c.showAt(e.getXY());
                    Ext.getCmp('opacitySliderID').setLayer(node.layer);
                    if (node.layer.url_metadata && node.layer.url_metadata != '') {
                        if (Ext.getCmp("butonMetadataId")) {
                            Ext.getCmp("butonMetadataId").handler = function () {
                                var url = node.layer.url_metadata.replace(pjesaReplaceGeonetwork, adresaGeonetwork);
                                window.open(url, "_blank")
                            }
                        }
                    } else {
                        if (Ext.getCmp("butonMetadataId")) {
                            Ext.getCmp("butonMetadataId").handler = function () {
                                Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_TEXT_NoMetadata"))
                            }
                        }
                    }
                    if (Ext.getCmp("refreshLayerIdPanelBtn")) {
                        Ext.getCmp("refreshLayerIdPanelBtn").handler = function () {
                            node.layer.redraw(true)
                        }
                    }
                }
            },
            beforemovenode: function (tree, node, oldParent, newParent, index) {
                // change the group when moving to a new container
                if (oldParent !== newParent) {
                    var store = newParent.loader.store;
                    var index = store.findBy(function (r) {
                        return r.get("layer") === node.layer;
                    });
                    var record = store.getAt(index);
                    //   record.set("group", newParent.attributes.group);
                    // record.set.group=newParent.attributes.group;
                    record.getLayer().group = newParent.attributes.group;
                }
            },
            scope: this
        },
        contextMenu: new Ext.menu.Menu({
            items: [{
                xtype: 'panel',
                title: perkthe("GP_LAYERPAN_MEN_TIT_TRANS"),
                id: "panelOpacityLayer",
                cls: 'opacityLayerPanel',
                layout: "fit",
                width: 200,
                cls: 'popPanel' + varSettings.theme.color,
                items: [{
                    xtype: "gx_opacityslider",
                    id: "opacitySliderID",
                    text: perkthe("GP_LAYERPAN_MEN_TIT_TRANS"),
                    aggressive: true,
                    vertical: false,
                    height: 20,
                    changeVisibility: true,
                    x: 10,
                    y: 20,
                    plugins: new GeoExt.LayerOpacitySliderTip({
                        template: perkthe("GP_OPACITY_SLIDER_PERC") + " {opacity}%"
                    })
                }]
            }, {
                xtype: 'panel',
                id: "refreshLayerIdPanel",
                layout: "fit",
                width: 200,
                height: 30,
                cls: 'popPanel' + varSettings.theme.color,
                items: [{
                    xtype: "button",
                    id: "refreshLayerIdPanelBtn",
                    text: perkthe("refreshLayerBtnText"),
                    handler: function () { }
                }]
            }]
        }),
        filterBy: function (fn, scope) {
            scope = scope || this;
            function applyFilter(node) {
                var out = [];
                Ext.each(node.childNodes, function (child) {
                    if (fn.call(scope, child)) {
                        applyFilter(child);
                    } else {
                        // we can't remove child right away, that would
                        // kill the loop
                        out.push(child);
                    }
                });
                Ext.each(out, function (child) {
                    // destroy, and suppressEvent
                    node.removeChild(child, true, true);
                });
            }
            applyFilter(this.getRootNode());
        },
    });

    legendpanel = new GeoExt.LegendPanel({
        layerStore: mapPanel.layers,
        title: perkthe("GP_LEGPAN_TIT_Shpjegues"),
        id: 'paneliIlegjendes',
        plugins: [Ext.ux.PanelCollapsedTitle],
        dynamic: true,
        autoScroll: true,
        region: 'south',
        height: 270,
        minSize: 75,
        maxSize: 800,
        cmargins: '10 10 10 10',
        collapsible: true,
        split: true,
        filter: function (record) {
             return ((record.getLayer().DEFAULTOBJEKT && record.getLayer().DEFAULTOBJEKT.DISPLAYLEGEND) || (record.getLayer().DISPLAYLEGEND)); // && (record.getLayer().DEFAULTOBJEKT.MINSCALE < map.getScale() && record.getLayer().DEFAULTOBJEKT.MAXSCALE > map.getScale()));
        },
        defaults: {
            style: 'padding:5px',
            baseParams: {
                LEGEND_OPTIONS: 'forceLabels:on'
            }
        }
    });


    var panelKerkimItems = 
        [{
            xtype: 'panel',
            id: "paneliMbajtesKerkimiId",
            width: '20%',
            region: 'west',
            collapsible: true,
            collapseMode: 'mini',
            header: false,
            split: true,
            bodyStyle: 'padding:2% 2% 2% 2%;', //top right bottom left
            cls: 'popPanel' + varSettings.theme.color,
            layout: "border",
            items: [{
                xtype: 'panel',
                id: harteUI.settings.paneliKerkimit.paneliMeButonatKerkimId,
                bbar: [],
                region: 'north',
                header: false,
                bodyStyle: 'padding:2% 2% 2% 2%;', //top right bottom left
                layout: {
                    type: 'table',
                    columns: 4
                },
                cls: 'popPanel' + varSettings.theme.color,
                items: []
            }, {
                xtype: 'panel',
                id: harteUI.settings.paneliKerkimit.item1Id,
                region: 'center',
                header: false,
                bodyStyle: 'padding:2% 1% 2% 1%;',
                layout: 'table',
                layoutConfig: {
                    columns: 3
                },
                cls: 'popPanel' + varSettings.theme.color,
                defaults: {
                    frame: true,
                    width: 120
                }
            }]
        }, {
            xtype: 'panel',
            id: 'grideKerkimPanel',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            cls: 'popPanel' + varSettings.theme.color,
            region: 'center',
            title: perkthe("GP_KERKIMTABPANREZ_GRID_TIT"),
            items: []
        }];
    PanelKerkim = gisElements.createGISElement("Panel", { id: harteUI.settings.paneliKerkimit.idPanelKerkimi, hidden: true, region: 'south', layout: 'border', header: false, height: 300, split: true, collapseMode: 'mini', minHeight: 100, items: panelKerkimItems });

    //if (arrayButonat.indexOf(3) > -1) {
    //    LayerStoreKerkim = new Ext.data.JsonStore({
    //        proxy: new Ext.data.HttpProxy({
    //            url: 'WebService_GIS.asmx/merrLayerskerkim',
    //            headers: {
    //                'Content-type': 'application/json'
    //            }
    //        }),
    //        root: 'd',
    //        fields: ["IdLayer", "Title", "ObjPerKerkim"],
    //        listeners: {
    //            loadexception: function () { }
    //        }
    //    });
    //    LayerStoreKerkim.load();
    //    var kerkimLayersCombo = new Ext.form.ComboBox({
    //        store: LayerStoreKerkim,
    //        displayField: 'Title',
    //        valueField: 'ObjPerKerkim',
    //        typeAhead: true,
    //        width: 120,
    //        mode: 'local',
    //        id: harteUI.settings.paneliKerkimit.idComboLayersFilter,
    //        forceSelection: true,
    //        triggerAction: 'all',
    //        emptyText: perkthe("GP_KERKIMTABPAN_COMBO_TIT"),
    //        selectOnFocus: true,
    //        listeners: {
    //            select: function (combo, record, index) {
    //                KtheKolonat(combo.getValue());
    //            }
    //        }
    //    });
    //    if (Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id)) {
    //        var options = {}
    //        harteUI.krijoFormenFiltersKerkim(options);
    //        //Ext.getCmp('kerkimFiltersPanelId').add(kerkimLayersCombo);
    //        //Ext.getCmp('kerkimFiltersPanelId').doLayout();
    //    }
    //    var kerkimHapLayersCombo = new Ext.form.ComboBox({
    //        store: LayerStoreKerkim,
    //        displayField: 'title',
    //        valueField: 'layerValues',
    //        typeAhead: true,
    //        mode: 'local',
    //        id: "kerkimHapLayersComboId",
    //        forceSelection: true,
    //        listWidth: 'auto',
    //        triggerAction: 'all',
    //        emptyText: perkthe("GP_KERKIMTABPAN_COMBO_TIT"),
    //        selectOnFocus: true,
    //        colspan: 4,
    //        listeners: {
    //            select: function (combo, record, index) {
    //                KtheKolonatKerkimHapesinor(combo.getValue());
    //            }
    //        }
    //    });
    //    kerkimHapLayersCombo.setValue('{"layer":"bosh","url_servicewfs":"","workspace_name":"","layer_title":"","url_servicewms":""}');
    //    if (Ext.getCmp("kerkimhtmlPanelHapesinor")) {
    //        Ext.getCmp("kerkimhtmlPanelHapesinor").add(kerkimHapLayersCombo);
    //        Ext.getCmp("kerkimhtmlPanelHapesinor").doLayout();
    //    }
    //}

    viewport = new Ext.Viewport({
        layout: 'border',
        id: "mainview",
        hideBorders: true,
        items: [{
            region: "center",
            xtype: 'panel',
            layout: 'border',
            header: false,
            id: "paneliKryesorID",
            cls: 'popPanel' + varSettings.theme.color,
            tbar: new Ext.Toolbar({
                cls: 'toolbarKryesor' + varSettings.theme.color,
                items: toolbarItems
            }),
            items: [{
                xtype: 'panel',
                id: 'panelLayer',
                title: perkthe("GP_TITLE_PAN_layerave"),
                plugins: [Ext.ux.PanelCollapsedTitle],
                bodyStyle: 'padding: 10px 10px 10px 10px;',
                region: 'west',
                collapsible: true,
                split: true,
                width: '15%',
                layout: 'border',
                cls: 'popPanel' + varSettings.theme.color,
                items: [tree, legendpanel]
            }, {
                width: 600,
                xtype: 'panel',
                layout: "border",
                region: 'center',
                id: "paneliHartesId",
                cls: 'popPanel' + varSettings.theme.color,
                items: [mapPanel],
                //html: '<div id="copyRight" class="copyRight' + varSettings.theme.color + '">'
            },
                PanelKerkim
            ]
        }]
    });
    mapPanel = Ext.getCmp("mappanel");
    panelLayer = Ext.getCmp("panelLayer");
    //document.getElementById("copyRight").innerHTML = "&copy " + perkthe("GP_emriMinistrise_text");
    shfaqDivElementSipasRastit("njoftimeRezultate", false, "", "", "");

    if (arrayButonat.indexOf(4) > -1) {
        var editimLayers = varSettings.webConfig.layers;
        var tempLayerStore = editimLayers.filter(function (layers) {
            return (layers.IDGRSTRUCTURE == 2 && (layers.D_SHTIM || layers.D_MOD || layers.D_FSH));
        }).sort(dynamicSortMultiple("IDLAYERSTYPE", "NRSTATUSI")).map(function (layer, index) {
            layer = { IDENTIFICATION: layer.IDENTIFICATION, DESCRIPTION: layer.DESCRIPTION, IDLAYER: layer.IDLAYER, IDLAYERSTYPE: layer.IDLAYERSTYPE, NRSTATUSI: layer.NRSTATUSI, DISPLAYLEGEND: layer.DISPLAYLEGEND };
            return layer;
        });

        varSettings.editim.cmbLayesStore = new Ext.data.JsonStore({
            fields: ["IDENTIFICATION", "DESCRIPTION", "IDLAYER", "IDLAYERSTYPE", "NRSTATUSI"],
            data: tempLayerStore,
            paging: false
        });
    };

    map.zoomToExtent(extent);

    /*tree.getNodeById("layercontainer_1032").ui.toggleCheck(true);
    tree.getNodeById("layercontainer_1033").ui.toggleCheck(false);
    tree.getNodeById("layercontainer_1033").eachChild(function(child) {
        child.ui.toggleCheck(false);
        child.disable();
    });*/

    document.title = perkthe("GP_TITPAGE");
    tree.filterBy(function (record) {
        return (record.text != this.baseLayersText && record.text != this.otherLayersText);
    });

   //if (varSettings.webConfig.ndermarrjeLogin.NdermarrjeKodi == "DPSH")
   //     ndryshoStilLayer(540, 'V_GIS_Layer_DPSH_RASTEKB_NR1', 'V_GIS_Layer_DPSH_MONITORIMIAJROR_NR1');
    //Ext.util.Observable.capture(Ext.getCmp("MenuLayersTree"), function (evname) { console.log(evname, arguments); })
});