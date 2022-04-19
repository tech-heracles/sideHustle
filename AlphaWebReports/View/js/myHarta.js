;
var Harta = function (options) {
    this.ngarkoHarten(options);
    this.afishoNeHarte(options);
}
Harta.prototype = {
    vektoriMePikat: null,
    layerSwitch: null,
    map: null,
    settings: null,
    defaults: {
        DOTS_PER_INCH: 96,
        option: {
            div: 'map',
            projection: "EPSG:900913",
            units: 'm'
        },
        drawPoint: true,
        deletePoints: true,
        devControlToStorePoint: null,
        devControlToStorePointKey: null,
        devControlToShowPointCoordinates: null,
        devControlToShowPointCoordinatesParent: null,
        projectionFrom: "EPSG:4326"
    },
    ngarkoHarten: function (options) {
        options = typeof options !== "undefined" ? options : {};
        this.settings = $.extend(true, this.defaults, options);
        OpenLayers.DOTS_PER_INCH = this.settings.DOTS_PER_INCH;//90.71428571428572;  //sdfsdf
        var extent = new OpenLayers.Bounds(1878516.4068749994, 4383204.949375, 2504688.5425000004, 5635549.220624998); // 0,0,x,y
        var mapCenter = new OpenLayers.LonLat(2224085.168137983, 5057241.075681678);
       

        this.map = new OpenLayers.Map(this.settings.option.div, this.settings.option);

        var hibrideGooglemap = new OpenLayers.Layer.Google("Harta hibride", { type: google.maps.MapTypeId.HYBRID, disableDefaultUI: false, numZoomLevels: 22, visibility: true, isBaseLayer: true, displayInLayerSwitcherBase: true });
        this.map.addLayer(hibrideGooglemap);
        var rrugetGooglemap = new OpenLayers.Layer.Google("Harta e rrugeve", { type: google.maps.MapTypeId.ROADMAP, disableDefaultUI: false, numZoomLevels: 22, visibility: false, isBaseLayer: true, displayInLayerSwitcherBase: true });
        this.map.addLayer(rrugetGooglemap);
        var satelitoreGooglemap = new OpenLayers.Layer.Google("Harta satelitore", { type: google.maps.MapTypeId.SATELLITE, disableDefaultUI: false, numZoomLevels: 22, visibility: false, isBaseLayer: true, displayInLayerSwitcherBase: true });
        this.map.addLayer(satelitoreGooglemap);
        var fizikeGooglemap = new OpenLayers.Layer.Google("Harta fizike", { type: google.maps.MapTypeId.TERRAIN, disableDefaultUI: false, numZoomLevels: 22, visibility: false, isBaseLayer: true, displayInLayerSwitcherBase: true });
        this.map.addLayer(fizikeGooglemap);
        
        //var layerSwitch = new OpenLayers.Control.LayerSwitcher();
        this.layerSwitch = new OpenLayers.Control.LayerSwitcher();
        
        this.map.addControl(this.layerSwitch);

        stiliPikes = new OpenLayers.StyleMap({
            "default": new OpenLayers.Style({
                strokeColor: "red",
                fillColor: "red",
                strokeWidth: 2,
                strokeOpacity: 1,
                fillOpacity: 0.7,
                pointRadius: 5
            })
        });
        this.vektoriMePikat = new OpenLayers.Layer.Vector("Shperndarja e pikave", {
            styleMap: stiliPikes, displayInLayerSwitcher: false
        });

        this.map.addLayer(this.vektoriMePikat);

        var stiliPikesNeMouse = {
            pointRadius: 5,
            graphicName: "circle",
            fillColor: "#7fda9e",
            fillOpacity: 1,
            strokeWidth: 1,
            strokeOpacity: 1,
            strokeColor: "#7fda9e"
        }
        var drawPoint;
        if (this.settings.drawPoint) {
            var vektoriMePikat = this.vektoriMePikat;
            var myMapSettings = this.settings;
            drawPoint = new OpenLayers.Control.DrawFeature(
                this.vektoriMePikat, OpenLayers.Handler.Point,
                {
                    displayClass: "olControlModifyFeature",
                    map: this.map,
                    handlerOptions: { 'style': stiliPikesNeMouse },
                    eventListeners: {
                        'activate': function () {
                            vektoriMePikat.events.register('sketchcomplete', vektoriMePikat, function (event) {
                                vektoriMePikat.removeFeatures(vektoriMePikat.features);
                                var pikaGrade = event.feature.geometry.clone();
                                pikaGrade.transform(myMapSettings.option.projection, myMapSettings.projectionFrom);
                                if (myMapSettings.devControlToStorePoint)
                                    myMapSettings.devControlToStorePoint.Set(myMapSettings.devControlToStorePointKey, JSON.stringify((["POINT (" + pikaGrade.x.toFixed(6) + " " + pikaGrade.y.toFixed(6) + ")"])));
                                if (myMapSettings.devControlToShowPointCoordinates) 
                                    myMapSettings.devControlToShowPointCoordinates.SetText(pikaGrade.y.toFixed(6) + ", " + pikaGrade.x.toFixed(6));
                                if (myMapSettings.devControlToShowPointCoordinatesParent) 
                                    myMapSettings.devControlToShowPointCoordinatesParent.SetText(pikaGrade.y.toFixed(6) + ", " + pikaGrade.x.toFixed(6));
                            });
                        },
                        'deactivate': function () {
                            vektoriMePikat.events.remove('sketchcomplete');
                        }
                    }
                });
            this.map.addControl(drawPoint);
        }
        var panel = new OpenLayers.Control.Panel({
            displayClass: 'customEditingToolbar',
            allowDepress: true
        });
        //panel.addControls([drawPoint]);
        this.map.addControl(panel);
        //  this.map.zoomToExtent(extent);
        this.map.setCenter(mapCenter,5);

        var featureSelected = new Array();
        var addSelected = this.addSelected;
        var clearSelected = this.clearSelected;
        var selektoControl = new OpenLayers.Control.SelectFeature(
                this.vektoriMePikat,
                {
                    // clickout: false,
                    toggle: false,
                    multiple: false, hover: false,
                    toggleKey: "ctrlKey", // ctrl key e heq nga selektimi
                    multipleKey: "shiftKey", // shift key selekton me shume se 1 objekt
                    onSelect: function (feature) {
                        addSelected(feature, featureSelected)
                    },
                    onUnselect: function (feature) {
                        clearSelected(feature, featureSelected)
                    },
                    eventListeners: {
                        'activate': function () {

                        },
                        'deactivate': function () {
                            clearSelected();
                        }
                    }
                }
        )
        this.map.addControl(selektoControl);
        selektoControl.activate();
        var deletePoints;

        deletePoints = new OpenLayers.Control.Button({
            title: "Fshij",
            trigger: function () {
                vektoriMePikat.removeFeatures(featureSelected)
            },
            //   displayClass: "olControlSaveFeatures"
            displayClass: "olControlButton1",
            type: OpenLayers.Control.TYPE_BUTTON
        });

        var panel = new OpenLayers.Control.Panel({
            displayClass: 'customEditingToolbar',
            allowDepress: true
        });
        var arrayOfControls = new Array();
        if (this.settings.drawPoint)
            arrayOfControls.push(drawPoint);
        if (this.settings.deletePoints)
            arrayOfControls.push(deletePoints);
        if (arrayOfControls.length > 0)
            panel.addControls(arrayOfControls);
        this.map.addControl(panel);
    },
    afishoNeHarte: function (options) {
        options = typeof options !== "undefined" ? options : {};
        // Merge defaults and options, without modifying defaults
        this.settings = $.extend(true, this.defaults, options);
        if (this.settings.devControlToStorePoint) {
            var koordinatat = this.settings.devControlToStorePoint.Get(this.settings.devControlToStorePointKey);                        
            if (koordinatat) {
                koordinatat = $.parseJSON(koordinatat);
                for (var i = 0; i < koordinatat.length; i++) {
                    koordinata = koordinatat[i];
                    if (!koordinata) {
                        continue;
                    }
                    var pika = new OpenLayers.Format.WKT().read(koordinata);
                    if (this.settings.devControlToShowPointCoordinates && koordinatat.length == 1) {
                        this.settings.devControlToShowPointCoordinates.SetText(pika.geometry.y + ', ' + pika.geometry.x);
                    }
                    pika.geometry.transform(this.settings.projectionFrom, this.settings.option.projection);

                    this.vektoriMePikat.addFeatures(pika);
                    this.map.setCenter(new OpenLayers.LonLat(pika.geometry.x, pika.geometry.y), 17);
                }
                if (koordinatat.length > 1) {
                    this.map.zoomToExtent(this.vektoriMePikat.getDataExtent());
                }
            }
        }
        this.layerSwitch.maximizeControl();
    },
    addSelected: function (feature, vekt) {
        vekt.push(feature);
    },
    clearSelected: function (feature, vekt) {
        for (var i = 0; i < vekt.length; i++) {
            vekt.pop();
        }
    },
    ndryshoPikeNeHarte: function () {
        var koordinatat = this.settings.devControlToShowPointCoordinates.GetText().split(', ');
        if (koordinatat.length != 2) {
            alert('Formati i kordinatave duhet te jete: "x.x, y.yy" - pra te ndara me ", "');
        }
        var koordinatePike = 'POINT (' + koordinatat[1] + ' ' + koordinatat[0] + ')';
        if (this.settings.devControlToStorePoint)
            this.settings.devControlToStorePoint.Set(this.settings.devControlToStorePointKey, JSON.stringify(([koordinatePike])));
        var pika = new OpenLayers.Format.WKT().read(koordinatePike);
        pika.geometry.transform(this.settings.projectionFrom, this.settings.option.projection);
        this.vektoriMePikat.addFeatures(pika);
        this.map.setCenter(new OpenLayers.LonLat(pika.geometry.x, pika.geometry.y), 17);
    },
};
