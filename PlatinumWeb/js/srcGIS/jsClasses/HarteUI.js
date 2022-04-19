function HarteUI(options)
{
    this.defaults = {
        paneliLayers: {}
    };
    var options = options ? options : {};
    this.settings = $.extend(true, {}, this.defaults, options);
}

HarteUI.prototype.inicializoGrupetDisable = function(options)
{
    var defaults = {};
    var options = options ? options : {};

    var settings = $.extend(true, {}, defaults, this.settings, options);
    if (settings.paneliLayers.idGroupDisableFirst) {
        if (settings.paneliLayersidGroupDisableFirst == settings.paneliLayers.idFolderContainerVersioni1)
        {
            var idEn = settings.paneliLayers.idFolderContainerVersioni2;
            var idDis = settings.paneliLayers.idFolderContainerVersioni1;
        }
        else
        {
            var idEn = settings.paneliLayers.idFolderContainerVersioni1;
            var idDis = settings.paneliLayers.idFolderContainerVersioni2;
        }
        tree.getNodeById("layercontainer_" + idDis).ui.toggleCheck(false);
        tree.getNodeById("layercontainer_" + idEn).ui.toggleCheck(true);

        tree.getNodeById("layercontainer_" + idDis).eachChild(function(child) {
            child.ui.toggleCheck(false);
            child.disable();
        });
    }
}

HarteUI.prototype.krijoFormenFiltersKerkim = function (options) {
    var defaults = {};
    var options = options ? options : {};
    var settings = $.extend(true, {}, defaults, options);
        
    if (!map.getControl("drawKerkimHapesinorPoligonContrId"))
        VizatoPoligonKerkimHapesinor(layer = '', workspace_name = '', urlservicewfs = '');
    if (!map.getControl("drawKerkimHapesinorVijeContrId"))
        VizatoVijeKerkimHapesinor(layer = '', workspace_name = '', urlservicewfs = '');
    if (!map.getControl("polygonControlRrethContrId"))
        VizatoPikeKerkimHapesinor(layer = '', workspace_name = '', urlservicewfs = '');

    if (!Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId")) {
        vizatoPoligonBtnKerkimHapesinor = new Ext.Button({
            enableToggle: true,
            toggleGroup: "panButton",
            id: "vizatoPoligonBtnKerkimHapesinorId",
            tooltipType: "title",
            tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerkoPol"),
            icon: varSettings.theme.urlCustom + 'searchSpatial.png',
            cls: "butoniKerkimHapesinor" + varSettings.theme.color,
            handler: function (toggled) {
                if (toggled) {
                    caktivizoButonatAktivizoButonin('', actions);
                    caktivizoKontrolletAktivizoKontrollin(drawKerkimHapesinorPoligon, vektoriKontrollet)
                }
            }
        });
        Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).add(Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId"));
        vektoriButonatKerkimHapesinor.push(Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId"));
    }
    else
        Ext.getCmp("vizatoPoligonBtnKerkimHapesinorId").show();
    
    Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).doLayout();

    if (!Ext.getCmp("vizatoVijeBtnKerkimHapesinorId")) {
        vizatoVijeBtnKerkimHapesinor = new Ext.Button({
            enableToggle: true,
            toggleGroup: "panButton",
            id: "vizatoVijeBtnKerkimHapesinorId",
            icon: varSettings.theme.urlCustom + 'searchSpatial.png',
            tooltipType: "title",
            tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerVije"),
            cls: "butoniKerkimHapesinor",
            cls: "butoniKerkimHapesinor" + varSettings.theme.color,
            handler: function (toggled) {
                if (toggled) {
                    caktivizoButonatAktivizoButonin('', actions);
                    caktivizoKontrolletAktivizoKontrollin(drawKerkimHapesinorVije, vektoriKontrollet)
                }
            }
        });

        Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).add(Ext.getCmp("vizatoVijeBtnKerkimHapesinorId"));
        vektoriButonatKerkimHapesinor.push(Ext.getCmp("vizatoVijeBtnKerkimHapesinorId"));
    }
    else
        Ext.getCmp("vizatoVijeBtnKerkimHapesinorId").show();
    Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).doLayout();

    if (!Ext.getCmp("vizatoPikeBtnKerkimHapesinorId")) {
        vizatoPikeBtnKerkimHapesinor = new Ext.Button({
            enableToggle: true,
            id: "vizatoPikeBtnKerkimHapesinorId",
            toggleGroup: "panButton",
            tooltipType: "title",
            tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerRR"),
            icon: varSettings.theme.urlCustom + 'searchSpatial.png',
            cls: "butoniKerkimHapesinor" + varSettings.theme.color,
            handler: function (toggled) {
                if (toggled) {
                    caktivizoButonatAktivizoButonin('', actions);
                    caktivizoKontrolletAktivizoKontrollin(polygonControlRreth, vektoriKontrollet)
                }
            }
        });
        Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).add(Ext.getCmp("vizatoPikeBtnKerkimHapesinorId"));
        vektoriButonatKerkimHapesinor.push(Ext.getCmp("vizatoPikeBtnKerkimHapesinorId"));
    }
    else
        Ext.getCmp("vizatoPikeBtnKerkimHapesinorId").show();
    Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).doLayout();

    if (!Ext.getCmp("fshiWfsBtnKerkimHapesinorId")) {
        var fshiWfsBtnKerkimHapesinor = new Ext.Button({
            id: "fshiWfsBtnKerkimHapesinorId",
            icon: varSettings.theme.urlCustom + 'fileDelete.png',
            tooltipType: "title",
            tooltip: perkthe("GP_KERKIMHAP_BTN_TOOLTIP_KerFshij"),
            cls: "butoniKerkimHapesinor" + varSettings.theme.color,
            handler: function (toggled) {
                if (toggled) {
                    caktivizoButonatAktivizoButonin('', actions);
                    caktivizoKontrolletAktivizoKontrollin('', vektoriKontrollet)
                    rregulloToggleKerkimHapesinor(vektoriButonatKerkimHapesinor);
                    if (InfoBtn.pressed) {
                        caktivizoKontrolletAktivizoKontrollin(featureInfo, vektoriKontrollet);                
                    }
                    if (matButton.pressed) {
                        matButton.toggle();
                    }
                    map.getLayersByName("drawingsPoligcaktivizoButonatAktivizoButoninon")[0].removeAllFeatures();
                    map.getLayersByName("drawingsVije")[0].removeAllFeatures();
                    map.getLayersByName("PoligonRrethKerkim")[0].removeAllFeatures();
                }
            }
        });
        Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).add(Ext.getCmp("fshiWfsBtnKerkimHapesinorId"));
    }
    else
        Ext.getCmp("fshiWfsBtnKerkimHapesinorId").show();
    Ext.getCmp(this.settings.paneliKerkimit.paneliMeButonatKerkimId).doLayout();

    Ext.getCmp(this.settings.paneliKerkimit.item1Id).add(Ext.getCmp(harteUI.settings.paneliKerkimit.idComboLayersFilter));
    Ext.getCmp(this.settings.paneliKerkimit.item1Id).doLayout();


    if (!Ext.getCmp("kolonatKerkimIdCmb")) {
        var kolonatKerkim = new Ext.form.ComboBox({
            id: "kolonatKerkimIdCmb",
            displayField: 'dispField',
            valueField: 'valField',
            typeAhead: true,
            layer: layer,
            colspan: 2,
            workspace_name: '',
            urlservicewfs: '',
            mode: 'local',
            forceSelection: true,
            triggerAction: 'all',
            emptyText: perkthe('GP_KERKIMHAP_COMBO_Kolonat'),
            selectOnFocus: true,
            disabled: true,
            matchFieldWidth: false,
            listWidth: 127,

            listeners: {
                select: function (combo, record, index) {
                    ZgjidhOperatoret(this.layer, combo.getValue(), this.workspace_name, this.urlservicewfs)
                }
            }
        });
        
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).add(kolonatKerkim);
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
    }
    else {
        Ext.getCmp('kolonatKerkimIdCmb').clearValue();
        Ext.getCmp('kolonatKerkimIdCmb').show();
    }
    
    if (!Ext.getCmp("kolonatOperatoretIdCmb")) {
        var arrayOperatoret = [["NOT_EQUAL_TO", "!="], ["LESS_THAN", "<"], ["LESS_THAN_OR_EQUAL_TO", "<="], ["GREATER_THAN", ">"], ["GREATER_THAN_OR_EQUAL_TO", ">="], ["LIKE", "=/~"]];

        var storeKolonatOperatoret = new Ext.data.ArrayStore({
            storeId: 'storeKolonatOperatoretId',
            fields: ['valField', 'dispField'],
            data: arrayOperatoret
        });
        var kolonatOperatoreKerkim = new Ext.form.ComboBox({
            store: storeKolonatOperatoret,
            id: "kolonatOperatoretIdCmb",
            displayField: 'dispField',
            valueField: 'valField',
            typeAhead: true,
            mode: 'local',
            forceSelection: true,
            triggerAction: 'all',
            emptyText: perkthe("GP_KERKIMHAP_COMBO_Operatoret"),
            selectOnFocus: true,
            disabled: true,
            listeners: {
                select: function (combo, record, index) {
                    krijoFormenRezultat(this.layer, this.kolona, combo.getValue(), this.workspace_name, this.urlservicewfs)
                }
            }
        });

        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).add(kolonatOperatoreKerkim);
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
    }
    else {
        Ext.getCmp('kolonatOperatoretIdCmb').clearValue();
        Ext.getCmp('kolonatOperatoretIdCmb').show();
        Ext.getCmp('kolonatOperatoretIdCmb').bindStore(storeKolonatOperatoret);
    }
    
    if (!Ext.getCmp("searchText")) {
        var textFieldSearchTab = new Ext.form.TextField({
            id: "searchText",
            allowBlank: true,
            width: "65%",
            colspan: 3,
            cls: 'textFieldKerkimiTabelarCss',
            disabled: true
        });
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).add(textFieldSearchTab);
    }

    else {
        Ext.getCmp("searchText").show();
        Ext.getCmp("searchText").setValue('');
    }
    if (!Ext.getCmp("searchBtnTabid")) {
        var kerkoTabBtn = new Ext.Button({
            scale: "medium",
            text: perkthe("GP_KERKIMHAP_BTN_TEXT_Kerko"),
            id: "searchBtnTabid",
            tooltipType: "title",
            width: '90%',
            colspan: 1,
            tooltip: perkthe("GP_KERKIMHAP_BTN_TEXT_Kerko"),
            handler: function () {
                KrijoGride();
            }
        });

        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).add(kerkoTabBtn);
    }
    else {
        Ext.getCmp("searchBtnTabid").show();
    }

    if (!Ext.getCmp("anulloKerkimBtnTabid")) {
        var anulloKerkimBtn = new Ext.Button({
            scale: "medium",
            text: perkthe("GP_KERKIMHAP_BTN_TEXT_KerFshij"),
            id: "anulloKerkimBtnTabid",
            tooltipType: "title",
            width: '50%',
            colspan: 2,
            tooltip: perkthe("GP_KERKIMHAP_BTN_TEXT_KerFshij"),
            handler: function () {
                anullo();
            }
        });

        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
        Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).add(anulloKerkimBtn);
    }
    else {
        Ext.getCmp("anulloKerkimBtnTabid").show();
        Ext.getCmp("anulloKerkimBtnTabid").handler = function () {
            anullo();
        };
    }

    Ext.getCmp(harteUI.settings.paneliKerkimit.item1Id).doLayout();
};


GISElements = function () {
    this.defaultWindowProperties = {
        title: "",
        layout: 'form',
        constrain: true,
        modal: false,
        header: true,
        floating: { shadow: false },
        resizable: true,
        maximizable: false,
        collapsible: true,
        closable: true,
        closeAction: 'destroy',
        border: true,
        bodyBorder: false,
        hideBorders: false,
        animateTarget: "",
        showAnimDuration: 0.25,
        cls: '',
        items: []
    };

    this.defaultTabPanelProperties = {
        title: "",
        renderTo: Ext.getBody(),
        plain: true,
        activeTab: 0,
        enableTabScroll: true,
        layoutOnTabChange: true,
        cls: '',
        items: []
    };

    this.defaultFormPanelProperties = {
        title: "",
        layout: 'form',
        frame: true,
        monitorValid: true,
        msgTarget: 'side',
        width: 'auto',
        height: 'auto',
        bodyStyle: '',
        defaults: {
            width: 'auto',
            allowBlank: true
        },
        buttonAlign: 'center',
        items: []
    };

    this.defaultPanelProperties = {
        title: "",
        border: false,
        layout: 'form',
        closable: true,
        resizable: true,
        width: 'auto',
        height: 'auto',
        region: 'center',
        cls: '',
        html: '',
        tbar: [],
        bbar: []
    };

    this.defaultButtonProperties = {
        tooltipType: "title"
    };

    this.defaultFileUploadField = {
        hideLabel: true,
        buttonOnly: false,
        buttonText: ''        
    };
};

GISElements.prototype = {
    constructor: GISElements,

    // Krijimi i cdo Objekti
    createGISElement: function (element, options) {
        gisElement = this.getGISElement(options.id);

        if (gisElement == undefined)
            return this.performCreateGISElement(element, options);
        else 
            switch (element) {
                case "Window":
                    this.showGISElement(options.id);
                    return;
                default:
                    return gisElement;
            };
    },

    // Krijohet Objekti nese nuk ekziston
    performCreateGISElement: function (element, options) {
        switch (element) {
            case "Window":
                this.createExtWindow(options);
                this.showGISElement(options.id);
                return;
            case "TabPanel":
                return this.createExtTabPanel(options);
            case "FormPanelBasic":
                return this.createExtFormPanelBasic(options);
            case "FormPanel":
                return this.createExtFormPanel(options);
            case "Panel":
                return this.createExtPanel(options);
            case "Button":
                return this.createExtButton(options);
            case "FileUploadField":
                return this.createFileUploadField(options);
            default:
                return undefined;
        };
    },

    // Kontrollon ekzistencen e Objektit dhe kthen true/false
    existsGISElement: function (id) {
        if (Ext.getCmp(id) != undefined)
            return true;
        else
            return false;
    },

    // Shfaq Objektin e kerkuar
    showGISElement: function (id) {
        if (this.existsGISElement(id))
            Ext.getCmp(id).show();
    },

    // Fshin Objektin e kerkuar
    destroyGISElement: function (id) {
        if (this.existsGISElement(id))
            Ext.getCmp(id).destroy();
    },

    // Gjen dhe kthen objektin e kerkuar
    getGISElement: function (id) {
        return Ext.getCmp(id);
    },

    // Ben Merge Properties Default te Objektit qe kerkohet te krijohet me Properties e modifikuar sipas rastit
    prepareGISElementProperties: function (defaultProperties, options) {
        var options = options ? options : {};
        var properties = $.extend(true, {}, defaultProperties, options);
        return properties;
    },

    // Ndryshon properties default te ngjyres sipas objektit te kerkuar
    addDefaultColor: function (color, optionCls) {
        var classCss = color + (optionCls ? (' ' + optionCls) : '');
        return classCss;
    },


    // Krijimi i Windows
    createExtWindow: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultWindowProperties, options);
        properties.cls = this.addDefaultColor('popWindow' + varSettings.theme.color, options.cls);
        new Ext.Window(properties).doLayout();
    },

    // Krijimi i TabPanel
    createExtTabPanel: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultTabPanelProperties, options);
        return new Ext.TabPanel(properties);
    },

    // Krijimi i FormPanel pa bere Layout ne fund
    createExtFormPanelBasic: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultFormPanelProperties, options);
        return new Ext.FormPanel(properties);
    },
    
    // Krijimi i FormPanel
    createExtFormPanel: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultFormPanelProperties, options);
        return new Ext.FormPanel(properties).doLayout();
    },

    // Krijimi i Panelet
    createExtPanel: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultPanelProperties, options);
        properties.cls = this.addDefaultColor('popPanel' + varSettings.theme.color, options.cls);
        return new Ext.Panel(properties);
    },

    // Krijimi i Butonave
    createExtButton: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultButtonProperties, options);
        return new Ext.Button(properties);
    },

    // Krijimi i FileUploadField
    createFileUploadField: function (options) {
        var properties = this.prepareGISElementProperties(this.defaultFileUploadField, options);
        return new Ext.ux.form.FileUploadField(properties);
    },
};

