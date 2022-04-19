; if (typeof myJQGridFushat == 'undefined') {
    myJQGridFushat = {};
}

myJQGridFushat.inicializoGride = function (emriGrides, pagerId, captionGrides, grupim, lastSel, idgjuha, enabled) {
    var arrNames;
    if (idgjuha == 0) arrNames = ['IdKolona', 'Kolona', 'EmerTabeleDB', 'EmerKoloneDB', 'GrupimKolona', 'TipiKolones', 'Analitik/Total','llojGrupimi'];
    else arrNames = ['ColumnId', 'Column', 'TableNameDB', 'ColumnNameDB', 'ColumnGroup', 'ColumnType', 'Line/Total', 'SummaryType'];


    jQuery(emriGrides).jqGrid({
        datatype: "local",
        width: '300',
        colNames: arrNames,
        colModel: [
                   { name: 'idKolonaPivotGrid', index: 'idKolonaPivotGrid', hidden: true, editable: false, sortable: false },
                   { name: 'emrikolonesshfaq', index: 'emrikolonesshfaq', width: 300, editable: false, sortable: false, align: 'left' },
                   { name: 'emerTabeleDB', index: 'emerTabeleDB', editable: false, sortable: false, hidden: true },
                   { name: 'emerKoloneDB', index: 'emerKoloneDB', editable: false, sortable: false, hidden: true },
                   { name: 'grupimKolone', index: 'grupimKolone', editable: false, sortable: false, hidden: true },
                   { name: 'tipiKolones', index: 'tipiKolones', editable: false, sortable: false, hidden: true },
                   { name: 'analitikTotal', index: 'analitikTotal', editable: false, sortable: false, hidden: true },
                   { name: 'llojGrupimi', index: 'llojGrupimi', editable: false, sortable: false, hidden: true }

                   ],
        pager: pagerId,
        pgbuttons: false,
        pginput: false,
        viewrecords: true,
        caption: captionGrides
    });

    jQuery(emriGrides).jqGrid('gridDnD', {
        connectWith: '#tblFilter, #tblRow, #tblData, #tblColumn',
        dragcopy: false,
        dropbyname: true,
        droppos: "last",
        beforedrop: function (e, ui, data, source, target) {
            if (!enabled)
            {
                ui.helper.dropped = false;
                return;
            }
            var newData = new Object();
            newData.idKolonaPivotGrid = data.idKolonaPivotGrid;
            $(this).delRowData("jqg_empty_row");
            newData.rendi = $(this).getGridParam('reccount') + 1;
            newData.emrikolonesshfaq = data.emrikolonesshfaq;
            newData.emerTabeleDB = data.emerTabeleDB;
            newData.emerKoloneDB = data.emerKoloneDB;
            newData.grupimKolone = data.grupimKolone;
            newData.tipiKolones = data.tipiKolones;
            newData.analitikTotal = data.analitikTotal;
            newData.llojGrupimi = data.llojGrupimi;
            return newData;
        }
    });

    if (jQuery(emriGrides).getGridParam('reccount') < 1) {
        lastSel = 1;
        return lastSel;
    }
};
