OpenLayers.Control.GetFeatureT = OpenLayers.Class(OpenLayers.Control.GetFeature, {
 bboxPoligon:null,   
        selectBox: function(position) {
        var bounds;
        if (position instanceof OpenLayers.Bounds) {
            var minXY = this.map.getLonLatFromPixel({
                x: position.left,
                y: position.bottom
            });
            var maxXY = this.map.getLonLatFromPixel({
                x: position.right,
                y: position.top
            });
            bounds = new OpenLayers.Bounds(
                minXY.lon, minXY.lat, maxXY.lon, maxXY.lat
            );
            
            bounds=bounds.transform(new OpenLayers.Projection(projeksioniGeo), new OpenLayers.Projection(projeksioniGeoV));
            bboxPoligon=bounds.toGeometry();
            
            
            
        } else {
            if(this.click) {
                // box without extent - let the click handler take care of it
                return;
            }
            bounds = this.pixelToBounds(position);
        }
        this.setModifiers(this.handlers.box.dragHandler.evt);
        this.request(bounds);
    },
    
     CLASS_NAME: "OpenLayers.Control.GetFeatureT"
});


