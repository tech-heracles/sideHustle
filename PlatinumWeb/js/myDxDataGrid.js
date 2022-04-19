;
function myDxDataGrid(emerGride, options, noInstanciate) {
    this.Options = {
        customData: {},
        gridaObj: this,
        dataSource: new DevExpress.data.ArrayStore(),
        showBorders: true,
        addDeleteRowCommand: false,
        addEditRowCommand: false,
        cancelEditDataCommand: false,
        deleteMethod: undefined,
        allowNewRowAdding: true,
        editorColumnOption: { step: 0 },
        showRowNumber: false,
        lastFocusedCell: null,
        formatNumriZgjedhur: {},
        formatoVleraNumerike: true,
        allowColumnResizing: true,
        setColumnWidth: false,
        columnResizingMode: "widget",
        searchPanel: {
            visible: true
        },
        scrolling: {
            mode: "virtual"
        },
        editing: {
            mode: "cell",
            allowUpdating: true
        },
        grouping: {
            autoExpandAll: true,
            expandMode: "rowClick"
        },
        summary: {
            groupItems: {},
            totalItems: {}
        },
        sorting: {
            mode: "multiple"
        },
        onCellClick: function (el) {
            if (el.rowType == "group") {
                var gridaObject = this._options.gridaObj;
                gridaObject.SaveCurrentValues();
                if (el.row.isExpanded)
                    gridaObject.Grida.collapseRow(el.key);
                else
                    gridaObject.Grida.expandRow(el.key);
            }
        },
        onEditorPreparing: function (e) {
            this._options.gridaObj.SetSelectionOnFocus(e);
        }
    };
    this.Options = $.extend({}, this.Options, options);

    //percakton kolonat e grides sipas konfigurimit
    this.SetColumnsFromConfig = function (columnsConfig, commandColumn) {
        var columns = new Array();
        var column;

        if (commandColumn)
            columns.push(commandColumn);

        for (var i = 0; i < columnsConfig.length; i++) {
            column = columnsConfig[i];
            columns.push({
                dataField: column.KodiTrupi,
                caption: column.PershkrimiTrupi,
                allowEditing: !column.ReadonlyTrupi,
                visible: column.VisibleTrupi,
                visibleIndex: column.IndexTrupi ? column.IndexTrupi : i,
                format: ((!column.TipiFushes || column.TipiFushes == "number") && this.Options.formatoVleraNumerike) ? {
                    type: "fixedPoint",
                    precision: this.Options.formatNumriZgjedhur.ShifraPasPresjesVlefta || 2
                } : column.TipiFushes && column.TipiFushes.toLowerCase() == "datetime" ? "dd/MM/yyyy hh:mm:ss a" :
                    column.TipiFushes && column.TipiFushes.toLowerCase() == 'date' ? 'dd/MM/yyyy' :
                    undefined,
                showInColumnChooser: column.VisibleCostumize,
                editorOptions: this.Options.editorColumnOption,
                dataType: column.TipiFushes,
                sortOrder: column.sortOrder ? column.sortOrder : "",
                width: this.Options.setColumnWidth ? ((column.WidthTrupi == 0 ? 1 : column.WidthTrupi) + "%") : null
            });
        }

        if (this.Options.addDeleteRowCommand || this.Options.addEditRowCommand) {
            this.AddCommands(columns, this.Options.addEditRowCommand, this.Options.addDeleteRowCommand, this.Options.cancelEditDataCommand);
        }

        this.Grida.option('columns', columns);
    };

    this.SetNativeColumns = function (columns) {
        var grida = this;
        if (this.Options.showRowNumber) {
            columns.unshift({
                cellTemplate: function (cellElement, cellInfo) {
                    cellElement.text((grida.Grida.pageSize() * grida.Grida.pageIndex()) + (cellInfo.row.rowIndex + 1));
                },
                width: "auto",
                cssClass: "grayBackground",
                allowFiltering: false,
                allowExporting: false,
                allowSorting: false,
                allowReordering: false,
                allowResizing: false,
                allowGrouping: false,
                allowFixing: false,
                showInColumnChooser: false
            });
        }

        this.Grida.option('columns', columns);
    };

    //percakton grupimet e kolonave
    this.SetGroupingColumnsByOrder = function (columns) {
        for (var i = 0; i < columns.length; i++)
            this.Grida.columnOption(columns[i], 'groupIndex', i);
    };

    //percakton kolonat per shuma
    this.SetSummaryColumns = function (sumColumns, summaryType) {
        var summaryColumns = new Array();
        for (var i = 0; i < sumColumns.length; i++) {
            summaryColumns.push({
                column: sumColumns[i],
                summaryType: summaryType,
                alignByColumn: true,
                displayFormat: "{0}",
                valueFormat: {
                    type: "fixedPoint",
                    precision: this.Options.formatNumriZgjedhur.ShifraPasPresjesVlefta || 2
                }
            });
        }
        var summaryOption = this.Grida.option('summary.groupItems');
        this.Grida.option('summary.groupItems', $.extend({}, summaryOption, summaryColumns));
    };

    this.AddDataSourceToColumn = function (columnDataField, dataSource, valueField, displayField, allowClearing, withDataSourceFiltering, basedOnDataField) {
        var lookup = {
            dataSource: !withDataSourceFiltering ? { store: dataSource } : function (options) {
                return {
                    store: dataSource,
                    filter: options.data ? [basedOnDataField, "=", options.data[basedOnDataField]] : null
                };
            },
            valueExpr: valueField,
            displayExpr: displayField,
            allowClearing: allowClearing
        };
        this.Grida.columnOption(columnDataField, 'lookup', lookup);
    };

    //shton opsion ne kolone
    this.AddCustomOptionToColumns = function (dataFields, option, value) {        
        for (var i = 0; i < dataFields.length; i++)
            this.Grida.columnOption(dataFields[i], option, value);
    };

    this.AddCustomSortingToColumns = function (sortingConfig, option) {
        var dataFields = sortingConfig.split(",");
        for (var i = 0; i < dataFields.length; i++) {
            var columnConfig = dataFields[i].split(" ");
            this.Grida.columnOption(columnConfig[0], option, columnConfig[1] == "Ascending" ? "asc" : "desc");
        }
    };

    this.AddAutocompleteToColumn = function (dataField, idField, descriptionField, dataSourceWebService, placeHolder, dataSourceValueField, dataSourceTextField, dataSourceDescField, lupaFunction, onValueChangeFunction, listColumns) {
        var grida = this;
        var autocomplete = function (container, cellInfo) {
            container.dxAutocomplete({
                dataSource: grida.GetDataSourceFromWebservice(dataSourceWebService, cellInfo.rowIndex),
                valueExpr: dataSourceTextField,
                placeholder: placeHolder,
                value: cellInfo.value,
                tabindex: 1,
                focusStateEnabled: true,
                selected: false,
                onFocusIn: function (e) {
                    e.element.find("input").select();
                },
                onItemClick: function (e) {
                    grida.VendosVleraNeDataSource(cellInfo.rowIndex, dataField, idField, descriptionField, e.itemData, dataSourceTextField, dataSourceValueField, dataSourceDescField, onValueChangeFunction);
                    grida.lastFocusedCell = { rowIndex: cellInfo.rowIndex, dataField: dataField };
                    //grida.Grida.closeEditCell();
                    this.selected = true;
                    var cell = grida.Grida.getCellElement(cellInfo.rowIndex, dataField);
                    grida.Grida.focus(cell);
                },
                onFocusOut: function (e) {
                    if (this.selected) {
                        this.selected = false;
                        return;
                    }
                    if (!e.component._options.value) {
                        grida.VendosVleraNeDataSource(cellInfo.rowIndex, dataField, idField, descriptionField, null, dataSourceTextField, dataSourceValueField, dataSourceDescField, onValueChangeFunction);
                    }
                    if (e.component._dataSource && e.component._dataSource._items) {
                        var objekti = e.component._dataSource._items.filter(function (item) { return item[dataSourceTextField] == e.component._options.value || item[dataSourceDescField] == e.component._options.value; })[0];
                        if (!objekti) {
                            if (grida.GetData()[cellInfo.rowIndex][dataField] != e.component._options.value)
                                dataSourceWebService(e.component._options.value, cellInfo.rowIndex, false);
                            return;
                        }
                        grida.VendosVleraNeDataSource(cellInfo.rowIndex, dataField, idField, descriptionField, objekti, dataSourceTextField, dataSourceValueField, dataSourceDescField, onValueChangeFunction);
                    }
                },
                itemTemplate: function (data, index, container) {
                    if (listColumns && listColumns.length > 0)
                        grida.AddColumnsToAutocompleteList(data, index, container, dataSourceTextField, dataSourceDescField, listColumns);
                    else
                        return data[dataSourceTextField];
                }
            });
            container.find("input").css("width", "65%");
            container.find("input").parent().append($("<div style='width:35%;'>").dxButton({
                icon: "search",
                onClick: function (e) {
                    lupaFunction(cellInfo.rowIndex);
                    console.log("U hap lupa nga rreshti: " + cellInfo.rowIndex + " qeliza: " + dataField);
                }
            }));
        };
        this.Grida.columnOption(dataField, "editCellTemplate", autocomplete);
    };

    this.AddColumnsToAutocompleteList = function (data, index, container, dataSourceTextField, dataSourceDescField, listColumns) {
        container.addClass("bootstrap-iso");
        var row = $("<div>").addClass("row-flex");
        if (index == 0) {
            row.addClass("divTableHeading");
            for (var i = 0; i < listColumns.length; i++)
                $("<div>").addClass("col-sm-" + listColumns[i].width + " divTableCell").text(listColumns[i].capField).appendTo(row);
            container.append(row);
        }
        row = $("<div>").addClass("row-flex");

        for (var i = 0; i < listColumns.length; i++) {
            if (data.objekti && listColumns[i].dataField != dataSourceDescField && listColumns[i].dataField != dataSourceTextField)
                $("<div>").addClass("col-sm-" + listColumns[i].width).text(data.objekti[listColumns[i].dataField]).appendTo(row);
            else
                $("<div>").addClass("col-sm-" + listColumns[i].width).text(data[listColumns[i].dataField]).appendTo(row);
        }
        container.append(row);
    };

    this.ShtoRreshtBosh = function (object, dataField) {
        if (!this.Options.allowNewRowAdding)
            return;
        var datas = this.GetData();
        if (datas.filter(function (item) { return item[dataField] == null || item[dataField] == 0; }).length < 1) {
            datas.push(Utils.CloneObject(object));
            this.Refresh();
        }
    };

    this.AddCommands = function (columns, addEditCommand, addDeleteCommand, cancelEditDataCommand) {
        var grida = this;
        var buttons = new Array();

        if (addEditCommand) {
            buttons.push({
                icon: "edit",
                visible: true,
                onClick: function (e) {
                    if (e.row.isExpanded)
                        grida.Grida.collapseRow(e.row.key);
                    grida.Grida.editRow(e.row.rowIndex);
                }
            });
        }
        if (addDeleteCommand) {
            buttons.push({
                icon: "remove",
                visible: true,
                onClick: function (e) {
                    if (grida.Options.deleteMethod)
                        grida.Options.deleteMethod(grida, e.row.rowIndex);
                    else {
                        if (cancelEditDataCommand)
                            grida.Grida.cancelEditData(e.row.rowIndex);
                        grida.Grida.deleteRow(e.row.rowIndex);
                    }
                    e.event.preventDefault();
                }
            });
        }
        if (buttons.length > 0)
            columns.push({
                type: "buttons",
                width: 35 * buttons.length,
                buttons: buttons
            });
    };

    //merr summary te nje kolone
    this.GetSummaryOfColumns = function (dataFields, option) {
        var summaryValue = 0;
        var ds = this.GetData();
        switch (option) {
            case "sum":
                for (var i = 0; i < ds.length; i++)
                    for (var j = 0; j < dataFields.length; j++)
                        summaryValue += ds[i][dataFields[j]] ? ds[i][dataFields[j]] : 0;
                break;
            default:
                break;
        }
        return summaryValue;
    };

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

    //pastro gride
    this.Clean = function (defaultObjectStore) {
        this.SetDataSource(defaultObjectStore);
    };

    //pastro gride
    this.SetDataSource = function (dataSource) {
        this.Grida.option('dataSource', dataSource ? dataSource : new DevExpress.data.ArrayStore());
    };

    this.SetSelectionOnFocus = function (e) {
        if (e.parentType == "dataRow") {
            e.editorOptions.onFocusIn = function (args) {
                args.element.find("input").select();
            };
        }
    };

    this.VendosVleraNeDataSource = function (rowIndex, dataField, idField, descriptionField, objekti, dataSourceTextField, dataSourceValueField, dataSourceDescField, onValueChangeFunction) {
        var dataSource = this.GetData();
        var keyExpr = this.Options.keyExpr;
        var maxKeyExpr = Math.max.apply(Math, dataSource.map(function (item) { return item[keyExpr]; }));
        maxKeyExpr = maxKeyExpr ? maxKeyExpr + 1 : rowIndex;
        if (!objekti) {
            objekti = {};
            objekti[dataSourceTextField] = null;
            objekti[dataSourceValueField] = null;
            objekti[dataSourceDescField] = null;
        }
        if (dataField)
            dataSource[rowIndex][dataField] = objekti[dataSourceTextField];
        if (idField)
            dataSource[rowIndex][idField] = objekti[dataSourceValueField];
        if (descriptionField)
            dataSource[rowIndex][descriptionField] = objekti[dataSourceDescField];
        if (!dataSource[rowIndex][keyExpr])
            dataSource[rowIndex][keyExpr] = maxKeyExpr;
        if (onValueChangeFunction)
            onValueChangeFunction(rowIndex, objekti);
    };

    this.GetDataSourceFromWebservice = function (dataSourceWebService, rowIndex) {
        var ds = new DevExpress.data.CustomStore({
            load: function (loadOptions) {
                if (!loadOptions.searchValue)
                    return;
                var value = loadOptions.searchValue;
                return dataSourceWebService(value, rowIndex, true);
            },
            byKey: function (key) {
                return;
            }
        });
        return ds;
    };

    this.SetLastFocusedCell = function () {
        if (this.lastFocusedCell) {
            var cellElement = this.Grida.getCellElement(this.lastFocusedCell.rowIndex, this.lastFocusedCell.dataField);
            this.Grida.editCell(this.lastFocusedCell.rowIndex, this.lastFocusedCell.dataField);
            this.Grida.focus(cellElement);
            this.lastFocusedCell = null;
        }
    };

    this.GetColumnDataSourceItem = function (dataField, keyFieldValue) {
        var lookUpColumn = this.Grida.columnOption(dataField).lookup;
        if (!lookUpColumn)
            return null;
        var columDataSource = lookUpColumn.dataSource.store;
        return columDataSource.filter(function (item) { return item[lookUpColumn.valueExpr] == keyFieldValue; })[0];
    };

    this.PastroFusha = function (rowIndex, newRowValues, columns) {
        var dataSource = this.GetData();
        var column;
        for (var i = 0; i < columns.length; i++) {
            column = columns[i];
            dataSource[rowIndex][column] = newRowValues[column];
        }
        this.Refresh();
    };

    this.DeleteRow = function (rowIndex) {
        this.Grida.deleteRow(rowIndex);
        this.SaveCurrentValues();
    };

    this.GetColumns = function () {
        return this.Grida.option('columns').filter(function (column) { return (column.dataField && column.dataField != ""); });
    };

    this.SaveGridConfiguration = function (gridId, languageId, companyId, yearId, userId, filterCode, filterExp) {
        var visibleColumns = this.Grida.getVisibleColumns();
        var columns = this.GetColumns().map(function (column) {
            var col = visibleColumns.filter(function (visibleColumn) { return visibleColumn.dataField == column.dataField; })[0];
            if (!col)
                return { KodiTrupi: column.dataField, IndexTrupi: column.visibleIndex, VisibleTrupi: false, WidthTrupi: parseInt(column.width) };
            return { KodiTrupi: col.dataField, IndexTrupi: col.visibleIndex, VisibleTrupi: true, WidthTrupi: parseInt(col.width) };
        });

        var sortColumn = "", sortOrder = true;
        if (filterCode) {
            var column = this.Grida.getVisibleColumns().filter(function (column) { return column.sortOrder; });
            if (column && column.length > 0) {
                sortColumn = column.map(function (elem) {
                    return elem.dataField + " " + (elem.sortOrder == "asc" ? "Ascending" : "Descending");
                }).join(",");
            }
        }

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "RuajKolonaGrideDheFilter"),
            data: JSON.stringify({
                idGride: gridId, gridColumns: columns, idGjuha: languageId, idNdermarrje: companyId, idViti: yearId, idPerdoruesi: userId,
                kodFiltri: filterCode, filterExp: filterExp, koloneRenditje: sortColumn, renditja: sortOrder
            })
        }).done(function (result) { myMesazh.ShtoMesazhSesioni(result); });
    };

    this.SetFormEditingFieldsFromColumns = function (groupName) {
        this.Grida.option('editing.form.items', [{
            itemType: "group",
            caption: groupName,
            colCount: 4,
            colSpan: 4,
            items: this.GetColumns().filter(function (column) { return (column.allowEditing == true) })
        }]);
    };

    if (noInstanciate) {
        this.GridWidget = emerGride.dxDataGrid(this.Options);
        this.Grida = this.GridWidget.dxDataGrid("instance");
    }
    else
        this.Grida = $("#" + emerGride).dxDataGrid(this.Options).dxDataGrid("instance");
}