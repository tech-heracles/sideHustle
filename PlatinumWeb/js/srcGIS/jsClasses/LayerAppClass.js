function LayerApp(title)
{
    this.title = title;
    this.gjendjaLayer =map.getLayersByName(title)[0].getVisibility();  
}

LayerApp.prototype.shfaqLayer=function()
{
    map.getLayersByName(this.title)[0].setVisibility(true);
}

LayerApp.prototype.mbyllLayer = function () {
    if (this.gjendjaLayer) {
        map.getLayersByName(this.title)[0].setVisibility(true);
    }
    else {
        map.getLayersByName(this.title)[0].setVisibility(false);
    }
};

