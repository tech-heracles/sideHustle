;

function myDxTreeList(emerGride,options) {
    this.Options = {
        dataSource: new Array(),
        keyExpr: "Id",
        wordWrapEnabled: true,
        autoExpandAll: true,
        showBorders: true,
        scrolling: {
            mode: "virtual"
        },
        editing: {
            mode: "cell",
            allowUpdating: true
        },
        columns: new Array(),
        onEditorPreparing: function (e) {
            if (e.parentType == "dataRow") {
                e.editorOptions.onFocusIn = function (args) {
                    args.element.find("input").select();
                }

            }
        }
    }

    this.Options = $.extend({}, this.Options, options);
    this.Grida = $("#" + emerGride).dxTreeList(this.Options).dxTreeList("instance");

    //percakton kolonat e grides sipas konfigurimit
    this.SetColumnsFromConfig = function (columnsConfig) {
        var columns = new Array();
        var column;
        for (var i = 0 ; i < columnsConfig.length; i++) {
            column = columnsConfig[i];
            columns.push({
                dataField: column.KodiTrupi,
                caption: column.PershkrimiTrupi,
                allowEditing: !column.ReadonlyTrupi,
                visible: column.VisibleTrupi,
                format: {
                    type: "fixedPoint",
                    precision: this.Options.formatNumriZgjedhur.ShifraPasPresjesVlefta || 2
                },
                editorOptions: this.Options.editorColumnOption
            });
        }
        this.Grida.option('columns', columns);
    };

    //shton opsion ne kolone
    this.AddCustomOptionToColumns = function (dataFields, option, value) {
        for (var i = 0; i < dataFields.length; i++)
            this.Grida.columnOption(dataFields[i], option, value);
    };

    //merr summary te nje kolone
    this.GetSummaryOfColumns = function (dataFields, option) {
        var summaryValue = 0;
        var ds = this.GetData();
        switch (option){
            case "sum":
                for (var i = 0; i < ds.length; i++)
                    for (var j = 0; j < dataFields.length; j++)
                        summaryValue += ds[i][dataFields[j]];
                break;
            default:
                break;
        }
        return summaryValue;
    }

    //ben refresh griden
    this.Refresh = function () {
        this.Grida.refresh();
    };
    
    //merr ne forme array, gjithe datasourcen e grides
    this.GetData = function () {
        return this.Grida.getDataSource().store()._array;
    };

    //ruan vlerat ne gjendjen aktuale ne gride
    this.SaveCurrentValues = function () {
        this.Grida.saveEditData();
    };

}