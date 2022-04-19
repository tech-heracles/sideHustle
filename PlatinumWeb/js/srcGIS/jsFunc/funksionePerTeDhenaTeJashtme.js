var TeDhenaHyreseGisWindow;

function plotesoFushatMeTeDhenaNgaJashte(TeDhenatHyresePerObjekt, objFId) {
    for (var i = 0; i < emrKolDisableQePlotesohen.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'").setValue(TeDhenatHyresePerObjekt.data[0][emrKolDisableQePlotesohen[i]]);
    }

    for (var i = 0; i < emrKolDisableQePlotesohenZ.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'").setValue(TeDhenatHyresePerObjekt.data[0][emrKolDisableQePlotesohenZ[i]]);
    }
};

function bejDisableFushatMeTeDhenaNgaJashte(objFId, tipiVeprimit) //kjo duhet tek update
{
    for (var i = 0; i < emrKolDisableQePlotesohen.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'").setDisabled(true);
    }

    for (var i = 0; i < emrKolDisableQePlotesohenZ.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'").setDisabled(true);
    }
};

function bejBoshFushatMeTeDhenaNgaJashte(objFId) {
    for (var i = 0; i < emrKolDisableQePlotesohen.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohen[i] + objFId + "'").setValue();
    }

    if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emerKoloneIdLidheseGis + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emerKoloneIdLidheseGis + objFId + "'").setValue();

    for (var i = 0; i < emrKolDisableQePlotesohenZ.length; i++) {
        if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emrKolDisableQePlotesohenZ[i] + objFId + "'").setValue();
    }

    if (varSettings.editim.objPanel.frmeditim.getComponent("'" + emerKoloneIdLidheseGisZ + objFId + "'")) varSettings.editim.objPanel.frmeditim.getComponent("'" + emerKoloneIdLidheseGisZ + objFId + "'").setValue();
};

function UpdateTeDhenatHyreseGisPasEditimit(vektoriMeTeDhenatGis) {
    var jsonStr = JSON.stringify(vektoriMeTeDhenatGis);
    Ext.Ajax.request({
        url: ' phpUI/UpdateTeDhenatHyresePasGisEdit.php',
        method: 'POST',
        params: {
            vektoriMeTeDhenatGis: jsonStr
        },
        success: function (result, request) {
            if (Ext.getCmp('GridaTeDhenaHyreseGis')) {
                Ext.getCmp('GridaTeDhenaHyreseGis').getStore().load();
                Ext.getCmp('GridaTeDhenaHyreseGis').getView().refresh();
            }
        },
        failure: function () {
            Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe('GP_MSG_MSG_updateTeDhenHyrPasGisEdit'));
        }
    })
};

function merrTeDhenatPerZonatPerZoneId(vleraNr_Zona, objFId) {
    Ext.Ajax.request({
        url: ' phpUI/MerrTeDhenatPerZonat.php',
        method: 'GET',
        params: {
            NR_Zona: vleraNr_Zona
        },
        success: function (result, request) {
            var TeDhenatZona = new Array();
            TeDhenatZona = JSON.parse(result.responseText);

            if (TeDhenatZona.data.length == 1) plotesoFushatMeTeDhenaNgaJashte(TeDhenatZona, objFId);
            else Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_MerrTeDhenatZonatZone"), bejBoshFushatMeTeDhenaNgaJashte(objFId));
        },
        failure: function () {
            Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe('GP_MSG_MSG_gabim'));
        }
    })
};

function merrTeDhenatHyresePerObjektGis(vleraIdGrGis, objFId) {
    Ext.Ajax.request({
        url: ' phpUI/MerrTeDhenatHyresePerIdGis.php',
        method: 'GET',
        params: {
            IdGrGis: vleraIdGrGis
        },
        success: function (result, request) {
            var TeDhenatHyresePerObjekt = new Array();
            TeDhenatHyresePerObjekt = JSON.parse(result.responseText);
            if (TeDhenatHyresePerObjekt.data.length == 1) 
                plotesoFushatMeTeDhenaNgaJashte(TeDhenatHyresePerObjekt, objFId);
            else
                Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe('GP_MSG_MSG_G_teDhenatHyresePerObjektGis'), bejBoshFushatMeTeDhenaNgaJashte(objFId));
        },
        failure: function () {
            Ext.MessageBox.alert(perkthe("GP_MSG_TIT_kujdes"), perkthe("GP_MSG_MSG_gabim"));
        }
    })
};

function krijoGridenZonat() {
    var storeZonat = new Ext.data.JsonStore({
        autoDestroy: true,
        url: 'phpUI/merrTeGjithaZonat.php',
        method: 'POST',
        remoteSort: false,
        sortInfo: {  field: 'Nr_Zona', direction: 'ASC' },
        storeId: 'myStoreZonat',
        idProperty: 'Nr_Zona',
        root: 'data',
        totalProperty: 'total',
        fields: [
            { name: 'Nr_Zona' },
            { name: 'Zona_Pershkrim' }
        ]
    });


    var filters = new Ext.ux.grid.GridFilters({
        encode: false, 
        local: true, 
        filters: [
            { type: 'string', dataIndex: 'Nr_Zona' },
            { type: 'string', dataIndex: 'Zona_Pershkrim' }
        ]
    });
    var createColModel = function (finish, start) {
        var columns = [
            { dataIndex: 'Nr_Zona', header: 'Id Alpha', filterable: true, width: 300 },
            { dataIndex: 'Zona_Pershkrim', header: 'Pershkrimi i zones', id: 'Zona_Pershkrim', width: 480, }
        ];
        return new Ext.grid.ColumnModel({
            columns: columns.slice(start || 0, finish),
            defaults: { sortable: true }
        });
    };

    var grid = new Ext.grid.GridPanel({
        border: false,
        id: "GridaZonat",
        store: storeZonat,
        autoScroll: true,
        disableSelection: true,
        viewConfig: {
            getRowClass: function (rec, rowIdx, params, store) {
                return 'ngjyraGjelber2';
            }
        },
        colModel: createColModel(2),
        loadMask: true,
        plugins: [filters],
        listeners: {
            render: {
                fn: function () {
                    storeZonat.load({
                        params: {
                            start: 0,
                            limit: 20
                        }
                    });
                }
            }
        },
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            displayMsg: perkthe('GP_GRID_BBAR_MSG_displayMsg'),
            emptyMsg: perkthe('GP_GRID_BBAR_MSG_emptyMsg'),
            beforePageText: perkthe('GP_GRID_BBAR_MSG_beforePageText'),
            afterPageText: perkthe('GP_GRID_BBAR_MSG_afterPageText'),
            store: storeZonat,
            pageSize: 20,
            plugins: [filters]
        })
    });
    return grid;
};

function krijoGridenMagazinat() {
    var storeTeDhenaMagazinat = new Ext.data.JsonStore({
        autoDestroy: true,
        url: 'phpUI/merrTeGjithaTeDhenatMagazina.php',
        method: 'POST',
        remoteSort: false,
        sortInfo: { field: 'NJEKOD', direction: 'ASC' },
        storeId: 'myStoreTeDhenatMagazinat',
        idProperty: 'NJEKOD',
        root: 'data',
        totalProperty: 'total',
        fields: [
            { name: 'NJEKOD' },
            { name: 'NJEPERSHK' }
        ]
    });

    var filters = new Ext.ux.grid.GridFilters({
        encode: false, // json encode the filter query
        local: true, // defaults to false (remote filtering)
        filters: [
            { type: 'string', dataIndex: 'NJEKOD' },
            { type: 'string', dataIndex: 'NJEPERSHK' }
        ]
    });

    var createColModel = function (finish, start) {
        var columns = [
            { dataIndex: 'NJEKOD', header: 'Kodi i magazines', filterable: true, width: 300 },
            { dataIndex: 'NJEPERSHK', header: 'Pershkrimi i magazines', id: 'NJEPERSHK', width: 480, filter: {} }
        ];

        return new Ext.grid.ColumnModel({
            columns: columns.slice(start || 0, finish),
            defaults: {
                sortable: true
            }
        });
    };

    var grid = new Ext.grid.GridPanel({
        border: false,
        id: "GridaTeDhenaMagazinat",
        store: storeTeDhenaMagazinat,
        autoScroll: true,
        disableSelection: true,
        viewConfig: {
            getRowClass: function (rec, rowIdx, params, store) {
                return 'ngjyraGjelber2';
            }
        },
        colModel: createColModel(2),
        loadMask: true,
        plugins: [filters],
        listeners: {
            render: {
                fn: function () {
                    storeTeDhenaMagazinat.load({
                        params: {
                            start: 0,
                            limit: 20
                        }
                    });
                }
            }
        },
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            displayMsg: perkthe("GP_GRID_BBAR_MSG_displayMsg"),
            emptyMsg: perkthe("GP_GRID_BBAR_MSG_emptyMsg"),
            beforePageText: perkthe("GP_GRID_BBAR_MSG_beforePageText"),
            afterPageText: perkthe("GP_GRID_BBAR_MSG_afterPageText"),
            store: storeTeDhenaMagazinat,
            pageSize: 20,
            plugins: [filters]
        })
    });
    return grid;
};


function krijoGridenTeDhenaHyreseTab1() {
    var storeTeDhenaHyreseGIS = new Ext.data.JsonStore({
        autoDestroy: true,
        url: 'phpUI/merrTeGjithaTeDhenatHyreseGIS.php',
        method: 'POST',
        remoteSort: false,
        sortInfo: {
            field: 'gr_gis_id',
            direction: 'ASC'
        },
        storeId: 'myStoreTeDhenatHyrese',
        idProperty: 'gr_gis_id',
        root: 'data',
        totalProperty: 'total',
        fields: [
            { name: 'gr_gis_id' },
            { name: 'magazine_id' },
            { name: 'magazine_nr' },
            { name: 'magazine_date', type: 'date', dateFormat: 'Y-m-d' },
            { name: 'magazine_emertim' },
            { name: 'artikull_nr' },
            { name: 'artikulli_pershkrimi' },
            { name: 'sasia' },
            { name: 'vlere' },
            { name: 'Numerimi' }
        ]
    });

    var filters = new Ext.ux.grid.GridFilters({
        encode: false, // json encode the filter query
        local: true, // defaults to false (remote filtering)
        filters: [
            { type: 'numeric', dataIndex: 'gr_gis_id' },
            { type: 'numeric', dataIndex: 'magazine_id' },
            { type: 'string', dataIndex: 'magazine_nr' },
            { type: 'date', dataIndex: 'magazine_date' },
            { type: 'string', dataIndex: 'magazine_emertim' },
            { type: 'string', dataIndex: 'artikull_nr' },
            { type: 'string', dataIndex: 'artikulli_pershkrimi' },
            { type: 'numeric', dataIndex: 'sasia' },
            { type: 'numeric', dataIndex: 'vlere' },
            { type: 'int', dataIndex: 'Numerimi' },
        ]
    });

    var createColModel = function (finish, start) {
        var columns = [
            { dataIndex: 'gr_gis_id', header: 'Gr_Gis_id', filterable: true },
            { dataIndex: 'magazine_id', header: 'Magazine_id', id: 'Magazine_id', filter: {} },
            { dataIndex: 'magazine_nr', header: 'Magazine_nr', filter: {} },
            { dataIndex: 'magazine_date', header: 'Magazine_date', renderer: Ext.util.Format.dateRenderer('Y-m-d') },
            { dataIndex: 'magazine_emertim', header: 'Magazine_emertim', filter: {} },
            { dataIndex: 'artikull_nr', header: 'Artikull_nr', filter: {} },
            { dataIndex: 'artikulli_pershkrimi', header: 'Artikulli_pershkrimi', filter: {} },
            { dataIndex: 'sasia', header: 'Sasia', filter: {} },
            { dataIndex: 'Numerimi', header: 'Sasia ne GIS', filter: {} },
            { dataIndex: 'vlere', header: 'Vlere' },
        ];

        return new Ext.grid.ColumnModel({
            columns: columns.slice(start || 0, finish),
            defaults: {
                sortable: true
            }
        });
    };

    var grid = new Ext.grid.GridPanel({
        border: false,
        id: "GridaTeDhenaHyreseGis",
        store: storeTeDhenaHyreseGIS,
        autoScroll: true,
        disableSelection: true,
        viewConfig: {
            getRowClass: function (rec, rowIdx, params, store) {
                if (rec.data.sasia > rec.data.Numerimi)
                    return 'ngjyraGjelber';
                else
                    return 'ngjyraKuqe';
            }
        },
        colModel: createColModel(13),
        loadMask: true,
        plugins: [filters],
        listeners: {
            render: {
                fn: function () {
                    storeTeDhenaHyreseGIS.load({
                        params: {
                            start: 0,
                            limit: 20
                        }
                    });
                }
            }
        },
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            displayMsg: perkthe("GP_GRID_BBAR_MSG_displayMsg"),
            emptyMsg: perkthe("GP_GRID_BBAR_MSG_emptyMsg"),
            beforePageText: perkthe("GP_GRID_BBAR_MSG_beforePageText"),
            afterPageText: perkthe("GP_GRID_BBAR_MSG_afterPageText"),
            store: storeTeDhenaHyreseGIS,
            pageSize: 20,
            plugins: [filters]
        })
    });
    return grid;
};

function fshihDritareTeDhenaHyreseGis() {
    TeDhenaHyreseGisWindow.hide();
};

function afishoDritareTeDhenaHyreseGis() {
    if (!TeDhenaHyreseGisWindow)
        ndertoDritareTeDhenaHyreseGis();
    else
        TeDhenaHyreseGisWindow.show();
};

function ndertoDritareTeDhenaHyreseGis() {
    var gridTeDhenaHyreseGIS = krijoGridenTeDhenaHyreseTab1();
    var gridTeDhenaMagazinat = krijoGridenMagazinat();
    var gridTeDhenaZonat = krijoGridenZonat();

    var TeDhenaHyreseGisTabPanel = new Ext.TabPanel({
        activeTab: 0,
        items: [
            { layout: 'fit', title: 'Asete', xtype: 'panel', cls: 'popPanel' + varSettings.theme.color, items: [gridTeDhenaHyreseGIS] },
            { layout: 'fit', title: 'Zonat', xtype: 'panel', cls: 'popPanel' + varSettings.theme.color, items: [gridTeDhenaZonat] }
        ]
    })

    TeDhenaHyreseGisWindow = new Ext.Window({
        title: perkthe("GP_WIN_TIT_TeDhenaHyreseNgaAlpha"),
        layout: 'fit',
        closable: true,
        maximizable: true,
        constrain: true,
        x: 400,
        y: 100,
        width: 800,
        height: 500,
        closeAction: 'hide',
        collapsible: true,
        cls: 'popWindow' + varSettings.theme.color,
        items: TeDhenaHyreseGisTabPanel,
        listeners: {
            'close': function (win) {
            },
            'hide': function (win) {
                if (IMBBtn.pressed) IMBBtn.toggle();
            }
        }
    });
    TeDhenaHyreseGisWindow.doLayout();
    TeDhenaHyreseGisWindow.show();
};