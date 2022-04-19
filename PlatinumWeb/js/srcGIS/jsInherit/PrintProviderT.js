/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
Ext.namespace("GeoExt.data");
GeoExt.data.PrintProviderT = Ext.extend(GeoExt.data.PrintProvider, {
    adresaServerit: 'localhost',
    print: function (map, pages, options) {
        var a = new OpenLayers.LonLat(pages[0].feature.layer.features[0].geometry.getCentroid().x, pages[0].feature.layer.features[0].geometry.getCentroid().y)
        if (map instanceof GeoExt.MapPanel) {
            map = map.map;
        }
        pages = pages instanceof Array ? pages : [pages];
        options = options || {};
        if (this.fireEvent("beforeprint", this, map, pages, options) === false)
            return;

        var jsonData = Ext.apply({
            units: map.getUnits(),
            srs: map.baseLayer.projection.getCode(),
            layout: this.layout.get("name"),
            dpi: this.dpi.get("value"),
            projectUrl: urlApp,
            scopeID: Utils.getUrlVar("scopeID")
        }, this.customParams);

        var pagesLayer = pages[0].feature.layer;
        var encodedLayers = [];

        // ensure that the baseLayer is the first one in the encoded list
        var layers = map.layers.concat();
        layers.remove(map.baseLayer);
        layers.unshift(map.baseLayer);

        Ext.each(layers, function (layer) {
            //if (layer !== pagesLayer && layer.getVisibility() === true && !layer.VektPerkohshemLayer && (layer.name != "print")) {
            if (layer !== pagesLayer && (!layer.VektPerkohshemLayer) && (layer.name != "print") && (
                    (varSettings.printimiPdf.ngaEditimi && ((layer.isBaseLayer && layer.getVisibility() === true) || layer.name == "TempLayerPrindFemi")) ||
                    (!varSettings.printimiPdf.ngaEditimi && (layer.getVisibility() === true) && layer.name != "TempLayerPrindFemi"))
                ) {
                var enc = this.encodeLayer(layer);
                enc && encodedLayers.push(enc);
            }
        }, this);
        jsonData.layers = encodedLayers;

        var encodedPages = [];
        Ext.each(pages, function (page) {
            encodedPages.push(Ext.apply({
                center: [page.center.lon, page.center.lat],
                scale: page.scale.get("value"),
                rotation: page.rotation
            }, page.customParams));
        }, this);
        jsonData.pages = encodedPages;

        if (options.overview) {
            var encodedOverviewLayers = [];
            Ext.each(options.overview.layers, function (layer) {
                var enc = this.encodeLayer(layer);
                enc && encodedOverviewLayers.push(enc);
            }, this);
            jsonData.overviewLayers = encodedOverviewLayers;
        }

        if (options.legend) {
            var legend = options.legend;
            var rendered = legend.rendered;
            if (!rendered) {
                legend = legend.cloneConfig({
                    renderTo: document.body,
                    hidden: true
                });
            }
            var encodedLegends = [];
            if (varSettings.printimiPdf.meShpjegues == 1 && !varSettings.printimiPdf.ngaEditimi) {
                legend.items && legend.items.each(function (cmp) {
                    if (!cmp.hidden) {
                        if (cmp.layerRecord.data.layer.eshteNeLegjende) {
                            var encFn = this.encoders.legends[cmp.getXType()];
                            // MapFish Print doesn't currently support per-page
                            // legends, so we use the scale of the first page.
                            encodedLegends = encodedLegends.concat(
						    encFn.call(this, cmp, jsonData.pages[0].scale));
                        }
                    }
                }, this);
            }
            if (!rendered) {
                legend.destroy();
            }
            jsonData.legends = encodedLegends;
        }        

        if (this.method === "GET") {
            var url = Ext.urlAppend(this.capabilities.printURL,
				"spec=" + encodeURIComponent(Ext.encode(jsonData)));
            this.download(url);
        } else {
            Ext.Ajax.request({
                url: varSettings.proxyConfig.publicProxy + "geoserverPrintCapabilities&printUrl=" + this.capabilities.createURL, //url: varSettings.proxyConfig.url + this.capabilities.createURL,
                method: "POST",
                jsonData: jsonData,
                headers: { "Content-Type": "application/json; charset=" + this.encoding },
                success: function (response) {
                    var url = varSettings.proxyConfig.publicProxyFullUrl + "geoserverPrintCapabilities&printUrl=" + Ext.decode(response.responseText).getURL;
                    this.download(url);
                },
                failure: function (response) {
                    this.fireEvent("printexception", this, response);
                },
                params: this.initialConfig.baseParams,
                scope: this
            });
            return false;
        }
    },
    encoders: {
        "layers": {

            "XYZ_A_T": function (layer) {
                //per tileServer                
                var enc = this.encoders.layers.TileCache.call(this, layer);
                varBaseURL = enc.baseURL.substr(0, enc.baseURL.indexOf("$"));
                varBaseURL = varBaseURL.substring(0, varBaseURL.length - 1);
                varBaseURL = varSettings.proxyConfig.publicProxyFullUrl + "geoserverRequestForPrint&proxyHost=" + varBaseURL;
                return Ext.apply(enc, {
                    type: 'XYZ',
                    baseURL: varBaseURL, 
                    tileSize: [256, 256],
                    resolutions: [156543.03390625, 78271.516953125, 39135.7584765625, 19567.87923828125, 9783.939619140625, 4891.9698095703125, 2445.9849047851562, 1222.9924523925781, 611.4962261962891, 305.74811309814453, 152.87405654907226, 76.43702827453613, 38.218514137268066, 19.109257068634033, 9.554628534317017, 4.777314267158508, 2.388657133579254, 1.194328566789627, 0.5971642833948135, 0.29858214169740677, 0.14929107084870338],
                    extension: enc.baseURL.substr(enc.baseURL.length - 3, enc.baseURL.length),
                    path_format: "${z}/${x}/${y}." + enc.baseURL.substr(enc.baseURL.length - 3, enc.baseURL.length) + ""
                });
            },
            "Bing": function (layer) {

                var enc = this.encoders.layers.TileCache.call(this, layer);
                return Ext.apply(enc, {
                    type: 'XYZ',
                    baseURL: enc.baseURL.substr(0, enc.baseURL.indexOf("$")),
                    tileSize: [256, 256],
                    resolutions: [156543.03390625, 78271.516953125, 39135.7584765625,19567.87923828125, 9783.939619140625, 4891.9698095703125,2445.9849047851562, 1222.9924523925781, 611.4962261962891,305.74811309814453, 152.87405654907226, 76.43702827453613,38.218514137268066, 19.109257068634033, 9.554628534317017,4.777314267158508, 2.388657133579254, 1.194328566789627,0.5971642833948135, 0.29858214169740677, 0.14929107084870338,0.07464553542435169],
                    extension: enc.baseURL.substr(enc.baseURL.length - 3, enc.baseURL.length),
                    source: "bing",
                    title: "Bing Road Map",
                    name: "Road",
                    ptype: "gxp_bingsource"
                });
            },
            "XYZ": function (layer) {
                var enc = this.encoders.layers.TileCache.call(this, layer);
                return Ext.apply(enc, {
                    type: 'XYZ',
                    baseURL: enc.baseURL.substr(0, enc.baseURL.indexOf("$")),
                    tileSize: [256, 256],
                    resolutions: [156543.03390625, 78271.516953125, 39135.7584765625, 19567.87923828125, 9783.939619140625, 4891.9698095703125, 2445.9849047851562, 1222.9924523925781, 611.4962261962891, 305.74811309814453, 152.87405654907226, 76.43702827453613, 38.218514137268066, 19.109257068634033, 9.554628534317017, 4.777314267158508, 2.388657133579254, 1.194328566789627, 0.5971642833948135, 0.29858214169740677, 0.14929107084870338],
                    extension: enc.baseURL.substr(enc.baseURL.length - 3, enc.baseURL.length),
                    path_format: "${z}/${x}/${y}." + enc.baseURL.substr(enc.baseURL.length - 3, enc.baseURL.length) + ""
                });
            },
            "Layer": function (layer) {
                var enc = {};
                if (layer.options && layer.options.maxScale) {
                    enc.minScaleDenominator = layer.options.maxScale;
                }
                if (layer.options && layer.options.minScale) {
                    enc.maxScaleDenominator = layer.options.minScale;
                }
                return enc;
            },
            "WMS": function (layer) {
                var enc = this.encoders.layers.HTTPRequest.call(this, layer);
                var mp = [];

                Ext.apply(enc, {
                    baseURL: window.location.origin + "/" + 'GISProxyGEO.ashx?scopeID=' + Utils.getUrlVar("scopeID") + '&url=wms?',
                    type: 'WMS',
                    layers: [layer.params.LAYERS].join(",").split(","),
                    format: layer.params.FORMAT,
                    styles: [layer.params.STYLES].join(",").split(",")
                });
                var param;
                for (var p in layer.params) {
                    param = p.toLowerCase();
                    if (!layer.DEFAULT_PARAMS[param]) {

                        if ("layers,styles,width,height,srs".indexOf(param) == -1) {
                            if (!enc.customParams) {
                                enc.customParams = {};
                            }
                            if (param == "cql_filter")
                                if (varSettings.printimiPdf.ngaEditimi)
                                    enc.customParams[p] = varSettings.printimiPdf.whereQuery;
                                else
                                    enc.customParams[p] = map.getLayersBy("visibility", true).filter(function (el) { return el.IDLAYERSTYPE > 2; }).map(function (value, index) { return value.params; }).map(function (value, index) { return value.CQL_FILTER; }).join(';');
                            else
                                enc.customParams[p] = layer.params[p];
                        }
                    }
                }
                return enc;
            },
            "OSM": function (layer) {
                var enc = this.encoders.layers.TileCache.call(this, layer);
                return Ext.apply(enc, {
                    type: 'OSM',
                    baseURL: enc.baseURL.substr(0, enc.baseURL.indexOf("$")),
                    extension: "png"
                });
            },
            "TMS": function (layer) {
                var enc = this.encoders.layers.TileCache.call(this, layer);
                return Ext.apply(enc, {
                    type: 'TMS',
                    format: layer.type
                });
            },
            "TileCache": function (layer) {
                var enc = this.encoders.layers.HTTPRequest.call(this, layer);
                return Ext.apply(enc, {
                    type: 'TileCache',
                    layer: layer.layername,
                    maxExtent: layer.maxExtent.toArray(),
                    tileSize: [layer.tileSize.w, layer.tileSize.h],
                    extension: layer.extension,
                    resolutions: layer.serverResolutions || layer.resolutions
                });
            },
            "WMTS": function (layer) {
                var enc = this.encoders.layers.HTTPRequest.call(this, layer);
                return Ext.apply(enc, {
                    type: 'WMTS',
                    layer: layer.layer,
                    version: layer.version,
                    requestEncoding: layer.requestEncoding,
                    tileOrigin: [layer.tileOrigin.lon, layer.tileOrigin.lat],
                    tileSize: [layer.tileSize.w, layer.tileSize.h],
                    style: layer.style,
                    formatSuffix: layer.formatSuffix,
                    dimensions: layer.dimensions,
                    params: layer.params,
                    maxExtent: (layer.tileFullExtent != null) ? layer.tileFullExtent.toArray() : layer.maxExtent.toArray(),
                    matrixSet: layer.matrixSet,
                    zoomOffset: layer.zoomOffset,
                    resolutions: layer.serverResolutions || layer.resolutions
                });
            },
            "KaMapCache": function (layer) {
                var enc = this.encoders.layers.KaMap.call(this, layer);
                return Ext.apply(enc, {
                    type: 'KaMapCache',
                    // group param is mandatory when using KaMapCache
                    group: layer.params['g'],
                    metaTileWidth: layer.params['metaTileSize']['w'],
                    metaTileHeight: layer.params['metaTileSize']['h']
                });
            },
            "KaMap": function (layer) {
                var enc = this.encoders.layers.HTTPRequest.call(this, layer);
                return Ext.apply(enc, {
                    type: 'KaMap',
                    map: layer.params['map'],
                    extension: layer.params['i'],
                    // group param is optional when using KaMap
                    group: layer.params['g'] || "",
                    maxExtent: layer.maxExtent.toArray(),
                    tileSize: [layer.tileSize.w, layer.tileSize.h],
                    resolutions: layer.serverResolutions || layer.resolutions
                });
            },
            "HTTPRequest": function (layer) {
                var enc = this.encoders.layers.Layer.call(this, layer);
                return Ext.apply(enc, {
                    baseURL: this.getAbsoluteUrl(layer.url instanceof Array ? layer.url[0] : layer.url),
                    opacity: (layer.opacity != null) ? layer.opacity : 1.0,
                    singleTile: layer.singleTile
                });
            },
            "Image": function (layer) {
                var enc = this.encoders.layers.Layer.call(this, layer);
                return Ext.apply(enc, {
                    type: 'Image',
                    baseURL: this.getAbsoluteUrl(layer.getURL(layer.extent)),
                    opacity: (layer.opacity != null) ? layer.opacity : 1.0,
                    extent: layer.extent.toArray(),
                    pixelSize: [layer.size.w, layer.size.h],
                    name: layer.name
                });
            },
            "Google": function (layer) {
                var gMapType = layer.type;
                var maptype = "roadmap"; //default map type
                switch (gMapType) {
                    case google.maps.MapTypeId.ROADMAP:
                        maptype = "roadmap";
                        break;
                    case google.maps.MapTypeId.SATELLITE:
                        maptype = "satellite";
                        break;
                    case google.maps.MapTypeId.HYBRID:
                        maptype = "hybrid";
                        break;
                    case google.maps.MapTypeId.TERRAIN:
                        maptype = "terrain";
                        break;
                }
                return { 
                    baseURL: 'http://maps.google.com/maps/api/staticmap',
                    type: 'TiledGoogle',
                    maxExtent: [-20037508.3392, -20037508.3392, 20037508.3392, 20037508.3392],
                    tileSize: [256, 256],
                    resolutions: [156543.03390625, 78271.516953125, 39135.7584765625,19567.87923828125, 9783.939619140625, 4891.9698095703125,2445.9849047851562, 1222.9924523925781, 611.4962261962891,305.74811309814453, 152.87405654907226, 76.43702827453613,38.218514137268066, 19.109257068634033, 9.554628534317017,4.777314267158508, 2.388657133579254, 1.194328566789627,0.5971642833948135, 0.29858214169740677, 0.14929107084870338,0.07464553542435169],
                    extension: 'png',
                    format: 'png',
                    sensor: 'false',
                    maptype: maptype
                }
            },
            "Vector": function (layer) {
                if (!layer.features.length) {
                    return;
                }
                var encFeatures = [];
                var encStyles = {};
                var features = layer.features;
                var featureFormat = new OpenLayers.Format.GeoJSON();
                var styleFormat = new OpenLayers.Format.JSON();
                var nextId = 1;
                var styleDict = {};
                var feature, style, dictKey, dictItem, styleName;
                for (var i = 0, len = features.length; i < len; ++i) {
                    if (layer.name == "OpenLayers.Handler.Path" && (features[i].geometry.components == undefined || features[i].geometry.components.length < 2))
                        continue;
                    feature = features[i];
                    style = feature.style || layer.style || layer.styleMap.createSymbolizer(feature,
					feature.renderIntent);
                    dictKey = styleFormat.write(style);
                    dictItem = styleDict[dictKey];
                    if (dictItem) {
                        //this style is already known
                        styleName = dictItem;
                    } else {
                        //new style
                        styleDict[dictKey] = styleName = nextId++;
                        if (style.externalGraphic) {
                            encStyles[styleName] = Ext.applyIf({
                                externalGraphic: this.getAbsoluteUrl(
								style.externalGraphic)
                            }, style);
                        } else {
                            encStyles[styleName] = style;
                        }
                    }
                    var featureGeoJson = featureFormat.extract.feature.call(
					featureFormat, feature);
                    featureGeoJson.properties = OpenLayers.Util.extend({
                        _gx_style: styleName
                    }, featureGeoJson.properties);
                    encFeatures.push(featureGeoJson);
                }
                var enc = this.encoders.layers.Layer.call(this, layer);
                return Ext.apply(enc, {
                    type: 'Vector',
                    styles: encStyles,
                    styleProperty: '_gx_style',
                    geoJson: {
                        type: "FeatureCollection",
                        features: encFeatures
                    },
                    name: layer.name,
                    opacity: (layer.opacity != null) ? layer.opacity : 1.0
                });
            },
            "Markers": function (layer) {
                var features = [];
                for (var i = 0, len = layer.markers.length; i < len; i++) {
                    var marker = layer.markers[i];
                    var geometry = new OpenLayers.Geometry.Point(marker.lonlat.lon, marker.lonlat.lat);
                    var style = {
                        externalGraphic: marker.icon.url,
                        graphicWidth: marker.icon.size.w,
                        graphicHeight: marker.icon.size.h,
                        graphicXOffset: marker.icon.offset.x,
                        graphicYOffset: marker.icon.offset.y
                    };
                    var feature = new OpenLayers.Feature.Vector(geometry, {}, style);
                    features.push(feature);
                }
                var vector = new OpenLayers.Layer.Vector(layer.name);
                vector.addFeatures(features);
                var output = this.encoders.layers.Vector.call(this, vector);
                vector.destroy();
                return output;
            }
        },
        "legends": {
            "gx_wmslegend": function (legend, scale) {
                var enc = this.encoders.legends.base.call(this, legend);
                var icons = [];
                for (var i = 1, len = legend.items.getCount() ; i < len; ++i) {
                    icons.push(this.getAbsoluteUrl(legend.items.get(i).url));
                }
                enc[0].classes[0] = {
                    name: "",
                    icons: icons
                };
                return enc;
            },
            "gx_urllegend": function (legend) {

                var enc = this.encoders.legends.base.call(this, legend);
                enc[0].classes.push({
                    name: "",
                    icon: this.getAbsoluteUrl(legend.items.get(1).url)
                });
                return enc;
            },
            "base": function (legend) {
                return [{
                    name: legend.getLabel(),
                    classes: []
                }];
            }
        }
    },
    encodeLayer: function (layer) {
        var encLayer;
        for (var c in this.encoders.layers) {
            if (OpenLayers.Layer[c] && layer instanceof OpenLayers.Layer[c]) {
                if (this.fireEvent("beforeencodelayer", this, layer) === false) {
                    return;
                }
                encLayer = this.encoders.layers[c].call(this, layer);
                this.fireEvent("encodelayer", this, layer, encLayer);
                break;
            }
        }
        return (encLayer && encLayer.type) ? encLayer : null;
    }
});