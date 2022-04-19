; jQuery(document).ready(function () {
    inicializoGride();
});

function inicializoGride() {
    jQuery("#rowed5").jqGrid({
        datatype: "local",
        colNames: ['Lloji', 'Kodi', 'Pershkrimi'],
        colModel: [
                        { name: 'Lloji', index: 'Lloji', width: 100, align: 'left', editable: true },
                        { name: 'Kodi', index: 'Kodi', width: 100, align: 'left', editable: true },
                        { name: 'Pershkrimi', index: 'Pershkrimi', width: 150, align: 'left', editable: true },
                     ],
        sortable: true,
        height: 'auto',
        recreateForm: true,
        multiselect: false,
        rowNum: 5,
        width: 700
    });
    var myData = [{ "Lloji": "Lloji 1", "Kodi": "Kod 1", "Pershkrimi": "Pershkrim 1" }, { "Lloji": "Lloji 2", "Kodi": "Kod 2", "Pershkrimi": "Pershkrim 2" }, { "Lloji": "Lloji 3", "Kodi": "Kod 3", "Pershkrimi": "Pershkrim 3"}];
    for (var i = 0; i < myData.length; i++) {
        jQuery("#rowed5").jqGrid('addRowData', i, myData[i]);
    }
}