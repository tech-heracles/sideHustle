; if (typeof myJQGridPivot == 'undefined') {
    myJQGridPivot = {};
}

myJQGridPivot.inicializoGride = function (emriGrides, pagerId, captionGrides, zona, lastSel, idgjuha, enabled) {
     var   arrNames;
     if (idgjuha == 0) arrNames = ['IdKolona', 'Rendi', 'Kolona', 'Width', 'EmerTabeleDB', 'EmerKoloneDB', 'GrupimKolone', 'TipiKolones', 'Analitik/Total', 'Fshi', 'llojGrupimi'];
     else       arrNames= ['ColumnId', 'Order', 'Column', 'Width', 'TableNameDB', 'ColumnNameDB', 'ColumnGroup', 'ColumnType', 'Line/Total', 'Delete','Summary']   ;
    $(emriGrides).jqGrid({
        datatype: "local",
        width: '450',
        colNames: arrNames ,
        colModel: [
                   { name: 'idKolonaPivotGrid', index: 'idKolonaPivotGrid', hidden: true, editable: false, sortable: false },
                   { name: 'rendi', index: 'rendi', editable: false, sortable: false },
                   { name: 'emrikolonesshfaq', index: 'emrikolonesshfaq', width: 150, editable: false, sortable: false },
                   { name: 'width', index: 'width', editable: enabled, sortable: false },
                   { name: 'emerTabeleDB', index: 'emerTabeleDB', editable: false, sortable: false, hidden: true },
                   { name: 'emerKoloneDB', index: 'emerKoloneDB', editable: false, sortable: false, hidden: true },
                   { name: 'grupimKolone', index: 'grupimKolone', hidden: true, editable: false, sortable: false },
                   { name: 'tipiKolones', index: 'tipiKolones', editable: false, sortable: false, hidden: true },
                   { name: 'analitikTotal', index: 'analitikTotal', editable: false, sortable: false, hidden: true },
                   { name: 'fshi', index: 'fshi', sortable: false, align: 'center' },
                   { name: 'llojGrupimi', index: 'llojGrupimi', editable: false, sortable: false, hidden: true }
                 ],
        userdata: { zona: zona },
        pager: pagerId,
        pgbuttons: false,
        pginput: false,
        viewrecords: true,
        caption: captionGrides,
        sortable: enabled,

        gridComplete: function () {
            var ids = jQuery(emriGrides).jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var idRow = ids[i];
                if (idRow == "jqg_empty_row")
                    continue;
                be = myJQGridPivot.myValueButtonFshi(idRow, emriGrides, enabled);
                jQuery(emriGrides).jqGrid('setRowData', ids[i], { fshi: be });
            }
        }, 
        onSelectRow: function (id) {
            if (id && id !== lastSel) {
                idkontrolli = "#" + jQuery(emriGrides).getGridParam('colModel')[3].index + lastSel;
                editorWidth = $(idkontrolli);
                if (editorWidth != null) {
                    if (parseFloat(editorWidth.value) != "" && (parseFloat(editorWidth.value) < 20 || parseFloat(editorWidth.value) > 300)) {
                        myMesazh.ShtoMesazhGabimi('Gjeresia duhet te jete numer >= 20 dhe <= 300 !');
                        editorWidth.focus();
                        return;
                    }
                }
                jQuery(emriGrides).jqGrid('saveRow', lastSel, null, 'clientArray');
                lastSel = id;
            }
            jQuery(emriGrides).editRow(id, true);
        }
    });

    if (enabled) {
        jQuery(emriGrides).sortableRows({
            stop: function (event, ui) {
                myJQGridPivot.renditGriden(emriGrides);
            }
        });
    }
    if (jQuery(emriGrides).getGridParam('reccount') < 1) {
        lastSel = 1;
        return lastSel;
    }
}

myJQGridPivot.myValueWidth = function (elem, operation, value) {
    if (operation === 'get') {
        return $(elem).find("input").val();
    }
    else if (operation === 'set') {
        $('input', elem).val(value);
    }
}

myJQGridPivot.myElemWidth = function (value, option) {
    var el3 = $('<div></div>');
    var textField = $('<input type="text" />');
    textField.attr('id', option.id);
    textField.val(value);
    textField.focusout(function () {
        myJQGridPivot.myValueWidth();
    });
    textField.appendTo(el3);
    return el3;
}

    myJQGridPivot.getTextFromCell = function (cellNode) {
        return cellNode.childNodes[0].nodeName === "INPUT" ?
               cellNode.childNodes[0].value :
               cellNode.textContent || cellNode.innerText;
    }

    myJQGridPivot.myValueButtonFshi = function (lastSel, grida, enabled) {
        var disableOptions = enabled ? "" : "style='opacity: 0.5;cursor:not-allowed' disabled";
        
        fshiButton = "<input id='butonFshi" + lastSel + "' type='image' value='Fshi' src='images/blue-square-icon.png' "+disableOptions+" onclick=\" myJQGridPivot.fshiClicked('" + lastSel + "','" + grida + "') \" />";
        return fshiButton;
    }

    myJQGridPivot.renditGriden = function (emriGrides) {
        var gridIDs = jQuery(emriGrides).getDataIDs();
        for (var i = 0; i < gridIDs.length; i++)
            jQuery(emriGrides).jqGrid('setCell', gridIDs[i], "rendi", i + 1);
    }

    myJQGridPivot.fshiClicked = function (index, emergride) {
        var rowData = jQuery(emergride).jqGrid('getRowData', index);
        var dataRow = { idKolonaPivotGrid: rowData.idKolonaPivotGrid, emrikolonesshfaq: rowData.emrikolonesshfaq, emerTabeleDB: rowData.emerTabeleDB, emerKoloneDB: rowData.emerKoloneDB, grupimKolone: rowData.grupimKolone, tipiKolones: rowData.tipiKolones };
        var numRows = jQuery("#tblFushat" + rowData.grupimKolone).getGridParam('reccount');
        var ids = jQuery("#tblFushat" + rowData.grupimKolone).jqGrid('getDataIDs');
        var idNew = parseInt(ids[ids.length - 1]) + 1;
        var su = jQuery("#tblFushat" + rowData.grupimKolone).addRowData(idNew, dataRow);
        jQuery(emergride).delRowData(index);
        myJQGridPivot.renditGriden(emergride);
        //do shtohet pjesa ku rreshti i fshire te shtohet te grida perkatese e kolonave
    }

    myJQGridPivot.myElemValueTextBox = function (value, option, lastsel2, id) {
        var el3 = $('<div></div>');
        var textField = $('<input type="text" />');
        textField.attr('id', id + lastsel2);
        textField.val(value);
        textField.appendTo(el3);
        return el3;
    }

    myJQGridPivot.myValueTextBox = function (elem, operation, value) {

        if (operation === 'get') {

            return $(elem).find("input").val();
        }
        else if (operation === 'set') {
            $('input', elem).val(value);
        }
    };