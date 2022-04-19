/* Copyright (c) 2006-2010 by OpenLayers Contributors (see authors.txt for
 * full list of contributors). Published under the Clear BSD license.
 * See http://svn.openlayers.org/trunk/openlayers/license.txt for the
 * full text of the license. */
/**
 * @requires OpenLayers/Control.js
 * @requires sprintf.js               !!! to set labelFmt, default : "#%3.1E"
 */
OpenLayers.Control.GraticuleXYT = OpenLayers.Class(OpenLayers.Control, {
    displayProjT:"",
    realProjT:"",
    autoActivate: true,
    intervals: [1000000, 500000, 200000, 100000, 50000, 20000, 10000, 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1],
    displayInLayerSwitcher: true,
    visible: true,
    numPoints: 50,
    targetSize: 200,
    lineSymbolizer: {
        strokeColor: "#42C0FB",
        strokeWidth: 1.5,
        strokeOpacity: 0.5
    },
    labelFmt: "#%3.1E",
    layerName: null,
    labelled: true,
    labelFormat: 'dm',
    labelSymbolizer: {
        fontColor: "Black",
        fontSize: "10px",
        fontOpacity: 1,
        fontFamily: "Tahoma",
        fontWeight: "bold",
        labelOutlineColor: "#B2DFEE",
        labelOutlineWidth: 2,
        labelAlign: "ct",
        xOffset: 500,
        yOffset: 500,
    },
    gratLayer: null,
    xOffset: 500, 
    initialize: function(options) {
        options = options || {};
        options.layerName = options.layerName || OpenLayers.i18n("graticule");
        OpenLayers.Control.prototype.initialize.apply(this, [options]);

        this.labelSymbolizer.stroke = false;
        this.labelSymbolizer.fill = false;
        this.labelSymbolizer.label = "${label}";
        this.labelSymbolizer.labelAlign = "${labelAlign}";
        this.labelSymbolizer.labelXOffset = "${xOffset}";
        this.labelSymbolizer.labelYOffset = "${yOffset}";
    },

    destroy: function() {
        this.deactivate();
        OpenLayers.Control.prototype.destroy.apply(this, arguments);
        if (this.gratLayer) {
            this.gratLayer.destroy();
            this.gratLayer = null;
        }
    },

    draw: function() {
        OpenLayers.Control.prototype.draw.apply(this, arguments);
        if (!this.gratLayer) {
            var gratStyle = new OpenLayers.Style({},{
                rules: [new OpenLayers.Rule({
                    'symbolizer':
                        {
                            "Point": this.labelSymbolizer,
                            "Line": this.lineSymbolizer
                        }
                })]
            });
            this.gratLayer = new OpenLayers.Layer.Vector(this.layerName, {
                styleMap: new OpenLayers.StyleMap({'default':gratStyle}),
                visibility: this.visible,
                displayInLayerSwitcher: this.displayInLayerSwitcher
            });
        }
        return this.div;
    },

    activate: function() {
        if (OpenLayers.Control.prototype.activate.apply(this, arguments)) {
            this.map.addLayer(this.gratLayer);
            this.map.events.register('moveend', this, this.update);
            this.update();
            return true;
        } else {
            return false;
        }
    },

    deactivate: function() {
        if (OpenLayers.Control.prototype.deactivate.apply(this, arguments)) {
            this.map.events.unregister('moveend', this, this.update);
            this.map.removeLayer(this.gratLayer);
            return true;
        } else {
            return false;
        }
    },

    update: function() {
        var mapBounds = this.map.getExtent();
        if (!mapBounds) {
            return;
        }
        this.gratLayer.destroyFeatures();
        var mapRes = this.map.getResolution();
        var mapCenter = this.map.getCenter();
        var mapCenterLL = new OpenLayers.Pixel(mapCenter.lon, mapCenter.lat);
        var testSq = this.targetSize*mapRes;
        testSq *= testSq; 
        var llInterval;
        for (var i=0; i<this.intervals.length; ++i) {
            llInterval = this.intervals[i];
            var delta = llInterval/2;
            var p1 = mapCenterLL.offset(new OpenLayers.Pixel(-delta, -delta));
            var p2 = mapCenterLL.offset(new OpenLayers.Pixel(delta, delta));

            var distSq = (p1.x-p2.x)*(p1.x-p2.x) + (p1.y-p2.y)*(p1.y-p2.y);
            if (distSq <= testSq) {
                break;
            }
        }
        mapCenterLL.x = Math.floor(mapCenterLL.x/llInterval)*llInterval;
        mapCenterLL.y = Math.floor(mapCenterLL.y/llInterval)*llInterval;

        var iter = 0;
        var centerLonPoints = [mapCenterLL.clone()];
        var newPoint = mapCenterLL.clone();
        var mapXY;
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(0,llInterval));
            mapXY = newPoint.clone();
            centerLonPoints.unshift(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter<1000);
        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(0,-llInterval));
            mapXY = newPoint.clone();
            centerLonPoints.push(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter < 1000);

        iter = 0;
        var centerLatPoints = [mapCenterLL.clone()];

        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(-llInterval, 0));
            mapXY = newPoint.clone();
            centerLatPoints.unshift(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter < 1000);

        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(llInterval, 0));
            mapXY = newPoint.clone();
            centerLatPoints.push(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter < 1000);

        var lines = [];
        for(var i=0; i < centerLatPoints.length; ++i) {
            var lon = centerLatPoints[i].x;
            var pointList = [];
            var labelPoint = null;
            var latEnd = centerLonPoints[0].y;
            var latStart = centerLonPoints[centerLonPoints.length - 1].y;
            var latDelta = (latEnd - latStart)/this.numPoints;
            var lat = latStart;
            for(var j=0; j<= this.numPoints; ++j) {
                var gridPoint = new OpenLayers.Geometry.Point(lon,lat);
                pointList.push(gridPoint);
                lat += latDelta;
                if (gridPoint.y >= mapBounds.bottom && !labelPoint) {
                    labelPoint = gridPoint; 
                    var pikaLabel=labelPoint.clone().transform(new OpenLayers.Projection(this.realProjT), new OpenLayers.Projection(this.displayProjT));
                }
            }
            if (this.labelled) {
                var labelPos = new OpenLayers.Geometry.Point(labelPoint.x,mapBounds.bottom);
                var labeling = (Math.abs(pikaLabel.x)>1000) ? sprintf(this.labelFmt, pikaLabel.x) : pikaLabel.x;
                var labelAttrs = {
                    value: lon,
                    label: this.labelled ? labeling : "",
                    labelAlign: "cb",
                    xOffset: 0,
                    yOffset: 2
                };
                this.gratLayer.addFeatures(new OpenLayers.Feature.Vector(labelPos,labelAttrs));
            }
            var geom = new OpenLayers.Geometry.LineString(pointList);
            lines.push(new OpenLayers.Feature.Vector(geom));
        }

        for (var j=0; j < centerLonPoints.length; ++j) {
            lat = centerLonPoints[j].y;
            var pointList = [];
            var lonStart = centerLatPoints[0].x;
            var lonEnd = centerLatPoints[centerLatPoints.length - 1].x;
            var lonDelta = (lonEnd - lonStart)/this.numPoints;
            var lon = lonStart;
            var labelPoint = null;
            for(var i=0; i <= this.numPoints ; ++i) {
                var gridPoint = new OpenLayers.Geometry.Point(lon,lat);
                pointList.push(gridPoint);
                lon += lonDelta;
                if (gridPoint.x < mapBounds.right) {
                    labelPoint = gridPoint;
                    var pikaLabel=labelPoint.clone().transform(new OpenLayers.Projection(this.realProjT), new OpenLayers.Projection(this.displayProjT));
                }
            }
            if (this.labelled) {
                var labelPos = new OpenLayers.Geometry.Point(mapBounds.right, labelPoint.y);
                var labeling = (Math.abs(pikaLabel.y)>1000) ? sprintf(this.labelFmt, pikaLabel.y) : pikaLabel.y;
                var labelAttrs = {
                    value: lat,
                    label: this.labelled ? labeling :"",
                    labelAlign: "rb",
                    xOffset: -2,
                    yOffset: 2
                };
                this.gratLayer.addFeatures(new OpenLayers.Feature.Vector(labelPos,labelAttrs));
            }
            var geom = new OpenLayers.Geometry.LineString(pointList);
            lines.push(new OpenLayers.Feature.Vector(geom));
          }
          this.gratLayer.addFeatures(lines);
    },

    updateT: function(mapBoundsT,mapResT,mapCenterT) {
      var mapBounds=mapBoundsT;
        if (!mapBounds) {
            return;
        }
        this.gratLayer.destroyFeatures();
        var mapRes=mapResT;
        var mapCenter=mapCenterT;        
        var mapCenterLL = new OpenLayers.Pixel(mapCenter.lon, mapCenter.lat);
        var testSq = this.targetSize*mapRes;
        testSq *= testSq; 
        var llInterval;
        for (var i=0; i<this.intervals.length; ++i) {
            llInterval = this.intervals[i]; 
            var delta = llInterval/2;
            var p1 = mapCenterLL.offset(new OpenLayers.Pixel(-delta, -delta)); 
            var p2 = mapCenterLL.offset(new OpenLayers.Pixel( delta,  delta));
            var distSq = (p1.x-p2.x)*(p1.x-p2.x) + (p1.y-p2.y)*(p1.y-p2.y);
            if (distSq <= testSq) {
                break;
            }
        }
        mapCenterLL.x = Math.floor(mapCenterLL.x/llInterval)*llInterval;
        mapCenterLL.y = Math.floor(mapCenterLL.y/llInterval)*llInterval;
        var iter = 0;
        var centerLonPoints = [mapCenterLL.clone()];
        var newPoint = mapCenterLL.clone();
        var mapXY;
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(0,llInterval));
            mapXY = newPoint.clone();
            centerLonPoints.unshift(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter<1000);
        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(0,-llInterval));
            mapXY = newPoint.clone();
            centerLonPoints.push(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter < 1000);

        iter = 0;
        var centerLatPoints = [mapCenterLL.clone()];
        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(-llInterval, 0));
            mapXY = newPoint.clone();
            centerLatPoints.unshift(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter<1000);
        newPoint = mapCenterLL.clone();
        do {
            newPoint = newPoint.offset(new OpenLayers.Pixel(llInterval, 0));
            mapXY = newPoint.clone();
            centerLatPoints.push(newPoint);
        } while (mapBounds.containsPixel(mapXY) && ++iter<1000);

        var lines = [];
        for(var i=0; i < centerLatPoints.length; ++i) {
            var lon = centerLatPoints[i].x;
            var pointList = [];
            var labelPoint = null;
            var latEnd = centerLonPoints[0].y;
            var latStart = centerLonPoints[centerLonPoints.length - 1].y;
            var latDelta = (latEnd - latStart)/this.numPoints;
            var lat = latStart;
            for(var j=0; j<= this.numPoints; ++j) {
                var gridPoint = new OpenLayers.Geometry.Point(lon,lat);
                pointList.push(gridPoint);
                lat += latDelta;
                if (gridPoint.y >= mapBounds.bottom && !labelPoint) {
                    labelPoint = gridPoint;
                    var pikaLabel=labelPoint.clone().transform(new OpenLayers.Projection(this.realProjT), new OpenLayers.Projection(this.displayProjT));
                }
            }
            if (this.labelled) {
                var labelPos = new OpenLayers.Geometry.Point(labelPoint.x,mapBounds.bottom);
                var labeling = (Math.abs(pikaLabel.x)>1000) ? sprintf(this.labelFmt, pikaLabel.x) : pikaLabel.x;
                var labelAttrs = {
                    value: lon,
                    label: this.labelled ? labeling : "",
                    labelAlign: "cb",
                    xOffset: 0,
                    yOffset: 2
                };
                this.gratLayer.addFeatures(new OpenLayers.Feature.Vector(labelPos,labelAttrs));
            }
            var geom = new OpenLayers.Geometry.LineString(pointList);
            lines.push(new OpenLayers.Feature.Vector(geom));
        }

        for (var j=0; j < centerLonPoints.length; ++j) {
            lat = centerLonPoints[j].y;
            var pointList = [];
            var lonStart = centerLatPoints[0].x;
            var lonEnd = centerLatPoints[centerLatPoints.length - 1].x;
            var lonDelta = (lonEnd - lonStart)/this.numPoints;
            var lon = lonStart;
            var labelPoint = null;
            for(var i=0; i <= this.numPoints ; ++i) {
                var gridPoint = new OpenLayers.Geometry.Point(lon,lat);
                pointList.push(gridPoint);
                lon += lonDelta;
                if (gridPoint.x < mapBounds.right) {
                    labelPoint = gridPoint;
                    var pikaLabel=labelPoint.clone().transform(new OpenLayers.Projection(this.realProjT), new OpenLayers.Projection(this.displayProjT));      
                }
            }
            if (this.labelled) {
                var labelPos = new OpenLayers.Geometry.Point(mapBounds.right, labelPoint.y);
                var labeling = (Math.abs(pikaLabel.y)>1000) ? sprintf(this.labelFmt, pikaLabel.y) : pikaLabel.y;
                var labelAttrs = {
                    value: lat,
                    label: this.labelled ? labeling :"",
                    labelAlign: "rb",
                    xOffset: -2,
                    yOffset: 2
                };
                this.gratLayer.addFeatures(new OpenLayers.Feature.Vector(labelPos,labelAttrs));
            }
            var geom = new OpenLayers.Geometry.LineString(pointList);
            lines.push(new OpenLayers.Feature.Vector(geom));
          }
          this.gratLayer.addFeatures(lines);
    },
    
    CLASS_NAME: "OpenLayers.Control.GraticuleXY"
});

