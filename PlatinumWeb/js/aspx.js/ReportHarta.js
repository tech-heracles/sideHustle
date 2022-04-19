var harta;
$(document).on('ready', function () {
    var options = {
        DOTS_PER_INCH: 97,
        option: { div: 'map' },
        drawPoint: false,
        deletePoints: false,
        devControlToStorePoint: hfState,
        devControlToStorePointKey: "geom",
        devControlToShowPointCoordinates: null
    };
    harta = new Harta(options);
});