Ext.namespace("GeoExt.plugins");
GeoExt.plugins.PrintExtentT = Ext.extend(GeoExt.plugins.PrintExtent, {
    idShkallaTextBox: null,
    idShkallaV: null,
    idQendraLatTextBox: null,
    idQendraLonTextBox: null,
    updateBox: function () {
        var page = this.page;
        this.control.active &&
                this.control.setFeature(page.feature, { rotation: -page.rotation });
        if (this.idShkallaTextBox) {

            Ext.getCmp(this.idQendraLatTextBox.toString()).setValue(this.pages[0].center.lat);
            Ext.getCmp(this.idQendraLonTextBox.toString()).setValue(this.pages[0].center.lon);
            Ext.getCmp(this.idShkallaTextBox.toString()).setValue("1 : " + page.scale.get("value"));
            Ext.getCmp(this.idShkallaV.toString()).setValue(page.scale.get("value"));
        }
    },
    fitPage: function () {
        if (this.page) {

            this.page.fit(this.map, { mode: "screen" });
        }
        if (this.idShkallaTextBox) {

            Ext.getCmp(this.idQendraLatTextBox.toString()).setValue(this.pages[0].center.lat);
            Ext.getCmp(this.idQendraLonTextBox.toString()).setValue(this.pages[0].center.lon);
            Ext.getCmp(this.idShkallaTextBox.toString()).setValue("1 : " + this.page.scale.get("value"));
            Ext.getCmp(this.idShkallaV.toString()).setValue(this.page.scale.get("value"));
        }
    }



});


