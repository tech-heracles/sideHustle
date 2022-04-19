OpenLayers.Control.Click = OpenLayers.Class(OpenLayers.Control,
 {

 defaultHandlerOptions: {
 'single': true,
 'double': true,
 'pixelTolerance': 0,
 'stopSingle': false,
'stopDouble': false
 },
 handleRightClicks:true,
 initialize: function(options) {
 this.handlerOptions = OpenLayers.Util.extend(
 {}, this.defaultHandlerOptions
 );
 OpenLayers.Control.prototype.initialize.apply(
 this, arguments
 );
 this.handler = new OpenLayers.Handler.Click(
 this, this.eventMethods, this.handlerOptions
 );
 },
 CLASS_NAME: "OpenLayers.Control.Click"

 });