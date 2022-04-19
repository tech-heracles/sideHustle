/**
 * Copyright (c) 2008-2009 The Open Source Geospatial Foundation
 * 
 * Published under the BSD license.
 * See http://svn.geoext.org/core/trunk/geoext/license.txt for the full text
 * of the license.
 */

;Ext.namespace("GeoExt.ux")

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
GeoExt.ux.MultiPagePrint = Ext.extend(Ext.form.FormPanel, {
    
    /* begin i18n */
    /** api: config[layoutText] ``String`` i18n */
    layoutText: "Layout",
    /** api: config[dpiText] ``String`` i18n */
    dpiText: "DPI",
    /** api: config[scaleText] ``String`` i18n */
    scaleText: "Scale",
    /** api: config[rotationText] ``String`` i18n */
    rotationText: "Rotation",
    /** api: config[printText] ``String`` i18n */
    printText: "Print",
    /** api: config[creatingPdfText] ``String`` i18n */
    creatingPdfText: "Creating PDF...",
    /** api: config[pageText] ``String`` i18n */
    pageText: "Page",
    /** api: config[addPageText] ``String`` i18n */
    addPageText: "Add page",
    /** api: config[titleFieldLabel] ``String`` i18n */
    titleFieldLabel: "Title",
    /** api: config[commentFieldLabel] ``String`` i18n */
    commentFieldLabel: "Comment",
    /** api: config[defaultTitleText] ``String`` i18n */
    defaultTitleText: "A custom title",
    /** api: config[defaultCommentText] ``String`` i18n */
    defaultCommentText: "A custom comment",
    /* end i18n */
   
    /** api: config[printProvider]
     *  :class:`GeoExt.data.PrintProvider` The print provider this form
     *  is connected to.
     */
    
    /** api: config[mapPanel]
     *  :class:`GeoExt.MapPanel` The map panel that this form should be
     *  connected to.
     */
    
    /** api: config[layer]
     *  ``OpenLayers.Layer`` Layer to render page extents and handles
     *  to. Useful e.g. for setting a StyleMap. Optional, will be auto-created
     *  if not provided.
     */

    /** api: config[printOptions]
     *  ``Object`` Optional options for the printProvider's print command.
     */

    /** api: property[printOptions]
     *  ``Object`` Optional options for the printProvider's print command.
     */
    printOptions: null,
    
    /** api: config[fieldsWidth]
     *  ``Object``Optional width to give to the form fields. We use an 
     *  config option here because using defaults will not work with
     *  the form fields inside the tanpanel items. Defaults to 150.
     */
    fieldsWidth: 150,

    /** api: property[printOptions]
     *  ``Object`` Optional options for the printProvider's print command.
     */
    printOptions: null,
    
    /** api: config[hideUnique]
     *  ``Boolean`` If set to false, combo boxes for stores with just one value
     *  will be rendered. Default is true.
     */
    
    /** api: config[hideRotation]
     *  ``Boolean`` If set to true, the Rotation field will not be rendered.
     *  Default is false.
     */
    
    /** api: config[busyMask]
     *  ``Ext.LoadMask`` A LoadMask to use while the print document is
     *  prepared. Optional, will be auto-created with ``creatingPdfText` if
     *  not provided.
     */
    
    /** private: property[busyMask]
     *  ``Ext.LoadMask``
     */
    busyMask: null,

    /** private: property[pagesPanel]
     *  ``Ext.TabPanel``
     */
    pagesPanel: null,
   
    // FIXME required ?
    /** private: property[printExtent]
     *  :class:`GeoExt.plugins.PrintExtent`
     */
    printExtent: null,
    
    // FIXME: required ? do we need multiple printPages ?
    /** api: property[printPages]
     *  ``Array`` Array of :class:`GeoExt.data.PrintPage`
     *  The print pages for this form.
     */
    printPages: null,
   
    /** private: method[initComponent]
     */
    initComponent: function() {
        GeoExt.ux.MultiPagePrint.superclass.initComponent.call(this);
        
        this.printExtent = new GeoExt.plugins.PrintExtent({
            printProvider: this.initialConfig.printProvider,
            layer: this.initialConfig.layer
        });
        this.printPages = this.printExtent.pages;

        this.mapPanel.initPlugin(this.printExtent);

        if (!this.busyMask) {
            this.busyMask = new Ext.LoadMask(Ext.getBody(), {
                msg: this.creatingPdfText
            });
        }

        this.printExtent.printProvider.on({
            "beforeprint": this.busyMask.show,
            "print": this.busyMask.hide,
            scope: this.busyMask
        });

        if(this.printExtent.printProvider.capabilities) {
            this.initForm();
        } else {
            this.printExtent.printProvider.on({
                "loadcapabilities": this.initForm,
                scope: this,
                single: true
            });
        }        

        //for accordion
        this.on('expand', this.setUp, this);
        this.on('collapse', this.tearDown, this);

        //for tabs
        this.on('activate', this.setUp, this);
        this.on('deactivate', this.tearDown, this);

        //for manual enable/disable
        this.on('enable', this.setUp, this);
        this.on('disable', this.tearDown, this);

        //for use in an Ext.Window with closeAction close
        this.on('destroy', this.tearDown, this);
    },
    
    /** private: method[initForm]
     *  Creates and adds items to the form.
     */
    initForm: function() {
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
            handler: function() {
                this.printExtent.print(this.printOptions);
            },
            scope: this
        });

        this.doLayout();

        if(this.autoFit === true) {
            this.onMoveend();
            this.mapPanel.map.events.on({
                "moveend": this.onMoveend,
                scope: this
            })
        }
        this.addPage();

        this.printExtent.on('selectpage', function(page) {
            for(var i=0, len=this.printPages.length; i<len; ++i) {
                if(this.printPages[i] == page) {
                    this.pagesPanel.setActiveTab(i);
                    break;
                }
            }
        }, this);

        this.pagesPanel.on({
            'beforeremove': function(container, panel) {
                // don't remove the last tab
                if (container.items.length <= 1) {
                    return false;
                }
            }
        });
    },

    /** private: method[addPage]
     *  Adds a page to the print Extent and adds the corresponding
     *     tab in the form.
     */
    addPage: function() {
        var page = this.printExtent.addPage();
        this.addPageForm(page);
    },

    /** private: method[addPageForm]
     *  Adds a panel in the tab panel for the given page
     *  :param page: :class:`GeoExt.data.PrintPage`
     */
    addPageForm: function(page) {

        var p = this.printExtent.printProvider;
        var hideUnique = this.initialConfig.hideUnique !== false;

        var pageTab = new Ext.Panel({
            title: this.pageText,
            layout: 'form',
            cls: 'popPanel' + varSettings.theme.color,
            autoHeight: true,
            closable: true
        });
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
                valid: function(field) {
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
            validator: function(v) {
                return !isNaN(v)
            },
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: page
            })
        });

        pageTab.on({
            'activate': function(panel) {
                this.printExtent.selectPage(page);
            },
            'destroy': function(panel) {
                this.printExtent.removePage(page);
            },
            scope: this
        });

        this.pagesPanel.add(pageTab);
        this.doLayout();
        this.pagesPanel.setActiveTab(pageTab);
    },
    
    /** private: method[onMoveend]
     *  Handler for the map's moveend event
     */
    onMoveend: function() {
        // FIXME, now we have multiple pages
        //this.printPage.fit(this.mapPanel.map);
    },
    
    /** private: method[beforeDestroy]
     */
    beforeDestroy: function() {
        var p = this.printExtent.printProvider;
        p.un("beforePrint", this.busyMask.show, this.busyMask);
        p.un("print", this.busyMask.hide, this.busyMask);
        if(this.autoFit === true) {
            this.mapPanel.map.events.un({
                "moveend": this.onMoveend,
                scope: this
            })
        }
        GeoExt.ux.MultiPagePrint.superclass.beforeDestroy.apply(this, arguments);
    },

    /** private: method[setUp]
     * Handler for the panel's expand/activate/enable event
     */
    setUp: function() {
        this.printExtent.setUp();
    },

    /** private: method[tearDown]
     * Handler for the panel's collapse/deactivate/disable/destroy event
     */
    tearDown: function() {
        this.printExtent.tearDown();
       
    }
});

/** api: xtype = gxux_multipageprint */
Ext.reg("gxux_multipageprint", GeoExt.ux.MultiPagePrint);
