
function BaseLayer(BaseLayerPerzgjedhur,baseLayersapp) {
    this.BaseLayerPerzgjedhur = BaseLayerPerzgjedhur;
    this.baseLayersapp=baseLayersapp;//eshte emri qe layer ka ne javascript kur ndertohet si layer
}
 
BaseLayer.prototype.perzgjidhBaseLayer = function()
{
    for(i=0;i<this.baseLayersapp.length;i++){ 
        if(this.baseLayersapp[i]==this.BaseLayerPerzgjedhur)
        {
            //alert(this.BaseLayerPerzgjedhur)
            //alert(window[this.BaseLayerPerzgjedhur].getVisibility())
            //alert("orto"+orto.getVisibility())
            document.getElementById(this.BaseLayerPerzgjedhur).className = "button_Klikuar"+this.BaseLayerPerzgjedhur;

            map.setBaseLayer(window[this.BaseLayerPerzgjedhur]);
            map.baseLayer.setVisibility(true);
            //bashkia.setVisibility(true);
        }
        else
        {  
            document.getElementById(this.baseLayersapp[i]).className = "button_"+this.baseLayersapp[i];
            window[this.baseLayersapp[i]].setVisibility(false);
        }
    }
};






