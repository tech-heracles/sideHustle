;
/**
 * Copyright (c) 2008-2009 The Open Source Geospatial Foundation
 * 
 * Published under the BSD license.
 * See http://svn.geoext.org/core/trunk/geoext/license.txt for the full text
 * of the license.
 */

Ext.namespace("GeoExt.ux")

/*
 * @include GeoExt/data/PrintPage.js
 * @include GeoExt/plugins/PrintExtent.js
 * @include GeoExt/plugins/PrintProviderField.js
 * @include GeoExt/plugins/PrintPageField.js
 */

/** api: (define)
 *  module = GeoExt.form
 *  class = MultiPagePrint
 *  base_link = `Ext.form.FormPanel <http://extjs.com/deploy/dev/docs/?class=Ext.form.FormPanel>`_
 */

/** api: constructor
 *  .. class:: MultiPagePrint
 * 
 *  An instance of this form creates a multiplu print page. Layout, DPI, scale
 *  and rotation are configurable in the form. A Print button is also added to
 *  the form. User can add pages and configure specific parameters such as
 *  title and comment for each page.
 */
GeoExt.ux.MultiPagePrintT = Ext.extend(GeoExt.ux.MultiPagePrint, {
    legendTitleText: "Shpjegues",
    legendValue: "Shpjegues",
    logoNdermarrjeUrl: "",
    vendosLogonText: "Vendos logo",
    vendosShpjeguesText: "Vendos shpjegues",
    GP_MSG_TIT_kujdes_text: "Kujdes",
    GP_MSG_Text_NoLogoUrlPrintim_text: "Nuk ekziston logo per nderrmarrjen tuaj",

    initForm: function () {
        var p = this.printExtent.printProvider;
        var hideUnique = this.initialConfig.hideUnique !== false;
        !(hideUnique && p.layouts.getCount() <= 1) && this.add({
            xtype: "combo",
            fieldLabel: this.layoutText,
            store: p.layouts,
            displayField: "name",
            typeAhead: true,
            mode: "local",
            forceSelection: true,
            triggerAction: "all",
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintProviderField()
        });
        !(hideUnique && p.dpis.getCount() <= 1) && this.add({
            xtype: "combo",
            fieldLabel: this.dpiText,
            store: p.dpis,
            displayField: "name",
            typeAhead: true,
            mode: "local",
            forceSelection: true,
            triggerAction: "all",
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintProviderField()
        });

        !(hideUnique && p.dpis.getCount() <= 1) && this.add({
            xtype: "textarea",
            fieldLabel: this.legendTitleText,
            name: "legendTitle",
            value: this.legendValue,
            hidden: true,
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintProviderField()
        });

        this.pagesPanel = new Ext.TabPanel({
            title: 'the pages form',
            enableTabScroll: true,
            plain: true,
            layoutOnTabChange: true
        });

        this.add(new Ext.Button({
            text: this.addPageText,
            handler: this.addPage,
            scope: this
        }));

        this.add(this.pagesPanel);

        this.addButton({
            text: this.printText,
            handler: function () {
                this.printExtent.print(this.printOptions);
            },
            scope: this
        });

        this.doLayout();

        if (this.autoFit === true) {
            this.onMoveend();
            this.mapPanel.map.events.on({
                "moveend": this.onMoveend,
                scope: this
            })
        }
        this.addPage();

        this.printExtent.on('selectpage', function (page) {
            for (var i = 0, len = this.printPages.length; i < len; ++i) {
                if (this.printPages[i] == page) {
                    this.pagesPanel.setActiveTab(i);
                    break;
                }
            }
        }, this);

        this.pagesPanel.on({
            'beforeremove': function (container, panel) {
                // don't remove the last tab
                if (container.items.length <= 1) {
                    return false;
                }
            }
        });
    },

    addPageForm: function (page) {
        var p = this.printExtent.printProvider;
        var hideUnique = this.initialConfig.hideUnique !== false;
        var pageTab = gisElements.createGISElement("Panel", { title: this.pageText, autoHeight: true });

        // add custom fields to the form
        pageTab.add({
            xtype: "textfield",
            name: "mapTitle",
            fieldLabel: this.titleFieldLabel,
            value: this.defaultTitleText,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            }),
            listeners: {
                valid: function (field) {
                    pageTab.setTitle(field.getValue());
                }
            }
        });
        pageTab.add({
            xtype: "textarea",
            fieldLabel: this.commentFieldLabel,
            name: "comment",
            value: this.defaultCommentText,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            })
        });

        !(hideUnique && p.scales.getCount() <= 1) && pageTab.add({
            xtype: "combo",
            fieldLabel: this.scaleText,
            store: p.scales,
            displayField: "name",
            typeAhead: true,
            mode: "local",
            forceSelection: true,
            triggerAction: "all",
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            })
        });
        pageTab.initialConfig.hideRotation !== true && pageTab.add({
            xtype: "textfield",
            fieldLabel: this.rotationText,
            name: "rotation",
            enableKeyEvents: true,
            width: this.fieldsWidth,
            validator: function (v) {
                return !isNaN(v)
            },
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            })
        });

        pageTab.add({
            xtype: "textarea",
            fieldLabel: "logo nderrmarrje",
            name: "logoNdermarrjeUrl",
            value: this.logoNdermarrjeUrl,
            hidden: true,
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            })
        });
        pageTab.add({
            xtype: "checkbox",
            fieldLabel: this.vendosLogonText,
            name: "vendosLogoPrintim",
            logoNdermarrjeUrl: this.logoNdermarrjeUrl,
            GP_MSG_TIT_kujdes_text: this.GP_MSG_TIT_kujdes_text,
            GP_MSG_Text_NoLogoUrlPrintim_text: this.GP_MSG_Text_NoLogoUrlPrintim_text,
            value: true,
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            }),
            listeners: {
                check: function (checkbox, isChecked) {
                    if (isChecked && !checkbox.logoNdermarrjeUrl) {
                        checkbox.setValue(false);
                        Ext.MessageBox.alert(checkbox.GP_MSG_TIT_kujdes_text, checkbox.GP_MSG_Text_NoLogoUrlPrintim_text);
                    }
                },
                beforerender: function (checkbox) {
                    if (checkbox.logoNdermarrjeUrl) {
                        checkbox.setValue(true);
                    } else {
                        checkbox.setValue(false);
                    }
                }
            }
        });

        pageTab.add({
            xtype: "checkbox",
            fieldLabel: this.legendTitleText,
            name: "vendosLegendPrintim",
            value: true,
            selectOnFocus: true,
            width: this.fieldsWidth,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            }),
            listeners: {
                check: function (checkbox, isChecked) {
                    varSettings.printimiPdf.meShpjegues = isChecked ? 1 : 0;
                },
                beforerender: function (checkbox) {
                    checkbox.setValue(Boolean(varSettings.printimiPdf.meShpjegues));
                }
            }
        });

        pageTab.on({
            'activate': function (panel) {
                this.printExtent.selectPage(page);
            },
            'destroy': function (panel) {
                this.printExtent.removePage(page);
            },
            scope: this
        });
        this.pagesPanel.add(pageTab);
        this.doLayout();
        this.pagesPanel.setActiveTab(pageTab);
    },

    beforeDestroy: function () {
        var p = this.printExtent.printProvider;
        p.un("beforePrint", this.busyMask.show, this.busyMask);
        p.un("print", this.busyMask.hide, this.busyMask);
        if (this.autoFit === true) {
            this.mapPanel.map.events.un({
                "moveend": this.onMoveend,
                scope: this
            })
        }
        this.busyMask.hide;
        map.removeLayer(this.layer);
    },

    tearDown: function () {
    }
});

/** api: xtype = gxux_multipageprint */
Ext.reg("gxux_multipageprint", GeoExt.ux.MultiPagePrintT);