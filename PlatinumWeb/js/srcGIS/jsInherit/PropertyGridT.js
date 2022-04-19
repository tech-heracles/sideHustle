/*Ext.grid.PropertyStoreT = Ext.extend(Ext.grid.PropertyStore, {
    
     setSource : function(o){
        this.source = o;
        this.store.removeAll();
        var data = [];
        
        for(var k in o){
      
         // if(!o[k])alert(k+"teuta")
            if(this.isEditableValue(o[k])){
                data.push(new Ext.grid.PropertyRecord({name: k, value: o[k]}, k));
            }
         
        }
        this.store.loadRecords({records: data}, {}, true);
    },
            isEditableValue: function(val){
        return Ext.isPrimitive(val) || Ext.isDate(val);//ketu do bere qe te marre edhe atributet qe jane bosh
    },
    
    
    
})*/
Ext.grid.PropertyGridT = Ext.extend(Ext.grid.PropertyGrid, {
    
      initComponent : function(){
        this.customRenderers = this.customRenderers || {};
        this.customEditors = this.customEditors || {};
        this.lastEditRow = null;
        var store = new Ext.grid.PropertyStore(this);
        this.propStore = store;
        var cm = new Ext.grid.PropertyColumnModel(this, store);
      //  store.store.sort('name', 'DESC');//per kete e trashegojme qe ti heqim renditjen
       store.store.sort('none');
        this.addEvents(
            /**
             * @event beforepropertychange
             * Fires before a property value changes.  Handlers can return false to cancel the property change
             * (this will internally call {@link Ext.data.Record#reject} on the property's record).
             * @param {Object} source The source data object for the grid (corresponds to the same object passed in
             * as the {@link #source} config property).
             * @param {String} recordId The record's id in the data store
             * @param {Mixed} value The current edited property value
             * @param {Mixed} oldValue The original property value prior to editing
             */
            'beforepropertychange',
            /**
             * @event propertychange
             * Fires after a property value has changed.
             * @param {Object} source The source data object for the grid (corresponds to the same object passed in
             * as the {@link #source} config property).
             * @param {String} recordId The record's id in the data store
             * @param {Mixed} value The current edited property value
             * @param {Mixed} oldValue The original property value prior to editing
             */
            'propertychange'
        );
        this.cm = cm;
        this.ds = store.store;
        Ext.grid.PropertyGrid.superclass.initComponent.call(this);

		this.mon(this.selModel, 'beforecellselect', function(sm, rowIndex, colIndex){
            if(colIndex === 0){
                this.startEditing.defer(200, this, [rowIndex, 1]);
                return false;
            }
        }, this);
    }  
    
    
})

Ext.reg("propertygridT", Ext.grid.PropertyGridT);
