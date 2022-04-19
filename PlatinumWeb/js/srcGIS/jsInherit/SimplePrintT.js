/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


GeoExt.ux.SimplePrintT = Ext.extend(GeoExt.ux.SimplePrint, {



    /** private: method[initForm]
     *  Creates and adds items to the form.
     */
    initForm: function () {
        this.mapPanel.initPlugin(this.printExtent);
        var p = this.printExtent.printProvider;
        var hideUnique = this.initialConfig.hideUnique !== false;
        var cbOptions = this.comboOptions || {
            typeAhead: true,
            selectOnFocus: true
        };

        !(hideUnique && p.layouts.getCount() <= 1) && this.add(Ext.apply({
            xtype: "combo",
            fieldLabel: this.layoutText,
            store: p.layouts,
            forceSelection: true,
            displayField: "name",
            mode: "local",
            triggerAction: "all",
            plugins: new GeoExt.plugins.PrintProviderField({
                printProvider: p
            })
        }, cbOptions));
        !(hideUnique && p.dpis.getCount() <= 1) && this.add(Ext.apply({
            xtype: "combo",
            fieldLabel: this.dpiText,
            store: p.dpis,
            forceSelection: true,
            displayField: "name",
            mode: "local",
            triggerAction: "all",
            plugins: new GeoExt.plugins.PrintProviderField({
                printProvider: p
            })
        }, cbOptions));
        !(hideUnique && p.scales.getCount() <= 1) && this.add(Ext.apply({
            xtype: "combo",
            fieldLabel: this.scaleText,
            store: p.scales,
            forceSelection: true,
            displayField: "name",
            mode: "local",
            triggerAction: "all",
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: this.printPage
            })
        }, cbOptions));
        this.initialConfig.hideRotation !== true && this.add({
            xtype: "numberfield",
            fieldLabel: this.rotationText,
            name: "rotation",
            enableKeyEvents: true,
            plugins: new GeoExt.plugins.PrintPageField({
                printPage: this.printPage
            })
        });

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
            });
        }
    }





});