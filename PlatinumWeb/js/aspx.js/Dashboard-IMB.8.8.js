;
var pageState = {
    grida: null,
    usersGrid: null,
    dashboards: null,
    rowOptions: null,
    teDrejta: null,
    messages: {
        msg_D_btnSave: "",
        msg_D_btnSaveAs: "",
        msg_D_btnViewerMode: "",
        msg_D_btnManage: "",
        msg_D_btnRefresh: "",
        msg_D_btnChange: "" ,
        msg_D_btnCreate: "",
        msg_D_btnEdit: "",
        msg_D_btnShare: "",
        msg_D_btnDelete: "",
        msg_D_btnCancel: "",
        msg_D_columnTitle: "",
        msg_D_columnOwner: "",
        msg_D_columnRefreshTime: "",
        msg_D_columnUserName: "" ,
        msg_D_columnName: "",
        msg_D_columnLastName: "",
        msg_D_columnRole: "",
        msg_D_saveDefaultException: "",
        msg_D_saveEmptyNameException: "",
        msg_D_titleManager: "",
        msg_D_titleSharing: "",
        msg_D_confirmDeleteTitle: "",
        msg_D_confirmSaveTitle: "",
        msg_D_loseChanges: "",
        msg_D_saveChangesQuestion: "",
        msg_D_deleteQuestion: ""        
    }
}

function SetMessages() {
    pageState.messages.msg_D_btnSave = hfState.Get("msg_D_btnSave");
    pageState.messages.msg_D_btnSaveAs = hfState.Get("msg_D_btnSaveAs");
    pageState.messages.msg_D_btnViewerMode = hfState.Get("msg_D_btnViewerMode");
    pageState.messages.msg_D_btnManage = hfState.Get("msg_D_btnManage");
    pageState.messages.msg_D_btnRefresh = hfState.Get("msg_D_btnRefresh");
    pageState.messages.msg_D_btnChange = hfState.Get("msg_D_btnChange");
    pageState.messages.msg_D_btnCreate = hfState.Get("msg_D_btnCreate");
    pageState.messages.msg_D_btnEdit = hfState.Get("msg_D_btnEdit");
    pageState.messages.msg_D_btnShare = hfState.Get("msg_D_btnShare");
    pageState.messages.msg_D_btnDelete = hfState.Get("msg_D_btnDelete");
    pageState.messages.msg_D_btnCancel = hfState.Get("msg_D_btnCancel");//hfState.Get("msg_D_btnCancel");
    pageState.messages.msg_D_columnTitle = hfState.Get("msg_D_columnTitle");
    pageState.messages.msg_D_columnOwner = hfState.Get("msg_D_columnOwner");
    pageState.messages.msg_D_columnRefreshTime = hfState.Get("msg_D_columnRefreshTime");
    pageState.messages.msg_D_columnUserName = hfState.Get("msg_D_columnUserName");
    pageState.messages.msg_D_columnName = hfState.Get("msg_D_columnName");
    pageState.messages.msg_D_columnLastName = hfState.Get("msg_D_columnLastName");
    pageState.messages.msg_D_columnRole = hfState.Get("msg_D_columnRole");
    pageState.messages.msg_D_saveDefaultException = hfState.Get("msg_D_saveDefaultException");
    pageState.messages.msg_D_saveEmptyNameException = hfState.Get("msg_D_saveEmptyNameException");

    pageState.messages.msg_D_titleManager = hfState.Get("msg_D_titleManager");
    pageState.messages.msg_D_titleSharing = hfState.Get("msg_D_titleSharing");
    pageState.messages.msg_D_confirmDeleteTitle = hfState.Get("msg_D_confirmDeleteTitle");
    pageState.messages.msg_D_confirmSaveTitle = hfState.Get("msg_D_confirmSaveTitle");
    pageState.messages.msg_D_loseChanges = hfState.Get("msg_D_loseChanges");
    pageState.messages.msg_D_saveChangesQuestion = hfState.Get("msg_D_saveChangesQuestion");
    pageState.messages.msg_D_deleteQuestion = hfState.Get("msg_D_deleteQuestion");
}


function onBeforeRender(sender) {
    if (!sender)
        return;
    SetMessages();

    myFaqeCelje.changeName('Dashboard.aspx', 0, null);
    pageState.teDrejta = JSON.parse(hfState.Get("teDrejta"));
    var dashboardControl = sender.GetDashboardControl();
    dashboardControl.registerExtension(new CustomItems.OnlineMapItemExtension(dashboardControl, pageState.messages));
    dashboardControl.registerExtension(new SaveAsDashboardExtension(dashboardControl, pageState.messages));
    dashboardControl.registerExtension(new SaveDashboardExtension(dashboardControl, pageState.messages));

    DisableExtensions(dashboardControl);
    EditMessages();
    GetDashboardList(true);
}

function onDashboardTitleToolbarUpdated(s, e) {
    if (ASPxDashboard1.IsDesignMode()) {
        e.Options.actionItems.unshift({
            type: "button",
            icon: "viewerIcon",
            hint: pageState.messages.msg_D_btnViewerMode,
            click: EnsureDashboardIsSavedBeforeSwitching
        });
        return;
    }

    e.Options.actionItems.unshift({
        type: "button",
        icon: "toolsIcon",
        hint: pageState.messages.msg_D_btnManage,
        click: OpenDashboardManager
    });


    e.Options.actionItems.unshift({
        type: "button",
        icon: "refreshIcon",
        hint: pageState.messages.msg_D_btnRefresh,
        click: RefreshDashboard
    });

    e.Options.actionItems.unshift({
        type: "menu",
        icon: "itemsIcon",
        hint: pageState.messages.msg_D_btnChange,
        menu: {
            columnCount: 1,
            title: "Dashboards",
            items: pageState.dashboards ? pageState.dashboards.map(function (item) { return item.Name }) : [],
            type: 'list',
            selectionMode: 'single',
            data: pageState.dashboards ? pageState.dashboards : [],
            itemClick: function (data, element, index) {
                var dashboard = this.data[index];
                ASPxDashboard1.LoadDashboard(dashboard.IdDashboard);
                Timer.SetInterval(dashboard.RefreshTime * 60000);
            }
        }
    });
}

function RefreshDashboard() {
    ASPxDashboard1._dashboardControl.reloadData();
    GetDashboardList(true);
}

function CreateNewDashboard() {
    ASPxDashboard1.SwitchToDesigner();
    ASPxDashboard1._dashboardControl.isDesignMode(true);
    ASPxDashboard1.UpdateDashboardTitleToolbar();
    var toolbox = ASPxDashboard1._dashboardControl.findExtension("toolbox");
    toolbox.selectMenuItem(toolbox._findMenuItem("create-dashboard"));
    $('#dashboardManagerModal').modal('hide');
}

function EnsureDashboardIsSavedBeforeSwitching() {
    var saveExtension = ASPxDashboard1._dashboardControl.findExtension("dxdde-dashboard-save");
    if (saveExtension.IsDashboardDirty())
        return $("#dashboardSaveModal").modal("show");
    else
        SwitchBackToViewer();
}

function DoNotSaveDashboard() {
    ASPxDashboard1.LoadDashboard(ASPxDashboard1.GetDashboardId());
    SwitchBackToViewer();
}

function SaveDashboard() {
    var saveExtension = ASPxDashboard1._dashboardControl.findExtension("dxdde-dashboard-save");
    if (ASPxDashboard1.GetDashboardId() < 0) {
        myMesazh.ShtoMesazhGabimi(pageState.messages.msg_D_saveDefaultException);
        return;
    }
    saveExtension.SaveDashboard();
}

function SwitchBackToViewer() {
    ASPxDashboard1.SwitchToViewer();
    ASPxDashboard1._dashboardControl.isDesignMode(false);
    ASPxDashboard1.UpdateDashboardTitleToolbar();
}

function DisableExtensions(dashboardControl) {
    dashboardControl.unregisterExtension("dashboard-title-editor");
    dashboardControl.unregisterExtension("dashboard-parameter-editor");
    dashboardControl.unregisterExtension("open-dashboard");
    dashboardControl.unregisterExtension("save-dashboard");
    var dashboardToolbox = dashboardControl.findExtension("toolbox");
    if (dashboardToolbox)
        dashboardToolbox.removeToolboxItem("layout", "TabContainer");
}

function EditMessages() {
    DevExpress.JS.Localization.messages["DashboardWebStringId.Errors.AttemptToLoadData"] += ", please contact the dashboard owner or the administrator.";

    $("#exampleModalLongTitle").html(pageState.messages.msg_D_titleManager);
    $("#exampleModalSharingTitle").html(pageState.messages.msg_D_titleSharing);
    $("#exampleModalDeleteTitle").html(pageState.messages.msg_D_confirmDeleteTitle);
    $("#exampleModalSaveTitle").html(pageState.messages.msg_D_confirmSaveTitle);
    $("#modalSaveBody").html("<p>" + pageState.messages.msg_D_loseChanges + "<br />" + pageState.messages.msg_D_saveChangesQuestion +"</p>");
    $("#modalDeleteBody").html("<p>" + pageState.messages.msg_D_deleteQuestion + "</p>");

    $(".shareBtn").val(pageState.messages.msg_D_btnShare);
    $(".saveBtn").val(pageState.messages.msg_D_btnSave);
    $(".deleteBtn").val(pageState.messages.msg_D_btnDelete);
    $(".cancelBtn").val(pageState.messages.msg_D_btnCancel);
}

function ReloadData(s, e) {
    ASPxDashboard1._dashboardControl.reloadData();
}

function GetDashboardList(reloadMenuItems) {
    $.ajax({
        type: "GET",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Dashboard", "GetUserDashboards")
    }).done(function (result) {
        if (reloadMenuItems)
            UpdateToolbarItems(result);
        else
            OpenDashboardManagerSuccess(result);
    });
}

function OpenDashboardManager() {
    GetDashboardList(false);
}

function OpenDashboardManagerSuccess(result) {
    CreateDashboardManagerGride(result);
    $('#dashboardManagerModal').modal('show');
    $($('.modal-backdrop')[0]).css('z-index', 1);
    SetDashboardResizable('dashboardManagerModal');
}

function SetDashboardResizable(modalId) {
    var modalDialog = $('#' + modalId).find('.modal-dialog');
    var modalContent = $('#' + modalId).find('.modal-content');

    modalContent.css("cssText", "width: 100%; height:100%;");
    modalDialog.css("cssText", "top: 20%;");

    modalDialog.draggable({
        handle: ".modal-header"
    });

    $('#' + modalId).on('shown.bs.modal', function (e) {
        var element = modalContent;
        element.resizable({
            minHeight: element.height(),
            minWidth: element.width()
        });
    });
}

function CreateDashboardManagerGride(dataSource) {
    var idPerdoruesi = parseInt(hfState.Get("_idPerdoruesi"));
    pageState.grida = new myDxDataGrid("gvDashboard", {
        dataSource: {
            store: dataSource,
            sort: 'Rendi'
        },
        sorting: {
            mode: "none"
        },
        focusStateEnabled: false,
        columnResizingMode: "nextColumn",
        editing: {
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        showRowLines: true,
        selection: {
            mode: "none"
        },
        scrolling: {
            mode: "standart"
        },
        onContentReady: function (e) {
            initDragging(e.element);
        },
        onRowPrepared: function (e) {
            if (e.rowType != 'data')
                return;
            e.rowElement
                .addClass('myRow')
                .data('keyValue', e.key);
        },
        onEditorPreparing: function (e) {
            if (!e.parentType == "dataRow")
                return;
            if (e.dataField === "Name" || e.dataField === "RefreshTime") {
                e.editorOptions.disabled = (e.row.data.IdKrijuesi != idPerdoruesi);
                pageState.grida.SetSelectionOnFocus(e);
            }
        },
        onToolbarPreparing: function (e) {
            if (!pageState.teDrejta.DMod)
                return;
            var toolbarItems = e.toolbarOptions.items;
            toolbarItems.push({
                widget: "dxButton",
                options: { icon: "add", text: pageState.messages.msg_D_btnCreate, onClick: CreateNewDashboard },
                location: "before"
            });
        }
    });

    pageState.grida.SetColumnsFromConfig(pergatitKolona("dashboardManager"));
    pageState.grida.AddCustomOptionToColumns(["Rendi","Settings"], "width", "45");
    pageState.grida.AddCustomOptionToColumns(["Settings"], "cellTemplate", SettingsCellTemplate);
    pageState.grida.Grida.clearFilter();
}

function initDragging($gridElement) {
    $gridElement.find('.myRow').draggable({
        helper: 'clone',
        start: function (event, ui) {
            var $originalRow = $(this),
                $clonedRow = ui.helper;
            var $originalRowCells = $originalRow.children(),
                $clonedRowCells = $clonedRow.children();
            for (var i = 0; i < $originalRowCells.length; i++)
                $($clonedRowCells.get(i)).width($($originalRowCells.get(i)).width());
            $clonedRow
                .width($originalRow.width())
                .addClass('drag-helper');
        }
    });

    $gridElement.find('.myRow').droppable({
        drop: function (event, ui) {
            var draggingRowKey = ui.draggable.data('keyValue');
            var targetRowKey = $(this).data('keyValue');
            var draggingIndex = null,
                targetIndex = null;
            var store = pageState.grida.Grida.getDataSource().store();
            store.byKey(draggingRowKey).done(function (item) {
                draggingIndex = item.Rendi;
            });
            store.byKey(targetRowKey).done(function (item) {
                targetIndex = item.Rendi;
            });
            var draggingDirection = (targetIndex < draggingIndex) ? 1 : -1;
            var dataItems = null
            store.load().done(function (data) {
                dataItems = data;
            });
            for (var dataIndex = 0; dataIndex < dataItems.length; dataIndex++) {
                if ((dataItems[dataIndex].Rendi > Math.min(targetIndex, draggingIndex)) && (dataItems[dataIndex].Rendi < Math.max(targetIndex, draggingIndex))) {
                    dataItems[dataIndex].Rendi += draggingDirection;
                }
            }
            store.update(draggingRowKey, { Rendi: targetIndex });
            store.update(targetRowKey, { Rendi: targetIndex + draggingDirection });
            $gridElement.dxDataGrid('instance').refresh();
        }
    });
}

function SettingsCellTemplate(container, options) {
    var items = new Array();
    var isOwner = (options.data.IdKrijuesi == parseInt(hfState.Get("_idPerdoruesi")));
    if (pageState.teDrejta.DMod && (isOwner || options.data.IdDashboard < 0))
        items.push({ text: pageState.messages.msg_D_btnEdit });

    if (pageState.teDrejta.DEksporto && isOwner && options.data.IdDashboard > 0)
        items.push({ text: pageState.messages.msg_D_btnShare });

    if (pageState.teDrejta.DFsh && options.data.IdDashboard > 0)
        items.push({ text: pageState.messages.msg_D_btnDelete });

    $('<div>').appendTo(container).dxMenu({
        items: [{
            icon: "preferences",
            items: items
        }],
        showFirstSubmenuMode: 'onClick',
        hideSubmenuOnMouseLeave: true,
        onItemClick: function (e) {
            if (e.itemData.text)
                DoSettingsAction(e.itemData.text, options);
        },
    });
}

function DoSettingsAction(action, options) {
    switch (action) {
        case pageState.messages.msg_D_btnEdit:
            ASPxDashboard1._dashboardControl.isDesignMode(true);
            ASPxDashboard1.LoadDashboard(options.data.IdDashboard);
            ASPxDashboard1.SwitchToDesigner();
            $('#dashboardManagerModal').modal('hide');
            break;
        case pageState.messages.msg_D_btnShare:
            pageState.rowOptions = options;
            OpenDashboardSharing(options);
            break;
        case pageState.messages.msg_D_btnDelete:
            pageState.rowOptions = options;
            $("#dashboardDeleteModal").modal("show");
            $($('.modal-backdrop')[1]).css('z-index', 3);
            break;
    }
}

function OpenDashboardSharing(options) {
    $.ajax({
        type: "POST",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Dashboard", "GetDashboardUsers"),
        data: JSON.stringify({ idDashboard: options.data.IdDashboard })
    }).done(OpenDashboardSharingSuccess);
}

function DeleteDashboard() {
    var options = pageState.rowOptions;
    Utils.shfaqLoadingGif();
    $.ajax({
        type: "POST",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Dashboard", "DeleteDashboard"),
        data: JSON.stringify({ idDashboard: options.data.IdDashboard })
    }).done(
        function (result) {
            if (result.Status) {
                pageState.grida.Grida.deleteRow(options.rowIndex);
                UpdateToolbarItems(pageState.grida.GetData());
                myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
            }
            else
                myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
            Utils.hiqLoadingGif()
        }
    );
}

function OpenDashboardSharingSuccess(result) {
    pageState.usersGrid = new myDxDataGrid("gvUsers", {
        dataSource: result,
        paging: {
            enabled: true,
            pageSize: 10,
            pageIndex: 0
        },
        scrolling: {
            mode: "standard"
        },
        focusStateEnabled: false,
        columnResizingMode: "nextColumn",
        editing: {
            mode: "cell",
            allowUpdating: true
        },
        sorting: {
            mode: "none"
        },
        filterRow: {
            visible: true
        },
        searchPanel: {
            visible: false
        },
        showRowLines: true
    });

    pageState.usersGrid.SetColumnsFromConfig(pergatitKolona("userSharing"));
    pageState.usersGrid.AddCustomOptionToColumns(["CHECKED"], "width", "55");
    pageState.usersGrid.AddCustomOptionToColumns(["CHECKED"], "headerCellTemplate", CheckBoxHeaderCellTemplate);
    pageState.usersGrid.Grida.clearFilter();

    $('#dashboardSharingModal').modal('show');
    $($('.modal-backdrop')[1]).css('z-index', 3);
    SetDashboardResizable('dashboardSharingModal');    
}

function pergatitKolona(type) {
    var columns = new Array();
    switch (type) {
        case "dashboardManager":
            columns.push({ KodiTrupi: "Rendi", PershkrimiTrupi: "", ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "Name", PershkrimiTrupi: pageState.messages.msg_D_columnTitle, ReadonlyTrupi: !pageState.teDrejta.DMod, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "Krijuesi", PershkrimiTrupi: pageState.messages.msg_D_columnOwner, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "RefreshTime", PershkrimiTrupi: pageState.messages.msg_D_columnRefreshTime, ReadonlyTrupi: !pageState.teDrejta.DMod, VisibleTrupi: true });
            if (pageState.teDrejta.DMod || pageState.teDrejta.DEksporto || pageState.teDrejta.DFsh)
                columns.push({ KodiTrupi: "Settings", PershkrimiTrupi: "", ReadonlyTrupi: true, VisibleTrupi: true });
            break;
        case "userSharing":
            columns.push({ KodiTrupi: "CHECKED", PershkrimiTrupi: "", ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "boolean", sortOrder:"desc" });
            columns.push({ KodiTrupi: "USERNAME", PershkrimiTrupi: pageState.messages.msg_D_columnUserName, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "NAME", PershkrimiTrupi: pageState.messages.msg_D_columnName, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "LASTNAME", PershkrimiTrupi: pageState.messages.msg_D_columnLastName, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "ROLE", PershkrimiTrupi: pageState.messages.msg_D_columnRole, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            break;
    }
    return columns;
}

function ShareDashboard() {
    var idPerdoruesish = pageState.usersGrid.GetData().filter(function (item) { return item.CHECKED == 1; }).map(function (item) { return item.USERID; }).join(',');
    var options = pageState.rowOptions;
    Utils.shfaqLoadingGif()
    $.ajax({
        type: "POST",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Dashboard", "ShareDashboard"),
        data: JSON.stringify({ idDashboard: options.data.IdDashboard, idPerdoruesish: idPerdoruesish })
    }).done(
        function (result) {
            if (result.Status) {
                myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
                $('#dashboardSharingModal').modal('hide');
            }
            else
                myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
            Utils.hiqLoadingGif()
        }
    );
}

function CheckBoxHeaderCellTemplate(container, options) {
    container.parent().css("padding-left", 0);
    container.parent().css("padding-right", 0);
    container.dxCheckBox({
        onValueChanged: function (args) {
            pageState.usersGrid.GetData().map(function (item) { item.CHECKED = args.value });
            pageState.usersGrid.Refresh();
        },
        value: !(pageState.usersGrid.GetData().filter(function (item) { return item.CHECKED == 0; }).length > 0)
    })
    .on("dxclick", function (e) {
        e.stopPropagation();
    });
}

function SaveDashboardsChanges() {
    Utils.shfaqLoadingGif();
    pageState.grida.SaveCurrentValues();
    var gridData = pageState.grida.GetData();
    $.ajax({
        type: "POST",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Dashboard", "SaveDashboardsChanges"),
        data: JSON.stringify({ dashboards: gridData })
    }).done(
        function (result) {
            if (result.Status) {
                myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
                UpdateToolbarItems(gridData);
                $('#dashboardManagerModal').modal('hide');
            }
            else
                myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
            Utils.hiqLoadingGif()
        }
    );
}

function UpdateToolbarItems(dashboardCollection) {
    pageState.dashboards = dashboardCollection.sort(function (item1, item2) { return item1.Rendi - item2.Rendi; });
    ASPxDashboard1.UpdateDashboardTitleToolbar();
}

function onDashboardEndCallback(s, e) {
    if (s.cpDashboardMessage) {
        ShowDashboardMessage(s, s.cpDashboardMessage);
        switch (s._dashboardControl.data) {
            case "save":
                SwitchBackToViewer();
                break;
            case "save-as":
                GetDashboardList(true);
                DoNotSaveDashboard();
                break;
        }
        s._dashboardControl.data = "";
    }
}

function onDashboardCallbackError(s, e) {
    console.log(e.message);
    e.handled = true;
}

function onDashboardChanged(s, e) {
    ReloadData(s, e);
}

function onDashboardBeginCallback(s, e) {
    $(".dx-dashboard-notificator").css("visibility", "unset");
}

function ShowDashboardMessage(s, message) {
    s.cpDashboardMessage = "";

    var parsedMessage = JSON.parse(message);
    if (parsedMessage.Status) {
        myMesazh.ShtoMesazhSuksesi(parsedMessage.PershkrimMesazhi);
        return;
    }
    $(".dx-dashboard-notificator").css("visibility", "hidden");

    myMesazh.ShtoMesazhGabimi(parsedMessage.PershkrimMesazhi);
    s._dashboardControl.data = "";
}